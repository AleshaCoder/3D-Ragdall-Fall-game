using UnityEngine;

namespace TimeSystem
{
    public static class TimeService
    {
        public static float Scale = 1f;
        public static float Delta => Time.deltaTime * Scale;
        public static float FixedDelta => Time.fixedDeltaTime;
    }
}
