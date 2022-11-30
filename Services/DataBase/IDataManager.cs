

using System.Collections.Generic;
using System.Threading.Tasks;


namespace TelegramBot.Services.DataBase
{
    internal interface IDataManager
    {
        Task<int> Write_Async<T>(T item) where T : class;
        Task<int> Read_Async<T>(T item) where T : class;
        Task Delete_Async(object item);
        List<T> FindByDate<T>(int year, int mounth, int day)where T:class;
    }
}
