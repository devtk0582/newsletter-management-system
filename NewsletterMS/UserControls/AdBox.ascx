<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdBox.ascx.cs" Inherits="NewsletterMS.UserControls.AdBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

    <asp:HiddenField ID="hfAdName" runat="server" />
<asp:Panel ID="pnlAdBox" runat="server" BorderColor="Black" BorderWidth="2px" BorderStyle="Inset">
            <asp:HiddenField ID="hfCurrentMode" runat="server" />
            <div runat="server" id="divNews">
            <div style="padding: 10px 3px 3px 3px; background-position:center">
                <asp:ImageButton ID="imgView" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" runat="server" ImageUrl="~/Images/default_img_medium.png" OnClientClick="window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);" Width="450" Height="300" />
            </div>
            <div style="margin: 5px;">
                    <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
            </div>
        </div>
        </asp:Panel>
