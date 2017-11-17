using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterBillManagementSystemWebApp.Model
{
    [Serializable]
    public class SelectedTestAndFee
    {
        public string SelectedTest { get; set; }
        public double Fee { get; set; }
    }
}