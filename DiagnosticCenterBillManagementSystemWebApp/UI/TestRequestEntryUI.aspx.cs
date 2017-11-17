using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterBillManagementSystemWebApp.BLL;
using DiagnosticCenterBillManagementSystemWebApp.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace DiagnosticCenterBillManagementSystemWebApp.UI
{
    public partial class TestRequestEntryUI : System.Web.UI.Page
    {
        TestSetupManager testSetupManager = new TestSetupManager();
        TestRequestEntryManager testRequestEntryManager = new TestRequestEntryManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                feeTextBox.ReadOnly = true;
                totalTextBox.ReadOnly = true;
                totalTextBox.Visible = false;
                saveButton.Visible = false;
                resetButton.Visible = false;
                totalLabel.Visible = false;
                LoadTests();
            }

        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            
        }
        private void LoadTests()
        {
            List<TestSetup> tests = testSetupManager.ShowAllTests();
            selectTestDropDownList.DataSource = tests;
            selectTestDropDownList.DataTextField = "TestName";
            selectTestDropDownList.DataValueField = "TestName";
            selectTestDropDownList.DataBind();
            selectTestDropDownList.Items.Insert(0, new ListItem("--- Please Select ---", ""));
            totalTextBox.Text = (0.00).ToString();
            feeTextBox.Text = (0.00).ToString();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            if (nameOfThePatientTextBox.Text != String.Empty)
            {
                if (dateOfBirthTextBox.Text != String.Empty)
                {
                    if (new Regex(@"^(?:\+88|01)?\d{11}$").IsMatch(mobileNoTextBox.Text))
                    {
                        if (selectTestDropDownList.SelectedIndex != 0)
                        {
                            messageLabel.Text = String.Empty;

                            if (nameOfThePatientTextBox.Enabled)
                            {
                                List<SelectedTestAndFee> selectedTestAndFees = new List<SelectedTestAndFee>();
                                SelectedTestAndFee selectedTest = new SelectedTestAndFee();
                                selectedTest.SelectedTest = selectTestDropDownList.Text;
                                selectedTest.Fee = Convert.ToDouble(feeTextBox.Text);
                                if (selectedTest.SelectedTest != String.Empty && selectedTest.Fee != 0.00)
                                {
                                    selectedTestAndFees.Add(selectedTest);
                                    selectTestDropDownList.Items.Remove(selectedTest.SelectedTest);
                                    ViewState.Add("Tests", selectedTestAndFees);
                                    testRequestEntryGridView.DataSource = ViewState["Tests"];
                                    testRequestEntryGridView.DataBind();

                                    double total = Convert.ToDouble(totalTextBox.Text);
                                    totalTextBox.Text = Convert.ToString(selectedTest.Fee + total);
                                }


                                nameOfThePatientTextBox.Enabled = false;
                                dateOfBirthTextBox.Enabled = false;
                                mobileNoTextBox.Enabled = false;
                                feeTextBox.Text = 0.ToString();
                            }
                            else
                            {
                                List<SelectedTestAndFee> selectedTestAndFees = (List<SelectedTestAndFee>)ViewState["Tests"];
                                SelectedTestAndFee selectedTest = new SelectedTestAndFee();
                                selectedTest.SelectedTest = selectTestDropDownList.Text;
                                selectedTest.Fee = Convert.ToDouble(feeTextBox.Text);


                                if (selectedTest.SelectedTest != String.Empty && selectedTest.Fee != 0)
                                {
                                    selectedTestAndFees.Add(selectedTest);
                                    selectTestDropDownList.Items.Remove(selectedTest.SelectedTest);
                                    ViewState.Add("Tests", selectedTestAndFees);
                                    testRequestEntryGridView.DataSource = ViewState["Tests"];
                                    testRequestEntryGridView.DataBind();

                                    double total = Convert.ToDouble(totalTextBox.Text);
                                    totalTextBox.Text = Convert.ToString(selectedTest.Fee + total);
                                    feeTextBox.Text = 0.ToString();
                                }
                            }

                            totalTextBox.Visible = true;
                            totalLabel.Visible = true;
                            saveButton.Visible = true;
                            resetButton.Visible = true;
                        }
                        else
                        {
                            messageLabel.Text = "Must Select a test.";
                        }
                    }
                    else
                    {
                        messageLabel.Text = "Mobile must be in correct format.";
                    }
                }
                else
                {
                    messageLabel.Text = "Must have a date of birth.";
                }
            }
            else
            {
                messageLabel.Text = "Must enter a Name.";
            }
        }

        protected void selectTestDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTest = selectTestDropDownList.SelectedValue;
            feeTextBox.Text = testRequestEntryManager.GetFeeForSelectedTest(selectedTest).ToString();

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            TestRequestEntry entry = new TestRequestEntry();
            entry.NameOfPatient = nameOfThePatientTextBox.Text;
            entry.DateOfBirth = dateOfBirthTextBox.Text;
            entry.MobileNo = mobileNoTextBox.Text;
            entry.TotalFee = Convert.ToDouble(totalTextBox.Text);
            entry.BillNo = DateTime.Now.ToString().GetHashCode().ToString("X");
            entry.InvoiceDate = DateTime.Now.ToString();

            messageLabel.Text = testRequestEntryManager.SavePatientData(entry);

            if (messageLabel.Text == "Patient Saved Successfully")
            {
                foreach (GridViewRow view in testRequestEntryGridView.Rows)
                {
                    entry.TestName = ((Label)view.FindControl("testNameLabel")).Text;
                    entry.Fee = Convert.ToDouble(((Label)view.FindControl("feeLabel")).Text);

                    messageLabel2.Text = testRequestEntryManager.SaveBillInfo(entry);
                }

                if (messageLabel.Text == "Patient Saved Successfully" && messageLabel2.Text == "Info Saved")
                {
                    GeneratePdf(entry.BillNo, entry.InvoiceDate, entry.NameOfPatient, entry.DateOfBirth, entry.MobileNo, entry.TotalFee);
                }
            }
            LoadTests();
            nameOfThePatientTextBox.Text = String.Empty;
            nameOfThePatientTextBox.Enabled = true;
            dateOfBirthTextBox.Text = String.Empty;
            dateOfBirthTextBox.Enabled = true;
            mobileNoTextBox.Text = String.Empty;
            mobileNoTextBox.Enabled = true;
            selectTestDropDownList.SelectedIndex = 0;
            feeTextBox.Text = String.Empty;
            totalTextBox.Text = 0.ToString();
            saveButton.Visible = false;
            resetButton.Visible = false;
            totalLabel.Visible = false;
            totalTextBox.Visible = false;
            testRequestEntryGridView.DataSource = null;
            testRequestEntryGridView.DataBind();

        }

        private void GeneratePdf(string billNo, string date, string patientName, string dateOfBirth, string mobileNo, double total)
        {
            Paragraph aparagraph = new Paragraph("Diagnostic center\n\n");
            aparagraph.PaddingTop = 10;
            aparagraph.Alignment = 1;
            aparagraph.Font.Size = 30;
            Paragraph paragraph1 = new Paragraph("Print Date: " + date);
            Paragraph bParagraph = new Paragraph("Bill No: " + billNo);
            Paragraph paragraph2 = new Paragraph("\nPatient name: " + patientName + "\nDate Of birth: " + dateOfBirth + "\nMobile No: " + mobileNo + "\n\n");

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + billNo + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            testRequestEntryGridView.RenderControl(htmlTextWriter);
            testRequestEntryGridView.HeaderRow.Style.Add("width", "15%");
            testRequestEntryGridView.HeaderRow.Style.Add("font-size", "14px");
            testRequestEntryGridView.Style.Add("text-decoration", "none");
            testRequestEntryGridView.Style.Add("font-family", "Arial, Helvetica, sans-serif");
            testRequestEntryGridView.Style.Add("font-size", "8px");

            Paragraph paragraph3 = new Paragraph("\nTotal: " + total);
            paragraph3.Alignment = 2;
            paragraph3.Font.Size = 18;
            //Paragraph paragraph4 = new Paragraph("\nPayment status: Unpaid");
            //paragraph4.Alignment = 2;
            //paragraph4.Font.Size = 18;


            StringReader stringReader = new StringReader(stringWriter.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();
            pdfDoc.Add(paragraph1);
            pdfDoc.Add(aparagraph);
            pdfDoc.Add(bParagraph);
            pdfDoc.Add(paragraph2);
            htmlparser.Parse(stringReader);
            pdfDoc.Add(paragraph3);
            //pdfDoc.Add(paragraph4);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            testRequestEntryGridView.AllowPaging = true;
            testRequestEntryGridView.DataBind();

        }

        protected void resetButton_Click(object sender, EventArgs e)
        {
            LoadTests();
            nameOfThePatientTextBox.Text = String.Empty;
            nameOfThePatientTextBox.Enabled = true;
            dateOfBirthTextBox.Text = String.Empty;
            dateOfBirthTextBox.Enabled = true;
            mobileNoTextBox.Text = String.Empty;
            mobileNoTextBox.Enabled = true;
            selectTestDropDownList.SelectedIndex = 0;
            feeTextBox.Text = String.Empty;
            totalTextBox.Text = 0.ToString();
            saveButton.Visible = false;
            resetButton.Visible = false;
            totalLabel.Visible = false;
            totalTextBox.Visible = false;
            testRequestEntryGridView.DataSource = null;
            testRequestEntryGridView.DataBind();

        }
    }
}