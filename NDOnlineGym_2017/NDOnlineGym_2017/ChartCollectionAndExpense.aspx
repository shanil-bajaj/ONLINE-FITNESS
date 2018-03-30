<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChartCollectionAndExpense.aspx.cs" Inherits="NDOnlineGym_2017.ChartCollectionAndExpense" %>
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
       <h3>Collection / Expense Chart
           <div class="navigation">
              <ul>
                  <li>Chart &nbsp; > &nbsp;</li>
                  <li>Collection / Expense</li>
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
                        <td style="width:35%"><strong>Total Collection : </strong><asp:Label ID="lblTotalCollection" runat="server" Text="2987869"></asp:Label></td>
                        <td style="width:35%"><strong>Total Expense : </strong><asp:Label ID="lblTotalExpense" runat="server" Text="2987869"></asp:Label></td>
                    </tr>
                </table>
                <div id="divChartSection" style="width:65%;height:380px;float:left">


                   <%----------------------------Chart Section----------------------------------------%>

                     <asp:Chart ID="Chart8" runat="server" Width="600px">
            <Series>
                <asp:Series Name="Collection" ToolTip="#VALX, Rs. #VAL{N}" Legend="Legend1" LegendMapAreaAttributes="Collection" LegendText="Collection" LegendToolTip="Collection" Color="65, 140, 240"></asp:Series>
                <asp:Series ChartArea="ChartArea1" Name="Expense" ToolTip="#VALX, Rs. #VAL{N}" Legend="Legend1" Color="12, 68, 143">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisY IntervalAutoMode="VariableCount" Title="Collection & Expense">
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
                             <th>Collection</th>
                             <th>Expense</th>
                         </tr>
                         <tr>
                             <th>January</th>
                             <td><asp:Label ID="lblJanCollection" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblJanExpanse" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>February</th>
                             <td><asp:Label ID="lblFebColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblFebExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>March</th>
                             <td><asp:Label ID="lblMarColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblMarExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>April</th>
                             <td><asp:Label ID="lblAprColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblAprExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                          <tr>
                             <th>May</th>
                             <td><asp:Label ID="lblMayColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblMayExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>June</th>
                             <td><asp:Label ID="lbljuneColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lbljuneExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>July</th>
                             <td><asp:Label ID="lblJulyColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblJulyExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>August</th>
                             <td><asp:Label ID="lblAugColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblAugExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                          <tr>
                             <th>September</th>
                             <td><asp:Label ID="lblSepColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblSepExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>October</th>
                             <td><asp:Label ID="lblOctColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblOctExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>November</th>
                             <td><asp:Label ID="lblNovColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblNovExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                         <tr>
                             <th>December</th>
                             <td><asp:Label ID="lblDecColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblDecExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                          <tr>
                             <th>Total</th>
                             <td><asp:Label ID="lblTotColl" runat="server" Text="8767564"></asp:Label></td>
                             <td><asp:Label ID="lblTotExp" runat="server" Text="8767564"></asp:Label></td>
                         </tr>
                     </table>
                </div>
            </div>
        </div>
    </div>
        </div>
</asp:Content>
