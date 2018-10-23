<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Sections.aspx.cs" Inherits="NewsletterMS.Admin.Sections" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="SectionsUpdatePanel" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1">
                <tr>
                    <td align="center" valign="top" style="font-size: 18px; color: Maroon;">
                        Sections Maintenance
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div style="float: right; white-space: nowrap; padding: 5px;">
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbAddSection" runat="server" Text="Add Section" CssClass="lbPaddingGrayStyle"
                                    OnClick="lbAddSection_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr valign="top" style="padding: 5px;">
                    <td>
                        <asp:GridView ID="gvSections" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                            AllowSorting="True" AllowPaging="True" EnableSortingAndPagingCallbacks="True"
                            CellPadding="3" PageSize="14" Height="100%" Width="100%" OnSorting="gvSections_Sorting"
                            OnRowCommand="gvSections_RowCommand" DataSourceID="odsSections">
                            <HeaderStyle CssClass="gvHeaderStyle" />
                            <EmptyDataTemplate>
                                <p class="EmptyMessageStyle">
                                    No Section Found</p>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Name" SortExpression="Name" ItemStyle-Width="40%"
                                    HeaderStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfSectionID" runat="server" Value='<%#Eval("ID").ToString()%>' />
                                        <asp:LinkButton CommandName="EditSection" CommandArgument='<%#Eval("ID").ToString()%>'
                                            ID="lnkEditSection" Text='<%#Eval("Name").ToString()%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Code" ItemStyle-Width="30%" HeaderStyle-Width="30%" SortExpression="Code"
                                    HeaderText="Code"></asp:BoundField>
                                <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30%"
                                    ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandName="DeleteSection" CommandArgument='<%#Eval("ID").ToString()%>'
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
            <cc1:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="dummy" PopupDragHandleControlID="pnlPopUp"
                PopupControlID="pnlPopUp" BackgroundCssClass="popupBG" DropShadow="true" />
            <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup">
                <center>
                    <table class="PopupFormBG" width="60%" cellpadding="0" cellspacing="0">
                        <tr style="height: 30px;">
                            <td align="left" class="PopupFormTitleStyle">
                                <asp:Label ID="lblTitle" runat="server" Text="Add / Edit Section"></asp:Label>
                                <asp:HiddenField ID="hfSectionID" runat="server" />
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
                                            <strong>Name</strong><span class="requiredStyle">*</span>:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="30" ID="txtSectionName" TabIndex="1" runat="server" CssClass="textbox"
                                                MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Code</strong>:
                                        </td>
                                        <td>
                                            <asp:TextBox Columns="50" ID="txtSectionCode" TabIndex="2" runat="server" CssClass="textbox"
                                                MaxLength="50" ReadOnly="true" ></asp:TextBox>
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
            <asp:ObjectDataSource ID="odsSections" runat="server" TypeName="NewsletterMSBLL.BOSections"
                EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="TotalNumberOfSections"
                SelectMethod="GetSectionsPagedAndSorted" StartRowIndexParameterName="pageIndex"
                SortParameterName="sortExpression">
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
