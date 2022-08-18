namespace TgKarBot.Constants
{
    internal class Commands
    {

        internal const string Start = "/start";
        internal static List<string> StartSynonims = new List<string>() { Start, "start", "старт", "начать", "запуск"};

        internal const string RegTeam = "/regteam";
        internal static List<string> RegTeamSynonims = new List<string>() { RegTeam, "team", "reg", "регистрация", "команда" };

        internal static List<List<string>> Synonims = new List<List<string>>()
        {
            StartSynonims,
            RegTeamSynonims
        };
    }
}
