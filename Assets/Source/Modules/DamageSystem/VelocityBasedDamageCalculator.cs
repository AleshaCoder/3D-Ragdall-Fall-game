namespace DamageSystem
{
    public class VelocityBasedDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(float velocity, BodyPartType bodyPartType)
        {
            int baseDamage = velocity > 5f ? (int)(velocity * 10) : 0;

            return bodyPartType switch
            {
                BodyPartType.Head => baseDamage * 10,
                BodyPartType.Arm => baseDamage * 2,
                BodyPartType.Leg => baseDamage,
                BodyPartType.Body => baseDamage * 5,
                _ => baseDamage,
            };
        }
    }

}
