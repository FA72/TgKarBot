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
                    ? Constants.Messages.AlreadyRegistered
                    : Constants.Messages.OtherUser;
            }

            await Database.Teams.CreateAsync(userId.ToString(), teamId);
            return Constants.Messages.DoneTeamRegisteration;
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
