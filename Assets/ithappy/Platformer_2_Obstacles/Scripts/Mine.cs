using UnityEngine;
using RootMotion.Dynamics;
using System.Linq;
using TimeSystem;
using DamageSystem;

namespace ithappy
{
    public class Mine : MonoBehaviour
    {
        public float force = 10f;
        public Transform director;
        public Vector3 direction = Vector3.forward;
        public AudioSource audioSource;
        public ParticleSystem explosion;
        public MeshRenderer mineRendrer;

        private void OnTriggerEnter(Collider collision)
        {
            Rigidbody rb = collision.GetComponent<Rigidbody>();

            if (rb != null)
            {
                PuppetMaster puppetMaster = rb.GetComponentInParent<PuppetMaster>();
                puppetMaster.Kill();
                var damage = puppetMaster.transform.root.GetComponentInChildren<Damage>();


                if (puppetMaster != null && puppetMaster != transform.GetComponentInParent<PuppetMaster>())
                {
                    var muscles = puppetMaster.muscles.Where(m => m.props.group == Muscle.Group.Spine || m.props.group == Muscle.Group.Foot);

                    Vector3 dir = direction;

                    if (director != null)
                        dir = (director.position - transform.position).normalized;

                    foreach (var m in muscles)
                    {
                        m.rigidbody.AddForce(dir * force * TimeService.Scale, ForceMode.Impulse);
                        BodyPartType bodyPartType = m.props.group == Muscle.Group.Spine ? BodyPartType.Body : BodyPartType.Leg;
                        damage.TakeDamage(force, bodyPartType);
                    }
                }
                audioSource.Play();
                explosion.Play();
                //gameObject.SetActive(false);
                mineRendrer.enabled = false;
                Destroy(gameObject, 1f);
            }
        }
    }
}
