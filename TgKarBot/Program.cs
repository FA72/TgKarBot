using TgKarBot.API;

namespace TgKarBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connect = new Connect();
            connect.Start();
        }
    }
}