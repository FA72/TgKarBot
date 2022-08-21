using System.Text;

namespace TgKarBot.Logic.Helpers
{
    internal class Parser
    {
        public static string ParseBodyMessage(string[] splittedMessage, int askPosition)
        {
            var sb = new StringBuilder(splittedMessage[askPosition]);
            for (var i = askPosition + 1; i < splittedMessage.Length; i++)
                sb.Append(splittedMessage[i]);

            var ask = sb.ToString();
            return ask;
        }
    }
}
