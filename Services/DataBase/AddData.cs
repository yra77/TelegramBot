

using TelegramBot.Models;
using TelegramBot.Services.Logs;
using TelegramBot.Constatnts;

using Microsoft.VisualStudio.Threading;
using System.Threading.Tasks;


namespace TelegramBot.Services.DataBase
{
    internal class AddData
    {

        public static AsyncQueue<ProfileInfo> Add_DB_Queue;

        private readonly ILog _log;
        private readonly IDataManager _db;


        public AddData(ILog log, IDataManager db)
        {
            _log = log;
            _db = db;
            Add_DB_Queue = new AsyncQueue<ProfileInfo>();

            _ = Task.Run(() =>
            {
                _ = Start_Async();
            });
        }


        private async Task<int> Add_Async(ProfileInfo profelInfo)
        {
           return await _db.Add_Async(profelInfo);
        }

        private async Task Start_Async()
        {
            while (true)
            {

                if (Add_DB_Queue.IsEmpty)
                {
                    await Task.Delay(1500);
                }
                else 
                {
                    Add_DB_Queue.TryDequeue(out ProfileInfo profelInfo);
                    
                    if (await Add_Async(profelInfo) != 1)
                    {
                            _log.logDelegate(this, ConstantMessage.ERRORSAVE);
                            Add_DB_Queue.TryEnqueue(profelInfo);
                    }
                }
                    await Task.Delay(1000);
            };
        }

    }
}
