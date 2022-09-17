using System.Configuration;
using System.Collections.Specialized;
using System.Text;
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
                Database.Rewards.ReadAsync,
                Database.Rewards.CreateAsync,
                Messages.RewardAlreadyExist,
                Messages.RewardSuccessCreation,
                2);
        }

        internal static async Task<string> SetRewardType(long userId, string message)
        {
            if (!await Admins.CheckAdmins(userId)) return Messages.OnlyForAdmins;

            var splittedMessage = message.Split();
            var id = splittedMessage[1];
            if (await Database.Rewards.ReadAsync(id) == null)
                return Messages.RewardDoesntExist;

            var isMain = splittedMessage[2] != "0";
            int time;
            if (!isMain)
            {
                try
                {
                    time = int.Parse(splittedMessage[3]);
                }
                catch (Exception)
                {
                    return Messages.IncorrectInput + Commands.SetRewardTypeSample;
                }
                await Database.Rewards.UpdateTypeAsync(id, isMain, time);
            }
            else
                await Database.Rewards.UpdateTypeAsync(id, isMain);
            return Messages.RewardSuccessUpdateType;
        }

        internal static async Task<string> DeleteReward(long userId, string message)
        {
            return await GeneralActions.DeleteSomething(
                userId, message,
                Database.Rewards.ReadAsync,
                Database.Rewards.DeleteAsync,
                Messages.RewardDoesntExist,
                Messages.RewardSuccessDelete);
        }


        internal static async Task<bool> CheckAdmins(long userId)
        {
            var adminId = await Database.Admins.ReadAsync(userId.ToString());
            return adminId != null;
        }

        internal static async Task<string> GlobalStart(long userId, string message)
        {
            if (!await CheckAdmins(userId)) return Messages.OnlyForAdmins;

            var split = message.Split();
            if (split.Length > 1 && split[1] == "0")
            {
                ConfigurationManager.AppSettings.Set("GameStarted", "false");
                var text = new StringBuilder(Messages.AdminStopGame);

                if (split.Length <= 2) return text.ToString();

                for (var i = 2; i < split.Length; i++)
                {
                    text.Append($" {split}");
                }

                return text.ToString();
            }

            ConfigurationManager.AppSettings.Set("GameStarted", "true");
            return Messages.GameGlobalStart;
        }
    }
}
