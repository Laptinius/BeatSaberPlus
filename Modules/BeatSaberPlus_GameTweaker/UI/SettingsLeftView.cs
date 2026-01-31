using CP_SDK.XUI;
using UnityEngine;

namespace BeatSaberPlus_GameTweaker.UI
{
    /// <summary>
    /// Settings left view
    /// </summary>
    internal sealed class SettingsLeftView : CP_SDK.UI.ViewController<SettingsLeftView>
    {
        private static readonly string s_InformationStr = "<line-height=125%><b>Game Tweaker</b>"
            + "\n" + "<i><color=#CCCCCCFF>Цей модуль дозволяє налаштовувати ігровий досвід, прибирати нудні базові фічі та додавати нові корисні твіки</color></i>";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// On view creation
        /// </summary>
        protected override sealed void OnViewCreation()
        {
            Templates.FullRectLayout(
                Templates.TitleBar("Інформація"),

                Templates.ScrollableInfos(50,
                    XUIText.Make(s_InformationStr)
                        .SetAlign(TMPro.TextAlignmentOptions.Left)
                ),

                Templates.ExpandedButtonsLine(
                    XUIPrimaryButton.Make("Скинути", OnResetButton)
                ),
                Templates.ExpandedButtonsLine(
                    XUISecondaryButton.Make("Документація", OnDocumentationButton)
                )
            )
            .SetBackground(true, null, true)
            .BuildUI(transform);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Reset button
        /// </summary>
        private void OnResetButton()
        {
            ShowConfirmationModal("<color=yellow><b>Справді скинути\nналаштування Game Tweaker?", (p_Confirm) =>
            {
                if (!p_Confirm)
                    return;

                /// Reset config
                GTConfig.Instance.Reset();
                GTConfig.Instance.Enabled = true;
                GTConfig.Instance.Save();

                /// Refresh values
                SettingsMainView.Instance.RefreshSettings();
            });
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Documentation button
        /// </summary>
        private void OnDocumentationButton()
        {
            ShowMessageModal("URL відкрито у вашому браузері.");
            Application.OpenURL(GameTweaker.Instance.DocumentationURL);
        }
    }
}
