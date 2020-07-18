using MonoMod;
using Microsoft.Xna.Framework.Graphics;

namespace TAS.Patches.ProjectTower.config
{
    [MonoModPatch("global::ProjectTower.config.ConfigMgr")]
    public class ConfigMgr : global::ProjectTower.config.ConfigMgr
    {
        public static extern void orig_Read(GraphicsDevice dev);

        public static new void Read(GraphicsDevice dev)
        {
            orig_Read(dev);
            vSync = 0;
        }
    }
}
