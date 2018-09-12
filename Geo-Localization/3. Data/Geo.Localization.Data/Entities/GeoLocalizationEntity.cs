namespace Geo.Localization.Data
{
    public partial class GeoLocalizationEntity
    {
        public GeoLocalizationEntity()
        {
        }

        public int GeoLocalizationID { get; set; }
        public int EmployeeID { get; set; }
        public string LocalName { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}
