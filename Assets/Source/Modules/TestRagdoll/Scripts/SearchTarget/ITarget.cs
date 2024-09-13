using UnityEngine;

public interface ITarget 
{
    public Transform Transform { get; }
    public bool IsAlive { get; }
}
