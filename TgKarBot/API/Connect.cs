using System.Configuration;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace TgKarBot.API
{
    internal class Connect
    {
        private ITelegramBotClient _bot = new TelegramBotClient(token: ConfigurationManager.AppSettings.Get("TgToken"));
        private MessagesHandler _messagesHandler = new MessagesHandler();

        internal async Task StartAsync()
        {
            var me = await _bot.GetMeAsync();
            Console.WriteLine("Запущен бот " + me.FirstName);
            StaticLogger.Logger.Info("Запуск бота");

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };

            _bot.StartReceiving(
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
