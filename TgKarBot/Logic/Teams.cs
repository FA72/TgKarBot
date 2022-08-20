using TgKarBot.Database;

namespace TgKarBot.Logic
{
    internal class Teams
    {
        public static async Task<string> Registrate(string teamId, long userId)
        {
            try
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
            catch (Exception e)
            {
                return Constants.Messages.Error;
            }
        }

        internal static async Task<string?> CheckRegistration(long userId)
        {
            var teamIdFromDb = await Database.Database.ReadTeamByUserId(userId.ToString());
            return teamIdFromDb;
        }
    }
}
