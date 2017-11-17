using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemWebApp.Gateway;
using DiagnosticCenterBillManagementSystemWebApp.Model;

namespace DiagnosticCenterBillManagementSystemWebApp.BLL
{
    public class ReportManager
    {
        ReportGateway reportGateway = new ReportGateway();
        public List<TotalTest> GetSearchedTestData(string fromDate, string toDate)
        {
             return reportGateway.GetSearchedTestData(fromDate, toDate);
        }
        public List<TotalTest> GetSearchedTypeData(string fromDate, string toDate)
        {
             return reportGateway.GetSearchedTypeData(fromDate, toDate);
        }
    }
}