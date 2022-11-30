


using TelegramBot.Services.Logs;


namespace TelegramBot.Services.SearchHuman
{
    internal interface IFindHuman
    {
        string SearchInList(string str, ILog log);
    }
}
