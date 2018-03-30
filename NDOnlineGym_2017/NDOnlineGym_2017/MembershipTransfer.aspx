<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MembershipTransfer.aspx.cs" Inherits="NDOnlineGym_2017.MembershipTransfer" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />

    <style>
        .ddlTimeExe {
            width: 91%;
            padding: 4px 10px;
            border-radius: 3px;
            border: none;
            border: solid 1px silver;
            outline: none;
            height: 25px;
        }

            .ddlTimeExe:focus {
                box-shadow: 0px 0px 10px rgba(81, 203, 238, 1);
            }

        .GridView {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

            .GridView a /** FOR THE PAGING ICONS  **/ {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
            }

                .GridView a:focus {
                    color: orangered;
                }

                .GridView a:hover {
                    color: orangered;
                }

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .btn-remove {
            background-color: rgb(248, 45, 70);
            color: white;
            border: 1px solid rgb(248, 45, 70);
            margin-top: 3px;
        }

            .btn-remove:focus {
                border: 1px solid black;
                cursor: pointer;
            }

        .btn-file:focus {
            border: 1px solid silver;
            cursor: pointer;
        }

        input[type="checkbox"]:focus {
            border-color: #ffffcc;
        }
    </style>
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <script src="JS/Common-Script.js"></script>
    
    <script type="text/javascript">        
        //Disable enable executive dropdown on check box
        function ChExecutive() {
            var _chkExecutive = document.getElementById('<%= chkExecutive.ClientID %>');
            document.getElementById('<%= ddlExecutive.ClientID %>').disabled = _chkExecutive.checked;
        }
    </script>
    <script>
        function showConfirmation(myurl) {
            if (confirm("First Unblock Member") == true) {
                window.parent.location.href = myurl;
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-name-header" id="divMembershipTransfer" runat="server">
        <h3>Membership Transfer
            <div class="navigation">
                <ul>
                    <li>Member &nbsp; > &nbsp;</li>
                    <li>Membership Transfer</li>
                </ul>
            </div>
        </h3>
    </div>
    <div class="form-name-header" id="divMembershipTransferDetails" runat="server" visible="false">
        <h3>Membership Transfer Details
            <div class="navigation">
                <ul>                         
                    <li>Member &nbsp; > &nbsp;</li>
                    <li>Membership Transfer Details </li>
                </ul>
            </div>
        </h3>
    </div>

    <div class="divForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--Member Details--%>
                <div id="divFormDetails" runat="server">
                    <div class="form-header">
                        <h4>&#10148;Old Member Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Old Member ID </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtOldMemberId" runat="server" CssClass="txt" AutoPostBack="true" TabIndex="1" OnTextChanged="txtOldMemberId_TextChanged"
                                                    onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtOldMemberID" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtOldMemberId"
                                                    ErrorMessage="Enter Member ID" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Old First Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtOldFirstName" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="2" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Old Last Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtOldLastName" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="3" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Old Contact No </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtOldContactNo" runat="server" CssClass="txt" AutoPostBack="true" TabIndex="4" OnTextChanged="txtOldContactNo_TextChanged"
                                                    onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtOldContactNo"
                                                    ErrorMessage="Enter Contact" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--End Personal Details--%>

                    <%--Contact Details--%>
                    <div class="form-header">
                        <h4>&#10148; New Member Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>First Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtNewFirstName" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtNewFirstName"
                                                    ErrorMessage="Enter Name" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Last Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtNewLastName" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="6"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Contact </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtNewContact1" runat="server" CssClass="txt" TabIndex="7" MaxLength="11" onkeypress="return RestrictSpaceSpecial(event);"
                                                    OnTextChanged="txtNewContact1_TextChanged" AutoPostBack="true" Style="margin-top: 10px"></asp:TextBox>
                                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" CssClass="ErrorBox" SetFocusOnError="true" runat="server"
                                                ValidationExpression="^[0-9]{1,45}$" ControlToValidate="txtNewContact1" ErrorMessage="Field Must Be Numeric"></asp:RegularExpressionValidator>--%>
                                                <asp:RequiredFieldValidator ID="rfvContact" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtNewContact1"
                                                    ErrorMessage="Enter Contact " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Gender</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlNewGender" runat="server" CssClass="ddl" TabIndex="8">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlNewGender" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlNewGender"
                                                    ErrorMessage="Select Gender " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">DOB </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtDOB" runat="server" CssClass="txt" TabIndex="9"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtDob_CalendarExtender" runat="server" BehaviorID="txtDob_CalendarExtender" TargetControlID="txtDob" Format="dd-MM-yyyy" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Executive</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <%--<asp:CheckBox ID="chkExecutive" runat="server" Checked="true" TabIndex="10" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" /></td>--%>
                                                            <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" TabIndex="10" onChange="ChExecutive();" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="11" Enabled="false">
                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                            <asp:RequiredFieldValidator ID="rfvddlExecutive" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlExecutive"
                                                            ErrorMessage="Select Executive " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <%--End Contact Details--%>

                    <%--Other Details--%>

                    <div class="form-header">
                        <h4>&#10148; Transfer Course Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table>
                            <tr>
                                <th>Receipt No :  </th>
                                <th>Transfer Date :  </th>
                                <th>Transfer Receipt No :  </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlReceiptNo" runat="server" CssClass="ddl" TabIndex="12" AutoPostBack="true" OnSelectedIndexChanged="ddlReceiptNo_SelectedIndexChanged">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    </asp:DropDownList>                                   
                                    <asp:RequiredFieldValidator ID="rfvddlReceipt" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlReceiptNo"
                                        ErrorMessage="Select Receipt " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTransferDate" runat="server" CssClass="txt" TabIndex="13"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvregdate" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtTransferDate"
                                        ErrorMessage="Enter Reg Date" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:CalendarExtender ID="txtTrans_CalendarExtender" runat="server" BehaviorID="txtTransferDate_CalendarExtender" TargetControlID="txtTransferDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTransReceiptid" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="14" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11"
                                        OnTextChanged="txtTransReceiptid_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 10px;">

                            <asp:GridView ID="gvCourseDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found." Width="1000px"
                                PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" PageSize="20">
                                <Columns>

                                    <asp:BoundField HeaderText="Receipt NO" DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Start Date" DataField="StartDate" DataFormatString="{0: dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="End Date" DataField="EndDate" DataFormatString="{0: dd-MM-yyyy}" />

                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="form-header">
                        <h4>&#10148; Transfer Course Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table>
                            <tr>
                                <th>Transfer Fees :  </th>
                                <th>Payment Mode :  </th>
                                <th>Tax Type :  </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtTransferFees" runat="server" CssClass="txt" Text="0" TabIndex="15"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="ddl" TabIndex="16">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTaxType" runat="server" CssClass="ddl" TabIndex="17" AutoPostBack="true" Enabled="false">
                                        <asp:ListItem Value="Including">Including</asp:ListItem>
                                        <asp:ListItem Value="Excluding">Excluding</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" ID="addPayment" Text="+" TabIndex="18" ToolTip="Add Payment" OnClick="addPayment_Click"
                                        Style="font-size: 25px; font-weight: bold; color: rgb(12, 99, 16); text-decoration: none; margin-left: 20px"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <div id="paymentdiv" runat="server" style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 10px;">
                            <asp:GridView ID="gvTransferPayment" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found." Width="1000px"
                                PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" PageSize="20"
                                OnRowDeleting="gvTransferPayment_RowDeleting">
                                <Columns>
                                    <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300" />
                                    <asp:TemplateField HeaderText="Payment Mode">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPaymentMode" runat="server" Style="width: 80px" Text='<%#Eval("PaymentMode") %>' Enabled="false" TabIndex="19"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Number">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNumber" runat="server" Style="width: 80px" Text='<%#Eval("Cardno") %>' TabIndex="19" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="30"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDate" runat="server" DataFormatString="{0:dd-MM-yyyy}" AutoPostBack="true"
                                                Style="width: 80px" Text='<%#Eval("payDate","{0:dd-MM-yyyy}") %>' TabIndex="19"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                BehaviorID="txtDate_CalendarExtender" TargetControlID="txtDate" Format="dd-MM-yyyy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtExpiryDate" runat="server" DataFormatString="{0:dd-MM-yyyy}"
                                                Style="width: 80px" Text='<%#Eval("CardExpirydate","{0:dd-MM-yyyy}") %>' TabIndex="19"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server"
                                                BehaviorID="txtExpiryDate_CalendarExtender" TargetControlID="txtExpiryDate" Format="dd-MM-yyyy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBankName" runat="server" Style="width: 80px" Text='<%#Eval("BankName") %>' TabIndex="19"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBranchName" runat="server" Style="width: 80px" Text='<%#Eval("BranchName") %>' TabIndex="19"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid Amount">
                                        <ItemTemplate>
                                            <asp:UpdatePanel runat="server" ID="paidID" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtPaidAmount" runat="server" Style="width: 80px" Text='<%#Eval("Paid") %>' onkeypress="return RestrictSpaceSpecial(event);"
                                                        OnTextChanged="txtPaidAmount_TextChanged" AutoPostBack="true" MaxLength="7" TabIndex="19"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax Type">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTaxType" runat="server" Style="width: 80px" Text='<%#Eval("TaxType") %>' Enabled="false" TabIndex="19"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax">
                                        <ItemTemplate>
                                            <asp:UpdatePanel runat="server" ID="TaxID" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtTax" runat="server" Style="width: 40px" Text='<%#Eval("taxpec") %>' AutoPostBack="true" onkeypress="return isNumberKey(event,this)"
                                                        MaxLength="4" TabIndex="19"></asp:TextBox><sapn>%</sapn>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:TextBox ID="Txtvalue" runat="server" Style="width: 40px" Text='<%#Eval("TaxValue") %>' Enabled="false" TabIndex="19"></asp:TextBox><sapn>Rs</sapn>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid With Tax">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTotalAmount" runat="server" Style="width: 80px" Text='<%#Eval("PaidWithTax") %>' Enabled="false" TabIndex="19"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>
                            <table>
                                <tr>
                                    <th>Next Payment Date</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNextPaymentDate" runat="server" TabIndex="20"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtNextPaymentDate_CalendarExtender" runat="server"
                                            BehaviorID="txtNextPaymentDate_CalendarExtender" TargetControlID="txtNextPaymentDate" Format="dd-MM-yyyy" />
                                    </td>
                                </tr>
                            </table>
                            <div style="float: right;">
                                <h3>Total Fee :<asp:Label ID="lblTotalFeeDue" runat="server" TabIndex="21"></asp:Label>
                                </h3>
                                <h3>Paid Fee :<asp:Label ID="lblPaidFee" runat="server" TabIndex="22"></asp:Label>
                                </h3>
                                <h3>Balance :<asp:Label ID="lblBalance" runat="server" TabIndex="23"></asp:Label>
                                </h3>
                            </div>
                            <table>
                                <tr>
                                    <th>Comment</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Style="width: 700px; resize: none;" Rows="4" TabIndex="24"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <center class="btn-section">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="25" 
                            OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="form-btn" TabIndex="26" OnClick="btnCancel_Click"  />
                <%-- <asp:Button ID="btnView" runat="server"  Text="View" CssClass="form-btn" OnClick="btnview_Click" />--%>
                        </center>
                    </div>
                </div>
                <%--End Other Details --%>

                <%--Start Search Details --%>
                <div id="divsearch" runat="server" visible="false">
                    <div class="form-header">
                        <h4 style="float: left;">&#10148; Search Category</h4>
                    </div>
                    <div class="form-panel">
                        <table>
                            <tr>
                                <th>From Date</th>
                                <th>To Date</th>
                                <th></th>
                                <th>Category</th>
                                <th>Search by</th>
                                <th></th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="27" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="28" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" TabIndex="29" Text="Search" OnClick="btnSearch_Click" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="30" Width="143px">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="New_Member_ID">New Member_ID</asp:ListItem>                                        
                                        <asp:ListItem Value="New_Member_Name">New Member Name</asp:ListItem>
                                        <asp:ListItem Value="New_Member_Contact">New Member Contact</asp:ListItem>
                                        <asp:ListItem Value="Old_Member_ID">Old Member_ID</asp:ListItem>
                                        <asp:ListItem Value="Old_Member_Name">Old Member Name</asp:ListItem>                                        
                                        <asp:ListItem Value="Old_Member_Contact">Old Member Contact</asp:ListItem>                                        
                                        <asp:ListItem Value="Executive">Executive</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" TabIndex="31" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchCategory" runat="server" CssClass="form-btn" TabIndex="32" Text="Date with category" OnClick="btnSearchCategory_Click" />
                                    <asp:Button ID="btnExpord" runat="server" CssClass="form-btn" TabIndex="33" Text="Export To Excel" OnClick="btnExpord_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <center>
                                    <asp:Button ID="btnRefresh" runat="server" CssClass="form-btn" TabIndex="34" Text="Clear" OnClick="btnRefresh_Click"/>                                
                                </center>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 15px;">
                         <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </div>
                        <asp:GridView ID="gvMemberTransferDetails" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="Transfer_AutoID" EmptyDataText="No record found." Width="1100px"
                            PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3" AllowPaging="True" OnPageIndexChanging="gvMemberTransferDetails_PageIndexChanging"
                            PageSize="20">
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="33"
                                            CommandArgument='<%#Eval("Transfer_AutoID")%>' OnCommand="btnDelete_Command"
                                            Style="background-image: url('../NotificationIcons/f-cross_256-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Preview" HeaderStyle-HorizontalAlign="left" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPreview" runat="server" CausesValidation="false" Text="Preview" Visible="false" CommandArgument='<%#Eval("Transfer_AutoID")%>'
                                            TabIndex="33" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:BoundField HeaderText="Trans Id" DataField="Transfer_AutoID" HeaderStyle-HorizontalAlign="left" />--%>
                                <asp:BoundField HeaderText="Trans Rec Id" DataField="TransReceipt_ID" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Course Rec Id" DataField="Receipt_ID" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Trans Date" DataField="Transfer_Date" DataFormatString="{0: dd-MM-yyyy}" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Old Mem Id" DataField="OldMember_ID1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Old Mem Name" DataField="OldMName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="New Mem Id" DataField="NewMember_ID1" HeaderStyle-HorizontalAlign="left" />                                
                                <asp:BoundField HeaderText="New Mem Name" DataField="NewMName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Old Contact" DataField="OldContact" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="New Contact" DataField="NewContact" HeaderStyle-HorizontalAlign="left" />                                
                                <asp:BoundField HeaderText="Executive" DataField="ExecutiveName" HeaderStyle-HorizontalAlign="left" />

                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>
                    </div>

                </div>
                <%--End Search Details --%>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnExpord" />
            </Triggers>
            <%--  <Triggers>
            <asp:PostBackTrigger ControlID="txtContact1" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>
