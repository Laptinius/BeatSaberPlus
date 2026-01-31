using CP_SDK.XUI;
using UnityEngine;

namespace BeatSaberPlus_ChatRequest.UI
{
    /// <summary>
    /// Settings left view
    /// </summary>
    internal sealed class SettingsLeftView : CP_SDK.UI.ViewController<SettingsLeftView>
    {
        private static readonly string s_InformationStr =
                     "<b>Команди</b>"
            + "\n" + "- <b>!bsr #KEY/#NAME</b>\n<i><color=#CCCCCCFF>Запитати пісню за BSR‑кодом або пошуком за назвою</color></i>"
            + "\n" + "- <b>!bsrhelp</b>\n<i><color=#CCCCCCFF>Показати інструкцію, як запросити пісню</color></i>"
            + "\n" + "- <b>!oops !wrongsong !wrong</b>\n<i><color=#CCCCCCFF>Видалити останню пісню, запитану користувачем</color></i>"
            + "\n" + "- <b>!queue</b>\n<i><color=#CCCCCCFF>Показати пісні в черзі</color></i>"
            + "\n" + "- <b>!queuestatus</b>\n<i><color=#CCCCCCFF>Показати кількість пісень у черзі</color></i>"
            + "\n" + "- <b>!link</b>\n<i><color=#CCCCCCFF>Показати в чаті поточну або останню пісню</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!allow #KEY</b>\n<i><color=#CCCCCCFF>Дозволити мапу за BSR‑кодом, ігноруючи всі фільтри</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!att #KEY/#NAME</b>\n<i><color=#CCCCCCFF>Підняти будь‑яку пісню в черзі на початок за BSR‑кодом</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!block #KEY</b>\n<i><color=#CCCCCCFF>Заблокувати та видалити будь‑яку пісню з черги за BSR‑кодом</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!bsrban #USER</b>\n<i><color=#CCCCCCFF>Заборонити користувачу робити запити</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!bsrbanmapper #USER</b>\n<i><color=#CCCCCCFF>Заборонити запити мап цього мапера</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!bsrunban #USER</b>\n<i><color=#CCCCCCFF>Зняти користувача з бан‑листа запитів</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!bsrunbanmapper #USER</b>\n<i><color=#CCCCCCFF>Зняти мапера з бан‑листа</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!close</b>\n<i><color=#CCCCCCFF>Закрити чергу запитів</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!modadd #KEY/#NAME</b>\n<i><color=#CCCCCCFF>Додати пісню за BSR‑кодом, ігноруючи всі фільтри</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!mtt #KEY/#USER</b>\n<i><color=#CCCCCCFF>Перемістити будь‑яку пісню в черзі на початок за BSR‑кодом</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!open</b>\n<i><color=#CCCCCCFF>Відкрити чергу запитів</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!remap #KEY #KEY</b>\n<i><color=#CCCCCCFF>Замінити всі запити з лівого BSR на правий BSR</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!remove #KEY/#USER</b>\n<i><color=#CCCCCCFF>Видалити будь‑яку пісню з черги за BSR‑кодом</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!sabotage on/off</b>\n<i><color=#CCCCCCFF>Увімкнути або вимкнути бомби LIV streamer kit</color></i>"
            + "\n" + "- <color=yellow>[Moderator]</color><b>!songmsg #KEY #MSG</b>\n<i><color=#CCCCCCFF>Дозволити встановити повідомлення для запиту</color></i>"
            + "\n"
            + "\n" + "<b>Фільтри</b>"
            + "\n" + "- <b>Без BeatSage</b>\n<i><color=#CCCCCCFF>Відкинути всі авто‑змаплені мапи</color></i>"
            + "\n" + "- <b>NPS мін</b>\n<i><color=#CCCCCCFF>Відкинути мапи без складності з NPS вище мінімуму</color></i>"
            + "\n" + "- <b>NPS макс</b>\n<i><color=#CCCCCCFF>Відкинути мапи без складності з NPS нижче максимуму</color></i>"
            + "\n" + "- <b>NJS мін</b>\n<i><color=#CCCCCCFF>Відкинути мапи без складності з NJS вище мінімуму</color></i>"
            + "\n" + "- <b>NJS макс</b>\n<i><color=#CCCCCCFF>Відкинути мапи без складності з NJS нижче максимуму</color></i>"
            + "\n" + "- <b>Тривалість макс</b>\n<i><color=#CCCCCCFF>Максимальна тривалість мапи в хвилинах</color></i>"
            + "\n" + "- <b>Мін. рейтинг</b>\n<i><color=#CCCCCCFF>Необхідний відсоток позитивних голосів</color></i>"
            + "\n" + "- <b>Мін. дата завантаження</b>\n<i><color=#CCCCCCFF>Мінімальна дата завантаження</color></i>"
            + "\n" + "- <b>Макс. дата завантаження</b>\n<i><color=#CCCCCCFF>Максимальна дата завантаження</color></i>";

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
                    XUIPrimaryButton.Make("OBS інтеграція",      OnOBSIntegrationButton),
                    XUIPrimaryButton.Make("Скинути",             OnResetButton),
                    XUIPrimaryButton.Make("Веб‑налаштування",    OnWebConfigurationButton)
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
        /// Open OBS integration button
        /// </summary>
        private void OnOBSIntegrationButton()
        {
            ShowMessageModal("URL відкрито у вашому браузері.");
            Application.OpenURL("https://github.com/hardcpp/BeatSaberPlus/wiki#chat-request---obs-integration");
        }
        /// <summary>
        /// Reset button
        /// </summary>
        private void OnResetButton()
        {
            ShowConfirmationModal("<color=yellow><b>Справді скинути\nналаштування запитів і фільтри?", (p_Confirm) =>
            {
                if (!p_Confirm)
                    return;

                /// Reset config
                CRConfig.Instance.Reset();
                CRConfig.Instance.Enabled = true;
                CRConfig.Instance.Save();

                /// Refresh values
                SettingsMainView.Instance.RefreshSettings();
                SettingsRightView.Instance.RefreshSettings();
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
            Application.OpenURL(ChatRequest.Instance.DocumentationURL);
        }
    }
}
