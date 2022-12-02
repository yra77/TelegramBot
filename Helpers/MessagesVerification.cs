

using System.Text.RegularExpressions;


namespace TelegramBot.Helpers
{
    internal class MessagesVerification
    {

        public bool VerifyText(string text)
        {
            string pattern = @"[а-я'іїє]+ [а-я'іїє]+ [а-я'іїє]+ [0-9]{1,2}\.[0-9]{1,2}\.[0-9]{4}";
            string antiPattern = @"[ыъэё]+";

            if (text != null && 
                Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase) &&
               !Regex.IsMatch(text, antiPattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public static bool VerifyPhoto()
        //{

        //}

    }
}
