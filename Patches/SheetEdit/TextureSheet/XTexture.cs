using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using TAS.Attributes;

namespace TAS.Patches.SheetEdit.TextureSheet
{
    // Textures are normally loaded in a separate thread
    // All threading must be removed to make each frame completely deterministic
    [MonoModPatch("global::SheetEdit.TextureSheet.XTexture")]
    [BeforeFieldInit(false)]
    public class XTexture : global::SheetEdit.TextureSheet.XTexture
    {
        static XTexture()
        {
            new ILHook
            (
                typeof(XTexture).GetMethod(nameof(IsLoaded)),
                RemoveThreading
            );
        }

        private static void RemoveThreading(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            c.GotoNext(i => i.MatchLdloc(1), i => i.OpCode == OpCodes.Callvirt);
            c.RemoveRange(2);

            c.Emit(OpCodes.Ldarg_0);
            c.Emit(OpCodes.Call, typeof(XTexture).GetMethod(nameof(ThreadedLoader),
                BindingFlags.Instance | BindingFlags.NonPublic));

            c.IL.Body.ExceptionHandlers.Clear();
        }

        [MonoModIgnore]
        public new extern bool IsLoaded();

        [MonoModIgnore]
        [RemoveSleep]
        private extern void ThreadedLoader();
    }
}
