<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="demoCourse.aspx.cs" Inherits="NDOnlineGym_2017.demoCourse" EnableEventValidation="false" %>


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

    <script type="text/javascript">
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="divCourseReg" runat="server" visible="true">
                    <div class="form-name-header">
                        <h3>Receipt
                 <div class="navigation">
                     <ul>
                         <li>Member Setting &nbsp; > &nbsp;</li>
                         <li>Course &nbsp; > &nbsp;</li>
                         <li>Add Course </li>
                     </ul>
                 </div>
                        </h3>
                    </div>
                </div>



                <div id="CourseDetails" runat="server" visible="false">
                    <div class="form-name-header">
                        <h3>Receipt Details
                 <div class="navigation">
                     <ul>
                         <li>Member Setting &nbsp; > &nbsp;</li>
                         <li>Course &nbsp; > &nbsp;</li>
                         <li>Add Course </li>
                     </ul>
                 </div>
                        </h3>
                    </div>
                </div>
                <div id="divFormDetails" runat="server">
                    <div class="divForm">
                        <%--Member Details--%>
                        <div class="form-header">
                            <h4>&#10148; Member Details  </h4>
                        </div>
                        <div class="form-panel">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Lblmemeber_Auto" runat="server" Text="Receipt No:"></asp:Label>
                                    </td>
                                    <td>
                                        <%-- <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>--%>
                                        <asp:TextBox ID="txtReceiptid" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="0" AutoPostBack="true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtReceiptid_TextChanged"></asp:TextBox>
                                    </td>
                                    <td><span class="lbl">Executive</span></td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <%--<asp:CheckBox ID="chkExecutive" runat="server"  Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" TabIndex="1" /></td>--%>
                                                    <asp:CheckBox ID="chkExecutive" runat="server" onChange="ChExecutive();" Checked="true" TabIndex="1" /></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="2" Enabled="false">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                            </table>
                            <table style="width: 50%;">
                                <tr>
                                    <td>
                                        <asp:RadioButton GroupName="ch" runat="server" ID="rbtnSingle" OnCheckedChanged="rbtnSingle_CheckedChanged" AutoPostBack="true" />&nbsp;<span style="font-size: 14px; font-weight: bold" tabindex="3">Single</span>
                                    </td>
                                    <td>
                                        <asp:RadioButton GroupName="ch" runat="server" ID="rbtnCouple" OnCheckedChanged="rbtnCouple_CheckedChanged" AutoPostBack="true" />&nbsp;<span style="font-size: 14px; font-weight: bold" tabindex="4">Couple</span>
                                    </td>
                                    <td>
                                        <asp:RadioButton GroupName="ch" runat="server" ID="rbtnGroup" OnCheckedChanged="rbtnGroup_CheckedChanged" AutoPostBack="true" />&nbsp;<span style="font-size: 14px; font-weight: bold" tabindex="5">Group</span>
                                        <asp:TextBox ID="txtnumber" runat="server" Style="width: 50px;" Enabled="false" OnTextChanged="txtnumber_TextChanged" AutoPostBack="true" TabIndex="6"> </asp:TextBox>
                                        <span style="font-weight: bold; color: red; margin-left: 10px;">*Note : Maximum Group Member 10</span>
                                    </td>
                                </tr>
                            </table>

                            <table id="tableInfo" runat="server" style="margin-top: 30px;" visible="false">
                                <tr>
                                    <th>ID</th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Gender</th>
                                    <th>Contact</th>
                                    <th>EmailID</th>
                                </tr>

                                <tr id="row1" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId1" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="7" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName1" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="8" onChange="javascript:capFirst(this);"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName1" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="9" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender1" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="10">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact1" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="11" OnTextChanged="txtContact1_TextChanged" AutoPostBack="true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" oncopy="return false" onpaste="return true"></asp:TextBox>



                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail1" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="12"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr id="row2" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId2" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="13" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName2" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="14" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName2" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="15" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender2" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="16">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact2" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="17" oncopy="return false" onpaste="return true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtContact2_TextChanged" AutoPostBack="true"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail2" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="18"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr id="row3" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId3" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="19" Enabled="false"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator  ID="RequiredFieldValidator5" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtPackageName"
                                    ErrorMessage="Enter Name"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>    --%>                 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName3" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="20" onChange="javascript:capFirst(this);"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDuration"
                                    ErrorMessage="Enter Duration"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator> --%>                             
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName3" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="21" onChange="javascript:capFirst(this);"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtSession"
                                    ErrorMessage="Enter Session"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>    --%>                         
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGende3" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="22">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator  ID="RequiredFieldValidator4" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlStatus"
                                    ErrorMessage="Select Status"  SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>   --%>                    
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact3" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" oncopy="return false" onpaste="return true" TabIndex="23" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtContact3_TextChanged" AutoPostBack="true"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail3" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="24"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr id="row4" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId4" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="25" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName4" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="26" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName4" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="27" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender4" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="28">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConatct4" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" oncopy="return false" onpaste="return true" TabIndex="29" onkeypress="return RestrictSpaceSpecial(event)" OnTextChanged="txtConatct4_TextChanged" MaxLength="11" AutoPostBack="true"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail4" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="30"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr id="row5" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId5" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="31" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName5" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="32" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName5" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="33" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender5" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="34">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact5" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="35" oncopy="return false" onpaste="return true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtContact5_TextChanged" AutoPostBack="true"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail5" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="36"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr id="row6" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId6" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="37" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName6" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="38" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName6" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="39" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender6" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="40">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact6" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" oncopy="return false" onpaste="return true" TabIndex="41" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtContact6_TextChanged" AutoPostBack="true"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail6" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="42"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr id="row7" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId7" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="43" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName7" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="44" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName7" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="45" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender7" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="46">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact7" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="47" oncopy="return false" onpaste="return true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtContact7_TextChanged" AutoPostBack="true"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail7" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="48"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr id="row8" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId8" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="49" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName8" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="50" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName8" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="51" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender8" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="52">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact8" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="53" oncopy="return false" onpaste="return true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtContact8_TextChanged" AutoPostBack="true"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail8" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="54"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr id="row9" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId9" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="55" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName9" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="56" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName9" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="57" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender9" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="58">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConatct9" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="59" oncopy="return false" onpaste="return true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtConatct9_TextChanged" AutoPostBack="true"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail9" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="60"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr id="row10" runat="server" visible="false">
                                    <td>
                                        <asp:TextBox ID="txtId10" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="61" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName10" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="62" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName10" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="63" onChange="javascript:capFirst(this);"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender10" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="64">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConatct10" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="65" oncopy="return false" onpaste="return true" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="11" OnTextChanged="txtConatct10_TextChanged" AutoPostBack="true"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail10" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled" TabIndex="66"></asp:TextBox>

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
                                            <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="67" AutoPostBack="true">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Package">Package</asp:ListItem>
                                                <asp:ListItem Value="Duration">Duration</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="true" TabIndex="68" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                        <td style="width: 45%;">
                                            <%-- <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" />--%>
                                        </td>
                                    </tr>
                                </table>
                                <div style="width: 1000px; height: 250px; overflow-y: scroll">

                                    <asp:GridView ID="gvCourse" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                                        DataKeyNames="Pack_AutoID" EmptyDataText="No record found." Width="970px" Font-Size="13px" PagerStyle-CssClass="pager"
                                        AllowPaging="True" PageSize="20" TabIndex="69" CssClass="GridView" OnPageIndexChanging="gvCourse_PageIndexChanging">
                                        <Columns>

                                            <asp:TemplateField ControlStyle-Width="5px" HeaderText="Add">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="+" TabIndex="70" OnClick="btnEdit_Click"
                                                        CommandArgument='<%#Eval("Pack_AutoID")%>' Style="font-size: 18px; font-weight: bold; color: rgb(242, 137, 9);" />
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
                            <div style="width: 1000px; height: auto; overflow-x: scroll;">

                                <%--                        <asp:GridView ID="GvPakageAssign" runat="server" AutoGenerateColumns="false"
                            EmptyDataText="No record found." Width="700px"
                            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" AllowPaging="True" TabIndex="27">--%>

                                <asp:GridView ID="GvPakageAssign" runat="server" AutoGenerateColumns="False" DataKeyNames="Pack_AutoID" OnRowDeleting="GvPakageAssign_RowDeleting"
                                    EmptyDataText="No record found." Width="740px" GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                                    CssClass="GridView" AllowPaging="True" TabIndex="71" OnRowDataBound="GvPakageAssign_RowDataBound">
                                    <Columns>

                                        <%-- <asp:TemplateField ControlStyle-Width="5px" HeaderText="Add">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="-" TabIndex="5" CommandArgument='<%#Eval("Pack_AutoID") %>' OnCommand="btnDelete_Command" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                                        <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass="remove" ForeColor="#CC3300" />
                                        </asp:CommandField>

                                        <%--                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Add">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete_AssignPackage" runat="server" CausesValidation="false" Text="-" TabIndex="5" CommandArgument='<%#Eval("Course_Auto") %>' OnCommand="btnDelete_AssignPackage_Command" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

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
                                                        <asp:TextBox ID="txtsDate" runat="server" Style="width: 80px" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' DataFormatString="{0:dd-MM-yyyy}" AutoPostBack="true" OnTextChanged="txtsDate_TextChanged" TabIndex="72"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txtsDate_CalendarExtender" runat="server" BehaviorID="txtsDate_CalendarExtender" TargetControlID="txtsDate" Format="dd-MM-yyyy" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEndate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' DataFormatString="{0:dd-MM-yyyy}" Style="width: 80px" TabIndex="73" Enabled="false"></asp:TextBox>
                                                <%--                                        <ajaxToolkit:CalendarExtender ID="txtEndate_CalendarExtender" runat="server" BehaviorID="txtEndate_CalendarExtender" TargetControlID="txtEndate" Format="dd-MM-yyyy">--%>
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
                                                        <asp:TextBox ID="txtDisc" runat="server" Text='<%#Eval("Discount")%>' AutoPostBack="true" OnTextChanged="txtDisc_TextChanged" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="8" Style="width: 80px" TabIndex="77"></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Instructor" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ddlInstru" runat="server" Text='<%#Eval("Staff_AutoID")%>' Style="width: 80px" TabIndex="80"></asp:TextBox>
                                                <%--<asp:DropDownList ID="ddlInstructor" runat="server" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" Style="width: 80px"   AutoPostBack="true"></asp:DropDownList>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <%--     <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotal" runat="server" Text='<%#Eval("Total")%>' Enabled="false"></asp:TextBox> 
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                        <%--<asp:TemplateField HeaderText="Executive Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExecutive" runat="server" Text='<%#Eval("Excutive")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                                        <%--<asp:BoundField HeaderText="Amount" DataField="ColumnName4" HeaderStyle-HorizontalAlign="left" />--%>
                                        <%--<asp:BoundField HeaderText="Type" DataField="ColumnName5" HeaderStyle-HorizontalAlign="left" />--%>
                                        <%--<asp:BoundField HeaderText="Discount" DataField="Discount" HeaderStyle-HorizontalAlign="left" />--%>
                                        <%--<asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />--%>
                                        <%-- <asp:BoundField HeaderText="FromTime" DataField="ColumnName6" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="ToTime" DataField="ColumnName7" HeaderStyle-HorizontalAlign="left" />--%>
                                    </Columns>
                                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                    <PagerStyle CssClass="pager" />
                                    <RowStyle Height="20px" />
                                    <AlternatingRowStyle Height="20px" BackColor="White" />
                                </asp:GridView>

                                <div style="float: right;">
                                    <h3>Total Fee :<asp:Label ID="lblTotalFee" runat="server" Text="0" TabIndex="81"></asp:Label>
                                    </h3>
                                </div>
                            </div>

                        </div>
                        <%--End Assign Package--%>
                        <%-- Balance Payment--%>
                        <div class="form-header">
                            <h4>&#10148;Payment Details </h4>
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
                                            <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="ddl" TabIndex="82" AutoPostBack="true">
                                                <%--  <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                    <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                    <asp:ListItem Value="Creadit Card">Creadit Card</asp:ListItem>
                                    <asp:ListItem Value="Debit Card">Debit Card</asp:ListItem>
                                    <asp:ListItem Value="NEFT">NEFT</asp:ListItem>
                                    <asp:ListItem Value="RTGS">RTGS</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl" TabIndex="83" AutoPostBack="true" Enabled="false">
                                                <asp:ListItem Value="Including">Including</asp:ListItem>
                                                <asp:ListItem Value="Excluding">Excluding</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:LinkButton runat="server" ID="addReceipt" Text="+" TabIndex="84" Style="font-size: 25px; font-weight: bold; color: rgb(12, 99, 16); text-decoration: none; margin-left: 20px"
                                                ToolTip="Add Payment" OnClick="addReceipt_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 1000px; height: auto; overflow-x: scroll;">

                                <asp:GridView ID="gvBalancePayment" runat="server" AutoGenerateColumns="false"
                                    EmptyDataText="No record found." Width="1000px" GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                                    CssClass="GridView" AllowPaging="True" TabIndex="85" OnRowDeleting="gvBalancePayment_RowDeleting">
                                    <Columns>
                                        <%--<asp:BoundField HeaderText="Payment Mode" HeaderStyle-HorizontalAlign="left" DataField="PaymentMode" />--%>
                                        <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300" />

                                        <%--   <asp:TemplateField ControlStyle-Width="5px" HeaderText="Add">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="-" TabIndex="5" CommandArgument='<%#Eval("Bal_Auto") %>' OnCommand="btnDelete_Command" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Payment Mode">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPaymentMode" runat="server" Style="width: 80px" Text='<%#Eval("PaymentMode") %>' Enabled="false" TabIndex="86"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Number">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNumber" runat="server" Style="width: 80px" Text='<%#Eval("Cardno") %>' TabIndex="87" onkeypress="return RestrictSpaceSpecial(event)" MaxLength="30"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDate" runat="server" DataFormatString="{0:dd-MM-yyyy}" AutoPostBack="true"
                                                    Style="width: 80px" Text='<%#Eval("payDate","{0:dd-MM-yyyy}") %>' TabIndex="88"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtDate_CalendarExtender" TargetControlID="txtDate" Format="dd-MM-yyyy" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Expiry Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtExpiryDate" runat="server" DataFormatString="{0:dd-MM-yyyy}"
                                                    Style="width: 80px" Text='<%#Eval("CardExpirydate","{0:dd-MM-yyyy}") %>' TabIndex="89"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtExpiryDate_CalendarExtender" TargetControlID="txtExpiryDate" Format="dd-MM-yyyy" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBankName" runat="server" Style="width: 80px" Text='<%#Eval("BankName") %>' TabIndex="90"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Branch Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBranchName" runat="server" Style="width: 80px" Text='<%#Eval("BranchName") %>' TabIndex="91"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Amount">
                                            <ItemTemplate>
                                                <asp:UpdatePanel runat="server" ID="paidID" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtPaidAmount" runat="server" Style="width: 80px" Text='<%#Eval("Paid") %>' AutoPostBack="true" OnTextChanged="txtPaidAmount_TextChanged" MaxLength="7" onkeypress="return isNumberKey(event,this)" TabIndex="92"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tax Type">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ddlTax" runat="server" Style="width: 80px" Text='<%#Eval("TaxType") %>' Enabled="false" TabIndex="93"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tax">
                                            <ItemTemplate>
                                                <%--<asp:TextBox ID="ddlTax" runat="server" Style="width: 40px" Text='<%#Eval("ddlTax") %>'>--%>

                                                <asp:UpdatePanel runat="server" ID="TaxID" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtTax" runat="server" Style="width: 40px" Text='<%#Eval("taxpec") %>' AutoPostBack="true" OnTextChanged="txtTax_TextChanged" onkeypress="return isNumberKey(event,this)" MaxLength="4" TabIndex="94"></asp:TextBox><sapn>%</sapn>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <asp:TextBox ID="Txtvalue" runat="server" Style="width: 40px" Text='<%#Eval("TaxValue") %>' Enabled="false" TabIndex="95"></asp:TextBox><sapn>Rs</sapn>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Paid With Tax">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotalAmount" runat="server" Style="width: 80px" Text='<%#Eval("PaidWithTax") %>' Enabled="false" TabIndex="96"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                <asp:Button ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click"  CssClass="form-btn" TabIndex="102" OnClientClick=" this.disabled = true;" UseSubmitBehavior="false"/>
              <%--  <asp:Button ID="btnView" runat="server" Text="View" CssClass="form-btn" />--%>
                <asp:Button ID="btnCancle" runat="server" Text="Clear" TabIndex="103" OnClick="btnCancle_Click" OnClientClick="window.location.reload();" CssClass="form-btn" />
             </center>

                    </div>
                </div>
                <div id="divsearch" runat="server">
                    <div class="form-header">
                        <h4 style="float: left;">&#10148; Search Category
               <%-- <div style="float:right;padding-right:10px;">--%>

                            <%-- </div>--%>

                        </h4>
                    </div>

                    <asp:UpdatePanel ID="pnlPageRefresh" runat="server">
                        <ContentTemplate>
                            <div class="form-panel">
                                <table>
                                    <tr>
                                        <th>From Date</th>
                                        <th>To Date</th>
                                        <th>Category</th>
                                        <th>Search by</th>
                                        <th></th>
                                        <th></th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="29" Style="width: 200px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="30" Style="width: 200px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="ddl" TabIndex="104" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="true" Style="width: 200px">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Member ID">Member ID</asp:ListItem>
                                                <asp:ListItem Value="Receipt ID">Receipt ID</asp:ListItem>
                                                <asp:ListItem Value="First Name">First Name</asp:ListItem>
                                                <asp:ListItem Value="Last Name">Last Name</asp:ListItem>
                                                <asp:ListItem Value="ContactNo">ContactNo</asp:ListItem>
                                                <%--<asp:ListItem Value="Course Name">Course Name</asp:ListItem>--%>
                                                <asp:ListItem Value="Active">Active</asp:ListItem>
                                                <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                                <asp:ListItem Value="New">New</asp:ListItem>
                                                <asp:ListItem Value="Upgrade">Upgrade</asp:ListItem>
                                                <asp:ListItem Value="Executive">Executive</asp:ListItem>
                                                <asp:ListItem Value="PaymentMode">Payment Mode</asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="txt" Enabled="true" TabIndex="105" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" Style="width: 200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <table style="margin-top: 15px">
                                    <tr>

                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" TabIndex="106" Text="Search" OnClick="btnSearch_Click" Style="width: 200px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" CssClass="form-btn" TabIndex="107" Text="Date with category" OnClick="Button2_Click" Style="width: 200px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnclear" runat="server" CssClass="form-btn" TabIndex="108" Text="Clear" OnClick="btnclear_Click" Style="width: 200px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnExportExcel" runat="server" CssClass="form-btn" TabIndex="109" Text="Export to Excel" OnClick="BtnExportExcel_Click" Style="width: 200px" />
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 5px;">
                        <div style="margin: 10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records = " Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </div>
                        <asp:GridView ID="GVCourseDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found."
                            Width="1700px" AllowPaging="True" PageSize="20" TabIndex="110" OnPageIndexChanging="GVCourseDetails_PageIndexChanging"
                            PagerStyle-CssClass="pager" CssClass="GridView1" GridLines="None" CellPadding="5">
                            <Columns>
                                <%--<asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" CommandArgument='<%#Eval("ReceiptID")%>' OnCommand="btnEdit_Command" TabIndex="111" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="112"
                                            CommandArgument='<%#Eval("ReceiptID")%>' Text="Delete" OnCommand="btnDelete_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="preview">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnpreview" runat="server" CausesValidation="false" Text="Preview" CommandArgument='<%#Eval("ReceiptID")%>' OnCommand="btnpreview_Command" TabIndex="113" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="ResReceipt">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnResend" runat="server" CausesValidation="false" Text="Resend" CommandArgument='<%#Eval("ReceiptID")%>' OnCommand="btnResend_Command" TabIndex="114" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Mem.ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Rec.ID" DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                                <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                    <HeaderTemplate>
                                        <b>Pay.Date</b>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("payDate","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="FirstName" DataField="FName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="LastName" DataField="LName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="MemberType" DataField="MemberType" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="CourseMemberType" DataField="CourseMemberType" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Qty" DataField="Qty" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Total" DataField="TotalFeeDue" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Paid" DataField="PaidFee" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Balance" DataField="Balance" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="PaymentMode" DataField="PaymentMode" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Comment" DataField="Comment" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />
                                <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                    <HeaderTemplate>
                                        <b>NextPayDate</b>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("NextBalDate","{0:dd-MM-yyyy}")%>
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
                <asp:PostBackTrigger ControlID="BtnExportExcel" />

            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
