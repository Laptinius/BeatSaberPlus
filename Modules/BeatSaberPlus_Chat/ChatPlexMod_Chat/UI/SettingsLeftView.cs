using CP_SDK.XUI;
using UnityEngine;

namespace ChatPlexMod_Chat.UI
{
    /// <summary>
    /// Setting left view
    /// </summary>
    internal sealed class SettingsLeftView : CP_SDK.UI.ViewController<SettingsLeftView>
    {
        private static readonly string s_InformationStr =
                     "<b>Thanks to <b>brian</b> for original ChatCore lib</b>"
            + "\n" + " - https://github.com/brian91292/ChatCore";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// On view creation
        /// </summary>
        protected override sealed void OnViewCreation()
        {
            Templates.FullRectLayout(
                Templates.TitleBar("Інформація / Подяки"),

                Templates.ScrollableInfos(50,
                    XUIText.Make(s_InformationStr)
                        .SetAlign(TMPro.TextAlignmentOptions.Left)
                ),

                Templates.ExpandedButtonsLine(
                    XUIPrimaryButton.Make("Reset",               OnResetButton),
                    XUIPrimaryButton.Make("Reset Position",      OnResetPositionButton),
                    XUIPrimaryButton.Make("Web Configuration",   OnWebConfigurationButton)
                ),
                Templates.ExpandedButtonsLine(
                    XUISecondaryButton.Make("Documentation", OnDocumentationButton)
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
            ShowConfirmationModal("<color=yellow><b>Справді скинути всі налаштування чату?", (x) =>
            {
                if (!x)
                    return;

                /// Reset settings
                CConfig.Instance.Reset();
                CConfig.Instance.Enabled = true;
                CConfig.Instance.Save();

                /// Refresh values
                SettingsMainView.Instance.RefreshSettings();
                SettingsRightView.Instance.RefreshSettings();

                /// Update floating view
                Chat.Instance.UpdateFloatingPanels();
            });
        }
        /// <summary>
        /// Reset position button
        /// </summary>
        private void OnResetPositionButton()
        {
            ShowConfirmationModal("<color=yellow><b>Справді скинути позицію чату?", (x) =>
            {
                if (!x)
                    return;

                /// Reset position settings
                CConfig.Instance.ResetPosition();
                CConfig.Instance.Save();

                /// Refresh values
                SettingsMainView.Instance.RefreshSettings();
                SettingsRightView.Instance.RefreshSettings();

                /// Update floating view
                Chat.Instance.UpdateFloatingPanels();
            });
        }
        /// <summary>
        /// Open web configuration button
        /// </summary>
        private void OnWebConfigurationButton()
        {
            ShowMessageModal("URL відкрито у вашому браузері.");
            CP_SDK.Chat.Service.OpenWebConfiguration();
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Documentation button
        /// </summary>
        private void OnDocumentationButton()
        {
            ShowMessageModal("URL відкрито у вашому браузері.");
            Application.OpenURL(Chat.Instance.DocumentationURL);
        }
    }
}
