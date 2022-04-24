using System;
using ClientV3.Messages;
using ClientV3.Output;
using Newtonsoft.Json.Linq;
using ClientV3.Connection;

namespace ClientV3
{
    class MessageReactionService
    {
        private MessageOutputService messageOutputService;
        private Sender sender;

        public MessageReactionService(Sender sender)
        {
            messageOutputService = new MessageOutputService();
            this.sender = sender;
        }

        public void React(JObject message)
        {
            if (ReturnMessageType(message) == MessageType.ChatMessageFromServer)
            {
                ReactOnChatMessage(message.ToObject<ChatMessageFromServer>());
            }

            if (ReturnMessageType(message) == MessageType.CommandMessage)
            {
                ReactOnCommandMessage(message.ToObject<CommandMessage>());
            }
        }

        private MessageType ReturnMessageType(JObject message)
        {
            return message.ToObject<BaseMessage>().MessageType;
        }

        private void ReactOnChatMessage(ChatMessageFromServer message) =>
            messageOutputService.OutputMessage(message.Message, message.Name);

        private void ReactOnCommandMessage(CommandMessage commandMessage)
        {
            switch (commandMessage.CommandType)
            {
                case CommandType.Start:
                    break;
                case CommandType.SetUsername:
                    break;
                case CommandType.Here:
                    Console.WriteLine("There is: ");
                    foreach (var username in Convert.ToString(commandMessage.Args["Users"]).Split(','))
                    {
                        Console.WriteLine(username);
                    }
                    break;
                case CommandType.Ping:
                    commandMessage.CommandType = CommandType.Pong;
                    sender.SendJObject(JObject.FromObject(commandMessage));
                    break;
                case CommandType.Pong:
                    break;
                case CommandType.SendFile:
                    break;
                default:
                    break;
            }
        }
    }
}
