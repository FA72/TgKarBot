namespace TgKarBot.Constants
{
    internal class Commands
    {

        internal const string Start = "/start";
        internal static List<string> StartSynonims = new List<string>() { Start, "start", "старт", "начать", "запуск"};

        internal const string RegTeam = "/regteam";
        internal static List<string> RegTeamSynonims = new List<string>() { RegTeam, "team", "reg", "регистрация", "команда" };
        internal const string RegTeamSample = RegTeam + " id";

        internal const string Ask = "/ask";
        internal static List<string> AskSynonims = new List<string>() { Ask, "ask", "Ответ" };
        internal const string AskSample = RegTeam + " (номер вопроса) (ответ)";

        internal const string AddTask = "/addtask";
        internal const string AddTaskSample = AddTask + " (номер вопроса) (ответ)";

        internal static List<List<string>> Synonims = new List<List<string>>()
        {
            StartSynonims,
            RegTeamSynonims
        };
    }
}
