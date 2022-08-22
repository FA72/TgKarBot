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
            string text;
            await Database.Database.Connect();
            try
            {
                switch (command.ToLower())
                {
                    case Constants.Commands.Start:
                        await botClient.SendTextMessageAsync(message.Chat, Constants.Messages.Start);
                        StaticLogger.Logger.Info(Constants.Commands.Start + " is done");
                        break;
                    case Constants.Commands.RegTeam:
                        var splitMessage = message.Text.Split();
                        if (splitMessage.Length < 2)
                            text = Constants.Messages.IncorrectInput + Constants.Commands.RegTeamSample;
                        else text = await Logic.Teams.RegTeam(splitMessage[1], message.From.Id);

                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info("Попытка зарегистрировать команду: " + text);
                        break;
                    case Constants.Commands.Ask:
                        text = await Logic.Asks.CheckAsk(message.From.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Попытка ответить: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.AddAsk:
                        text = await Logic.Admins.AddAsk(message.From.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Добавлен правильный ответ: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.AddAdmin:
                        text = await Logic.Admins.AddAdmin(message.From.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Добавлен админ: {message.Text}. Результат: {text}");
                        break;
                    default:
                        await botClient.SendTextMessageAsync(message.Chat, Constants.Messages.Default);
                        StaticLogger.Logger.Info("Default message is sended");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StaticLogger.Logger.Error(e);
                await botClient.SendTextMessageAsync(message.Chat, Constants.Messages.Error);
            }
            await Database.Database.Disconnect();
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            StaticLogger.Logger.Info(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}
