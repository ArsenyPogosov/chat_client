using ClientV3.Connection;
using ClientV3.Messages;
using Newtonsoft.Json.Linq;
using System;
using System.Timers;

namespace ClientV3
{
    class ConnectionChecker
    {
        private const int delay = 10 * 1000;
        private Sender sender;
        private bool getAnswer;
        private Timer timer;

        public ConnectionChecker(Sender sender)
        {
            this.sender = sender;
            timer = new Timer(delay);
            timer.Elapsed += delegate { CheckConnection(); };
            getAnswer = true;
        }

        private void CheckConnection()
        {
            if (!getAnswer)
            {
                ConnectionProblem?.Invoke();
                Stop();
            }
            else
            {
                var message = new CommandMessage();
                message.CommandType = CommandType.Ping;
                message.Args = new JObject();
                message.Args["ID"] = 0;
                sender.SendJObject(JObject.FromObject(message));
                getAnswer = false;
            }
        }

        public event ConnectionProble ConnectionProblem;

        public delegate void ConnectionProble();

        public void Start() => timer?.Start();

        public void Stop() => timer?.Stop();

        public void GetAnswer()
        {
            getAnswer = true;
        }
    }
}
