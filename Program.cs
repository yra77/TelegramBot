

using TelegramBot.Services.SearchHuman;
using TelegramBot.ConnectToTelegram;
using TelegramBot.Services.DataBase;
using TelegramBot.Services.Logs;
using TelegramBot.Helpers;

using System;
using System.Text;
using System.Threading.Tasks;


namespace TelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            CreateFolders.FoldersExist();//create folders if not
            FindHuman.InitializeStatic();//data to memory

            ILog log = new Log();
            IDataManager db = new DataManager(log);

            ConnectionTelegram connectionTelegram = new ConnectionTelegram(log);
 
            _=Task.Run(() =>
            {
                _ = connectionTelegram.Start_Async();
            });

            _ = Task.Run(() =>
            {
                AddData addData = new AddData(log, db);
            });

            PrintTable printTable = new PrintTable(db);
            printTable.Print_Table();

            Console.ReadLine();
        }

    }
}
