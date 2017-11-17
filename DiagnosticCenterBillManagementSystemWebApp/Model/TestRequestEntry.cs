using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterBillManagementSystemWebApp.Model
{
    [Serializable]
    public class TestRequestEntry
    {
        public int Id { get; set; }
        public string NameOfPatient { get; set; }
        public string DateOfBirth { get; set; }
        public string MobileNo { get; set; }
        public double TotalFee { get; set; }
        public string BillNo { get; set; }
        public string TestName { get; set; }
        public double Fee { get; set; }
        public string InvoiceDate { get; set; }
        public string Status { get; set; }
    }
}