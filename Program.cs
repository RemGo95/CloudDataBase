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
            Choose();
        }

        static void Choose(){
            //char input=Console.ReadKey().KeyChar;
            ConsoleKeyInfo symbol;
            Console.WriteLine("Starting DataBase");
            Console.WriteLine("Choose SQL data handle method: (M-Connect via MSC method, C-Connect Simple, R-Read)");

            symbol = Console.ReadKey();

            switch (symbol.Key) //Switch on Key enum
            {
                case ConsoleKey.M:
                    ConnectionMSC();
                    break;
                case ConsoleKey.C:
                    Conn();
                    break;
                case ConsoleKey.R:
                    Read();
                    break;
            }

            ConnectionMSC();

        }

        static void Conn(){
            Console.WriteLine("Connecting to db...");
            string ConnectionData;
            SqlConnection con;
            ConnectionData = @"Data Source=DESKOP-#####;Initial Catalog=Datadb;User ID=sa;Password=########";
            con = new SqlConnection(ConnectionData);
            con.Open();
            Console.WriteLine("Connection...");
            con.Close(); 
        }

        static void Read()
        {
            Console.WriteLine("Reading from db...");
            string ConnectionData;
            SqlConnection con;
            ConnectionData = @"Data Source=DESKOP-#####;Initial Catalog=Datadb;User ID=sa;Password=########";
            con = new SqlConnection(ConnectionData);
            con.Open();
            SqlCommand cmd;
            SqlDataReader dreader;
            string sql, output = "";
            sql = "Select articleID, articleName from Datadb";
            cmd = new SqlCommand(sql, con);
            dreader = cmd.ExecuteReader();
            while (dreader.Read()) {
                output = output + dreader.GetValue(0) + " - " +
                                dreader.GetValue(1) + "\n";
            }

            Console.Write(output);
 
            // to close all the objects
            dreader.Close();
            cmd.Dispose();
            con.Close();
        }

        static void ConnectionMSC()
        {
            Console.WriteLine("Connection via msc db...");
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
