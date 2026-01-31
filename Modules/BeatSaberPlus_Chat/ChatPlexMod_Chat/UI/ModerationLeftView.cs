using CP_SDK.XUI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace ChatPlexMod_Chat.UI
{
    /// <summary>
    /// Moderation left screen
    /// </summary>
    internal sealed class ModerationLeftView : CP_SDK.UI.ViewController<ModerationLeftView>
    {
        private XUIText m_RoomStateText = null;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// On view creation
        /// </summary>
        protected override void OnViewCreation()
        {
            Templates.FullRectLayout(
                Templates.TitleBar("Дії каналу"),

                XUIVLayout.Make(
                    XUIVSpacer.Make(5f),

                    XUIText.Make("Стан чату: Звичайний")
                        .SetAlign(TMPro.TextAlignmentOptions.Center)
                        .Bind(ref m_RoomStateText),

                    XUIVSpacer.Make(5f),

                    XUIPrimaryButton.Make("Перемкнути режим лише емоти",   OnToggleEmoteOnlyModeButton),
                    XUIPrimaryButton.Make("Перемкнути режим для фоловерів", OnToggleFollowerModeButton),
                    XUIPrimaryButton.Make("Перемкнути повільний режим",     OnToggleSlowModeButton),

                    XUIVSpacer.Make(5f),

                    XUISecondaryButton.Make("Керувати швидкими діями",      OnManageShortcutButton),

                    XUIVSpacer.Make(5f)
                )
                .SetWidth(60f)
                .SetPadding(0)
                .ForEachDirect<XUIPrimaryButton>(y =>
                {
                    y.SetHeight(8f);
                    y.OnReady((x) => x.CSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained);
                })
                .ForEachDirect<XUISecondaryButton>(y =>
                {
                    y.SetHeight(8f);
                    y.OnReady((x) => x.CSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained);
                })
            )
            .SetBackground(true, null, true)
            .BuildUI(transform);
        }
        /// <summary>
        /// On view activation
        /// </summary>
        protected override sealed void OnViewActivation()
            => UpdateRoomState();

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Update room state
        /// </summary>
        internal void UpdateRoomState()
        {
            if (!UICreated)
                return;

            var l_States = new List<string>();

            if (CP_SDK.Chat.Service.Multiplexer.Channels.Count != 0)
            {
                var l_Channel = CP_SDK.Chat.Service.Multiplexer.Channels.First();
                if (l_Channel.Item2 is CP_SDK.Chat.Models.Twitch.TwitchChannel)
                {
                    var l_TwitchChannel = l_Channel.Item2 as CP_SDK.Chat.Models.Twitch.TwitchChannel;

                    if (l_TwitchChannel.Roomstate.EmoteOnly)                l_States.Add("Лише емоти");
                    if (l_TwitchChannel.Roomstate.FollowersOnly)            l_States.Add("Лише фоловери");
                    if (l_TwitchChannel.Roomstate.SlowModeInterval != 0)    l_States.Add("Повільний режим (" + l_TwitchChannel.Roomstate.SlowModeInterval + "с)");
                }
            }

            if (l_States.Count == 0)
                l_States.Add("Звичайний");

            m_RoomStateText.SetText("Стан чату: " + string.Join(", ", l_States.ToArray()));
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Check for moderation permissions
        /// </summary>
        /// <returns></returns>
        private bool CheckForModeratorPermissions()
        {
            if (CP_SDK.Chat.Service.Multiplexer.Channels.Count == 0)
            {
                ShowMessageModal("Ви не під'єднані до каналу чату!");
                return false;
            }

            /// todo

            return true;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Toggle emote only
        /// </summary>
        private void OnToggleEmoteOnlyModeButton()
        {
            if (!CheckForModeratorPermissions())
                return;

            var l_Channel = CP_SDK.Chat.Service.Multiplexer.Channels.First();
            if (l_Channel.Item2 is CP_SDK.Chat.Models.Twitch.TwitchChannel)
            {
                var l_TwitchChannel = l_Channel.Item2 as CP_SDK.Chat.Models.Twitch.TwitchChannel;

                if (l_TwitchChannel.Roomstate.EmoteOnly)
                    ShowConfirmationModal("Справді <b>вимкнути</b> режим лише емоти?", (x) => { if (x) l_Channel.Item1.SendTextMessage(l_Channel.Item2, "/emoteonlyoff"); });
                else
                    ShowConfirmationModal("Справді <b>увімкнути</b> режим лише емоти?", (x) => { if (x) l_Channel.Item1.SendTextMessage(l_Channel.Item2, "/emoteonly"); });
            }
        }
        /// <summary>
        /// Toggle follower mode
        /// </summary>
        private void OnToggleFollowerModeButton()
        {
            if (!CheckForModeratorPermissions())
                return;

            var l_Channel = CP_SDK.Chat.Service.Multiplexer.Channels.First();
            if (l_Channel.Item2 is CP_SDK.Chat.Models.Twitch.TwitchChannel)
            {
                var l_TwitchChannel = l_Channel.Item2 as CP_SDK.Chat.Models.Twitch.TwitchChannel;

                if (l_TwitchChannel.Roomstate.FollowersOnly)
                    ShowConfirmationModal("Справді <b>вимкнути</b> режим лише для фоловерів?", (x) => { if (x) l_Channel.Item1.SendTextMessage(l_Channel.Item2, "/followersoff"); });
                else
                    ShowConfirmationModal("Справді <b>увімкнути</b> режим лише для фоловерів?", (x) => { if (x) l_Channel.Item1.SendTextMessage(l_Channel.Item2, "/followers"); });
            }
        }
        /// <summary>
        /// Toggle slow mode
        /// </summary>
        private void OnToggleSlowModeButton()
        {
            if (!CheckForModeratorPermissions())
                return;

            var l_Channel = CP_SDK.Chat.Service.Multiplexer.Channels.First();
            if (l_Channel.Item2 is CP_SDK.Chat.Models.Twitch.TwitchChannel)
            {
                var l_TwitchChannel = l_Channel.Item2 as CP_SDK.Chat.Models.Twitch.TwitchChannel;

                if (l_TwitchChannel.Roomstate.SlowModeInterval != 0)
                    ShowConfirmationModal("Справді <b>вимкнути</b> повільний режим?", (x) => { if (x) l_Channel.Item1.SendTextMessage(l_Channel.Item2, "/slowoff"); });
                else
                    ShowConfirmationModal("Справді <b>увімкнути</b> повільний режим?", (x) => { if (x) l_Channel.Item1.SendTextMessage(l_Channel.Item2, "/slow"); });
            }
        }
        /// <summary>
        /// Manage shortcuts
        /// </summary>
        private void OnManageShortcutButton()
        {
            ModerationViewFlowCoordinator.Instance().SwitchToShortcuts();
        }
    }
}
