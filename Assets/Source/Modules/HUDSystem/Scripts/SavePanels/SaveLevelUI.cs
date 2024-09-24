using SaveLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLevelUI : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    public TMP_InputField saveNameInputField;
    public GameObject savePanel;
    public GameObject warningPanel;
    public TMP_Text warningText;
    public Button confirmOverwriteButton;
    public Button saveButton;

    private string currentSaveName;

    void Start()
    {
        saveButton.onClick.AddListener(SaveLevel);
        confirmOverwriteButton.onClick.AddListener(OverwriteSave);
    }

    public void OpenSavePanel()
    {
        savePanel.SetActive(true);
        warningPanel.SetActive(false);
        saveNameInputField.text = "";
    }

    public void CloseSavePanel()
    {
        savePanel.SetActive(false);
    }

    private void ShowWarning(string message)
    {
        warningPanel.SetActive(true);
        warningText.text = message;
    }

    private void HideWarning()
    {
        warningPanel.SetActive(false);
    }

    private void SaveLevel()
    {
        currentSaveName = saveNameInputField.text;

        if (string.IsNullOrEmpty(currentSaveName))
        {
            Debug.LogWarning("Имя сохранения не может быть пустым!");
            return;
        }

        if (SaveRecordsManager.GetAllSaves().Exists(record => record.SaveName == currentSaveName))
        {
            ShowWarning($"A save named '{currentSaveName}' already exists. Re-record?");
        }
        else
        {
            _levelManager.SaveLevel(currentSaveName);
            CloseSavePanel();
        }
    }

    public void OverwriteSave()
    {
        _levelManager.SaveLevel(currentSaveName);
        HideWarning();
        CloseSavePanel();
    }
}
