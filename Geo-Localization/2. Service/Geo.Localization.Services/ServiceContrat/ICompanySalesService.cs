using System.Collections.Generic;
using System.ServiceModel;
using Geo.Localization.Services.DataTransferObject;

namespace Geo.Localization.Services.ServiceContrat
{
    [ServiceContract]
    public interface ICompanySalesService
    {
        [OperationContract]
        List<CompanySalesDto> GetCompanySaleByType(string typeCompany);
    }
}