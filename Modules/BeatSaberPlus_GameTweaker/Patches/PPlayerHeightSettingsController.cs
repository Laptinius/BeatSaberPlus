using HarmonyLib;
using TMPro;
using System.Reflection;

namespace BeatSaberPlus_GameTweaker.Patches
{
    /// <summary>
    /// PlayerHeightSettingsController remover
    /// </summary>
[HarmonyPatch(typeof(PlayerHeightSettingsController))]
public class PPlayerHeightSettingsController
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(typeof(PlayerHeightSettingsController), "RefreshUI")
            ?? AccessTools.Method(typeof(PlayerHeightSettingsController), "UpdateUI");
    }

        /// <summary>
        /// Prefix
        /// </summary>
        /// <param name="____text">Target text display</param>
        /// <param name="____value">Player height meters</param>
        internal static void Postfix(ref TextMeshProUGUI ____text, ref float ____value)
        {
            if (GTConfig.Instance.Enabled)
                ____text.text = string.Format("{0:0.00}", ____value);
        }
    }
}
