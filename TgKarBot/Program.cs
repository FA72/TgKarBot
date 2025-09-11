using TgKarBot.API;

namespace TgKarBot;

internal class Program
{
    static async Task Main(string[] args)
    {
        const string logPath = @"C:\\TgKarBotLog\\log.txt";
        Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);
        await using var fileWriter = new StreamWriter(logPath, append: true);
        fileWriter.AutoFlush = true;
        Console.SetOut(new ConsoleFileWriter(Console.Out, fileWriter));
        Console.SetError(new ConsoleFileWriter(Console.Error, fileWriter));

        Connect connect = new();
        await connect.StartAsync();
    }
}