using UnityEngine;
using UnityEngine.UI;

public class ClosablePanel : MonoBehaviour
{
    [SerializeField] private Button _changeActivityButton;
    [SerializeField] private GameObject _panel; 

    private void Start()
    {
        _changeActivityButton.onClick.AddListener(TogglePanel);
    }

    private void TogglePanel()
    {
        _panel.SetActive(!_panel.activeInHierarchy);
    }
}