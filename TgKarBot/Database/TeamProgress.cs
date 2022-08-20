namespace TgKarBot.Database
{
    internal partial class Database
    {
        public static async Task CreateTeamProgressAsync(string teamId, string askId)
        {
            await Create(Constants.Database.InsertIntoTeamProgress, teamId, askId);
        }

        public static async Task<List<string?>> ReadTeamProgressAsync(string teamId)
        {
            return await ReadAllAsync(Constants.Database.GetFromTeamProgress, teamId, "UserId");
        }

        public static async Task UpdateTeamProgressAsync(string teamId, string askId)
        {
            await UpdateAsync(Constants.Database.UpdateTeamProgress, teamId, askId, "TeamProgressId");
        }

        public static async Task DeleteTeamProgressAsync(string teamId)
        {
            await DeleteAsync(Constants.Database.DeleteFromTeamProgress, teamId);
        }
    }
}
