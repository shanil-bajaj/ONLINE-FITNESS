<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportPayment.aspx.cs" Inherits="NDOnlineGym_2017.ReportPayment" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <div style="width:1021px">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
    <div class="form-name-header">
       <h3>Daily Collection Reports
           <div class="navigation">
              <ul>
                  <li>Report &nbsp; > &nbsp;</li>
                  <li>Payment</li>
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
                                             <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
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
                                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                              <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Type</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddltype" runat="server" CssClass="ddl" TabIndex="3">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                  <asp:ListItem>New</asp:ListItem>
                                                  <asp:ListItem>Balance</asp:ListItem>
                                                  <asp:ListItem>Upgrade</asp:ListItem>
                                                  <asp:ListItem>Transfer</asp:ListItem>
                                               </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>
                             <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Search By</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddlSearch" runat="server" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" AutoPostBack="true" CssClass="ddl" TabIndex="4">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                 <asp:ListItem Value="Member Id">Member Id</asp:ListItem>
                                                 <asp:ListItem Value="Member Name">Member Name</asp:ListItem>
                                                 <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                                 <asp:ListItem Value="Payment Type">Payment Type</asp:ListItem>
                                                 <asp:ListItem Value="Executive">Executive</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <%--<td style="width: 45%;"><span class="lbl">Payment Mode</span></td>--%>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:TextBox ID="txtSearch" runat="server" CssClass="txt"  AutoPostBack="true"  TabIndex="4">
                                             </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
            </table>

            <center class="btn-section">

                 <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearch_Click" TabIndex="5" />
                 <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="6" OnClick="btnCancle_Click"/>
                 <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="7" OnClick="btnExportToExcel_Click" />

            </center>
        </div>
    </div>
             </ContentTemplate>
        </asp:UpdatePanel>
<%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>
    <div class="divForm" style="margin-top:5px">
         <div class="form-panel">
           <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver; ">
                 <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
              <asp:GridView ID="GvPaymentReports" runat="server" AutoGenerateColumns="false"  Width="1100px" 
                Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" OnPageIndexChanging="GvPaymentReports_PageIndexChanging"
                AllowPaging="True" PageSize="20">
                  <Columns>
                            <%--  <asp:BoundField HeaderText="Member Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />--%>
                           <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                <HeaderTemplate>
                                    <b>Pay Date</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("payDate","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:BoundField HeaderText="Rec.No." DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />  
                            <asp:BoundField HeaderText="Refer Recno." DataField="ReferRec" HeaderStyle-HorizontalAlign="left" />  
                            <asp:BoundField HeaderText="M.Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />     
                            <asp:BoundField HeaderText="M.Name" DataField="MemberName" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="M.Type" DataField="Status" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Paid Amt" DataField="Paid" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Tax Amt" DataField="tax" HeaderStyle-HorizontalAlign="left" />
                           <%--   <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />--%>
                            <asp:BoundField HeaderText="Paid Amt With Tax" DataField="PaidFee" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Pay Mode" DataField="PaymentMode" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Pay Details" DataField="paymentDetails" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />
                           <%-- <asp:BoundField HeaderText="Total Fees" DataField="TotalFeeDue" HeaderStyle-HorizontalAlign="left" />--%>
                           <%--<asp:BoundField HeaderText="Bal Fees" DataField="Balance" HeaderStyle-HorizontalAlign="left" />--%>
                    </Columns>
                     
                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                <RowStyle Height="20px" />
                <AlternatingRowStyle Height="20px" BackColor="White" />
              </asp:GridView>
               
            </div>

               <div style="text-align:right">
                    <h3><asp:Label ID="lblFinalAmount" runat="server" Text="Total Paid Fees :" TabIndex="69"></asp:Label>
                        <asp:Label ID="lblTotalpaid" runat="server" Text="0" TabIndex="69"></asp:Label>
                    </h3>
                   <h3><asp:Label ID="lblAmtPaidByCash" runat="server" Text="Total Cash Fees :" TabIndex="69"></asp:Label>
                       <asp:Label ID="lblToatalCash" runat="server" Text="0" TabIndex="69"></asp:Label>
                   </h3>
                   <h3><asp:Label ID="lblAmtPaidByCard" runat="server" Text="Total Card Fees :" TabIndex="69"></asp:Label>
                       <asp:Label ID="lblToatalCard" runat="server" Text="0" TabIndex="69"></asp:Label>
                   </h3>
                   <h3><asp:Label ID="lblAmtPaidByCheck" runat="server" Text="Total Cheque Fees :" TabIndex="69"></asp:Label>
                       <asp:Label ID="lblToatalCheck" runat="server" Text="0" TabIndex="69"></asp:Label>
                   </h3>
                   <h3><asp:Label ID="lblAmtPaidByOther" runat="server" Text="Total Other Fees :" TabIndex="69"></asp:Label>
                       <asp:Label ID="lblToatalOther" runat="server" Text="0" TabIndex="69"></asp:Label>
                   </h3>
                   <%--<h3><asp:Label ID="lblAmtBalance" runat="server" Text="Total Other Fees" TabIndex="69"></asp:Label>
                       <asp:Label ID="lblTotalBalance" runat="server" Text="0" TabIndex="69"></asp:Label>
                   </h3>--%>
               </div>

         </div>
    </div>
  
      </ContentTemplate>
      <Triggers>
          <%--  <asp:PostBackTrigger ControlID="btnSave" />--%>
        <asp:PostBackTrigger ControlID="btnExportToExcel"/>
    </Triggers>
    </asp:UpdatePanel>
          </div>
</asp:Content>