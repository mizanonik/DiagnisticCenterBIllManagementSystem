<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRequestEntryUI.aspx.cs" Inherits="DiagnosticCenterBillManagementSystemWebApp.UI.TestRequestEntryUI" EnableEventValidation="false" %>

<!DOCTYPE html>

<html lang="en-US">
<head runat="server">
    <meta charset="UTF-8" />
    <title>Test Request Entry</title>
    <link href="../Scripts/reset.css" rel="stylesheet" />
    <link href="../Scripts/style.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
</head>
<body>
    <form id="form2" runat="server">
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
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Name of the patient"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="nameOfThePatientTextBox" runat="server" Height="35px" Width="242px" name="patientName"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Date of Birth"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="dateOfBirthTextBox" AutoCompleteType="Disabled" runat="server" Height="35px" Width="242px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Mobile No"></asp:Label></td>
                            <td>
                                <asp:TextBox TextMode="Phone" ID="mobileNoTextBox" MaxLength="14" runat="server" Height="35px" Width="242px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Select Test"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="selectTestDropDownList" runat="server" AutoPostBack="True" Height="35px" Width="245px" OnSelectedIndexChanged="selectTestDropDownList_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="button">FEE
                                <asp:TextBox ID="feeTextBox" runat="server" Height="35px" Width="120px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="button">
                                <br />
                                <asp:Button ID="addButton" runat="server" Text="Add" Height="37px" Width="77px" OnClick="addButton_Click" /></td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
                </fieldset>
                <br />
                <br />
                <div>
                    <asp:GridView ID="testRequestEntryGridView" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Test">
                                <ItemTemplate>
                                    <asp:Label ID="testNameLabel" runat="server" Text='<%#Eval("SelectedTest") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fee">
                                <ItemTemplate>
                                    <asp:Label ID="feeLabel" runat="server" Text='<%#Eval("Fee") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <div class="button">
                        <asp:Label ID="totalLabel" runat="server" Text="Total"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="totalTextBox" runat="server" Height="31px" Width="148px"></asp:TextBox>
                    </div>
                    <br />
                    <br />
                    <div class="button">
                        <asp:Button ID="resetButton" runat="server" Text="Reset" Height="33px" Width="77px" OnClick="resetButton_Click" />
                        <asp:Button ID="saveButton" runat="server" Text="save" Height="33px" Width="77px" OnClick="saveButton_Click" />
                    </div>
                    <asp:Label ID="messageLabel2" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <br />
            <br />
            <footer>
                <h1>&copy; 2017, Amigos.</h1>
            </footer>
        </div>
    </form>
    <script src="../Scripts/jquery-3.1.1.min.js"></script>
    <script src="../Scripts/jquery.validate.min.js"></script>
    <script src="../Scripts/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dateOfBirthTextBox').datepicker({
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