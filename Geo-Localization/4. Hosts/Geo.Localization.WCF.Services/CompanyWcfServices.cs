using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.ServiceImplementation;

namespace Geo.Localization.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CompanyWcfServices : ICompanyWcfServices
    {
        public CompanyWcfServices()
        {
            CultureInfo ci = new CultureInfo("en-US", false);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Debug.WriteLine("CompanyWcfServices Passed in Contructor.");
        }

        public string GetData(int value)
        {

            return $"You entered: {value}";
        }

        public List<CompanySaleDto> Backend(string sector)
        {
            CompanySalesService _companySalesService = new CompanySalesService();

            var companySales = _companySalesService.GetCompanySaleByType(sector);

            return companySales;
        }
    }
}
