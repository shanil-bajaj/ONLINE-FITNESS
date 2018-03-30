<%@ Page Title="" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSNewInvoice.aspx.cs" Inherits="NDOnlineGym_2017.POSNewInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <%-- <link rel="icon" type="image/png" href="Logo/NDLogo.png" />--%>
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
       <style>
        input:focus { border: 1px solid rgb(242, 137, 9); }
        .GridView { margin-top: 10px;float: left; border: solid 1px silver; border-radius: 3px;  }
        .GridView a /** FOR THE PAGING ICONS  **/ { background-color: Transparent; padding: 5px 5px 5px 5px; color: black;text-decoration: none;font-weight: bold;  }
        .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ { color: #fff;padding: 5px 5px 5px 5px;  }
        .remove { font-size: 18px; font-weight: bold; }
        .GridView1 { margin-top: 10px;float: left;border: solid 1px silver; border-radius: 3px; }
        .GridView1 a /** FOR THE PAGING ICONS  **/  { background-color: Transparent;padding: 5px 5px 5px 5px; color: black;text-decoration: none;font-weight: bold;  }
        .GridView1 a:focus  {  color: orangered; }
        .GridView1 a:hover { color: orangered;  }
        .GridView1 span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {  /*color: #fff;*/ padding: 5px 5px 5px 5px; }
        .hideGridColumn { display: none; }
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="sc" >
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <div id="divFormDetails" runat="server">
            <div class="divForm">
                <div class="form-name-header">
                <h3>Invoice
                 <div class="navigation">
                     <ul>
                         <li>Sales &nbsp; > &nbsp;</li>
                         <li>Invoice &nbsp; > &nbsp;</li>
                         <li>Invoice</li>
                     </ul>
                 </div>
                </h3>
            </div>
                <%--Member/customer Details--%>
                <div class="form-header">
                    <h4>&#10148; Member/Customer Details  </h4>
                </div>
                <div class="form-panel">
                     <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No:"></asp:Label>
                            </td>
                            <td>
                                <%-- <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>--%>
                                <asp:TextBox ID="txtInvoiceNo" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="1" MaxLength="11" ></asp:TextBox>
                            </td>
                        </tr>
                        </table>
                       <table id="tableInfo" runat="server" style="margin-top: 10px;" >
                        <tr>
                            <th>ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Gender</th>
                            <th>Contact</th>
                            <th>Email</th>
                        </tr>

                        <tr id="row1" runat="server" >
                            <td>
                                <asp:TextBox ID="txtId1" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="1"></asp:TextBox>

                            </td>
                            <td>
                                <asp:TextBox ID="txtFirstName1" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="2"></asp:TextBox>

                            </td>
                            <td>
                                <asp:TextBox ID="txtLastName1" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="3"></asp:TextBox>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGender1" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="4">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>
                                <asp:TextBox ID="txtContact1" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="5"></asp:TextBox>


                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail1" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="6"></asp:TextBox>

                            </td>
                        </tr>
                    </table>
                </div>
                <%--End Member/customer Details--%>

                 <%-- Product Details--%>

                <div class="form-header">
                    <h4>&#10148; Product Details  </h4>
                </div>
                <div class="form-panel">
                       <div style="width: 1000px; height: 250px; overflow-y: scroll">

                        <asp:GridView ID="gvCourse" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                            EmptyDataText="No record found." Width="970px" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView"
                            AllowPaging="True" TabIndex="27" >
                            <Columns>

                               
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>


                    </div>

                </div>

             <%-- Product Details--%>

             <%--Add Product--%>
               

                <div class="form-header">
                    <h4>&#10148; Add Product  </h4>
                </div>

                <div class="form-panel">
                    <div style="width: 1000px; height: auto; overflow-x: scroll;">
                          <asp:GridView ID="GvPakageAssign" runat="server" AutoGenerateColumns="False"  EmptyDataText="No record found." 
                              Width="700px" GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                            CssClass="GridView" AllowPaging="True" TabIndex="71">
                            <Columns>

                               
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PagerStyle CssClass="pager" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>

                       
                    </div>

                </div>
             <%--End Add Product--%>

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
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddl" TabIndex="82" >
                                        <%--  <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                    <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                    <asp:ListItem Value="Creadit Card">Creadit Card</asp:ListItem>
                                    <asp:ListItem Value="Debit Card">Debit Card</asp:ListItem>
                                    <asp:ListItem Value="NEFT">NEFT</asp:ListItem>
                                    <asp:ListItem Value="RTGS">RTGS</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl" TabIndex="83" Enabled="false">
                                        <asp:ListItem Value="Including">Including</asp:ListItem>
                                        <asp:ListItem Value="Excluding">Excluding</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" ID="addReceipt" Text="+" TabIndex="84" Style="font-size: 25px; font-weight: bold; color: rgb(12, 99, 16); text-decoration: none; margin-left: 20px"
                                        ToolTip="Add Payment"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 1000px; height: auto; overflow-x: scroll;">

                        <asp:GridView ID="gvBalancePayment" runat="server" AutoGenerateColumns="false"
                            EmptyDataText="No record found." Width="1000px" GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                            CssClass="GridView" AllowPaging="True" TabIndex="85" >
                            <Columns>
                               
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>


                        <div style="float: right;">
                            <h3>Total Fee :<asp:Label ID="lblTotalFeeDue" runat="server" Text="0" TabIndex="97"></asp:Label>
                            </h3>
                            <h3>Paid Fee :<asp:Label ID="lblPaidFee" runat="server" Text="0" TabIndex="98"></asp:Label>
                            </h3>
                            <h3>Balance :<asp:Label ID="lblBalance" runat="server" Text="0" TabIndex="99"></asp:Label>
                            </h3>
                        </div>

                        <table>
                            <tr>
                                <th>Next Payment Date</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtNextFollowupDate" runat="server" TabIndex="100"></asp:TextBox>
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
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Style="width: 700px; resize: none;" Rows="4" TabIndex="101"></asp:TextBox></td>
                            </tr>
                        </table>

                    </div>

                </div>
                <%--End Balance Payment--%>
                <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="form-btn" TabIndex="102"/>
              <%--  <asp:Button ID="btnView" runat="server" Text="View" CssClass="form-btn" />--%>
                <asp:Button ID="btnCancle" runat="server" Text="Clear" TabIndex="103" OnClientClick="window.location.reload();" CssClass="form-btn" />
             </center>

            </div>
     </div>
     </div>
</asp:Content>
  