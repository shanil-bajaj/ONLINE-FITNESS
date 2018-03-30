<%@ Page Title="" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSVendorBalancePayment.aspx.cs" Inherits="NDOnlineGym_2017.POSVendorBalancePayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <style>
        table td {  padding-bottom: 10px; }
        input:focus { border: 1px solid rgb(242, 137, 9); }
        .ddl:focus  { border: 1px solid rgb(242, 137, 9); }
        .ErrorBox { position: relative;z-index: 1;font-weight: normal;border-radius: 3px;box-shadow: 10px 10px 10px rgba(0, 0, 0, 0.5); padding: 5px 7px;
            color: #a94442; background-color: #f2dede; border: 1px solid #ebccd1; }
        .errorborder {  border: 1px solid red;  }
         .form-panel {  width: 100%;padding: 0px; }
         .remove { font-size: 18px; font-weight: bold;text-decoration: none;  }
         .GridView { margin-top: 10px; float: left;border: solid 1px silver; border-radius: 3px; }
         .GridView a /** FOR THE PAGING ICONS  **/ { background-color: Transparent;padding: 5px 5px 5px 5px;color: black;text-decoration: none;font-weight: bold; }
         .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/  { color: #fff; padding: 5px 5px 5px 5px;  }
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="sc" >
    <div class="form-name-header">
        <h3>Vendor Balance Payment
                 <div class="navigation">
                     <ul>
                         <li>Balance Payment &nbsp; > &nbsp;</li>
                         <li>Vendor Payment</li>
                         <asp:Label ID="lblTempPaid" runat="server" Visible="false"></asp:Label>
                     </ul>
                 </div>

        </h3>
    </div>

    <div class="divForm">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                 <div id="divFormDetails" runat="server">

                <div class="form-header" id="formheader1">
                    <h4>&#10148;Vendor Details </h4>
                </div>
                <div class="form-panel" id="formpanel1">
                    <table style="height: 80px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No:"></asp:Label>
                            </td>
                            <td>
                                <%-- <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>--%>
                                <asp:TextBox ID="txtInvoiceNo" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="1" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>ID</th>
                            <th>Vendor Name</th>
                            <th>Contact</th>
                            <th>GST No</th>
                            <th>Executive</th>

                        </tr>
                        <tr id="row1" runat="server">
                            <td>
                                <asp:TextBox ID="TxtID" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="2" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="3"></asp:TextBox>

                            </td>
                           
                            <td>
                                <asp:TextBox ID="txtContact" runat="server" AutoPostBack="true" Style="width: 200px; padding: 3px 5px;" TabIndex="6"  MaxLength="11"></asp:TextBox>


                            </td>
                            <td>
                                <asp:TextBox ID="txtGSTNo" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="7"></asp:TextBox>
                            </td>

                            <td>
                                <asp:CheckBox ID="chkExecutive" runat="server" Checked="true"  TabIndex="8"/>
                                <asp:DropDownList ID="ddlExecutive" runat="server" TabIndex="9" Enabled="false" Style="padding: 3px;width: 150px; ">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>


                            </td>

                        </tr>
                    </table>


                </div>

                <%-- Receipt Details--%>
                <div class="form-header">
                    <h4>&#10148; Invoice Details  </h4>
                </div>
                <table>
                    <tr>
                        <th>Refer Invoice ID</th>
                        <th>Total fees</th>
                        <th>Paid Fees</th>
                        <th>Balance Fees</th>
                    </tr>

                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlReceipt" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="10" >
                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="11" Enabled="false" Text="0"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox ID="txtPaid" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="12" Enabled="false" Text="0"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox ID="txtBalance" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="13" Enabled="false" Text="0"></asp:TextBox>

                        </td>

                    </tr>

                </table>


                <%--  End--%>


                <%-- Balance Payment--%>
                <div class="form-header">
                    <h4>&#10148; Balance Payment  </h4>
                </div>

                <div class="form-panel">
                       <div id="Div_paymode" runat="server" visible="true">
                    <table>
                        <tr>
                            <th>Payment Mode</th>
                            <th>Tax Type</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="ddl" TabIndex="14">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>

                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGSTType" runat="server" CssClass="ddl" TabIndex="15" Enabled="false" >
                                    <asp:ListItem Value="Including">Including</asp:ListItem>
                                    <asp:ListItem Value="Excluding">Excluding</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="addReceipt" Text="+" TabIndex="16" Style="font-size: 25px; font-weight: bold; color: rgb(12, 99, 16); text-decoration: none; margin-left: 20px"
                                    ToolTip="Add Payment" ></asp:LinkButton>
                            </td>

                        </tr>
                    </table>
                           </div>
                    <div style="width: 1000px; height: auto;">

                        <asp:GridView ID="gvBalancePayment" runat="server" AutoGenerateColumns="false"
                            EmptyDataText="No record found." Width="1000px" GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                            CssClass="GridView" AllowPaging="True" TabIndex="17" >
                            <Columns>

                               
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>


                        <div style="float: right;">
                              <h3>Total Fees :<asp:Label ID="lblTotalFeeDue" runat="server" Text="0" TabIndex="18" ></asp:Label>
                            </h3>
                            <h3>Paid Fees :<asp:Label ID="lblPaidFee" runat="server" Text="0" TabIndex="19" ></asp:Label>
                            </h3>                          
                            <h3>Balance Fees :<asp:Label ID="lblBalance" runat="server" Text="0" TabIndex="20"></asp:Label>
                            </h3>
                        </div>

                        <table>
                            <tr>
                                <th>Next Payment Date</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtNextFollowupDate" runat="server" DataFormatString="{0:dd-MM-yyyy}" TabIndex="21"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtNextFollowupDate_CalendarExtender" runat="server"
                                        BehaviorID="txtNextFollowupDate_CalendarExtender" TargetControlID="txtNextFollowupDate" Format="dd-MM-yyyy" />
                                </td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <th>Comment</th>

                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Style="width: 700px;resize:none" Rows="4" TabIndex="22" ></asp:TextBox></td>
                            </tr>
                        </table>

                    </div>

                </div>


                <%--End Balance Payment--%> 
                <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save"   CssClass="form-btn"  TabIndex="23"  OnClientClick="this.disabled = true;"  UseSubmitBehavior="false"/>
                <asp:Button ID="btnCancle" runat="server" Text="Clear"  CssClass="form-btn"  TabIndex="24" />
             </center>

                      </div>

                     <div id="divsearch" runat="server" visible="false">
                <div class="form-header">
                    <h4 style="float: left;">&#10148; Balance Payment Details
               
                    </h4>
                </div>
               
                 <center>
                <table>
                    <tr>
                        <th>Form Date</th>
                        <th>To Date</th>
                        <th>Category</th>
                        <th>Search by</th>
                        <th></th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="28"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="29"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                          <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="30" >
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="ReceiptNumber">Receipt Number</asp:ListItem>
                                    <asp:ListItem Value="MemberId">Member Id</asp:ListItem>
                                    <asp:ListItem Value="FirstName">First Name</asp:ListItem>
                                    <asp:ListItem Value="LastName">Last Name</asp:ListItem>
                                    <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                       <td >
                                <asp:TextBox ID="txtSearch" runat="server"  CssClass="txt" Enabled="True" TabIndex="31" ></asp:TextBox>
                            </td>
                
                    </tr>

                    <caption>
                        <h4></h4>
                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                        </tr>
                    </caption>

                </table>
              </center>

                  <center style="margin-top: 20px;">
                             <asp:Button ID="btnSearch" runat="server" CssClass="form-btn"  TabIndex="32" Text="Search"  />
                             <asp:Button ID="btnDtWithCategory" runat="server" CssClass="form-btn"  TabIndex="33" Text="Search By Date with Category"  />
                             <asp:Button ID="btnReresh" runat="server" CssClass="form-btn"  TabIndex="34" Text="Clear" />
                  </center>
                         </div>
                   <div id="divGridView" runat="server" >
                <div style="width: 1000px; height: auto; margin-top: 20px; overflow-x: auto">

                    <asp:GridView ID="gvBalanceDetails" runat="server" AutoGenerateColumns="false"
                        EmptyDataText="No record found." Width="1500px" GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                        CssClass="GridView" AllowPaging="True" TabIndex="28"  PageSize="20" >
                        <Columns>
                           
                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <RowStyle Height="20px" />
                        <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>
                </div>
                         </div>

            </ContentTemplate>
             <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        </asp:UpdatePanel>
    </div>
        </div>
</asp:Content>
