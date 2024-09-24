using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SaveRecordsManager
    {
        private static string GetRecordsFilePath()
        {
            return Path.Combine(Application.persistentDataPath, "SaveRecords.dat");
        }

        public static void SaveRecords(SaveRecordsList saveRecords)
        {
            string path = GetRecordsFilePath();
            BinaryFormatter formatter = new();
            using (FileStream file = new(path, FileMode.Create))
            {
                formatter.Serialize(file, saveRecords);
            }
        }

        public static SaveRecordsList LoadRecords()
        {
            string path = GetRecordsFilePath();
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new();
                using (FileStream file = new(path, FileMode.Open))
                {
                    return (SaveRecordsList)formatter.Deserialize(file);
                }
            }
            else
            {
                return new SaveRecordsList();
            }
        }

        public static void AddSaveRecord(string saveName, string sceneName, string savePath)
        {
            SaveRecordsList records = LoadRecords();

            records.SaveRecords.RemoveAll(record => record.SaveName == saveName);

            records.SaveRecords.Add(new SaveRecord
            {
                SaveName = saveName,
                SceneName = sceneName,
                SavePath = savePath
            });

            SaveRecords(records);
        }

        public static List<SaveRecord> GetSavesByScene(string sceneName)
        {
            SaveRecordsList records = LoadRecords();
            return records.SaveRecords.Where(record => record.SceneName == sceneName).ToList();
        }

        public static List<SaveRecord> GetAllSaves()
        {
            SaveRecordsList records = LoadRecords();
            return records.SaveRecords;
        }
    }
}
