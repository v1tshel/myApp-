using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace myApp_1
{
    internal class DataBase
    {
        SqlConnection con = new SqlConnection(@"Data Source=V1TSHEL;Initial Catalog=task;Integrated Security=True; Encrypt=False");


        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return con;
        }
    }
}

