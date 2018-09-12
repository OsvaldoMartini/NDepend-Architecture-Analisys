using System;
using System.Globalization;

namespace Geo.Localization.WebApp.Extensions
{
    public class CultureInfoExtension : CultureInfo,IDisposable
    {
        internal CultureInfoExtension(string lang) : base(lang)
        {
        }
      
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}