using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using HarmonyLib;
using System;
using Platform.Utils;
using FMODUnity;
using UnityEngine.Events;

namespace VRTweaks
{
        [HarmonyPatch(typeof(FPSInputModule), nameof(FPSInputModule.EscapeMenu))]
        class FPSInputModule_EscapeMenu_Patch
        {
            [HarmonyPrefix]
            static void Prefix(FPSInputModule __instance)
            {
                if (__instance.lockPauseMenu)
                {
                    return;
                }
                //Press and hold pda to access escape menu with touch controllers (should be left controller menu button by default)
                if (GameInput.GetButtonDown(GameInput.Button.UIMenu) && IngameMenu.main != null && !IngameMenu.main.selected || GameInput.GetButtonHeldTime(GameInput.Button.PDA) > 1.0f)
                {
                    IngameMenu.main.Open();
                    GameInput.ClearInput();
                }
            }
        }
}
