using CP_SDK.XUI;
using UnityEngine;

namespace ChatPlexMod_ChatIntegrations.UI
{
    /// <summary>
    /// Settings left view controller
    /// </summary>
    internal sealed class SettingsLeftView : CP_SDK.UI.ViewController<SettingsLeftView>
    {
        private static readonly string s_InformationStr =
                     "<b>Особлива подяка HypersonicSharkz#3301 за допомогу з TwitchAPI та кодом дій!"
            + "\n" + "Цей модуль дозволяє виконувати дії у грі, коли спрацьовують події."
            + "\n" + ""
            + "\n" + "<b>Події</b>"
            + "\n" + "- <color=yellow><b>ChatBits</b></color>\n<i><color=#CCCCCCFF>Коли хтось витрачає біти на вашому каналі!</color></i>"
            + "\n" + "- <color=yellow><b>ChatCommand</b></color>\n<i><color=#CCCCCCFF>Дозволяє створювати чат-команди та виконувати дії за ними</color></i>"
            + "\n" + "- <color=yellow><b>ChatFollow</b></color>\n<i><color=#CCCCCCFF>Коли хтось фоловить ваш канал</color></i>"
            + "\n" + "- <color=yellow><b>ChatPointsReward</b></color>\n<i><color=#CCCCCCFF>Дозволяє створювати нагороди Channel Points, налаштовувати їх і прив'язувати дії</color></i>"
            + "\n" + "- <color=yellow><b>ChatSubscription</b></color>\n<i><color=#CCCCCCFF>Коли хтось підписується або дарує підписку</color></i>"
            + "\n" + "- <color=yellow><b>Dummy</b></color>\n<i><color=#CCCCCCFF>Фіктивна подія, яку можуть запускати інші події</color></i>"
            + "\n" + "- <color=yellow><b>LevelEnded</b></color>\n<i><color=#CCCCCCFF>Коли ви виходите з мапи</color></i>"
            + "\n" + "- <color=yellow><b>LevelPaused</b></color>\n<i><color=#CCCCCCFF>Коли ви ставите мапу на паузу</color></i>"
            + "\n" + "- <color=yellow><b>LevelResumed</b></color>\n<i><color=#CCCCCCFF>Коли ви відновлюєте мапу</color></i>"
            + "\n" + "- <color=yellow><b>LevelStarted</b></color>\n<i><color=#CCCCCCFF>Коли ви заходите на мапу</color></i>"
            + "\n" + "- <color=yellow><b>VoiceAttackCommand</b></color>\n<i><color=#CCCCCCFF>Привʼязує команди VoiceAttack до BS+</color></i>"
            + "\n" + ""
            + "\n" + "";

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
                    XUIPrimaryButton.Make("Веб‑налаштування", OnWebConfigurationButton)
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
            Application.OpenURL(ChatIntegrations.Instance.DocumentationURL);
        }
    }
}
