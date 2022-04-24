using ClientV3.Connection;
using ClientV3.Input;
using ClientV3.Output;
using ClientV3.Authorization;
using System.Timers;
using System.Threading;

namespace ClientV3
{
    class ChatLauncher
    {
        public ChatLauncher() { }

        public void Start()
        {
            var logger = new Logger();

            ServerConnection serverConnection;
            try
            {
                serverConnection = new ServerConnection();
            }
            catch
            {
                logger.PostException("Can't connect to the server!");
                return;
            }
            var sender = new Sender(serverConnection, logger);
            var recivier = new Recivier(serverConnection, logger);
            new Thread(() => recivier.listen()).Start();

            var authorizator = new Authorizator(logger, sender);
            recivier.MessageCame += authorizator.MessageCame;
            authorizator.Authorize();
            if (authorizator.IsAuthorized())
            {
                logger.Post("Success authorization!");
            }
            recivier.MessageCame -= authorizator.MessageCame;
            var connectionChecker = new ConnectionChecker(sender);
            connectionChecker.Start();
            var chatCore = new ChatCore(sender, logger, connectionChecker);
            recivier.MessageCame += chatCore.MessageCame;
            connectionChecker.ConnectionProblem += chatCore.ConnectionProblem;
            var inputHandler = new InputHandler(logger);
            inputHandler.MessageEntered += chatCore.MessageEntered;
            inputHandler.start();
        }
    }
}