using System.ServiceModel;
using Geo.Localization.Services.DataTransferObject;

namespace Geo.Localization.Services.ServiceContrat
{
    [ServiceContract]
    public interface ICompanyService
    {
        [OperationContract]
        
        CompanyDto FindByID(int id);
    }
}