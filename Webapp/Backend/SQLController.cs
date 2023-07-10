using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZadanieZCT.Backend
{

    public static class SQLController
    {

        private const string connectionString = "Server=tcp:<servername>.database.windows.net,1433;"
                                                + "Initial Catalog=<yourDB>;"
                                                + "Persist Security Info=False;"
                                                + "User ID=<usernamehere>;"
                                                + "Password=<passhere>;"
                                                + "MultipleActiveResultSets=False;"
                                                + "Encrypt=True;"
                                                + "TrustServerCertificate=False;"
                                                + "Connection Timeout=30;";

        private static String SQLQueryBuilderINSERT()
        {
            String sql_query = "INSERT INTO dbo.env_sensors (temp, hum, press, created_at) values(@temperature,@pressure,@humidity,@created_at)";

            return sql_query;
        }

        private static String SQLQueryBuilderGET(int lastNRows)
        {
            String sql_query = "SELECT temp,hum,press,created_at FROM dbo.env_sensors WHERE ID IN (select TOP "
                + lastNRows.ToString()
                + " id from dbo.env_sensors order by id DESC)";
            return sql_query;
        }


        public static void UpdateDB(EnvSensorAWS new_env_vals)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO dbo.env_sensors (temp, hum, press, created_at)";
                    query += " VALUES (@temperature, @humidity, @pressure, @created_at)";

                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@temperature", new_env_vals.GetTemperature().ToString());
                    command.Parameters.AddWithValue("@pressure", new_env_vals.GetPressure().ToString());
                    command.Parameters.AddWithValue("@humidity", new_env_vals.GetHumidity().ToString());
                    command.Parameters.AddWithValue("@created_at", new_env_vals.GetCreated_at());
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }
        public static List<EnvSensorSQL> GetFromDB(int lastNRows)
        {
            List<EnvSensorSQL> env_vals = new List<EnvSensorSQL>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    using (SqlCommand command = new SqlCommand(SQLQueryBuilderGET(lastNRows), connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                env_vals.Add(new EnvSensorSQL(Convert.ToDouble(reader.GetValue(0)),
                                                           Convert.ToDouble(reader.GetValue(1)),
                                                           Convert.ToDouble(reader.GetValue(2)),
                                                           Convert.ToDateTime(reader.GetValue(3))));
                            }
                        }
                    }
                }
            }
            catch (SqlException ee)
            {
                Console.WriteLine(ee.ToString());
            }
            return env_vals;
        }
    }
}