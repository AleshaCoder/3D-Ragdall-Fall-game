using UnityEngine;
using UnityEngine.UIElements;
using Assets.Source.Entities.Scripts;

public class RotateCanvas : MonoBehaviour
{
    private const float GroundOffset = 0.05f;

    [SerializeField] private LayerMask _spawnLayerMask;
    [SerializeField] private RotateButton _leftButton;
    [SerializeField] private RotateButton _rightButton;
    [SerializeField] private Image _backImage;
    [SerializeField] private float _sensitivity;

    private Item _item;

    public bool IsRotating { get; private set; } 

    private void OnEnable() => Init();

    private void OnDisable() => Dispose();

    internal void Init()
    {
        _leftButton.Init(_sensitivity);
        _rightButton.Init(-_sensitivity);
        _leftButton.Hold += Rotate;
        _rightButton.Hold += Rotate;
    }

    internal void Dispose()
    {
        Set(null);
        _leftButton.Hold -= Rotate;
        _rightButton.Hold -= Rotate;
    }

    public void Tick()
    {
        if (_item == null)
            return;

        SetPosition();
    }

    public void Set(Item item)
    {
        _item = item;

        if (_item != null && item.Center == null)
        {
            _item = null;
            return;
        }
    }

    public void StopRotation()
    {
        _leftButton.OnUnpressed();
        _rightButton.OnUnpressed();
    }

    private void Rotate(float delta)
    {
        IsRotating = delta != 0f;

        if (delta == 0f && _item != null)
        {
            _item.Rotate(0f);
            return;
        }

        if (_item != null)
            _item.Rotate(delta);
    }

    private void SetPosition()
    {
        Ray ray = new(_item.Center.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _spawnLayerMask))
        {
            transform.position = new(hit.point.x, hit.point.y + GroundOffset, hit.point.z);
            transform.up = hit.normal;
        }
        else
        {
            Set(null);
            gameObject.SetActive(false);
        }
    }
}