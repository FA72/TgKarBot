using TgKarBot.Constants;
using System.Configuration;
using System.Collections.Specialized;
using System.Text;

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

        internal static async Task<string?> CheckRegistration(long userId)
        {
            var teamIdFromDb = await Database.Teams.ReadByUserId(userId.ToString());
            return teamIdFromDb;
        }

        internal static async Task<bool> SaveProgress(string teamId, string num)
        {
            await Database.TeamsProgress.CreateAsync(teamId, num);
            var readAllProgress = await Database.TeamsProgress.ReadAllAsync(teamId);
            // TODO Подумать, что делать, с этим и как определять победу;
            var isWin = readAllProgress.Count >= 10;
            return isWin;
        }
    }
}
