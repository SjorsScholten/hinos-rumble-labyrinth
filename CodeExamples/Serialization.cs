using System;
using System.IO;
using UnityEngine;

namespace hinos.serialization {
    public class Serialization {
        private readonly string saveDirectory;

        public Serialization() {
            saveDirectory = $"{Application.persistantDataPath}/saves"
        }

        private void CreateSaveDirectory() {
            if(!Directory.Exists(dirName)) {
                Directory.CreateDirectory(dirName);
            }
        }

        public void SaveToFile(string fileName, object obj){
            var jsonString = JsonUtility.ToJson(obj);
            File.WriteAllText(saveDirectory, jsonString);
        }

        public T ReadFromFile<T>(string fileName) {
            var jsonString = File.ReadAllText(saveDirectory)
            return JsonUtility.FromJson<T>(jsonString);
        }
    }
}