using NLog;

namespace TgKarBot
{
    public class StaticLogger
    {
        public static ILogger Logger { get; set; } = LogManager.GetCurrentClassLogger();
    }
}
