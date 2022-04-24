using ClientV3.Output;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ClientV3.Connection
{
    public class Recivier
    {
        private ServerConnection serverConnection;
        private Logger logger;

        public Recivier(ServerConnection serverConnection, Logger logger)
        {
            this.serverConnection = serverConnection;
            this.logger = logger;
        }

        public void listen()
        {
            while (true)
            {
                var buffer = new byte[64];
                var builder = new StringBuilder();
                var bytes = 0;
                try
                {
                    while (serverConnection.stream.DataAvailable)
                    {
                        bytes = serverConnection.stream.Read(buffer, 0, buffer.Length);
                        builder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                    }
                    if (builder.Length != 0)
                    {
                        MessageCame?.Invoke(JObject.Parse(builder.ToString()));
                    }
                }
                catch
                {
                    logger.PostException("Connection problems");
                }
            }
        }
        
        public event MessageCameDelegate MessageCame;

        public delegate void MessageCameDelegate(JObject message);

    }
}
