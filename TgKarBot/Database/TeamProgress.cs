namespace TgKarBot.Database
{
    internal partial class Database
    {
        public static async Task CreateTeamProgressAsync(string teamId, string askId)
        {
            await Create(Constants.Database.InsertIntoTeamProgress, teamId, askId);
        }

        public static async Task<List<string?>> ReadAllTeamProgressAsync(string teamId)
        {
            return await ReadAllAsync(Constants.Database.GetAllFromTeamProgress, teamId, "AskId");
        }

        public static async Task<string?> ReadTeamProgressAsync(string teamId, string askId)
        {
            return await ReadAsync(Constants.Database.GetAllFromTeamProgress, teamId, "AskId", askId);
        }

        public static async Task UpdateTeamProgressAsync(string teamId, string askId)
        {
            await UpdateAsync(Constants.Database.UpdateTeamProgress, teamId, askId, "TeamId");
        }

        public static async Task DeleteTeamProgressAsync(string teamId)
        {
            await DeleteAsync(Constants.Database.DeleteFromTeamProgress, teamId);
        }
    }
}
