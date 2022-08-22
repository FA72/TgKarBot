using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal partial class Database
    {
        public static async Task CreateAskAsync(string askId, string ask)
        {
            await using var context = new TgBotDatabaseContext();
            await context.Asks.AddAsync(new Ask(askId, ask));
            await context.SaveChangesAsync();
        }

        public static async Task<string?> ReadAskAsync(string askId)
        {
            return await ReadAsync(Constants.Database.GetFromAsks, askId, "CorrectAsk");
        }

        public static async Task UpdateAskAsync(string askId, string ask)
        {
            await UpdateAsync(Constants.Database.UpdateAsks, askId, ask, "Id");
        }

        public static async Task DeleteAskAsync(string askId)
        {
            await DeleteAsync(Constants.Database.DeleteFromAsks, askId);
        }
    }
}