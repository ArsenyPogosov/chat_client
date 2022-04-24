using Newtonsoft.Json.Linq;

namespace ClientV3.Messages
{
    public class CommandMessage : BaseMessage
    {
        public CommandType CommandType { get; set; }
        public JObject Args { get; set; }

        public CommandMessage() : this(default(CommandType), null) { }

        public CommandMessage(CommandType commandType, JObject args)
            : base(MessageType.CommandMessage)
        {
            CommandType = commandType;
            Args = args;
        }
    }
}
