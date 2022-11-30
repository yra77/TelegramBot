

using TelegramBot.Constatnts;
using TelegramBot.DataBase;
using TelegramBot.Helpers;
using TelegramBot.Logs;
using TelegramBot.SearchHuman;

using Telegram.Bot;
using Telegram.Bot.Types;

using System.Linq;
using System.Threading.Tasks;


namespace TelegramBot.ConnectToTelegram
{
    internal class InputPhoto
    {

        public async Task InputPhoto_Async(ITelegramBotClient botClient,
                                           IFindHuman _findHuman,
                                           ILog _log,
                                           IDataManager _db,
                                           Message message,
                                           Update update)
        {
            if (MessagesVerification.VerifyText(message.Caption))
            {
                string fileId = update.Message.Photo.Last().FileId;
                string? filePath = await SaveToFile.SaveFoto_Async(botClient, fileId, _log);

                if (filePath != null && message.Caption != null)
                {
                    var profelInfo = ConvertToProfileInfo.ToProfelInfo(message, filePath);
                    string searchHuman = _findHuman.SearchInList(message.Caption, _log);//checking human

                    await botClient.SendTextMessageAsync(message.From.Id, searchHuman);

                    if (await _db.Write_Async(profelInfo) != 1)
                    {
                        await botClient.SendTextMessageAsync(message.From.Id, ConstantMessage.ERRORSAVE);
                        _log.logDelegate(this, ConstantMessage.ERRORSAVE);
                    }
                }
                else
                {
                    await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    await botClient.SendTextMessageAsync(message.From.Id, ConstantMessage.ERRORPHOTO);
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
