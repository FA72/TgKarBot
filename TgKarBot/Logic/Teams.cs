using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Logic
{
    internal class Teams
    {
        private static string _teamsPath = Directory.GetCurrentDirectory() + "/teams.txt";
        private static string _asksPath = Directory.GetCurrentDirectory() + "/asks.txt";
        private static string _teamsProgressPath = Directory.GetCurrentDirectory() + "/teamsProgress.txt";
        private static string _rewardsPath = Directory.GetCurrentDirectory() + "/rewards.txt";
        public static async Task<string> Registrate(string commandId, long userId)
        {
            if (File.Exists(_teamsPath))
            {
                using var streamReader = new StreamReader(_teamsPath);
                var line = streamReader.ReadLine();
                
                while (line != null)
                {
                    var splittedLine = line.Split();
                    if (splittedLine[0] == commandId)
                    {
                        if (splittedLine[1] == userId.ToString())
                            return Constants.Messages.AlreadyRegistered;

                        return Constants.Messages.OtherUser;
                    }
                    line = await streamReader.ReadLineAsync();
                }
            }

            using (var file = new StreamWriter(_teamsPath))
                await file.WriteLineAsync($"{commandId} {userId}");

            using (var file = new StreamWriter(_teamsProgressPath))
                await file.WriteLineAsync($"{commandId} 0");
            return Constants.Messages.DoneTeamRegisteration;
        }

        public static async Task<string> Ask(long userId, string message)
        {
            if (!File.Exists(_asksPath)) return Constants.Messages.Error;

            using var streamReader = new StreamReader(_teamsPath);
            var line = streamReader.ReadLine();
            var commandId = "";

            while (line != null)
            {
                var splittedLine = line.Split();
                if (splittedLine[1] == userId.ToString())
                {
                    commandId = splittedLine[0];
                    break;
                }
                               
                line = await streamReader.ReadLineAsync();
            }

            if (string.IsNullOrEmpty(commandId))
                return Constants.Messages.NotRegistered;

            var splittedMessage = message.Split();
            var num = int.Parse(splittedMessage[0]);
            var ask = message.Remove(0, splittedMessage[0].Length + 1);
            var allLinesAsks = await File.ReadAllLinesAsync(_asksPath);
            if (allLinesAsks.Length < num)
                return Constants.Messages.NotCorrectNum;

            var correctAsk = allLinesAsks.First(x => x.StartsWith(num.ToString()));

            if (ask != correctAsk)
                return Constants.Messages.NotCorrectAsk;

            var allLinesProgress = await File.ReadAllLinesAsync(_teamsProgressPath);
            var currentMagicNumber = int.Parse(allLinesProgress.First(x => x.Contains(commandId)).Split()[1]);
            currentMagicNumber = (int) Math.Pow(2, num);
            
            await File.WriteAllLinesAsync(_teamsProgressPath, allLinesProgress.Where(x => !x.Contains(commandId)));

            using (var file = new StreamWriter(_teamsProgressPath))
                await file.WriteLineAsync($"{commandId} {currentMagicNumber}");

            var rewards = await File.ReadAllLinesAsync(_teamsProgressPath);
            var rewardText = rewards.First(x => x.StartsWith(num.ToString())).Remove(0, splittedMessage[0].Length + 1);

            if (Math.Pow(2, allLinesProgress.Length + 1) - 1 == num)
                return Constants.Messages.WinTheGame + @"\n" + rewardText; // Todo Тут потом надо будет обязательно подумать про финальное сообщение,
                                                      // я так до сих пор в механике и не уверен
            return Constants.Messages.Correct + @"\n" + rewardText;
            
        }
    }
}
