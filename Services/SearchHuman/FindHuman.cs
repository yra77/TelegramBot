

using TelegramBot.Services.Logs;
using System;
using System.Threading.Tasks;


namespace TelegramBot.Services.SearchHuman
{
    internal class FindHuman : IFindHuman
    {

        private string[] ReadFromFile()
        {
            try
            {
                string[] lists = System.IO.File.ReadAllLines(Constatnts.ConstantFolders.PATHTOHUMAN);
                return lists;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public string SearchInList(string str, ILog log)
        {
           
            string[] lists = ReadFromFile();
            string? personFound = null;

            str = Checking(str);
           
            if (lists == null)
            {
                log.logDelegate(this, "Error - Відсутній файл");
                return Constatnts.ConstantMessage.NOT_HUMAN;
            }

            Parallel.For(0, lists.Length, (i, state) =>
            {
                if (state.ShouldExitCurrentIteration)
                {
                    if (state.LowestBreakIteration < i)
                        return;
                }
                if (lists[i].Equals(str))
                {
                    personFound = lists[i];
                    state.Break();
                }
            });

            //  System.Console.WriteLine($"\n{personFound}");

            if (personFound == null)
            {
                return Constatnts.ConstantMessage.NOT_HUMAN;
            }
            else
            {
                return Constatnts.ConstantMessage.OK;
            }
        }

        private string Checking(string str)//add zero
        {
            string[] temp = str.Split(' ');
           
            if (temp[3].Length != 10)
            {
                string[] buf = temp[3].Trim(' ').Split('.');

                if (buf[0].Length == 1)
                {
                   buf[0] = buf[0].Insert(0, "0");
                }
                if (buf[1].Length == 1)
                {
                   buf[1] = buf[1].Insert(0, "0");
                }

                str = temp[0] + " " + temp[1] + " " + temp[2] + " " + buf[0] + "." + buf[1] + "." + buf[2];    
            }
            
            str = str.Trim(' ');
            
            return str;
        }

    }
}
