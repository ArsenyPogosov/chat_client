using System.Net.Sockets;

namespace ClientV3.Connection
{
    public class ServerConnection
    {
        private string host;
        private int port;

        private TcpClient client;
        public NetworkStream stream;

        public ServerConnection(string host = "localhost", int port = 8888)
        {
            this.host = host;
            this.port = port;
            Start();
        }

        public void Start()
        {
            client = new TcpClient();
            client.Connect(host, port);
            stream = client.GetStream();
        }
    }
}
