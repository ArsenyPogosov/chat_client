namespace ClientV3.Messages
{
    public class BaseMessage
    {
        public MessageType MessageType { get; set; }

        public BaseMessage() : this(MessageType.CommandMessage) { }
        public BaseMessage(MessageType type)
        {
            MessageType = type;
        }
    }
}
