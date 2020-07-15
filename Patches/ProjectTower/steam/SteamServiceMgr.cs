using MonoMod;
using MonoMod.RuntimeDetour;
using Steamworks;

namespace TAS.Patches.ProjectTower.steam
{
    // Necessary to remove steam integration for deterministic gameplay
    [MonoModPatch("global::ProjectTower.steam.SteamServiceMgr")]
    public class SteamServiceMgr : global::ProjectTower.steam.SteamServiceMgr
    {
        static SteamServiceMgr()
        {
            new Detour
            (
                typeof(SteamAPI).GetMethod(nameof(SteamAPI.Init)),
                typeof(SteamServiceMgr).GetMethod(nameof(KillSteam))
            );
        }

        public static bool KillSteam() => false;
    }
}
