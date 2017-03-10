using Lshp.OpenIDConnect.Util.ConfigManager;
using System;

namespace Lshp.OpenIDConnect.Util
{
    public class ConfigurationManager
    {
        private static readonly ConfigurationManager Instance = new ConfigurationManager();
        private ConfigEntry _config;
        private bool _isInitialized;

        private ConfigurationManager()
        {

        }

        public static ConfigurationManager GetInstance()
        {
            return Instance;
        }

        public void Init(string directoryPath)
        {
            if (!_isInitialized)
            {
                _config = LoadConfigFileToDictionary(directoryPath);
                _isInitialized = true;
            }
        }

        public ConfigEntry GetConfig()
        {
            return _config;
        }

        private ConfigEntry LoadConfigFileToDictionary(String filePath)
        {
            var configDoc = LoadConfigurationFile(filePath);
            return ConfigToDictionary(configDoc);
        }


        private string LoadConfigurationFile(string filePath)
        {
            string Json = System.IO.File.ReadAllText(filePath);
            return Json;
        }

        private ConfigEntry ConfigToDictionary(string json)
        {
            return Jil.JSON.Deserialize<ConfigEntry>(json);
        } 
    }
}
