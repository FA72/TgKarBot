namespace TgKarBot.Constants
{
    internal class Commands
    {

        internal const string Start = "/start";
        internal static List<string> StartSynonims = new List<string>() { "/start", "start", "старт", "начать", "запуск"};

        internal static List<List<string>> Synonims = new List<List<string>>()
        {
            StartSynonims,
        };
    }
}
