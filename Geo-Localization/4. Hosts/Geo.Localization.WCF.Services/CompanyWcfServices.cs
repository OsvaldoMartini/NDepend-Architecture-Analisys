using System.Collections.Generic;
using System.Diagnostics;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.ServiceImplementation;

namespace Geo.Localization.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CompanyWcfServices : ICompanyWcfServices
    {
        public CompanyWcfServices()
        {
            Debug.WriteLine("CompanyWcfServices Passed in Contructor.");
        }

        public string GetData(int value)
        {

            return $"You entered: {value}";
        }

        public List<CompanySalesDto> Backend(string sector)
        {
            CompanySalesService _companySalesService = new CompanySalesService();
            return _companySalesService.GetCompanySaleByType(sector);
        }
    }
}
