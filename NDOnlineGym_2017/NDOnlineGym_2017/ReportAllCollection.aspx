<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportAllCollection.aspx.cs" EnableEventValidation="false" Inherits="NDOnlineGym_2017.ReportAllCollection" %>

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
        .auto-style1 {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
      <div style="width:1021px">
    <div class="form-name-header">
        <h3>All Collection Report
           <div class="navigation">
               <ul>
                   <li>Report &nbsp; > &nbsp;</li>
                   <li>All Collection</li>
               </ul>
           </div>
            <h3></h3>
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
                                <td>
                                 <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearch_Click" TabIndex="5" />
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
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="Member ID">Member ID</asp:ListItem>
                                        <asp:ListItem Value="Member Name">Member Name</asp:ListItem>
                                        <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                        <asp:ListItem Value="Receipt No">Receipt No</asp:ListItem>
                                        <asp:ListItem Value="Payment Mode">Payment Mode</asp:ListItem>
                                       <%-- <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>--%>


                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>


                    <td class="cols">
                        <table>
                            <tr>
                                <td style="width: 45%;"><span class="lbl">Search</span></td>
                                <td style="width: 55%; text-align: left;">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"  TabIndex="4"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                   
                    <td class="cols">
                        
                    </td>
                </tr>
            </table>

            <div class="btn-section" style="margin-left: 80px">
                 <asp:Button ID="btnSearchByDateAndCategory" runat="server" Text="Category and Date" CssClass="form-btn" OnClick="btnSearchByDateAndCategory_Click" TabIndex="6" Width="251px" />
                 <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" OnClick="btnCancle_Click" TabIndex="7" />
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" OnClick="btnExportToExcel_Click" TabIndex="8" />

            </div>
        </div>
    </div>

    <%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
    <div class="divForm" style="margin-top: 5px">
        <div class="form-panel">
            <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver;">
                <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
                <asp:GridView ID="gvAllCollectionReport" runat="server" AutoGenerateColumns="false" 
                     Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                     OnPageIndexChanging="gvAllCollectionReport_PageIndexChanging"    AllowPaging="True" PageSize="20">
		             <Columns>
                        <asp:BoundField HeaderText="Member ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Refer Receipt ID" DataField="ReferRecNo" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Receipt ID" DataField="ReceiptNo" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Course Name" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                        <asp:TemplateField HeaderText="StartDate" >
                            <ItemTemplate>
                                <%# Eval("StartDate","{0:dd-MM-yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EndDate">
                            <ItemTemplate>
                                <%# Eval("EndDate","{0:dd-MM-yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="CourseFee" DataField="CourseFee" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Discount" DataField="Discount" HeaderStyle-HorizontalAlign="left" />
                       
                        <asp:BoundField HeaderText="Total Course Fee" DataField="TotCourseFee" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Paid" DataField="PaidFee" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="FinalCourseFee" DataField="FinalCourseFee" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Tax" DataField="Tax" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Tax Value" DataField="TaxValue" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Paid With Tax" DataField="PaidWithTax" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Payment Mode" DataField="PaymentMode" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Payment Details" DataField="Cardno" HeaderStyle-HorizontalAlign="left" />
                        <asp:TemplateField HeaderText="PayDate" >
                            <ItemTemplate>
                                <%# Eval("PayDate","{0:dd-MM-yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:BoundField HeaderText="PayDate" DataField="PayDate" HeaderStyle-HorizontalAlign="left" />--%>
                        <asp:BoundField HeaderText="Quantity" DataField="MemQty" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Total Fees" DataField="TotalFeeDue" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Paid Fees" DataField="TotalPaid" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Balance Fees" DataField="Balance" HeaderStyle-HorizontalAlign="left" />
                        <%--<asp:BoundField HeaderText="PaymentMode" DataField="PaymentMode" HeaderStyle-HorizontalAlign="left" />--%>
                    </Columns>

                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                    <RowStyle Height="20px" />
                    <AlternatingRowStyle Height="20px" BackColor="White" />
                </asp:GridView>
            </div>
           <%--  --%>
            <table style="margin-top:10px;width:100%;padding:0px;border-collapse:collapse;font-size:14px;">

           <tr>
            <td style="padding:3px 5px;" class="auto-style1">
            
                        <%--<strong><asp:Label ID="lblFees" runat="server" Text="Fees :" TabIndex="69"></asp:Label>
                        <asp:Label ID="lblFeepaid" runat="server" Text="0" TabIndex="69"></asp:Label>
                        </strong>--%>
                 <strong><asp:Label ID="lblTotal" runat="server" Text="Total Fees:" TabIndex="69"></asp:Label>
                        <asp:Label ID="lblTotalAmt" runat="server" Text="0" TabIndex="69"></asp:Label>
                        </strong>
                </td>
               <td style="padding:3px 5px;" class="auto-style1">
                        <strong><asp:Label ID="lblAmtPaidByCash" runat="server" Text="Total Cash Fees :" TabIndex="69"></asp:Label>
                       <asp:Label ID="lblToatalCash" runat="server" Text="0" TabIndex="69"></asp:Label>
                    </strong> 
                 </td>
               <td style="padding:3px 5px;" class="auto-style1">
                       <strong><asp:Label ID="lblAmtPaidByOther" runat="server" Text="Total Other Fees :" TabIndex="69"></asp:Label>
                       <asp:Label ID="lblToatalOther" runat="server" Text="0" TabIndex="69"></asp:Label>
                      </strong> 
                </td>
               </tr>
               <tr>
               <td style="padding:3px 5px;">
                        
                    <strong><asp:Label ID="lblFinalAmount" runat="server" Text="Total Paid Fees :" TabIndex="69"></asp:Label>
                        <asp:Label ID="lblTotalpaid" runat="server" Text="0" TabIndex="69"></asp:Label>
                     </strong>
                
               </td>
               <td style="padding:3px 5px;">
                      <strong><asp:Label ID="lblAmtPaidByCard" runat="server" Text="Total Card Fees :" TabIndex="69"></asp:Label>
                       <asp:Label ID="lblToatalCard" runat="server" Text="0" TabIndex="69"></asp:Label>
                      </strong>
                    
              </td>
               <td style="padding:3px 5px;"> 
                    <strong><asp:Label ID="lblGst" runat="server" Text="GST :" TabIndex="69"></asp:Label>
                        <asp:Label ID="lblGstamt" runat="server" Text="0" TabIndex="69"></asp:Label>
                        </strong>
               </td>
               </tr>
                <tr>
                  <td style="padding:3px 5px;">
                    <strong><asp:Label ID="lblTotalBalance" runat="server" Text="Balance :" TabIndex="69"></asp:Label>
                        <asp:Label ID="lblTotalBalance1" runat="server" Text="0" TabIndex="69"></asp:Label>
                        <%--<strong><asp:Label ID="lblDisc" runat="server" Text="Discount :" TabIndex="69"></asp:Label>
                        <asp:Label ID="lblTotalDisc" runat="server" Text="0" TabIndex="69"></asp:Label>--%>
                        </strong>
                    
                   </td>
                    <td style="padding:3px 5px;">
                     <strong><asp:Label ID="lblAmtPaidByCheck" runat="server" Text="Total Cheque Fees :" TabIndex="69"></asp:Label>
                       <asp:Label ID="lblToatalCheck" runat="server" Text="0" TabIndex="69"></asp:Label>
                     </strong>
                    </td>
                    <td style="padding:3px 5px;">
                   </td>
               </tr>

                <tr>
                    <td style="padding:3px 5px;">
                    <strong><asp:Label ID="lblDisc" runat="server" Text="Discount :" TabIndex="69"></asp:Label>
                          <asp:Label ID="lblTotalDisc" runat="server" Text="0" TabIndex="69"></asp:Label>
                    </strong>
                    
                   </td>
               </tr>
          </table>
        </div>
        </div>
   </div>
             </ContentTemplate>
        <Triggers>
   
             <asp:PostBackTrigger ControlID="btnExportToExcel" />

        </Triggers>
        </asp:UpdatePanel>
</asp:Content>
