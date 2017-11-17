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
    public class PaymentGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;
        public int UpdatePaymentStatus(double amount, string billNo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE testRequestEntry SET totalFee=@totalFee, paymentStatus=@status WHERE billNo=@bill";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@totalFee", amount);
            command.Parameters.AddWithValue("@status", "Paid");
            command.Parameters.AddWithValue("@bill", billNo);

            connection.Open();
            int rowaffected=command.ExecuteNonQuery();
            return rowaffected;
        }
    }
}