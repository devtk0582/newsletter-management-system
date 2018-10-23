<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="AdminMaintenance.aspx.cs" Inherits="NewsletterMS.Admin.AdminMaintenance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="AdminUsersUpdatePanel" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1">
                <tr>
                    <td align="center" valign="top" style="font-size: 18px; color: Maroon;">
                        Admin Maintenance
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErrAdmin" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div style="float: right; white-space: nowrap; padding: 5px;">
                            <div style="display: inline-block;">
                                <asp:Label ID="Label1" runat="server" Text="Search Admin: " AssociatedControlID="txtSearchAdmin"></asp:Label>
                                <asp:TextBox ID="txtSearchAdmin" runat="server" Text=""></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="txtWESearch" runat="server" TargetControlID="txtSearchAdmin"
                                    WatermarkText="Search Here">
                                </cc1:TextBoxWatermarkExtender>
                                <cc1:AutoCompleteExtender ID="txtSearchAdmin_AutoCompleteExtender" runat="server"
                                    Enabled="true" ServicePath="~/AutoCompleteWebService.asmx" MinimumPrefixLength="1"
                                    TargetControlID="txtSearchAdmin" ServiceMethod="SearchAdmins">
                                </cc1:AutoCompleteExtender>
                            </div>
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbSearchAdmin" runat="server" CssClass="lbPaddingGrayStyle" Text="Go"
                                    OnClick="lbSearchAdmin_Click"></asp:LinkButton>
                            </div>
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbShowAll" runat="server" CssClass="lbPaddingGrayStyle" Text="Show All"
                                    OnClick="lbShowAll_Click"></asp:LinkButton>
                            </div>
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbAddAdmin" runat="server" Text="Add Admin" CssClass="lbPaddingGrayStyle"
                                    OnClick="lbAddAdmin_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr valign="top" style="padding: 5px;">
                    <td>
                        <asp:GridView ID="gvAdmins" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                            AllowSorting="True" AllowPaging="True" EnableSortingAndPagingCallbacks="True"
                            CellPadding="3" PageSize="14" Height="100%" Width="100%" OnSorting="gvAdmins_Sorting"
                            OnRowCommand="gvAdmins_RowCommand" OnRowDataBound="gvAdmins_RowDataBound" DataSourceID="odsAdmins">
                            <HeaderStyle CssClass="gvHeaderStyle" />
                            <EmptyDataTemplate>
                                <p class="EmptyMessageStyle">
                                    No Admin Found</p>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="User ID" SortExpression="UserID" ItemStyle-Width="20%"
                                    HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfAdminID" runat="server" Value='<%#Eval("AdminUserID").ToString()%>' />
                                        <asp:LinkButton CommandName="EditAdmin" CommandArgument='<%#Eval("AdminUserID").ToString()%>'
                                            ID="lnkEditAdmin" Text='<%#Eval("UserID").ToString()%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="Name"
                                    HeaderText="Name"></asp:BoundField>
                                <asp:BoundField DataField="Phone" ItemStyle-Width="15%" HeaderStyle-Width="15%" SortExpression="Phone"
                                    HeaderText="Phone"></asp:BoundField>
                                <asp:BoundField DataField="Role" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="Role"
                                    HeaderText="Role"></asp:BoundField>
                                <asp:BoundField DataField="ContactEmail" ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                    SortExpression="ContactEmail" HeaderText="ContactEmail"></asp:BoundField>
                                <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                    ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandName="DeleteAdmin" CommandArgument='<%#Eval("AdminUserID").ToString()%>'
                                            ID="lnkDelete" Text="Delete" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" />
                            <PagerStyle CssClass="gvPagerStyle" />
                            <RowStyle CssClass="gvRowStyle" />
                            <AlternatingRowStyle CssClass="gvAltRowStyle" />
                            <SortedAscendingHeaderStyle CssClass="GVSortingAsc" />
                            <SortedDescendingHeaderStyle CssClass="GVSortingDesc" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <input id="dummy" type="button" style="display: none" runat="server" />
            <cc1:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="dummy" 
                PopupControlID="pnlPopUp" BackgroundCssClass="popupBG" DropShadow="true" />
            <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup">
                <center>
                    <table class="PopupFormBG" width="60%" cellpadding="0" cellspacing="0">
                        <tr style="height: 30px;">
                            <td align="left" class="PopupFormTitleStyle">
                                <asp:Label ID="lblTitle" runat="server" Text="Add / Edit Admin"></asp:Label>
                                <asp:HiddenField ID="hfAdminID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr style="padding: 10px 8px 5px 8px;">
                            <td align="center">
                                <table cellpadding="5" cellspacing="3" width="100%" class="PopUpFormMainTbl">
                                    <tr>
                                        <td>
                                            <strong>Admin User ID</strong><span class="requiredStyle">*</span>:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="30" ID="txtUserID" TabIndex="1" runat="server" CssClass="textbox"
                                                MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trPassword">
                                        <td>
                                            <strong>Password</strong><span class="requiredStyle">*</span>:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="30" ID="txtPassword" TextMode="Password" TabIndex="1" runat="server"
                                                CssClass="textbox" MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trConfirmPassword">
                                        <td>
                                            <strong>Confirm Password</strong><span class="requiredStyle">*</span>:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="30" ID="txtConfirmPassword" TextMode="Password" TabIndex="1"
                                                runat="server" CssClass="textbox" MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Name</strong><span class="requiredStyle">*</span>:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="50" ID="txtName" TabIndex="2" runat="server" CssClass="textbox"
                                                MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Email</strong><span class="requiredStyle">*</span>:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="40" ID="txtEmail" TabIndex="2" runat="server" CssClass="textbox"
                                                MaxLength="40"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Phone:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="25" ID="txtPhone" TabIndex="2" runat="server" CssClass="textbox"
                                                MaxLength="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Role:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRoles" runat="server">
                                                <asp:ListItem Text="Editor" Value="L" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Super Administrator" Value="S"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Newsletter:
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlNewsletters" runat="server" Height="100px" ScrollBars="Auto">
                                                <asp:CheckBoxList ID="cblNewsletters" runat="server" RepeatDirection="Horizontal" RepeatColumns="3"></asp:CheckBoxList> 
                                            </asp:Panel>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height: 50px;">
                            <td align="right">
                                <table width="100px">
                                    <tr>
                                        <td align="center">
                                            <asp:LinkButton TabIndex="10" ID="lbSave" runat="server" Text="Add" CssClass="PopupFormButton"
                                                OnClick="lbSave_Click"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <asp:LinkButton TabIndex="11" ID="lbCancel" runat="server" Text="Cancel" CssClass="PopupFormButton"
                                                OnClick="lbCancel_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
            <asp:ObjectDataSource ID="odsAdmins" runat="server" TypeName="NewsletterMSBLL.BOAdmins"
                EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="TotalNumberOfAdmins"
                SelectMethod="GetAdminsPagedAndSorted" StartRowIndexParameterName="pageIndex"
                SortParameterName="sortExpression">
                <SelectParameters>
                    <asp:ControlParameter Name="searchValue" ControlID="txtSearchAdmin" DefaultValue=""
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
