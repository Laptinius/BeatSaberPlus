using Newtonsoft.Json;

namespace ChatPlexMod_ChatIntegrations.Models.Actions
{
    public class Chat_SendMessage : Action
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool AddTTSPefix = true;

        public Chat_SendMessage()
        {
            BaseValue = "Тестове повідомлення";
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public class Chat_ToggleVisibility : Action
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public int ToggleType = 0;
    }
}
