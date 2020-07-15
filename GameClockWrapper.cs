using System;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace TAS
{
    // This may be the worst code I've ever written
    // TODO: Replace reflection with something faster
    /// <summary>
    /// Wrapper for the inacessible XNA class GameClock
    /// </summary>
    public class GameClockWrapper
    {
        private static readonly Type ClockType = Type.GetType(typeof(Color).AssemblyQualifiedName.Replace(".Color", ".GameClock"));

        // Static properties
        private static readonly PropertyInfo CounterInfo = ClockType?.GetProperty("Counter", BindingFlags.Static | BindingFlags.NonPublic);
        private static readonly PropertyInfo FrequencyInfo = ClockType?.GetProperty("Frequency", BindingFlags.Static | BindingFlags.NonPublic);

        // Instance properties
        private static readonly PropertyInfo CurrentTimeInfo = ClockType?.GetProperty("CurrentTime", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly PropertyInfo ElapsedTimeInfo = ClockType?.GetProperty("ElapsedTime", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly PropertyInfo ElapsedAdjustedTimeInfo = ClockType?.GetProperty("ElapsedAdjustedTime", BindingFlags.Instance | BindingFlags.NonPublic);

        // Instance methods
        private static readonly MethodInfo ResetInfo = ClockType?.GetMethod("Reset", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo UpdateElapsedTimeInfo = ClockType?.GetMethod("UpdateElapsedTime", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo AdvanceFrameTimeInfo = ClockType?.GetMethod("AdvanceFrameTime", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo SuspendInfo = ClockType?.GetMethod("Suspend", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo ResumeInfo = ClockType?.GetMethod("Resume", BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly object _clock;

        static GameClockWrapper()
        {
            if (ClockType == null)
            {
                throw new NullReferenceException("Failed to find GameClock type");
            }
        }

        public GameClockWrapper(object obj, string clockFieldName, Type objType = null, bool isStatic = false)
        {
            if (objType == null)
            {
                objType = obj?.GetType();
            }

            FieldInfo fi = objType?.GetField(clockFieldName,
                BindingFlags.Public | BindingFlags.NonPublic | (isStatic ? BindingFlags.Static : BindingFlags.Instance));

            if (fi?.FieldType != ClockType || (_clock = fi.GetValue(obj)) == null)
            {
                throw new ArgumentException($"Could not locate field '{clockFieldName}' on type '{objType}', or it is not a GameClock");
            }
        }

        public static long Counter => (long)CounterInfo.GetValue(null, new object[0]);
        public static long Frequency => (long)FrequencyInfo.GetValue(null, new object[0]);

        public TimeSpan CurrentTime => (TimeSpan)CurrentTimeInfo.GetValue(_clock, new object[0]);
        public TimeSpan ElapsedTime => (TimeSpan)ElapsedTimeInfo.GetValue(_clock, new object[0]);
        public TimeSpan ElapsedAdjustedTime => (TimeSpan)ElapsedAdjustedTimeInfo.GetValue(_clock, new object[0]);

        public void Reset() => ResetInfo.Invoke(_clock, new object[0]);
        public void UpdateElapsedTime() => UpdateElapsedTimeInfo.Invoke(_clock, new object[0]);
        public void AdvanceFrameTime() => AdvanceFrameTimeInfo.Invoke(_clock, new object[0]);
        public void Suspend() => SuspendInfo.Invoke(_clock, new object[0]);
        public void Resume() => ResumeInfo.Invoke(_clock, new object[0]);
    }
}
