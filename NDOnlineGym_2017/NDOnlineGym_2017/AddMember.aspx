<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddMember.aspx.cs" Inherits="NDOnlineGym_2017.AddMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
     <style>
        .ddlTimeExe
        {
            width: 91%;
            padding: 4px 10px;
            border-radius: 3px;
            border: none;
            border: solid 1px silver;
            outline: none;
            height: 25px;
        }

            .ddlTimeExe:focus
            {
                box-shadow: 0px 0px 10px rgba(81, 203, 238, 1);
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

                .GridView a:focus
                {
                    color: orangered;
                }

                .GridView a:hover
                {
                    color: orangered;
                }

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
            {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .btn-remove
        {
            background-color: rgb(248, 45, 70);
            color: white;
            border: 1px solid rgb(248, 45, 70);
            margin-top: 3px;
        }

            .btn-remove:focus
            {
                border: 1px solid black;
                cursor: pointer;
            }

        .btn-file:focus
        {
            border: 1px solid silver;
            cursor: pointer;
        }

        input[type="checkbox"]:focus
        {
            border-color: #ffffcc;
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
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <script src="JS/Common-Script.js"></script>
    <script>

        // Rating Change Event
        function ddlMaritalChange() {
            var _ddlMarital = document.getElementById('<%= ddlMaritalStatus.ClientID %>');
            var Anniversary = document.getElementById('<%= txtAnniversary.ClientID %>');
            var selectedText = _ddlMarital.options[_ddlMarital.selectedIndex].value;
            if (selectedText == "Married") {
                Anniversary.disabled = false;
            }
            else{
                Anniversary.value = "";
                Anniversary.disabled = true;
            }
        }

        // Check Email validation
        function IsValidEmail(email) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(email);
        };
        function ValidateEmail() {
            var email = document.getElementById('<%= txtEmailID.ClientID %>');

            if (email.value != "") {
                if (!IsValidEmail(email.value)) {
                    toastr.error('Invalid Email Id !!!', 'Error');
                    email.focus();
                }
            }
        }

        //Disable enable executive dropdown on check box
        function ChExecutive() {
            var _chkExecutive = document.getElementById('<%= chkExecutive.ClientID %>');
            document.getElementById('<%= ddlExecutive.ClientID %>').disabled = _chkExecutive.checked;
            }
    </script>
    <script>
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgMember.ClientID%>').prop('src', e.target.result)
                };
                reader.readAsDataURL(input.files[0]);
                }
        }       

    </script>
    <script>
        function ShowImagePreview1(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgIDProof.ClientID%>').prop('src', e.target.result)
                };
                reader.readAsDataURL(input.files[0]);
                }
        }

        function chkIdProffName() {
            var filePath = document.getElementById("<%=fileIDProof.ClientID %>");
            var imagename = filePath.value.substring(filePath.value.lastIndexOf('\\') + 1);
            var IdProffName = document.getElementById("<%=txtIDProof.ClientID%>");
            if (imagename != "") {
                if (IdProffName.value != "") {
                    return true;
                }
                else {
                    toastr.error('Please Enter IDProof Name !!!', 'Error');
                    IdProffName.focus();
                    return false;
                }
            }
            else {
                if (IdProffName.value == "") {
                    return true;
                }
                else {
                    toastr.error('Please Select IDProof Image !!!', 'Error');
                    filePath.focus();
                    return false;
                }
            }
        }
    </script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
        <div class="form-name-header">
            <h3>Add Member
                 <div class="navigation">
                     <ul>
                         <li>Member Setting &nbsp; > &nbsp;</li>
                         <li>Member &nbsp; > &nbsp;</li>
                         <li>Add Member</li>
                     </ul>
                 </div>
            </h3>


        </div>
        <table style="width: 100%">
            <tr>
                <td style="width: 45%"></td>
                <td style="width: 45%"></td>
                <td>
                    <asp:Button ID="btnAddFromEnquiry" runat="server" Text="Add From Enquiry" OnClick="btnAddFromEnquiry_Click" />
                </td>
            </tr>

        </table>
        <div class="divForm">

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <%--Member Details--%>

                    <div class="form-header">
                        <h4>&#10148; Member Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Member ID </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtMemberID" runat="server" CssClass="txt" AutoPostBack="true" OnTextChanged="txtMemberID_TextChanged" TabIndex="1" onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCompanyID" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtMemberID"
                                                    ErrorMessage="Enter Member ID" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> First Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtFirstName" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="2" AutoCompleteType="Disabled"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtFirstName"
                                                    ErrorMessage="Enter First Name" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Last Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtLastName" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="3" AutoCompleteType="Disabled"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator  ID="rVLName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtLastName"
                                ErrorMessage="Enter Last Name"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
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
                                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="ddl" TabIndex="4">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlGender" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlGender"
                                                    ErrorMessage="Select Gender " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Reg Date </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtRegDate" runat="server" CssClass="txt" TabIndex="5"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtRegDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtRegDate" Format="dd-MM-yyyy" />
                                                <asp:RequiredFieldValidator ID="rfvregdate" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtRegDate"
                                                    ErrorMessage="Enter Reg Date" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                <%--<ajaxToolkit:CalendarExtender ID="txtReg_CalendarExtender" runat="server" BehaviorID="txtRegDate_CalendarExtender" 
                                                    TargetControlID="txtRegDate" Format="dd-MM-yyyy" />--%>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">DOB </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtDOB" runat="server" CssClass="txt" TabIndex="6"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtDOB_CalendarExtender" runat="server"
                                                    BehaviorID="txtDOB_CalendarExtender" TargetControlID="txtDOB" Format="dd-MM-yyyy" />
                                               <%-- <ajaxToolkit:CalendarExtender ID="txtDob_CalendarExtender" runat="server" BehaviorID="txtDob_CalendarExtender" 
                                                    TargetControlID="txtDob" Format="dd-MM-yyyy" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>

                            <tr>

                                <td class="cols">
                                    <%--<table><tr>
                            <td style="width:45%;"><span class="lbl">Anniversary </span></td>
                            <td style="width:55%;text-align:left;">
                             <asp:TextBox ID="txtAnniversary" runat="server" CssClass="ddlTimeExe numericOnly dateFormet" TabIndex="7"></asp:TextBox>
                               <ajaxToolkit:CalendarExtender ID="txtAnnverysary_CalendarExtender" runat="server" BehaviorID="txtAnniversary_CalendarExtender" TargetControlID="txtAnniversary" Format="dd-MM-yyyy" />
                            </td>
                         </tr></table>--%>

                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Marital Status </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <%--<asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="ddl" TabIndex="7" AutoPostBack="true" OnSelectedIndexChanged="ddlMaritalStatus_SelectedIndexChanged">--%>
                                                <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="ddl" TabIndex="7" onChange="ddlMaritalChange();">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Married">Married</asp:ListItem>
                                                    <asp:ListItem Value="Unmarried">Unmarried</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">

                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Anniversary </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtAnniversary" runat="server" CssClass="txt" TabIndex="8" Enabled="false"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtAnnverysary_CalendarExtender" runat="server" BehaviorID="txtAnniversary_CalendarExtender" TargetControlID="txtAnniversary" Format="dd-MM-yyyy" />
                                            </td>
                                        </tr>
                                    </table>

                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Blood Group </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="ddl" TabIndex="9">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem Value="A+">A+</asp:ListItem>
                                                    <asp:ListItem Value="B+">B+</asp:ListItem>
                                                    <asp:ListItem Value="O+">O+</asp:ListItem>
                                                    <asp:ListItem Value="AB+">AB+</asp:ListItem>
                                                    <asp:ListItem Value="A-">A-</asp:ListItem>
                                                    <asp:ListItem Value="B-">B-</asp:ListItem>
                                                    <asp:ListItem Value="O-">O-</asp:ListItem>
                                                    <asp:ListItem Value="AB-">AB-</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--   <asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlBloodGroup"
                                ErrorMessage="Select Blood Group "  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
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
                        <h4>&#10148; Contact Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Contact 1 </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtContact1" runat="server" AutoPostBack="true" AutoCompleteType="Disabled" CssClass="txt" TabIndex="10" OnTextChanged="txtContact1_TextChanged" MaxLength="11" onkeypress="return RestrictSpaceSpecial(event);" Style="margin-top: 10px"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" CssClass="ErrorBox" SetFocusOnError="true" runat="server" ValidationExpression="^[0-9]{1,45}$" ControlToValidate="txtContact1" ErrorMessage="Field Must Be Numeric"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="rfvContact" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtContact1"
                                                            ErrorMessage="Enter Contact " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Contact 2</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtContact2" runat="server" CssClass="txt" TabIndex="11" AutoCompleteType="Disabled"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Whatsapp No.</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtWatsapp" runat="server" CssClass="txt" TabIndex="12" AutoCompleteType="Disabled"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Email ID </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="txt-add" Rows="3" TabIndex="13" onchange='ValidateEmail();' AutoCompleteType="Disabled"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td>
                                    <table style="margin-left: 80px">
                                        <tr>
                                            <td style="width: 50%;"><span class="lbl">Address </span></td>
                                            <td style="width: 50%; text-align: left;">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="txt-add" TextMode="MultiLine" AutoCompleteType="Disabled" Rows="3" TabIndex="14" Width="290px" Style="resize: none"></asp:TextBox>
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
                        <h4>&#10148; Other Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Occupation </span></td>
                                            <td style="width: 55%; text-align: left;">

                                                <asp:DropDownList ID="ddlOccupation" runat="server" CssClass="ddl" TabIndex="15">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Health Details</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtHealthDetails" runat="server" CssClass="txt" TabIndex="16" AutoCompleteType="Disabled"></asp:TextBox>
                                                <%--  <asp:RequiredFieldValidator  ID="RequiredFieldValidator4" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtHealthDetails"
                              ErrorMessage="Enter Health Details"  SetFocusOnError="true" ValidationGroup="a" ></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">ID Proof </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtIDProof" runat="server" CssClass="txt" TabIndex="17" AutoCompleteType="Disabled"></asp:TextBox>


                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Card Number</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtCardNumber" runat="server" CssClass="txt" TabIndex="18" AutoCompleteType="Disabled"></asp:TextBox>
                                                <%--  <asp:RequiredFieldValidator  ID="RequiredFieldValidator5" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtCardNumber"
                              ErrorMessage="Enter Card Number"  SetFocusOnError="true" ValidationGroup="a" ></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Status</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" CssClass="ddl" TabIndex="19">
                                                    <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                                    <asp:ListItem Value="Active">Active</asp:ListItem>

                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> SMS Status</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlSMSStatus" runat="server" CssClass="ddl" TabIndex="20">
                                                    <%--<asp:ListItem Value="--Select--">--Select--</asp:ListItem>--%>
                                                    <asp:ListItem Value="Yes">YES</asp:ListItem>
                                                    <asp:ListItem Value="No">NO</asp:ListItem>

                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="Reqsms" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlSMSStatus"
                                                    ErrorMessage="Select SMS Status " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; margin-top: 5px;">
                            <tr>
                                <td class="cols">
                                    <table style="padding-left: 45px;">
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Executive</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <%--<asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" TabIndex="21" /></td>--%>
                                                            <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" onChange="ChExecutive();" TabIndex="21" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="22" Enabled="false">
                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table style="padding-left: 90px;">
                                        <tr>
                                            <td><span style="font-size: 12.5px; float: left">Image</span></td>
                                            <td>
                                                <asp:FileUpload ID="fileLogo" runat="server" TabIndex="23" onchange="ShowImagePreview(this);" CssClass="btn-file" /></td>
                                        </tr>
                                    </table>
                                </td>
                                
                                <td class="cols" style="padding-left: 70px;">                                    
                                    <div style="width: 90px; height: 70px; border: 1px solid silver;">
                                    
                                        <asp:Image ID="imgMember" runat="server" Style="width: 90px; height: 70px; border: 1px solid silver;"
                                            class="fileupload-preview thumbnail" />                                        
                                    </div>
                                    <asp:Button runat="server" ID="btnRemove" Text="Remove" OnClick="btnRemove_Click" TabIndex="24" CssClass="btn-remove" />     
                                    <%--<asp:Button runat="server" ID="btnRemove" Text="Remove" OnClientClick="ClearFileUploadControl()" TabIndex="24" CssClass="btn-remove" />--%>
                                </td>                                   
                            </tr>
                        </table>                   
                        <table>
                            <tr>
                                <td class="cols" style="padding-left: 70px;">
                                    <span style="padding-right: 10px; padding-left: 30px; font-size: 12.5px;">ID Proof</span>
                                    <asp:FileUpload ID="fileIDProof" CssClass="btn-file" runat="server" TabIndex="25" onchange="ShowImagePreview1(this);" />
                                </td>
                                <td class="cols" style="padding-left: 70px;">
                                    
                                    <div style="width: 90px; height: 70px; border: 1px solid silver;">                                   
                                        <asp:Image ID="imgIDProof" runat="server" Style="width: 90px; height: 70px; border: 1px solid silver;"
                                            class="fileupload-preview thumbnail" />                                            
                                    </div>
                                    <asp:Button runat="server" ID="btnRemove1" Text="Remove" OnClick="btnRemove1_Click" TabIndex="26"
                                        CssClass="btn-remove" />                                                                  
                                </td>
                                <td class="cols">
                                </td>
                            </tr>

                        </table>                                                           
                        <div>

                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                            <script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>
                            <script type="text/javascript">
                                var pageUrl = '<%=ResolveUrl("~/AddMember.aspx") %>';
                                $(function () {
                                    jQuery("#webcam").webcam({
                                        width: 320,
                                        height: 240,
                                        mode: "save",
                                        swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                                        debug: function (type, status) {
                                            $('#camStatus').append(type + ": " + status + '<br /><br />');
                                        },
                                        onSave: function (data) {
                                            $.ajax({
                                                type: "POST",
                                                url: pageUrl + "/GetCapturedImage",
                                                data: '',
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (r) {
                                                    $("[id*=imgMember]").css("visibility", "visible");
                                                    $("[id*=imgMember]").attr("src", r.d);
                                                },
                                                failure: function (response) {
                                                    alert(response.d);
                                                }
                                            });
                                        },
                                        onCapture: function () {
                                            webcam.save(pageUrl);
                                        }
                                    });
                                });
                                function Capture() {
                                    webcam.capture();
                                    return false;
                                }
                            </script>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="center">
                                        <u>Live Camera</u>
                                    </td>
                                    <td></td>
                                    <td align="center">
                                        <u>Captured Picture</u>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="webcam">
                                        </div>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Image ID="imgCapture" runat="server" Style="visibility: hidden; width: 320px; height: 240px" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Button ID="btnCapture" Text="Capture" runat="server" OnClientClick="return Capture();" />
                            <br />
                            <span id="camStatus"></span>
                        </div>


                        <%-- <div>

                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                            <script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>
                            <script type="text/javascript">
                                var pageUrl = '<%=ResolveUrl("~/AddMember.aspx") %>';
                                $(function () {
                                    jQuery("#webcam1").webcam({
                                        width: 320,
                                        height: 240,
                                        mode: "save",
                                        swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                                        debug: function (type, status) {
                                            $('#camStatus1').append(type + ": " + status + '<br /><br />');
                                        },
                                        onSave: function (data) {
                                            $.ajax({
                                                type: "POST",
                                                url: pageUrl + "/GetCapturedImage",
                                                data: '',
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (r) {
                                                    $("[id*=imgCapture1]").css("visibility", "visible");
                                                    $("[id*=imgCapture1]").attr("src", r.d);
                                                },
                                                failure: function (response) {
                                                    alert(response.d);
                                                }
                                            });
                                        },
                                        onCapture: function () {
                                            webcam.save(pageUrl);
                                        }
                                    });
                                });
                                function Capture() {
                                    webcam.capture();
                                    return false;
                                }
                            </script>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="center">
                                        <u>Live Camera</u>
                                    </td>
                                    <td></td>
                                    <td align="center">
                                        <u>Captured Picture</u>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="webcam1">
                                        </div>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Image ID="imgCapture1" runat="server" Style="visibility: hidden; width: 320px; height: 240px" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Button ID="btnCapture1" Text="Capture" runat="server" OnClientClick="return Capture();" />
                            <br />
                            <span id="camStatus1"></span>
                        </div>--%>
                        <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="27" 
                    OnClientClick="if (!Page_ClientValidate()){ return false; } else if(!chkIdProffName()){ return false; } this.disabled = true;" UseSubmitBehavior="false" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="form-btn" TabIndex="28" OnClick="btnClear_Click" />
                <%-- <asp:Button ID="btnView" runat="server"  Text="View" CssClass="form-btn" OnClick="btnview_Click" />--%>
             </center>
                    </div>



                    <div id="divsearch" runat="server" visible="false">
                        <div class="form-header">
                            <h4 style="float: left;">&#10148; Search Category
                <div style="float: right; padding-right: 10px;">
                </div>

                            </h4>
                        </div>


                        <div class="form-panel">
                            <table>
                                <tr>
                                    <th>Form Date</th>
                                    <th>To Date</th>
                                    <th>Category</th>
                                    <th>Search by</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="29"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="30"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="31">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Member_ID">Member_ID</asp:ListItem>
                                            <asp:ListItem Value="First Name">First Name</asp:ListItem>
                                            <asp:ListItem Value="Last Name">Last Name</asp:ListItem>
                                            <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                            <asp:ListItem Value="Gender">Gender</asp:ListItem>
                                            <asp:ListItem Value="Status">Status</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="True" TabIndex="32"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" TabIndex="33" OnClick="btnSearch_Click" Text="Search" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" CssClass="form-btn" TabIndex="34" Text="Date with category" />
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <%--End Other Details --%>

                        <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 50px;">

                            <asp:GridView ID="gvMember" runat="server" AutoGenerateColumns="false"
                                DataKeyNames="Member_AutoID" EmptyDataText="No record found." Width="2000px" Font-Size="11"
                                PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3" AllowPaging="True"
                                PageSize="20" OnPageIndexChanging="gvMember_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command1"
                                                CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="35"
                                                Style="background-image: url('../NotificationIcons/edit.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="35"
                                                CommandArgument='<%#Eval("Member_AutoID")%>' OnCommand="btnDelete_Command1"
                                                Style="background-image: url('../NotificationIcons/f-cross_256-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%--<asp:TemplateField HeaderText="Equipment ID" ControlStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="txtstorid" runat="server" Text='<%#Eval("Member_ID") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblstorid" runat="server" Width="40px" Text='<%#Eval("Equipment_ID") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>--%>


                                    <asp:BoundField HeaderText="Member ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                    <%--<asp:BoundField HeaderText="Regis Date" DataField="RegDate" HeaderStyle-HorizontalAlign="left" />--%>
                                    <asp:BoundField HeaderText="First Name" DataField="FName" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Last Name" DataField="LName" HeaderStyle-HorizontalAlign="left" />
                                    <%--<asp:BoundField HeaderText="Date of Birth" DataField="DOB" HeaderStyle-HorizontalAlign="left" />--%>
                                    <asp:TemplateField HeaderText="Reg_Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <%# Eval("RegDate","{0:dd-MM-yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- <asp:BoundField HeaderText="DOB" DataField="DOB"  HeaderStyle-HorizontalAlign="Center" />--%>

                                    <asp:TemplateField HeaderText="DOB" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <%# Eval("DOB","{0:dd-MM-yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Anniversary Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                        <ItemTemplate>
                                            <%# Eval("AniversaryDate","{0:dd-MM-yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Marital Status" DataField="MariatalStatus" HeaderStyle-HorizontalAlign="left" />
                                    <%--   <asp:BoundField HeaderText="Aniversary Date" DataField="AniversaryDate" HeaderStyle-HorizontalAlign="left" />--%>
                                    <asp:BoundField HeaderText="Gender" DataField="Gender" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Contact1 " DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Contact2" DataField="Contact2" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Whatsapp No" DataField="WhatsAppNo" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Email ID" DataField="Email" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="BloodGroup" DataField="BloodGroup" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Health Details" DataField="HealthDetails" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Occupation" DataField="Occupation" HeaderStyle-HorizontalAlign="left" />

                                    <asp:BoundField HeaderText="ID Proof" DataField="IDProofName" HeaderStyle-HorizontalAlign="left" />
                                    <%--<asp:BoundField HeaderText="Branch LogoPath" DataField="BranchLogoPath" HeaderStyle-HorizontalAlign="left" />--%>
                                    <asp:BoundField HeaderText="Access Card No" DataField="AccessCardNo" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Block Status" DataField="BlockStatus" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="SMS Status" DataField="SMSStatus" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Executive Name" DataField="ExecutiveName" HeaderStyle-HorizontalAlign="left" />


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
                    <%--<asp:PostBackTrigger ControlID="txtContact1" />--%>
                    <asp:PostBackTrigger ControlID="txtContact1"  />
                </Triggers>               
            </asp:UpdatePanel>
        </div>
    </div>


</asp:Content>
