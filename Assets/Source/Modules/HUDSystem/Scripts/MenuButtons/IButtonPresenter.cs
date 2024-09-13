using System;

namespace Assets.Source.HUDSystem.Scripts.MenuButtons
{
    public interface IButtonPresenter<T> : IButton where T : Enum
    {
        T Type { get; }
    }
}
