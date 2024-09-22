namespace DamageSystem
{
    public interface IDamageCalculator
    {
        int CalculateDamage(float force, BodyPartType bodyPartType);
    }

}
