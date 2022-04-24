namespace ClientV3.Messages
{
    public class ChatMessageFromClient : BaseMessage
    {
        public string Message { get; set; }

        public ChatMessageFromClient() : this(null) { }

        public ChatMessageFromClient(string message) 
            : base(MessageType.ChatMessageFromClient)
        {
            Message = message;
        }
    }
}
