using CP_SDK;
using CP_SDK.XUI;
using System;
using System.Linq;

namespace ChatPlexMod_ChatIntegrations.UI.Modals
{
    /// <summary>
    /// Event template modal
    /// </summary>
    internal sealed class EventTemplateModal : CP_SDK.UI.IModal
    {
        private XUIDropdown m_Template = null;

        private Action<Interfaces.IEventBase>   m_Callback = null;
        private string                          m_Selected = null;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// On modal show
        /// </summary>
        public override void OnShow()
        {
            if (m_Template != null)
                return;

            Templates.ModalRectLayout(
                XUIText.Make("Який шаблон ви хочете використати?"),

                XUIDropdown.Make()
                    .SetOptions(ChatIntegrations.RegisteredTemplates.Keys.ToList())
                    .OnValueChanged((_, p_Selected) => m_Selected = p_Selected)
                    .Bind(ref m_Template),

                XUIHLayout.Make(
                    XUISecondaryButton.Make("Скасувати", OnCancelButton).SetWidth(30f),
                    XUIPrimaryButton.Make("Створити", OnCreateButton).SetWidth(30f)
                )
                .SetPadding(0)
            )
            .SetWidth(90.0f)
            .BuildUI(transform);
        }
        /// <summary>
        /// On modal close
        /// </summary>
        public override void OnClose()
        {

        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="p_Callback">Callback</param>
        public void Init(Action<Interfaces.IEventBase> p_Callback)
        {
            m_Callback = p_Callback;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// On cancel button
        /// </summary>
        private void OnCancelButton()
        {
            VController.CloseModal(this);
        }
        /// <summary>
        /// On create button
        /// </summary>
        private void OnCreateButton()
        {
            if (!ChatIntegrations.RegisteredTemplates.TryGetValue(m_Selected, out var l_CreateFunc))
            {
                VController.ShowMessageModal("Шаблон не знайдено!");
                return;
            }

            try
            {
                var l_NewEvent = l_CreateFunc();
                if (l_NewEvent != null)
                {
                    ChatIntegrations.Instance.AddEvent(l_NewEvent);

                    VController.CloseModal(this);

                    try { m_Callback?.Invoke(l_NewEvent); }
                    catch (System.Exception l_Exception)
                    {
                        ChatPlexSDK.Logger.Error($"[ChatPlexMod_ChatIntegrations.UI][EventTemplateModal.OnCreateButton] Error:");
                        ChatPlexSDK.Logger.Error(l_Exception);
                    }
                }
                else
                {
                    VController.CloseModal(this);
                    VController.ShowMessageModal("Шаблон не вибрано!");
                }
            }
            catch (System.Exception l_Exception)
            {
                VController.CloseModal(this);
                VController.ShowMessageModal("Невідома помилка!");

                ChatPlexSDK.Logger.Error($"[ChatPlexMod_ChatIntegrations.UI][EventCreateModal.OnCreateButton] Error:");
                ChatPlexSDK.Logger.Error(l_Exception);
            }
        }
    }
}
