using System.Collections.Generic;
using UnityEngine;

public interface ISearchTarget 
{
    public T GetNearestTarget<T>(IReadOnlyList<T> targets, Vector3 position, float maxDistance) where T : ITarget;
    public IReadOnlyList<T> GetTargets<T>(Vector3 position, float maxDistance) where T : ITarget;
}
