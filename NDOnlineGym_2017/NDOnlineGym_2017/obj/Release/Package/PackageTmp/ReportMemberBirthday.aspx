<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportMemberBirthday.aspx.cs" Inherits="NDOnlineGym_2017.ReportMemberBirthday" %>
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
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
      <div class="sc">
     <div class="form-name-header">
       <h3>Member Birthday Report
           <div class="navigation">
              <ul>
                  <li>Report &nbsp; > &nbsp;</li>
                  <li>Member Birthday</li>
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
                                           <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy"/>
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
                                           <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Select Month</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddl" TabIndex="3" AutoPostBack="true" 
                                                 OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="January">January</asp:ListItem>
                                                <asp:ListItem Value="February">February</asp:ListItem>
                                                <asp:ListItem Value="March">March</asp:ListItem>
                                                <asp:ListItem Value="April">April</asp:ListItem>
                                                <asp:ListItem Value="May">May</asp:ListItem>
                                                <asp:ListItem Value="June">June</asp:ListItem>
                                                <asp:ListItem Value="July">July</asp:ListItem>
                                                <asp:ListItem Value="August">August</asp:ListItem>
                                                <asp:ListItem Value="September">September</asp:ListItem>
                                                <asp:ListItem Value="October">October</asp:ListItem>
                                                <asp:ListItem Value="November">November</asp:ListItem>
                                                <asp:ListItem Value="December">December</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
              </table>

            <center class="btn-section">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" ValidationGroup="a" TabIndex="4" OnClick="btnSearch_Click" />
                 <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="5" OnClick="btnClear_Click"   />
                 <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="6" OnClick="btnExportToExcel_Click"  />
             </center>
        </div>
    </div>

<%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
    <div class="divForm" style="margin-top:5px">
         <div class="form-panel">
           <div style="width: 1000px; overflow-x: auto; overflow-y: auto; margin-top: 20px; border: 1px solid silver; ">
               <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
              <asp:GridView ID="gvReportMemberBirthday" runat="server" AutoGenerateColumns="false"  Width="990px" 
                 Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvReportMemberBirthday_PageIndexChanging">
                   <Columns>
                          
                            <asp:BoundField HeaderText="Member Id" DataField="Member_ID1"  />                                
                            <asp:BoundField HeaderText="M.Name" DataField="Name"  />
                            <asp:BoundField HeaderText="Contact" DataField="Contact1"  />
                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <b>DOB</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("DOB","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:BoundField HeaderText="Status" DataField="Status" />
                           
                        </Columns>
                     
                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                <RowStyle Height="20px" />
                <AlternatingRowStyle Height="20px" BackColor="White" />
              </asp:GridView>
            </div>
         </div>
    </div>
          </div>
             </ContentTemplate>
              </asp:UpdatePanel>
</asp:Content>
