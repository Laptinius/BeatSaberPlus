using BeatSaberPlus_ChatRequest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using static UnityEngine.EventSystems.EventTrigger;

namespace BeatSaberPlus_ChatRequest
{
    internal class CRConfig : CP_SDK.Config.JsonConfig<CRConfig>
    {
        internal class _Filters
        {
            [JsonProperty] internal bool NoBeatSage = false;

            [JsonProperty] internal bool NoRanked = false;

            [JsonProperty] internal bool NPSMin = false;
            [JsonProperty] internal int NPSMinV = 0;

            [JsonProperty] internal bool NPSMax = false;
            [JsonProperty] internal int NPSMaxV = 30;

            [JsonProperty] internal bool NJSMin = false;
            [JsonProperty] internal int NJSMinV = 0;

            [JsonProperty] internal bool NJSMax = false;
            [JsonProperty] internal int NJSMaxV = 30;

            [JsonProperty] internal bool DurationMin = false;
            [JsonProperty] internal int DurationMinV = 1;

            [JsonProperty] internal bool DurationMax = false;
            [JsonProperty] internal int DurationMaxV = 3;

            [JsonProperty] internal bool VoteMin = false;
            [JsonProperty] internal float VoteMinV = 0.5f;

            [JsonProperty] internal bool IgnoreMinVoteBelow = true;
            [JsonProperty] internal int IgnoreMinVoteBelowV = 4;

            [JsonProperty] internal bool DateMin = false;
            [JsonProperty] internal int DateMinV = 0;

            [JsonProperty] internal bool DateMax = false;
            [JsonProperty] internal int DateMaxV = 36;
        }

        internal class _OverlayIntegration
        {
            [JsonProperty] internal string SimpleQueueFileFormat = "%i - %n від %m";
            [JsonProperty] internal int SimpleQueueFileCount = 10;
            [JsonProperty] internal string SimpleQueueStatusOpen = "Черга відкрита!";
            [JsonProperty] internal string SimpleQueueStatusClosed = "Черга закрита!";
        }

