using System.Text.RegularExpressions;

namespace Geo.Localization.Data.Utils
{
    public abstract class BaseBO : IValidation
    {
        public virtual void Validation()
        {
        }
        public static bool IsValidEmail(string inputEmail)
        {
            var strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var re = new Regex(strRegex);
            
            
            if (re.IsMatch(inputEmail))
                return true;
            return false;
        }
    }
}