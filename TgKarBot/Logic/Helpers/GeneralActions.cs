using TgKarBot.Constants;

namespace TgKarBot.Logic.Helpers
{
    internal class GeneralActions
    {
        internal static async Task<string> AddSomething<T>(
            long userId,
            string message,
            Func<string, Task<T>> readFunc,
            Func<string, string, Task> writeFunc,
            string alreadyExistMessage,
            string successMessage,
            int numberOfArguments
            )
        {
            if (!await Admins.CheckAdmins(userId)) return Messages.OnlyForAdmins;

            var splittedMessage = message.Split();
            var id = splittedMessage[1];
            if (await readFunc(id) != null)
                return alreadyExistMessage;

            var value = Parser.ParseBodyMessage(splittedMessage, numberOfArguments);
            await writeFunc(id, value);

            return successMessage;
        }

        internal static async Task<string> DeleteSomething<T>(
            long userId,
            string message,
            Func<string, Task<T>> readFunc,
            Func<string, Task> deleteFunc,
            string doesntExistMessage,
            string successMessage
        )
        {
            if (!await Admins.CheckAdmins(userId)) return Messages.OnlyForAdmins;

            var splittedMessage = message.Split();
            var id = splittedMessage[1];
            if (await readFunc(id) == null)
                return doesntExistMessage;

            await deleteFunc(id);

            return successMessage;
        }
    }
}
