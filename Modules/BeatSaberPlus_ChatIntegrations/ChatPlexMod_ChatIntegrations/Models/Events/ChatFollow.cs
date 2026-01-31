namespace ChatPlexMod_ChatIntegrations.Models.Events
{
    /// <summary>
    /// Chat follow event model
    /// </summary>
    public class ChatFollow : Event
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ChatFollow()
        {
            Name = "Нова подія фолоу в чаті";
        }
    }
}
