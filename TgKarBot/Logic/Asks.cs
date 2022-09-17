using System.Text;
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

            var startTimeString = await Database.Teams.ReadStartTimeAsync(teamId);
            if (startTimeString == null)
                return Messages.NotStarted;

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

            var output = new StringBuilder($"{Messages.Correct}\n");

            var (isWin, progress, maxAsks) = await Teams.SaveProgress(teamId, num);
            var reward = await Database.Rewards.ReadAsync(num);

            if (!reward.IsMain) 
                await Database.Teams.UpdateBonusTimeAsync(teamId, (int) reward.TimeBonus);

            var bonusTime = await Database.Teams.ReadBonusTimeAsync(teamId);

            if (isWin)
            {
                await Database.Teams.EndGame(teamId);
                var time = await GetTime(startTimeString, teamId, bonusTime);
                var wingameMessage = $"{Messages.EndTheGame}{progress}/{maxAsks}\n{Messages.EndTheGameTime}{time}";

                output.Append(wingameMessage);
            }
            else 
            {
                output.Append($"На данный момент отвечено на {progress} из {maxAsks} вопросов. Также заработано {bonusTime} бонусного времени.\n" +
                               $"{Messages.Reward}\n {reward.Reward}");
            }

            return output.ToString();
        }

        public static async Task<string> GetTime(string startTimeString, string teamId, int? bonusTime)
        {
            var lastTime = await Database.TeamsProgress.ReadLastAskTimeAsync(teamId);
            var startTime = DateTime.Parse(startTimeString);
            var time = (lastTime - startTime - TimeSpan.FromMinutes((double) bonusTime)).ToString();
            return time;
        }
    }
}
