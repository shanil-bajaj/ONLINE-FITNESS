﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChartAllCourseWise.aspx.cs" Inherits="NDOnlineGym_2017.ChartAllCourseWise" %>
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
       <h3>All Coursewise Chart
           <div class="navigation">
              <ul>
                  <li>Chart &nbsp; > &nbsp;</li>
                  <li>All Coursewise</li>
              </ul>
           </div>
      </h3>
    </div>
    <div class="divForm">
        <div class="form-panel">
          
            <div id="divChart" style="width:100%;height:400px;border:1px solid silver;padding:10px" >
                <table style="width:100%;">
                    <tr>
                         <td style="width:35%"><strong>Total Active Member : </strong><asp:Label ID="lblActiveMember" runat="server" Text="2987869"></asp:Label></td>
                    </tr>
                </table>
                <div id="divChartSection" style="width:100%;height:380px;">


                   <%----------------------------Chart Section----------------------------------------%>

                     <asp:Chart ID="Chart1" runat="server" Width="718px" ClientIDMode="Static" Height="250px" OnClick="Chart1_Click" OnLoad="Chart1_Load"   >
                        <Series>
                            <asp:Series Name="Allcourse" ToolTip="#VAL" Legend="Legend1"  PostBackValue="#VALX" ></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" >
                                <AxisY IntervalAutoMode="VariableCount" Title="Members">
                                    <MajorGrid LineColor="White" LineDashStyle="Dash" />
                                    <MinorGrid Enabled="True" LineColor="White" LineDashStyle="Dash" />
                                </AxisY>
                                <AxisX IntervalAutoMode="VariableCount" Interval="1" Minimum="0" Title="Course">
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
