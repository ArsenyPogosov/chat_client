namespace ClientV3.Messages
{
    public class ServerNotification : BaseMessage
    {
        public string Message { get; set; }


        public ServerNotification() : this(null) { }
        public ServerNotification(string message)
            : base(MessageType.ServerNotification)
        {
            Message = message;
        }
    }
}