<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayBox.ascx.cs" Inherits="NewsletterMS.UserControls.DisplayBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/AdBox.ascx" TagName="AdBox" TagPrefix="UC" %>    
    <asp:HiddenField ID="hfBoxID" runat="server" />
        <asp:HiddenField ID="hfMode" runat="server" />
<asp:Panel ID="pnlSnippet" runat="server" BorderColor="Black" BorderWidth="2px" BorderStyle="Inset">
            <asp:HiddenField ID="hfCurrentMode" runat="server" />
            <div runat="server" id="divNews">
            <div style="padding: 10px 3px 3px 3px; background-position:center">
                <asp:Image ID="imgView" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" runat="server" ImageUrl="~/Images/default_img_medium.png" Width="450" Height="300" />
            </div>
            <div style="margin: 5px;">
                    <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
            </div>
        </div>
            <div runat="server" id="divVideo" visible="false">
                    <object width='450' height='300'>
<%--    <param runat="server" id="paramLink" name='movie' value='http://www.youtube.com/v/dgp2-2nf5ME' />--%>
    <param name='allowFullScreen' value='true'/>
    <param name='allowscriptaccess' value='always'/>
    <embed runat="server" id="videoEmbed" src='http://www.youtube.com/v/dgp2-2nf5ME' type='application/x-shockwave-flash' allowscriptaccess='always' allowfullscreen='true' width='320' height='240'>
    </embed>
    </object>
            </div>
            <div runat="server" id="divAd" visible="false">
                <div>
                    Ad: 
                    <asp:Label ID="lblAdName" runat="server"></asp:Label>
                </div>
                <div>
                    <UC:AdBox runat="server" ID="adBox"></UC:AdBox>
                </div>
            </div>
        </asp:Panel>
