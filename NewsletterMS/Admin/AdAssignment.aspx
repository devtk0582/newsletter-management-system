<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdAssignment.aspx.cs" Inherits="NewsletterMS.Admin.AdAssignment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="AdEntriesUpdatePanel" runat="server">
        <ContentTemplate>
            <table width="90%" cellspacing="5" cellpadding="3">
                <tr>
                    <td colspan="2" align="center" valign="top" style="font-size: 18px; color: Maroon;">
                        Ad Assignment
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErrAdAssignment" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                                Advertiser: 
                                <asp:DropDownList ID="ddlAdvertisers" runat="server" AutoPostBack="true"
                                    onselectedindexchanged="ddlAdvertisers_SelectedIndexChanged">
                                </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center">
                        <div>
                            <div>
                                Ad Selection:
                            </div>
                            <div style="border: 1px solid black; height: 250px; width: 350px; overflow: auto">
                                <asp:HiddenField ID="hfSelectedAd" runat="server" />
                                <asp:ListView ID="lvAds" runat="server" onitemcommand="lvAds_ItemCommand">
                                    <LayoutTemplate>
    <table runat="server" id="table1" width="100%">
      <tr runat="server" id="itemPlaceholder"></tr>
    </table>
  </LayoutTemplate>
  <ItemTemplate>
    <tr id="trAdDesc" runat="server" style="width: 100%">
      <td id="Td1" runat="server">
          <asp:LinkButton ID="lbAdDesc" runat="server" Text='<%#Eval("AdDescription") %>' CommandArgument='<%#Eval("AdID") %>' CommandName="SelectAd"></asp:LinkButton>
      </td>
    </tr>
  </ItemTemplate>

                                </asp:ListView>
                            </div>
                        </div>
                    </td>
                    <td align="center">
                        <table>
                            <tr>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxA001" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxA001_Click">A001</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxA001" runat="server" />
                                </td>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxA002" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxA002_Click">A002</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxA002" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxN001" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxN001_Click">N001</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxN001" runat="server" />
                                </td>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxN002" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxN002_Click">N002</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxN002" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxN003" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxN003_Click">N003</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxN003" runat="server" />
                                </td>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxN004" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxN004_Click">N004</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxN004" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxN005" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxN005_Click">N005</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxN005" runat="server" />
                                </td>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxN006" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxN006_Click">N006</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxN006" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxN007" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxN007_Click">N007</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxN007" runat="server" />
                                </td>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxN008" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxN008_Click">N008</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxN008" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxA003" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxA003_Click">A003</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxA003" runat="server" />
                                </td>
                                <td style="border: 1px solid black;">
                                    <asp:LinkButton ID="lbBoxA004" runat="server" Width="100" Height="40" 
                                        BackColor="White" onclick="lbBoxA004_Click">A004</asp:LinkButton>
                                    <asp:HiddenField ID="hfBoxA004" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                <td align="center">
                    Ad Description:
                        <asp:TextBox ID="txtAdDesc" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                <td align="center">
                    Ad Instruction:
                        <asp:TextBox ID="txtAdInstructions" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="lbSave" runat="server" Text="Save" 
                            CssClass="lbPaddingGrayStyle" onclick="lbSave_Click"></asp:LinkButton>
                    </td>
                </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
