using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Data
{
    public class DataMapper
    {
        private static string _connString = "Data Source=C:\\skola\\csharp\\pokus\\Cyklopujcovna\\Cyklopujcovna\\bin\\Debug\\cyklo.db;";

        public static async Task Insert<T>(T obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] propertyInfo = type.GetProperties();
            List<string> sqlParts = new List<string>();
            List<string> columnName = new List<string>();
            List<object> parameterName = new List<object>();

            int counter = 0;
            sqlParts.Add($"INSERT INTO {type.Name} (");
            foreach (var property in propertyInfo)
            { 
                bool ignore = property.GetCustomAttributes().Any(x => x.GetType().Name == "KeyAttribute");
                if (ignore) { continue; }

                sqlParts.Add(property.Name);
                sqlParts.Add(", ");
                columnName.Add(property.Name);
                counter++;
                if (counter == propertyInfo.Length -1)
                {
                    sqlParts.RemoveAt(sqlParts.Count-1);
                }
            }

            counter = 0;
            sqlParts.Add(") VALUES (");
            foreach (var property in propertyInfo)
            {
                bool ignore = property.GetCustomAttributes().Any(x => x.GetType().Name == "KeyAttribute");
                if (ignore) { continue; }

                sqlParts.Add("@"+property.Name);
                sqlParts.Add(", ");
                parameterName.Add(property.GetValue(obj));
                counter++;
                if (counter == propertyInfo.Length -1)
                {
                    sqlParts.RemoveAt(sqlParts.Count-1);
                }
            }
            sqlParts.Add(");");
            string sqlcmd = string.Join("", sqlParts);

            try
            { 
                using (SqliteConnection conn = new SqliteConnection(_connString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = sqlcmd;
                        for (int i = 0; i < columnName.Count; i++)
                        {
                            cmd.Parameters.AddWithValue(columnName[i], parameterName[i]);
                        }
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public static async Task Update<T>(T obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] propertyInfo = type.GetProperties();
            List<string> sqlParts = new List<string>();
            List<string> columnName = new List<string>();
            List<object> parameterName = new List<object>();

            int counter = 0;
            string id = null;
            sqlParts.Add($"UPDATE {type.Name} SET");
            foreach (var property in propertyInfo)
            { 
                bool ignore = property.GetCustomAttributes().Any(x => x.GetType().Name == "KeyAttribute");
                if (ignore)
                {
                    id = property.Name;
                    continue;
                }

                sqlParts.Add(" "+property.Name);
                sqlParts.Add(" = ");
                sqlParts.Add("@"+property.Name);
                sqlParts.Add(", ");
                columnName.Add(property.Name);
                parameterName.Add(property.GetValue(obj));
                counter++;
                if (counter == propertyInfo.Length -1)
                {
                    sqlParts.RemoveAt(sqlParts.Count-1);
                }

            }
            object idValue = propertyInfo[0].GetValue(obj);
            sqlParts.Add($" WHERE {id} = @{id};");
            string sqlcmd = string.Join("", sqlParts);

            try
            { 
                using (SqliteConnection conn = new SqliteConnection(_connString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = sqlcmd;
                        for (int i = 0; i < columnName.Count; i++)
                        {
                            cmd.Parameters.AddWithValue(columnName[i], parameterName[i]);
                        }

                        cmd.Parameters.AddWithValue(id, idValue);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }
        public static async Task Delete<T>(T obj)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperties()[0];
            string sqlcmd = $"DELETE FROM {type.Name} WHERE {propertyInfo.Name} = @{propertyInfo.Name}";
            try
            { 
                using (SqliteConnection conn = new SqliteConnection(_connString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = sqlcmd;
                        cmd.Parameters.AddWithValue(propertyInfo.Name, propertyInfo.GetValue(obj));
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public static async Task<BindingList<T>> Select<T>()
        {
            BindingList<T> list = new BindingList<T>();
            Type type = typeof(T);
            var propertyInfo = type.GetProperties();

            try
            {
                using (SqliteConnection conn = new SqliteConnection(_connString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = $"select * from {type.Name};";
                        using (SqliteDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                T obj = (T)Activator.CreateInstance(type);
                                foreach (var prop in propertyInfo)
                                {
                                   var value = reader.GetString(reader.GetOrdinal($"{prop.Name}"));
                                   Type propertyType = prop.PropertyType;
                                   prop.SetValue(obj,Convert.ChangeType(value,propertyType));
                                }
                                list.Add(obj);
                            }
                        }
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }

        public static async Task<bool> Login<T>(T obj)
        {
            Type type = typeof(T);
            var propertyInfo = type.GetProperties();

            List<string> sqlParts = new List<string>();
            List<string> columnName = new List<string>();
            List<object> parameterName = new List<object>();

            int counter = 0;
            sqlParts.Add($"SELECT * FROM {type.Name} WHERE");
            foreach (var property in propertyInfo)
            { 
                bool ignore = property.GetCustomAttributes().Any(x => x.GetType().Name == "KeyAttribute");
                if (ignore)
                {
                    continue;
                }

                sqlParts.Add(" "+property.Name);
                sqlParts.Add(" = ");
                sqlParts.Add("@"+property.Name);
                sqlParts.Add(" and ");
                columnName.Add(property.Name);
                parameterName.Add(property.GetValue(obj));
                counter++;
                if (counter == propertyInfo.Length -1)
                {
                    sqlParts.RemoveAt(sqlParts.Count-1);
                }

            }

            string sqlcmd = string.Join("", sqlParts);

            try
            {
                using (SqliteConnection conn = new SqliteConnection(_connString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = sqlcmd;
                        for (int i = 0; i < columnName.Count; i++)
                        {
                            cmd.Parameters.AddWithValue(columnName[i], parameterName[i]);
                        }
                        using (SqliteDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (!reader.HasRows)
                            {
                                return false;
                            }

                            return true;


                        }
                    }
                }


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static async Task<T> SelectById<T>(T obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] propertyInfo = type.GetProperties();
            string sqlcmd = $"SELECT * FROM {type.Name} WHERE {propertyInfo[0].Name} = @{propertyInfo[0].Name}";
            try
            {
                T retObj = default;
                using (SqliteConnection conn = new SqliteConnection(_connString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = sqlcmd;
                        cmd.Parameters.AddWithValue(propertyInfo[0].Name, propertyInfo[0].GetValue(obj));
                        using (SqliteDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                retObj = (T)Activator.CreateInstance(type);
                                foreach (var prop in propertyInfo)
                                {
                                    var value = reader.GetString(reader.GetOrdinal($"{prop.Name}"));
                                    Type propertyType = prop.PropertyType;
                                    prop.SetValue(retObj,Convert.ChangeType(value,propertyType));
                                }
                                
                            }
                        }
                    }
                }

                return retObj;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
