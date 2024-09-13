using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private ContentSizeFitter _fitter;

    private void Start()
    {
        Debug.Log($"_scrollRect {_scrollRect.content.sizeDelta.x}");
    }

    private void OnEnable()
    {
        _scrollRect.onValueChanged.AddListener(ScrollOnValueChanged);
        Debug.Log($"_scrollRect {_scrollRect.content.sizeDelta.x}");
    }

    private void OnDisable()
    {
        _scrollRect.onValueChanged.RemoveListener(ScrollOnValueChanged);
    }

    private void ScrollOnValueChanged(Vector2 vector)
    {
        Debug.Log($"vector.x {vector.x}");
        Debug.Log($"_scrollRect {_scrollRect.content.sizeDelta.x}");
    }
}
