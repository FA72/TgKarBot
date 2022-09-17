using Telegram.Bot.Types;
using TgKarBot.Constants;
using TgKarBot.Database.Models;
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
                return Messages.NotRegistered;

            var splittedMessage = message.Split();
            if (splittedMessage.Length < 3)
                return Messages.IncorrectInput + Commands.AskSample;

            var num = splittedMessage[1];

            if (await Database.TeamsProgress.ReadAsync(teamId, num) != null)
                return $"{Messages.AlreadyAsked}\n{(await Database.Rewards.ReadAsync(num)).Reward}";

            var ask = Parser.ParseBodyMessage(splittedMessage, 2);
            var correctAsk = await Database.Asks.ReadAsync(num);

            if (correctAsk == null) return Messages.IncorrectNum;

            if (!ask.Equals(correctAsk.Trim(), StringComparison.OrdinalIgnoreCase)) return Messages.NotCorrectAsk;

            var reward = await Database.Rewards.ReadAsync(num);
            var isWin = await Teams.SaveProgress(teamId, num);

            if (reward.IsMain)
            {
                if (isWin) return Messages.WinTheGame;
            }
            else
            {
                await Database.Teams.UpdateBonusTimeAsync(teamId, (int) reward.TimeBonus);
            }

            return $"{Messages.Correct}\n{reward.Reward}";
        }
    }
}
