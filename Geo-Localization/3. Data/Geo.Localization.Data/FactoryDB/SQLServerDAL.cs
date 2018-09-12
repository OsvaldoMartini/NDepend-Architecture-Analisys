using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Geo.Localization.Data.FactoryDB
{
    class SQLServerDAL : DBase
    {
        public SqlConnection cnn;
        public SQLServerDAL()
        {
            if ((cnn = (SqlConnection) GetDataProviderConnection()) == null)
                Dispose();
        }

        public SQLServerDAL(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        // DALBaseClass Members
        public sealed override IDbConnection GetDataProviderConnection()
        {
            SqlConnection conn = null;

            try
            {
                var connStr = ConfigurationManager.ConnectionStrings["DBLocalConn"].ToString();
                conn = new SqlConnection(connStr);
                //conn.Open();
                return conn;
            }
            catch (SqlException e)
            {
                conn.Dispose();
                return null;
            }
        }

        public override IDbCommand GeDataProviderCommand()
        {
            return new SqlCommand();
        }

        public override IDbDataParameter GeDataParameter()
        {
            return new SqlParameter();
        }

        public override IDbDataParameter CreateParameter(DbType parameterType, int size, string name, ParameterDirection direction, object value)
        {
            return new SqlParameter
            {
                DbType = parameterType,
                Size = size,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            return new SqlDataAdapter();
        }

        public void Dispose()
        {
            if (cnn != null)
                cnn.Dispose();
        }

    }
}
