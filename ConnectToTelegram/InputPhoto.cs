

using TelegramBot.Constatnts;
using TelegramBot.Services.DataBase;
using TelegramBot.Helpers;
using TelegramBot.Services.Logs;
using TelegramBot.Services.SearchHuman;

using Telegram.Bot;
using Telegram.Bot.Types;

using System.Linq;
using System.Threading.Tasks;
using System.Globalization;


namespace TelegramBot.ConnectToTelegram
{
    internal class InputPhoto
    {

        public async Task InputPhoto_Async(ITelegramBotClient botClient,
                                           ILog _log,
                                           Message message,
                                           Update update)
        {

            MessagesVerification messagesVerification = new MessagesVerification();


            if (messagesVerification.VerifyText(message.Caption))
            {

                message.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(message.Caption.ToLower());//title upper
                
                string fileId = update.Message.Photo.Last().FileId;

                SaveToFile saveToFile = new SaveToFile();
                string? filePath = await saveToFile.SaveFoto_Async(botClient, fileId, _log);


                if (filePath != null && message.Caption != null)
                {

                    ConvertToProfileInfo convertToProfile = new ConvertToProfileInfo();
                    var profelInfo = convertToProfile.ToProfelInfo(message, filePath);

                    IFindHuman findHuman = new FindHuman();
                    string searchHuman = findHuman.SearchInList(message.Caption, _log);//checking human

                    await botClient.SendTextMessageAsync(message.From.Id, searchHuman);

                    _ = AddData.Add_DB_Queue.TryEnqueue(profelInfo);
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
