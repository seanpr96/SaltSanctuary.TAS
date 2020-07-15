using MonoMod;
using TAS.Attributes;

namespace TAS.Patches.ProjectTower.storage
{
    [MonoModPatch("global::ProjectTower.storage.StorageJob")]
    public class StorageJob : global::ProjectTower.storage.StorageJob
    {
        [MonoModIgnore] public StorageJob() : base(0, 0, 0) { }

        [MonoModIgnore]
        [RemoveSleep]
        private extern void ThreadedWork();

        // Normally starts a new thread, necessary to prevent that for deterministic gameplay
        public new void Work() => ThreadedWork();
    }
}
