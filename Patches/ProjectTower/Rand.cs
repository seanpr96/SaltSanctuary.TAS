using System;
using MonoMod;

namespace TAS.Patches.ProjectTower
{
    // This patch only serves to make Rand.rand accessible within the TAS project
    [MonoModPatch("global::ProjectTower.Rand")]
    internal class Rand
    {
        [MonoModIgnore] public static Random rand;
    }
}
