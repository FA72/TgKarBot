﻿using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal class Teams
    {
        public static async Task CreateAsync(string userId, string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            await context.Teams.AddAsync(new TeamModel(userId, teamId));
            await context.SaveChangesAsync();
        }

        public static async Task<string?> ReadAsync(string teamId)
        {
            await using var context = new TgBotDatabaseContext();
            var team = await context.Teams.FirstOrDefaultAsync(x => x.TeamId == teamId);
            return team?.UserId;
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
            var parameters = new Microsoft.Data.SqlClient.SqlParameter(TeamModel.TeamIdparam, teamId);
            context.Teams.FromSqlRaw($"StartForTeam {TeamModel.TeamIdparam}", parameters);
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
