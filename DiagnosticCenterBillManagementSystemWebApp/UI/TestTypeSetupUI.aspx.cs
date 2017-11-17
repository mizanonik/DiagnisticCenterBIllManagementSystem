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
    public partial class TestTypeSetup : System.Web.UI.Page
    {
        TypeNameManager manager = new TypeNameManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowDataInGridView();
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            string typeName = typeNameTextBox.Text;
            messageLabel.Text = manager.SaveType(typeName);
            
            ShowDataInGridView();
            typeNameTextBox.Text=String.Empty;
        }

        public void ShowDataInGridView()
        {
            typeNameGridView.DataSource = manager.GetAllTestTypes();
            typeNameGridView.DataBind();
        }
    }
}