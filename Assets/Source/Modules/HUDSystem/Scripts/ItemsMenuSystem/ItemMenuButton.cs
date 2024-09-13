using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    public class ItemMenuButton : Button
    {
        [SerializeField] private Image _enableSprite;

        public bool Activated { get; private set; } = false;

        public Action OnClick;

        protected override void Awake() => onClick.AddListener(OnClicked);

        protected override void OnDestroy() => onClick.RemoveListener(OnClicked);

        public void EnableSprite() => _enableSprite.gameObject.SetActive(true);
        public void DisableSprite() => _enableSprite.gameObject.SetActive(false);

        private void OnClicked()
        {
            Activated = !Activated;
            OnClick?.Invoke();
        }
    }
}
