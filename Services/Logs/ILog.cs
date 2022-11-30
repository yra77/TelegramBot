


namespace TelegramBot.Logs
{
    internal interface ILog
    {
        LogDelegate logDelegate { get; set; }
    }
}
