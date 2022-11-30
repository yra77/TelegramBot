

using System.IO;


namespace TelegramBot.Constatnts
{
    internal class ConstantFolders
    {
        public static readonly string BASE_FOLDER = Directory.GetCurrentDirectory();
        public static readonly string LOGS_FOLDER = Directory.GetCurrentDirectory() + "\\Logs\\";
        public static readonly string FOLDERPATH_PHOTO = Directory.GetCurrentDirectory() + "\\" + "DownloadPhotos\\";

        public const string PATHTOHUMAN = "C:\\Users\\User\\source\\repos\\TelegramBot\\SearchHuman\\Humans.txt";
    }
}
