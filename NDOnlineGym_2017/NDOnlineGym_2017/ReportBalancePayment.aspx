<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportBalancePayment.aspx.cs" Inherits="NDOnlineGym_2017.ReportBalancePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>

    <style>
        .GridView
        {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

       .GridView a /** FOR THE PAGING ICONS  **/
        {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
         }

         .GridView a:focus
          {
             color: orangered;
          }

         .GridView a:hover
          {
            color: orangered;
          }

         .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
          { /*color: #fff;*/
                padding: 5px 5px 5px 5px;
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
      <div style="width:1021px">
    <div class="form-name-header">
        <h3>Balance Report
           <div class="navigation">
               <ul>
                   <li>Report &nbsp; > &nbsp;</li>
                   <li>Balance</li>
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
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />

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
                                <td style="width: 45%;"><span class="lbl">Member Name</span></td>
                                <td style="width: 55%; text-align: left;">
                                    <asp:DropDownList ID="ddlMemberName" runat="server" CssClass="ddl" TabIndex="3">
                                     <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
           <%--     <tr>
                    <td class="cols">
                        <table>
                            <tr>
                                <td style="width: 45%;"><span class="lbl">Executive</span></td>
                                <td style="width: 55%; text-align: left;">
                                    <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="4">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td class="cols">
                        <table>
                            <tr>
                                <td style="width: 45%;"><span class="lbl">Payment Mode</span></td>
                                <td style="width: 55%; text-align: left;">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddl" TabIndex="4">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>--%>

                <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Search By</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="true" CssClass="ddl" TabIndex="4">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                 <asp:ListItem Value="Member Id">Member Id</asp:ListItem>
                                                  <%--<asp:ListItem Value="RecNo">RecNo</asp:ListItem>--%>
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
            </table>

            <center class="btn-section">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn"  TabIndex="5" OnClick="btnSearch_Click" />
                 <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="6" OnClick="btnCancle_Click" />
                 <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="7" OnClick="btnExportToExcel_Click"  />

             </center>
        </div>
    </div>
    <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    <%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
<%--    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>--%>
    <div class="divForm" style="margin-top: 5px">
        <div class="form-panel">
            <div style="width: 1000px; overflow-x: hidden; overflow-y: scroll; margin-top: 20px; border: 1px solid silver;">
                  <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
                <asp:GridView ID="GvBalanceReports" runat="server" AutoGenerateColumns="false" Width="1000px"
                    Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"  AllowPaging="True" PageSize="20" OnPageIndexChanging="GvBalanceReports_PageIndexChanging">
                    <Columns>
                        <%--  <asp:BoundField HeaderText="Member Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />--%>
                        <asp:BoundField HeaderText="Ref RecNo." DataField="ReferRecNo" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Receipt No." DataField="ReceiptNo" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="M.Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />             
                        <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Total Fees" DataField="TotalFeeDue" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Paid Fees" DataField="TotalPaid" HeaderStyle-HorizontalAlign="left" />
                        <%--   <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />--%>
                        <asp:BoundField HeaderText="Balance Fees" DataField="Balance" HeaderStyle-HorizontalAlign="left" />
                         <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                            <HeaderTemplate>
                                <b>Payment Date</b>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("payDate","{0:dd-MM-yyyy}")%>
                            </ItemTemplate>
                         </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                            <HeaderTemplate>
                                <b>Next Payment Date</b>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("NextBalDate","{0:dd-MM-yyyy}")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />
                    </Columns>

                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                    <RowStyle Height="20px" />
                    <AlternatingRowStyle Height="20px" BackColor="White" />
                </asp:GridView>
            </div>

             <div style="text-align: right;">
        
                <table style="padding:0px;margin:0px;border-collapse:collapse;width:100%;margin-top:5px">
                    <tr>
                        <td style="text-align:right;padding:5px;font-size:14px;"> 
                            <strong> <asp:Label ID="lblTotal" runat="server"  TabIndex="69"></asp:Label></strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;padding:5px;font-size:14px;"> 
                            <strong> <asp:Label ID="lblTotalpaid" runat="server"  TabIndex="69"></asp:Label> </strong>
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align:right;padding:5px;font-size:14px;"> 
                            <strong> Balance Fees : <asp:Label ID="lblbal" runat="server" Text="0" TabIndex="69"></asp:Label></strong>
                        </td>
                   </tr>
                </table>
            </div>
        </div>
    </div>
   
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExportToExcel"/>
    </Triggers>
      </asp:UpdatePanel>
         
</asp:Content>
