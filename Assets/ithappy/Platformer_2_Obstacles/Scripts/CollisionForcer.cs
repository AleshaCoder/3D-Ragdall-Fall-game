using UnityEngine;
using RootMotion.Dynamics;
using System.Linq;
using TimeSystem;

namespace ithappy
{
    public class CollisionForcer : MonoBehaviour
    {
        public float force = 10f;
        public Transform director;
        public Vector3 direction = Vector3.forward;


        private Vector3 _direction;
        private Vector3 _point;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(new Ray(_point, _direction));
            Gizmos.color = Color.red;
            if (director != null)
            Gizmos.DrawRay(new Ray(_point, (director.position - transform.position).normalized));
        }

        private void OnCollisionEnter(Collision collision)
        {
            Rigidbody rb = collision.rigidbody;

            if (rb != null)
            {
                PuppetMaster puppetMaster = rb.GetComponentInParent<PuppetMaster>();

                if (puppetMaster != null && puppetMaster != transform.GetComponentInParent<PuppetMaster>())
                {
                    var muscles = puppetMaster.muscles.Where(m => m.props.group == Muscle.Group.Spine || m.props.group == Muscle.Group.Hips);

                    _direction = collision.transform.position - collision.contacts[0].point;
                    _point = collision.contacts[0].point;

                    if (director != null)
                        _direction = (director.position - transform.position).normalized;

                    foreach (var m in muscles)
                    {
                        m.rigidbody.AddForce(_direction * force * TimeService.Scale, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
