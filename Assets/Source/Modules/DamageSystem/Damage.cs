using RootMotion.Dynamics;
using System;
using UnityEngine;

namespace DamageSystem
{
    public class Damage : MonoBehaviour, ICharacterDamageable, IDamageMessage
    {
        [SerializeField] private PuppetMaster _puppetMaster;
        private IDamageCalculator _damageCalculator;
        private ScoreRepository _score;

        public event Action<string> OnDamageMessage;

        public void Construct(ScoreRepository scoreRepository)
        {
            _damageCalculator = new VelocityBasedDamageCalculator();
            _score = scoreRepository;
        }

        public void TakeDamage(float force, BodyPartType bodyPart)
        {
            int damage = _damageCalculator.CalculateDamage(force, bodyPart);

            if (damage < 100)
                return;

            if (_puppetMaster.state == PuppetMaster.State.Dead)
            {
                _score.AddScore(damage);

                string damageText = $"{bodyPart} +{damage}";
                OnDamageMessage?.Invoke(damageText);
            }
            else
            {
                _score.ResetScore();
            }
        }
    }


}
