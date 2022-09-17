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

        public static async Task<RewardModel?> ReadAsync(string askId)
        {
            await using var context = new TgBotDatabaseContext();
            var reward = await context.Rewards.FirstOrDefaultAsync(x => x.AskId == askId);
            return reward;
        }

        public static async Task<int> GetMainTaskCount()
        {
            await using var context = new TgBotDatabaseContext();
            return await context.Rewards.CountAsync(x => x.IsMain == true);
        }

        public static async Task UpdateTypeAsync(string askId, bool isMain, int? timeBonus = null)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Rewards.FirstOrDefaultAsync(x => x.AskId == askId);
            if (obj != null)
            {
                obj.IsMain = isMain;
                obj.TimeBonus = timeBonus;
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
