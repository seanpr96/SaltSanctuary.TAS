using System;
using System.Diagnostics;
using System.Reflection;

namespace TAS.Utils
{
    /// <summary>
    /// Helper class that logs all random calls using the <see cref="Logger"/> class
    /// </summary>
    public class RandomWithLogging : Random
    {
        public RandomWithLogging() : base() { }
        public RandomWithLogging(int seed) : base(seed) { }

        public override int Next()
        {
            int result = base.Next();
            Logger.Log($"{InputManager.CurrentFrame}: Next() = {result}");
            return result;
        }

        public override int Next(int maxValue)
        {
            int result = base.Next(maxValue);
            Logger.Log($"{InputManager.CurrentFrame}: Next({maxValue}) = {result}");
            return result;
        }

        public override int Next(int minValue, int maxValue)
        {
            int result = base.Next(minValue, maxValue);
            Logger.Log($"{InputManager.CurrentFrame}: Next({minValue}, {maxValue}) = {result}");
            return result;
        }

        public override void NextBytes(byte[] buffer)
        {
            base.NextBytes(buffer);
            Logger.Log($"{InputManager.CurrentFrame}: NextBytes(byte[]) = {BitConverter.ToString(buffer)}");
        }

        public override double NextDouble()
        {
            double result = base.NextDouble();
            Logger.Log($"{InputManager.CurrentFrame}: NextDouble() = {result}");
            return result;
        }

        private MethodBase GetCaller()
        {
            StackTrace stack = new StackTrace();
            for (int i = 0; i < stack.FrameCount; i++)
            {
                MethodBase caller = stack.GetFrame(i).GetMethod();
                if (caller.DeclaringType != typeof(RandomWithLogging)
                    && caller.DeclaringType != typeof(Random)
                    && caller.DeclaringType != typeof(Patches.ProjectTower.Rand))
                {
                    return caller;
                }
            }

            return null;
        }
    }
}
