<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="BalancePayment.aspx.cs" Inherits="NDOnlineGym_2017.BalancePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <style>
        table td
        {
            padding-bottom: 10px;
        }

        input:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ddl:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ErrorBox
        {
            position: relative;
            z-index: 1;
            font-weight: normal;
            border-radius: 3px;
            box-shadow: 10px 10px 10px rgba(0, 0, 0, 0.5);
            padding: 5px 7px;
            color: #a94442;
            background-color: #f2dede;
            border: 1px solid #ebccd1;
        }

        .errorborder
        {
            border: 1px solid red;
        }

        .form-panel
        {
            width: 100%;
            padding: 0px;
        }

        .remove
        {
            font-size: 18px;
            font-weight: bold;
            text-decoration: none;
        }

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

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
            {
                color: #fff;
                padding: 5px 5px 5px 5px;
            }
            
                .GridView a:focus {
                    color: orangered;
                }

                .GridView a:hover {
                    color: orangered;
                }
                  .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>

     <script>

         function RestrictSpaceSpecial(e) {
             try {
                 if (window.event) {
                     var charCode = window.event.keyCode;
                 }
                 else if (e) {
                     var charCode = e.which;
                 }
                 else { return true; }
                 if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                     return false;
                 }
                 return true;
             }
             catch (err) {
                 alert(err.Description);
             }
         }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="sc">
    <div class="form-name-header">
        <h3>Balance Payment
                 <div class="navigation">
                     <ul>
                         <li>Member Settings &nbsp; > &nbsp;</li>
                         <li>Balance  &nbsp; > &nbsp;</li>
                         <li>Balance Payment</li>
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
                    <h4>&#10148;Member Details </h4>
                </div>
                <div class="form-panel" id="formpanel1">
                    <table style="height: 80px;">
                        <tr>
                            <td>
                                <asp:Label ID="Lblmemeber_Auto" runat="server" Text="Receipt No:"></asp:Label>
                            </td>
                            <td>
                                <%-- <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>--%>
                                <asp:TextBox ID="txtReceiptid" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="1" AutoPostBack="true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtReceiptid_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Gender</th>
                            <th>Contact</th>
                            <th>Email</th>
                            <th>Executive</th>

                        </tr>
                        <tr id="row1" runat="server">
                            <td>
                                <asp:TextBox ID="TxtID" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="2" OnTextChanged="TxtID_TextChanged" AutoPostBack="true" onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFirst" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="3"></asp:TextBox>

                            </td>
                            <td>
                                <asp:TextBox ID="txtLast" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="4"></asp:TextBox>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGender1" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="5">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>
                                <asp:TextBox ID="txtContact" runat="server" AutoPostBack="true" Style="width: 150px; padding: 3px 5px;" TabIndex="6" OnTextChanged="txtContact_TextChanged" MaxLength="11"></asp:TextBox>


                            </td>
                            <td>
                                <asp:TextBox ID="txtmail" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="7"></asp:TextBox>


                                <asp:Label ID="lblMemberAutoID" runat="server" Visible="false"></asp:Label>

                                <asp:Label ID="lblBalAuto" runat="server" Visible="false"></asp:Label>

                            </td>

                            <td>
                                <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" TabIndex="8"/>
                                <asp:DropDownList ID="ddlExecutive" runat="server" TabIndex="8" Enabled="false" Style="padding: 3px">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>


                            </td>

                        </tr>
                    </table>


                </div>

                <%-- Receipt Details--%>
                <div class="form-header">
                    <h4>&#10148; Receipt Details  </h4>
                </div>
                <table>
                    <tr>
                        <th>Refer Receipt ID</th>
                        <th>Total fees</th>
                        <th>Paid Fees</th>
                        <th>Balance Fees</th>
                    </tr>

                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlReceipt" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="9" OnSelectedIndexChanged="ddlReceipt_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true">
                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="10" Enabled="false" Text="0"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox ID="txtPaid" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="11" Enabled="false" Text="0"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox ID="txtBalance" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="12" Enabled="false" Text="0"></asp:TextBox>

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
                                <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="ddl" TabIndex="13">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>

                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGSTType" runat="server" CssClass="ddl" TabIndex="14" Enabled="false" >
                                    <asp:ListItem Value="Including">Including</asp:ListItem>
                                    <asp:ListItem Value="Excluding">Excluding</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="addReceipt" Text="+" TabIndex="15" Style="font-size: 25px; font-weight: bold; color: rgb(12, 99, 16); text-decoration: none; margin-left: 20px"
                                    ToolTip="Add Payment" OnClick="addReceipt_Click"></asp:LinkButton>
                            </td>

                        </tr>
                    </table>
                           </div>
                    <div style="width: 1000px; height: auto;">

                        <asp:GridView ID="gvBalancePayment" runat="server" AutoGenerateColumns="false"
                            EmptyDataText="No record found." Width="1000px" GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                            CssClass="GridView" AllowPaging="True" TabIndex="16" OnRowDeleting="gvBalancePayment_RowDeleting" OnPageIndexChanging="gvBalancePayment_PageIndexChanging">
                            <Columns>

                                <asp:CommandField  HeaderText="Remove" ShowDeleteButton="True" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300" />

                                <asp:TemplateField HeaderText="Payment Mode">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPaymentMode" runat="server" Style="width: 80px" Text='<%#Eval("PaymentMode") %>' TabIndex="17" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNumber" runat="server" Style="width: 80px" Text='<%#Eval("Number") %>' TabIndex="18" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate" runat="server" DataFormatString="{0:dd-MM-yyyy}" AutoPostBack="true"
                                            Style="width: 80px" Text='<%#Eval("Date","{0:dd-MM-yyyy}") %>' TabIndex="19" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            BehaviorID="txtDate_CalendarExtender" TargetControlID="txtDate" Format="dd-MM-yyyy" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Expiry Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExpiryDate" runat="server" DataFormatString="{0:dd-MM-yyyy}"
                                            Style="width: 80px" Text='<%#Eval("CardExpiryDate","{0:dd-MM-yyyy}") %>' TabIndex="20" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server"
                                            BehaviorID="txtExpiryDate_CalendarExtender" TargetControlID="txtExpiryDate" Format="dd-MM-yyyy" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBankName" runat="server" Style="width: 80px" Text='<%#Eval("BankName") %>' TabIndex="21" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Branch Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBranchName" runat="server" Style="width: 80px" Text='<%#Eval("BranchName") %>' TabIndex="22" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid Amount">
                                    <ItemTemplate>
                                        <asp:UpdatePanel runat="server" ID="TaxPaid" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtPaidAmount" runat="server" Style="width: 80px" Text='<%#Eval("PaidAmount") %>' AutoPostBack="true" OnTextChanged="txtPaidAmount_TextChanged" TabIndex="23" ></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="GST Type">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ddlTax" runat="server" Style="width: 80px" Text='<%#Eval("ddlTax") %>' Enabled="false" TabIndex="24" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="GST">
                                    <ItemTemplate>
                                        <%--  <asp:TextBox ID="ddlTax" runat="server" Style="width: 40px" Text='<%#Eval("ddlTax") %>'>--%>

                                        <asp:UpdatePanel runat="server" ID="TaxID" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtTax" runat="server" Style="width: 40px" Text='<%#Eval("txtTax") %>'  Enabled="false"  AutoPostBack="true" OnTextChanged="txtTax_TextChanged" TabIndex="25" ></asp:TextBox><sapn>%</sapn>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <asp:TextBox ID="Txtvalue" runat="server" Style="width: 40px" Text='<%#Eval("Txtvalue") %>'  Enabled="false"  TabIndex="26" ></asp:TextBox><sapn>Rs</sapn>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" Style="width: 80px" Text='<%#Eval("txtTotalAmount") %>'  Enabled="false"  TabIndex="27" > </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                

                                <%-- <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" Style="width: 80px" Text='<%#Eval("txtTotalAmount") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>


                        <div style="float: right;">
                              <h3>Total Fees :<asp:Label ID="lblTotalFeeDue" runat="server" Text="0" TabIndex="28" ></asp:Label>
                            </h3>
                            <h3>Paid Fees :<asp:Label ID="lblPaidFee" runat="server" Text="0" TabIndex="29" ></asp:Label>
                            </h3>                          
                            <h3>Balance Fees :<asp:Label ID="lblBalance" runat="server" Text="0" TabIndex="30"></asp:Label>
                            </h3>
                        </div>

                        <table>
                            <tr>
                                <th>Next Payment Date</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtNextFollowupDate" runat="server" DataFormatString="{0:dd-MM-yyyy}" TabIndex="31"></asp:TextBox>
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
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Style="width: 700px;resize:none" Rows="4" TabIndex="32" ></asp:TextBox></td>
                            </tr>
                        </table>

                    </div>

                </div>


                <%--End Balance Payment--%> 
                <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save"   CssClass="form-btn" OnClick="btnSave_Click" TabIndex="33"  OnClientClick="this.disabled = true;"  UseSubmitBehavior="false"/>
                <asp:Button ID="btnCancle" runat="server" Text="Clear"  CssClass="form-btn" OnClick="btnCancle_Click" TabIndex="34" />
             </center>

                      </div>

                     <div id="divsearch" runat="server" visible="false">
                <div class="form-header">
                    <h4 style="float: left;">&#10148; Balance Payment Details
               
                    </h4>
                </div>
               <%-- <div class="form-panel">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 20%;">
                                <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="25" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="ReceiptNumber">Receipt Number</asp:ListItem>
                                    <asp:ListItem Value="MemberId">Member Id</asp:ListItem>
                                    <asp:ListItem Value="FirstName">First Name</asp:ListItem>
                                    <asp:ListItem Value="LastName">Last Name</asp:ListItem>
                                    <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%;">
                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" CssClass="txt" Enabled="True" TabIndex="26" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                            </td>
                            <td style="width: 45%;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" TabIndex="27" OnClick="btnSearch_Click" Text="Search" />
                            </td>
                        </tr>
                    </table>

                </div>--%>

                 <center>
                <table>
                    <tr>
                        <th>From Date</th>
                        <th>To Date</th>
                        <th>Category</th>
                        <th>Search by</th>
                        <th></th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="35"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="36"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                          <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="37" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="ReceiptNumber">Receipt Number</asp:ListItem>
                                    <asp:ListItem Value="MemberId">Member Id</asp:ListItem>
                                    <asp:ListItem Value="FirstName">First Name</asp:ListItem>
                                    <asp:ListItem Value="LastName">Last Name</asp:ListItem>
                                    <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                       <td >
                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" CssClass="txt" Enabled="True" TabIndex="38" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
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
                             <asp:Button ID="btnSearch" runat="server" CssClass="form-btn"  TabIndex="39" Text="Search" OnClick="btnSearch_Click1"  />
                      <asp:Button ID="btnDtWithCategory" runat="server" CssClass="form-btn"  TabIndex="40" Text="Date with Category" OnClick="btnDtWithCategory_Click"  />
                              <asp:Button ID="btnReresh" runat="server" CssClass="form-btn"  TabIndex="41" Text="Clear" OnClick="btnReresh_Click" />
                              
                             
                        </center>
                         </div>
                   <div id="divGridView" runat="server" >
                <div style="width: 1000px; height: auto; margin-top: 20px; overflow-x: auto">
                     <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records = " Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
                    <asp:GridView ID="gvBalanceDetails" runat="server" AutoGenerateColumns="false"
                        EmptyDataText="No record found." Width="1700px" GridLines="None" CellPadding="3" Font-Size="11px" PagerStyle-CssClass="pager"
                        CssClass="GridView" AllowPaging="True" TabIndex="42" OnRowDeleting="gvBalancePayment_RowDeleting" PageSize="20" OnPageIndexChanging="gvBalanceDetails_PageIndexChanging" OnSelectedIndexChanged="gvBalanceDetails_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField> <%--ItemStyle-Width="150px"--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnfollowup" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Member_AutoID")%>' OnCommand="btnpreview_Command1" TabIndex="43" 
                                        style="background-image:url('../NotificationIcons/arrow_top_right-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Followup"/>
                                </ItemTemplate>

                            </asp:TemplateField>
                           <asp:TemplateField> <%--ItemStyle-Width="150px"--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Member_AutoID")%>' OnCommand="btnEdit_Command" TabIndex="44"
                                        style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="25"
                                        CommandArgument='<%#Eval("Bal_ReceiptID")%>'  OnCommand="btnDelete_Command" 
                                         style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnpreview" runat="server" CausesValidation="false" OnClick="btnpreview_Click1" TabIndex="45"
                                        style="background-image:url('../NotificationIcons/images (5).png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Preview" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnResend" runat="server" CausesValidation="false"  CommandArgument='<%#Eval("ReceiptID")%>' OnCommand="btnResend_Command" TabIndex="46"
                                        style="background-image:url('../NotificationIcons/images (10).jpg');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Resend"  />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Refer Receipt No" DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Receipt No" DataField="Bal_ReceiptID" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Member ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Member Name" DataField="MemberName" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                            <asp:TemplateField HeaderText="Payment Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <%# Eval("payDate","{0:dd-MM-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Total Fees" DataField="TotalFeeDue" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Paid Fees" DataField="Paid" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="GST Type" DataField="TaxType" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="GST %" DataField="taxpec" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="GST Value" DataField="TaxValue" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="PaidWithTax" DataField="PaidWithTax" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="PaymentMode" DataField="PaymentMode" HeaderStyle-HorizontalAlign="left" />
                            <asp:TemplateField HeaderText="Next Payment Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <%# Eval("NextBalDate","{0:dd-MM-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Executive" DataField="Name" HeaderStyle-HorizontalAlign="left" />


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
