using System;
using System.Threading;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;

namespace TAS.Attributes
{
    /// <summary>
    /// Removes all calls to Thread.Sleep in the target method
    /// </summary>
    [MonoModCustomAttribute("RemoveSleep")]
    [AttributeUsage(AttributeTargets.Method)]
    public class RemoveSleep : Attribute
    {
        public RemoveSleep() { }
    }
}

namespace MonoMod
{
    static partial class MonoModRules
    {
        public static void RemoveSleep(MethodDefinition method, CustomAttribute attrib)
        {
            if (!method.HasBody)
            {
                return;
            }

            ILCursor c = new ILCursor(new ILContext(method));
            while (c.TryGotoNext(i => i.MatchCall(typeof(Thread), nameof(Thread.Sleep))))
            {
                // Replacing the sleep with a pop preserves any effects of logic used to generate the sleep time
                // Also much easier to program this way
                c.Next.OpCode = OpCodes.Pop;
            }
        }
    }
}
