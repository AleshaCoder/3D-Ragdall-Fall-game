using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Assets.Source.Extensions.Scripts;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    public class ObjectsScroll : MonoBehaviour
    {
        private const float LeftEdge = 0.001f;
        private const float RightEdge = 0.999f;

        [Header("settings")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        [SerializeField] private CanvasGroup _windowCanvas;
        [SerializeField] private float _width;
        [SerializeField] private float _spacing;
        [SerializeField] private float _cellWidth;

        private IScrollNavigation _scrollNavigation;
        private bool _tooFewElements = false;

        public ScrollRect ScrollRect => _scrollRect;
        public float WidthStep => _cellWidth + _spacing;

        public void Construct(IScrollNavigation scrollNavigation)
        {
            _scrollNavigation = scrollNavigation ?? throw new ArgumentNullException(nameof(scrollNavigation));
            _scrollRect.onValueChanged.AddListener(ScrollValueChanged);
        }

        public void Dispose()
        {
            _scrollRect.onValueChanged.RemoveListener(ScrollValueChanged);
        }

        public void Hide() => _windowCanvas.DisableGroup();

        public void Show()
        {
            if (_width < 0)
            {
                _scrollNavigation.ChangeForwardButtonEnable(enabled = false);
                _scrollNavigation.ChangeBackButtonEnable(enabled = false);
                _tooFewElements = true;
            }
            else
            {
                _scrollNavigation.ChangeForwardButtonEnable(enabled = true);
                _tooFewElements = false;
            }

            if (_scrollRect.content.localPosition.x < LeftEdge)
                _scrollNavigation.ChangeBackButtonEnable(enabled = false);

            _scrollRect.content.localPosition = Vector2.zero;
            _scrollNavigation.SetScroll(this);
            _windowCanvas.EnableGroup();
        }

        private void ScrollValueChanged(Vector2 position)
        {
            if (_tooFewElements)
                return;

            if (position.x < LeftEdge)
                _scrollNavigation.ChangeBackButtonEnable(enabled = false);
            else if(position.x > LeftEdge && _scrollNavigation.BackInteractable == false)
                _scrollNavigation.ChangeBackButtonEnable(enabled = true);

            if (position.x > RightEdge)
                _scrollNavigation.ChangeForwardButtonEnable(enabled = false);
            else if (position.x < RightEdge && _scrollNavigation.ForwardInteractable == false)
                _scrollNavigation.ChangeForwardButtonEnable(enabled = true);
        }

#if UNITY_EDITOR
        public void SetWidth(float cellWidth)
        {
            _width = _scrollRect.content.sizeDelta.x;
            _cellWidth = cellWidth;
        }

        public void SetGridSpacing() => _spacing = _horizontalLayoutGroup.spacing;

        public void SetMainSettings()
        {
            _scrollRect = gameObject.GetComponent<ScrollRect>();
            _horizontalLayoutGroup = _scrollRect.content.GetComponent<HorizontalLayoutGroup>();
            _windowCanvas = _scrollRect.GetComponent<CanvasGroup>();
        }
#endif
    }
}
