namespace TgKarBot.Logic
{
    internal class Teams
    {
        public static async Task<string> RegTeam(string teamId, long userId)
        {
            var userIdFromDb = await Database.Database.ReadTeamAsync(teamId);
            if (userIdFromDb != null)
            {
                return userIdFromDb == userId.ToString()
                    ? Constants.Messages.AlreadyRegistered
                    : Constants.Messages.OtherUser;
            }

            await Database.Database.CreateTeamAsync(userId.ToString(), teamId);
            return Constants.Messages.DoneTeamRegisteration;
        }

        internal static async Task<string?> CheckRegistration(long userId)
        {
            var teamIdFromDb = await Database.Database.ReadTeamByUserId(userId.ToString());
            return teamIdFromDb;
        }


        internal static async Task<bool> SaveProgress(string teamId, string num)
        {
            await Database.Database.CreateTeamProgressAsync(teamId, num);
            var readAllProgress = await Database.Database.ReadTeamProgressAsync(teamId);
            // TODO Подумать, что делать, с этим и как определять победу;
            var isWin = readAllProgress.Count >= 10;
            return isWin;
        }
    }
}
