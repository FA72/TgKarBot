using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal class TeamsProgress
    {
        public static async Task CreateAsync(string teamId, string askId)
        {
            await using var context = new TgBotDatabaseContext();
            await context.TeamsProgress.AddAsync(new TeamProgressModel(teamId, askId));
            await context.SaveChangesAsync();
        }

        public static async Task<List<string?>> ReadAllAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var query = from tp in context.TeamsProgress where tp.TeamId == teamId select tp.TeamId;
            var teamsProgress = await query.ToListAsync();
            return teamsProgress;
        }

        public static async Task<string?> ReadAsync(string teamId, string askId)
        {
            await using var context = new TgBotDatabaseContext();
            var teamProgress = await context.TeamsProgress.FirstOrDefaultAsync(x => x.TeamId == teamId && x.AskId == askId);
            return teamProgress?.TeamId;
        }
    }
}
