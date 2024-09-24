using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SaveLoadSystem
    {
        public static string GetSavePath(string saveName)
        {
            return Path.Combine(Application.persistentDataPath, saveName + ".sav");
        }

        public static void Save(SaveData data)
        {
            string path = GetSavePath(data.SaveName);
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(file, data);
            }
        }

        public static SaveData Load(string saveName)
        {
            string path = GetSavePath(saveName);
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream file = new FileStream(path, FileMode.Open))
                {
                    return (SaveData)formatter.Deserialize(file);
                }
            }
            else
            {
                Debug.LogError($"Save file {saveName} not found!");
                return null;
            }
        }

        public static List<string> GetAllSaves()
        {
            DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
            FileInfo[] files = dir.GetFiles("*.sav");
            List<string> saveNames = new List<string>();

            foreach (FileInfo file in files)
            {
                saveNames.Add(Path.GetFileNameWithoutExtension(file.Name));
            }

            return saveNames;
        }
    }
}
