using System;
using UnityEngine;
using Assets.Source.Extensions.Scripts;

namespace Assets.Source.HUDSystem.Scripts.HideInterfaceSystem
{
    [Serializable]
    public class HideInterface
    {
        [SerializeField] private HideInterfaceButton _hideInterfaceButton;
        [SerializeField] private CanvasGroup _mainSandboxInterface;

        public void Construct()
        {
            _hideInterfaceButton.OnClick += TryHideInterfaceView;
        }

        public void Dispose()
        {
            _hideInterfaceButton.OnClick -= TryHideInterfaceView;
        }

        private void TryHideInterfaceView()
        {
            if (_hideInterfaceButton.Activated)
                _mainSandboxInterface.DisableGroup();
            else
                _mainSandboxInterface.EnableGroup();
        }
    }
}
