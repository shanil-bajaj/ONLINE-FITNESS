<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChartDayWiseExpense.aspx.cs" Inherits="NDOnlineGym_2017.ChartDayWiseExpense" %>
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
       <h3>Daywise Expense Chart
           <div class="navigation">
              <ul>
                  <li>Chart &nbsp; > &nbsp;</li>
                  <li>Day Wise &nbsp; > &nbsp;</li>
                  <li>Expense</li>
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
                                             <asp:DropDownList ID="ddlYear" runat="server" CssClass="ddl" TabIndex="1">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Select Month</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddl" TabIndex="2" >
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
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
                                             <asp:DropDownList ID="ddlChart" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlChart_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                 <asp:ListItem Value="Column">Column</asp:ListItem>
                                                 <asp:ListItem Value="Pie">Pie</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>

                                <td class="cols">
                                <table>
                                    <tr>
                                       <%-- <td style="width: 45%;"><span class="lbl">Select Chart</span></td>--%>
                                        <td>  <asp:Button ID="btnSave" runat="server" Text="Show" CssClass="form-btn" OnClick="btnSave_Click" TabIndex="4" /></td>
                                    </tr>
                                </table>
                                
                                <%-- <table>
                                    <tr>
                                        
                                        <td style="width: 55%; text-align: left;">
                                              <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="form-btn" 
                                                  ValidationGroup="a" OnClick="btnShow_Click"/>
                                        </td>
                                    </tr>
                                </table>--%>
                            </td>

                        </tr>
            </table>
            <table style="width:100%;">
                  <tr>
                       
                        <td style="width:35%"><strong>Total Expense : </strong><asp:Label ID="lblTotalExpense" runat="server" ></asp:Label></td>
                  </tr>
            </table>

            <div id="divChart" style="width:100%;height:400px;border:1px solid silver;padding:10px" >
                
                <div id="divChartSection" style="width:100%;height:380px;">


                   <%----------------------------Chart Section----------------------------------------%>
                     <asp:Chart ID="Chart1" runat="server" Width="718px" ClientIDMode="Static" Height="250px">
                        <Series>
                            <asp:Series Name="Expense" ToolTip="#VALX, Rs. #VAL{N}" Legend="Legend1"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisY IntervalAutoMode="VariableCount" Title="Expense">
                                    <MajorGrid LineColor="White" LineDashStyle="Dash" />
                                    <MinorGrid Enabled="True" LineColor="White" LineDashStyle="Dash" />
                                </AxisY>
                                <AxisX IntervalAutoMode="VariableCount" Interval="1" Minimum="0" Title="Days">
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
                    <table>
                        <tr>
                            <td> <asp:Label ID="Label16" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                 
            </div>
        </div>
    </div>
        </div>
</asp:Content>
