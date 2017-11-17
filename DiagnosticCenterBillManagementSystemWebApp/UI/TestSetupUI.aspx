<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSetupUI.aspx.cs" Inherits="DiagnosticCenterBillManagementSystemWebApp.UI.TestSetupUI" %>

<!DOCTYPE html>

<html lang="en-US">
<head runat="server">
    <meta charset="UTF-8"/>
    <title>Test Setup</title>
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
                            <td><asp:Label ID="Label4" runat="server" Text="Test Name"></asp:Label></td>
                            <td><asp:TextBox ID="testNameTextBox" runat="server" Height="31px" Width="266px"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:Label ID="Label5" runat="server" Text="Fee"></asp:Label></td>
                            <td>
                                <br />
                                <asp:TextBox ID="feeTextBox" runat="server" TextMode="Number" Height="31px" Width="266px"></asp:TextBox></td>
                            <td class="tag">
                                <br />
                                BDT</td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:Label ID="Label6" runat="server" Text="Test Type"></asp:Label></td>
                            <td>
                                <br />
                                <asp:DropDownList ID="testTypeDropDownList" runat="server" Height="31px" Width="266px"></asp:DropDownList></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="button">
                                <br />
                                <asp:Button ID="saveButton" runat="server" Text="Save" Height="35px" Width="66px" OnClick="saveButton_Click" /></td>
                            <td></td>
                        </tr>
                    </table>
                    <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
                </fieldset>
                <br/>
                <asp:GridView ID="testNameGridView" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Test Name">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("TestName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fee">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Fee") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <footer>
                <h1>&copy; 2017, Amigos.</h1>
            </footer>
        </div>
    </form>
</body>
</html>