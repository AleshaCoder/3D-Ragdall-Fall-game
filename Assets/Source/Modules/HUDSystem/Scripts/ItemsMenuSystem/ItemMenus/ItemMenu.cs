using System;
using UnityEngine;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public abstract class ItemMenu
    {
        [field: SerializeField] public ItemMenuButton ItemMenuButton { get; private set; }

        public virtual void Open() => ItemMenuButton.EnableSprite();

        public virtual void Close() => ItemMenuButton.DisableSprite();
    }
}
