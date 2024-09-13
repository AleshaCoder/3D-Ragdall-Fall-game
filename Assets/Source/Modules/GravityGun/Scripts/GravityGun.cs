using UnityEngine;

namespace Assets.Source.GravityGunSystem.Scripts
{
    public class GravityGun
    {
        private const float MaxForce = 100f;

        private float _throwForce;
        private float _dragForce;
        private Vector3 _direction;

        internal GravityGun(float throwForce, float dragForce)
        {
            _dragForce = dragForce;
            _throwForce = throwForce;
        }

        internal void Drag(Rigidbody grabbedObject, Vector3 targetPosition)
        {
            _direction = targetPosition - grabbedObject.position;
            grabbedObject.velocity = _direction.sqrMagnitude * _dragForce * _direction.normalized;

            if (grabbedObject.velocity.sqrMagnitude > MaxForce * MaxForce)
                grabbedObject.velocity = grabbedObject.velocity.normalized * MaxForce;
        }

        internal void Throw(Rigidbody grabbedObject, Vector3 direction) => grabbedObject.AddForce(direction * _throwForce, ForceMode.Impulse);
    }
}