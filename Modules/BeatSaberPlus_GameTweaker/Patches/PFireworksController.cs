using HarmonyLib;

using System.Reflection;

namespace BeatSaberPlus_GameTweaker.Patches
{
    /// <summary>
    /// FireworksController disabler
    /// </summary>
[HarmonyPatch(typeof(FireworksController))]
public class PFireworksController
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(typeof(FireworksController), "OnEnable");
    }

        /// <summary>
        /// Prefix
        /// </summary>
        internal static bool Prefix()
        {
            if (!GTConfig.Instance.Enabled || !GTConfig.Instance.MainMenu.DisableFireworks)
                return true; ///< Continue to original function

            return false;   ///< Skip original function
        }
    }
}
