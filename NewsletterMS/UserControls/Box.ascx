<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Box.ascx.cs" Inherits="NewsletterMS.UserControls.Box" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/AdBox.ascx" TagName="AdBox" TagPrefix="UC" %>

    <asp:HiddenField ID="hfBoxID" runat="server" />
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfTrack" runat="server" />
            <asp:HiddenField ID="hfCurrentMode" runat="server" />
            <div class="row">
            <div class="col-lg-12 portfolio-item" style="border:1px solid black">
            <div class="row">
            <div style="display: inline-block;float: right; position: relative; margin: 5px;">
                <asp:DropDownList ID="ddlTypes" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddlTypes_SelectedIndexChanged">
                    <asp:ListItem Text="News" Selected="True" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Ad" Value="D"></asp:ListItem>
                </asp:DropDownList>
            </div>
            </div>
            <div class="row">
                <h3>
                    <asp:Label ID="lblBoxNumber" runat="server" ></asp:Label>
                </h3>
                            
            <div>
                <asp:Label ID="lblErr" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            </div>
            </div>
            <div class="row">
                <textarea id="editor1" name="editor1">Enter Text Here</textarea>
    <script type="text/javascript">
        CKEDITOR.replace('editor1', {
            filebrowserUploadUrl: '~/NewsletterMS/UploadHandler.ashx'
        });

    </script>
            </div>
            </div>
            </div>