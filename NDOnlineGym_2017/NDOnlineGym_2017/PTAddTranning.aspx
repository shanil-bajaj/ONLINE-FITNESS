<%@ Page Title="" Language="C#" MasterPageFile="~/PTMaster.Master" AutoEventWireup="true" CodeBehind="PTAddTranning.aspx.cs" Inherits="NDOnlineGym_2017.PTAddTranning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <%-- <link rel="icon" type="image/png" href="Logo/NDLogo.png" />--%>
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script type="text/javascript">
        function capFirst(oTextBox) {
            oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
        }
    </script>
    <style>
        input:focus
        {
            border: 1px solid rgb(242, 137, 9);
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

        .remove
        {
            font-size: 18px;
            font-weight: bold;
        }

        .GridView1
        {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

            .GridView1 a /** FOR THE PAGING ICONS  **/
            {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
            }

                .GridView1 a:focus
                {
                    color: orangered;
                }

                .GridView1 a:hover
                {
                    color: orangered;
                }

            .GridView1 span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
            {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .hideGridColumn
        {
            display: none;
        }

        .GridView a:focus
        {
            color: orangered;
        }

        .GridView a:hover
        {
            color: orangered;
        }

        .sc
        {
            width: 1021px;
        }

        @media screen and (min-width: 1400px)
        {
            .sc
            {
                width: 1100px;
            }
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
    <script type="text/javascript">

        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }



    </script>

    <%--    <script type="text/javascript">
        function ConfirmDelete() {

            var gv = document.getElementById("<%=GVCourseDetails.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("No records to delete.");
                return false;
            }
            else {
                return confirm("Do you want to delete records.");
            }
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-name-header">
                    <h3>Personal Tranning  Form
                 <div class="navigation">
                     <ul>
                         <li>PT &nbsp; > &nbsp;</li>
                         <li>Add Tranning </li>
                     </ul>
                 </div>
                    </h3>
                </div>


                <div class="divForm">
                    <%--Member Details--%>
                    <div class="form-header">
                        <h4>&#10148; Member Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="LblReceipt" runat="server" Text="Receipt No:"></asp:Label>
                                </td>
                                <td>
                                    <%-- <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>--%>
                                    <asp:TextBox ID="txtReceiptid" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="0" OnTextChanged="txtReceiptid_TextChanged" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="6" AutoPostBack="true"></asp:TextBox>
                                </td>
                                <td><span class="lbl">Executive</span></td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" TabIndex="1" /></td>
                                            <td>
                                                <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="2" Enabled="false">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                        </table>

                        <table id="tableInfo" runat="server" style="margin-top: 30px;" visible="true">
                            <tr>
                                <th>ID</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Gender</th>
                                <th>Contact</th>
                                <th>EmailID</th>
                            </tr>

                            <tr id="row1" runat="server" visible="true">
                                <td>
                                    <asp:TextBox ID="txtId1" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="3" OnTextChanged="txtId1_TextChanged" AutoPostBack="true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="6"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtFirstName1" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="4" Enabled="false"></asp:TextBox>


                                </td>
                                <td>
                                    <asp:TextBox ID="txtLastName1" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="5" Enabled="false"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGender1" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="6" Enabled="false">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtContact1" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="7" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtContact1_TextChanged" AutoPostBack="true"></asp:TextBox>



                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail1" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="8" Enabled="false"></asp:TextBox>
                                    <asp:Label ID="lblMemberAutoID" runat="server" Visible="false"></asp:Label>

                                </td>
                            </tr>


                        </table>

                    </div>

                    <%--End Member Details--%>
                    <%--Course Details--%>
                    <div id="Divcourse" runat="server" visible="true">
                        <div class="form-header">
                            <h4>&#10148; Course Details  </h4>
                        </div>
                        <div class="form-panel">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 20%;">
                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="9" AutoPostBack="true">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Package">Package</asp:ListItem>
                                            <asp:ListItem Value="Duration">Duration</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="true" TabIndex="10"></asp:TextBox>
                                    </td>
                                    <td style="width: 45%;">
                                        <%-- <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" />--%>
                                    </td>
                                </tr>
                            </table>
                            <div style="width: 1000px; height: 250px; overflow-y: scroll">

                                <asp:GridView ID="gvCourse" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                                    DataKeyNames="Pack_AutoID" EmptyDataText="No record found." Width="970px" Font-Size="13px" PagerStyle-CssClass="pager"
                                    AllowPaging="True" PageSize="20" TabIndex="11" CssClass="GridView">
                                    <Columns>

                                        <asp:TemplateField ControlStyle-Width="5px" HeaderText="Add">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="+" TabIndex="12"
                                                    CommandArgument='<%#Eval("Pack_AutoID")%>' Style="font-size: 18px; font-weight: bold; color: rgb(242, 137, 9);" OnClick="btnEdit_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Package ID" DataField="Pack_AutoID" HeaderStyle-HorizontalAlign="left" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="left" />
                                        <%--<asp:BoundField HeaderText="Type" DataField="Type" HeaderStyle-HorizontalAlign="left" />--%>
                                        <%--<asp:BoundField HeaderText="Discount" DataField="Discount" HeaderStyle-HorizontalAlign="left" />--%>
                                        <%--<asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />--%>
                                        <asp:BoundField HeaderText="FromTime" DataField="FromTime" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="ToTime" DataField="ToTime" HeaderStyle-HorizontalAlign="left" />

                                    </Columns>
                                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                    <RowStyle Height="20px" />
                                    <AlternatingRowStyle Height="20px" BackColor="White" />
                                </asp:GridView>


                            </div>

                        </div>

                        <%--End Course Details--%>

                        <%--Assign Package--%>
                    </div>

                    <div class="form-header">
                        <h4>&#10148; Assign Package  </h4>
                    </div>

                    <div class="form-panel">
                        <div style="width: 1000px; height: auto; overflow-x: auto;">

                            <table>
                                <tr>
                                    <th>Days</th>
                                    <th>Instructor</th>
                                    <th>Time</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlDays" runat="server" CssClass="ddl" TabIndex="13" Style="width: 200px;">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInstructor" runat="server" CssClass="ddl" TabIndex="14" Style="width: 200px;" AutoPostBack="true" OnSelectedIndexChanged="ddlInstructor_SelectedIndexChanged">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTime" runat="server" CssClass="ddl" TabIndex="15" Style="width: 200px;" OnSelectedIndexChanged="ddlTime_SelectedIndexChanged">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblpackAutoID" runat="server" Text="Label" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                            <table style="margin-top: 20px;">
                                <tr>
                                    <th>Package</th>
                                    <th>Duration</th>
                                    <th>Session</th>
                                    <th>Start Date</th>
                                    <th>End Date</th>
                                    <th>Amount</th>
                                    <th>Oty</th>
                                    <th>Total</th>
                                    <th>Discount</th>
                                    <th>Discount Reason</th>
                                    <th>Final Total</th>

                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtPackage" runat="server" Style="width: 120px; padding: 3px 5px;" TabIndex="16" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDuration" runat="server" Style="width: 70px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="17" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSession" runat="server" Style="width: 80px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="18" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStartDate" runat="server" Style="padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="19" Width="86px" AutoPostBack="true" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="20" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="21" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="8" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty" runat="server" Style="padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="22" Width="24px" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="2" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal" runat="server" Style="width: 70px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="23" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="7" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDic" runat="server" Style="width: 80px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="24" AutoPostBack="true" OnTextChanged="txtDic_TextChanged" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="4"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDicReason" runat="server" Style="width: 80px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="25"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFinalTotal" runat="server" Style="width: 100px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="26" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" Enabled="false"></asp:TextBox>
                                    </td>


                                </tr>
                            </table>


                            <div style="float: right;">
                                <h3>Total Fee :<asp:Label ID="lblTotalFee" runat="server" Text="0" TabIndex="27"></asp:Label>
                                </h3>
                            </div>
                        </div>

                    </div>
                    <%--End Assign Package--%>
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
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddl" TabIndex="28">
                                            <%--  <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                    <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                    <asp:ListItem Value="Creadit Card">Creadit Card</asp:ListItem>
                                    <asp:ListItem Value="Debit Card">Debit Card</asp:ListItem>
                                    <asp:ListItem Value="NEFT">NEFT</asp:ListItem>
                                    <asp:ListItem Value="RTGS">RTGS</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl" TabIndex="29" AutoPostBack="true" Enabled="false">
                                            <asp:ListItem Value="Including">Including</asp:ListItem>
                                            <asp:ListItem Value="Excluding">Excluding</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="addReceipt" Text="+" TabIndex="30" Style="font-size: 25px; font-weight: bold; color: rgb(12, 99, 16); text-decoration: none; margin-left: 20px"
                                            ToolTip="Add Payment" OnClick="addReceipt_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%--       <div style="width: 1000px; height: auto; overflow-x: scroll;">

                    <asp:GridView ID="gvBalancePayment" runat="server" AutoGenerateColumns="false"
                        EmptyDataText="No record found." Width="1000px" GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                        CssClass="GridView" AllowPaging="True" TabIndex="85">
                        <Columns>

                            <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="left"
                                ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="remove" ForeColor="#CC3300" />
                            </asp:CommandField>

                            <asp:BoundField HeaderText="Package ID" DataField="Pack_AutoID" HeaderStyle-HorizontalAlign="left" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left">

                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate>
                                    <asp:UpdatePanel runat="server" ID="UpId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtsDate" runat="server" Style="width: 80px" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' DataFormatString="{0:dd-MM-yyyy}" AutoPostBack="true" TabIndex="72"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtsDate_CalendarExtender" runat="server" BehaviorID="txtsDate_CalendarExtender" TargetControlID="txtsDate" Format="dd-MM-yyyy" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEndate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' DataFormatString="{0:dd-MM-yyyy}" Style="width: 80px" TabIndex="73"></asp:TextBox>
                                    
                                    <ajaxToolkit:CalendarExtender ID="txtEndate_CalendarExtender" runat="server" BehaviorID="txtEndate_CalendarExtender" TargetControlID="txtEndate" Format="dd-MM-yyyy" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmt" runat="server" Text='<%#Eval("Amount")%>' Enabled="false" Style="width: 60px" TabIndex="74"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Qty")%>' Enabled="false" Style="width: 40px" TabIndex="75"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTotal" runat="server" Text='<%#Eval("Total")%>' Enabled="false" Style="width: 80px" TabIndex="76"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Discount">
                                <ItemTemplate>
                                    <asp:UpdatePanel runat="server" ID="DiscId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDisc" runat="server" Text='<%#Eval("Discount")%>' AutoPostBack="true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="8" Style="width: 80px" TabIndex="77"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Final Total">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtFinalTotal" runat="server" Enabled="false" Text='<%#Eval("FinalTotal")%>' Style="width: 70px" TabIndex="78"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Disc Reason">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDiscreason" runat="server" Text='<%#Eval("DiscReason")%>' Style="width: 80px" TabIndex="79"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Instructor">
                                <ItemTemplate>
                                    <asp:TextBox ID="ddlInstru" runat="server" Text='<%#Eval("Staff_AutoID")%>' Style="width: 80px" TabIndex="80"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <PagerStyle CssClass="pager" />
                        <RowStyle Height="20px" />
                        <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>


                 

                </div>--%>


                        <table style="margin-top: 20px;">
                            <tr>
                                <th>Paymode</th>
                                <th>Number</th>
                                <th>Date</th>
                                <th>Expiry Date</th>
                                <th>Bank Name</th>
                                <th>Branch  Name</th>
                                <th>Paid Amount</th>
                                <th>Tax Type</th>
                                <th>Tax</th>
                                <th>Paid With Tax</th>


                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPaymode" runat="server" Style="width: 120px; padding: 3px 5px;" TabIndex="31" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtnumber" runat="server" Style="width: 70px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="32" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="17"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdate" runat="server" Style="width: 80px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="33"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExpiryDate" runat="server" Style="padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="34" Width="86px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBankName" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="35"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBranchname" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="36"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPaidAmount" runat="server" Style="padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="37" Width="100px" AutoPostBack="true" OnTextChanged="txtPaidAmount_TextChanged" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="8"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTaxType" runat="server" Style="width: 100px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="38" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTax" runat="server" Style="width: 40px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="39" AutoPostBack="true" OnTextChanged="txtTax_TextChanged" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="5"></asp:TextBox>
                                    <asp:TextBox ID="txtrs" runat="server" Style="width: 40px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="40" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPaidWithTax" runat="server" Style="width: 80px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="41" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" Enabled="false"></asp:TextBox>
                                </td>

                            </tr>
                        </table>
                        <div style="float: right;">
                            <h3>Total Fee :<asp:Label ID="lblTotalFeeDue" runat="server" Text="0" TabIndex="42"></asp:Label>
                            </h3>
                            <h3>Paid Fee :<asp:Label ID="lblPaidFee" runat="server" Text="0" TabIndex="43"></asp:Label>
                            </h3>
                            <h3>Balance :<asp:Label ID="lblBalance" runat="server" Text="0" TabIndex="44"></asp:Label>
                            </h3>
                        </div>

                        <table>
                            <tr>
                                <th>Next Payment Date</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtNextFollowupDate" runat="server" TabIndex="45"></asp:TextBox>
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
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Style="width: 700px; resize: none;" Rows="4" TabIndex="46"></asp:TextBox></td>
                            </tr>
                        </table>

                    </div>
                    <%--End Balance Payment--%>
                    <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="form-btn" TabIndex="47" OnClick="btnSave_Click"/>
              <%--  <asp:Button ID="btnView" runat="server" Text="View" CssClass="form-btn" />--%>
                <asp:Button ID="btnCancle" runat="server" Text="Clear" TabIndex="48"  CssClass="form-btn"  OnClick="btnCancle_Click"/>
             </center>

                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
