

using System;


namespace TelegramBot.Models
{
    internal class ProfileInfo
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? UserName { get; set; } = null;
        public long IdClient { get; set; }
        public string? Text { get; set; } = null;
        public string? PathToPhoto { get; set; } = null;
        public string? Location { get; set; } = null;
        public DateTime DateTime { get; set; }
        public bool IsBot { get; set; }
    }
}
