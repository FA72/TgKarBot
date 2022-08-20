using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgKarBot.Logic.Helpers
{
    internal class Parser
    {
        public static string ParseBodyMessage(string[] splittedMessage, int askPosition)
        {
            var sb = new StringBuilder(splittedMessage[askPosition]);
            for (var i = 3; i < splittedMessage.Length; i++)
                sb.Append(splittedMessage[i]);

            var ask = sb.ToString();
            return ask;
        }
    }
}
