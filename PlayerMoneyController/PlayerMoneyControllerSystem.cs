using Game;
using Game.City;
using Game.Simulation;
using UnityEngine.InputSystem;

namespace PlayerMoneyController;

public class PlayerMoneyControllerSystem : GameSystemBase {
    private CitySystem? citySystem;

    protected override void OnCreate() {
        base.OnCreate();
        citySystem = World.GetExistingSystemManaged<CitySystem>();
        if (citySystem is null) {
            Mod.Log.Error($"[CitySystem] is null");
            return;
        }
        CreateKeyBinding();
        Mod.Log.Info("PlayerMoneyControllerSystem: OnCreate");
    }

    private void CreateKeyBinding() {
        var inputAction = new InputAction("AddMoney");
        inputAction.AddCompositeBinding("ButtonWithTwoModifiers").With("Modifier1", "<Keyboard>/ctrl").With("Modifier2", "<Keyboard>/shift").With("Button", "<Keyboard>/equals");
        inputAction.performed += OnToggleAddMoney;
        inputAction.Enable();

        inputAction = new InputAction("SubtractMoney");
        inputAction.AddCompositeBinding("ButtonWithTwoModifiers").With("Modifier1", "<Keyboard>/ctrl").With("Modifier2", "<Keyboard>/shift").With("Button", "<Keyboard>/minus");
        inputAction.performed += OnToggleSubtractMoney;
        inputAction.Enable();
    }

    private void OnToggleAddMoney(InputAction.CallbackContext obj) {
        if (citySystem is null)
            return;
        PlayerMoney componentData = EntityManager.GetComponentData<PlayerMoney>(citySystem.City);
        componentData.Add(ModSettings.Instance.ManuallyMoneyAmount);
        EntityManager.SetComponentData(citySystem.City, componentData);
    }

    private void OnToggleSubtractMoney(InputAction.CallbackContext obj) {
        if (citySystem is null)
            return;
        PlayerMoney componentData = EntityManager.GetComponentData<PlayerMoney>(citySystem.City);
        componentData.Subtract(ModSettings.Instance.ManuallyMoneyAmount);
        EntityManager.SetComponentData(citySystem.City, componentData);
    }

    protected override void OnUpdate() { }
}
