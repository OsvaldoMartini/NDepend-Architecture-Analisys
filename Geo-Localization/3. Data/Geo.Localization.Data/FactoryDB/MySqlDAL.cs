using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

namespace Geo.Localization.Data.FactoryDB
{
    class MySqlDAL : DBase
    {
        public MySqlConnection cnn;
        public MySqlDAL()
        {
            if ((cnn = (MySqlConnection)GetDataProviderConnection()) == null)
                Dispose();
        }

        public MySqlDAL(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        // DALBaseClass Members
        public sealed override IDbConnection GetDataProviderConnection()
        {
            MySqlConnection conn = null;

            try
            {
                var connStr = ConfigurationManager.ConnectionStrings["DBCloudConn"].ToString();
                conn = new MySqlConnection(connStr);
                //conn.Open();
                return conn;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        System.Diagnostics.Debug.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        System.Diagnostics.Debug.WriteLine("Invalid username/password, please try again");
                        break;
                }
            }

            return null;
        }

        public override IDbCommand GeDataProviderCommand()
        {
            return new MySqlCommand("MySqlConn", cnn);
        }

        public override IDbDataParameter GeDataParameter()
        {
          return new MySqlParameter();
        }

        
        public override IDbDataParameter CreateParameter(DbType parameterType, int size, string name, ParameterDirection direction, object value)
        {
            return new MySqlParameter()
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
            return new MySqlDataAdapter();
        }

        public void Dispose()
        {
            if (cnn != null)
                cnn.Dispose();
        }

    }
}
