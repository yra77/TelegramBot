

using TelegramBot.Services.DataBase;
using TelegramBot.Helpers;
using TelegramBot.Services.Logs;

using Telegram.Bot;
using Telegram.Bot.Types;

using System.Threading.Tasks;


namespace TelegramBot.ConnectToTelegram
{
    internal class InputLocation
    {

        public async Task InputLocation_Async(ITelegramBotClient botClient,
                                           ILog _log,
                                           Message message,
                                           Update update)
        {
            ConvertToProfileInfo convertToProfile = new ConvertToProfileInfo();
            var profelInfo = convertToProfile.ToProfelInfo(message);

            _ = AddData.Add_DB_Queue.TryEnqueue(profelInfo);
        }

    }
}
