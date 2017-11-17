using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticCenterBillManagementSystemWebApp.Model;

namespace DiagnosticCenterBillManagementSystemWebApp.Gateway
{
    public class TypeNameGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;
        public int SaveType(string typeName)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO test_type VALUES(@name)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", typeName);
            connection.Open();
            int rowaffected = command.ExecuteNonQuery();
            connection.Close();

            return rowaffected;
        }

        public List<TestType> GetAllTypeNames()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM test_type ORDER BY name";
            SqlCommand command = new SqlCommand(query, connection);
            List<TestType> testTypes = new List<TestType>();

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                TestType testType = new TestType();
                testType.Id = Convert.ToInt32(reader["id"]);
                testType.TypeName = reader["name"].ToString();

                testTypes.Add(testType);
            }
            reader.Close();
            connection.Close();

            return testTypes;
        }

        public string GetSearchedType(string typeName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT DISTINCT name FROM test_type WHERE name=@typeName";
            SqlCommand command=new SqlCommand(query, connection);
            string test = null;
            command.Parameters.AddWithValue("@typeName", typeName);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                test = reader["name"].ToString();
            }
            reader.Close();
            connection.Close();
            return test;
        }
    }
}