<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChartEnquiryToAddMember.aspx.cs" Inherits="NDOnlineGym_2017.ChartEnquiryToAddMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
        .records { padding:5px 10px  }
        .records th { padding:5px 10px;width:30%  }
         .records td { padding:5px 10px;width:30%  }
          .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
        <div class="form-name-header">
       <h3>Enquiry To Add Member Chart
           <div class="navigation">
              <ul>
                  <li>Chart &nbsp; > &nbsp;</li>
                  <li>Enquiry To Add Member</li>
              </ul>
           </div>
      </h3>
    </div>
    <div class="divForm">
        <div class="form-panel">
            <table style="width: 100%;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Select Year </span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddlYear" runat="server" CssClass="ddl" TabIndex="6">
                                              
                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                           
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Select Chart</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddlChart" runat="server" CssClass="ddl" TabIndex="6" OnSelectedIndexChanged="ddlChart_SelectedIndexChanged" AutoPostBack="true">
                                               <asp:ListItem >Column</asp:ListItem>
                                                    <asp:ListItem >Line</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
            </table>
            <div id="divChart" style="width:100%;height:410px;border:1px solid silver;padding:10px" >
                <table>
                    <tr>
                        <td style="width:35%"><strong>Total Enquiries : </strong><asp:Label ID="Label23" runat="server" Text="2987869"></asp:Label></td>
                        <td style="width:35%"><strong>Total Add Members : </strong><asp:Label ID="Label24" runat="server" Text="2987869"></asp:Label></td>
                    </tr>
                </table>
                <div id="divChartSection" style="width:65%;height:380px;float:left">


                   <%----------------------------Chart Section----------------------------------------%>

                     <asp:Chart ID="Chart8" runat="server" Width="640px" OnLoad="Chart8_Load">
            <Series>
                <asp:Series Name="Enquiry" ToolTip="#VALX, #VAL" Legend="Legend1" LegendMapAreaAttributes="Enquiry" LegendText="Enquiry" LegendToolTip="Enquiry" Color="60, 140, 240"></asp:Series>
                <asp:Series ChartArea="ChartArea1" Name="AddMember" ToolTip="#VALX, #VAL" Legend="Legend1" Color="12, 68, 143">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisY IntervalAutoMode="VariableCount" Title="Enquiries and Members">
                        <MajorGrid LineColor="White" LineDashStyle="Dash" />
                    </AxisY>
                    <AxisX IntervalAutoMode="VariableCount" Title="Months">
                        <MajorGrid LineColor="White" LineDashStyle="Dash" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1">
                </asp:Legend>
            </Legends>
        </asp:Chart>

                </div>
                 <div id="div1" style="width:35%;height:380px;float:left">
                     <table class="records">
                         <tr>
                             <th>Month</th>
                             <th>Enquiry</th>
                             <th>Add Member</th>
                         </tr>
                         <tr>
                             <th>January</th>
                             <td><asp:Label ID="lblJanEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblJanRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>February</th>
                             <td><asp:Label ID="lblFebEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblFebRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>March</th>
                             <td><asp:Label ID="lblMarEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblMarRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>April</th>
                             <td><asp:Label ID="lblAprEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblAprRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                          <tr>
                             <th>May</th>
                             <td><asp:Label ID="lblMayEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblMayRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>June</th>
                             <td><asp:Label ID="lbljuneEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lbljuneRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>July</th>
                             <td><asp:Label ID="lblJulyEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblJulyRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>August</th>
                             <td><asp:Label ID="lblAugEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblAugRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                          <tr>
                             <th>September</th>
                             <td><asp:Label ID="lblSepEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblSepRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>October</th>
                             <td><asp:Label ID="lblOctEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblOctRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>November</th>
                             <td><asp:Label ID="lblNovEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblNovRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>December</th>
                             <td><asp:Label ID="lblDecEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblDecRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                          <tr>
                             <th>Total</th>
                             <td><asp:Label ID="lblTotEnquiry" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblTotRegMember" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                     </table>
                </div>
            </div>
        </div>
    </div>
        </div>
</asp:Content>
