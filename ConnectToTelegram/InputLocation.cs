

using TelegramBot.SearchHuman;
using TelegramBot.Constatnts;
using TelegramBot.DataBase;
using TelegramBot.Helpers;
using TelegramBot.Logs;

using Telegram.Bot;
using Telegram.Bot.Types;

using System.Threading.Tasks;


namespace TelegramBot.ConnectToTelegram
{
    internal class InputLocation
    {

        public async Task InputLocation_Async(ITelegramBotClient botClient,
                                           IFindHuman _findHuman,
                                           ILog _log,
                                           IDataManager _db,
                                           Message message,
                                           Update update)
        {
            var profelInfo = ConvertToProfileInfo.ToProfelInfo(message);

            if (await _db.Write_Async(profelInfo) != 1)
            {
                await botClient.SendTextMessageAsync(message.From.Id, ConstantMessage.ERRORSAVE);
                _log.logDelegate(this, ConstantMessage.ERRORSAVE);
            }
        }
    }
}