        internal class _Commands
        {
            [Flags]
            internal enum EPermission
            {
                Viewers         = 1 << 1,
                Subscribers     = 1 << 2,
                VIPs            = 1 << 3,
                Moderators      = 1 << 4
            }

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission BSRCommandPermissions       = EPermission.Viewers;
            [JsonProperty] internal bool        BSRCommandEnabled           = true;
            [JsonProperty] internal bool        BSRCommandCooldownPerUser   = true;
            [JsonProperty] internal int         BSRCommandCooldown          = 10;
            [JsonProperty] internal string      BSRCommand                  = "bsr";
            [JsonProperty] internal string      BSRCommand_UserBanned       = "@$UserName тобі заборонено робити запити!";
            [JsonProperty] internal string      BSRCommand_QueueClosed      = "@$UserName черга закрита!";
            [JsonProperty] internal string      BSRCommand_SearchDisabled   = "@$UserName пошук вимкнено";
            [JsonProperty] internal string      BSRCommand_Search0Result    = "@$UserName за запитом $Search знайдено 0 результатів!";
            [JsonProperty] internal string      BSRCommand_SearchResults    = "@$UserName за запитом $Search знайдено $Count результатів: $Results";
            [JsonProperty] internal string      BSRCommand_Blocklisted      = "@$UserName (bsr $BSRKey) $SongName / $LevelAuthorName у чорному списку!";
            [JsonProperty] internal string      BSRCommand_AlreadyQueued    = "@$UserName (bsr $BSRKey) $SongName / $LevelAuthorName уже в черзі!";
            [JsonProperty] internal string      BSRCommand_RequestLimit     = "@$UserName у тебе вже $UserRequestCount у черзі. $UserType можуть мати лише $UserTypeLimit запит(ів).";
            [JsonProperty] internal string      BSRCommand_AlreadyPlayed    = "@$UserName цю пісню вже запитували в цій сесії!";
            [JsonProperty] internal string      BSRCommand_NotFound         = "@$UserName мапу $BSRKey не знайдено.";
            [JsonProperty] internal string      BSRCommand_MapperBanned     = "@$UserName мапи від $UploaderName заборонені!";
            [JsonProperty] internal string      BSRCommand_RequestOK        = "(bsr $BSRKey) $SongName / $LevelAuthorName $Vote% запитано @$UserName, додано до черги.";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission BSRHelpCommandPermissions   = EPermission.Viewers;
            [JsonProperty] internal bool        BSRHelpCommandEnabled       = true;
            [JsonProperty] internal string      BSRHelpCommand              = "bsrhelp";
            [JsonProperty] internal string      BSRHelpCommand_Reply        = "@$UserName Щоб запросити пісню, зайди на https://beatsaver.com/search, знайди трек, натисни іконку Twitch поруч і встав це в чат стріму — я зіграю її незабаром!.";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission LinkCommandPermissions  = EPermission.Viewers;
            [JsonProperty] internal bool        LinkCommandEnabled      = true;
            [JsonProperty] internal string      LinkCommand             = "link";
            [JsonProperty] internal string      LinkCommand_NoSong      = "@$UserName зараз нічого не грає!";
            [JsonProperty] internal string      LinkCommand_LastSong    = "@$UserName попередня пісня: $SongInfo $SongLink";
            [JsonProperty] internal string      LinkCommand_CurrentSong = "@$UserName поточна пісня: $SongInfo $SongLink";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission QueueCommandPermissions     = EPermission.Viewers;
            [JsonProperty] internal bool        QueueCommandEnabled         = true;
            [JsonProperty] internal int         QueueCommandShowSize        = 4;
            [JsonProperty] internal bool        QueueCommandCooldownPerUser = false;
            [JsonProperty] internal int         QueueCommandCooldown        = 10;
            [JsonProperty] internal string      QueueCommand                = "queue";
            [JsonProperty] internal string      QueueCommand_Empty          = "Черга пісень порожня!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission QueueStatusCommandPermissions   = EPermission.Viewers;
            [JsonProperty] internal bool        QueueStatusCommandEnabled       = true;
            [JsonProperty] internal string      QueueStatusCommand              = "queuestatus";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission WrongCommandCommandPermissions  = EPermission.Viewers;
            [JsonProperty] internal bool        WrongCommandEnabled             = true;
            [JsonProperty] internal string      WrongCommand                    = "wrong,oops,wrongsong";
            [JsonProperty] internal string      WrongCommand_NoSong             = "@$UserName у тебе немає пісень у черзі!";
            [JsonProperty] internal string      WrongCommand_NoSongFound        = "@$UserName у черзі немає пісні з вказаним кодом!!";
            [JsonProperty] internal string      WrongCommand_Removed            = "@$UserName (bsr $BSRKey) $SongName / $LevelAuthorName видалено з черги!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission ModAddPermissions     = EPermission.Moderators;
            [JsonProperty] internal bool        ModAddCommandEnabled  = true;
            [JsonProperty] internal string      ModAddCommand         = "modadd";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission OpenCommandPermissions    = EPermission.Moderators;
            [JsonProperty] internal bool        OpenCommandEnabled        = true;
            [JsonProperty] internal string      OpenCommand               = "open";
            [JsonProperty] internal string      OpenCommand_AlreadyOpen   = "@$UserName черга вже відкрита!";
            [JsonProperty] internal string      OpenCommand_OK            = "Черга відкрита!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission CloseCommandPermissions     = EPermission.Moderators;
            [JsonProperty] internal bool        CloseCommandEnabled         = true;
            [JsonProperty] internal string      CloseCommand                = "close";
            [JsonProperty] internal string      CloseCommand_AlreadyClosed  = "@$UserName черга вже закрита!";
            [JsonProperty] internal string      CloseCommand_OK             = "Чергу закрито!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission SabotageCloseCommandPermissions = EPermission.Moderators;
            [JsonProperty] internal bool        SabotageCommandEnabled          = true;
            [JsonProperty] internal string      SabotageCommand                 = "sabotage";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission SongMessageCommandPermissions   = EPermission.Moderators;
            [JsonProperty] internal bool        SongMessageCommandEnabled       = true;
            [JsonProperty] internal string      SongMessageCommand              = "songmsg";
            [JsonProperty] internal string      SongMessage_OK                  = "@$UserName повідомлення встановлено!";
            [JsonProperty] internal string      SongMessage_NotFound            = $"@$UserName Не знайдено пісні в черзі за ключем або ім'ям користувача \"$Subject\"!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission MoveToTopCommandPermissions = EPermission.Moderators;
            [JsonProperty] internal bool        MoveToTopCommandEnabled     = true;
            [JsonProperty] internal string      MoveToTopCommand            = "mtt";
            [JsonProperty] internal string      MoveToTopCommand_OK         = $"@$UserName (bsr $BSRKey) $SongName / $LevelAuthorName, запитано @$RequesterName, тепер на початку черги!";
            [JsonProperty] internal string      MoveToTopCommand_NotFound   = $"@$UserName Не знайдено пісні в черзі за ключем або ім'ям користувача \"$Subject\"!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission AddToTopCommandPermissions  = EPermission.Moderators;
            [JsonProperty] internal bool        AddToTopCommandEnabled      = true;
            [JsonProperty] internal string      AddToTopCommand             = "att";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission RemoveCommandPermissions    = EPermission.Moderators;
            [JsonProperty] internal bool        RemoveCommandEnabled        = true;
            [JsonProperty] internal string      RemoveCommand               = "remove";
            [JsonProperty] internal string      RemoveCommand_OK            = $"@$UserName (bsr $BSRKey) $SongName / $LevelAuthorName, запит від @RequesterName, видалено з черги!";
            [JsonProperty] internal string      RemoveCommand_NotFound      = $"@$UserName Не знайдено пісні в черзі за ключем або ім'ям користувача \"$Subject\"!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission BsrBanCommandPermissions    = EPermission.Moderators;
            [JsonProperty] internal bool        BsrBanCommandEnabled        = true;
            [JsonProperty] internal string      BsrBanCommand               = "bsrban";
            [JsonProperty] internal string      BsrBanCommand_OK            = $"@$UserName Користувача \"$l_TargetUserName\" додано до бан-листа запитів!";
            [JsonProperty] internal string      BsrBanCommand_AlreadyIn     = $"@$UserName Користувач \"$l_TargetUserName\" вже в бан-листі запитів!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission BsrUnbanCommandPermissions  = EPermission.Moderators;
            [JsonProperty] internal bool        BsrUnbanCommandEnabled      = true;
            [JsonProperty] internal string      BsrUnbanCommand             = "bsrunban";
            [JsonProperty] internal string      BsrUnbanCommand_OK          = $"@$UserName Користувача \"$TargetUserName\" видалено з бан-листа запитів!";
            [JsonProperty] internal string      BsrUnbanCommand_NotFound    = $"@$UserName Користувача \"$TargetUserName\" немає в бан-листі запитів!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission BsrBanMapperCommandPermissions  = EPermission.Moderators;
            [JsonProperty] internal bool        BsrBanMapperCommandEnabled      = true;
            [JsonProperty] internal string      BsrBanMapperCommand             = "bsrbanmapper";
            [JsonProperty] internal string      BsrBanMapperCommand_OK          = $"@$UserName \"$MapperName\" додано до бан-листа маперів!";
            [JsonProperty] internal string      BsrBanMapperCommand_AlreadyIn   = $"@$UserName \"$MapperName\" вже в бан-листі маперів!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission BsrUnbanMapperCommandPermissions    = EPermission.Moderators;
            [JsonProperty] internal bool        BsrUnbanMapperCommandEnabled        = true;
            [JsonProperty] internal string      BsrUnbanMapperCommand               = "bsrunbanmapper";
            [JsonProperty] internal string      BsrUnbanMapperCommand_OK            = $"@$UserName \"$MapperName\" видалено з бан-листа маперів!";
            [JsonProperty] internal string      BsrUnbanMapperCommand_NotIn         = $"@$UserName \"$MapperName\" немає в бан-листі маперів!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission RemapCommandPermissions = EPermission.Moderators;
            [JsonProperty] internal bool        RemapCommandEnabled    = true;
            [JsonProperty] internal string      RemapCommand           = "remap";
            [JsonProperty] internal string      RemapCommand_OK        = $"@$UserName Усі запити $Source буде перенаправлено на $Target!";
            [JsonProperty] internal string      RemapCommand_OKRemoved = $"@$UserName Перенаправлення для пісні $Source видалено!";
            [JsonProperty] internal string      RemapCommand_NotFound  = $"@$UserName Перенаправлення для $Source не знайдено!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission AllowlistCommandPermissions         = EPermission.Moderators;
            [JsonProperty] internal bool        AllowlistCommandEnabled             = true;
            [JsonProperty] internal string      AllowlistCommand                    = "allow";
            [JsonProperty] internal string      AllowlistCommand_OK                 = $"@$UserName Усі запити $BSRKey буде дозволено!";
            [JsonProperty] internal string      AllowlistCommand_AlreadyAllowlisted = "@$UserName (bsr $BSRKey) $SongName / $LevelAuthorName уже в білому списку!";

