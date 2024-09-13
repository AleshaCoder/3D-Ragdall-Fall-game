using System.Collections.Generic;
using UnityEngine;

public class OverlapSphereSearchTarget : MonoBehaviour, ISearchTarget
{
    public T GetNearestTarget<T>(IReadOnlyList<T> targets, Vector3 position, float maxDistance) where T : ITarget
    {
        if (targets.Count == 0)
            return default;

        T targetNearest = targets[0];
        float nearestDistance = Vector3.Distance(position, targetNearest.Transform.position);

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(position, target.Transform.position);
            if (distance < nearestDistance)
            {
                targetNearest = target;
                nearestDistance = distance;
            }
        }

        return targetNearest;
    }

    public IReadOnlyList<T> GetTargets<T>(Vector3 position, float maxDistance) where T : ITarget
    {
        Collider[] colliders = Physics.OverlapSphere(position, maxDistance);
        List<T> targets = new List<T>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out T component))
            {
                targets.Add(component);
            }
        }

        return targets;
    }
}
