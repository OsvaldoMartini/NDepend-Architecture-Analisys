using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Localization.Data.Utils;

namespace Geo.Localization.Data.FactoryDB
{
    class Factory :IFactory
    {
        private static Factory self;

        public static Factory Instance()
        {
            if (self == null)
                self = new Factory();
            return self;
        }
        public override DBase GetDataAccessLayer(DataProviderType dataProviderType)
        {
            // construct specific data access provider class
            switch (dataProviderType)
            {
                case DataProviderType.SQLite:
                    return new SQLiteDAL();

                case DataProviderType.MySQL:
                    return new MySqlDAL();

                case DataProviderType.SQLServer:
                    return new SQLServerDAL();

                default:
                    throw new ArgumentException("Invalid data access layer provider type.");
            }
        }
    }
}