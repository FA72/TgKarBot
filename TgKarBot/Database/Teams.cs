namespace TgKarBot.Database
{
    internal partial class Database
    {
        public static async Task CreateTeamAsync(string userId, string teamId)
        {
            await Create(Constants.Database.InsertIntoTeams, teamId, userId);
        }

        public static async Task<string?> ReadTeamAsync(string teamId)
        {
            return await ReadAsync(Constants.Database.GetFromTeams, teamId, "UserId");
        }

        public static async Task<string?> ReadTeamByUserId(string userId)
        {
            return await ReadAsync(Constants.Database.GetFromTeamsByUserId, userId, "TeamId");
        }

        public static async Task UpdateTeamAsync(string teamId, string userId)
        {
            await UpdateAsync(Constants.Database.UpdateTeams, teamId, userId, "TeamId");
        }

        public static async Task DeleteTeamAsync(string teamId)
        {
            await DeleteAsync(Constants.Database.DeleteFromTeams, teamId);
        }
    }
}
