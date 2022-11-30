

using TelegramBot.Constatnts;
using TelegramBot.DataBase;
using TelegramBot.Helpers;
using TelegramBot.Logs;
using TelegramBot.SearchHuman;

using Telegram.Bot;
using Telegram.Bot.Types;

using System.Threading.Tasks;


namespace TelegramBot.ConnectToTelegram
{
    internal class InputText
    {

        public async Task InputText_Async(ITelegramBotClient botClient,
                                           IFindHuman _findHuman,
                                           ILog _log,
                                           IDataManager _db,
                                           Message message,
                                           Update update)
        {
            if (MessagesVerification.VerifyText(message.Text))
            {
                var profelInfo = ConvertToProfileInfo.ToProfelInfo(message);
                string searchHuman = _findHuman.SearchInList(message.Text, _log);//checking human

                await botClient.SendTextMessageAsync(message.From.Id, searchHuman);

                if (await _db.Write_Async(profelInfo) != 1)
                {
                    await botClient.SendTextMessageAsync(message.From.Id, ConstantMessage.ERRORSAVE);
                    _log.logDelegate(this, ConstantMessage.ERRORSAVE);
                }
            }
            else
            {
                await botClient.DeleteMessageAsync(update.Message.Chat.Id, update.Message.MessageId);
                await botClient.SendTextMessageAsync(message.From.Id, ConstantMessage.ERRORINPUT);
            }
        }

    }
}
