using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticCenterBillManagementSystemWebApp.Model;

namespace DiagnosticCenterBillManagementSystemWebApp.Gateway
{
    public class TestSetupGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;
        public int SaveTest(TestSetup testSetup)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO test_setup VALUES(@testName, @fee, @type)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testName", testSetup.TestName);
            command.Parameters.AddWithValue("@fee", testSetup.Fee);
            command.Parameters.AddWithValue("@type", testSetup.TestType);

            connection.Open();
            int rowaffected = command.ExecuteNonQuery();
            connection.Close();
            return rowaffected;
        }

        public List<TestAndType> ShowAllTestsAndTypes()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM TestAndType ORDER BY testName";
            SqlCommand command = new SqlCommand(query, connection);

            List<TestAndType> testSetups = new List<TestAndType>();

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TestAndType testSetup = new TestAndType();
                testSetup.TestName = reader["testName"].ToString();
                testSetup.Fee = Convert.ToDouble(reader["fee"]);
                testSetup.TypeName = reader["name"].ToString();

                testSetups.Add(testSetup);
            }
            reader.Close();
            connection.Close();

            return testSetups;
        }

        public List<TestSetup> ShowAllTests()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM test_setup ORDER BY testName";
            SqlCommand command = new SqlCommand(query, connection);

            List<TestSetup> testSetups = new List<TestSetup>();

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TestSetup testSetup = new TestSetup();
                testSetup.TestName = reader["testName"].ToString();
                testSetup.Fee = Convert.ToDouble(reader["fee"]);
                testSetup.TestType = Convert.ToInt32(reader["typeName"]);

                testSetups.Add(testSetup);
            }
            reader.Close();
            connection.Close();

            return testSetups;
        }
        public int ShowSearchedTests(string testName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            int count = 0;

            string query = "SELECT DISTINCT * FROM test_setup WHERE testName=@testName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testName", testName);
            

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                count = 1;
            }

            reader.Close();
            connection.Close();

            return count;
        }
    }
}