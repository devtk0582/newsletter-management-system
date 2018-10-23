<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NewsletterMS.Login" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Newsletter Management Log In</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="90%" >
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="5" cellspacing="2" bgcolor="#2F374A">
                                <tr>
                                    <td align="left" height="64">
                                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/NewsLetterLogo.gif" Width="128" Height="64" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle" style="background-color: #232B3E" >
                    <asp:Panel ID="plLogInTable" runat="server" CssClass="log_in_table" DefaultButton="lbSubmit">
                        <table cellpadding="3" cellspacing="3">
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="LogInValidationSummary" runat="server" DisplayMode="BulletList" CssClass="validation_summary_style" ValidationGroup="LogIn"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblErrMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Administration User ID:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserID" runat="server" MaxLength="50"></asp:TextBox>
                                <span style="color: Red">*</span><asp:RequiredFieldValidator ID="rfvUserID" runat="server" Display="None" ErrorMessage="User ID is required" ControlToValidate="txtUserID" ValidationGroup="LogIn" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Password:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                                <span style="color: Red">*</span><asp:RequiredFieldValidator ID="rfvPassword" runat="server" Display="None" ErrorMessage="Password is required" ControlToValidate="txtPassword" ValidationGroup="LogIn" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:LinkButton ID="lbSubmit" runat="server" Text="Sign In" 
                                    CssClass="lbPaddingStyle" ValidationGroup="LogIn" onclick="lbSubmit_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                        
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
