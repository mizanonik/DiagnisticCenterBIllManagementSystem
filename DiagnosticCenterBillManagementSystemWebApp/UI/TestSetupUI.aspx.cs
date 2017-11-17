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
    public partial class TestSetupUI : System.Web.UI.Page
    {
        TestSetupManager manager = new TestSetupManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowAllTestData();
                LoadTestDropDown();
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            TestSetup testSetup = new TestSetup();
            testSetup.TestName = testNameTextBox.Text;
            if (feeTextBox.Text != String.Empty)
            {
                testSetup.Fee = Convert.ToDouble(feeTextBox.Text);
            }
            else
            {
                testSetup.Fee=0;
            }
            testSetup.TestType = Convert.ToInt32(testTypeDropDownList.SelectedValue);

            messageLabel.Text = manager.SaveTest(testSetup);
            ShowAllTestData();
            testNameTextBox.Text=String.Empty;
            feeTextBox.Text = String.Empty;
            testTypeDropDownList.SelectedIndex = 0;
        }

        public void LoadTestDropDown()
        {
            TypeNameManager manager1 = new TypeNameManager();
            testTypeDropDownList.DataSource = manager1.GetAllTestTypes();
            testTypeDropDownList.DataTextField = "TypeName";
            testTypeDropDownList.DataValueField = "Id";
            testTypeDropDownList.DataBind();
            testTypeDropDownList.Items.Insert(0, new ListItem("--- Please Select ---", "0"));
        }

        public void ShowAllTestData()
        {
            testNameGridView.DataSource = manager.ShowAllTestsAndTypes();
            testNameGridView.DataBind();
        }
    }
}