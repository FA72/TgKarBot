using System;
using TgKarBot.API;

namespace TgKarBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connect = new Connect();
            connect.Start();
        }
    }
}