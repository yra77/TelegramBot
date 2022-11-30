

using Microsoft.VisualStudio.Threading;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace TelegramBot.Services.Logs
{

    public delegate void LogDelegate(object obj, string str);

    internal class Log : ILog
    {

        private static AsyncQueue<string> _asyncQueue;  
        public LogDelegate logDelegate { get; set; }
        

        public Log()
        {
            logDelegate = HandleOut;
            _asyncQueue = new AsyncQueue<string>();
        }

        private void HandleOut(object obj, string str)
        {
            _ = _asyncQueue.TryEnqueue(DateTime.Now.ToString() + "\n\t" + str + "\n\t" + obj.ToString());

            _=Task.Run(() => 
            {
                Start();
            });
        }

        private void Start()
        {
            do
            {
                _ = _asyncQueue.TryDequeue(out string result);

                if (result != null)
                {
                    Console.WriteLine(result);
                    LogToFile(result);
                }
            } while (!_asyncQueue.IsEmpty);
        }

        private void LogToFile(string str)
        {
            try
            {
                using StreamWriter sw = File.AppendText(RealPath());
                sw.WriteLine(str);
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + "\n" + e.Message + "\n" + this);
            }
        }

        private string RealPath()
        {
            string path = Constatnts.ConstantFolders.LOGS_FOLDER + DateTime.Now.Date.ToString().First() + ".txt";
            
            if(!File.Exists(path))
            {
                _ = File.Create(path);
            }

            return path;
        }

    }
}
