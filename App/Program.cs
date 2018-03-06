using System;
using System.Data;
using System.Data.SqlClient;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = @"
                    Server=(LocalDB)\MSSQLLocalDB;
                    Integrated Security=true;
                    AttachDbFileName=" +
                        Environment.CurrentDirectory +
                        "\\data\\NORTHWND.MDF";

            SqlConnection con = new SqlConnection(connStr);
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT ProductID, ProductName FROM Products";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    Console.WriteLine(dr["ProductName"]);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }

        }
    }
}
