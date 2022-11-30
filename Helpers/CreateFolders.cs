

using TelegramBot.Constatnts;


namespace TelegramBot.Helpers
{
    internal class CreateFolders
    {
        public static void FoldersExist()
        {
            if (!System.IO.Directory.Exists(ConstantFolders.FOLDERPATH_PHOTO) || 
                !System.IO.Directory.Exists(ConstantFolders.LOGS_FOLDER))
            {
                System.IO.Directory.CreateDirectory(ConstantFolders.FOLDERPATH_PHOTO);
                System.IO.Directory.CreateDirectory(ConstantFolders.LOGS_FOLDER);
            }
        }
    }
}
