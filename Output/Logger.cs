using System;
namespace ClientV3.Output
{
    public class Logger
    {
        public Logger() { }

        public void PostException(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void Post(string text)
        {
            Console.WriteLine(text);
        }
    }
}
