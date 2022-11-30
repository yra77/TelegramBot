

using TelegramBot.Services.DataBase;
using TelegramBot.Services.SearchHuman;
using TelegramBot.Constatnts;
using TelegramBot.Services.Logs;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;

using System;
using System.Threading.Tasks;
using System.Threading;


namespace TelegramBot.ConnectToTelegram
{
    internal sealed class ConnectionTelegram
    {

        private readonly ILog _log;
        private readonly IFindHuman _findHuman;
        private readonly IDataManager _db;
        private readonly ITelegramBotClient _bot;


        public void Execute() 
        { 
            _=Task.Run(() => 
            { 
                _=Start_Async(); 
            }); 
        }

        public ConnectionTelegram(IDataManager db, 
                                  ILog log, 
                                  IFindHuman findHuman)
        {
            _log = log;
            _db = db;
            _bot = new TelegramBotClient(Constatnts.Constant.TOKEN_TELEGRAM);
            _findHuman = findHuman;
        }


        private async Task HandleUpdate_Async(ITelegramBotClient botClient,
                                              Update update,
                                              CancellationToken cancellationToken)
        {

            if (update.Message.From.IsBot)
            {
                await botClient.DeleteMessageAsync(update.Message.Chat.Id, update.Message.MessageId);
                return;
            }

            // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(update));

            if (update.Type == UpdateType.Message)
            {
                Message message = update.Message;

                Console.WriteLine(message.From.FirstName
                    + "\n" + message.From.Username
                    + "\n" + message.From.LastName
                    + "\n" + message.From.Id
                    + "\n" + message.Date
                    + "\n" + message.Location
                    + "\n" + message.Caption
                    + "\n" + message.Text
                    + "\n Is bot - " + message.From.IsBot);

                switch (message.Type)
                {
                    case MessageType.Text:
                        {
                            _ = Task.Run(async () =>
                            {
                                InputText inputText = new InputText();
                                await inputText.InputText_Async(botClient, _findHuman, _log, _db, message, update);
                            });
                            break;
                        }
                    case MessageType.Photo:
                        {
                            _ = Task.Run(async () =>
                            {
                                InputPhoto inputPhoto = new InputPhoto();
                                await inputPhoto.InputPhoto_Async(botClient, _findHuman, _log, _db, message, update);
                            });
                            break;
                        }
                    case MessageType.Location:
                        {
                            _ = Task.Run(async () =>
                            {
                                InputLocation inputLocation = new InputLocation();
                                await inputLocation.InputLocation_Async(botClient, _findHuman, _log, _db, message, update);
                            });
                            break;
                        }
                    default:
                        {
                            _ = Task.Run(async () =>
                            {
                                await botClient.DeleteMessageAsync(update.Message.Chat.Id, update.Message.MessageId);
                                await botClient.SendTextMessageAsync(message.From.Id, ConstantMessage.ERRORTYPE);
                            });
                            break;
                        }
                }
            }
        }

        private async Task<Task> HandleError_Async(ITelegramBotClient botClient,
                                                            Exception exception,
                                                            CancellationToken cancellationToken)
        {
            _log.logDelegate(this, "Error - " + System.Text.Json.JsonSerializer.Serialize(exception));

            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _log.logDelegate(this, ErrorMessage);

            return Task.CompletedTask;
        }

        private async Task Start_Async()
        {
            _log.logDelegate(this, "Телеграм бот " + (await _bot.GetMeAsync()).FirstName + " почав роботу");

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };

            _bot.StartReceiving(HandleUpdate_Async, HandleError_Async, receiverOptions, cancellationToken);

            Console.ReadLine();
        }
    }
}
