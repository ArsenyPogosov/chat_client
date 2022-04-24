using ClientV3.Output;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace ClientV3.Connection
{
    public class Sender
    {
        private ServerConnection serverConnection;
        private Logger logger;

        public Sender(ServerConnection serverConnection, Logger logger)
        {
            this.serverConnection = serverConnection;
            this.logger = logger;
        }

        public void SendJObject(JObject toSend)
        {
            var data = Encoding.UTF8.GetBytes(toSend.ToString());
            try
            {
                serverConnection.stream.Write(data, 0, data.Length);
            }
            catch
            {
                logger.PostException("Can't send to the server!");
            }
        }
    }
}
