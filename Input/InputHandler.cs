using ClientV3.Messages;
using ClientV3.Output;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace ClientV3.Input
{
    class InputHandler
    {
        CommandMaster commandMaster;
        Logger logger;

        public InputHandler(Logger logger)
        {
            this.logger = logger;
            commandMaster = new CommandMaster(logger);
        }

        public void start() => HandleUserInput();

        private void HandleUserInput()
        {
            while (true)
            {
                var userInput = Console.ReadLine().Trim();
                if (String.IsNullOrEmpty(userInput))
                {
                    continue;
                }

                if (commandMaster.IsCommand(userInput))
                {
                    var commandMessage = commandMaster.FormCommand(userInput);
                    if (commandMessage != null)
                    {
                        MessageEntered?.Invoke(JObject.FromObject(commandMessage));
                    }
                    continue;
                }

                if (Convert.ToString(userInput.Take(2)) == "//")
                {
                    userInput = userInput.Remove(0, 1);
                }
                var message = new ChatMessageFromClient(userInput);
                MessageEntered?.Invoke(JObject.FromObject(message));
            }
        }

        public delegate void MessageEnteredDelegate(JObject message);

        public MessageEnteredDelegate MessageEntered;
    }
}
