using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CsharpConsole
{
    public static class DataManager
    {
        const string DIR_NAME = "data";
        public static void SaveData<T>(string fileName, T data)
        {
            string filePath = Path.Combine(DIR_NAME, $"{fileName}.json");
            string json = JsonConvert.SerializeObject(data);
            if (!Directory.Exists(DIR_NAME))
            {
                Directory.CreateDirectory(DIR_NAME);
            }
            File.WriteAllText(filePath, json);
        }

        public static T LoadData<T>(string fileName)
        {
            string filePath = Path.Combine(DIR_NAME, $"{fileName}.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                if (json != null)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            return default(T);
        }
    }
}