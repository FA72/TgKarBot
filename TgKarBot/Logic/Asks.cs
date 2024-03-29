﻿using System;
using System.Configuration;
using System.Text;
using TgKarBot.Constants;
using TgKarBot.Database;
using TgKarBot.Logic.Helpers;

namespace TgKarBot.Logic
{
    internal class Asks
    {
        /// <summary>
        /// Принимает ответ от команды и проверяет его на правильность.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<string> CheckAsk(long userId, string message)
        {
            if (IsGameFinished())
            {
                return "Игра завершена( Больше ответы приниматься не будут";
            }

            var teamId = await Teams.CheckRegistration(userId);
            if (teamId == null)
                return Messages.NotRegistered;

            var startTimeString = await Database.Teams.ReadStartTimeAsync(teamId);
            if (startTimeString == null)
                return Messages.NotStarted;

            var awaitNext = await TeamsProgress.ReadLastDrinkTimeAsync(teamId);
            if (awaitNext.endDrinkTime == null)
            {
                var test = await TeamsProgress.ReadAllAsync(teamId);
                if (test.Count != 0)
                    return Messages.WriteNext;
            }

            var splittedMessage = message.Split();
            if (splittedMessage.Length < 3)
                return Messages.IncorrectInput + Commands.AskSample;

            var isEndGame = await Database.Teams.ReadEndStateAsync(teamId);
            if (isEndGame)
            {
                return Messages.AlreadyEnd;
            }

            var num = splittedMessage[1];

            if (await TeamsProgress.ReadAsync(teamId, num) != null)
                return $"{Messages.AlreadyAsked}\n\n{(await Rewards.ReadAsync(num)).Reward}";

            var ask = Parser.ParseBodyMessage(splittedMessage, 2);
            var correctAsk = await Database.Asks.ReadAsync(num);

            if (correctAsk == null) return Messages.IncorrectNum;

            if (!ask.Equals(correctAsk.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                await Database.Teams.AddPenaltyAsync(teamId);
                return Messages.NotCorrectAsk;
            }

            var output = new StringBuilder($"{Messages.Correct}\n");

            var (isWin, progress, maxAsks) = await Teams.SaveProgress(teamId, num);
            var reward = await Rewards.ReadAsync(num);

            if (!reward.IsMain) 
                await Database.Teams.UpdateBonusTimeAsync(teamId, (int) reward.TimeBonus);

            var (bonusTime, penalty) = await Database.Teams.ReadBonusTimeAndPenaltyAsync(teamId);
            var penaltyTime = penalty * Numbers.penaltyTime;

            if (isWin)
            {
                await Database.Teams.EndGame(teamId);
                var time = await GetTime(startTimeString, teamId, bonusTime, penaltyTime);
                var wingameMessage = $"{Messages.EndTheGame}{progress}/{maxAsks}\n{Messages.EndTheGameTime}{time} из них бонусные минуты - {bonusTime} и добавленные за неправильные ответы {penaltyTime} мин.";

                output.Append(wingameMessage);
            }
            else 
            {
                output.Append($"На данный момент отвечено на {progress} из {maxAsks} основных вопросов. Также заработано {bonusTime} минут бонусного времени.\n" +
                              $"Вы можете остановить время игры и выпить в баре (для этого введите команду /drink) или продолжить игру и увидеть следующее задание" +
                              $" (для этого введите команду /next. Обратите внимание, что после ввода команды /next вы не сможете остановить время до следующего правильного ответа.");
            }

            return output.ToString();
        }

        private static bool IsGameFinished()
        {
            return ConfigurationManager.AppSettings.Get("GameFinished") == "true";
        }

        public static async Task<string> Next(long userId)
        {
            var teamId = await Teams.CheckRegistration(userId);
            if (teamId == null)
                return Messages.NotRegistered;

            var time = await TeamsProgress.ReadLastDrinkTimeAsync(teamId);
            if (time.endDrinkTime != null)
            {
                return Messages.AlreadyContinued;
            }

            var num = await TeamsProgress.UpdateDrinkTimesAsync(teamId);
            var reward = await Rewards.ReadAsync(num);
            return $"{Messages.Reward}\n\n{reward.Reward}";
        }

        public static async Task<string> Drink(long userId)
        {
            var teamId = await Teams.CheckRegistration(userId);
            if (teamId == null)
                return Messages.NotRegistered;

            var time = await TeamsProgress.ReadLastDrinkTimeAsync(teamId);
            if (time.endDrinkTime != null)
            {
                return Messages.AlreadyContinued;
            }

            if (time.startDrinkTime != null)
            {
                return Messages.AlreadyStartDrink;
            }

            await TeamsProgress.StartDrinkAsync(teamId);
            return Messages.Drink;
        }

        public static async Task<string> GetTime(string startTimeString, string teamId, int? bonusTime, int? penaltyTime)
        {
            var lastTime = await TeamsProgress.ReadLastAskTimeAsync(teamId);
            var startTime = DateTime.Parse(startTimeString);
            var totalDrinkTime = await TeamsProgress.CalculateTotalDrinkTimeAsync(teamId);
            var time = (lastTime - startTime - totalDrinkTime - TimeSpan.FromMinutes((double)bonusTime) + TimeSpan.FromMinutes((double)penaltyTime)).ToString();
            return time;
        }
    }
}
