using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Geo.Localization.Data.IRepository;
using Geo.Localization.Data.Utils;
using MySql.Data.MySqlClient;

namespace Geo.Localization.Data.Repository
{
    public class CompanyRepository : GenericRepository<CompanyEntity>, ICompanyRepository
    {
        public override CompanyEntity FindByID(int id)
        {
            IList<CompanyEntity> list = new List<CompanyEntity>();
            using (var conn = new DBLoadMySql().cnn)
            {
                using (var dataCommand = new MySqlCommand())
                {

                    dataCommand.CommandText = "SELECT CompanyID" +
                                              ",CompanyType"+
                                              ",Name " +
                                              ",Address " +
                                              ",PostCode" +
                                              ",State" +
                                              ",Country" +
                                              ",Email" +
                                              ",WebSite" +
                                              ",Phone" +
                                              ",DateCreated " +
                                              "FROM Company " +
                                              "Where CompanyID = @CompanyID ";

                    dataCommand.Parameters.AddWithValue("@CompanyID", id);
                    dataCommand.CommandType = CommandType.Text;

                    try
                    {
                        dataCommand.Connection = conn;
                        conn.Open();

                        var reader = dataCommand.ExecuteReader();

                        if (reader.HasRows)
                            list = Mapper.DataReaderMapToList<CompanyEntity>(reader);
                        reader.Close();
                    }
                    catch (SqlException e)
                    {
                        Debug.WriteLine(string.Format("Error: {0} ", e.Message));
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
    }
}