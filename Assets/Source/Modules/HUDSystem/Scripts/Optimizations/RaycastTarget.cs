using UnityEngine.UI;

namespace Assets.Source.HUDSystem.Scripts.Optimizations
{
    public class RaycastTarget : Graphic
    {
        public override void SetMaterialDirty() { return; }
        public override void SetVerticesDirty() { return; }
    }
}
