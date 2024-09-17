using UnityEngine;

public class PropellerTrampoline : MonoBehaviour
{
    public float jumpForce = 100f;

    public float spinForce = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            Vector3 randomSpinAxis = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;

            rb.AddTorque(randomSpinAxis * spinForce, ForceMode.Impulse);
        }
    }
}
