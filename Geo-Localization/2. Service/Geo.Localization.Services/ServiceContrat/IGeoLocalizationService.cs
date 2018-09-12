using System.Collections.Generic;
using System.ServiceModel;
using Geo.Localization.Services.DataTransferObject;

namespace Geo.Localization.Services.ServiceContrat
{
    [ServiceContract]
    public interface IGeoLocalizationService
    {
        [OperationContract]
        IList<GeoLocalizationDto> GetAllGeoLocalization(GeoLocalizationDto geoLocalization);

        [OperationContract]
        GeoLocalizationDto FindByID(GeoLocalizationDto geoLocalization);

        [OperationContract]
        string DeleteGeoLocalization(GeoLocalizationDto geoLocalization);

        [OperationContract]
        void UpdateGeoLocalization(GeoLocalizationDto geoLocalization);

        void InsertGeoLocalization(GeoLocalizationDto geoLocalization);

    }
}