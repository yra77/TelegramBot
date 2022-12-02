

using TelegramBot.Constatnts;
using TelegramBot.Services.DataBase;
using TelegramBot.Helpers;
using TelegramBot.Services.Logs;
using TelegramBot.Services.SearchHuman;

using Telegram.Bot;
using Telegram.Bot.Types;

using System.Threading.Tasks;
using System.Globalization;


namespace TelegramBot.ConnectToTelegram
{
    internal class InputText
    {

        public async Task InputText_Async(ITelegramBotClient botClient,
                                           ILog _log,
                                           Message message,
                                           Update update)
        {

            MessagesVerification messagesVerification = new MessagesVerification();


            if (messagesVerification.VerifyText(message.Text))
            {

                message.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(message.Text.ToLower());

                ConvertToProfileInfo convertToProfile = new ConvertToProfileInfo();
                var profelInfo = convertToProfile.ToProfelInfo(message);

                IFindHuman findHuman = new FindHuman();
                string searchHuman = findHuman.SearchInList(message.Text, _log);//checking human

                await botClient.SendTextMessageAsync(message.From.Id, searchHuman);

                _ = AddData.Add_DB_Queue.TryEnqueue(profelInfo);
            }
            else
            {
                await botClient.DeleteMessageAsync(update.Message.Chat.Id, update.Message.MessageId);
                await botClient.SendTextMessageAsync(message.From.Id, ConstantMessage.ERRORINPUT);
            }
        }

    }
}
