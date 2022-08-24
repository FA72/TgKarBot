using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal class Rewards
    {
        public static async Task CreateAsync(string askId, string reward)
        {
            await using var context = new TgBotDatabaseContext();
            await context.Rewards.AddAsync(new RewardModel(askId, reward));
            await context.SaveChangesAsync();
        }

        public static async Task<string?> ReadAsync(string askId)
        {
            await using var context = new TgBotDatabaseContext();
            var reward = await context.Rewards.FirstOrDefaultAsync(x => x.AskId == askId);
            return reward?.Reward;
        }

        public static async Task UpdateAsync(string askId, string Reward)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Rewards.FirstOrDefaultAsync(x => x.AskId == askId);
            if (obj != null)
            {
                obj.Reward = Reward;
                await context.SaveChangesAsync();
            }
        }

        public static async Task DeleteAsync(string askId)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Rewards.FirstOrDefaultAsync(x => x.AskId == askId);
            if (obj != null)
            {
                context.Rewards.Remove(obj);
                await context.SaveChangesAsync();
            }
        }
    }
}
