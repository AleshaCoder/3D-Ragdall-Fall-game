using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _sensitivity;

    internal event Action<float> Hold;

    internal void Init(float sensitivity) => _sensitivity = sensitivity;

    public void OnPressed() => Hold?.Invoke(_sensitivity);

    public void OnUnpressed() => Hold?.Invoke(0f);

    public void OnPointerDown(PointerEventData eventData) => Hold?.Invoke(_sensitivity);

    public void OnPointerUp(PointerEventData eventData) => Hold?.Invoke(0f);
}