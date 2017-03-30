using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SteveRGB.ExcelDnaExample
{
    public class Settings : Dictionary<string, string>
    {
        public const string AppDataFolder = "ExcelDnaExample";

        private const string SettingsFilename = "Settings.json";

        internal static Settings Default { get; } = new Settings();

        private static string SettingsFullFilename
            => Path.Combine(AppDataPath, SettingsFilename);

        public Settings()
        {
            base["API_Port"] = "8088";
            base["Geometry_ExampleWindow"] = "";

            var filename = Path.Combine(AppDataPath, SettingsFilename);
            if (!File.Exists(filename)) return;

            // Restore settings from file.
            var str = File.ReadAllText(SettingsFullFilename);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
            foreach (var key in dict.Keys)
            {
                // Ignore unsupported properties.
                if (ContainsKey(key))
                {
                    base[key] = dict[key];
                }
            }
        }

        internal static string AppDataPath
        {
            get
            {
                var appDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(appDataRoot, AppDataFolder);
            }
        }

        public void Save()
        {
            var str = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(SettingsFullFilename, str);
        }
    }
}
