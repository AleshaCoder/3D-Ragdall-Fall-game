using UnityEngine;
using UnityEngine.UI;

namespace Analytics
{
    public class MainMenuAnalizer : MonoBehaviour
    {
        [SerializeField] private Button _settings;
        [SerializeField] private Button _skins;
        [SerializeField] private Button _maps;
        [SerializeField] private Button _mapsCancel;
        [SerializeField] private Button _skinCancel;
        
        private void Start()
        {
            AnalyticsSender.OpenMainMenu();
            _settings.onClick.AddListener(AnalyticsSender.OpenSettings);
            _skins.onClick.AddListener(AnalyticsSender.OpenSkins);
            _skinCancel.onClick.AddListener(AnalyticsSender.CancelSelectSkin);
            _maps.onClick.AddListener(AnalyticsSender.OpenMapsMenu);
            _mapsCancel.onClick.AddListener(AnalyticsSender.CancelSelectMap);
        }
    }
}
