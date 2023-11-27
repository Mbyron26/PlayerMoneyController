using BepInEx;
using BepInEx.Unity.Mono;
using Colossal.Logging;
using HarmonyLib;
using System.Linq;
using System.Reflection;

namespace PlayerMoneyController;

[BepInPlugin(RAWMODNAME, MODNAME, MODVERSION)]
public class Mod : BaseUnityPlugin {
    public const string RAWMODNAME = "PlayerMoneyController";
    public const string MODNAME = "Player Money Controller";
    public const string MODVERSION = "0.1.0";
    public static readonly ILog Log = LogManager.GetLogger(RAWMODNAME, true);

    private void Awake() {
        Logger.LogInfo($"Plugin {MODNAME} is loaded!");
        var harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), RAWMODNAME + "_Cities2Harmony");
        var patchedMethods = harmony.GetPatchedMethods().ToArray();

        Logger.LogInfo($"Plugin {MODNAME} made patches! Patched methods: " + patchedMethods.Length);

        foreach (var patchedMethod in patchedMethods) {
            Logger.LogInfo($"Patched method: {patchedMethod.Module.Name}:{patchedMethod.Name}");
        }
    }

}

