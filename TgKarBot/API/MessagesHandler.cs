using Microsoft.VisualBasic;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgKarBot.API
{
    internal class MessagesHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            StaticLogger.Logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
                return;
            
            var message = update?.Message;

            if (message?.Text == null) return;

            if (message.From.Id < 0)
            {
                await botClient.SendTextMessageAsync(message.Chat, Constants.Messages.Angry);
                StaticLogger.Logger.Info("Добавили в группу");
                return;
            }

            if (message?.Entities != null)
            {
                foreach (var entity in message.Entities)
                {
                    if ((entity?.Type) == null || entity?.Type != Telegram.Bot.Types.Enums.MessageEntityType.BotCommand)
                        continue;

                    string command = message.Text.Substring(entity.Offset, entity.Length);
                    await CommandSwitcher(botClient, message, command);
                    return;
                }
            }

            var firstWord = message.Text.Split()[0];
            var commandSynonimList = Constants.Commands.Synonims.FirstOrDefault(x => x.Contains(firstWord));
            if (commandSynonimList != null)
                await CommandSwitcher(botClient, message, commandSynonimList[0]);
        }

        private static async Task CommandSwitcher(ITelegramBotClient botClient, Message? message, string command)
        {
            switch (command.ToLower())
            {
                case Constants.Commands.Start:
                    await botClient.SendTextMessageAsync(message.Chat, Constants.Messages.Start);
                    StaticLogger.Logger.Info(Constants.Commands.Start + " is done");
                    break;
                case Constants.Commands.RegTeam:
                    var text = Logic.Teams.Registrate(message.Text.Split()[1], message.From.Id);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info("Попытка зарегистрировать команду: " + text);
                    break;
                default:
                    await botClient.SendTextMessageAsync(message.Chat, Constants.Messages.Default);
                    StaticLogger.Logger.Info("Default message is sended");
                    break;
            }

        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            StaticLogger.Logger.Info(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}
