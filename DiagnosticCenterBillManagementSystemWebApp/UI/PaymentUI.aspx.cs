using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterBillManagementSystemWebApp.BLL;
using DiagnosticCenterBillManagementSystemWebApp.Model;

namespace DiagnosticCenterBillManagementSystemWebApp.UI
{
    public partial class PaymentUI : System.Web.UI.Page
    {
        TestRequestEntryManager testRequestEntryManager = new TestRequestEntryManager();
        PaymentManager paymentManager = new PaymentManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                payButton.Enabled = false;
                amountTextBox.ReadOnly = true;
                dueDateTextBox.ReadOnly = true;
                resetButton.Visible = false;
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            if (mobileNoTextBox.Text==String.Empty && billNoTextBox.Text==String.Empty)
            {
                messageLabel.Text = "Please enter a Bill No or Mobile No.";
                return;
            }
            string mobileNo = mobileNoTextBox.Text;
            string billNo = billNoTextBox.Text;

            TestRequestEntry testRequestEntries = testRequestEntryManager.GetSearchedPatientDetails(mobileNo, billNo);
            if (testRequestEntries!=null)
            {
                billNoTextBox.Text = testRequestEntries.BillNo;
                mobileNoTextBox.Text = testRequestEntries.MobileNo;
                amountTextBox.Text = testRequestEntries.TotalFee.ToString();
                dueDateTextBox.Text = testRequestEntries.InvoiceDate;
                ViewState.Add("Bill", testRequestEntries.BillNo);
                billNoTextBox.ReadOnly = true;
                mobileNoTextBox.ReadOnly = true;
                resetButton.Visible = true;
                payButton.Enabled = true;
                messageLabel.Text=String.Empty;
            }
            else
            {
                messageLabel.Text = "No Record Found";
                billNoTextBox.Text=String.Empty;
                mobileNoTextBox.Text=String.Empty;
                amountTextBox.Text=String.Empty;
                dueDateTextBox.Text=String.Empty;
            }
        }

        protected void payButton_Click(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(amountTextBox.Text);
            string billNo = (string) ViewState["Bill"];
            messageLabel.Text= paymentManager.UpdatePaymentStatus(amount, billNo);
            mobileNoTextBox.Text = String.Empty;
            mobileNoTextBox.ReadOnly = false;
            billNoTextBox.Text = String.Empty;
            billNoTextBox.ReadOnly = false;
            resetButton.Visible = false;
            amountTextBox.Text = null;
            dueDateTextBox.Text = String.Empty;
            payButton.Enabled = false;
        }

        protected void resetButton_Click(object sender, EventArgs e)
        {
            mobileNoTextBox.Text=String.Empty;
            mobileNoTextBox.ReadOnly = false;
            billNoTextBox.Text=String.Empty;
            billNoTextBox.ReadOnly = false;
            resetButton.Visible = false;
            messageLabel.Text=String.Empty;
            amountTextBox.Text = null;
            dueDateTextBox.Text=String.Empty;
            payButton.Enabled = false;
        }
    }
}