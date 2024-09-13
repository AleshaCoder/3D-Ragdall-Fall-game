using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class ListsOfObjects
    {
        private const float CellWidth = 200f;

        [SerializeField] private Transform ScrollerContainer;
        [SerializeField] private List<ObjectsScroll> _objectsScrolls;

#if UNITY_EDITOR
        public void GetLists()
        {
            _objectsScrolls = new List<ObjectsScroll>();
            _objectsScrolls = ScrollerContainer.GetComponentsInChildren<ObjectsScroll>().ToList();
        }

        public void SetSettingsToLists()
        {
            _objectsScrolls.ForEach(scroll =>
            {
                scroll.SetMainSettings();
                scroll.SetWidth(CellWidth);
                scroll.SetGridSpacing();
            });
        }
#endif
    }
}
