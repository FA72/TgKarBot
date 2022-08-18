using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TgKarBot.API
{
    internal class Connect
    {
        internal ITelegramBotClient Bot = new TelegramBotClient(Constants.Tokens.TgToken);
        private MessagesHandler _messagesHandler = new MessagesHandler();

        internal void Start()
        {
            Console.WriteLine("Запущен бот " + Bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };

            Bot.StartReceiving(
                _messagesHandler.HandleUpdateAsync,
                _messagesHandler.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.ReadLine();
        }
    }
}
