using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    public class CharacterDiscriptionTypeButton : Button
    {
        private const float ShowAngle = 180;

        [SerializeField] private IconsAnimator _iconsAnimator;
        [SerializeField] private Image _arrow;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _animationTime = 0.3f;

        private Vector3 _showAngle = new Vector3(0, 0, ShowAngle);
        private Tween _windowTween;

        [field: SerializeField] public CharacterDiscriptionsTypes CharacterDiscriptionsType { get; private set; }

        public Vector3 InitVerticalPosition { get; private set; }

        public bool Activated { get; private set; } = false;

        public Action<CharacterDiscriptionsTypes> OnClick;

        protected override void Awake() => onClick.AddListener(OnClicked);

        protected override void OnDestroy() => onClick.RemoveListener(OnClicked);

        public void Construct() => InitVerticalPosition = _transform.localPosition;

        public void EnableSprite()
        {
            RotateArrow(_showAngle);
            _iconsAnimator.Select();
        }

        public void DisableSprite()
        {
            RotateArrow(Vector3.zero);
            _iconsAnimator.Deselect();
        }

        private void OnClicked()
        {
            Activated = !Activated;
            OnClick?.Invoke(CharacterDiscriptionsType);
        }

        private void RotateArrow(Vector3 angle)
        {
            _windowTween?.Kill();
            _windowTween = _arrow.transform.DOLocalRotate(angle, _animationTime).SetUpdate(true);
        }
    }
}
