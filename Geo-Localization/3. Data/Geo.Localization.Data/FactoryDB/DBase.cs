using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Localization.Data.FactoryDB
{
    public abstract class DBase : IDisposable
    {
        private string strConnectionString;
        private IDbConnection connection;
        private IDbCommand command;
        private IDbTransaction transaction;

        public string ConnectionString
        {
            get
            {
                if (strConnectionString == string.Empty || strConnectionString.Length == 0)
                    throw new ArgumentException("Invalid database connection string.");
                
                return strConnectionString;
            }
            set
            { strConnectionString = value; }
        }

        protected DBase() { }

        public abstract IDbConnection GetDataProviderConnection();
        
        public abstract IDbCommand GeDataProviderCommand();

        public abstract IDbDataParameter GeDataParameter();

        public abstract IDbDataParameter CreateParameter(DbType parameterType, int size, string name, ParameterDirection direction, object value=null);

        public abstract IDbDataAdapter GetDataProviderDataAdapter();

        #region Database Transaction

        public string OpenConnection()
        {
            string Response = string.Empty;
            try
            {
                connection = GetDataProviderConnection(); // instantiate a connection object
                //connection.ConnectionString = this.ConnectionString;
                //connection.Open(); // open connection
                Response = ((System.Reflection.MemberInfo)(connection.GetType())).Name + " Open Successfully";
            }
            catch
            {
                connection.Close();
                Response = "Unable to Open " + ((System.Reflection.MemberInfo)(connection.GetType())).Name;
            }
            return Response;
        }

        #endregion

        public void Dispose()
        {
            if (connection != null)
                connection.Dispose();
        }

        public IDbTransaction BeginTransaction()
        {
            return connection.BeginTransaction();
        }

    }
}


