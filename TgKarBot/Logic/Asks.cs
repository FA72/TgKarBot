using TgKarBot.Logic.Helpers;

namespace TgKarBot.Logic
{
    internal class Asks
    {
        /// <summary>
        /// Принимает ответ от команды и проверяет его на правильность.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<string> CheckAsk(long userId, string message)
        {
            var teamId = await Teams.CheckRegistration(userId);
            if (teamId == null)
                return Constants.Messages.NotRegistered;

            var splittedMessage = message.Split();
            if (splittedMessage.Length < 3)
                return Constants.Messages.IncorrectInput + Constants.Commands.AskSample;

            var num = splittedMessage[1];

            if (await Database.Database.ReadTeamProgressAsync(teamId, num) != null)
                return Constants.Messages.AlreadyAsked;

            var ask = Parser.ParseBodyMessage(splittedMessage, 2);
            var correctAsk = await Database.Database.ReadAskAsync(num);

            if (correctAsk == null) return Constants.Messages.IncorrectNum;

            if (!ask.Equals(correctAsk, StringComparison.OrdinalIgnoreCase)) return Constants.Messages.NotCorrectAsk;

            var isWin = await Teams.SaveProgress(teamId, num);

            if (isWin) return Constants.Messages.WinTheGame;

            var reward = await Database.Database.ReadRewardAsync(num);
            return $"{Constants.Messages.Correct}\n{reward}";
        }
    }
}
