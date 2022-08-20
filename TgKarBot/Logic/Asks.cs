using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Logic
{
    internal class Asks
    {
        public static async Task<string> Ask(long userId, string message)
        {
            try
            {
                var teamId = await Teams.CheckRegistration(userId);
                if (teamId == null)
                    return Constants.Messages.NotRegistered;

                var splittedMessage = message.Split();
                if (splittedMessage.Length < 3)
                    return Constants.Messages.IncorrectInput + Constants.Commands.AskSample;

                var num = splittedMessage[0];
                var sb = new StringBuilder(splittedMessage[2]);
                for (var i = 3; i < splittedMessage.Length; i++)
                {
                    sb.Append(splittedMessage[i]);
                }

                var ask = sb.ToString();
                var correctAsk = await Database.Database.ReadAskAsync(num);
                
                if (correctAsk == null) return Constants.Messages.IncorrectNum;

                if (ask != correctAsk) return Constants.Messages.NotCorrectAsk;

                await Database.Database.CreateTeamProgressAsync(teamId, num);
                var readAllProgress = await Database.Database.ReadTeamProgressAsync(teamId);
                // TODO Подумать, что делать, с этим и как определять победу;

                if (readAllProgress.Count >= 10) return Constants.Messages.WinTheGame;

                var reward = await Database.Database.ReadRewardAsync(num);
                return Constants.Messages.Correct + @"\n" + reward;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
