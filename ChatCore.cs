using ClientV3.Connection;
using ClientV3.Messages;
using ClientV3.Output;
using Newtonsoft.Json.Linq;

namespace ClientV3
{
    class ChatCore
    {
        private Sender sender;
        private Logger logger;
        private ConnectionChecker connectionChecker;
        private MessageReactionService messageReactionService;

        public ChatCore(Sender sender, Logger logger, ConnectionChecker connectionChecker)
        {
            this.sender = sender;
            this.logger = logger;
            this.connectionChecker = connectionChecker;
            messageReactionService = new MessageReactionService(sender);
        }

        public void MessageCame(JObject message)
        {
            if (message.ToObject<CommandMessage>()?.CommandType == CommandType.Pong)
            {
                connectionChecker.GetAnswer();
            }
            messageReactionService.React(message);
        }

        public void MessageEntered(JObject message)
        {
            sender.SendJObject(message);
        }

        public void ConnectionProblem()
        {
            logger.PostException("Server don't answer");
        }
    }
}
