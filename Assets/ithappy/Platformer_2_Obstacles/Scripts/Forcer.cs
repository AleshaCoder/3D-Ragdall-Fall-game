using UnityEngine;
using RootMotion.Dynamics;
using System.Linq;
using TimeSystem;

namespace ithappy
{

    public class Forcer : MonoBehaviour
    {
        public float force = 10f;
        public Transform director;
        public Vector3 direction = Vector3.forward;

        private void OnTriggerEnter(Collider collision)
        {
            Rigidbody rb = collision.GetComponent<Rigidbody>();

            if (rb != null)
            {
                PuppetMaster puppetMaster = rb.GetComponentInParent<PuppetMaster>();

                if (puppetMaster != null && puppetMaster != transform.GetComponentInParent<PuppetMaster>())
                {
                    var muscles = puppetMaster.muscles.Where(m => m.props.group == Muscle.Group.Spine || m.props.group == Muscle.Group.Hips);

                    Vector3 dir = direction;
                    
                    if (director != null)
                        dir = (director.position - transform.position).normalized;

                    foreach (var m in muscles)
                    {
                        m.rigidbody.AddForce(dir * force, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
