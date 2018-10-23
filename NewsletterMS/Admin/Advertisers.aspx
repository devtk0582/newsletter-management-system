<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Advertisers.aspx.cs" Inherits="NewsletterMS.Admin.Advertisers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="AdvertisersUpdatePanel" runat="server">
        <ContentTemplate>
             <table width="100%" cellspacing="1" cellpadding="1" >
                <tr>
                   <td align="center" valign="top" style="font-size: 18px; color: Maroon;">
                                Advertisers Maintenance
                   </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErrAdvertiser" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div style="float: right; white-space: nowrap; padding: 5px;">
                    <div style="display: inline-block;">
                        <asp:Label ID="lblSearchAdvertiser" runat="server" Text="Search Advertiser:" AssociatedControlID="txtSearchAdvertiser"></asp:Label>
                        
                                     <asp:TextBox ID="txtSearchAdvertiser" runat="server" Text=""></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWESearch" runat="server" TargetControlID="txtSearchAdvertiser" WatermarkText="Search Here">
                        </cc1:TextBoxWatermarkExtender>
                        <cc1:AutoCompleteExtender ID="txtSearchAdvertiser_AutoCompleteExtender" runat="server" Enabled="true" ServicePath="~/AutoCompleteWebService.asmx"
                                         MinimumPrefixLength="1" TargetControlID="txtSearchAdvertiser" ServiceMethod="SearchAdvertisers">
                                        </cc1:AutoCompleteExtender>
                    </div>
                    <div style="display: inline-block; margin: 5px;">
                        <asp:LinkButton ID="lbSearchAdvertiser" runat="server" CssClass="lbPaddingGrayStyle" Text="Go" OnClick="lbSearchAdvertiser_Click"></asp:LinkButton>
                    </div>
                    <div style="display: inline-block; margin: 5px;">
                        <asp:LinkButton ID="lbShowAll" runat="server" CssClass="lbPaddingGrayStyle" Text="Show All" OnClick="lbShowAll_Click"></asp:LinkButton>
                    </div>
                    <div style="display: inline-block; margin: 5px;">
                        <asp:LinkButton ID="lbAddAdvertiser" runat="server" Text="Add Advertiser" CssClass="lbPaddingGrayStyle" onclick="lbAddAdvertiser_Click"></asp:LinkButton>
                    </div>
                            </div>
                        <div style="padding: 5px;">
                            <asp:GridView ID="gvAdvertisers" runat="server" AutoGenerateColumns="False" 
                            BorderWidth="0px" AllowSorting="True" AllowPaging="True"
                                        EnableSortingAndPagingCallbacks="True" CellPadding="3" PageSize="14"
                                        Height="100%" Width="100%" OnSorting="gvAdvertisers_Sorting" 
                            OnRowCommand="gvAdvertisers_RowCommand" OnRowDataBound="gvAdvertisers_RowDataBound" 
                            DataSourceID="odsAdvertisers">
                                        <HeaderStyle CssClass="gvHeaderStyle"/>
                                        <EmptyDataTemplate>
                                        <p class="EmptyMessageStyle">No Advertiser Found</p>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Advertiser Name" SortExpression="Name" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfAdvertiserID" runat="server" Value='<%#Eval("AdvertiserID").ToString()%>' />
                                                    <asp:LinkButton CommandName="EditAdvertiser" CommandArgument='<%#Eval("AdvertiserID").ToString()%>'
                                                        ID="lnkEditAdvertiser" Text='<%#Eval("AdvertiserName").ToString()%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AdvertiserRegionType" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                                SortExpression="Type" HeaderText="Type">                                              
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AdvertiserContact1Name" ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                                SortExpression="ContactName" HeaderText="Contact Name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AdvertiserContact1Email" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                                SortExpression="ContactEmail" HeaderText="Contact Email">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AdvertiserContact1Phone" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                                SortExpression="ContactPhone" HeaderText="Contact Phone">
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="DeleteAdvertiser" CommandArgument='<%#Eval("AdvertiserID").ToString()%>'
                                                        ID="lnkDelete" Text="Delete" runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last"/>
                                        <PagerStyle CssClass="gvPagerStyle"/>
                                        <RowStyle CssClass="gvRowStyle"/>
                                        <AlternatingRowStyle CssClass="gvAltRowStyle" />
                                        <SortedAscendingHeaderStyle CssClass="GVSortingAsc" />
                                        <SortedDescendingHeaderStyle CssClass="GVSortingDesc" />
                                    </asp:GridView>
                            </div>
                    </td>
                </tr>
            </table>
            <input id="dummy" type="button" style="display: none" runat="server" />
