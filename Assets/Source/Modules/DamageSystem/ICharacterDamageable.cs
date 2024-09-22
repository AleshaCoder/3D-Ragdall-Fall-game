using System;

namespace DamageSystem
{
    public interface ICharacterDamageable
    {
        event Action<string> OnDamageMessage;

        void TakeDamage(float force, BodyPartType bodyPart);
    }
}
