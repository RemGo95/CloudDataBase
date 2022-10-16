using System;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer;
using Azure.Core;

namespace _net5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting DataBase");
            ConnectionMSC();
            
        }

        static void Conn(){
            string ConnectionData;
            SqlConnection con;
            ConnectionData = @"Data Source=DESKOP-#####;Initial Catalog=Datadb;User ID=sa;Password=########";
            con = new SqlConnection(ConnectionData);
            con.Open();
            Console.WriteLine("Connection...");
            con.Close(); 
        }

        static void ConnectionMSC()
        {
            //Connect by method from Microsoft manual
            try
            {
                 SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "<your_server.database.windows.net>"; 
                builder.UserID = "<your_username>";            
                builder.Password = "<your_password>";     
                builder.InitialCatalog = "<your_database>";
         
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");
                    
                    connection.Open();       

                    String sql = "SELECT name, collation_name FROM sys.databases";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }                    
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Problem with Sql connection");
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine(); 

        }
    }
}
