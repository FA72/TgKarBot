using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal partial class Admins
    {
        public static async Task CreateAsync(string userId, string dummy = null)
        {
            await using var context = new TgBotDatabaseContext();
            await context.Admins.AddAsync(new AdminModel(userId));
            await context.SaveChangesAsync();
        }

        public static async Task<string?> ReadAsync(string userId)
        {
            await using var context = new TgBotDatabaseContext();
            var admins = await context.Admins.FirstOrDefaultAsync(x => x.UserId == userId);
            return admins?.UserId;
        }

        public static async Task DeleteAsync(string userId)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Admins.FirstOrDefaultAsync(x => x.UserId == userId);
            if (obj != null)
            {
                context.Admins.Remove(obj);
                await context.SaveChangesAsync();
            }
        }
    }
}