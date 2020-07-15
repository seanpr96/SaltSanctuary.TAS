using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using TAS.Attributes;

namespace TAS.Patches.AudioEdit.sfx
{
    // Audio causes desyncs when fast forwarding due to a cap of 64 concurrent sounds
    // Easiest solution is to use a different Random, no reason sound has to be deterministic
    // Maybe change this at some point? Modifying the rng to make the TAS sync is sketchy
    [MonoModPatch("global::AudioEdit.sfx.BankCue")]
    [BeforeFieldInit(false)]
    public class BankCue : global::AudioEdit.sfx.BankCue
    {
        private static readonly MethodInfo RandIntInfo = typeof(BankCue)
            .GetMethod(nameof(GetRandomInt), BindingFlags.Static | BindingFlags.NonPublic);

        private static readonly Random NonSeededRand = new Random();
        
        static BankCue()
        {
            List<MethodInfo> randMethods = typeof(BankCue)
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(m => m.Name == "Play").ToList();

            randMethods.Add(typeof(BankCue)
                .GetMethod("GetInstance", BindingFlags.Instance | BindingFlags.NonPublic));

            foreach (MethodInfo info in randMethods)
            {
                new ILHook(info, UnseedRandom);
            }
        }

        private static void UnseedRandom(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            while (c.TryGotoNext(i => i.MatchCall(typeof(ProjectTower.Rand), "GetRandomInt")))
            {
                c.Remove();
                c.Emit(OpCodes.Call, RandIntInfo);
            }
        }

        private static int GetRandomInt(int min, int max)
        {
            return NonSeededRand.Next(min, max);
        }
    }
}
