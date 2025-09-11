using System.Configuration;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TgKarBot.API;

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
        var receiverOptions = new ReceiverOptions();

        var commands = new[]
        {
            new BotCommand { Command = "regteam", Description = "Зарегистрировать команду" },
            new BotCommand { Command = "startgame", Description = "Начать игру и отсчёт вашего времени" },
            new BotCommand { Command = "ask", Description = "Ввести ответ на вопрос. Ответы вводятся в формате /ask [номер вопроса] [ответ]." },
            new BotCommand { Command = "next", Description = "Следующая пара вопросов не появится до введения команды. Вы можете ввести эту команду до прохождения второго вопроса пары. В таком случае пауза будет снята, время пойдёт, но вы всё ещё сможете ответить на второй вопрос пары в будущем, но уже без паузы" },
            new BotCommand { Command = "progress", Description = "Узнать, на сколько вопросов вы уже ответили и какое зачётное время имеете" },
            new BotCommand { Command = "help", Description = "Список доступных команд" },
            new BotCommand { Command = "support", Description = "Связь с организатором. Команду вводите в формате /support [ваше сообщение]." },
        };

        await _bot.SetMyCommandsAsync(commands);


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