using HarmonyLib;
using UnityEngine.XR;

namespace VRTweaks
{

    [HarmonyPatch(typeof(VROptions), "GetUseGazeBasedCursor")]
    public static class AlternateGazeCursor
    {
        [HarmonyPrefix]
        public static bool Prefix(ref bool __result)
        {
			__result = (VROptions.gazeBasedCursor || SnapTurningOptions.GamepadGazeCursor) && XRSettings.enabled;
			return false;
        }
    }


    [HarmonyPatch(typeof(IngameMenu), "UpdateRaycasterStatus")]
    public static class PauseMenu
    {
        [HarmonyPrefix]
        public static bool Prefix(ref uGUI_GraphicRaycaster raycaster, IngameMenu __instance)
        {
            if (GameInput.IsPrimaryDeviceGamepad() && !SnapTurningOptions.GamepadGazeCursor)
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
    public static class PDAMenu
    {
        [HarmonyPrefix]
        public static bool Prefix(ref uGUI_GraphicRaycaster raycaster, uGUI_PDA __instance)
        {
            if (GameInput.IsPrimaryDeviceGamepad() && !SnapTurningOptions.GamepadGazeCursor)
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


    //[HarmonyPatch(typeof(uGUI_CraftingMenu), "SetRaycasterStatus")]
    //public static class CraftingMenuRaycaster
    //{
    //    [HarmonyPrefix]
    //    public static bool Prefix(ref uGUI_GraphicRaycaster raycaster, uGUI_CraftingMenu __instance)
    //    {
    //        if (GameInput.IsPrimaryDeviceGamepad() && !SnapTurningOptions.GamepadGazeCursor)
    //        {
    //            raycaster.enabled = false;
    //        }
    //        else
    //        {
    //            raycaster.enabled = __instance.focused;
    //        }

    //        return false;
    //    }
    //}


	//[HarmonyPatch(typeof(uGUI_CraftingMenu), "SetRaycasterStatus")]
	//public static class CraftingMenuPointerBehavior
	//{
 //       [HarmonyPrefix]
	//	public static bool Prefix(ref uGUI_ItemIcon icon, ref int button, uGUI_CraftingMenu __instance bool __result)
	//	{
	//		bool flag = GameInput.GetPrimaryDevice() == GameInput.Device.Controller;
	//		if (!__instance.locked)
	//		{
	//			uGUI_CraftingMenu.Node node = __instance.GetNode(icon);
	//			if (node != null)
	//			{
	//				if (flag)
	//				{
	//					switch (button)
	//					{
	//						case 0:
	//							if (node.action == TreeAction.Craft)
	//							{
	//								__instance.Action(node);
	//							}
	//							else
	//							{
	//								((uGUI_INavigableIconGrid)__instance.SelectItemInDirection(1, 0);
	//							}
	//							break;
	//						case 1:
	//							__instance.Out(node.parent as uGUI_CraftingMenu.Node);
	//							break;
	//						case 2:
	//							if (node.action == TreeAction.Craft)
	//							{
	//								TechType techType = node.techType;
	//								if (CrafterLogic.IsCraftRecipeUnlocked(techType))
	//								{
	//									PinManager.TogglePin(techType);
	//								}
	//							}
	//							break;
	//					}
	//				}
	//				else if (button != 0)
	//				{
	//					if (button == 1)
	//					{
	//						if (node.action == TreeAction.Craft)
	//						{
	//							TechType techType2 = node.techType;
	//							if (CrafterLogic.IsCraftRecipeUnlocked(techType2))
	//							{
	//								PinManager.TogglePin(techType2);
	//							}
	//						}
	//					}
	//				}
	//				else
	//				{
	//					__instance.Action(node);
	//				}
	//			}

	//			return true;
	//		}
	//		if (flag && button == 1)
	//		{
	//			__instance.Deselect(null);
	//			return true;
	//		}
	//		return false;
	//	}
	//}
}
