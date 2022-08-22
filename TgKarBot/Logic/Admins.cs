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
                    Database.Asks.ReadAsync,
                    Database.Asks.CreateAsync,
                    Messages.AskAlreadyExist,
                    Messages.AskSuccessCreation,
                    2);
        }

        internal static async Task<string> DeleteAsk(long userId, string message)
        {
            return await GeneralActions.DeleteSomething(
                userId, message,
                Database.Asks.ReadAsync,
                Database.Asks.DeleteAsync,
                Messages.AskDoesntExist,
                Messages.AskSuccessDelete);
        }

        internal static async Task<string> AddAdmin(long userId, string message)
        {
            return await GeneralActions.AddSomething(
                userId, message,
                Database.Admins.ReadAsync,
                Database.Admins.CreateAsync,
                Messages.AdminAlreadyExist,
                Messages.AdminSuccessCreation,
                1);
        }

        internal static async Task<string> DeleteAdmin(long userId, string message)
        {
            return await GeneralActions.DeleteSomething(
                userId, message,
                Database.Admins.ReadAsync,
                Database.Admins.DeleteAsync,
                Messages.AdminDoesntExist,
                Messages.AdminSuccessDelete);
        }

        internal static async Task<string> AddReward(long userId, string message)
        {
            return await GeneralActions.AddSomething(
                userId, message,
                Database.Asks.ReadAsync,
                Database.Asks.CreateAsync,
                Messages.RewardAlreadyExist,
                Messages.RewardSuccessCreation,
                2);
        }

        internal static async Task<string> DeleteReward(long userId, string message)
        {
            return await GeneralActions.DeleteSomething(
                userId, message,
                Database.Asks.ReadAsync,
                Database.Asks.DeleteAsync,
                Messages.RewardDoesntExist,
                Messages.RewardSuccessDelete);
        }


        internal static async Task<bool> CheckAdmins(long userId)
        {
            var adminId = await Database.Admins.ReadAsync(userId.ToString());
            return adminId != null;
        }
    }
}
