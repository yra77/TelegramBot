

using TelegramBot.Constatnts;

using Telegram.Bot;
using TelegramBot.Logs;

using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;


namespace TelegramBot.Helpers
{
    internal class SaveToFile
    {

        public static async Task<string> SaveFoto_Async(ITelegramBotClient botClient, string fileId, ILog log)
        {
            try
            {
                var fileInfo = await botClient.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;

                string destinationFilePath = ConstantFolders.FOLDERPATH_PHOTO + filePath.Split('\\','/').Last<string>();

                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                await botClient.DownloadFileAsync(
                    filePath: filePath,
                    destination: fileStream);

                return destinationFilePath;
            }
            catch(Exception e)
            {
                log.logDelegate(typeof(SaveToFile), "Error saving to file - " + e.Message);
                return null;
            }
        }

    }
}
