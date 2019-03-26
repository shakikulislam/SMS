using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.DAL
{
    public class CompanyDal
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        private static SqlConnection _sqlConnection = new SqlConnection(_connectionString);
        private static SqlCommand _sqlCommand = new SqlCommand("", _sqlConnection);
        private static SqlDataReader _sqlDataReader;
        private static SqlDataAdapter _sqlDataAdapter;
        private static DataTable _dataTable;
    }
}
