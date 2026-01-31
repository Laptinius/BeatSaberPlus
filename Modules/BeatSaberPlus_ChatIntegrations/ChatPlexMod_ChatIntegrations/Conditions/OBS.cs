using CP_SDK.XUI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using OBSService = CP_SDK.OBS.Service;

namespace ChatPlexMod_ChatIntegrations.Conditions
{
    internal class OBSRegistration
    {
        internal static void Register()
        {
            ChatIntegrations.RegisterConditionType("OBS_IsConnected",         () => new OBS_IsConnected());
            ChatIntegrations.RegisterConditionType("OBS_IsNotConnected",      () => new OBS_IsNotConnected());
            ChatIntegrations.RegisterConditionType("OBS_IsStreaming",         () => new OBS_IsStreaming());
            ChatIntegrations.RegisterConditionType("OBS_IsNotStreaming",      () => new OBS_IsNotStreaming());
            ChatIntegrations.RegisterConditionType("OBS_IsRecording",         () => new OBS_IsRecording());
            ChatIntegrations.RegisterConditionType("OBS_IsNotRecording",      () => new OBS_IsNotRecording());
            ChatIntegrations.RegisterConditionType("OBS_IsInStudioMode",      () => new OBS_IsInStudioMode());
            ChatIntegrations.RegisterConditionType("OBS_IsNotInStudioMode",   () => new OBS_IsNotInStudioMode());
            ChatIntegrations.RegisterConditionType("OBS_IsInScene",           () => new OBS_IsInScene());
            ChatIntegrations.RegisterConditionType("OBS_IsNotInScene",        () => new OBS_IsNotInScene());
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsConnected
        : Interfaces.ICondition<OBS_IsConnected, Models.Condition>
    {
        public override string Description      => "OBS підключено?";
        public override string UIPlaceHolder    => "<b><i>Ensure that OBS is connected</i></b>";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
            => OBSService.Status == OBSService.EStatus.Connected;
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsNotConnected
        : Interfaces.ICondition<OBS_IsNotConnected, Models.Condition>
    {
        public override string Description      => "OBS не підключено?";
        public override string UIPlaceHolder    => "<b><i>Ensure that OBS is not connected</i></b>";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
            => OBSService.Status != OBSService.EStatus.Connected;
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsStreaming
        : Interfaces.ICondition<OBS_IsStreaming, Models.Condition>
    {
        public override string Description      => "OBS стрімить?";
        public override string UIPlaceHolder    => "<b><i>Ensure that OBS is streaming</i></b>";


        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
            => OBSService.Status == OBSService.EStatus.Connected && OBSService.IsStreaming;
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsNotStreaming
        : Interfaces.ICondition<OBS_IsNotStreaming, Models.Condition>
    {
        public override string Description      => "OBS не стрімить?";
        public override string UIPlaceHolder    => "<b><i>Ensure that OBS is not streaming</i></b>";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
            => OBSService.Status == OBSService.EStatus.Connected && !OBSService.IsStreaming;
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsRecording
        : Interfaces.ICondition<OBS_IsRecording, Models.Condition>
    {
        public override string Description      => "OBS записує?";
        public override string UIPlaceHolder    => "<b><i>Ensure that OBS is recording</i></b>";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
            => OBSService.Status == OBSService.EStatus.Connected && OBSService.IsRecording;
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsNotRecording
        : Interfaces.ICondition<OBS_IsNotRecording, Models.Condition>
    {
        public override string Description      => "OBS не записує?";
        public override string UIPlaceHolder    => "<b><i>Ensure that OBS is not recording</i></b>";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
            => OBSService.Status == OBSService.EStatus.Connected && !OBSService.IsRecording;
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsInStudioMode
        : Interfaces.ICondition<OBS_IsInStudioMode, Models.Condition>
    {
        public override string Description      => "OBS у студійному режимі?";
        public override string UIPlaceHolder    => "<b><i>Ensure that OBS is in studio mode</i></b>";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
            => OBSService.Status == OBSService.EStatus.Connected && OBSService.IsInStudioMode;
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsNotInStudioMode
        : Interfaces.ICondition<OBS_IsNotInStudioMode, Models.Condition>
    {
        public override string Description      => "OBS не у студійному режимі?";
        public override string UIPlaceHolder    => "<b><i>Ensure that OBS is not in studio mode</i></b>";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
            => OBSService.Status == OBSService.EStatus.Connected && !OBSService.IsInStudioMode;
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsInScene
        : Interfaces.ICondition<OBS_IsInScene, Models.Conditions.OBS_IsInScene>
    {
        private XUIDropdown m_Dropdown = null;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override string Description => "OBS на сцені";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override sealed void BuildUI(Transform p_Parent)
        {
            var l_Choices   = new List<string>() { "<i>Немає</i>" };
            var l_Selected  = "<i>Немає</i>";

            if (OBSService.Status == OBSService.EStatus.Connected)
                l_Choices.AddRange(OBSService.Scenes.Values.Select(x => x.sceneName));
            else
                CP_SDK.Chat.Service.Multiplexer?.InternalBroadcastSystemMessage("ChatIntegrations: умову не виконано, немає з'єднання з OBS!");

            for (int l_I = 0; l_I < l_Choices.Count; ++l_I)
            {
                if (l_Choices[l_I] != Model.SceneName)
                    continue;

                l_Selected = l_Choices[l_I];
                break;
            }

            XUIElements = new IXUIElement[]
            {
                Templates.SettingsHGroup("Сцена",
                    XUIDropdown.Make()
                        .SetOptions(l_Choices).SetValue(l_Selected).OnValueChanged((_, __) => OnSettingChanged())
                        .Bind(ref m_Dropdown)
                ),

                XUIPrimaryButton.Make("Взяти активну сцену", OnSelectActiveSceneButton)
            };

            BuildUIAuto(p_Parent);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        private void OnSettingChanged()
        {
            if (OBSService.Status != OBSService.EStatus.Connected)
                return;

            Model.SceneName = m_Dropdown.Element.GetValue();
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        private void OnSelectActiveSceneButton()
        {
            Model.SceneName = OBSService.ActiveProgramScene?.sceneName;
            m_Dropdown.SetValue(OBSService.ActiveProgramScene?.sceneName);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
        {
            if (OBSService.Status != OBSService.EStatus.Connected)
            {
                CP_SDK.Chat.Service.Multiplexer?.InternalBroadcastSystemMessage("ChatIntegrations: умову не виконано, немає з'єднання з OBS!");
                return false;
            }

            return OBSService.ActiveProgramScene?.sceneName == Model.SceneName;
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class OBS_IsNotInScene
        : Interfaces.ICondition<OBS_IsNotInScene, Models.Conditions.OBS_IsNotInScene>
    {
        private XUIDropdown m_Dropdown = null;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override string Description => "OBS не на сцені";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override sealed void BuildUI(Transform p_Parent)
        {
            var l_Choices   = new List<string>() { "<i>Немає</i>" };
            var l_Selected  = "<i>Немає</i>";

            if (OBSService.Status == OBSService.EStatus.Connected)
                l_Choices.AddRange(OBSService.Scenes.Values.Select(x => x.sceneName));
            else
                CP_SDK.Chat.Service.Multiplexer?.InternalBroadcastSystemMessage("ChatIntegrations: умову не виконано, немає з'єднання з OBS!");

            for (int l_I = 0; l_I < l_Choices.Count; ++l_I)
            {
                if (l_Choices[l_I] != Model.SceneName)
                    continue;

                l_Selected = l_Choices[l_I];
                break;
            }

            XUIElements = new IXUIElement[]
            {
                Templates.SettingsHGroup("Сцена",
                    XUIDropdown.Make()
                        .SetOptions(l_Choices).SetValue(l_Selected).OnValueChanged((_, __) => OnSettingChanged())
                        .Bind(ref m_Dropdown)
                ),

                XUIPrimaryButton.Make("Взяти активну сцену", OnSelectActiveSceneButton)
            };

            BuildUIAuto(p_Parent);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        private void OnSettingChanged()
        {
            if (OBSService.Status != OBSService.EStatus.Connected)
                return;

            Model.SceneName = m_Dropdown.Element.GetValue();
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        private void OnSelectActiveSceneButton()
        {
            Model.SceneName = OBSService.ActiveProgramScene?.sceneName;
            m_Dropdown.SetValue(OBSService.ActiveProgramScene?.sceneName);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
        {
            if (OBSService.Status != OBSService.EStatus.Connected)
            {
                CP_SDK.Chat.Service.Multiplexer?.InternalBroadcastSystemMessage("ChatIntegrations: умову не виконано, немає з'єднання з OBS!");
                return false;
            }

            return !(OBSService.ActiveProgramScene?.sceneName == Model.SceneName);
        }
    }
}
