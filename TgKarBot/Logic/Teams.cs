using TgKarBot.Constants;
using System.Configuration;
using System.Collections.Specialized;
using System.Text;
using System;

namespace TgKarBot.Logic
{
    internal class Teams
    {
        public static async Task<string> RegTeam(string teamId, long userId)
        {
            var userIdFromDb = await Database.Teams.ReadAsync(teamId);
            if (userIdFromDb != null)
            {
                return userIdFromDb == userId.ToString()
                    ? Messages.AlreadyRegistered
                    : Messages.OtherUser;
            }

            await Database.Teams.CreateAsync(userId.ToString(), teamId);
            return Messages.DoneTeamRegisteration;
        }

        public static async Task<string> StartGame(long userId)
        {
            var teamId = await CheckRegistration(userId);
            
            if (teamId is null) return Messages.NotRegistered;


            if (!bool.Parse(ConfigurationManager.AppSettings.Get("GameStarted")))
                return Messages.GameIsNotStarted;

            await Database.Teams.StartGame(teamId);
            var textBuilder = new StringBuilder(Messages.GameStart);
            textBuilder.Append($" {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}");
            textBuilder.Append($"\n{Messages.GameStartTasks}");
            var tasks = await Database.Tasks.ReadAllAsync();
            foreach (var task in tasks)
            {
                textBuilder.Append($"\n{task.Id}. {task.Text}");
            }
            return textBuilder.ToString();
        }

        public static async Task<string> EndGame(long userId)
        {
            var teamId = await CheckRegistration(userId);

            if (teamId is null) return Messages.NotRegistered;

            var startTimeString = await Database.Teams.ReadStartTimeAsync(teamId);
            if (startTimeString == null)
                return Messages.NotStarted;

            await Database.Teams.EndGame(teamId);
            var (isWin, mainCorrect, mainTasksCount) = await GetFullProgress(teamId);

            var bonusTime = await Database.Teams.ReadBonusTimeAsync(teamId);

            var time = await Asks.GetTime(startTimeString, teamId, bonusTime);
            var wingameMessage = $"{Messages.EndTheGame}{mainCorrect}/{mainTasksCount}\n{Messages.EndTheGameTime}{time}";

            return wingameMessage;
        }

        internal static async Task<string?> CheckRegistration(long userId)
        {
            var teamIdFromDb = await Database.Teams.ReadByUserId(userId.ToString());
            return teamIdFromDb;
        }

        internal static async Task<(bool, int, int)> SaveProgress(string teamId, string num)
        {
            await Database.TeamsProgress.CreateAsync(teamId, num);
            var (isWin, mainCorrect, mainTasksCount) = await GetFullProgress(teamId);
            return (isWin, mainCorrect, mainTasksCount);
        }

        private static async Task<(bool isWin, int mainCorrect, int Count)> GetFullProgress(string teamId)
        {
            var readAllProgress = await Database.TeamsProgress.ReadAllAsync(teamId);

            // TODO Подумать, что делать, с этим и как определять победу;
            var mainTasks = await Database.Rewards.GetMainTaskIds();
            var mainCorrect = readAllProgress.Count(x => x != null && mainTasks.Contains(x));
            var isWin = mainCorrect == mainTasks.Count;
            return (isWin, mainCorrect, mainTasks.Count);
        }
    }
}
