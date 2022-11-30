

using TelegramBot.Models;


namespace TelegramBot.Helpers
{
    internal class ConvertToProfileInfo
    {
        public static ProfileInfo ToProfelInfo(Telegram.Bot.Types.Message message, string pathToPhoto = null)
        {
            ProfileInfo profileInfo = new ProfileInfo()
            {
                FirstName = message.From.FirstName,
                UserName = message.From.Username,
                LastName = message.From.LastName,
                IdClient = message.From.Id,
                DateTime = message.Date,
                Location = (message.Location != null)?(message.Location.Latitude + ";" + message.Location.Longitude):null,
                Text = message.Text ?? message.Caption,
                PathToPhoto = pathToPhoto,
                IsBot = message.From.IsBot
            };

            return profileInfo;
        }
    }
}
