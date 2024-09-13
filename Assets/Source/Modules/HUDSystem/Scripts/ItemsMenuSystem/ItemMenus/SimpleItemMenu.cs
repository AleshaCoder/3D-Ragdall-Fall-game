using System;
using UnityEngine;

namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    [Serializable]
    public class SimpleItemMenu : ItemMenu
    {
        [SerializeField] protected ObjectsScroll ObjectsScroll;

        public void Construct(IScrollNavigation scrollNavigation) => ObjectsScroll.Construct(scrollNavigation);

        public void Dispose() => ObjectsScroll.Dispose();

        public override void Close()
        {
            base.Close();
            ObjectsScroll.Hide();
        }

        public override void Open()
        {
            base.Open();
            ObjectsScroll.Show();
        }
    }
}
