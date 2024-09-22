using RootMotion.Dynamics;
using System.Linq;
using TimeSystem;
using UnityEngine;

public class PropellerTrampoline : MonoBehaviour
{
    public float jumpForce = 100f;

    public float spinForce = 20f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            PuppetMaster puppetMaster = rb.GetComponentInParent<PuppetMaster>();

            if (puppetMaster != null && puppetMaster != transform.GetComponentInParent<PuppetMaster>())
            {
                var muscles = puppetMaster.muscles.Where(m => m.props.group == Muscle.Group.Spine || m.props.group == Muscle.Group.Hips);
                foreach (var m in muscles)
                {
                    m.rigidbody.AddForce(Vector3.up * jumpForce * TimeService.Scale, ForceMode.Acceleration);
                    Vector3 randomSpinAxis = new Vector3(
                       Random.Range(-1f, 1f),
                       Random.Range(-1f, 1f),
                       Random.Range(-1f, 1f)
                   ).normalized;

                    m.rigidbody.AddTorque(randomSpinAxis * spinForce, ForceMode.Impulse);
                }
            }
        }
    }

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
                    m.rigidbody.AddForce(Vector3.up * jumpForce * TimeService.Scale, ForceMode.Force);

                    Vector3 randomSpinAxis = new Vector3(
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f)
                    ).normalized;

                    m.rigidbody.AddTorque(randomSpinAxis * spinForce, ForceMode.Impulse);
                }
            }
        }
    }
}
