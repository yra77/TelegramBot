
using TelegramBot.ConnectToTelegram;
using TelegramBot.DataBase;
using TelegramBot.Helpers;
using TelegramBot.Logs;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Threading.Tasks;
using System.Text;


namespace TelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            CreateFolders.FoldersExist();

            ILog log = new Log();

            IDataManager db = new DataManager(log);

            _=Task.Run(() =>
            {
                ConnectionTelegram myBot = new ConnectionTelegram(db, log);
                _=myBot.Start_Async();
            });

            Console.ReadLine();
        }
    }
}
