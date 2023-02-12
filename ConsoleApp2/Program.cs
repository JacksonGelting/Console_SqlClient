using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String connString = "";                                                         //Connection String we will use to gain access to the Sql server
            connString += "Data Source =;";                  //location of server, in this case local instance
            connString += "Initial Catalog=;";
            connString += "Integrated Security=true";


            try
            {
                Console.WriteLine("Connecting to sql server...");
                using (SqlConnection connection = new SqlConnection(connString))            //two things of note. using() is for when you are using a disposable object, and SqlConnection is a class used for mannaging connections
                {
                    connection.Open();
                   

                    void opening ()
                    {
                        Console.WriteLine("Done.\nHere the options\n\n");
                        Console.WriteLine("1 - display table");
                        Console.WriteLine("2 - append a new record to the table");
                        Console.WriteLine("3 - delete a record from the table");
                        Console.WriteLine("4 - quit");

                    }
                    opening();
                    bool t = true;
                    while (t)
                    {
                        String s = Console.ReadLine();

                        switch (s)
                        {
                            case "1":
                                displayAll(connection);
                                break;
                            case "2":
                                Console.WriteLine("insert @name then @location");
                                String name = Console.ReadLine();
                                Console.WriteLine("name = " + name);
                                String location = Console.ReadLine();
                                Console.WriteLine("location = " + location);

                                Console.WriteLine("please double check the information above before peceding, if an error is made enter q, else hit any key");
                                if (Console.ReadLine().Equals("q")) break;

                                append(connection, name, location);
                                



                                break;
                            case "3":
                                break;
                            case "4":
                                t=false;
                                break;
                            default:
                                opening();
                                break;
                        }


                        
                    }
                    


                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);


            void displayAll(SqlConnection connection)
            {
                String sql = "SELECT * FROM Employees;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        }

                    }
                }

            }

            void append(SqlConnection connection, String name, String location )
            {
                String sql = "INSERT Employees (Name, Location) ";
                sql += "VALUES (@name, @location);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@location", location);
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected + "row(s) inserted");

                }


            }

        }




         
    }

    
}
