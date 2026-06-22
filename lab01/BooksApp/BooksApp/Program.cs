using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BooksApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=user-ПК\SQLEXPRESS;Initial Catalog=TEST2301;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("State:      \t" + connection.State);
                Console.WriteLine("DataSource: \t" + connection.DataSource);
                Console.WriteLine("Database:   \t" + connection.Database);
                Console.WriteLine(Environment.NewLine);

                SqlCommand cmdDdisplayTable = new SqlCommand();
                cmdDdisplayTable.Connection = connection;
                cmdDdisplayTable.CommandText = "SELECT * FROM [dbo].[Table]";
                SqlDataReader reader = cmdDdisplayTable.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
                reader.Close();
               
                Console.WriteLine(Environment.NewLine);

                //*************************************************//
                
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = connection;
                cmdInsert.CommandText = "INSERT INTO [dbo].[Table] (BookID, Title, Author, Pages, PublishYear) VALUES (@BookID, @Title, @Author, @Pages, @PublishYear)";
                cmdInsert.Parameters.Add(new SqlParameter("@BookID", "4"));
                cmdInsert.Parameters.Add(new SqlParameter("@Title", "Test  Book"));
                cmdInsert.Parameters.Add(new SqlParameter("@Author", "Test  Author"));
                cmdInsert.Parameters.Add(new SqlParameter("@Pages", "100"));
                cmdInsert.Parameters.Add(new SqlParameter("@PublishYear", "2019"));
                
                int rowsAffected = cmdInsert.ExecuteNonQuery();
                Console.WriteLine("{0} rows affected by insert", rowsAffected);
                Console.WriteLine(Environment.NewLine);
                
               
                reader = cmdDdisplayTable.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4));
                }
                reader.Close();

                Console.WriteLine(Environment.NewLine);

                //delete
                using (SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Table] WHERE Title LIKE '%Test%'", connection))
                {
                    int rowsAffectedD = cmd.ExecuteNonQuery();
                    Console.WriteLine("{0} rows affect by delete", rowsAffectedD);
                }

                Console.WriteLine(Environment.NewLine);

                //display
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Table]", connection))
                using (SqlDataReader readerR = cmd.ExecuteReader())
                {
                    while (readerR.Read())
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",
                                          readerR.GetInt32(0),
                                          readerR.GetString(1),
                                          readerR.GetString(2),
                                          readerR.GetInt32(3),
                                          readerR.GetInt32(4));
                    }
                }


            }
            catch (SqlException ex)
            {
                Console.Write(ex);
            }
            finally
            {
                connection.Close();
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("State:      \t" + connection.State);
                Console.ReadKey();
            }
        }
    }
}
