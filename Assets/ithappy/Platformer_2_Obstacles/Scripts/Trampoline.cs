using Assets.Source.Entities.Scripts;
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
            PuppetMaster puppetMaster = rb?.GetComponentInParent<PuppetMaster>();

            if (puppetMaster != null && puppetMaster != transform.GetComponentInParent<PuppetMaster>())
            {
                Debug.Log("Attack");
                var muscles = puppetMaster.muscles.Where(m => m.props.group == Muscle.Group.Spine || m.props.group == Muscle.Group.Hips);
                foreach (var m in muscles)
                {
                    m.rigidbody.AddForce(puppetMaster.transform.rotation * direction * jumpForce, ForceMode.Impulse);
                }
            }
        }


    }
}
