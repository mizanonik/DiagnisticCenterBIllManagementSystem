using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterBillManagementSystemWebApp.BLL;
using DiagnosticCenterBillManagementSystemWebApp.Model;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Crypto.Encodings;

namespace DiagnosticCenterBillManagementSystemWebApp.UI
{
    public partial class TestWiseReportUI : System.Web.UI.Page
    {
        ReportManager reportManager = new ReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            generatePDFButton.Visible = false;
            totalLabel.Visible = false;
            totalTextBox.ReadOnly = true;
            totalTextBox.Visible = false;
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            
        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            generatePDFButton.Visible = true;
            totalLabel.Visible = true;
            totalTextBox.Visible = true;


            string fromDate = fromDateTextBox.Text;
            string toDate = toDateTextBox.Text;
            double finalAmount = 0;

            List<TotalTest> totalTests = reportManager.GetSearchedTestData(fromDate, toDate);
            foreach (TotalTest test in totalTests)
            {
                finalAmount += test.TotalAmount;
            }
            testWiseReportGridView.DataSource = totalTests;
            testWiseReportGridView.DataBind();
            totalTextBox.Text = finalAmount.ToString();
        }

        protected void generatePDFButton_Click(object sender, EventArgs e)
        {
            string fromDate = fromDateTextBox.Text;
            string toDate = toDateTextBox.Text;
            string date = DateTime.Now.ToString();
            double total = Convert.ToDouble(totalTextBox.Text);

            
            Paragraph aparagraph = new Paragraph("Diagnostic center\n\n");
            aparagraph.PaddingTop = 10;
            aparagraph.Alignment = 1;
            aparagraph.Font.Size = 30;
            Paragraph paragraph1 = new Paragraph("Print Date: " + date);
            Paragraph paragraph2 = new Paragraph("\nShowing Test Wise Report\nfrom: " + fromDate + " to: " + toDate+"\n\n" );
            paragraph2.Font.Size = 22;
            paragraph2.Alignment = 1;

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TestWiseReportFom"+fromDate+"To"+toDate+".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            testWiseReportGridView.RenderControl(htmlTextWriter);
            testWiseReportGridView.HeaderRow.Style.Add("width", "15%");
            testWiseReportGridView.HeaderRow.Style.Add("font-size", "14px");
            testWiseReportGridView.Style.Add("text-decoration", "none");
            testWiseReportGridView.Style.Add("font-family", "Arial, Helvetica, sans-serif");
            testWiseReportGridView.Style.Add("font-size", "8px");

            Paragraph paragraph3 = new Paragraph("\nTotal: " + total);
            paragraph3.Alignment = 2;
            paragraph3.Font.Size = 18;


            StringReader stringReader = new StringReader(stringWriter.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(paragraph1);
            pdfDoc.Add(aparagraph);
            pdfDoc.Add(paragraph2);
            htmlparser.Parse(stringReader);
            pdfDoc.Add(paragraph3);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            testWiseReportGridView.AllowPaging = true;
            testWiseReportGridView.DataBind();

        }
    }
}