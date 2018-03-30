<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportExpense.aspx.cs" Inherits="NDOnlineGym_2017.ReportExpense" %>
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
       <h3>Expense Report
           <div class="navigation">
              <ul>
                  <li>Report &nbsp; > &nbsp;</li>
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
                                        <td style="width: 45%;"><span class="lbl">Date (From) </span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt"></asp:TextBox>
                                             <ajaxToolkit:CalendarExtender ID="txtDate_Calender" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy"/>
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
                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy"/>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Expense Group</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddlExpenseGroup" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlExpenseGroup_SelectedIndexChanged" AutoPostBack="true" TabIndex="6">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
              </table>

            <center class="btn-section">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" ValidationGroup="a"   UseSubmitBehavior="false" OnClick="btnSearch_Click" on />
                 <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn"  OnClick="btnCancle_Click" />
                 <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" OnClick="btnExportToExcel_Click"   />
            </center>
        </div>
    </div>

<%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
    <div class="divForm" style="margin-top:5px">
         <div class="form-panel">
           <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver; ">
              <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
               <asp:GridView ID="GvExpenseReport1" runat="server" AutoGenerateColumns="false" Width="980"
                DataKeyNames="Exp_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                AllowPaging="True" PageSize="20" OnPageIndexChanging="GvExpenseReport1_PageIndexChanging">
                  <Columns>
                       <%-- <asp:BoundField HeaderText="Date" DataField="Exp_Date" HeaderStyle-HorizontalAlign="left" />--%>
                         <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <b>Exp Date</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("Exp_Date","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                    <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Exp Grp" DataField="ExpGroupName" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="left"/>
                    <asp:BoundField HeaderText="Tax" DataField="TaxableAmount" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Total Amount" DataField="TotalAmount" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Note 1" DataField="Note1" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Note 2" DataField="Note2" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Pay Mode" DataField="PayMode" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Pay Details" DataField="Pay_Details" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Pay Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />
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
