using TgKarBot.API;

namespace TgKarBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Connect connect = new();
            await connect.StartAsync();
        }
    }
}