


using TelegramBot.Logs;

namespace TelegramBot.SearchHuman
{
    internal interface IFindHuman
    {
        string SearchInList(string str, ILog log);
    }
}
