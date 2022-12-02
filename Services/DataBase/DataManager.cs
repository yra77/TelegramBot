

using TelegramBot.Models;
using TelegramBot.Services.Logs;
using TelegramBot.Constatnts;

using Microsoft.Data.Sqlite;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;


namespace TelegramBot.Services.DataBase
{
    internal class DataManager : IDataManager
    {

        private static readonly string DB_FILE_NAME;
        private readonly SqliteCommand _command;
        private readonly ILog _log;


        static DataManager()
        {
            DB_FILE_NAME = DateTime.Now.Date.ToString().Split(' ').First() + ".db";
        }

        public DataManager(ILog log)
        {
            _log = log;
            try
            {
                var connection = new SqliteConnection($"Data Source={ConstantFolders.SqLite_FOLDER}" +
                                                                    $"{DB_FILE_NAME}");

                connection.Open();

                _command = new SqliteCommand();
                _command.Connection = connection;

                if (!IsExistTable(ConstantFolders.SqLite_FOLDER + DB_FILE_NAME))
                {
                    CreateTable();
                    _log.logDelegate(this, ConstantMessage.CREATE_TABLE);
                }
                
            }
            catch (Exception e)
            {
                _log.logDelegate(this, e.Message);
            }
        }


        public async Task<int> Delete_Async(int id)
        {
                _command.CommandText = $"DELETE  FROM ProfileInfo WHERE Id='{id}'";

                return await _command.ExecuteNonQueryAsync();
        }

        public List<T> FindByDate<T>(int year, int mounth, int day) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Read_Async<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<int> Add_Async<T>(T item) where T : class
        {

            string query = "";

            if (typeof(T) == typeof(ProfileInfo))
            {
                ProfileInfo structProfile = item as ProfileInfo;
                query = $"INSERT INTO ProfileInfo (FirstName ,LastName, UserName, " +
                                                  $"IdClient, Text, PathToPhoto, " +
                                                  $"Location, DateTime, IsBot)" +
                                                  $"VALUES" +
                                                  $"('{structProfile.FirstName}', " +
                                                  $"'{structProfile.LastName}'," +
                                                  $"'{structProfile.UserName}'," +
                                                  $"'{structProfile.IdClient}'," +
                                                  $"'{structProfile.Text}'," +
                                                  $"' {structProfile.PathToPhoto}'," +
                                                  $"' {structProfile.Location}'," +
                                                  $"' {structProfile.DateTime}'," +
                                                  $"'{structProfile.IsBot}')";
            }

            _command.CommandText = query;

            return await _command.ExecuteNonQueryAsync();
        }

        public List<T> Lists<T>() where T : class
        {
            List<ProfileInfo> profileList = new List<ProfileInfo>();

            _command.CommandText = "SELECT * FROM ProfileInfo";

            using (SqliteDataReader reader = _command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {

                        ProfileInfo profileInfo = new ProfileInfo();

                        profileInfo.Id = reader.GetInt32(0);
                        profileInfo.FirstName = reader.GetString(1);
                        profileInfo.LastName = reader.GetString(2);
                        profileInfo.UserName = reader.GetString(3);
                        profileInfo.IdClient = reader.GetInt64(4);
                        profileInfo.Text = reader.GetString(5);
                        profileInfo.PathToPhoto = reader.GetString(6);
                        profileInfo.Location = reader.GetString(7);
                        profileInfo.DateTime = reader.GetDateTime(8);
                        profileInfo.IsBot = reader.GetBoolean(9);

                        profileList.Add(profileInfo);
                    }
                }
            }

            return profileList as List<T>;
        }

        private bool IsExistTable(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if (fi.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateTable()
        {

            _command.CommandText = @"CREATE TABLE ProfileInfo(" +
                                  "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                                  "FirstName TEXT NULL, " +
                                  "LastName TEXT NULL," +
                                  "UserName TEXT NULL," +
                                  "IdClient INTEGER NOT NULL," +
                                  "Text TEXT NULL," +
                                  "PathToPhoto TEXT NULL," +
                                  "Location TEXT NULL," +
                                  "DateTime TEXT NOT NULL," +
                                  "IsBot INTEGER NOT NULL)";
            _command.ExecuteNonQuery();
        }

    }
}
