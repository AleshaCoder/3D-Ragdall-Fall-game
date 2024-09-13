using UnityEngine;

namespace Assets.Source.GravityGunSystem.Scripts
{
    public interface IGrabbable
    {
        public Rigidbody Rigidbody { get; }
    }
}