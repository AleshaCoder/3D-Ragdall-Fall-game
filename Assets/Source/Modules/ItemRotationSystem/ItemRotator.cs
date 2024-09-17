using UnityEngine;

namespace RotationSystem
{
    public class ItemRotator
    {
        private IRotatable _rotatable;

        public void SetRotatable(IRotatable rotatable)
        {
            _rotatable = rotatable;
        }

        public void FreeRotatable()
        {
            _rotatable = null;
        }

        public void Rotate(Vector3 eulerAngle)
        {
            if (_rotatable == null)
                return;

            if (eulerAngle == Vector3.zero)
                _rotatable.UpdateOriginRotation();

            if (_rotatable.CanRotateX && eulerAngle.x != 0)
                _rotatable.RotateX(eulerAngle.x);

            if (_rotatable.CanRotateY && eulerAngle.y != 0)
                _rotatable.RotateY(eulerAngle.y);

            if (_rotatable.CanRotateZ && eulerAngle.z != 0)
                _rotatable.RotateZ(eulerAngle.z);
        }
    }
}
