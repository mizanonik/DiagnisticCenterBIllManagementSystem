using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterBillManagementSystemWebApp.Model
{
    public class TotalTest
    {
        public string TypeName { get; set; }
        public string TestName { get; set; }
        public double Fee { get; set; }
        public int TestId { get; set; }
        public int TestCount { get; set; }
        public double TotalAmount { get; set; }
        public double FinalAmount { get; set; }
        public string InvoiceDate { get; set; }
    }
}