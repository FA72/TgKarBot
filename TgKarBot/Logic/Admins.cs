﻿using TgKarBot.Constants;
using TgKarBot.Logic.Helpers;

namespace TgKarBot.Logic
{
    internal class Admins
    {
        internal static async Task<string> AddTask(long userId, string message)
        {
            return await GeneralActions.AddSomething(
                    userId, message,
                    Database.Database.ReadAskAsync,
                    Database.Database.CreateAskAsync,
                    Messages.TaskAlreadyExist,
                    Messages.TaskSuccessCreation,
                    2);
        }

        internal static async Task<string> DeleteTask(long userId, string message)
        {
            return await GeneralActions.DeleteSomething(
                    userId, message,
                    Database.Database.ReadAskAsync,
                    Database.Database.DeleteAskAsync,
                    Messages.TaskDoesntExist,
                    Messages.TaskSuccessDelete);
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
