using System;
using System.Collections.Generic;
using System.Dynamic;
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
                return await GeneralActions.AddSomething(
                    userId, message, 
                    Database.Database.ReadAskAsync, 
                    Database.Database.CreateAskAsync, 
                    Messages.TaskAlreadyExist, 
                    Messages.TaskSuccessCreation);
            }
            catch (Exception)
            {
                return Messages.Error;
            }
        }

        internal static async Task<string> DeleteTask(long userId, string message)
        {
            try
            {
                return await GeneralActions.DeleteSomething(
                    userId, message,
                    Database.Database.ReadAskAsync,
                    Database.Database.DeleteAskAsync,
                    Messages.TaskDoesntExist,
                    Messages.TaskSuccessDelete);
            }
            catch (Exception)
            {
                return Messages.Error;
            }
        }

        internal static async Task<string> AddAdmin(long userId, string message)
        {
            try
            {
                return await GeneralActions.AddSomething(
                    userId, message,
                    Database.Database.ReadAdminAsync,
                    Database.Database.CreateAdminAsync,
                    Messages.AdminAlreadyExist,
                    Messages.AdminSuccessCreation);
            }
            catch (Exception)
            {
                return Messages.Error;
            }
        }

        internal static async Task<string> DeleteAdmin(long userId, string message)
        {
            try
            {
                return await GeneralActions.DeleteSomething(
                    userId, message,
                    Database.Database.ReadAdminAsync,
                    Database.Database.DeleteAdminAsync,
                    Messages.AdminDoesntExist,
                    Messages.AdminSuccessDelete);
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
