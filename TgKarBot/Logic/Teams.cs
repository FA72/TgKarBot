using TgKarBot.Constants;
using System.Configuration;
using System.Collections.Specialized;
using System.Text;
using System;
using TgKarBot.Database.Models;

namespace TgKarBot.Logic
{
    internal class Teams
    {
        public static async Task<string> RegTeam(string teamId, long userId)
        {
            var team = await Database.Teams.ReadByUserId(userId.ToString());
            if (team != null)
            {
                return Messages.AlreadyRegistered;
            }

            var userIdFromDb = await Database.Teams.ReadAsync(teamId);
            if (userIdFromDb != null)
            {
                return Messages.OtherUser;
            }

            await Database.Teams.CreateAsync(userId.ToString(), teamId);
            return Messages.DoneTeamRegisteration;
        }

        public static async Task<string> StartGame(long userId)
        {
            var teamId = await CheckRegistration(userId);
            
            if (teamId is null) return Messages.NotRegistered;
            var textBuilder = new StringBuilder();
            var startTimeString = await Database.Teams.ReadStartTimeAsync(teamId);
            List<TaskModel>? tasks;
            if (startTimeString != null)
            {
                textBuilder.Append(Messages.AlreadyStarted);
                await ShowFirstTasks(textBuilder);
                return textBuilder.ToString();
            }

            if (!bool.Parse(ConfigurationManager.AppSettings.Get("GameStarted")))
                return Messages.GameIsNotStarted;

            await Database.Teams.StartGame(teamId);
            textBuilder.Append(Messages.GameStart);
            textBuilder.Append($" {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}");
            await ShowFirstTasks(textBuilder);
            return textBuilder.ToString();
        }

        private static async Task ShowFirstTasks(StringBuilder textBuilder)
        {
            List<TaskModel> tasks;
            textBuilder.Append($"\n{Messages.GameStartTasks}");
            tasks = await Database.Tasks.ReadAllAsync();
            foreach (var task in tasks)
            {
                textBuilder.Append($"\n\n{task.Id}. {task.Text}");
            }
        }

        public static async Task<string> Progress(long userId)
        {
            var teamId = await CheckRegistration(userId);

            if (teamId is null) return Messages.NotRegistered;

            var startTimeString = await Database.Teams.ReadStartTimeAsync(teamId);
            if (startTimeString == null)
                return Messages.NotStarted;

            var (isWin, mainCorrect, mainTasksCount) = await GetFullProgress(teamId);

            if (mainCorrect == 0)
            {
                return "Вы пока не ответили ни на один вопрос из основного блока =(";
            }
            var (bonusTime, penalty) = await Database.Teams.ReadBonusTimeAndPenaltyAsync(teamId);
            var penaltyTime = penalty * Numbers.penaltyTime;

            var time = await Asks.GetTime(startTimeString, teamId, bonusTime);
            var progressMessage = $"Вы ответили на {mainCorrect}/{mainTasksCount}\n{Messages.EndTheGameTime}{time}. Из них бонусные минуты - {bonusTime} и добавленные за неправильные ответы {penaltyTime} мин.";

            return progressMessage;
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
            var mainTasks = await Database.Rewards.GetMainTaskIds();
            var mainCorrect = readAllProgress.Count(x => x != null && mainTasks.Contains(x));
            var isWin = mainCorrect == mainTasks.Count;
            return (isWin, mainCorrect, mainTasks.Count);
        }
    }
}
