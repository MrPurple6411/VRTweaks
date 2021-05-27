using HarmonyLib;

namespace VRTweaks
{
    [HarmonyPatch(typeof(IngameMenu), "UpdateRaycasterStatus")]
    public static class PauseMenuRaycaster
    {
        [HarmonyPrefix]
        public static bool Prefix(ref uGUI_GraphicRaycaster raycaster, IngameMenu __instance)
        {
            if (GameInput.IsPrimaryDeviceGamepad() && !VROptions.GetUseGazeBasedCursor())
            {
                raycaster.enabled = false;
            }
            else
            {
                raycaster.enabled = __instance.focused;
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(uGUI_PDA), "UpdateRaycasterStatus")]
    public static class PDAMenuRaycaster
    {
        [HarmonyPrefix]
        public static bool Prefix(ref uGUI_GraphicRaycaster raycaster, uGUI_PDA __instance)
        {
            if (GameInput.IsPrimaryDeviceGamepad() && !VROptions.GetUseGazeBasedCursor())
            {
                raycaster.enabled = false;
            }
            else
            {
                raycaster.enabled = __instance.focused;
            }

            return false;
        }
    }


    [HarmonyPatch(typeof(uGUI_CraftingMenu), "SetRaycasterStatus")]
    public static class CraftingMenuRaycaster
    {
        [HarmonyPrefix]
        public static bool Prefix(ref uGUI_GraphicRaycaster raycaster, uGUI_CraftingMenu __instance)
        {
            if (GameInput.IsPrimaryDeviceGamepad() && !VROptions.GetUseGazeBasedCursor())
            {
                raycaster.enabled = false;
            }
            else
            {
                raycaster.enabled = __instance.focused;
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(uGUI_CraftingMenu), "uGUI_IIconManager.OnPointerClick")]
    public static class CraftingMenuOnPointerClick
    {
        private static bool _didSpoofKeyboard;

        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (VROptions.GetUseGazeBasedCursor())
            {
                GameInputSpoofer.ShouldSpoofKeyboard = true;
                _didSpoofKeyboard = true;
            }

            return true;
        }

        [HarmonyPostfix]
        public static void Postfix()
        {
            if (_didSpoofKeyboard)
            {
                GameInputSpoofer.ShouldSpoofKeyboard = false;
            }
        }
    }

    [HarmonyPatch(typeof(GameInput), nameof(GameInput.GetPrimaryDevice))]
    public static class GameInputSpoofer
    {
        public static bool ShouldSpoofKeyboard { get; set; }

        [HarmonyPrefix]
        public static bool Prefix(ref GameInput.Device __result)
        {
            if(ShouldSpoofKeyboard)
            {
                __result = GameInput.Device.Keyboard;
                return false;
            }

            return true;
        }
    }
}
