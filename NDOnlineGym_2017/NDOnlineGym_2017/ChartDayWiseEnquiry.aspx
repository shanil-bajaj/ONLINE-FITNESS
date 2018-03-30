<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChartDayWiseEnquiry.aspx.cs" Inherits="NDOnlineGym_2017.ChartDayWiseEnquiry" %>
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
       <h3>Daywise Enquiry Chart
           <div class="navigation">
              <ul>  <li>
                Chart &nbsp; > &nbsp;</li>
                 

                  <li>Enquiry</li>
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
                                             <asp:DropDownList ID="ddlYear" runat="server" CssClass="ddl" OnSelectedIndexChanged ="ddlYear_SelectedIndexChanged" TabIndex="1">
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
                                             <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" TabIndex="2">
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
                                             <asp:DropDownList ID="ddlChart" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChart_SelectedIndexChanged" CssClass="ddl" TabIndex="3">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                 <asp:ListItem >Column</asp:ListItem>
                                                 <asp:ListItem >Pie</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>

                                <td class="cols">
                                <table>
                                    <tr>
                                       <%-- <td style="width: 45%;"><span class="lbl">Select Chart</span></td>--%>
                                        <td>  <asp:Button ID="btnSave" runat="server" Text="Show" CssClass="form-btn" OnClick="btnSave_Click" TabIndex="4"/></td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
            </table>
            <div id="divChart" style="width:100%;height:400px;border:1px solid silver;padding:10px" >
                <table style="width:100%;">
                    <tr>
                        <td style="width:35%"><strong>Total Enquiry : </strong><asp:Label ID="Label15" runat="server"></asp:Label></td>
                    </tr>
                </table>
                         <ul class="Chart">
            
              <%--  <li>--%>
                     <div  id="imgRS" runat="server" >
                <div id="divChartSection" style="width:100%;height:380px;">
                   <%----------------------------Chart Section----------------------------------------%>
                 
                       <asp:Chart ID="Chart2" runat="server" Width="596px">
                            <Series>
                          <asp:Series Name="Enquiry" Legend="Legend1" MarkerColor="255, 192, 192" ToolTip="#VALX, #VAL"  IsXValueIndexed="True"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY IntervalAutoMode="VariableCount" Title="Enquiries">
                                        <MajorGrid LineColor="White" LineDashStyle="Dash"  />
                                    </AxisY>
                                    <AxisX IntervalAutoMode="VariableCount" Title="Days">
                                        <MajorGrid Interval="Auto" LineColor="White" LineDashStyle="Dash" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1">
                                </asp:Legend>
                            </Legends>
                        </asp:Chart>
                 <li>
                   <asp:Label ID="Label16" runat="server"></asp:Label>
                   
                </li>


                </div>
                         </div>
                 <%--</li>
                    <li style="float:right;">
                   <span>Total Enquiries :</span>
                    <asp:Label ID="Label15" runat="server"></asp:Label>
               </li>--%>
               
               
            </ul>
            </div>
        </div>
    </div>
        </div>
</asp:Content>
