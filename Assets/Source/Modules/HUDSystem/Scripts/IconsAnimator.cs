using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.HUDSystem.Scripts
{
    [Serializable]
    internal class IconsAnimator : IIconsAnimator
    {
        [SerializeField] private Image _enableSprite;

        public void Select() => _enableSprite.gameObject.SetActive(true);
        public void Deselect() => _enableSprite.gameObject.SetActive(false);
    }
}
