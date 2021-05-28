using HarmonyLib;
using VRTweaks.Utilities.GamepadGazeCursorFixer;

namespace VRTweaks.GamepadGazeCursorFixer
{
    [HarmonyPatch(typeof(uGUI_PDA), "UpdateRaycasterStatus")]
    public static class PDAMenuRaycaster
    {
        public static bool Prefix(ref uGUI_GraphicRaycaster raycaster, uGUI_PDA __instance)
        {
            RaycastFixer.Fix(ref raycaster, __instance);
            return false;
        }
    }
}
