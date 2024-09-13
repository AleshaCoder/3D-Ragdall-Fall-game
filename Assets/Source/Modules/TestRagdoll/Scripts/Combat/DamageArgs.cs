using UnityEngine;

public readonly struct DamageArgs 
{
    public readonly float Damage;
    public readonly float Unpin;
    public readonly Vector3 Force;
    public readonly Vector3 Point;

    public DamageArgs(float damage, float unpin, Vector3 force, Vector3 point)
    {
        Damage = damage;
        Unpin = unpin;
        Force = force;
        Point = point;
    }
}
