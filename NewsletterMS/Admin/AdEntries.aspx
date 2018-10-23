<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="AdEntries.aspx.cs" Inherits="NewsletterMS.Admin.AdEntries" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="AdEntriesUpdatePanel" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1">
                <tr>
                    <td align="center" valign="top" style="font-size: 18px; color: Maroon;">
                        Ads Maintenance
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErrAds" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trViewAds" valign="top">
                    <td>
                        <div style="float: right; white-space: nowrap; padding: 5px;">
                            <div style="display: inline-block;">
                                <asp:Label ID="lblSearchAd" runat="server" Text="Search Ad:" AssociatedControlID="txtSearchAd"></asp:Label>
                                <asp:TextBox ID="txtSearchAd" runat="server" Text=""></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="txtWESearch" runat="server" TargetControlID="txtSearchAd"
                                    WatermarkText="Search Here">
                                </cc1:TextBoxWatermarkExtender>
                                <cc1:AutoCompleteExtender ID="txtSearchAd_AutoCompleteExtender" runat="server" Enabled="true"
                                    ServicePath="~/AutoCompleteWebService.asmx" MinimumPrefixLength="1" TargetControlID="txtSearchAd"
                                    ServiceMethod="SearchAds">
                                </cc1:AutoCompleteExtender>
                            </div>
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbSearchAd" runat="server" CssClass="lbPaddingGrayStyle" Text="Go"
                                    OnClick="lbSearchAd_Click"></asp:LinkButton>
                            </div>
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbShowAll" runat="server" CssClass="lbPaddingGrayStyle" Text="Show All"
                                    OnClick="lbShowAll_Click"></asp:LinkButton>
                            </div>
                            <div style="display: inline-block; margin: 5px;">
                                <asp:LinkButton ID="lbAddAd" runat="server" Text="Add Ad" CssClass="lbPaddingGrayStyle"
                                    OnClick="lbAddAd_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div style="padding: 5px;">
                            <asp:GridView ID="gvAds" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                AllowSorting="True" AllowPaging="True" EnableSortingAndPagingCallbacks="True"
                                CellPadding="3" PageSize="14" Height="100%" Width="100%" OnSorting="gvAds_Sorting"
                                OnRowCommand="gvAds_RowCommand" OnRowDataBound="gvAds_RowDataBound" DataSourceID="odsAds">
                                <HeaderStyle CssClass="gvHeaderStyle" />
                                <EmptyDataTemplate>
                                    <p class="EmptyMessageStyle">
                                        No Ad Found</p>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Ad Description" SortExpression="Desc" ItemStyle-Width="25%"
                                        HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfAdID" runat="server" Value='<%#Eval("AdID").ToString()%>' />
                                            <asp:LinkButton CommandName="EditAd" CommandArgument='<%#Eval("AdID").ToString()%>'
                                                ID="lnkEditAd" Text='<%#Eval("AdDescription").ToString()%>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AdCampaign" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                        SortExpression="Campaign" HeaderText="Campaign"></asp:BoundField>
                                    <asp:BoundField DataField="AdRegionCode" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                        SortExpression="Region" HeaderText="Region"></asp:BoundField>
                                    <asp:BoundField DataField="AdType" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                        SortExpression="Type" HeaderText="Type"></asp:BoundField>
                                    <asp:BoundField DataField="AdPrice" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                        SortExpression="Price" HeaderText="Price" DataFormatString="{0:C}"></asp:BoundField>
                                    <asp:BoundField DataField="Active" ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                        SortExpression="Status" HeaderText="Status"></asp:BoundField>
                                </Columns>
                                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gvPagerStyle" />
                                <RowStyle CssClass="gvRowStyle" />
                                <AlternatingRowStyle CssClass="gvAltRowStyle" />
                                <SortedAscendingHeaderStyle CssClass="GVSortingAsc" />
                                <SortedDescendingHeaderStyle CssClass="GVSortingDesc" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr runat="server" id="trAddAd" visible="false">
                    <td>
                        <table class="PopupFormBG" width="60%" cellpadding="0" cellspacing="0">
                            <tr style="height: 30px;">
                                <td align="left" class="PopupFormTitleStyle">
                                    <asp:Label ID="lblTitle" runat="server" Text="Add / Edit Ad"></asp:Label>
                                    <asp:HiddenField ID="hfAdID" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr style="padding: 10px 8px 5px 8px;">
                                <td align="center">
                                    <table cellpadding="2" cellspacing="2" width="100%" class="PopUpFormMainTbl">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlAdBox" runat="server" Width="100%" BorderColor="Black" BorderWidth="1px"
                                                    BorderStyle="Inset">
                                                    <div runat="server" id="divEditNews">
                                                        <div style="padding: 10px 3px 3px 3px; background-position: center">
                                                            <div>
                                                                <asp:Image ID="imgView" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                                                                    runat="server" ImageUrl="~/Images/default_img_medium.png" />
                                                                <br />
                                                                <div runat="server" id="divUpload">
                                                                    <div style="display: inline-block">
                                                                        <asp:FileUpload ID="UploadImage" runat="server" />
                                                                    </div>
                                                                    <div style="display: inline-block; margin-left: 10px;">
                                                                        <asp:LinkButton ID="lbUpload" runat="server" Text="Upload" CssClass="lbPaddingGrayStyle"
                                                                            OnClick="lbUpload_Click"></asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                                <div>
                                                                        Video Link:
                                                                        <asp:TextBox ID="txtVideoLink" runat="server"></asp:TextBox>
                                                                </div>
                                                        </div>
                                                        <div style="margin: 5px;">
                                                            <div class="divBlock">
                                                                <asp:TextBox ID="txtContent" runat="server" Columns="70" TextMode="MultiLine" Rows="5"
                                                                    Text="This is sample"></asp:TextBox>
                                                                <cc1:HtmlEditorExtender ID="txtContent_HtmlEditorExtender" runat="server" Enabled="True"
                                                                    EnableSanitization="false" TargetControlID="txtContent">
                                                                    <Toolbar>
                                                                        <cc1:Undo />
                                                                        <cc1:Redo />
                                                                        <cc1:Bold />
                                                                        <cc1:Italic />
                                                                        <cc1:Underline />
                                                                        <cc1:StrikeThrough />
                                                                        <cc1:Subscript />
                                                                        <cc1:Superscript />
                                                                        <cc1:JustifyLeft />
                                                                        <cc1:JustifyCenter />
                                                                        <cc1:JustifyRight />
                                                                        <cc1:JustifyFull />
                                                                        <cc1:InsertOrderedList />
                                                                        <cc1:InsertUnorderedList />
                                                                        <cc1:CreateLink />
                                                                        <cc1:UnLink />
                                                                        <cc1:RemoveFormat />
                                                                        <cc1:SelectAll />
                                                                        <cc1:UnSelect />
                                                                        <cc1:Delete />
                                                                        <cc1:Cut />
                                                                        <cc1:Copy />
                                                                        <cc1:Paste />
                                                                        <cc1:BackgroundColorSelector />
                                                                        <cc1:ForeColorSelector />
                                                                        <cc1:FontNameSelector />
                                                                        <cc1:FontSizeSelector />
                                                                        <cc1:Indent />
                                                                        <cc1:Outdent />
                                                                        <cc1:InsertHorizontalRule />
                                                                        <cc1:HorizontalSeparator />
                                                                    </Toolbar>
                                                                </cc1:HtmlEditorExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                            <td valign="top">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            Advertiser:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAdvertisers" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Campaign:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Columns="30" ID="txtCampaign" TabIndex="3" runat="server" CssClass="textbox"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Region:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRegions" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Type:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTypes" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>Ad Description</strong><span class="requiredStyle">*</span>:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Columns="30" ID="txtAdDesc" TabIndex="1" runat="server" CssClass="textbox"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Price:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Columns="5" ID="txtPrice" TabIndex="3" runat="server" CssClass="textbox"
                                                                MaxLength="8"></asp:TextBox>
                                                            K
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Ad Instruction:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Columns="30" ID="txtInstruction" TabIndex="1" runat="server" CssClass="textbox"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Ad Web Link:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox Columns="30" ID="txtLink" TabIndex="1" runat="server" CssClass="textbox"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Status:
                                                        </td>
                                                        <td>                                                      <asp:DropDownList ID="ddlStatus" runat="server">
                                                                <asp:ListItem Text="Active" Value="True" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Inactive" Value="False"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr style="margin: 15px 0 10px 0;">
                                                        <td align="right" colspan="2">
                                                            <table width="100px">
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:LinkButton TabIndex="6" ID="lbSave" runat="server" Text="Add" CssClass="PopupFormButton"
                                                                            OnClick="lbSave_Click"></asp:LinkButton>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:LinkButton TabIndex="7" ID="lbCancel" runat="server" Text="Cancel" CssClass="PopupFormButton"
                                                                            OnClick="lbCancel_Click"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                </td>
                            </tr>
                             
                        </table>
                    </td>
                </tr>
            </table>
            <asp:ObjectDataSource ID="odsAds" runat="server" TypeName="NewsletterMSBLL.BOAds"
                EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="TotalNumberOfAds"
                SelectMethod="GetAdsPagedAndSorted" StartRowIndexParameterName="pageIndex" SortParameterName="sortExpression">
                <SelectParameters>
                    <asp:ControlParameter Name="searchValue" ControlID="txtSearchAd" DefaultValue=""
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
