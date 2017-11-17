<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentUI.aspx.cs" Inherits="DiagnosticCenterBillManagementSystemWebApp.UI.PaymentUI" %>

<!DOCTYPE html>

<html lang="en-US">
<head runat="server">
    <meta charset="UTF-8"/>
    <title>Payment</title>
    <link href="../Scripts/reset.css" rel="stylesheet" />
    <link href="../Scripts/style.css" rel="stylesheet" />
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
                            <td><asp:Label ID="Label5" runat="server" Text="Bill No"></asp:Label></td>
                            <td><asp:TextBox ID="billNoTextBox" runat="server" Height="35px" Width="180px"></asp:TextBox></td>
                            <td class="tag">or</td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:Label ID="Label6" runat="server" Text="Mobile No"></asp:Label></td>
                            <td>
                                <br />
                                <asp:TextBox ID="mobileNoTextBox" runat="server" Height="35px" Width="180px"></asp:TextBox></td>
                            <td class="tag">
                                <br />
                                <asp:Button ID="searchButton" runat="server" Text="Search" Height="35px" Width="75px" OnClick="searchButton_Click" /></td>
                        </tr>
                    </table>
                </fieldset>
                <br/>
                <br/>
                <fieldset>
                    <table>
                        <tr>
                            <td><asp:Label ID="Label7" runat="server" Text="Amount"></asp:Label></td>
                            <td><asp:TextBox ID="amountTextBox" runat="server" Height="35px" Width="180px"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:Label ID="Label8" runat="server" Text="Due Date"></asp:Label></td>
                            <td>
                                <br />
                                <asp:TextBox ID="dueDateTextBox" runat="server" Height="35px" Width="180px"></asp:TextBox>
                                <br/>
                            </td>
                            <td class="tag">
                                <br />
                                <asp:Button ID="payButton" runat="server" Text="Pay" Height="35px" Width="75px" OnClick="payButton_Click" />
                                <br/>
                                <br/>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="padding-left: 10%"><asp:Button ID="resetButton" Height="35px" Width="100px" runat="server" Text="Reset" OnClick="resetButton_Click" /></td>
                            <td></td>
                        </tr>
                    </table>
                    <br/>
                    <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
                </fieldset>
            </div>
            <footer>
                <h1>&copy; 2017, Amigos.</h1>
            </footer>
        </div>
    </form>
</body>
</html>
