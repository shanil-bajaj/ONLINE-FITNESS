﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportExecutivewiseEnquiryFollowup.aspx.cs" Inherits="NDOnlineGym_2017.ReportExecutivewiseEnquiryFollowup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
         .GridView { margin-top: 10px; float: left; border: solid 1px silver;  border-radius: 3px;  }
         .GridView a /** FOR THE PAGING ICONS  **/ { background-color: Transparent; padding: 5px 5px 5px 5px; color: black; text-decoration: none; font-weight: bold; }
         .GridView a:focus { color:orangered;}
         .GridView a:hover { color:orangered;}
         .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {  /*color: #fff;*/ padding: 5px 5px 5px 5px;   }
          .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <div class="sc">
     <div class="form-name-header">
       <h3>Executivewise Enquiry Followup Report
           <div class="navigation">
              <ul>
                  <li>Report &nbsp; > &nbsp;</li>
                  <li>Executivewise Enquiry Followup</li>
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
                                        <td style="width: 45%;"><span class="lbl">Date (From) </span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt"></asp:TextBox>
                                             <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender1" runat="server" BehaviorID="txtFromDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Date (To)</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt"></asp:TextBox>
                                              <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender1" runat="server" BehaviorID="txtToDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Executive</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlExecutive_SelectedIndexChanged" AutoPostBack="true" TabIndex="6">
                                                <asp:ListItem Value="--All--">--All--</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                    
            </table>

            <center class="btn-section">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" ValidationGroup="a"   UseSubmitBehavior="false" OnClick="btnSearch_Click" TabIndex="24" />
                <asp:Button ID="NFlwBtn" runat="server" Text="Search By Next Followup Date" CssClass="form-btn" ValidationGroup="a"   UseSubmitBehavior="false" TabIndex="24" />
                <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="25"  />
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" OnClick="btnExportToExcel_Click1" TabIndex="25"  />
             <%--</center>--%>
        </div>
    </div>

<%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
    <div class="divForm" style="margin-top:5px">
         <div class="form-panel">
           <div style="width:1000px; overflow-x:hidden; overflow-y: scroll; margin-top: 20px; border: 1px solid silver; ">
              <asp:GridView ID="GVEnquiryFollo" runat="server" AutoGenerateColumns="false"  Width="1000px" 
               Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                AllowPaging="True" PageSize="20">

                <Columns>
                    <asp:BoundField HeaderText="Name" DataField="EnquiryName"/>
                    <asp:BoundField HeaderText="Contact" DataField="Contact1"/>
                    <asp:TemplateField >
                                <HeaderTemplate>
                                    <b>Followup Date</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                     </asp:TemplateField>  
                    <asp:TemplateField >
                                <HeaderTemplate>
                                    <b>Next FollowupDate</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                     </asp:TemplateField>
                  <%--  <asp:BoundField HeaderText="Followup Date" DataField="FollowupDate" ItemStyle-Width="100px" />
                    <asp:BoundField HeaderText="Next FollowupDate" DataField="NextFollowupDate" ItemStyle-Width="100px" />--%>
                    <asp:BoundField HeaderText="Rating" DataField="Rating" />
                     <asp:BoundField HeaderText="Executive" DataField="Executive"/>
                </Columns>
                     
                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                <RowStyle Height="20px" />
                <AlternatingRowStyle Height="20px" BackColor="White" />
              </asp:GridView>
            </div>
         </div>
    </div>
          </div>
</asp:Content>
