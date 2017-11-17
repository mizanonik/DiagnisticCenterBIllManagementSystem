using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemWebApp.Gateway;
using DiagnosticCenterBillManagementSystemWebApp.Model;
using Org.BouncyCastle.Pkix;

namespace DiagnosticCenterBillManagementSystemWebApp.BLL
{
    public class TestRequestEntryManager
    {
        private TestRequestEntryGateway testRequestEntryGateway = new TestRequestEntryGateway();

        public double GetFeeForSelectedTest(string selectedTest)
        {
            return testRequestEntryGateway.GetFeeForSelectedtest(selectedTest);
        }

        public string SavePatientData(TestRequestEntry entry)
        {
            List<TestRequestEntry> testRequestEntries = testRequestEntryGateway.GetAllPatient();
            int checker = 0;
            foreach (TestRequestEntry testRequest in testRequestEntries)
            {
                if (testRequest.MobileNo == entry.MobileNo)
                {
                    checker = 1;
                }
            }
            if (checker > 0)
            {
                return "Mobile No Already Exists";
            }
            else
            {
                if (testRequestEntryGateway.SavePatientData(entry) > 0)
                {
                    return "Patient Saved Successfully";
                }
                return "Patient Saving Failed";

            }
        }

        public string SaveBillInfo(TestRequestEntry entry)
        {
            int testId = testRequestEntryGateway.GetTestId(entry.TestName);
            int patientId = testRequestEntryGateway.GetPatientId(entry.BillNo);
            string date = entry.InvoiceDate;

            if (testRequestEntryGateway.SaveBillInfo(testId, patientId, date) > 0)
            {
                return "Info Saved";
            }
            return "Saving Failed";
        }

        public TestRequestEntry GetSearchedPatientDetails(string mobileNo, string billNo)
        {
            return testRequestEntryGateway.GetSearchedPatientDetails(mobileNo, billNo);
        }

        public TestRequestEntry GetSearchedPatientDetailsWithBill(string billNo)
        {
            return testRequestEntryGateway.GetSearchedPatientDetailsWithBillNo(billNo);
        }

        public List<TestRequestEntry> GetUnpaidPatients(string fromDate, string toDate)
        {
            return testRequestEntryGateway.GetAllUnpaidPatient(fromDate, toDate);
        }
    }
}