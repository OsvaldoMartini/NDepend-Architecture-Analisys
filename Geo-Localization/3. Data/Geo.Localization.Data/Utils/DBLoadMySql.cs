using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Geo.Localization.Data.Utils
{
    class DBLoadMySql : IDisposable
    {
        public MySqlConnection cnn;

        public DBLoadMySql()
        {
            if ((cnn = _cnn()) == null)
                Dispose();
        }

        public void Dispose()
        {
            if (cnn != null)
                cnn.Dispose();
        }

        private MySqlConnection _cnn()
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
                return null;
            }
        }
    }
}

