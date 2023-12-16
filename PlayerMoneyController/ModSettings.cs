using Colossal.Json;
using Colossal.PSI.Environment;
using System;
using System.IO;

namespace PlayerMoneyController;

public class ModSettings {
    private static readonly string ModSettingsFileName = Mod.RAWMODNAME + "Settings";
    private static readonly string JSONExtension = ".json";
    private static readonly string ModSettingsPath = EnvPath.kUserDataPath + "/" + ModSettingsFileName + JSONExtension;

    public static ModSettings Instance {
        get {
            if (!TryGetSettings(out ModSettings settings)) {
                SaveSettings(settings);
            }
            return settings;
        }
    }

    public int ManuallyMoneyAmount { get; set; } = 1000000;
    public bool InitialMoneyEnabled { get; set; } = false;
    public int InitialMoneyAmount { get; set; } = 500000;

    public static void SaveSettings(ModSettings settings) {
        try {
            string contents = JSON.Dump(settings, EncodeOptions.None);
            File.WriteAllText(ModSettingsPath, contents);
            Mod.Log.Info("Settings saved successfully");
        } catch (Exception p) {
            Mod.Log.InfoFormat("Saving settings failed: {0}", p);
        }

    }

    private static bool TryGetSettings(out ModSettings settings) {
        if (File.Exists(ModSettingsPath)) {
            try {
                Variant variant = JSON.Load(File.ReadAllText(ModSettingsPath));
                settings = variant.Make<ModSettings>();
                Mod.Log.Info("Loaded settings successfully");
                SaveSettings(settings);
                return true;
            } catch (Exception p) {
                Mod.Log.InfoFormat("Loading settings failed: {0}", p);
            }
        }
        Mod.Log.Info("Settings not present");
        settings = new ModSettings();
        return false;
    }

}
