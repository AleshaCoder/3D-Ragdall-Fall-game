using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.HUDSystem.Scripts.HideInterfaceSystem
{
    public class HideInterfaceButton : Button
    {
        [SerializeField] private Image _enableSprite;
        [SerializeField] private Image _disableSprite;

        public bool Activated { get; private set; } = false;

        public Action OnClick;

        protected override void Awake() => onClick.AddListener(OnClicked);

        protected override void OnDestroy() => onClick.RemoveListener(OnClicked);

        private void OnClicked()
        {
            Activated = !Activated;

            if(Activated)
            {
                _disableSprite.gameObject.SetActive(false);
                _enableSprite.gameObject.SetActive(true);
            }
            else
            {
                _enableSprite.gameObject.SetActive(false);
                _disableSprite.gameObject.SetActive(true);
            }

            OnClick?.Invoke();
        }
    }
}
