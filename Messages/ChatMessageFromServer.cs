namespace ClientV3.Messages
{
    class ChatMessageFromServer : BaseMessage
    {
        public string Message { get; set; }
        public string Name { get; set; }

        public ChatMessageFromServer() : this(null, null) { }
        public ChatMessageFromServer(string message, string name)
            : base(MessageType.ChatMessageFromServer)
        {
            Message = message;
            Name = name;
        }
    }
}
