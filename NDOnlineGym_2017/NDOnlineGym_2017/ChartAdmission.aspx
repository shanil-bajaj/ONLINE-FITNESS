<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChartAdmission.aspx.cs" Inherits="NDOnlineGym_2017.ChartAdmission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
       <div class="form-name-header">
       <h3>Admission Chart
           <div class="navigation">
              <ul>
                  <li>Chart &nbsp; > &nbsp;</li>
                  <li>Admission</li>
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
            <div id="divChart" style="width:100%;height:400px;border:1px solid silver;padding:10px" >
                <table style="width:100%;">
                    <tr>
                        <td style="width:35%"><strong>New Total : </strong><asp:Label ID="lblNew" runat="server" Text="2987869"></asp:Label></td>
                     <%--   <td style="width:35%"><strong>Renew : </strong><asp:Label ID="lblTotalExpense" runat="server" Text="2987869"></asp:Label></td>
                        <td style="width:35%"><strong>Upgrade : </strong><asp:Label ID="Label1" runat="server" Text="2987869"></asp:Label></td>--%>
                    </tr>
                </table>
                <div id="divChartSection" style="width:100%;height:380px;">


                   <%----------------------------Chart Section----------------------------------------%>
                     <asp:Chart ID="Chart1" runat="server" Width="718px" ClientIDMode="Static" Height="250px">
                        <Series>
            <asp:Series Name="New" LabelBorderWidth="6" ToolTip="#VALX, #VAL" Legend="Legend1">
            </asp:Series>
         <%--   <asp:Series ChartArea="ChartArea1"  Legend="Legend1" Name="Renew" ToolTip="#VALX, #VAL"  Color="89, 208, 208">
            </asp:Series>
            <asp:Series ChartArea="ChartArea1"  Legend="Legend1" Name="Upgrade" ToolTip="#VALX, #VAL" Color="0, 128, 0" >
            </asp:Series>--%>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisY IntervalAutoMode="VariableCount" Title="Members">
                                    <MajorGrid LineColor="White" LineDashStyle="Dash" />
                                    <MinorGrid Enabled="True" LineColor="White" LineDashStyle="Dash" />
                                </AxisY>
                                <AxisX IntervalAutoMode="VariableCount" Interval="1" Minimum="0" Title="Months">
                                    <MajorGrid LineColor="White" LineDashStyle="Dash" />
                                    <MinorGrid Enabled="True" LineColor="White" LineDashStyle="Dash" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Name="Legend1">
                            </asp:Legend>
                        </Legends>
                    </asp:Chart>


                </div>
                 
            </div>
        </div>
    </div>
        </div>
</asp:Content>
