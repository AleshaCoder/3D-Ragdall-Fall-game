namespace Assets.Source.HUDSystem.Scripts.MenuButtons
{
    public interface IBlockedButton
    {
        public bool IsBlocked { get; }
        public void OnBlockedClicked(/*IAdsHandler rewardHandler*/);
    }
}
