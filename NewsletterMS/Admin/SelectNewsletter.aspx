<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SelectNewsletter.aspx.cs" Inherits="NewsletterMS.Admin.SelectNewsletter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <p>Please select the newsletter to proceed:</p>
<p>
<asp:DropDownList ID="ddlNewsletters" runat="server" AutoPostBack="true" CssClass="dropdown_style" 
                        onselectedindexchanged="ddlNewsletters_SelectedIndexChanged">
</asp:DropDownList>
</p>
    </div>

</asp:Content>
