using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgKarBot.Constants;
using TgKarBot.Logic.Helpers;

namespace TgKarBot.Logic
{
    internal class Admins
    {
        internal static async Task<string> AddTask(long userId, string message)
        {
            try
            {
                if (!await CheckAdmins(userId)) return Messages.OnlyForAdmins;

                var splittedMessage = message.Split();
                var num = splittedMessage[1];
                if (await Database.Database.ReadAskAsync(num) != null)
                    return Messages.TaskAlreadyExist;

                var ask = Parser.ParseBodyMessage(splittedMessage, 2);
                await Database.Database.CreateAskAsync(num, ask);

                return Messages.TaskSuccess;
            }
            catch (Exception)
            {
                return Messages.Error;
            }
        }

        internal static async Task<bool> CheckAdmins(long userId)
        {
            var adminId = await Database.Database.ReadAdminAsync(userId.ToString());
            return adminId != null;
        }
    }
}
