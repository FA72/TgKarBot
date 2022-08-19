using TgKarBot.API;

namespace TgKarBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Connect connect = new();
            connect.Start();
        }
    }
}