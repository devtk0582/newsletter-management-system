<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Publications.aspx.cs" Inherits="NewsletterMS.Admin.Publications" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="PublicationsUpdatePanel" runat="server">
        <ContentTemplate>
             <table width="100%" cellspacing="1" cellpadding="1">
                <tr>
                   <td align="center" valign="top" style="font-size: 18px; color: Maroon;">
                                Publication Maintenance
                   </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErrPublication" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div style="float: right; white-space: nowrap; padding: 5px;">
                    <div style="display: inline-block;">
                        <asp:Label ID="lblSearchPublication" runat="server" Text="Search Publication:" AssociatedControlID="txtSearchPublication"></asp:Label>
                        
                                     <asp:TextBox ID="txtSearchPublication" runat="server" Text=""></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWESearch" runat="server" TargetControlID="txtSearchPublication" WatermarkText="Search Here">
                        </cc1:TextBoxWatermarkExtender>
                        <cc1:AutoCompleteExtender ID="txtSearchPublication_AutoCompleteExtender" runat="server" Enabled="true" ServicePath="~/AutoCompleteWebService.asmx"
                                         MinimumPrefixLength="1" TargetControlID="txtSearchPublication" ServiceMethod="SearchPublications">
                                        </cc1:AutoCompleteExtender>
                    </div>
                    <div style="display: inline-block; margin: 5px;">
                        <asp:LinkButton ID="lbSearchPublication" runat="server" CssClass="lbPaddingGrayStyle" Text="Go" OnClick="lbSearchPublication_Click"></asp:LinkButton>
                    </div>
                    <div style="display: inline-block; margin: 5px;">
                        <asp:LinkButton ID="lbShowAll" runat="server" CssClass="lbPaddingGrayStyle" Text="Show All" OnClick="lbShowAll_Click"></asp:LinkButton>
                    </div>
                    <div style="display: inline-block; margin: 5px;">
                        <asp:LinkButton ID="lbAddPublication" runat="server" Text="Add Publication" CssClass="lbPaddingGrayStyle" onclick="lbAddPublication_Click"></asp:LinkButton>
                    </div>
                            </div>
                        <div style="padding: 5px;">
                            <asp:GridView ID="gvPublications" runat="server" AutoGenerateColumns="False" 
                            BorderWidth="0px" AllowSorting="True" AllowPaging="True"
                                        EnableSortingAndPagingCallbacks="True" CellPadding="3" PageSize="14"
                                        Height="100%" Width="100%" OnSorting="gvPublications_Sorting" 
                            OnRowCommand="gvPublications_RowCommand" OnRowDataBound="gvPublications_RowDataBound" 
                            DataSourceID="odsPublications">
                                        <HeaderStyle CssClass="gvHeaderStyle"/>
                                        <EmptyDataTemplate>
                                        <p class="EmptyMessageStyle">No Publication Found</p>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Publication Name" SortExpression="Name" ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfPublicationID" runat="server" Value='<%#Eval("NewsletterID").ToString()%>' />
                                                    <asp:LinkButton CommandName="EditPublication" CommandArgument='<%#Eval("NewsletterID").ToString()%>'
                                                        ID="lnkEditPublication" Text='<%#Eval("NewsletterName").ToString()%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NewsletterType" ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                                SortExpression="Type" HeaderText="Type">                                              
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NewsletterFrequency" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                                SortExpression="Frequency" HeaderText="Frequency">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PrimaryContactEmail" ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                                SortExpression="ContactEmail" HeaderText="Contact Email">
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="DeletePublication" CommandArgument='<%#Eval("NewsletterID").ToString()%>'
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
                        <asp:Label ID="lblTitle" runat="server" Text="Add / Edit Publication" ></asp:Label>
                        <asp:HiddenField ID="hfPublicationID" runat="server" />
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
                                    <strong>Publication Name</strong><span class="requiredStyle">*</span>:
                                </td>
                                <td>
                                    <asp:TextBox Columns="30" ID="txtPublicationName" TabIndex="1" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="trURL" visible="false">
                                <td>
                                    URL:
                                </td>
                                <td>
                                    <asp:TextBox Columns="100" ID="lblURL" TabIndex="2" runat="server" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Type</strong><span class="requiredStyle">*</span>:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTypes" runat="server" TabIndex="3">
                                    <asp:ListItem Text="6 Boxes" Value="6B" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="2 Boxes" Value="2B"></asp:ListItem>
                                    <asp:ListItem Text="4 Boxes" Value="4B"></asp:ListItem>
                                    <asp:ListItem Text="8 Boxes" Value="8B"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Background Color:
                                </td>
                                <td>
                                    <asp:TextBox Columns="30" ID="txtBackgroundColor" TabIndex="4" runat="server" CssClass="textbox"
                                        MaxLength="30"></asp:TextBox>
                                    <cc1:ColorPickerExtender ID="txtBackgroundColor_ColorPickerExtender" 
                                        runat="server" Enabled="True" TargetControlID="txtBackgroundColor" >
                                    </cc1:ColorPickerExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Section Color:
                                </td>
                                <td>
                                <asp:TextBox Columns="30" ID="txtSectionColor" TabIndex="5" runat="server" CssClass="textbox"
                                        MaxLength="30"></asp:TextBox>
                                    <cc1:ColorPickerExtender ID="txtSectionColor_ColorPickerExtender" 
                                        runat="server" Enabled="True" TargetControlID="txtSectionColor">
                                    </cc1:ColorPickerExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Primary Contact Name</strong><span class="requiredStyle">*</span>:
                                </td>
                                <td>
                                    <asp:TextBox Columns="30" ID="txtContactName" TabIndex="6" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Primary Contact Email</strong><span class="requiredStyle">*</span>:
                                </td>
                                <td>
                                   <asp:TextBox Columns="50" ID="txtContactEmail" TabIndex="7" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Primary Contact Phone:
                                </td>
                                <td>
                                   <asp:TextBox Columns="20" ID="txtContactPhone" TabIndex="8" runat="server" CssClass="textbox"
                                        MaxLength="20"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Publication Frequency:
                                </td>
                                <td>
                                    <asp:RadioButtonList TabIndex="9" ID="rblFrequency" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Daily" Value="D"></asp:ListItem>
                                        <asp:ListItem Text="Bi-Weekly" Value="B"></asp:ListItem>
                                        <asp:ListItem Text="Weekly" Value="W" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Monthly" Value="M"></asp:ListItem>
                                    </asp:RadioButtonList>
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
                                    <asp:LinkButton TabIndex="10" ID="lbSave" runat="server" Text="Add" CssClass="PopupFormButton" OnClick="lbSave_Click"></asp:LinkButton>
                                </td>
                                <td align="center">
                                    <asp:LinkButton TabIndex="11" ID="lbCancel" runat="server" Text="Cancel" CssClass="PopupFormButton" OnClick="lbCancel_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    </center>
</asp:Panel>
            <asp:ObjectDataSource ID="odsPublications" runat="server" TypeName="NewsletterMSBLL.BOPublications" EnablePaging="True" 
                MaximumRowsParameterName="pageSize" SelectCountMethod="TotalNumberOfPublications" 
                SelectMethod="GetPublicationsPagedAndSorted" StartRowIndexParameterName="pageIndex" 
                SortParameterName="sortExpression">
            <SelectParameters>
                <asp:ControlParameter Name="searchValue" ControlID="txtSearchPublication" DefaultValue="" PropertyName="Text" Type="String" />
            </SelectParameters>    
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
