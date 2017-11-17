<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TypeWiseReportUI.aspx.cs" Inherits="DiagnosticCenterBillManagementSystemWebApp.UI.TypeWiseReportUI" %>

<!DOCTYPE html>

<html lang="en-US">
<head runat="server">
    <meta charset="UTF-8"/>
    <title>Type Wise Report</title>
    <link href="../Scripts/reset.css" rel="stylesheet" />
    <link href="../Scripts/style.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap-datepicker.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <header>
                <div class="logo"></div>
                <div class="banner">
                    <h1>Diagnostic Center Bill Management System</h1>
                </div>
                <nav>
                    <ul>
                        <li><a href="IndexUI.aspx">Home</a></li>
                        <li><a>Setup</a>
                            <ul>
                                <li><a href="TestTypeSetupUI.aspx">Test Type</a></li>
                                <li><a href="TestSetupUI.aspx">Test</a></li>
                            </ul>
                        </li>
                        <li><a>Test Request</a>
                            <ul>
                                <li><a href="TestRequestEntryUI.aspx">Entry</a></li>
                                <li><a href="PaymentUI.aspx">Payment</a></li>
                            </ul>
                        </li>
                        <li><a>Report</a>
                            <ul>
                                <li><a href="TestWiseReportUI.aspx">Test Wise</a></li>
                                <li><a href="TypeWiseReportUI.aspx">Type Wise</a></li>
                                <li><a href="UnpaidBillUI.aspx">Unpaid Bill</a></li>
                            </ul>
                        </li>
                    </ul>
                </nav>
            </header>
            <div class="main">
                <fieldset>
                    <table>
                        <tr>
                            <td><asp:Label ID="Label4" runat="server" Text="From Date"></asp:Label></td>
                            <td><asp:TextBox ID="fromDateTextBox" runat="server" AutoCompleteType="Disabled" Height="35px" Width="240px"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:Label ID="Label5" runat="server" Text="To Date"></asp:Label></td>
                            <td>
                                <br />
                                <asp:TextBox ID="toDateTextBox" runat="server" AutoCompleteType="Disabled" Height="35px" Width="240px"></asp:TextBox></td>
                            <td style="padding-left: 10px;">
                                <br />
                                <asp:Button ID="showButton" runat="server" Text="Show" Height="36px" Width="79px" OnClick="showButton_Click"/></td>
                        </tr>
                    </table>
                    <br/>
                </fieldset>
                <asp:GridView ID="typeWiseReportGridView" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Test type Name">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total No of Test">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("TestCount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Amount">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
                <br/>
                <div class="buttonright">
                    <asp:Button ID="generatePDFButton" runat="server" Height="36px" Width="79px" Text="PDF" OnClick="generatePDFButton_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="totalLabel" runat="server" Text="Total"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="totalTextBox" runat="server" Height="31px" Width="148px"></asp:TextBox>
                </div>
            </div>
            <br/>
            <br/>
            <br/>
            <footer>
                <h1>&copy; 2017, Amigos.</h1>
            </footer>
        </div>
    </form>
    <script src="../Scripts/jquery-3.1.1.min.js"></script>
    <script src="../Scripts/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
       $(document).ready(function () {
           $('#fromDateTextBox').datepicker({
               format: "yyyy-mm-dd",
               clearBtn: true,
               weekStart: 6,
               daysOfWeekHighlighted: "6",
               endDate: new Date(),
               autoclose: true
           }).on('changeDate', function (selected) {
               var minDate = new Date(selected.date.valueOf());
               $('#toDateTextBox').datepicker('setStartDate', minDate);
           });
           $('#toDateTextBox').datepicker({
               format: "yyyy-mm-dd",
               clearBtn: true,
               weekStart: 6,
               daysOfWeekHighlighted: "6",
               endDate: new Date(),
               autoclose: true
           });
       });
    </script>
</body>
</html>
