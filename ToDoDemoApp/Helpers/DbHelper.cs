using System;
using System.Data.SqlClient;

namespace ToDoDemoApp.Helpers
{
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ToDoAppDb;");
        }
    }
}
