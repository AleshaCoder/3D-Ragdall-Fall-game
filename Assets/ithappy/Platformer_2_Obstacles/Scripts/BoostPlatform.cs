using UnityEngine;

namespace ithappy
{
    public class BoostPlatform : MonoBehaviour
    {
        public float boostForce = 20f;

        public Vector3 boostDirection = Vector3.forward;

        private void OnCollisionEnter(Collision collision)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 normalizedBoostDirection = boostDirection.normalized;
                rb.AddForce(normalizedBoostDirection * boostForce, ForceMode.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, boostDirection);
        }
    }
}
