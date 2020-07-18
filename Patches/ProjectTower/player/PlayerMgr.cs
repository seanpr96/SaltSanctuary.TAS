using MonoMod;
using ProjectTower.player;

namespace TAS.Patches.ProjectTower.player
{
    [MonoModPatch("global::ProjectTower.player.PlayerMgr")]
    public class PlayerMgr : global::ProjectTower.player.PlayerMgr
    {
        [MonoModIgnore]
        public static extern Player GetMainPlayer();
    }
}
