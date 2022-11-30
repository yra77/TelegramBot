


namespace TelegramBot.Services.Logs
{
    internal interface ILog
    {
        LogDelegate logDelegate { get; set; }
    }
}
