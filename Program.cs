
using TelegramBot.ConnectToTelegram;
using TelegramBot.Services.DataBase;
using TelegramBot.Helpers;
using TelegramBot.Services.Logs;
using TelegramBot.Services.SearchHuman;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Text;


namespace TelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            CreateFolders.FoldersExist();

            var services = new ServiceCollection();
            ConfigureServices(services);
            services
                .AddSingleton<ConnectionTelegram, ConnectionTelegram>()
                .BuildServiceProvider()
                .GetService<ConnectionTelegram>()
                .Execute();
          
            Console.ReadLine();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<ILog, Log>()
                .AddSingleton<IFindHuman, FindHuman>()
            .AddSingleton<IDataManager, DataManager>();
        }

    }
}
