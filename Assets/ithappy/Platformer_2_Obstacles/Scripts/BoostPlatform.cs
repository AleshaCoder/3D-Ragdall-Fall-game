using RootMotion.Dynamics;
using System.Linq;
using TimeSystem;
using UnityEngine;

namespace ithappy
{
    public class BoostPlatform : MonoBehaviour
    {
        public float boostForce = 20f;
        public Transform director;

        private void OnCollisionEnter(Collision collision)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                PuppetMaster puppetMaster = rb.GetComponentInParent<PuppetMaster>();

                if (puppetMaster != null && puppetMaster != transform.GetComponentInParent<PuppetMaster>())
                {
                    var muscles = puppetMaster.muscles.Where(m => m.props.group == Muscle.Group.Spine || m.props.group == Muscle.Group.Hips);
                    foreach (var m in muscles)
                    {
                        var boostDirection = (director.position - transform.position).normalized;
                        m.rigidbody.AddForce(boostDirection.normalized * boostForce, ForceMode.Impulse);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            var boostDirection = (director.position - transform.position).normalized;
            Gizmos.DrawRay(transform.position, boostDirection);
        }
    }
}
