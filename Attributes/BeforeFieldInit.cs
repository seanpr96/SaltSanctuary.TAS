using System;
using Mono.Cecil;
using MonoMod;

namespace TAS.Attributes
{
    /// <summary>
    /// Adds or removes the beforefieldinit flag. Usually necessary to be false on classes with a static constructor
    /// </summary>
    [MonoModCustomAttribute("BeforeFieldInit")]
    [AttributeUsage(AttributeTargets.Class)]
    public class BeforeFieldInit : Attribute
    {
        public BeforeFieldInit(bool beforeFieldInit) { }
    }
}

namespace MonoMod
{
    static partial class MonoModRules
    {
        public static void BeforeFieldInit(TypeDefinition type, CustomAttribute attrib)
        {
            type.IsBeforeFieldInit = (bool)attrib.ConstructorArguments[0].Value;
        }
    }
}
