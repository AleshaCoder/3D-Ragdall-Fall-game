using Assets.Source.Entities.Scripts;
using Assets.Source.ItemSpawnerSystem.Scripts;
using SkinsSystem;
using UnityEngine;

namespace SaveLoadSystem
{
    public class ItemFactory
    {
        private CreatedScriptableObjects _data;
        private SkinSelector _skinSelector;

        public ItemFactory(CreatedScriptableObjects data, SkinSelector skinSelector)
        {
            _data = data;
            _skinSelector = skinSelector;
        }

        public void CreateItem(BuildingData data)
        {
            _data.TryGetItem(data.ItemType, out Item template);

            if (template == null)
            {
                Debug.LogError($"Prefab {data.ItemType} not found!");
            }

            Item item = Object.Instantiate(template, data.Position, data.Rotation);
            item.transform.localScale = data.Scale;
        }

        public void CreateItem(EnemyData data)
        {
            _data.TryGetCharacter(data.RagdollType, out Item template);

            if (template == null)
            {
                Debug.LogError($"Prefab {data.RagdollType} not found!");
            }

            Item item = Object.Instantiate(template, data.Position, data.Rotation);
            item.transform.localScale = data.Scale;
        }

        public void CreateItem(SkinData data)
        {
            //_skinSelector.ActiveSkin.gameObject.SetActive(false);
            _skinSelector.ActiveSkin.transform.SetPositionAndRotation(data.Position, data.Rotation);
            _skinSelector.ActiveSkin.transform.localScale = data.Scale;
            _skinSelector.ChangeSkin(data.RagdollType);
        }
    }
}
