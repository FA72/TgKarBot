using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgKarBot.Constants;
using TgKarBot.Logic.Helpers;

namespace TgKarBot.API
{
    internal class MessagesHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            StaticLogger.Logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type != UpdateType.Message)
                return;
            
            var message = update?.Message;

            if (message?.Text == null) return;

            if (message.Chat.Type == ChatType.Group)
            {
                switch (message.Chat.Id)
                {
                    case Constants.ChatId.AdminChatId:
                        if (message.ReplyToMessage?.ForwardFrom == null || message.ReplyToMessage.From?.Id != botClient.BotId)
                            return;

                        await botClient.SendTextMessageAsync(message.ReplyToMessage.ForwardFrom.Id, $"Кар!\n{message.Text}");
                        StaticLogger.Logger.Info($"Ответили пользовалелю в ЛС. Текст: \"{message.Text}\".");
                        return;

                    default:
                        await botClient.SendTextMessageAsync(message.Chat.Id, Constants.Messages.Angry);
                        StaticLogger.Logger.Info("Попытка работы с ботом в группе");
                        return;
                }
            }

            if (message?.Entities != null)
            {
                foreach (var entity in message.Entities)
                {
                    if ((entity?.Type) == null || entity?.Type != MessageEntityType.BotCommand)
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
            try
            {
                List<string>? users;
                switch (command.ToLower())
                {
                    case Constants.Commands.Help:
                    case Constants.Commands.Start:
                        await botClient.SendTextMessageAsync(message.Chat, Constants.Messages.Start);
                        StaticLogger.Logger.Info($"{Constants.Commands.Start} is done");
                        break;
                    case Constants.Commands.RegTeam:
                        var splitMessage = message.Text.Split();
                        if (splitMessage.Length < 2)
                            text = Constants.Messages.IncorrectInput + Constants.Commands.RegTeamSample;
                        else text = await Logic.Teams.RegTeam(splitMessage[1], message.From.Id);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Попытка зарегистрировать команду: {text}");
                        break;
                    case Constants.Commands.StartGame:
                        text = await Logic.Teams.StartGame(message.From.Id);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Попытка начать игру: {text}");
                        break;
                    case Constants.Commands.Progress:
                        text = await Logic.Teams.Progress(message.From.Id);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Попытка начать игру: {text}");
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
                    case Constants.Commands.DeleteAsk:
                        text = await Logic.Admins.DeleteAsk(message.From.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Удалён правильный ответ: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.AddAdmin:
                        text = await Logic.Admins.AddAdmin(message.From.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Добавлен админ: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.DeleteAdmin:
                        text = await Logic.Admins.DeleteAdmin(message.From.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Удалён админ: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.AddReward:
                        text = await Logic.Admins.AddReward(message.From.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Добавлена награда за правильный ответ: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.SetRewardType:
                        text = await Logic.Admins.SetRewardType(message.From.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Изменён тип награды: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.DeleteReward:
                        text = await Logic.Admins.DeleteReward(message.From.Id, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat, text);
                        StaticLogger.Logger.Info($"Удалёна награда за правильный ответ: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.Support:
                        await botClient.ForwardMessageAsync(Constants.ChatId.AdminChatId, message.Chat.Id, message.MessageId);
                        StaticLogger.Logger.Info($"В чат направлен запрос на помощь. Сообщение: {message.Text}.");
                        break;
                    case Constants.Commands.Drink:
                        text = Logic.Asks.Drink(message.From.Id);
                        StaticLogger.Logger.Info($"Попытка выпить: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.Next:
                        text = Logic.Asks.Next(message.From.Id);
                        StaticLogger.Logger.Info($"Попытка продолжить игру: {message.Text}. Результат: {text}");
                        break;
                    case Constants.Commands.GlobalStart:
                        text = await Logic.Admins.GlobalStart(message.From.Id, message.Text);
                        users = await Database.Teams.ReadAllUsersId();
                        foreach (var userId in users)
                        {
                            try
                            {
                                await botClient.SendTextMessageAsync(userId, text);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                StaticLogger.Logger.Info($"Ошибка при отправки сообщения для {userId}. Текст ошибки: {e}.");
                                throw;
                            }
                        }
                        StaticLogger.Logger.Info($"Сообщение отправлено всем зарегистрированным пользователям. Сообщение: {message.Text}.");
                        break;
                    case Constants.Commands.GlobalFinish:
                        text = await Logic.Admins.GlobalFinish(message.From.Id, message.Text);
                        users = await Database.Teams.ReadAllUsersId();
                        foreach (var userId in users)
                        {
                            try
                            {
                                await botClient.SendTextMessageAsync(userId, text);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                StaticLogger.Logger.Info($"Ошибка при отправки сообщения для {userId}. Текст ошибки: {e}.");
                                throw;
                            }
                        }
                        StaticLogger.Logger.Info($"Сообщение отправлено всем зарегистрированным пользователям. Сообщение: {message.Text}.");
                        break;
                    case Constants.Commands.ToAll:
                        if (!await Logic.Admins.CheckAdmins(message.From.Id)) return;
                        text = message.Text.Substring(7);
                        users = await Database.Teams.ReadAllUsersId();
                        foreach (var userId in users)
                        {
                            try
                            {
                                await botClient.SendTextMessageAsync(userId, text);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                StaticLogger.Logger.Info($"Ошибка при отправки сообщения для {userId}. Текст ошибки: {e}.");
                                throw;
                            }
                        }
                        StaticLogger.Logger.Info($"Сообщение отправлено всем зарегистрированным пользователям. Сообщение: {message.Text}.");
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
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            StaticLogger.Logger.Error(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}
