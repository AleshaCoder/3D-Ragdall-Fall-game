using System;

namespace Assets.Source.HUDSystem.Scripts.MenuButtons
{
    public interface IBlockedButtonDataProvider
    {
        //public ISaveLoadService SaveLoadService { get; }
        //public SandboxData SandboxData { get; }
        //public DailyRewardData DailyRewardData { get; }
    }

    public class BlockedButtonDataProvider : IBlockedButtonDataProvider
    {
        /*public ISaveLoadService SaveLoadService { get; private set; }
        public SandboxData SandboxData { get; private set; }
        public DailyRewardData DailyRewardData { get; }

        public BlockedButtonDataProvider(ISaveLoadService saveLoadService, GameData gameData)
        {
            SaveLoadService = saveLoadService ?? throw new ArgumentNullException(nameof(saveLoadService));
            SandboxData = gameData.SandboxData ?? throw new ArgumentNullException(nameof(gameData.SandboxData));
            DailyRewardData = gameData.DailyRewardData ?? throw new ArgumentNullException(nameof(gameData.DailyRewardData));
        }*/
    }
}
