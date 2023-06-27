using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Windows;
using UnityEngine;

using Input = UnityEngine.Input;

namespace TogglePauseButton.Patch
{
    [HarmonyPatch(typeof(PauseMenuManager), nameof(PauseMenuManager.Update))]
    internal class PauseMenuManagerPatch
    {
        private static bool s_invoked = false;

        public static bool Prefix(PauseMenuManager __instance, ref float ____disabledInteractionRemainingTime, IVRPlatformHelper ____vrPlatformHelper, ref bool __runOriginal)
        {
            if (!__runOriginal) {
                return __runOriginal;
            }
            __runOriginal = false;
            InternalUpdate(__instance, ref ____disabledInteractionRemainingTime, ____vrPlatformHelper);
            return __runOriginal;
        }

        private static void InternalUpdate(PauseMenuManager instance, ref float disabledInteractionRemainingTime, IVRPlatformHelper vrPlatformHelper)
        {
            if (disabledInteractionRemainingTime > 0f) {
                disabledInteractionRemainingTime -= Time.deltaTime;
                return;
            }
            if (vrPlatformHelper.GetMenuButton() && ButtonStateEntity.Timer <= ButtonStateEntity.PrevTime) {
                if (!s_invoked) {
                    instance.ContinueButtonPressed();
                    s_invoked = true;
                }
            }
            else {
                s_invoked = false;
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                instance.RestartButtonPressed();
            }
            if (Input.GetKeyDown(KeyCode.M)) {
                instance.MenuButtonPressed();
            }
            if (Input.GetKeyDown(KeyCode.C)) {
                instance.ContinueButtonPressed();
            }
        }
    }
}
