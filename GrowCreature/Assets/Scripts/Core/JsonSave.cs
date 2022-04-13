using System.IO;
using UnityEngine;

namespace Core
{
    public abstract class JsonSave
    {
        public static void SaveData<T>(T data, string fileName)
        {
            string dataPath = Application.persistentDataPath + "/" + fileName;
            //Debug.Log(dataPath);
            string jsonString = JsonUtility.ToJson(data);
            File.WriteAllText(dataPath, jsonString);
        }

        public static T LoadData<T>(string fileName)
        {
            string dataPath = Application.persistentDataPath + "/" + fileName;
            if (!File.Exists(dataPath)) return default;
            string fileContents = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<T>(fileContents);
        }
    }
}