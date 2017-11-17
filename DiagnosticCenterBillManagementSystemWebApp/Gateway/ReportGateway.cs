using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticCenterBillManagementSystemWebApp.BLL;
using DiagnosticCenterBillManagementSystemWebApp.Model;

namespace DiagnosticCenterBillManagementSystemWebApp.Gateway
{
    public class ReportGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;
        TestSetupGateway testSetupGateway = new TestSetupGateway();
        TypeNameGateway typeName = new TypeNameGateway();
        public List<TotalTest> GetSearchedTestData(string fromDate, string toDate)
        {
            List<TestSetup> testSetups = testSetupGateway.ShowAllTests();
            List<TotalTest> tests = new List<TotalTest>();
            foreach (TestSetup test in testSetups)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string query = "SELECT * FROM totalTestAndTypeWithCount WHERE invoiceDate BETWEEN @fromDate AND @toDate AND testName=@testName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
                command.Parameters.AddWithValue("@testName", test.TestName);
                double amount = 0;
                int count = 0;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count += 1;
                    amount += Convert.ToDouble(reader["fee"]);
                }

                TotalTest totalTest = new TotalTest();
                totalTest.TestName = test.TestName;
                totalTest.TestCount = count;
                totalTest.TotalAmount = amount;

                tests.Add(totalTest);
            }
            return tests;
        } 
        public List<TotalTest> GetSearchedTypeData(string fromDate, string toDate)
        {
            List<TestType> testTypes = typeName.GetAllTypeNames();
            List<TotalTest> tests = new List<TotalTest>();
            foreach (TestType type in testTypes)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string query = "SELECT * FROM totalTestAndTypeWithCount WHERE invoiceDate BETWEEN @fromDate AND @toDate AND typeName=@typeName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
                command.Parameters.AddWithValue("@typeName", type.TypeName);
                double amount = 0;
                int count = 0;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count += 1;
                    amount += Convert.ToDouble(reader["fee"]);
                }

                TotalTest totalTest = new TotalTest();
                totalTest.TypeName = type.TypeName;
                totalTest.TestCount = count;
                totalTest.TotalAmount = amount;

                tests.Add(totalTest);
            }
            return tests;
        } 
        //public List<TotalTest> GetSearchedTestData(string fromDate, string toDate)
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);
        //    string query = "SELECT typeName, testName, fee, testId, COUNT(testName) AS totalTest FROM totalTestAndTypeWithCount WHERE invoiceDate BETWEEN @fromDate AND @toDate GROUP BY typeName, testName, fee, testId";

        //    List<TotalTest> totalTests = new List<TotalTest>(); ;

        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Parameters.AddWithValue("@fromDate", fromDate);
        //    command.Parameters.AddWithValue("@toDate", toDate);

        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        TotalTest test = new TotalTest();
        //        test.TestName = reader["testName"].ToString();
        //        test.Fee = Convert.ToDouble(reader["fee"]);
        //        test.TestId = Convert.ToInt32(reader["testId"]);
        //        test.TestCount = Convert.ToInt32(reader["totalTest"]);
        //        test.TotalAmount=test.Fee*test.TestCount;

        //        totalTests.Add(test);
        //    }
        //    reader.Close();
        //    connection.Close();

        //    return totalTests;
        //}
        //public List<TotalTest> GetSearchedTypeData(string fromDate, string toDate)
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);
        //    string query = "SELECT typeName, COUNT(typeName) AS totalTypes,COUNT(testId) AS testId, SUM(fee) AS totalAmount FROM totalTestAndTypeWithCount GROUP BY typeName";

        //    List<TotalTest> totalTests = new List<TotalTest>(); ;

        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Parameters.AddWithValue("@fromDate", fromDate);
        //    command.Parameters.AddWithValue("@toDate", toDate);

        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        TotalTest test = new TotalTest();
        //        test.TypeName = reader["typeName"].ToString();
        //        test.TestCount = Convert.ToInt32(reader["totalTypes"]);
        //        if (reader["totalAmount"]!=DBNull.Value)
        //        {
        //            test.TotalAmount = Convert.ToDouble(reader["totalAmount"]);
        //        }
        //        test.TestId = Convert.ToInt32(reader["testId"]);
        //        totalTests.Add(test);
        //    }
        //    reader.Close();
        //    connection.Close();

        //    return totalTests;
        //}
    }
}