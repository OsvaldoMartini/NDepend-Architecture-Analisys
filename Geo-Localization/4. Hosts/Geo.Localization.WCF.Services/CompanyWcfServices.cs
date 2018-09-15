using System;
using System.Collections.Generic;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.ServiceImplementation;

namespace Geo.Localization.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CompanyWcfServices : ICompanyWcfServices
    {

        //private CompanySalesService _companySalesService;

        public string GetData(int value)
        {
            return $"You entered: {value}";
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        //public IList<CompanySalesDto> Backend(string sector)
        //{
        //    return _companySalesService.GetCompanySaleByType(sector);
        //}
    }
}
