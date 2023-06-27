using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TogglePauseButton.Patch
{



    [HarmonyPatch(typeof(InstantMenuButtonTrigger), nameof(InstantMenuButtonTrigger.Tick))]
    internal class InstantMenuButtonTriggerPatch
    {
        public static bool Prefix(IVRPlatformHelper ____vrPlatformHelper, ref bool __runOriginal, Action ___menuButtonTriggeredEvent)
        {
            if (!__runOriginal) {
                return __runOriginal;
            }
            __runOriginal = false;
            RaiseEvent(____vrPlatformHelper, ___menuButtonTriggeredEvent);
            return __runOriginal;
        }

        private static void RaiseEvent(IVRPlatformHelper vrPlatformHelper, Action action)
        {
            if (vrPlatformHelper.GetMenuButton()) {
                ButtonStateEntity.Timer += Time.deltaTime;
                if (ButtonStateEntity.IsReleased && ButtonStateEntity.Timer < ButtonStateEntity.PrevTime) {
                    ButtonStateEntity.IsReleased = false;
                    ButtonStateEntity.PrevTime = ButtonStateEntity.Timer;
                    action?.Invoke();
                }
            }
            else {
                ButtonStateEntity.IsReleased = true;
                ButtonStateEntity.Timer = 0f;
                ButtonStateEntity.PrevTime = float.MaxValue;
            }
        }
    }

    [HarmonyPatch(typeof(DelayedMenuButtonTrigger), nameof(DelayedMenuButtonTrigger.Tick))]
    internal class DelayedMenuButtonTriggerPatch
    {

        public static bool Prefix(ref float ____pressDuration, ref float ____timer, ref bool ____waitingForButtonRelease, IVRPlatformHelper ____vrPlatformHelper, Action ___menuButtonTriggeredEvent, ref bool __runOriginal)
        {
            if (!__runOriginal) {
                return __runOriginal;
            }
            __runOriginal = false;
            RaiseEvent(____pressDuration, ref ____timer, ref ____waitingForButtonRelease, ____vrPlatformHelper, ___menuButtonTriggeredEvent);
            return __runOriginal;
        }

        private static void RaiseEvent(float pressDuration, ref float timer, ref bool waitingForButtonRelease, IVRPlatformHelper vrPlatformHelper, Action action)
        {
            if (vrPlatformHelper.GetMenuButton()) {
                timer += Time.deltaTime;
                ButtonStateEntity.Timer = timer;
                if (timer > pressDuration && !waitingForButtonRelease && ButtonStateEntity.Timer < ButtonStateEntity.PrevTime) {
                    ButtonStateEntity.IsReleased = false;
                    ButtonStateEntity.PrevTime = timer;
                    waitingForButtonRelease = true;
                    action?.Invoke();
                }
            }
            else {
                waitingForButtonRelease = false;
                timer = 0f;
                ButtonStateEntity.IsReleased = true;
                ButtonStateEntity.Timer = 0f;
                ButtonStateEntity.PrevTime = float.MaxValue;
            }
        }
    }
}
