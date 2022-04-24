using ClientV3.Messages;
using ClientV3.Output;
using Newtonsoft.Json.Linq;
using System;

namespace ClientV3.Input
{
    class CommandMaster
    {
        Logger logger;
        public CommandMaster(Logger logger)
        {
            this.logger = logger;
        }

        public bool IsCommand(string text)
        {
            return (text[0] == '/') && (text[1] != '/');
        }

        public CommandMessage FormCommand(string text)
        {
            var data = text.Split(' ');
            data[0] = data[0].Remove(0, 1).ToLower();
            if (data[0].Length == 0)
            {
                logger.PostException("Write any command! If you want to send message with / in the begining use //");
                return null;
            }

            var resultMessage = new CommandMessage();
            try
            {
                resultMessage.CommandType = ChooseCT(data[0]);
            }
            catch
            {
                return null;
            }

            resultMessage.Args = new JObject();
            for (var i = 1; i <= data.Length - 1; i += 2)
            {
                if (data[i][0] != '-')
                {
                    logger.PostException($"Unexcpected {data[i]}! If you want to send message with / in the begining use //");
                    return null;
                }
                data[i] = data[i].Remove(0, 1);

                if (i + 1 > data.Length - 1)
                {
                    logger.PostException($"Unexcpected {data[i]}! If you want to send message with / in the begining use //");
                    return null;
                }
                resultMessage.Args[data[i]] = data[i + 1];
            }

            return resultMessage;
        }

        private CommandType ChooseCT(string command)
        {
            switch (command)
            {
                case "start":
                    return CommandType.Start;
                case "setusername":
                    return CommandType.SetUsername;
                case "here":
                    return CommandType.Here;
                case "ping":
                    return CommandType.Ping;
                case "pong":
                    return CommandType.Pong;
                case "sendfile":
                    return CommandType.SendFile;
                default:
                    logger.PostException($"{command} is not a command! If you want to send message with / in the begining use //");
                    throw new ArgumentException("Not a command");
            }
        }
    }
}
