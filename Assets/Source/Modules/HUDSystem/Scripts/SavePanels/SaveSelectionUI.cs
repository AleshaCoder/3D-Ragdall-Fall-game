using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSelectionUI : MonoBehaviour
{
    public Button saveButtonPrefab;
    public Transform saveListContainer;
    public GameObject saveSelectionPanel;
    public TMP_Text sceneNameText;

    private string selectedSceneName;

    public void OpenSaveSelection(string sceneName)
    {
        selectedSceneName = sceneName;
        sceneNameText.text = $"—цена: {sceneName}";

        foreach (Transform child in saveListContainer)
        {
            Destroy(child.gameObject);
        }

        List<SaveRecord> saves = SaveRecordsManager.GetSavesByScene(sceneName);

        foreach (var save in saves)
        {
            Button button = Instantiate(saveButtonPrefab, saveListContainer);
            button.GetComponentInChildren<TMP_Text>().text = save.SaveName;
            button.onClick.AddListener(() => LoadSave(save));
        }

        saveSelectionPanel.SetActive(true);
    }

    private void LoadSave(SaveRecord save)
    {
        SceneLoadData.SelectedSave = save;

        UnityEngine.SceneManagement.SceneManager.LoadScene(save.SceneName);
    }

    public void CloseSaveSelection()
    {
        saveSelectionPanel.SetActive(false);
    }
}
