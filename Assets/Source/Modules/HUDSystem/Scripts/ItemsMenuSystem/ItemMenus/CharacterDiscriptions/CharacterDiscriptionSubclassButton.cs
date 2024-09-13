using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class CharacterDiscriptionSubclassButton : Button
    {
        [SerializeField] private IconsAnimator _iconsAnimator;

        [field: SerializeField] public CharacterDiscriptionSubclassesTypes CharacterDiscriptionSubclassType { get; private set; }

        public bool Activated { get; private set; } = false;

        public Action<CharacterDiscriptionSubclassesTypes> OnClick;

        protected override void Awake() => onClick.AddListener(OnClicked);

        protected override void OnDestroy() => onClick.RemoveListener(OnClicked);

        public void EnableSprite() => _iconsAnimator.Select();

        public void DisableSprite() => _iconsAnimator.Deselect();

        private void OnClicked()
        {
            Activated = !Activated;
            OnClick?.Invoke(CharacterDiscriptionSubclassType);
        }
    }
}
