

using System.IO;


namespace TelegramBot.Constatnts
{
    internal class ConstantFolders
    {
        public static readonly string BASE_FOLDER = Directory.GetCurrentDirectory();
        public static readonly string SqLite_FOLDER = Directory.GetCurrentDirectory() + "\\DataBase\\";
        public static readonly string LOGS_FOLDER = BASE_FOLDER + "\\Logs\\";
        public static readonly string FOLDERPATH_PHOTO = SqLite_FOLDER + "DownloadPhotos\\";
        public static readonly string PATHTOHUMAN = BASE_FOLDER + "\\Humans.txt";
    }
}
