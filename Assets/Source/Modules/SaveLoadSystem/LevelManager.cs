using Assets.Source.Entities.Scripts;
using SkinsSystem;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    public class LevelManager : MonoBehaviour
    {
        private ItemFactory _itemFactory;

        public void Construct(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public void SaveLevel(string saveName)
        {
            SaveData saveData = new SaveData
            {
                SaveName = saveName,
                SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
                Items = new List<BuildingData>(),
                Enemies = new List<EnemyData>(),
                Skins = new List<SkinData>()
            };

            foreach (Building item in FindObjectsOfType<Building>())
            {
                BuildingData itemData = new()
                {
                    ItemType = item.Type,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation,
                    Scale = item.transform.localScale
                };
                saveData.Items.Add(itemData);
            }

            foreach (Unit item in FindObjectsOfType<Unit>())
            {
                EnemyData itemData = new()
                {
                    RagdollType = item.Type,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation,
                    Scale = item.transform.localScale
                };
                saveData.Enemies.Add(itemData);
            }

            Skin skin = FindObjectOfType<Skin>();

            SkinData skinData = new SkinData
            {
                RagdollType = skin.RagdollType,
                Position = skin.transform.position,
                Rotation = skin.transform.rotation,
                Scale = skin.transform.localScale
            };
            saveData.Skins.Add(skinData);

            SaveLoadSystem.Save(saveData);

            string savePath = SaveLoadSystem.GetSavePath(saveData.SaveName);
            SaveRecordsManager.AddSaveRecord(saveData.SaveName, saveData.SceneName, savePath);
        }

        public void LoadLevel(string saveName)
        {
            SaveData saveData = SaveLoadSystem.Load(saveName);
            if (saveData == null) return;

            foreach (Building item in FindObjectsOfType<Building>())
            {
                Destroy(item.gameObject);
            }

            foreach (Unit item in FindObjectsOfType<Unit>())
            {
                Destroy(item.gameObject);
            }

            foreach (Skin skin in FindObjectsOfType<Skin>())
            {
                skin.gameObject.SetActive(false);
            }

            foreach (BuildingData itemData in saveData.Items)
            {
                _itemFactory.CreateItem(itemData);
            }

            foreach (EnemyData itemData in saveData.Enemies)
            {
                _itemFactory.CreateItem(itemData);
            }

            foreach (SkinData skinData in saveData.Skins)
            {
                _itemFactory.CreateItem(skinData);
            }
        }
    }
}
