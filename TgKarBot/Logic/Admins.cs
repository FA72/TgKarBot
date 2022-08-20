using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Logic
{
    internal class Admins
    {
        internal static async Task<string> AddTask(long userId, string message)
        {
            if (!await CheckAdmins(userId)) return Constants.Messages.OnlyForAdmins;


        }

        internal static async Task<bool> CheckAdmins(long userId)
        {
            var adminId = await Database.Database.ReadAdminAsync(userId.ToString());
            return adminId != null;
        }
    }
}
