namespace TgKarBot.Database
{
    internal partial class Database
    {
        public static async Task CreateRewardAsync(string askId, string reward)
        {
            await Create(Constants.Database.InsertIntoRewards, askId, reward);
        }

        public static async Task<string?> ReadRewardAsync(string askId)
        {
            return await ReadAsync(Constants.Database.GetFromRewards, askId, "UserId");
        }

        public static async Task UpdateRewardAsync(string askId, string Reward)
        {
            await UpdateAsync(Constants.Database.UpdateRewards, askId, Reward, "RewardId");
        }

        public static async Task DeleteRewardAsync(string askId)
        {
            await DeleteAsync(Constants.Database.DeleteFromRewards, askId);
        }
    }
}
