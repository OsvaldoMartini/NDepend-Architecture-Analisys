using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Geo.Localization.Data.Utils
{
    public class DBLoadSQLServer : IDisposable
    {
        public SqlConnection cnn;

        public DBLoadSQLServer()
        {
            if ((cnn = _cnn()) == null)
                Dispose();
        }

        public void Dispose()
        {
            if (cnn != null)
                cnn.Dispose();
        }

        private SqlConnection _cnn()
        {
            SqlConnection conn = null;

            try
            {
                var connStr = ConfigurationManager.ConnectionStrings["DBCloudConn"].ToString();
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
    }
}