

using TelegramBot.Models;
using TelegramBot.Logs;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TelegramBot.DataBase
{
    internal class DataManager : DbContext, IDataManager
    {
        public DbSet<ProfileInfo> ProfileInfos { get; set; }

        private readonly ILog _log;

        public DataManager(ILog log)
        {
            _log = log;
            _ = Database.EnsureCreated();//cheking and cretate data base
            _log.logDelegate(this, "База даних працює");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TelegramBotDB;Trusted_Connection=True;");
        }


        #region implement Interface

        public async Task<int> Write_Async<T>(T item) where T : class
        {
            if (item is ProfileInfo)
            {
                _ = ProfileInfos.Add(item as ProfileInfo);
                return await SaveChangesAsync();
            }
            return 0;
        }

        public Task<int> Read_Async<T>(T item) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task Delete_Async(object item)
        {
            throw new System.NotImplementedException();
        }

        public List<T> FindByDate<T>(int year, int mounth, int day)where T:class
        {
           // if (typeof(T) == typeof(ProfileInfo))
           
                var list1 = ProfileInfos.Where(p => p.DateTime.Date ==
                                  new System.DateTime(year, mounth, day)).ToList();//year, mounth, day
                
            return list1 as List<T>;
        }

        #endregion
    }
}
