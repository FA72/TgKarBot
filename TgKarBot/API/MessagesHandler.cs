using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgKarBot.Constants;
using TgKarBot.Logic;
using ChatId = TgKarBot.Constants.ChatId;

namespace TgKarBot.API;

internal class MessagesHandler
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        StaticLogger.Logger.Debug(JsonConvert.SerializeObject(update));
        Console.WriteLine(JsonConvert.SerializeObject(update));

        if (update.Type != UpdateType.Message) return;
            
        var message = update.Message;

        if (message?.Text == null) return;

        if (message.Chat.Type == ChatType.Group)
        {
            switch (message.Chat.Id)
            {
                case ChatId.AdminChatId:
                    if (message.ReplyToMessage?.ForwardFrom == null || message.ReplyToMessage.From?.Id != botClient.BotId)
                        return;

                    await botClient.SendTextMessageAsync(message.ReplyToMessage.ForwardFrom.Id, $"Кар!\n{message.Text}", cancellationToken: cancellationToken);
                    StaticLogger.Logger.Info($"Ответили пользовалелю в ЛС. Текст: \"{message.Text}\".");
                    return;

                default:
                    await botClient.SendTextMessageAsync(message.Chat.Id, Messages.Angry, cancellationToken: cancellationToken);
                    StaticLogger.Logger.Info("Попытка работы с ботом в группе");
                    return;
            }
        }

        if (message.Entities != null)
        {
            foreach (var entity in message.Entities)
            {
                if (entity.Type is not MessageEntityType.BotCommand)
                    continue;

                var command = message.Text.Substring(entity.Offset, entity.Length);
                await CommandSwitcher(botClient, message, command);
                return;
            }
        }

        var firstWord = message.Text.Split()[0];
        var commandSynonimList = Commands.Synonims.FirstOrDefault(x => x.Contains(firstWord));
        if (commandSynonimList != null)
            await CommandSwitcher(botClient, message, commandSynonimList[0]);
    }

    private static async Task CommandSwitcher(ITelegramBotClient botClient, Message? message, string command)
    {
        if (message?.Text == null)
        {
            Console.WriteLine("Сообщение или его текст пусты");
            return;
        }

        try
        {
            List<string>? users;
            string text;
            switch (command.ToLower())
            {
                case Commands.Help:
                case Commands.Start:
                    await botClient.SendTextMessageAsync(message.Chat, Messages.Start);
                    StaticLogger.Logger.Info($"{Commands.Start} is done");
                    break;
                case Commands.RegTeam:
                    var splitMessage = message.Text.Split();
                    if (splitMessage.Length < 2)
                        text = Messages.IncorrectInput + Commands.RegTeamSample;
                    else text = await Teams.RegTeam(splitMessage[1], message.From!.Id);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Попытка зарегистрировать команду: {text}");
                    break;
                case Commands.StartGame:
                    text = await Teams.StartGame(message.From!.Id);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Попытка начать игру: {text}");
                    break;
                case Commands.Progress:
                    text = await Teams.Progress(message.From!.Id);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Попытка начать игру: {text}");
                    break;
                case Commands.Ask:
                    text = await Asks.CheckAsk(message.From!.Id, message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Попытка ответить: {message.Text}. Результат: {text}");
                    break;
                case Commands.AddAsk:
                    text = await Admins.AddAsk(message.From!.Id, message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Добавлен правильный ответ: {message.Text}. Результат: {text}");
                    break;
                case Commands.DeleteAsk:
                    text = await Admins.DeleteAsk(message.From!.Id, message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Удалён правильный ответ: {message.Text}. Результат: {text}");
                    break;
                case Commands.AddAdmin:
                    text = await Admins.AddAdmin(message.From!.Id, message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Добавлен админ: {message.Text}. Результат: {text}");
                    break;
                case Commands.DeleteAdmin:
                    text = await Admins.DeleteAdmin(message.From!.Id, message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Удалён админ: {message.Text}. Результат: {text}");
                    break;
                case Commands.AddReward:
                    text = await Admins.AddReward(message.From!.Id, message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Добавлена награда за правильный ответ: {message.Text}. Результат: {text}");
                    break;
                case Commands.SetRewardType:
                    text = await Admins.SetRewardType(message.From!.Id, message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Изменён тип награды: {message.Text}. Результат: {text}");
                    break;
                case Commands.DeleteReward:
                    text = await Admins.DeleteReward(message.From!.Id, message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Удалёна награда за правильный ответ: {message.Text}. Результат: {text}");
                    break;
                case Commands.Support:
                    await botClient.ForwardMessageAsync(ChatId.AdminChatId, message.Chat.Id, message.MessageId);
                    StaticLogger.Logger.Info($"В чат направлен запрос на помощь. Сообщение: {message.Text}.");
                    break;
                case Commands.Next:
                    text = await Asks.Next(message.From!.Id);
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    StaticLogger.Logger.Info($"Попытка продолжить игру: {message.Text}. Результат: {text}");
                    break;
                case Commands.GlobalStart:
                    text = await Admins.GlobalStart(message.From!.Id, message.Text);
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
                case Commands.GlobalFinish:
                    text = await Admins.GlobalFinish(message.From!.Id, message.Text);
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
                case Commands.ToAll:
                    if (!await Admins.CheckAdmins(message.From!.Id)) return;
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
                    await botClient.SendTextMessageAsync(message.Chat, Messages.Default);
                    StaticLogger.Logger.Info("Default message is sended");
                    break;
            }   
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            StaticLogger.Logger.Error(e);
            await botClient.SendTextMessageAsync(message.Chat, Messages.Error);
        }
    }

    public void HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        StaticLogger.Logger.Error(JsonConvert.SerializeObject(exception));
        Console.WriteLine(JsonConvert.SerializeObject(exception));
    }
}