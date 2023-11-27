using Game.Audio;
using Game;
using HarmonyLib;

namespace PlayerMoneyController;

[HarmonyPatch(typeof(AudioManager), "OnGameLoadingComplete")]
public class Patch0 {
    public static void Postfix(AudioManager __instance, Colossal.Serialization.Entities.Purpose purpose, GameMode mode) {
        if (!mode.IsGameOrEditor())
            return;
        __instance.World.GetOrCreateSystem<PlayerMoneyControllerSystem>();
    }
}
