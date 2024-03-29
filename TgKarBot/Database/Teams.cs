﻿using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal class Teams
    {
        public static async Task CreateAsync(string userId, string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            await context.Teams.AddAsync(new TeamModel(teamId, userId));
            await context.SaveChangesAsync();
        }

        public static async Task<string?> ReadAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var team = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            return team?.UserId;
        }

        public static async Task<bool> ReadEndStateAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var team = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            return team.IsEnd;
        }

        public static async Task<(int?, int?)> ReadBonusTimeAndPenaltyAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var team = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            return (team?.BonusTime, team?.Penalty);
        }


        public static async Task<string?> ReadStartTimeAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var team = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            return team?.StartTime?.ToString("G");
        }

        public static async Task<string?> ReadByUserId(string userId)
        {
            await using var context = new TgBotDatabaseContext();
            var team = await context.Teams.FirstOrDefaultAsync(x => x.UserId == userId);
            return team?.TeamId;
        }

        public static async Task<List<string>> ReadAllUsersId()
        {
            await using var context = new TgBotDatabaseContext();
            var users = (await context.Teams.ToListAsync()).Select(x => x.UserId).ToList();
            return users;
        }

        public static async Task StartGame(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            if (obj != null)
            {
                obj.StartTime = DateTime.Now;
                await context.SaveChangesAsync();
            }
        } 

        public static async Task UpdateAsync(string teamId, string userId)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            if (obj != null)
            {
                obj.UserId = userId;
                await context.SaveChangesAsync();
            }
        }

        public static async Task EndGame(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            if (obj != null)
            {
                obj.IsEnd = true;
                await context.SaveChangesAsync();
            }
        }

        public static async Task UpdateBonusTimeAsync(string teamId, int bonusTime)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            if (obj != null)
            {
                obj.BonusTime += bonusTime;
                await context.SaveChangesAsync();
            }
        }

        public static async Task AddPenaltyAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            if (obj != null)
            {
                obj.Penalty ++;
                await context.SaveChangesAsync();
            }
        }

        public static async Task DeleteAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            if (obj != null)
            {
                context.Teams.Remove(obj);
                await context.SaveChangesAsync();
            }
        }
    }
}
