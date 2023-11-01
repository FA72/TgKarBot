using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;
using TgKarBot.Logic;

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

        public static async Task<List<string>> ReadAllAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var query = context.TeamsProgress.Where(tp => tp.TeamId == teamId).Select(tp => tp.AskId);
            var teamsProgress = await query.ToListAsync();
            return teamsProgress;
        }

        public static async Task<string?> ReadAsync(string teamId, string askId)
        {
            await using var context = new TgBotDatabaseContext();
            var teamProgress = await context.TeamsProgress.FirstOrDefaultAsync(x => x.TeamId == teamId && x.AskId == askId);
            return teamProgress?.TeamId;
        }

        public static async Task<DateTime?> ReadLastAskTimeAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            return await context.TeamsProgress
                .Where(tp => tp.TeamId == teamId)
                .OrderByDescending(tp => tp.Time)
                .Select(tp => tp.Time)
                .FirstOrDefaultAsync();
        }


        public static async Task<(DateTime? startDrinkTime, DateTime? endDrinkTime)> ReadLastDrinkTimeAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();

            var result = await context.TeamsProgress
                .Where(tp => tp.TeamId == teamId)
                .OrderByDescending(tp => tp.Time)
                .Select(tp => new { tp.StartDrinkTime, tp.EndDrinkTime })
                .FirstOrDefaultAsync();

            return (result?.StartDrinkTime, result?.EndDrinkTime);
        }

        public static async Task<string> UpdateDrinkTimesAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();

            var teamProgress = await context.TeamsProgress
                .Where(tp => tp.TeamId == teamId)
                .OrderByDescending(tp => tp.Time)
                .FirstOrDefaultAsync();

            if (teamProgress == null) return null;

            var currentTime = DateTime.Now;

            teamProgress.StartDrinkTime ??= currentTime;

            teamProgress.EndDrinkTime = currentTime;

            await context.SaveChangesAsync();

            return teamProgress.AskId;
        }

        public static async Task StartDrinkAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();

            var teamProgress = await context.TeamsProgress
                .Where(tp => tp.TeamId == teamId)
                .OrderByDescending(tp => tp.Time)
                .FirstOrDefaultAsync();

            if (teamProgress != null)
            {
                teamProgress.StartDrinkTime = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }

        public static async Task<TimeSpan> CalculateTotalDrinkTimeAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();

            var drinkTimes = await context.TeamsProgress
                .Where(tp => tp.TeamId == teamId && tp.StartDrinkTime.HasValue && tp.EndDrinkTime.HasValue)
                .Select(tp => new { Start = tp.StartDrinkTime.Value, End = tp.EndDrinkTime.Value })
                .ToListAsync();

            return drinkTimes.Aggregate(TimeSpan.Zero, (current, time) => current + (time.End - time.Start));
        }

        public static async Task<DateTime?> ReadAskTimeAsync(string teamId, string askId)
        {
            await using var context = new TgBotDatabaseContext();
            var teamProgress = await context.TeamsProgress.FirstOrDefaultAsync(x => x.TeamId == teamId && x.AskId == askId);
            return teamProgress?.Time;
        }
    }
}
