using System.Text.RegularExpressions;

namespace Geo.Localization.Services.Utils
{
    public abstract class BaseBO : IValidation
    {
        public virtual void Validation()
        {
        }

        public virtual bool isValidEmail(string inputEmail)
        {
            var strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return true;
            return false;
        }

        public virtual bool isPhoneUK(string phoneUK)
        {
            var strRegex = @"^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})" +
                           @"|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})" +
                           @"|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$";
            var re = new Regex(strRegex);
            if (re.IsMatch(phoneUK))
                return true;
            return false;
        }
    }
}