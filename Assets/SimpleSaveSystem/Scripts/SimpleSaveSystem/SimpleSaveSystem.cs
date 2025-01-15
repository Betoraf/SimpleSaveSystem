using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace Betoraf.SimpleSaveSystem
{
    public static class SimpleSaveSystem
    {
        // Path to the save file, stored in persistent data path.
        private static string SaveFilePath => Application.persistentDataPath + "/save.json";

        // Method to retrieve a saved value by its name, if it exists, or return the default value.
        public static T Get<T>(string name, T startValue)
        {
            // Check if the save file exists.
            if (File.Exists(SaveFilePath))
            {
                // Read the save file as a JSON string.
                string json = File.ReadAllText(SaveFilePath);
                // Deserialize the JSON into a SaveData object.
                var data = JsonConvert.DeserializeObject<SaveData>(json);

                // If data exists and contains the requested key, return the corresponding value.
                if (data != null && data.ContainsKey(name))
                {
                    return JsonConvert.DeserializeObject<T>(data[name]);
                }
            }
            // If the data doesn't exist, return the default starting value.
            return startValue;
        }

        // Method to save a value with a given name.
        public static void Set<T>(string name, T value)
        {
            // Initialize a new SaveData object to store the values.
            SaveData data = new SaveData();

            // If the save file exists, load the current data.
            if (File.Exists(SaveFilePath))
            {
                string json = File.ReadAllText(SaveFilePath);
                // Deserialize the data, or use a new SaveData if the file is empty.
                data = JsonConvert.DeserializeObject<SaveData>(json) ?? new SaveData();
            }

            // Serialize the value and add it to the SaveData dictionary.
            data[name] = JsonConvert.SerializeObject(value);

            // Serialize the entire data dictionary and write it back to the file.
            string saveJson = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(SaveFilePath, saveJson);
        }

        // Method to delete a saved value by its name.
        public static void Delete(string name)
        {
            // Check if the save file exists.
            if (File.Exists(SaveFilePath))
            {
                string json = File.ReadAllText(SaveFilePath);
                var data = JsonConvert.DeserializeObject<SaveData>(json);

                // If data exists and contains the key to delete, remove it.
                if (data != null && data.ContainsKey(name))
                {
                    data.values.Remove(name);
                    // Serialize the updated data and write it back to the file.
                    string saveJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                    File.WriteAllText(SaveFilePath, saveJson);
                }
            }
        }

        // Method to check if the save file exists.
        public static bool SaveExists()
        {
            return File.Exists(SaveFilePath);
        }

#if UNITY_EDITOR
        // Editor utility to clear all saved data from PlayerPrefs and the save file.
        [UnityEditor.MenuItem("Tools/Clear save")]
        public static void Clear()
        {
            // Clear PlayerPrefs.
            PlayerPrefs.DeleteAll();

            // If the save file exists, delete it.
            if (File.Exists(SaveFilePath))
            {
                File.Delete(SaveFilePath);
            }
        }
#endif

        // Inner class that represents the structure of saved data.
        [System.Serializable]
        private class SaveData
        {
            // A dictionary to store key-value pairs for the saved data.
            public System.Collections.Generic.Dictionary<string, string> values = new System.Collections.Generic.Dictionary<string, string>();

            // Check if a given key exists in the dictionary.
            public bool ContainsKey(string key) => values.ContainsKey(key);

            // Indexer to get or set values using the dictionary key.
            public string this[string key]
            {
                get => values.ContainsKey(key) ? values[key] : null;
                set => values[key] = value;
            }
        }
    }
}