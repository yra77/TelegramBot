

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TelegramBot.Services.DataBase
{
    internal interface IDataManager //: IDisposable
    {
        Task<int> Add_Async<T>(T item) where T : class;
        Task<int> Read_Async<T>(T item) where T : class;
        Task<int> Delete_Async(int id);
        List<T> FindByDate<T>(int year, int mounth, int day)where T:class;
        List<T> Lists<T>() where T : class;
    }
}
