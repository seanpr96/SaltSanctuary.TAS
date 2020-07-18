using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Xna.Framework;
using MonoMod;
using MonoMod.RuntimeDetour;
using ProjectTower;
using ProjectTower.config;
using ProjectTower.gamestate;
using TAS.Utils;

namespace TAS.Patches.ProjectTower
{
    [MonoModPatch("global::ProjectTower.Game1")]
    public class Game1 : global::ProjectTower.Game1
    {
        private static readonly PropertyInfo GameMinimized = typeof(GameWindow)
            .GetProperty("IsMinimized", BindingFlags.Instance | BindingFlags.NonPublic);

        private GameClockWrapper _clock;
        private bool _fastForward;
        private bool _paused;
        private bool _advanceFrame;

        /// <summary>
        /// Gets the currently executing game
        /// </summary>
        public static Game1 Instance { get; private set; }

        /// <summary>
        /// If true, the game will run as fast as it is capable. If false, it caps to 60 fps
        /// </summary>
        public bool FastForward
        {
            get => _fastForward;
            set
            {
                if (_fastForward && !value)
                {
                    AdvanceClock();
                }

                _fastForward = value;
                if (_fastForward)
                {
                    Paused = false;
                }
            }
        }

        /// <summary>
        /// If true, the game will stop all update/draw cycles
        /// </summary>
        public bool Paused
        {
            get => _paused;
            set
            {
                if (_paused && !value)
                {
                    AdvanceClock();
                }

                _paused = value;
                if (_paused)
                {
                    FastForward = false;
                }
            }
        }

        static Game1()
        {
            // Override update/draw calls
            new NativeDetour
            (
                typeof(Game).GetMethod(nameof(Game.Tick)),
                typeof(Game1).GetMethod(nameof(Tick), BindingFlags.Instance | BindingFlags.NonPublic)
            );

            // Kill non-deterministic draw calls coming from winforms
            new NativeDetour
            (
                typeof(Game).GetMethod("Paint", BindingFlags.Instance | BindingFlags.NonPublic),
                typeof(Game1).GetMethod(nameof(KillEvent), BindingFlags.Instance | BindingFlags.NonPublic)
            );

            // Stop the game from knowing if you tab out
            new NativeDetour
            (
                typeof(Game).GetMethod("HostDeactivated", BindingFlags.Instance | BindingFlags.NonPublic),
                typeof(Game1).GetMethod(nameof(KillEvent), BindingFlags.Instance | BindingFlags.NonPublic)
            );
        }

        // Instance assignments in the field initializer don't get patched in by monomod
        // Very important to remember to do them here if necessary
        [MonoModConstructor]
        public Game1()
        {
            if (Config.StartPaused)
            {
                AdvanceFrame();
            }

            IsMouseVisible = true;
        }

        /// <summary>
        /// Causes the game to advance forward by a single frame
        /// </summary>
        public void AdvanceFrame()
        {
            Paused = true;
            _advanceFrame = true;
        }

        /// <summary>
        /// Forcefully advances the game clock to the current time, effectively ignoring one iteration of elapsed time.
        /// </summary>
        public void AdvanceClock()
        {
            _clock.UpdateElapsedTime();
            _clock.AdvanceFrameTime();
        }

