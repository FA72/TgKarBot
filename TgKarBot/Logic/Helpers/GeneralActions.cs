using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgKarBot.Constants;

namespace TgKarBot.Logic.Helpers
{
    internal class GeneralActions
    {
        internal static async Task<string> AddSomething(
            long userId,
            string message,
            Func<string, Task<string?>> readFunc,
            Func<string, string, Task> writeFunc,
            string alreadyExistMessage,
            string successMessage
            )
        {
            if (!await Admins.CheckAdmins(userId)) return Messages.OnlyForAdmins;

            var splittedMessage = message.Split();
            var num = splittedMessage[1];
            if (await readFunc(num) != null)
                return alreadyExistMessage;

            var ask = Parser.ParseBodyMessage(splittedMessage, 2);
            await writeFunc(num, ask);

            return successMessage;
        }

        internal static async Task<string> DeleteSomething(
            long userId,
            string message,
            Func<string, Task<string?>> readFunc,
            Func<string, Task> deleteFunc,
            string doesntExistMessage,
            string successMessage
        )
        {
            if (!await Admins.CheckAdmins(userId)) return Messages.OnlyForAdmins;

            var splittedMessage = message.Split();
            var num = splittedMessage[1];
            if (await readFunc(num) == null)
                return doesntExistMessage;

            var ask = Parser.ParseBodyMessage(splittedMessage, 2);
            await deleteFunc(num);

            return successMessage;
        }
    }
}
