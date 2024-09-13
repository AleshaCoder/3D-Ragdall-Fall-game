using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.HUDSystem.Scripts.MenuButtons
{
    public abstract class BlockedButtonPresenter<T> : ButtonPresenter<T>, IBlockedButton where T : Enum
    {
        [SerializeField] protected Image BlockedImage;

        //protected SandboxData SandboxData;
        //protected ISaveLoadService SaveLoadService;
        protected bool Blocked;

        public bool IsBlocked => Blocked;

        public virtual void Construct(IBlockedButtonDataProvider dataProvider)
        {
            Blocked = true;
            //SaveLoadService = dataProvider.SaveLoadService ?? throw new ArgumentNullException(nameof(dataProvider.SaveLoadService));
            //SandboxData = dataProvider.SandboxData ?? throw new ArgumentNullException(nameof(dataProvider.SandboxData));

            CheckUnlockStatus();
        }

        public virtual void OnBlockedClicked(/*IAdsHandler rewardHandler*/) { }

        public virtual void CheckUnlockStatus()
        {
            /*if (SandboxData.UnlockItems.Any(p => p.ToString() == Type.ToString()))
            {
                BlockedImage.enabled = false;
                Blocked = false;
            }
            else
            {
                BlockedImage.enabled = true;
            }*/
        }

        protected virtual void OnUnblocked()
        {
            BlockedImage.enabled = false;
            Blocked = false;

            //SandboxData.UnlockItems.Add(Type.ToString());
            //SaveLoadService.Save();
        }
    }
}
