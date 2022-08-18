using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.ComponentModel.DataAnnotations;

namespace TgKarBot.API
{
    internal class MessagesHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
                return;
            
            var message = update?.Message;

            if (message?.Text == null) return;

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
                    break;
                default:
                    await botClient.SendTextMessageAsync(message.Chat, Constants.Messages.Default);
                    break;
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}
