using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemWebApp.Gateway;
using DiagnosticCenterBillManagementSystemWebApp.Model;

namespace DiagnosticCenterBillManagementSystemWebApp.BLL
{
    public class PaymentManager
    {
        PaymentGateway paymentGateway = new PaymentGateway();
        TestRequestEntryManager testRequestEntryManager = new TestRequestEntryManager();

        public string UpdatePaymentStatus(double inputAmount, string billNo)
        {
            TestRequestEntry test = testRequestEntryManager.GetSearchedPatientDetailsWithBill(billNo);
            double searchedAmount = test.TotalFee;
            if (searchedAmount>0)
            {
                double amount = searchedAmount - inputAmount;
                if (paymentGateway.UpdatePaymentStatus(amount, billNo) > 0)
                {
                    return "Payment successfull";
                }
                return "Payment count not be made.";
            }
            return "Already Paid";
        }
    }
}