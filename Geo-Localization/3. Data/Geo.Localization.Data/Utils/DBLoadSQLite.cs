using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Geo.Localization.Data.Utils
{
    public class DBLoadSQLite
    {
        public static void InitializeDatabase(string connectString)
        {
            var dbConnection = new SQLiteConnection(connectString);
            dbConnection.Open();

            try
            {

                SQLiteCommand command = new SQLiteCommand(CompanyTable, dbConnection);
                command.ExecuteNonQuery();

                command = new SQLiteCommand(EmployeeTable, dbConnection);
                command.ExecuteNonQuery();

                command = new SQLiteCommand(GeoLocalizationTable, dbConnection);
                command.ExecuteNonQuery();

                FirstDataCreate(dbConnection);
            }
            catch (Exception)
            {
                CleanUp(connectString);
                //throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private static void FirstDataCreate(SQLiteConnection _connection)
        {
            var companyCollection = _connection.Query<CompanyEntity>(
                "SELECT Count(*) FROM Company WHERE LIMIT 1;");

            if (!companyCollection.Any())
            {
                SQLiteCommand command = new SQLiteCommand(StartDataCompany, _connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand(StartDataEmployee, _connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand(StartDataGeoLocalization, _connection);
                command.ExecuteNonQuery();
            }
        }

        public static SQLiteConnection GetConnection(string connectString)
        {
            var dbConnection = new SQLiteConnection(connectString);
            dbConnection.Open();

            return dbConnection;
        }

        public static void CleanUp(string connectString)
        {
            var dbConnection = new SQLiteConnection(connectString);
            dbConnection.Open();

            try
            {
                SQLiteCommand command =
                    new SQLiteCommand("drop table GeoLocalization",dbConnection);
                command.ExecuteNonQuery();
                command =new SQLiteCommand("drop table Employee",dbConnection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("drop table Company",dbConnection);
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
        internal static string CompanyTable = @"CREATE TABLE IF NOT EXISTS Company(
	                                    CompanyID int AUTO_INCREMENT NOT NULL,
	                                    Name varchar(100) NOT NULL,
	                                    Address varchar(250) NOT NULL,
	                                    PostCode varchar(10) NOT NULL,
	                                    State  varchar(50) NOT NULL,
	                                    Country  varchar(50) NOT NULL,
	                                    Email varchar(255) NOT NULL,
	                                    WebSite varchar(255) NOT NULL,
	                                    Phone varchar(20) NOT NULL,
	                                    DateCreated datetime NULL,
                                        CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED (CompanyID ASC));";

        internal static string EmployeeTable = @"CREATE TABLE IF NOT EXISTS Employee(
                                        EmployeeID int AUTO_INCREMENT NOT NULL,
                                        CompanyID int NOT NULL,
                                        LastName varchar(30) NOT NULL,
                                        FirstName varchar(20) NOT NULL,
                                        UserName varchar(20) NOT NULL,
                                        Email varchar(255) NOT NULL,
                                        Password varchar(20) NOT NULL,
                                        RoleID int NOT NULL,
                                        DateCreated datetime NULL,
                                        CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED ( EmployeeID ASC));";


        internal static string GeoLocalizationTable = @"CREATE TABLE IF NOT EXISTS GeoLocalization(
	                                    GeoLocalizationID int AUTO_INCREMENT NOT NULL,
	                                    EmployeeID int NOT NULL,
	                                    LocalName varchar(100) NOT NULL,
	                                    Lat varchar(30) NOT NULL,
	                                    Lng varchar(30) NOT NULL,
	                                    DateCreated datetime NULL,
                                        CONSTRAINT PK_GeoLocalization PRIMARY KEY CLUSTERED (GeoLocalizationID ASC));";
        #endregion
        #region DML Scripts
         internal static string StartDataCompany =   @"INSERT INTO `Company` (`Name`,`Address`,`PostCode`,`State`,`Country`,`Email`,`WebSite`,`Phone`,`DateCreated`)
                                                    VALUES('Geo Localization','Geo Localization Global Headquarters Receptionist','W1W','London','United Kingdom','osvaldo.martini@gmail.com','osvaldo.martni@gmail.com', '+44 (0)20 3253 2337', CURRENT_TIMESTAMP()";


         internal static string StartDataEmployee = @"INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(1, 'admin','Adm','Admin','admin@gmail.com','admin', 1, CURRENT_TIMESTAMP());
                                                    INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(1, 'Martini','Osvaldo','Omartini','osvaldo.martini@gmail.com','martini', 1, CURRENT_TIMESTAMP())";


        internal static string StartDataGeoLocalization = @"INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'Liverpool Museum','-2.979919','51.521437', CURRENT_TIMESTAMP());
                                                        INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'Merseyside Maritime Museum','-0.081437','51.518952', CURRENT_TIMESTAMP());
                                                        INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'Walker Art Gallery','-0.143975','51.521831', CURRENT_TIMESTAMP());
                                                        INSERT INTO `GeoLocalization` (`EmployeeID`,`LocalName`,`Lat`,`Lng`,`DateCreated`) VALUES(1,'National Conservation Centre','-2.984683','53.407511', CURRENT_TIMESTAMP())";

        #endregion


    }
}