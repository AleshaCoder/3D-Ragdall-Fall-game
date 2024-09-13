using System;
using UnityEngine;

namespace Assets.Source.HUDSystem.Scripts.MenuButtons
{
    public class ButtonPresenter<T> : MonoBehaviour, ISelectableButton, IButtonPresenter<T> where T : Enum
    {
        [SerializeField] private IconsAnimator _iconsAnimator;

        [field: SerializeField] public T Type { get; private set; }

        public void Select() => _iconsAnimator.Select();
        public void Deselect() => _iconsAnimator.Deselect();
    }
}
