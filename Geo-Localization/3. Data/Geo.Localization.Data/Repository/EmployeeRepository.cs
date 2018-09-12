using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using Dapper;
using Geo.Localization.Data.FactoryDB;
using Geo.Localization.Data.IRepository;
using Geo.Localization.Data.Utils;
using MySql.Data.MySqlClient;

namespace Geo.Localization.Data.Repository
{
    public class EmployeeRepository : GenericRepository<EmployeeEntity>, IEmployeeRepository
    {
        internal IFactory factory;
        internal string dbVendor = ConfigurationManager.AppSettings["DbVendor"];

        private static EmployeeRepository self;
        public static EmployeeRepository Instance()
        {
            if (self == null)
                self = new EmployeeRepository();
            return self;
        }

        public EmployeeRepository()
        {
            this.factory = Factory.Instance();

        }

        //Consultations and specific operations we will put here
        public override int Insert(EmployeeEntity _employee)
        {
            using (var conn =
                factory.GetDataAccessLayer((DataProviderType) Enum.Parse(typeof(DataProviderType), dbVendor)))
            {
                using (var dataCommand = conn.GeDataProviderCommand())
                {

                    if (dbVendor != DataProviderType.SQLite.ToString())
                    {
                        dataCommand.CommandType = CommandType.StoredProcedure;
                        dataCommand.CommandText = "PROC_INSERT_EMPLOYEE";
                    }
                    else
                    {
                        dataCommand.Connection.Open();;

                        IDbTransaction tran = dataCommand.Connection.BeginTransaction();
                        
                        dataCommand.CommandType = CommandType.Text;
                        dataCommand.CommandText =
                            "INSERT INTO Employee (CompanyID,LastName,FirstName,UserName,Email,Password,RoleID,DateCreated) VALUES(?,?,?,?,?,?,?,?);select seq from sqlite_sequence where name = 'Employee';";
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.Int32, 50, "P_CompanyID",
                            ParameterDirection.Input,
                            _employee.TCompany.CompanyID));
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 30, "P_LastName",
                            ParameterDirection.Input,
                            _employee.LastName));
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 20, "P_FirstName",
                            ParameterDirection.Input,
                            _employee.FirstName));
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 20, "P_UserName",
                            ParameterDirection.Input,
                            _employee.UserName));
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 255, "P_Email",
                            ParameterDirection.Input,
                            _employee.Email));
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 20, "P_Password",
                            ParameterDirection.Input,
                            _employee.Password));
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.Int64, 50, "P_RoleID",
                            ParameterDirection.Input,
                            _employee.RoleID));
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.DateTime, 50, "P_DateCreated",
                            ParameterDirection.Input,
                            Convert.ToDateTime(DateTime.Today.ToString("yyyy-M-d"))));
                        int id = 0;
                        try
                        {

                            id = Convert.ToInt32(dataCommand.ExecuteScalar());
                            
                            tran.Commit();


                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            return 0;
                            //throw new Exception(ex.Message);
                        }
                        finally
                        {
                            dataCommand.Parameters.Clear();
                            dataCommand.Connection.Close();
                        }
                        return id;
                    }



                    dataCommand.Parameters.Add(conn.CreateParameter(DbType.Int32, 50, "P_CompanyID",
                        ParameterDirection.Input,
                        _employee.TCompany.CompanyID));
                    dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 30, "P_LastName",
                        ParameterDirection.Input,
                        _employee.LastName));
                    dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 20, "P_FirstName",
                        ParameterDirection.Input,
                        _employee.FirstName));
                    dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 20, "P_UserName",
                        ParameterDirection.Input,
                        _employee.UserName));
                    dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 255, "P_Email",
                        ParameterDirection.Input,
                        _employee.Email));
                    dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 20, "P_Password",
                        ParameterDirection.Input,
                        _employee.Password));
                    dataCommand.Parameters.Add(conn.CreateParameter(DbType.Int64, 50, "P_RoleID",
                        ParameterDirection.Input,
                        _employee.RoleID));

                    if (dbVendor != DataProviderType.SQLite.ToString())
                    {
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.Int64, 0, "P_int_Identity",
                            ParameterDirection.Output));
                        dataCommand.Parameters.Add(conn.CreateParameter(DbType.String, 500, "P_Return_Message",
                            ParameterDirection.Output));
                    }
                      
                    try
                    {
                        dataCommand.Connection.Open();
                        dataCommand.ExecuteNonQuery();
                        var id = 0;

                            var errorCode = ((IDbDataParameter) dataCommand.Parameters["P_Return_Message"]).Value;
                            if (errorCode != string.Empty)
                            {
                                Debug.WriteLine(id.ToString() + errorCode);
                            }
                            else
                            {
                                id = Convert.ToInt32(((IDbDataParameter)dataCommand.Parameters["P_int_Identity"]).Value
                                    .ToString());
                            }
                       
                        return id;
                    }
                    catch (Exception e)
                    {

                        Debug.WriteLine(e.Message);
                    }


                    finally
                    {
                        dataCommand.Dispose();
                        conn.Dispose();
                    }

                    return 0;
                }
            }
        }





        public override string Update(EmployeeEntity _employee)
        {
            var returnMessage = "";

            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {
                    dataCommand.CommandType = CommandType.StoredProcedure;
                    dataCommand.CommandText = "PROC_UPDATE_EMPLOYEE";
                    dataCommand.Parameters.AddWithValue("@P_CompanyID", _employee.TCompany.CompanyID);
                    dataCommand.Parameters.AddWithValue("@P_LastName", _employee.LastName);
                    dataCommand.Parameters.AddWithValue("@P_FirstName", _employee.FirstName);
                    dataCommand.Parameters.AddWithValue("@P_UserName", _employee.UserName);
                    dataCommand.Parameters.AddWithValue("@P_Email", _employee.Email);
                    dataCommand.Parameters.AddWithValue("@P_Password", _employee.Password);
                    dataCommand.Parameters.AddWithValue("@P_RoleID", _employee.RoleID);
                    dataCommand.Parameters.AddWithValue("@P_EmployeeID", _employee.EmployeeID);

                    dataCommand.Parameters.Add(new MySqlParameter("@P_Return_Message", MySqlDbType.VarChar));
                    dataCommand.Parameters["@P_Return_Message"].Direction = ParameterDirection.Output;



                    try
                    {
                        dataCommand.Connection = conn;
                        conn.Open();
                        dataCommand.ExecuteNonQuery();
                        var errorCode = dataCommand.Parameters["@P_Return_Message"].Value.ToString();
                        if (errorCode != string.Empty)
                            returnMessage = "Forbidden : <br>" + errorCode;
                    }
                    catch (MySqlException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    finally
                    {
                        dataCommand.Dispose();
                        conn.Dispose();
                    }
                }
            }
            return returnMessage;
        }

        public override IEnumerable<EmployeeEntity> GetAllByCompany(EmployeeEntity _employee)
        {
            IList<EmployeeEntity> list = new List<EmployeeEntity>();
            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {
                    dataCommand.CommandText = "SELECT  CompanyID " +
                                              ",EmployeeID " +
                                              ",LastName " +
                                              ",FirstName " +
                                              ",UserName " +
                                              ",Email " +
                                              ",Password " +
                                              ",RoleID " +
                                              ",DateCreated " +
                                              "FROM Employee " +
                                              "where CompanyID = @CompanyID " +
                                              "LIMIT 100";

                    dataCommand.Parameters.AddWithValue("@CompanyID", _employee.TCompany.CompanyID);
                    dataCommand.CommandType = CommandType.Text;

                    try
                    {
                        dataCommand.Connection = conn;
                        conn.Open();

                        var reader = dataCommand.ExecuteReader();

                        if (reader.HasRows)
                            list = Mapper.DataReaderMapToList<EmployeeEntity>(reader);

                        reader.Close();
                    }
                    finally
                    {
                        dataCommand.Dispose();
                        conn.Dispose();
                    }
                }
            }
            return list;
        }

        public override EmployeeEntity FindByID(EmployeeEntity _employee)
        {
            IList<EmployeeEntity> list = new List<EmployeeEntity>();
            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {
                    dataCommand.CommandText = "SELECT CompanyID, EmployeeID " +
                                              ",LastName " +
                                              ",FirstName " +
                                              ",UserName " +
                                              ",Email " +
                                              ",Password " +
                                              ",RoleID " +
                                              ",DateCreated " +
                                              "FROM Employee " +
                                              "Where CompanyID = @CompanyID and EmployeeID = @EmployeeID";

                    dataCommand.Parameters.AddWithValue("@CompanyID", _employee.TCompany.CompanyID);
                    dataCommand.Parameters.AddWithValue("@EmployeeID", _employee.EmployeeID);
                    dataCommand.CommandType = CommandType.Text;

                    try
                    {
                        dataCommand.Connection = conn;
                        conn.Open();

                        var reader = dataCommand.ExecuteReader();

                        if (reader.HasRows)
                            list = Mapper.DataReaderMapToList<EmployeeEntity>(reader);
                        reader.Close();
                    }
                    finally
                    {
                        dataCommand.Dispose();
                        conn.Dispose();
                    }
                }
            }
            return list.FirstOrDefault();
        }

        public override string Delete(EmployeeEntity _employee)
        {
            var returnMessage = "";
            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {
                    dataCommand.CommandType = CommandType.StoredProcedure;
                    dataCommand.CommandText = "PROC_DELETE_EMPLOYEE";

                    dataCommand.Parameters.AddWithValue("@P_CompanyID", _employee.TCompany.CompanyID);
                    dataCommand.Parameters.AddWithValue("@P_EmployeeID", _employee.EmployeeID);

                    dataCommand.Parameters.Add(new MySqlParameter("@P_Return_Message", MySqlDbType.VarChar));
                    dataCommand.Parameters["@P_Return_Message"].Direction = ParameterDirection.Output;


                    try
                    {
                        dataCommand.Connection = conn;
                        conn.Open();
                        dataCommand.ExecuteNonQuery();
                        var errorCode = dataCommand.Parameters["@P_Return_Message"].Value.ToString();
                        if (errorCode != string.Empty)
                            returnMessage = "Forbidden : <br>" + errorCode;
                    }

                    finally
                    {
                        dataCommand.Dispose();
                        conn.Dispose();
                    }
                }
            }
            return returnMessage;
        }

        public EmployeeEntity FindByUserName(EmployeeEntity _employee)
        {
            IList<EmployeeEntity> list = new List<EmployeeEntity>();
            using (var conn = new DBLoadMySql().cnn)
            {
                try
                {
                    conn.Open();

                    /* Dapper */
                    var query =
                        @"SELECT em.* FROM Employee em Where em.UserName = @UserName and em.Password = @Password and em.CompanyID = @CompanyID";
                    var oPara = new DynamicParameters();
                    oPara.Add("@UserName", _employee.UserName, DbType.String);
                    oPara.Add("@Password", _employee.Password, DbType.String);
                    oPara.Add("@CompanyID", _employee.TCompany.CompanyID, DbType.Int32);
                    var employeeFound = conn.Query<EmployeeEntity>(query, oPara);

                    var subsetquery =
                        @"select co.* from Company co where co.CompanyID = @Id";

                    foreach (var item in employeeFound)
                    {
                        var oChild = new DynamicParameters();
                        oChild.Add("@Id", item.CompanyID, DbType.Int32);

                        using (var multi = conn.QueryMultiple(subsetquery, oChild, commandType: CommandType.Text))
                        {
                            item.TCompany = multi.Read<CompanyEntity>().FirstOrDefault();
                        }
                    }

                    list = employeeFound.ToList();
                }
                finally
                {
                    conn.Dispose();
                }

                return list.FirstOrDefault();

            }

        }

    }
}