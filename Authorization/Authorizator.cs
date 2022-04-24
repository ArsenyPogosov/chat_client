using ClientV3.Connection;
using ClientV3.Messages;
using ClientV3.Output;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;

namespace ClientV3.Authorization
{
    public class Authorizator
    {
        private const int maxAuthTime = 60 * 1000;
        private Logger logger;
        private Sender sender;
        private AccessTokenStorage accessTokenStorage;
        private bool Authorized;

        public Authorizator(Logger logger, Sender sender)
        {
            this.logger = logger;
            this.sender = sender;
            accessTokenStorage = new AccessTokenStorage(logger);
            Authorized = false;
        }

        public void Authorize()
        {
            var message = new CommandMessage();
            message.CommandType = CommandType.Start;
            message.Args = new JObject();
            message.Args["AccessToken"] = accessTokenStorage.ReturnAccessToken();
            sender.SendJObject(JObject.FromObject(message));
            var sleepTime = 0;
            while ((!Authorized)&&(sleepTime < maxAuthTime))
            {
                Thread.Sleep(1);
                ++sleepTime;
            }
            if (!Authorized)
            {
                logger.PostException("Can't authorize!");
            }
        }

        public void MessageCame(JObject message)
        {
            if (message.ToObject<BaseMessage>().MessageType == MessageType.CommandMessage)
            {
                var commandMessage = message.ToObject<CommandMessage>();
                if (commandMessage.CommandType == CommandType.Start)
                {
                    Authorized = true;
                    accessTokenStorage.SetAccesToken(Convert.ToString(commandMessage.Args["AccessToken"]));
                }
            }
        }

        public bool IsAuthorized()
        {
            return Authorized;
        }
    }
}
