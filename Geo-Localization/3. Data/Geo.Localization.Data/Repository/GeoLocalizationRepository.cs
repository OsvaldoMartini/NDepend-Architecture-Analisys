using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Dapper;
using Geo.Localization.Data.IRepository;
using Geo.Localization.Data.Utils;
using MySql.Data.MySqlClient;

namespace Geo.Localization.Data.Repository
{
    public class GeoLocalizationRepository : GenericRepository<GeoLocalizationEntity>, IGeoLocalizationRepository
    {
        //Consultations and specific operations we will put here
        public override int Insert(GeoLocalizationEntity _geoLocalization)
        {
            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {

                    //string[] localLabes = _geoLocalization.LocalName.Split(' ');
                    //var twoLabel = (from i in localLabes
                    //                orderby i descending
                    //                select i).Take(3);

                    //var localName = string.Join(" ", twoLabel);

                    dataCommand.CommandType = CommandType.StoredProcedure;
                    dataCommand.CommandText = "PROC_INSERT_GEOLOCALIZATION";
                    dataCommand.Parameters.AddWithValue("@P_EmployeeID", _geoLocalization.EmployeeID);
                    dataCommand.Parameters.AddWithValue("@P_LocalName", _geoLocalization.LocalName);
                    dataCommand.Parameters.AddWithValue("@P_Lat", _geoLocalization.Lng);
                    dataCommand.Parameters.AddWithValue("@P_Long", _geoLocalization.Lat);

                    dataCommand.Parameters.Add(new MySqlParameter("@P_int_Identity", MySqlDbType.Int64));
                    dataCommand.Parameters["@P_int_Identity"].Direction = ParameterDirection.Output;

                    dataCommand.Parameters.Add(new MySqlParameter("@P_Return_Message", MySqlDbType.VarChar));
                    dataCommand.Parameters["@P_Return_Message"].Direction = ParameterDirection.Output;

                    try
                    {
                        dataCommand.Connection = conn;
                        conn.Open();
                        dataCommand.ExecuteNonQuery();
                        var id = 0;
                        var errorCode = dataCommand.Parameters["@P_Return_Message"].Value.ToString();
                        if (errorCode != string.Empty)
                        {
                            Debug.WriteLine(id.ToString() + errorCode);
                        }
                        else
                        {
                            id = Convert.ToInt32(dataCommand.Parameters["@P_int_Identity"].Value.ToString());
                        }

                        return id;
                    }
                    finally
                    {
                        dataCommand.Dispose();
                        conn.Dispose();
                    }
                }
            }
        }
    

        public override string Update(GeoLocalizationEntity _geoLocalization)
        {
            var returnMessage = "";

            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {
                    dataCommand.CommandType = CommandType.StoredProcedure;
                    dataCommand.CommandText = "PROC_UPDATE_GEOLOCALIZATION";
                    dataCommand.Parameters.AddWithValue("@P_GeoLocalizationID", _geoLocalization.GeoLocalizationID);
                    dataCommand.Parameters.AddWithValue("@P_EmployeeID", _geoLocalization.EmployeeID);
                    dataCommand.Parameters.AddWithValue("@P_LocalName", _geoLocalization.LocalName);
                    dataCommand.Parameters.AddWithValue("@P_Lat", _geoLocalization.Lng);
                    dataCommand.Parameters.AddWithValue("@P_Long", _geoLocalization.Lat);

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

        public override IEnumerable<GeoLocalizationEntity> GetAllByCompany(GeoLocalizationEntity _geoLocalization)
        {
            IList<GeoLocalizationEntity> list = new List<GeoLocalizationEntity>();
            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {
                    dataCommand.CommandText = "SELECT  GeoLocalizationID " +
                                              ",EmployeeID" +
                                              ",LocalName " +
                                              ",Lat " +
                                              ",Lng " +
                                              "FROM GeoLocalization ";
                                              //"LIMIT 100";
                    
                    dataCommand.CommandType = CommandType.Text;

                    try
                    {
                        dataCommand.Connection = conn;
                        conn.Open();

                        var reader = dataCommand.ExecuteReader();

                        if (reader.HasRows)
                            list = Mapper.DataReaderMapToList<GeoLocalizationEntity>(reader);

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

        public override GeoLocalizationEntity FindByID(GeoLocalizationEntity _geoLocalization)
        {
            IList<GeoLocalizationEntity> list = new List<GeoLocalizationEntity>();
            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {
                    dataCommand.CommandText = "SELECT GeoLocalizationID " +
                                              ",EmployeeID" +
                                              ",LocalName " +
                                              ",Lat " +
                                              ",Lng " +
                                              "FROM GeoLocalization " +
                                              "Where GeoLocalizationID = @GeoLocalizationID";

                    dataCommand.Parameters.AddWithValue("@GeoLocalizationID", _geoLocalization.GeoLocalizationID);
                    dataCommand.CommandType = CommandType.Text;

                    try
                    {
                        dataCommand.Connection = conn;
                        conn.Open();

                        var reader = dataCommand.ExecuteReader();

                        if (reader.HasRows)
                            list = Mapper.DataReaderMapToList<GeoLocalizationEntity>(reader);
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

        public override string Delete(GeoLocalizationEntity _geoLocalization)
        {
            var returnMessage = "";
            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {
                    dataCommand.CommandType = CommandType.StoredProcedure;
                    dataCommand.CommandText = "PROC_DELETE_GEOLOCALIZATION";

                    dataCommand.Parameters.AddWithValue("@P_EmployeeID", _geoLocalization.EmployeeID);
                    dataCommand.Parameters.AddWithValue("@P_GeoLocalizationID", _geoLocalization.GeoLocalizationID);

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

       
    }
}