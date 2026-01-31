using CP_SDK.XUI;
using UnityEngine;

namespace ChatPlexMod_MenuMusic.UI
{
    /// <summary>
    /// Settings left view
    /// </summary>
    internal sealed class SettingsLeftView : CP_SDK.UI.ViewController<SettingsLeftView>
    {
        private static readonly string s_InformationStr =
                      "<b>Дякуємо <b>Lunikc</b> за оригінальну ідею</b>" + "\n" +
                     $"Папка з власною музикою: UserData/{CP_SDK.ChatPlexSDK.ProductName}Plus/MenuMusic/CustomMusic";

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
                    XUIPrimaryButton.Make("Скинути", OnResetButton),
                    XUIPrimaryButton.Make("Перезавантажити", OnReloadButton)
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
            ShowConfirmationModal("<color=yellow><b>Справді скинути\nусі налаштування Menu Music?", (p_Confirm) =>
            {
                if (!p_Confirm)
                    return;

                MMConfig.Instance.Reset();
                MMConfig.Instance.Enabled = true;
                MMConfig.Instance.Save();

                SettingsMainView.Instance.OnResetButton();

                OnReloadButton();
            });
        }
        /// <summary>
        /// Reload songs
        /// </summary>
        private void OnReloadButton()
        {
            MenuMusic.Instance.UpdateMusicProvider();

            ShowMessageModal("Музику перезавантажено!");
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Documentation button
        /// </summary>
        private void OnDocumentationButton()
        {
            ShowMessageModal("URL відкрито у вашому браузері.");
            Application.OpenURL(MenuMusic.Instance.DocumentationURL);
        }
    }
}
