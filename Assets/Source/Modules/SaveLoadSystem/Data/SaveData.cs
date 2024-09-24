using Assets.Source.Structs.Scripts;
using SaveLoadSystem.SerializableTypes;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    [System.Serializable]
    public class SaveData
    {
        [SerializeField] public string SaveName;
        [SerializeField] public string SceneName;
        [SerializeField] public List<BuildingData> Items;
        [SerializeField] public List<EnemyData> Enemies;
        [SerializeField] public List<SkinData> Skins;  
    }

    [System.Serializable]
    public class BuildingData
    {
        [SerializeField] public ItemsType ItemType;      
        [SerializeField] public SVector3 Position;
        [SerializeField] public SQuaternion Rotation;
        [SerializeField] public SVector3 Scale;       
    }

    [System.Serializable]
    public class EnemyData
    {
        [SerializeField] public RagdollType RagdollType;
        [SerializeField] public SVector3 Position;
        [SerializeField] public SQuaternion Rotation;
        [SerializeField] public SVector3 Scale;       
    }

    [System.Serializable]
    public class SkinData
    {
        [SerializeField] public RagdollType RagdollType;
        [SerializeField] public SVector3 Position;
        [SerializeField] public SQuaternion Rotation;
        [SerializeField] public SVector3 Scale;
    }

    [System.Serializable]
    public class SaveRecord
    {
        [SerializeField] public string SaveName;
        [SerializeField] public string SceneName;
        [SerializeField] public string SavePath;
    }

    [System.Serializable]
    public class SaveRecordsList
    {
        [SerializeField] public List<SaveRecord> SaveRecords = new();
    }
}
