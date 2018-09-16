using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Geo.Localization.Services.DataTransferObject;

namespace Geo.Localization.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICompanyWcfServices
    {
        [OperationContract]
        [WebGet]
        string GetData(int value);

        [OperationContract]
        [WebGet(UriTemplate = "Backend/{*sector}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<CompanySalesDto> Backend(string sector);
    }

}
