using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticCenterBillManagementSystemWebApp.Model;

namespace DiagnosticCenterBillManagementSystemWebApp.Gateway
{
    public class TestRequestEntryGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;

        public double GetFeeForSelectedtest(string selectedTest)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            double fee = 0;
            string query = "SELECT * FROM test_setup WHERE testName=@testName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testName", selectedTest);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                fee = Convert.ToDouble(reader["fee"]);
            }
            reader.Close();
            connection.Close();

            return fee;
        }

        public int SavePatientData(TestRequestEntry entry)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO testRequestEntry VALUES(@name, @dob, @mobile, @total, @billNo, @status,@invoiceDate)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", entry.NameOfPatient);
            command.Parameters.AddWithValue("@dob", entry.DateOfBirth);
            command.Parameters.AddWithValue("@mobile", entry.MobileNo);
            command.Parameters.AddWithValue("@total", entry.TotalFee);
            command.Parameters.AddWithValue("@billNO", entry.BillNo);
            command.Parameters.AddWithValue("@status", "Unpaid");
            command.Parameters.AddWithValue("@invoiceDate", entry.InvoiceDate);

            connection.Open();
            int rowaffected = command.ExecuteNonQuery();
            connection.Close();

            return rowaffected;
        }

        public int GetTestId(string testName)
        {
            int testId = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM test_setup WHERE testName = @testName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testName", testName);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                testId = Convert.ToInt32(reader["id"]);
            }

            reader.Close();
            connection.Close();
            return testId;
        }

        public int GetPatientId(string billNo)
        {
            int patientId = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM testRequestEntry WHERE billNo=@bill";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bill", billNo);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                patientId = Convert.ToInt32(reader["id"]);
            }

            reader.Close();
            connection.Close();
            return patientId;
        }

        public int SaveBillInfo(int testId, int patientId, string date)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO testAndPatientData VALUES(@testId, @patientId, @date)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testId", testId);
            command.Parameters.AddWithValue("@patientId", patientId);
            command.Parameters.AddWithValue("@date", date);

            connection.Open();
            int rowaffected = command.ExecuteNonQuery();

            connection.Close();
            return rowaffected;
        }

        public List<TestRequestEntry> GetAllPatient()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM testRequestEntry";
            List<TestRequestEntry> testRequestEntries = new List<TestRequestEntry>();
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                TestRequestEntry entry = new TestRequestEntry();
                entry.NameOfPatient = reader["nameOfPatient"].ToString();
                entry.DateOfBirth = reader["dateOfBirth"].ToString();
                entry.MobileNo = reader["mobileNo"].ToString();
                entry.BillNo = reader["billNo"].ToString();
                entry.TotalFee = Convert.ToDouble(reader["totalFee"]);
                entry.InvoiceDate = reader["invoiceDate"].ToString();
                entry.Status = reader["paymentStatus"].ToString();

                testRequestEntries.Add(entry);
            }
            reader.Close();
            connection.Close();
            return testRequestEntries;
        }
        public TestRequestEntry GetSearchedPatientDetails(string mobileNo, string billNo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM testRequestEntry WHERE mobileNo=@mobileNo OR billNo=@billNo";
            TestRequestEntry entry = null;
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@mobileNo", mobileNo);
            command.Parameters.AddWithValue("@billNo", billNo);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                entry=new TestRequestEntry();
                entry.NameOfPatient = reader["nameOfPatient"].ToString();
                entry.DateOfBirth= reader["dateOfBirth"].ToString();
                entry.MobileNo = reader["mobileNo"].ToString();
                entry.BillNo = reader["billNo"].ToString();
                entry.TotalFee = Convert.ToDouble(reader["totalFee"]);
                entry.Status = reader["paymentStatus"].ToString();
                DateTime dateTime = (DateTime) reader["invoiceDate"];
                entry.InvoiceDate = dateTime.ToString("d");
                //entry.InvoiceDate = reader["invoiceDate"].ToString();
            }
            reader.Close();
            connection.Close();
            return entry;
        }
        public TestRequestEntry GetSearchedPatientDetailsWithBillNo(string billNo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM testRequestEntry WHERE billNo=@billno";
            TestRequestEntry entry = null;
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@billNo", billNo);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                entry=new TestRequestEntry();
                entry.NameOfPatient = reader["nameOfPatient"].ToString();
                entry.DateOfBirth = reader["dateOfBirth"].ToString();
                entry.MobileNo = reader["mobileNo"].ToString();
                entry.BillNo = reader["billNo"].ToString();
                entry.TotalFee = Convert.ToDouble(reader["totalFee"]);
                entry.Status = reader["paymentStatus"].ToString();
                entry.InvoiceDate = reader["invoiceDate"].ToString();
            }
            reader.Close();
            connection.Close();
            return entry;
        }
        public List<TestRequestEntry> GetAllUnpaidPatient(string fromDate, string toDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT DISTINCT * FROM testRequestEntry WHERE invoiceDate BETWEEN @fromDate AND @toDate AND paymentStatus=@status";
            List<TestRequestEntry> testRequestEntries = new List<TestRequestEntry>();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            command.Parameters.AddWithValue("@toDate", toDate);
            command.Parameters.AddWithValue("@status", "Unpaid");

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                TestRequestEntry entry = new TestRequestEntry();
                entry.NameOfPatient = reader["nameOfPatient"].ToString();
                entry.DateOfBirth = reader["dateOfBirth"].ToString();
                entry.MobileNo = reader["mobileNo"].ToString();
                entry.BillNo = reader["billNo"].ToString();
                entry.TotalFee = Convert.ToDouble(reader["totalFee"]);
                entry.InvoiceDate = reader["invoiceDate"].ToString();
                entry.Status = reader["paymentStatus"].ToString();

                testRequestEntries.Add(entry);
            }
            reader.Close();
            connection.Close();
            return testRequestEntries;
        }
    }
}