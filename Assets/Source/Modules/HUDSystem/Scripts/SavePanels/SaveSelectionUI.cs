using Analytics;
using SaveLoadSystem;
using SceneLoading;
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
    public SceneLoader sceneLoader;

    private void OnValidate()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void OpenSaveSelection(string sceneName)
    {
        AnalyticsSender.SelectMap(sceneName);
        sceneNameText.text = $"Map: {sceneName}";

        foreach (Transform child in saveListContainer)
        {
            Destroy(child.gameObject);
        }

        List<SaveRecord> saves = SaveRecordsManager.GetSavesByScene(sceneName);

        Button startButton = InstatiateButton("New Game");
        startButton.onClick.AddListener(() => sceneLoader.LoadScene(sceneName));
        startButton.onClick.AddListener(() => AnalyticsSender.NewGame(sceneName));

        foreach (var save in saves)
        {
            Button button = InstatiateButton(save.SaveName);
            button.onClick.AddListener(() => LoadSave(save));
        }

        saveSelectionPanel.SetActive(true);
    }

    private Button InstatiateButton(string saveName)
    {
        Button button = Instantiate(saveButtonPrefab, saveListContainer);
        button.GetComponentInChildren<TMP_Text>().text = saveName;
        return button;
    }

    private void LoadSave(SaveRecord save)
    {
        SceneLoadData.SelectedSave = save;
        AnalyticsSender.LoadSavedGame(save.SceneName);
        sceneLoader.LoadScene(save.SceneName);
    }

    public void CloseSaveSelection()
    {
        saveSelectionPanel.SetActive(false);
    }
}
