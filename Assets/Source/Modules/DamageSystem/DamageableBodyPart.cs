using UnityEngine;

namespace DamageSystem
{
    public class DamageableBodyPart : MonoBehaviour
    {
        public BodyPartType bodyPartType;
        private ICharacterDamageable characterDamageable;

        private void Start()
        {
            characterDamageable = transform.root.GetComponentInChildren<ICharacterDamageable>();
            if (characterDamageable == null)
            {
                Debug.LogError("CharacterDamageable component is missing on parent.");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            float mass = (collision.rigidbody == null) ? 1 : collision.rigidbody.mass;

            float force = Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity) * mass;
            characterDamageable?.TakeDamage(force, bodyPartType);
        }
    }

}
