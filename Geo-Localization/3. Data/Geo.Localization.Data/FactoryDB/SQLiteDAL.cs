using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Geo.Localization.Data.FactoryDB
{
    class SQLiteDAL : DBase
    {
        private SQLiteConnection cnn;
        private string connectString = ConfigurationManager.ConnectionStrings["DBSQLiteLocalConn"].ToString();

        public SQLiteDAL()
        {
            if ((cnn = (SQLiteConnection)GetDataProviderConnection()) == null)
                Dispose();
        }

        public SQLiteDAL(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        // DALBaseClass Members
        public sealed override IDbConnection GetDataProviderConnection()
        {
            SQLiteConnection conn = null;

            try
            {
                conn = new SQLiteConnection(connectString);
                Initialize(conn);
                //conn.Open();
                return conn;
            }
            catch (SQLiteException e)
            {
                conn.Dispose();
                return null;
            }
        }

        private void Initialize(SQLiteConnection conn)
        {
            conn.Open();

            try
            {

                SQLiteCommand command = new SQLiteCommand(CompanyTable, conn);
                command.ExecuteNonQuery();

                command = new SQLiteCommand(EmployeeTable, conn);
                command.ExecuteNonQuery();

                command = new SQLiteCommand(GeoLocalizationTable, conn);
                command.ExecuteNonQuery();

                FirstDataCreate(conn);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                CleanUp(connectString);
                //throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public override IDbCommand GeDataProviderCommand()
        {
            return new SQLiteCommand("SQLiteConn", cnn);
        }

        public override IDbDataParameter GeDataParameter()
        {
            return new SQLiteParameter();
        }

        public override IDbDataParameter CreateParameter(DbType parameterType, int size, string name, ParameterDirection direction, object value)
        {
            return new SQLiteParameter
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
            return new SQLiteDataAdapter();
        }

        public void Dispose()
        {
            if (cnn != null)
                cnn.Dispose();
        }

        private static void FirstDataCreate(SQLiteConnection _connection)
        {
            var companyCollection = _connection.Query<CompanyEntity>(
                "SELECT * FROM Company LIMIT 0,30;");

            if (companyCollection.FirstOrDefault().CompanyID == 0)
            {
                SQLiteCommand command = new SQLiteCommand(StartDataCompany, _connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand(StartDataEmployee, _connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand(StartDataGeoLocalization, _connection);
                command.ExecuteNonQuery();
            }
        }

        public static void CleanUp(string connectString)
        {
            var dbConnection = new SQLiteConnection(connectString);
            dbConnection.Open();

            try
            {
                SQLiteCommand command =
                    new SQLiteCommand("drop table GeoLocalization", dbConnection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("drop table Employee", dbConnection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("drop table Company", dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        #region DDL Scripts
        internal static string CompanyTable = @"CREATE TABLE IF NOT EXISTS Company (
	                                    CompanyID INTEGER PRIMARY KEY AUTOINCREMENT,
	                                    Name varchar(100) NOT NULL,
	                                    Address varchar(250) NOT NULL,
	                                    PostCode varchar(10) NOT NULL,
	                                    State  varchar(50) NOT NULL,
	                                    Country  varchar(50) NOT NULL,
	                                    Email varchar(255) NOT NULL,
	                                    WebSite varchar(255) NOT NULL,
	                                    Phone varchar(20) NOT NULL,
	                                    DateCreated datetime NULL);";

        internal static string EmployeeTable = @"CREATE TABLE IF NOT EXISTS Employee (
                                        EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
                                        CompanyID integer NOT NULL,
                                        LastName varchar(30) NOT NULL,
                                        FirstName varchar(20) NOT NULL,
                                        UserName varchar(20) NOT NULL,
                                        Email varchar(255) NOT NULL,
                                        Password varchar(20) NOT NULL,
                                        RoleID integer NOT NULL,
                                        DateCreated datetime NULL);";


        internal static string GeoLocalizationTable = @"CREATE TABLE IF NOT EXISTS GeoLocalization (
	                                    GeoLocalizationID INTEGER PRIMARY KEY AUTOINCREMENT,
	                                    EmployeeID integer NOT NULL,
	                                    LocalName varchar(100) NOT NULL,
	                                    Lat varchar(30) NOT NULL,
	                                    Lng varchar(30) NOT NULL,
	                                    DateCreated datetime NULL);";
        #endregion
        #region DML Scripts

        internal static string StartDataCompany =
            @"INSERT INTO Company (Name,Address,PostCode,State,Country,Email,WebSite,Phone,DateCreated)
                                                     VALUES('Geo Localization','Geo Localization Global Headquarters Receptionist','W1W','London','United Kingdom','osvaldo.martini@gmail.com','www.wservices.co.uk', '+44 (0)7951 144 116', date('now','start of month','+1 month','-1 day'));";


        internal static string StartDataEmployee = @"INSERT INTO Employee (CompanyID,LastName,FirstName,UserName,Email,Password,RoleID,DateCreated) VALUES(1, 'admin','Adm','Admin','admin@gmail.com','admin', 1, date('now','start of month','+1 month','-1 day'));
                                                    INSERT INTO Employee (CompanyID,LastName,FirstName,UserName,Email,Password,RoleID,DateCreated) VALUES(1, 'Martini','Osvaldo','Omartini','osvaldo.martini@gmail.com','martini', 1, date('now','start of month','+1 month','-1 day'));";


        internal static string StartDataGeoLocalization = @"INSERT INTO GeoLocalization (EmployeeID,LocalName,Lat,Lng,DateCreated) VALUES(1,'Liverpool Museum','-2.979919','51.521437', date('now','start of month','+1 month','-1 day'));
                                                        INSERT INTO GeoLocalization (EmployeeID,LocalName,Lat,Lng,DateCreated) VALUES(1,'Merseyside Maritime Museum','-0.081437','51.518952', date('now','start of month','+1 month','-1 day'));
                                                        INSERT INTO GeoLocalization (EmployeeID,LocalName,Lat,Lng,DateCreated) VALUES(1,'Walker Art Gallery','-0.143975','51.521831', date('now','start of month','+1 month','-1 day'));
                                                        INSERT INTO GeoLocalization (EmployeeID,LocalName,Lat,Lng,DateCreated) VALUES(1,'National Conservation Centre','-2.984683','53.407511', date('now','start of month','+1 month','-1 day'))";

        #endregion




    }
}
