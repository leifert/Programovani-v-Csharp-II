using System;
using System.Collections.Generic;
using System.IO;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer()
            {
                Name = "Jan",
                Address = "17. listopadu"
            };

            Order order = new Order()
            {
                CustomerId = 1,
                Product = "Neco",
                Price = 100
            };

            string sql = File.ReadAllText("database-create.sql");

            using (SqliteConnection conn = new SqliteConnection("Data Source=mydb.db;"))
            {
                conn.Open();

               

                using (SqliteTransaction tran = conn.BeginTransaction())
                {
                    conn.Execute("INSERT INTO Customer (Name, Address) VALUES (@Name, @Address)",customer);

                    IEnumerable<Customer> customers = conn.Query<Customer>("SELECT * FROM Customer");

                    foreach (Customer dbcust in customers)
                    {
                        Console.WriteLine(dbcust.Id + " | "+ dbcust.Name + " | "+ dbcust.Address);
                    }

                    long count = conn.ExecuteScalar<long>("SELECT (*) FROM Customer");

                    using (SqliteCommand cmd = new SqliteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        cmd.CommandText = $"INSERT INTO Customer (Name, Address) VALUES (@Name, @Address)";
                        cmd.Parameters.AddWithValue("Name", customer.Name);
                        cmd.Parameters.AddWithValue("Address", customer.Address == null ? DBNull.Value : customer.Address);
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                }


                //insert customer
                using (SqliteCommand cmd = new SqliteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"INSERT INTO Customer (Name, Address) VALUES (@Name, @Address)";
                    cmd.Parameters.AddWithValue("Name", customer.Name);
                    cmd.Parameters.AddWithValue("Address", customer.Address == null ? DBNull.Value : customer.Address);
                    cmd.ExecuteNonQuery();
                }
                //insert order
                using (SqliteCommand cmd = new SqliteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO \"Order\" (CustomerId, Product, Price) VALUES (@CustomerId, @Product, @Price)";
                    cmd.Parameters.AddWithValue("CustomerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("Product", order.Product);
                    cmd.Parameters.AddWithValue("Price", order.Price);
                    cmd.ExecuteNonQuery();
                }

                using (SqliteCommand cmd = new SqliteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Customer";
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer dbcust = new Customer()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address"))? null : reader.GetString(reader.GetOrdinal("Address"))
                            };
                            Console.WriteLine(dbcust.Id + " | "+ dbcust.Name + " | "+ dbcust.Address);
                        }
                    }
                }

                using (SqliteCommand cmd = new SqliteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) FROM Customer";
                    long count = (long)cmd.ExecuteScalar();
                    Console.WriteLine("Zakazniku celkem:" + count);

                }


                using (SqliteCommand cmd = new SqliteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM \"Order\"";
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order dborder = new Order()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                Product = reader.GetString(reader.GetOrdinal("Product")),
                                Price = reader.GetInt32(reader.GetOrdinal("Price"))
                            };
                            Console.WriteLine(dborder.Id + " | "+ dborder.CustomerId + " | "+ dborder.Product + " | "+ dborder.Price);
                        }
                    }
                }

                conn.Close();
            }

            
           
        }
    }
}
