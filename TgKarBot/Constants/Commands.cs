﻿namespace TgKarBot.Constants
{
    internal class Commands
    {
        internal const string Start = "/start";
        internal static List<string> StartSynonims = new() { Start, "start", "старт", "начать", "запуск"};

        internal const string RegTeam = "/regteam";
        internal static List<string> RegTeamSynonims = new() { RegTeam, "team", "reg", "регистрация", "команда" };
        internal const string RegTeamSample = $"{RegTeam} id";

        internal const string Ask = "/ask";
        internal static List<string> AskSynonims = new() { Ask, "ask", "Ответ" };
        internal const string AskSample = $"{RegTeam} (номер команды)";

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

        internal const string DeleteReward = "/delreward";
        internal const string DeleteRewardSample = $"{AddReward} (номер вопроса)";

        internal const string Help = "/help";
        internal static List<string> HelpSynonims = new() { Help, "help", "помощь" };

        internal static List<List<string>> Synonims = new()
        {
            StartSynonims,
            RegTeamSynonims,
            AskSynonims,
            HelpSynonims
        };
    }
}
