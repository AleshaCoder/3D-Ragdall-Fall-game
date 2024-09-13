using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Assets.Source.RayCasterSystem.Scripts
{
    public class RayCaster : IRayCaster
    {
        private readonly Camera _camera;
        private readonly int _uiLayer;

        public RayCaster(Camera camera)
        {
            _camera = camera;
            _uiLayer = LayerMask.NameToLayer("UI");
        }

        public bool TryGetHit(Vector3 screenPoint, out RaycastHit rayHit, LayerMask layer, float rayLength = Mathf.Infinity)
        {
            Ray ray = _camera.ScreenPointToRay(screenPoint);

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layer))
            {
                rayHit = hit;
                return true;
            }

            rayHit = default;
            return false;
        }

        public (Vector3 position, Vector3 normal) GetPositionAndNormal(Vector3 screenPoint, LayerMask layer, float rayLength = Mathf.Infinity)
        {
            Ray ray = _camera.ScreenPointToRay(screenPoint);

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layer))
            {
                return (position: hit.point, normal: hit.normal);
            }

            Vector3 endPoint = ray.origin + ray.direction * rayLength;
            return (position: endPoint, normal: Vector3.up);
        }

        public bool TryGetAny<T>(Vector3 screenPoint, out T entity) where T : MonoBehaviour
        {
            if (TryRayCast(screenPoint, out RaycastHit[] hits))
            {
                RaycastHit result = hits.FirstOrDefault(hit => hit.collider.TryGetComponent(out T _));

                if (result.collider && result.collider.TryGetComponent(out T tempEntity))
                {
                    entity = tempEntity;
                    return true;
                }
            }

            entity = default;
            return false;
        }

        public bool TryGetUI<T>(Vector3 screenPoint, out T entity)
        {
            var hasEnitity = TryGetUI(screenPoint, out T[] results);

            entity = default;

            if (hasEnitity)
                entity = results[0];

            return hasEnitity;
        }

        public bool TryGetUI<T>(Vector3 screenPoint, out T[] entity)
        {
            List<RaycastResult> result = GetRayCastAll(screenPoint);
            entity = result.Select(item => item.gameObject.GetComponent<T>()).ToArray();
            return entity.Length > 0;
        }

        public bool IsNotUIObject(Vector3 screenPoint)
        {
            List<RaycastResult> result = GetRayCastAll(screenPoint);
            return result.Count == 0 || result.All(x => x.gameObject.layer != _uiLayer);
        }

        private bool TryRayCast(Vector3 screenPoint, out RaycastHit[] hits)
        {
            Ray ray = _camera.ScreenPointToRay(screenPoint);
            RaycastHit[] raycasts = Physics.RaycastAll(ray);

            if (raycasts.Length > 0)
            {
                hits = raycasts;
                return true;
            }

            hits = null;
            return false;
        }

        private List<RaycastResult> GetRayCastAll(Vector3 screenPoint)
        {
            PointerEventData eventData = new(EventSystem.current) { position = screenPoint };
            var result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, result);
            return result;
        }
    }
}