using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal class Tasks
    {
        public static async Task CreateAsync(string taskId, string task)
        {
            await using var context = new TgBotDatabaseContext();
            await context.Tasks.AddAsync(new TaskModel(taskId, task));
            await context.SaveChangesAsync();
        }

        public static async Task<string?> ReadAsync(string taskId)
        {
            await using var context = new TgBotDatabaseContext();
            var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            return task?.Text;
        }

        public static async Task<List<TaskModel>> ReadAllAsync()
        {
            await using var context = new TgBotDatabaseContext();
            var task = await context.Tasks.ToListAsync();
            return task;
        }

        public static async Task UpdateAsync(string taskId, string task)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            if (obj != null)
            {
                obj.Text = task;
                await context.SaveChangesAsync();
            }
        }

        public static async Task DeleteAsync(string taskId)
        {
            await using var context = new TgBotDatabaseContext();
            var obj = await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            if (obj != null)
            {
                context.Tasks.Remove(obj);
                await context.SaveChangesAsync();
            }
        }
    }
}