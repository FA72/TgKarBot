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
            var id = splittedMessage[1];
            if (await readFunc(id) != null)
                return alreadyExistMessage;

            var value = Parser.ParseBodyMessage(splittedMessage, 1);
            await writeFunc(id, value);

            return successMessage;
        }
    }
}
