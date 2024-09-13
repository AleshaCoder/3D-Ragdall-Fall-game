using UnityEngine;

namespace Assets.Source.RayCasterSystem.Scripts
{
    public interface IRayCaster
    {
        (Vector3 position, Vector3 normal) GetPositionAndNormal(Vector3 screenPoint, LayerMask layer, float rayLength = float.PositiveInfinity);
        public bool IsNotUIObject(Vector3 screenPoint);
        public bool TryGetAny<T>(Vector3 screenPoint, out T entity) where T : MonoBehaviour;
        public bool TryGetHit(Vector3 screenPoint, out RaycastHit rayHit, LayerMask spawnLayer, float rayLength = Mathf.Infinity);
        public bool TryGetUI<T>(Vector3 screenPoint, out T[] entity);
    }
}