        private new void Tick()
        {
            // Can't draw when the game is minimized, it causes a crash
            // Best alternative is to freeze the game, running only update will cause desync
            if (ReflectionHelper.GetAttr<Game, bool>(this, "exitRequested")
                || (bool)GameMinimized.GetValue(Window, new object[0]))
            {
                // Advance clock to prevent fast forward when game is tabbed back in
                AdvanceClock();
                return;
            }

            // Allow other TAS components to run update logic
            OnTick?.Invoke();

            if (Paused && !_advanceFrame)
            {
                return;
            }

            // Calculate number of updates necessary for 60 fps, if not fast forwarding
            // This section is almost 100% copied from vanilla xna code
            int numTicks = 1;
            if (!FastForward && !_advanceFrame)
            {
                _clock.UpdateElapsedTime();
                TimeSpan elapsed = _clock.ElapsedAdjustedTime;
                if (elapsed < TimeSpan.Zero)
                {
                    elapsed = TimeSpan.Zero;
                }

                TimeSpan max = ReflectionHelper.GetAttr<Game, TimeSpan>(this, "maximumElapsedTime");
                if (elapsed > max)
                {
                    elapsed = max;
                }

                if (Math.Abs(elapsed.Ticks - TargetElapsedTime.Ticks) < TargetElapsedTime.Ticks >> 6)
                {
                    elapsed = TargetElapsedTime;
                }

                elapsed += ReflectionHelper.GetAttr<Game, TimeSpan>(this, "accumulatedElapsedGameTime");
                numTicks = (int)(elapsed.Ticks / TargetElapsedTime.Ticks);
                if (numTicks != 0)
                {
                    _clock.AdvanceFrameTime();
                    ReflectionHelper.SetAttr<Game, TimeSpan>(this, "accumulatedElapsedGameTime", elapsed);
                }
            }

            // Do update/draw cycles
            for (; numTicks > 0; numTicks--)
            {
                TimeSpan totalGameTime = ReflectionHelper.GetAttr<Game, TimeSpan>(this, "totalGameTime");
                GameTime gt = new GameTime(totalGameTime, TargetElapsedTime, false);
                ReflectionHelper.SetAttr<Game, GameTime>(this, "gameTime", gt);

                if (!ReflectionHelper.GetAttr<Game, bool>(this, "exitRequested"))
                {
                    try
                    {
                        OnUpdate?.Invoke(gt);
                        Update(gt);
                    }
                    finally
                    {
                        // If we run this while fast forwarding it causes the game to freeze for a very long time when set to normal speed
                        if (!FastForward && !_advanceFrame)
                        {
                            TimeSpan elapsed = ReflectionHelper.GetAttr<Game, TimeSpan>(this, "accumulatedElapsedGameTime");
                            ReflectionHelper.SetAttr<Game, TimeSpan>(this, "accumulatedElapsedGameTime", elapsed - TargetElapsedTime);
                        }

                        ReflectionHelper.SetAttr<Game, TimeSpan>(this, "totalGameTime", totalGameTime + TargetElapsedTime);
                    }

                    // Wait until drawing is possible to ensure 1 update = 1 draw always
                    while (!BeginDraw())
                    {
                        Thread.Sleep(10);
                    }

                    Draw(gt);
                    EndDraw();
                    ReflectionHelper.SetAttr<Game, bool>(this, "doneFirstDraw", true);
                }
            }

            _advanceFrame = false;
        }

        public extern void orig_Initialize();

        protected override void Initialize()
        {
            Instance = this;
            HotkeyHandler.Initialize();
            InputManager.Initialize();

            orig_Initialize();

            _clock = new GameClockWrapper(this, "clock", typeof(Game));

            // Ensure default binds on game launch for consistency
            for (int i = 0; i < InputMgr.inputProfile.Length; i++)
            {
                InputMgr.inputProfile[i]?.Reset();
            }

            // Rename all saves
            for (int i = 0; i <= 9; i++)
            {
                string save = $"./savedata/dat{i}.slv";
                if (File.Exists(save))
                {
                    BackupFile(save);
                }
            }
        }

        // Copied from vanilla code, except with threading removed for consistency
        protected override void LoadContent()
        {
            Rand.rand = new Random(Config.Seed);
            Logger.Log("Initialized random!");

            SpriteTools.Init(GraphicsDevice);
            CreateTargs();

            typeof(Loader).GetMethod("Init", BindingFlags.Static | BindingFlags.NonPublic)
                .Invoke(null, new object[] { GraphicsDevice, Content });

            ThreadedContentLoad();
        }

        private static void BackupFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }

            string backupName = fileName + ".bak";
            if (File.Exists(backupName))
            {
                BackupFile(backupName);
            }

            File.Move(fileName, backupName);
        }

        public extern void orig_OnExiting(object sender, EventArgs args);

        protected override void OnExiting(object sender, EventArgs args)
        {
            // Delete TAS saves, undo backups
            for (int i = 0; i <= 9; i++)
            {
                string fileName = $"./savedata/dat{i}.slv";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                string backupName = fileName + ".bak";
                if (File.Exists(backupName))
                {
                    File.Move(backupName, fileName);
                }
            }

            // Close the info window
            InfoWindow.NeedsClose = true;

            orig_OnExiting(sender, args);
        }

        public delegate void TickEvent();

        /// <summary>
        /// Called at the beginning of every application loop. Should not be used for anything tied to frames.
        /// </summary>
        public event TickEvent OnTick;

        public delegate void UpdateEvent(GameTime gameTime);

        /// <summary>
        /// Called immediately before every game update
        /// </summary>
        public event UpdateEvent OnUpdate;

        // Dummy method used for event detours
        private void KillEvent(object sender, EventArgs e) { }
    }
}
