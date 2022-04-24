using System;

namespace ClientV3.Output
{
    public class MessageOutputService
    {
        public MessageOutputService() { }

        public void OutputMessage(string text, string senderName = "Server-100level")
        {
            Console.WriteLine($"{senderName}: {text}");
        }
    }
}
