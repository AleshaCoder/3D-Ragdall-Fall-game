using UnityEngine;
using RootMotion.Dynamics;
using System.Linq;

namespace ithappy
{
    public class Trampoline : MonoBehaviour
    {
        public float jumpForce = 10f;
        public Vector3 direction = Vector3.forward;

        private void OnCollisionEnter(Collision collision)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                PuppetMaster puppetMaster = rb.GetComponentInParent<PuppetMaster>();

                if (puppetMaster != null && puppetMaster != transform.GetComponentInParent<PuppetMaster>())
                {
                    var muscles = puppetMaster.muscles.FirstOrDefault(m => m.props.group == Muscle.Group.Spine);
                    foreach (var m in puppetMaster.muscles)
                    {
                        if (m != muscles)
                            m.rigidbody.velocity = Vector3.zero;
                    }

                    if (muscles != default)
                    {
                       // muscles.rigidbody.AddForce(transform.up * jumpForce * TimeService.Scale, ForceMode.Impulse);
                        muscles.rigidbody.velocity = transform.up * jumpForce;
                    }
                }
            }
        }
    }
}