            [JsonProperty, JsonConverter(typeof(StringEnumConverter))]
                           internal EPermission BlocklistCommandPermissions         = EPermission.Moderators;
            [JsonProperty] internal bool        BlocklistCommandEnabled             = true;
            [JsonProperty] internal string      BlocklistCommand                    = "block";
            [JsonProperty] internal string      BlocklistCommand_AlreadyBlocklisted = "@$UserName (bsr $BSRKey) $SongName / $LevelAuthorName уже в чорному списку!";
        }

        internal class _Messages
        {
            [JsonProperty] internal string NoPermissions = "@$UserName у тебе немає прав!";
            [JsonProperty] internal string CommandFailed = "@$UserName команда не виконана!";
            [JsonProperty] internal string OnCooldown    = "@$UserName команда на перезарядці!";
            [JsonProperty] internal string BadSyntax     = "@$UserName неправильна команда, синтаксис: $Syntax";
            [JsonProperty] internal string NextSong      = $"$SongName / $LevelAuthorName $Vote% (bsr $BSRKey) запитано @$RequesterName — наступна!";
        }

        [JsonProperty] internal bool Enabled = true;

        [JsonProperty] internal bool SafeMode2 = true;

        [JsonProperty] internal bool QueueOpen = true;

        [JsonProperty] internal int UserMaxRequest = 2;
        [JsonProperty] internal int VIPBonusRequest = 2;
        [JsonProperty] internal int SubscriberBonusRequest = 3;

        [JsonProperty] internal int HistorySize = 50;

        [JsonProperty] internal bool PlayPreviewMusic   = true;
        [JsonProperty] internal bool BigCoverArt        = true;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        internal _Filters Filters = new _Filters();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        internal _OverlayIntegration OverlayIntegration = new _OverlayIntegration();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        internal _Commands Commands = new _Commands();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        internal _Messages Messages = new _Messages();

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Get relative config path
        /// </summary>
        /// <returns></returns>
        public override string GetRelativePath()
            => $"{CP_SDK.ChatPlexSDK.ProductName}Plus/ChatRequest/Config";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// On config init
        /// </summary>
        /// <param name="p_OnCreation">On creation</param>
        protected override void OnInit(bool p_OnCreation)
        {
            if (Commands != null && !Commands.LinkCommand_LastSong.Contains("$SongLink"))
                Commands.LinkCommand_LastSong += " $SongLink";

            if (Commands != null && !Commands.LinkCommand_CurrentSong.Contains("$SongLink"))
                Commands.LinkCommand_CurrentSong += " $SongLink";
        }
    }
}
