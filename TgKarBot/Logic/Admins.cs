using TgKarBot.Constants;
using TgKarBot.Logic.Helpers;

namespace TgKarBot.Logic
{
    internal class Admins
    {
        internal static async Task<string> AddAsk(long userId, string message)
        {
            return await GeneralActions.AddSomething(
                    userId, message,
                    Database.Database.ReadAskAsync,
                    Database.Database.CreateAskAsync,
                    Messages.AskAlreadyExist,
                    Messages.AskSuccessCreation,
                    2);
        }

        internal static async Task<string> DeleteAsk(long userId, string message)
        {
            return await GeneralActions.DeleteSomething(
                userId, message,
                Database.Database.ReadAskAsync,
                Database.Database.DeleteAskAsync,
                Messages.AskDoesntExist,
                Messages.AskSuccessDelete);
        }

        internal static async Task<string> AddAdmin(long userId, string message)
        {
            return await GeneralActions.AddSomething(
                userId, message,
                Database.Database.ReadAdminAsync,
                Database.Database.CreateAdminAsync,
                Messages.AdminAlreadyExist,
                Messages.AdminSuccessCreation,
                1);
        }

        internal static async Task<string> DeleteAdmin(long userId, string message)
        {
            return await GeneralActions.DeleteSomething(
                userId, message,
                Database.Database.ReadAdminAsync,
                Database.Database.DeleteAdminAsync,
                Messages.AdminDoesntExist,
                Messages.AdminSuccessDelete);
        }


        internal static async Task<bool> CheckAdmins(long userId)
        {
            var adminId = await Database.Database.ReadAdminAsync(userId.ToString());
            return adminId != null;
        }
    }
}
