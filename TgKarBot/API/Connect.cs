using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using TgKarBot.Database;

namespace TgKarBot.API
{
    internal class Connect
    {
        internal ITelegramBotClient Bot = new TelegramBotClient(Constants.Tokens.TgToken);
        private MessagesHandler _messagesHandler = new MessagesHandler();

        internal void Start()
        {
            Console.WriteLine("Запущен бот " + Bot.GetMeAsync().Result.FirstName);
            StaticLogger.Logger.Info("Запуск бота");

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

            StaticLogger.Logger.Info("Бот успешно запущен");
            Console.ReadLine();
        }
    }
}
