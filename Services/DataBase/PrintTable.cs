

using TelegramBot.Models;

using System;
using System.Collections.Generic;


namespace TelegramBot.Services.DataBase
{
    internal class PrintTable
    {

        private readonly IDataManager _db;


        public PrintTable(IDataManager db)
        {
            _db = db;
        }

        public void Print_Table()
        {
            
            List <ProfileInfo> list = _db.Lists<ProfileInfo>();

            Console.WriteLine(("\n").PadRight(95, '-'));

            for(int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{list[i].Id}\t{list[i].FirstName}\t{list[i].IdClient}\t" +
                                          $"{list[i].Text}\t{list[i].DateTime}");
                Console.WriteLine(("").PadRight(95, '-'));
            }

            Console.WriteLine("\n");
        }

    }
}
