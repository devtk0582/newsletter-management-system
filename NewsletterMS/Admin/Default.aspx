<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="NewsletterMS._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
    $(document).ready(function () {
        $("#divChooseNewsletters").hide();
    });

    function ToggleNewsletters() {
        $("#divChooseNewsletters").toggle();
        return false;
    }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <center>
        <div style="width: 60%;">
            <div>
                Welcome,
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </div>
            <div runat="server" id="SuperAdminMenu" visible="false">
                 <div class="menu_link">
                <asp:HyperLink ID="hlAdminMaintenance" runat="server" CssClass="main_menu_style" NavigateUrl="AdminMaintenance.aspx" ImageUrl="~/Images/admin-maintenance-link.png"></asp:HyperLink>
            </div>
                <div class="menu_link">
                    <asp:LinkButton ID="lbEditNewsletter" runat="server" OnClientClick="return ToggleNewsletters();" CssClass="main_menu_style"> <asp:Image ID="imgEditNewsletter" runat="server" ImageUrl="~/Images/edit-newsletter-link.png" /> 
                    </asp:LinkButton>
                </div>
                <div id="divChooseNewsletters" style="margin: 5px;" class="menu_link">
                    <asp:DropDownList ID="ddlNewsletters" runat="server" AutoPostBack="true" CssClass="dropdown_style" 
                        onselectedindexchanged="ddlNewsletters_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            <div class="menu_link">
                <asp:HyperLink ID="hlPublicationMaintenance" runat="server" CssClass="main_menu_style"
                    NavigateUrl="~/Admin/Publications.aspx" ImageUrl="~/Images/publication-maintenance-link.png"></asp:HyperLink>
            </div>
            <div class="menu_link">
                <asp:HyperLink ID="hlJumpToNewsletter" runat="server" CssClass="main_menu_style" ImageUrl="~/Images/view-newsletter-link.png" NavigateUrl="~/Admin/Preview.aspx"></asp:HyperLink>
            </div>
            <div class="menu_link">
                    <asp:HyperLink ID="hlUserMaintenance" runat="server" CssClass="main_menu_style" ImageUrl="~/Images/user-maintenance-link.png" NavigateUrl="~/Admin/UserMaintenance.aspx"></asp:HyperLink>
                </div>
            <div class="menu_link">
                <asp:HyperLink ID="hlSectionMaintenance" runat="server" CssClass="main_menu_style" NavigateUrl="Sections.aspx" ImageUrl="~/Images/section-maintenance-link.png"></asp:HyperLink>
            </div>
            </div>
            <div runat="server" id="LocalAdminMenu">
                <asp:HiddenField ID="hfNewsletterID" runat="server"></asp:HiddenField>
                <div class="menu_link">
                    <asp:LinkButton ID="UserMaintenanceLink" runat="server" 
                        CssClass="main_menu_style" onclick="UserMaintenanceLink_Click">
                     <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/user-maintenance-link.png" /> 
                    </asp:LinkButton>
                </div>
                <div class="menu_link">
                    <asp:LinkButton ID="ManageSiteLink" runat="server" CssClass="main_menu_style" 
                        onclick="ManageSiteLink_Click"> <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/edit-newsletter-link.png" /> 
                    </asp:LinkButton>
                </div>
                <div class="menu_link">
                    <asp:HyperLink ID="JumpToNewsletterLink" runat="server" CssClass="main_menu_style" ImageUrl="~/Images/view-newsletter-link.png" NavigateUrl="~/Admin/Preview.aspx"></asp:HyperLink>
                </div>
            </div>
        </div>
    </center>
</asp:Content>
