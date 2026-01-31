using CP_SDK.XUI;
using UnityEngine;

namespace ChatPlexMod_ChatIntegrations.Conditions
{
    public class Subscription_IsGift
        : Interfaces.ICondition<Subscription_IsGift, Models.Condition>
    {
        public override string Description      => "Це подія подарункової підписки?";
        public override string UIPlaceHolder    => "<b><i>Переконатися, що це подарункова підписка</i></b>";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
        {
            return p_Context.SubscriptionEvent.IsGift;
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class Subscription_PlanType
        : Interfaces.ICondition<Subscription_PlanType, Models.Conditions.Subscription_PlanType>
    {
        private XUIDropdown m_Dropdown = null;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override string Description => "Умова за типом підписки";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override sealed void BuildUI(Transform p_Parent)
        {
            XUIElements = new IXUIElement[]
            {
                Templates.SettingsHGroup("План підписки",
                    XUIDropdown.Make()
                        .SetOptions(Enums.TwitchSubscribtionPlanType.S).SetValue(Enums.TwitchSubscribtionPlanType.ToStr(Model.SubscribtionPlanType))
                        .OnValueChanged((_, __) => OnSettingChanged())
                        .Bind(ref m_Dropdown)
                )
            };

            BuildUIAuto(p_Parent);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        private void OnSettingChanged()
        {
            Model.SubscribtionPlanType = Enums.TwitchSubscribtionPlanType.ToEnum(m_Dropdown.Element.GetValue());
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
        {
            if (p_Context.SubscriptionEvent != null
                && p_Context.SubscriptionEvent.SubPlan.ToLower() == Enums.TwitchSubscribtionPlanType.ToStr(Model.SubscribtionPlanType).ToLower())
                return true;

            return false;
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class Subscription_PurchasedMonthCount
        : Interfaces.ICondition<Subscription_PurchasedMonthCount, Models.Conditions.Subscription_PurchasedMonthCount>
    {
        private XUIDropdown m_Comparison    = null;
        private XUISlider   m_Count         = null;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override string Description => "Перевірити кількість оплачених місяців";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override sealed void BuildUI(Transform p_Parent)
        {
            XUIElements = new IXUIElement[]
            {
                Templates.SettingsHGroup("Порівняння",
                    XUIDropdown.Make()
                        .SetOptions(Enums.Comparison.S).SetValue(Enums.Comparison.ToStr(Model.Comparison))
                        .OnValueChanged((_, __) => OnSettingChanged())
                        .Bind(ref m_Comparison)
                ),

                Templates.SettingsHGroup("Кількість",
                    XUISlider.Make()
                        .SetMinValue(1.0f).SetMaxValue(100.0f).SetIncrements(1.0f).SetInteger(true)
                        .SetValue(Model.Count).OnValueChanged((_) => OnSettingChanged())
                        .Bind(ref m_Count)
                )
            };

            BuildUIAuto(p_Parent);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        private void OnSettingChanged()
        {
            Model.Comparison    = Enums.Comparison.ToEnum(m_Comparison.Element.GetValue());
            Model.Count         = (uint)m_Count.Element.GetValue();
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override bool Eval(Models.EventContext p_Context)
        {
            return true;// p_Context.SubscriptionEvent != null && Enums.Comparison.Evaluate(Model.Comparison, (uint)p_Context.SubscriptionEvent.PurchasedMonthCount, Model.Count);
        }
    }
}