<cc1:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="dummy"
   PopupDragHandleControlID="pnlPopUp"  PopupControlID="pnlPopUp" BackgroundCssClass="popupBG" DropShadow="true" />
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup">
<center>
            <table class="PopupFormBG" width="60%" cellpadding="0" cellspacing="0">
                <tr style="height: 30px;">
                    <td align="left" class="PopupFormTitleStyle">
                        <asp:Label ID="lblTitle" runat="server" Text="Add / Edit Advertiser" ></asp:Label>
                        <asp:HiddenField ID="hfAdvertiserID" runat="server" />
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
                                    <strong>Advertiser Name</strong><span class="requiredStyle">*</span>:
                                </td>
                                <td>
                                    <asp:TextBox Columns="30" ID="txtAdvertiserName" TabIndex="1" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    Region Type:
                                </td>
                                <td>
                                    <asp:TextBox Columns="30" ID="txtAdvertiserRegionType" TabIndex="2" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Primary Contact Name</strong><span class="requiredStyle">*</span>:
                                </td>
                                <td>
                                    <asp:TextBox Columns="30" ID="txtContact1Name" TabIndex="3" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    <strong>Primary Contact Email</strong><span class="requiredStyle">*</span>:
                                </td>
                                <td>
                                   <asp:TextBox Columns="50" ID="txtContact1Email" TabIndex="4" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Primary Contact Phone:
                                </td>
                                <td>
                                   <asp:TextBox Columns="20" ID="txtContact1Phone" TabIndex="5" runat="server" CssClass="textbox"
                                        MaxLength="20"></asp:TextBox>
                                </td>
                                <td>
                                    Primary Contact Phone2:
                                </td>
                                <td>
                                   <asp:TextBox Columns="20" ID="txtContact1Phone2" TabIndex="5" runat="server" CssClass="textbox"
                                        MaxLength="20"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Secondary Contact Name:
                                </td>
                                <td>
                                    <asp:TextBox Columns="30" ID="txtContact2Name" TabIndex="3" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    Secondary Contact Email:
                                </td>
                                <td>
                                   <asp:TextBox Columns="50" ID="txtContact2Email" TabIndex="4" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Secondary Contact Phone:
                                </td>
                                <td>
                                   <asp:TextBox Columns="20" ID="txtContact2Phone" TabIndex="5" runat="server" CssClass="textbox"
                                        MaxLength="20"></asp:TextBox>
                                </td>
                                <td>
                                    Secondary Contact Phone2:
                                </td>
                                <td>
                                   <asp:TextBox Columns="20" ID="txtContact2Phone2" TabIndex="5" runat="server" CssClass="textbox"
                                        MaxLength="20"></asp:TextBox>
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
                                    <asp:LinkButton TabIndex="6" ID="lbSave" runat="server" Text="Add" CssClass="PopupFormButton" OnClick="lbSave_Click"></asp:LinkButton>
                                </td>
                                <td align="center">
                                    <asp:LinkButton TabIndex="7" ID="lbCancel" runat="server" Text="Cancel" CssClass="PopupFormButton" OnClick="lbCancel_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    </center>
</asp:Panel>
            <asp:ObjectDataSource ID="odsAdvertisers" runat="server" TypeName="NewsletterMSBLL.BOAdvertisers" EnablePaging="True" 
                MaximumRowsParameterName="pageSize" SelectCountMethod="TotalNumberOfAdvertisers" 
                SelectMethod="GetAdvertisersPagedAndSorted" StartRowIndexParameterName="pageIndex" 
                SortParameterName="sortExpression">
            <SelectParameters>
                <asp:ControlParameter Name="searchValue" ControlID="txtSearchAdvertiser" DefaultValue="" PropertyName="Text" Type="String" />
            </SelectParameters>    
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
