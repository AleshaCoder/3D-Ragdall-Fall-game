using UnityEngine;

namespace Assets.Source.Extensions.Scripts
{
    public static class Vector3Extenension
    {
        public static float DistanceTo(this Vector3 origin, Vector3 destination)
        {
            var distance = destination - origin;
            return distance.sqrMagnitude;
        }

        public static Vector3 Flat(this Vector3 origin) => new Vector3(origin.x, 0f, origin.z);

        public static Vector3 DirectionTo(this Vector3 origin, Vector3 destination) => (destination - origin).normalized;

        public static Vector3 With(this Vector3 origin, float? x = null, float? y = null, float? z = null) => new Vector3(x ?? origin.x, y ?? origin.y, z ?? origin.z);
    }
}
