<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserMaintenance.aspx.cs" Inherits="NewsletterMS.Admin.UserMaintenance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="UsersUpdatePanel" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1">
                <tr>
                    <td align="center" valign="top" style="font-size: 18px; color: Maroon;">
                        Users Maintenance
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErrUsers" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div style="float: right; white-space: nowrap; padding: 5px;">
                            <div style="display: inline-block;">
                                <asp:Label ID="Label1" runat="server" Text="Search User: " AssociatedControlID="txtSearchUser"></asp:Label>
                                <asp:TextBox ID="txtSearchUser" runat="server" Text=""></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="txtWESearch" runat="server" TargetControlID="txtSearchUser"
                                    WatermarkText="Search Here">
                                </cc1:TextBoxWatermarkExtender>
                                <cc1:AutoCompleteExtender ID="txtSearchUser_AutoCompleteExtender" runat="server"
                                    Enabled="true" ServicePath="~/AutoCompleteWebService.asmx" MinimumPrefixLength="1"
                                    TargetControlID="txtSearchUser" ServiceMethod="SearchUsers">
                                </cc1:AutoCompleteExtender>
                            </div>
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbSearchUser" runat="server" CssClass="lbPaddingGrayStyle" Text="Go"
                                    OnClick="lbSearchUser_Click"></asp:LinkButton>
                            </div>
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbShowAll" runat="server" CssClass="lbPaddingGrayStyle" Text="Show All"
                                    OnClick="lbShowAll_Click"></asp:LinkButton>
                            </div>
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbAddUser" runat="server" Text="Add User" CssClass="lbPaddingGrayStyle"
                                    OnClick="lbAddUser_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr valign="top" style="padding: 5px;">
                    <td>
                        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                            AllowSorting="True" AllowPaging="True" EnableSortingAndPagingCallbacks="True"
                            CellPadding="3" PageSize="14" Height="100%" Width="100%" OnSorting="gvUsers_Sorting"
                            OnRowCommand="gvUsers_RowCommand" OnRowDataBound="gvUsers_RowDataBound" DataSourceID="odsUsers">
                            <HeaderStyle CssClass="gvHeaderStyle" />
                            <EmptyDataTemplate>
                                <p class="EmptyMessageStyle">
                                    No User Found</p>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="User Name" SortExpression="Name" ItemStyle-Width="20%"
                                    HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfUserID" runat="server" Value='<%#Eval("UserID").ToString()%>' />
                                        <asp:LinkButton CommandName="EditUser" CommandArgument='<%#Eval("UserID").ToString()%>'
                                            ID="lnkEditUser" Text='<%#Eval("UserName").ToString()%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UserEmail" ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="Email"
                                    HeaderText="Email"></asp:BoundField>
                                <asp:BoundField DataField="UserPhone" ItemStyle-Width="15%" HeaderStyle-Width="15%" SortExpression="Phone"
                                    HeaderText="Phone"></asp:BoundField>
                                <asp:BoundField DataField="UserMobile" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="Mobile"
                                    HeaderText="Mobile"></asp:BoundField>
                                <asp:BoundField DataField="UserCity" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="City"
                                    HeaderText="City"></asp:BoundField>
                                <asp:BoundField DataField="UserState" ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="State"
                                    HeaderText="State"></asp:BoundField>
                                <asp:BoundField DataField="UserZip" ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                    SortExpression="Zip" HeaderText="Zip"></asp:BoundField>
                                <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                    ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandName="DeleteUser" CommandArgument='<%#Eval("UserID").ToString()%>'
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
                <tr>
                    <td valign="top" align="left">
                    <div style="display: inline-block; vertical-align: top;">
                        All emails will be sent a news update:
                    </div>
                    <div style="display: inline-block; vertical-align: top;">
                        <asp:RadioButtonList ID="rblFrequency" runat="server" Enabled="false" RepeatColumns="2" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Daily" Value="D"></asp:ListItem>
                                <asp:ListItem Text="Bi-Weekly" Value="B"></asp:ListItem>
                                <asp:ListItem Text="Weekly" Value="W" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Monthly" Value="M"></asp:ListItem>
                            </asp:RadioButtonList>
                    </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Import Email List: 
                        <asp:FileUpload ID="fuEmailList" runat="server" />&nbsp;&nbsp;
                        <asp:LinkButton ID="lbImportEmailList" runat="server" Text="Import" 
                            CssClass="lbPaddingGrayStyle" onclick="lbImportEmailList_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Warning: Importing a new list will remove the current email list.
                    </td>
                </tr>
            </table>
            <input id="dummy" type="button" style="display: none" runat="server" />
            <cc1:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="dummy" PopupDragHandleControlID="pnlPopUp"
                PopupControlID="pnlPopUp" BackgroundCssClass="popupBG" DropShadow="true" />
            <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup">
                <center>
                    <table class="PopupFormBG" width="60%" cellpadding="0" cellspacing="0">
                        <tr style="height: 30px;">
                            <td align="left" class="PopupFormTitleStyle">
                                <asp:Label ID="lblTitle" runat="server" Text="Add / Edit User"></asp:Label>
                                <asp:HiddenField ID="hfUserID" runat="server" />
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
                                            <strong>User Name</strong><span class="requiredStyle">*</span>:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="30" ID="txtUserName" TabIndex="1" runat="server" CssClass="textbox"
                                                MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Email</strong><span class="requiredStyle">*</span>:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="30" ID="txtEmail" TabIndex="2" runat="server"
                                                CssClass="textbox" MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Phone:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="50" ID="txtPhone" TabIndex="3" runat="server" CssClass="textbox"
                                                MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mobile:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="40" ID="txtMobile" TabIndex="4" runat="server" CssClass="textbox"
                                                MaxLength="40"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            City:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="40" ID="txtCity" TabIndex="5" runat="server" CssClass="textbox"
                                                MaxLength="40"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            State:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="40" ID="txtState" TabIndex="6" runat="server" CssClass="textbox"
                                                MaxLength="40"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Zip:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="10" ID="txtZip" TabIndex="7" runat="server" CssClass="textbox"
                                                MaxLength="10"></asp:TextBox>
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
                                            <asp:LinkButton TabIndex="8" ID="lbSave" runat="server" Text="Add" CssClass="PopupFormButton"
                                                OnClick="lbSave_Click"></asp:LinkButton>
                                        </td>
                                        <td align="center">
                                            <asp:LinkButton TabIndex="9" ID="lbCancel" runat="server" Text="Cancel" CssClass="PopupFormButton"
                                                OnClick="lbCancel_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
            <asp:ObjectDataSource ID="odsUsers" runat="server" TypeName="NewsletterMSBLL.BOUsers"
                EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="TotalNumberOfNewsletterUsers"
                SelectMethod="GetNewsletterUsersPagedAndSorted" StartRowIndexParameterName="pageIndex"
                SortParameterName="sortExpression">
                <SelectParameters>
                    <asp:ControlParameter Name="searchValue" ControlID="txtSearchUser" DefaultValue=""
                        PropertyName="Text" Type="String" />
                    <asp:SessionParameter Name="newsletterId" SessionField="NewsletterID" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbImportEmailList" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
