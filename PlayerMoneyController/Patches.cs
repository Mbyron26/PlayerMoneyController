using Game;
using HarmonyLib;
using Game.Common;
using Game.Simulation;
using Colossal.Serialization.Entities;
using Unity.Entities;
using Game.City;

namespace PlayerMoneyController;

[HarmonyPatch(typeof(SystemOrder), "Initialize")]
public class Patch1 {
    public static void Postfix(UpdateSystem updateSystem) {
        updateSystem.World.GetOrCreateSystem<PlayerMoneyControllerSystem>();
        updateSystem.UpdateAt<CitySystem>(SystemUpdatePhase.Deserialize);
    }
}

[HarmonyPatch(typeof(CitySystem), nameof(CitySystem.PostDeserialize))]
public class Patch2 {
    public static void Postfix(CitySystem __instance, Context context) {
        if (!ModSettings.Instance.InitialMoneyEnabled)
            return;
        if (context.purpose == Purpose.NewGame) {
            __instance.EntityManager.SetComponentData(__instance.City, new PlayerMoney(ModSettings.Instance.InitialMoneyAmount));
        }
    }
}