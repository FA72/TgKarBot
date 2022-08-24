using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal class Asks
    {
        public static async Task CreateAsync(string askId, string ask)
        {
            await using var context = new TgBotDatabaseContext();
            await context.Asks.AddAsync(new AskModel(askId, ask));
            await context.SaveChangesAsync();
        }

        public static async Task<string?> ReadAsync(string askId)
        {
            await using var context = new TgBotDatabaseContext();
            var ask = await context.Asks.FirstOrDefaultAsync(x => x.Id == askId);
            return ask?.CorrectAsk;
        }

        public static async Task UpdateAsync(string askId, string ask)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Asks.FirstOrDefaultAsync(x => x.Id == askId);
            if (obj != null)
            {
                obj.CorrectAsk = ask;
                await context.SaveChangesAsync();
            }
        }

        public static async Task DeleteAsync(string askId)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Asks.FirstOrDefaultAsync(x => x.Id == askId);
            if (obj != null)
            {
                context.Asks.Remove(obj);
                await context.SaveChangesAsync();
            }
        }
    }
}