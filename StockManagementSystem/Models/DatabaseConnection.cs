using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Models
{
    class DatabaseConnection
    {
        string connectionString = @"Server=SHAKIKUL-PC\SQLEXPRESS;Database=StockManagementSystemDb;Integrated Security=true";
        public static SqlConnection sqlConnection=null;
        public void ConnectionDatabase()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }
    }
}
