

using TelegramBot.Services.Logs;
using System.Threading.Tasks;


namespace TelegramBot.Services.SearchHuman
{
    internal class FindHuman : IFindHuman
    {

        private static string[] lists = null;


        public static void InitializeStatic() 
        {
            string[] list = System.IO.File.ReadAllLines(Constatnts.ConstantFolders.PATHTOHUMAN);

            lists = new string[list.Length];
            list.CopyTo(lists, 0);
        }


        public string SearchInList(string str, ILog log)
        {
            string[] list = new string[lists.Length];
            //array copy
            Parallel.For(0, list.Length, (i, state) =>
            {
                list[i] = lists[i];
            });

            string personFound = null;

            str = Checking(str);

            if (list == null)
            {
                log.logDelegate(this, Constatnts.ConstantMessage.ERRORREAD_HUMANS);
                return Constatnts.ConstantMessage.NOT_HUMAN;
            }

            Parallel.For(0, list.Length, (i, state) =>
            {
                if (state.ShouldExitCurrentIteration)
                {
                    if (state.LowestBreakIteration < i)
                        return;
                }
                if (list[i].Equals(str))
                {
                    personFound = list[i];
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
