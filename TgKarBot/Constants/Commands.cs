namespace TgKarBot.Constants
{
    internal class Commands
    {
        internal const string Start = "/start";
        internal const string Help = "/help";
        internal static List<string> StartSynonims = new() { Start, "start", "старт", "начать", "запуск"};

        internal const string GlobalStart = "/globalstart";
        internal const string GlobalFinish = "/globalfinish";

        internal const string StartGame = "/startgame";
        internal static List<string> StartGameSynonims = new() { StartGame, "startgame", "стартигры", "начатьигру", "запускигры" };

        internal const string RegTeam = "/regteam";
        internal static List<string> RegTeamSynonims = new() { RegTeam, "team", "reg", "регистрация", "команда" };
        internal const string RegTeamSample = $"{RegTeam} id";

        internal const string Progress = "/progress";
        internal const string ToAll = "/toall";


        internal const string Ask = "/ask";
        internal static List<string> AskSynonims = new() { Ask, "ask", "Ответ" };
        internal const string AskSample = $"{Ask} (номер вопроса) (ответ)";

        internal const string AddAsk = "/addask";
        internal const string AddAskSample = $"{AddAsk} (номер вопроса) (ответ)";

        internal const string DeleteAsk = "/deltask";
        internal const string DeleteAskSample = $"{DeleteAsk} (номер вопроса)";

        internal const string AddAdmin = "/addadmin";
        internal const string AddAdminSample = $"{AddAdmin} UserId";

        internal const string DeleteAdmin = "/deladmin";
        internal const string DeleteAdminSample = $"{DeleteAdmin} (номер вопроса) (ответ)";

        internal const string AddReward = "/addreward";
        internal const string AddRewardSample = $"{AddReward} (номер вопроса) (ответ)";
        internal const string SetRewardType = "/setrewardtype";
        internal const string SetRewardTypeSample = $"{AddReward} (номер вопроса) (isMain) (ВРЕМЯ)";

        internal const string DeleteReward = "/delreward";
        internal const string DeleteRewardSample = $"{AddReward} (номер вопроса)";

        internal const string Support = "/support";
        internal static List<string> SupportSynonims = new() { Support, "помощь", "поддежка" };

        internal static List<List<string>> Synonims = new()
        {
            StartSynonims,
            RegTeamSynonims,
            AskSynonims,
            SupportSynonims,
            StartGameSynonims
        };
    }
}
