using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
using MonsterEdit.monsters;
using ProjectTower.character.def;
using TAS.Patches.ProjectTower.character;
using TAS.Patches.ProjectTower.player;
using xCharEdit.Character;

namespace TAS
{
    public partial class InfoWindow : Form
    {
        public static bool NeedsClose { get; set; }

        protected override bool ShowWithoutActivation => true;

        public InfoWindow()
        {
            InitializeComponent();
            Application.Idle += Update;
        }

        private void Update(object sender, System.EventArgs e)
        {
            frameLabel.Text = "Frame: " + InputManager.CurrentFrame;

            var c = CharMgr.GetPlayerCharacter(PlayerMgr.GetMainPlayer());
            xLabel.Text = "Pos X: " + c?.loc.X;
            yLabel.Text = "Pos Y: " + c?.loc.Y;
            xSpeedLabel.Text = "Spd X: " + c?.traj.X;
            ySpeedLabel.Text = "Spd Y: " + c?.traj.Y;

            MouseState mouse = InputManager.GetActualMouse();
            mouseXLabel.Text = "Pos X: " + mouse.X;
            mouseYLabel.Text = "Pos Y: " + mouse.Y;

            var charAnim = c?.anim;
            animNameLabel.Text = "Name: " + charAnim?.animName;
            animKeyLabel.Text = "Key: " + charAnim?.key;
            animFrameLabel.Text = "Frame: " + charAnim?.frame;

            KeyFrame frame;
            try
            {
                MonsterDef mDef = MonsterCatalog.catalog[c.monsterIdx];
                CharDef charDef = CharDefMgr.charDefList[mDef.defIdx];
                Animation anim = charDef.animation[charAnim.anim];
                frame = anim.keyFrame[charAnim.key];
            }
            catch
            {
                frame = null;
            }

            animFrameDurationLabel.Text = "Duration: " + frame?.duration;

            if (NeedsClose)
            {
                NeedsClose = false;
                Close();
            }
            else
            {
                Invalidate();
            }
        }
    }
}
