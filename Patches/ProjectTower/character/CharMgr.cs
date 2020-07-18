using MonoMod;
using ProjectTower.character;
using ProjectTower.player;

namespace TAS.Patches.ProjectTower.character
{
    public class CharMgr : global::ProjectTower.character.CharMgr
    {
        [MonoModIgnore]
        public static extern Character GetPlayerCharacter(Player player);
    }
}
