﻿namespace TgKarBot.Constants
{
    internal class Messages
    {
        internal const string AlreadyAsked = "Вы уже корректно ответили на этот вопрос. Напоминаем награду:";
        internal const string AlreadyContinued = "Вы уже продолжили игру";
        internal const string AlreadyRegistered = "Вы уже зарегистрированы на игру. Чтобы начать дождитесть 19:30 пятницы, после чего можете начать при помощи команды /startgame";
        internal const string AlreadyStarted = "Вы уже начали игру";
        internal const string AlreadyEnd = "Вы уже закончили игру и не можете более отвечать на бонусные вопросы";
        internal const string Angry = "КАР! Я НЕ МОГУ РАБОТАТЬ В ТАКИХ УСЛОВИЯХ, ВАС СЛИШКОМ МНОГО! (бот не поддерживает групповые чаты)";
        internal const string AdminAlreadyExist = "Этот пользователь уже админ";
        internal const string AdminDoesntExist = "Этот пользователь не админ";
        internal const string AdminStopGame = "Администратор остановил игру.";
        internal const string AdminSuccessCreation = "Админ успешно добавлен";
        internal const string AdminSuccessDelete = "Админ успешно удалён";
        internal const string AskAlreadyExist = "Под этим номером ответ уже существует";
        internal const string AskDoesntExist = "Под этим номером ответа не существует";
        internal const string AskSuccessCreation = "Ответ успешно добавлен";
        internal const string AskSuccessDelete = "Ответ успешно удален";
        internal const string Correct = "Правильный ответ.";
        internal const string Default = "Кар! Если ты видишь это сообщение, значит что-то пошло не так или ты набрал не ту команду";
        internal const string Drink = "Ваше время приостановлено, можете спокойно посидеть в баре. Для продолжения игры и получения следующего задания введите /next";
        internal const string Error = "Произошла ошибка в работе бота, пожалуйста, свяжитесь с администрацией через команду /support";
        internal const string EndTheGame = "Поздравляем вас! Вы закончили игру! Вы ответили на ";
        internal const string EndTheGameTime = "Ваше время прохождения с учётом бонусов: ";
        internal const string FinishGame = "Время игры истекло! Результаты зафиксированы. Больше ответы приниматься не будут.";
        internal const string DoneTeamRegisteration = "Ваша команда успешно зарегистрирована и закреплена за этим пользователем." +
                                                      "Чтобы начать дождитесть 19:30 пятницы, после чего можете начать при помощи команды /startgame";
        internal const string GameIsNotStarted = "Игра не начата, пожалуйста, дождитесь объявления о начале игры";
        internal const string GameGlobalStart = "Всем привет! Игра начинается, вы можете ввести команду /startgame для начала игры. " +
                                          "После этого вы получите задания и начнётся отсчёт времени. Поэтому, рекомендуем вам начинать у метро Сенная.";
        internal const string GameStart = "Вы начали игру! С текущего момента начался отсчёт времени. Время старта: ";
        internal const string GameStartTasks = "На вопросы можно отвечать в любом порядке. Ваши первые пять заданий:";
        internal const string IncorrectInput = "Неправильно введённые данные, пожалуйста, введите команду по шаблону: ";
        internal const string IncorrectNum = "Неверный номер вопроса";
        internal const string NotCorrectAsk = "Ответ неверный. За это вам добавлены 3 минуты к итоговому времени";
        internal const string NotRegistered = "Вы не зарегистрированы, как участник, зарегистрируйте," +
                                              " пожалуйста, вашу команду при помощи команды /regteam или обратитесь к администрации через команду /support";
        internal const string NotStarted = "Вы не начали игру. Введите, пожалуйста, команду /startgame";
        internal const string OnlyForAdmins = "Эта команда только для админов";
        internal const string OtherUser = "Ваша команда уже зарегистрирована на игру, если хотите переместить" +
                                          " регистрацию на другое устройство, обратитесь к администрации через команду /support";
        internal const string Reward = "За правильный ответ вам полагается: ";
        internal const string RewardAlreadyExist = "Под этим номером награда уже существует";
        internal const string RewardDoesntExist = "Под этим номером награда не существует";
        internal const string RewardSuccessCreation = "Награда успешно создана";
        internal const string RewardSuccessUpdateType = "Награда успешно обновлена";
        internal const string RewardSuccessDelete = "Награда успешно удалена";
        internal const string Start = $"Кар! \n" +
                                      // Todo актуализация информации
                                      $"Квест \"Pub Crow\" начинается 22.09.2023 в 19:00 у Невского проспекта. В Екатерининском сквере. Время вашего старта считается с ввода команды /startgame. Отыгрыш продлится до 23:59 23.09.2023. Обратите внимание, что многие бары работают до 23:00. После времени окончания квеста ответы приниматься не будут.\r\n\r\n" +
                                      $"Впереди 13 основных вопросов и несколько бонусных заданий. Основные вопросы можно брать в любом порядке. Бонусные задания — в любом порядке до взятия тринадцатого по счёту основного. \r\n\r\n" +
                                      $"Результат игры считается по количеству взятых основных вопросов с дополнительным показателем по времени. Победит команда, ответившая на наибольшее количество вопросов за наименьшее время.\r\n\r\n" +
                                      $"После команды /startgame вы получите пять основных вопросов. Далее, по мере взятия, — новые вопросы, основные и бонусные.\r\n\r\n" +
                                      $"Бонусные вопросы не учитываются в подсчёте очков, но дают награду в виде времени. Оно будет отнято от итогового.\r\n\r\n" +
                                      $"Каждый неправильный ответ даёт +3 минуты к итоговому времени. После ввода неправильного ответа можно сразу же ввести правильный.\r\n\r\n" +
                                      $"Напоминаем вам, что квест пешеходный, а это значит, что по ходу прохождения квеста нельзя пользоваться любым транспортом, включая велосипеды и самокаты.\r\n\r\n" +
                                      $"В ходе квеста вам понадобятся следующие команды:\r\n\r\n" +
                                      $"/regteam — зарегистрировать команду. Перед тем, как начать игру, введите эту команду в формате /regteam [id команды]. Пример:" +
                                      $"\r\n/regteam 45554\r\n\r\n" +
                                      $"/startgame — начать игру и отсчёт вашего времени. Ввести команду можно после 19:30 пятницы (23.09). Прежде чем вводить команду, проверьте, что вы находитесь у метро Сенная площадь и помните, что остановить таймер нельзя.\r\n\r\n" +
                                      $"/ask — ввести ответ на вопрос. Ответы вводятся в формате ask [номер вопроса] [ответ]. Примеры:\r\n" +
                                      $"/ask 0 nevermore\r\n" +
                                      $"/ask бонус0 кар\r\n\r\n" +
                                      $"/drink — после правильного ответа вы можете выпить в баре. После ввода правильного ответа введите эту команду и таймер остановится (до ввода этой команды таймер идёт).\r\n\r\n" +
                                      $"/next — Продолжить игру после правильного ответа или остановки на выпить (после ввода этой команды, таймер возобновляется)\r\n\r\n" +
                                      $"/progress — узнать, на сколько вопросов вы уже ответили и какое зачётное время имеете.\r\n\r\n" +
                                      $"/help — список доступных команд.\r\n\r\n" +
                                      $"/support — связь с организатором. Команду вводите в формате /support [ваше сообщение]. Пример:\r\n" +
                                      $"/support Что каркнул ворон?\r\n\r\n" +
                                      $"Готовы начинать? Регистрируйте команду при помощи /regteam и вперёд!";
        internal const string WriteNext = $"Чтобы продолжить играть, введите /next";
    }
}
