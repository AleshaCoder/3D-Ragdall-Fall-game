namespace Assets.Source.HUDSystem.Scripts.ItemsMenuSystem
{
    public interface IScrollNavigation
    {
        public bool ForwardInteractable { get; }
        public bool BackInteractable { get; }

        public void SetScroll(ObjectsScroll objectsScroll);
        public void ChangeForwardButtonEnable(bool enable);
        public void ChangeBackButtonEnable(bool enable);
    }
}
