<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddEnquiry.aspx.cs" Inherits="NDOnlineGym_2017.AddEnquiry" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <style>
        .GridView1 {
            width: max-content;
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

        .txtTime {
            border: 1px solid silver;
            padding-left: 5px;
            float: left;
            width: 173px;
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
        .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="JS/Common-Script.js"></script>
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
        // Check Email validation
        function IsValidEmail(email) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(email);
        };
        function ValidateEmail() {
            var email = document.getElementById('<%= txtEmail.ClientID %>');     
            
            if (email.value != "") {
                if (!IsValidEmail(email.value)) {
                    toastr.error('Invalid Email Id !!!', 'Error');
                    email.focus();
                }
            }
        }
        // Rating Change Event
        function ddlChange() {
            var _ddlRating = document.getElementById('<%= ddlRating.ClientID %>');           
            var selectedText = _ddlRating.options[_ddlRating.selectedIndex].value;
            if (selectedText != "Not Interested") {
                document.getElementById('<%= txtNextFollowupDate.ClientID %>').disabled = false;                
            }
            else {
                document.getElementById('<%= txtNextFollowupDate.ClientID %>').disabled = true;
            }
        }
        //Disable enable executive dropdown on check box
        function ChExecutive(){
            var _chkExecutive = document.getElementById('<%= chkExecutive.ClientID %>');            
            document.getElementById('<%= ddlExecutive.ClientID %>').disabled = _chkExecutive.checked;
        }
    </script>


    <script src="http://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">

        function UserOrEmailAvailability() { //This function call on text change.           
            $.ajax({
                type: "POST",
                url: "AddEnquiry.aspx/CheckEmail", // this for calling the web method function in cs code.
                data: '{contact: "' + $("#<%=txtContact1.ClientID%>")[0].value + '" }',// user name or email value
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response);
                }
            });
        }

        // function OnSuccess
        function OnSuccess(response) {
           // var msg = $("#<%=Label2.ClientID%>")[0];
            switch (response.d) {
                case "true":
                    toastr.error('Contact is Already Exists !!!', 'Error');
                    $("#<%=txtContact1.ClientID%>").focus();
                    break;
                //case "false":
                //    msg.style.display = "block";
                //    msg.style.color = "green";
                //    msg.innerHTML = "Contact is Available You Can Use it";
                //    break;
            }
        }

    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="sc">
            <div class="form-name-header" id="divAddEnq" runat="server">
                <h3>Add Enquiry
                 <div class="navigation">
                     <ul>
                         <li>Enquiry &nbsp; > &nbsp;</li>
                         <li>Add Enquiry </li>
                     </ul>
                 </div>                    
                    <h3></h3>
                </h3>
            </div>

            <div class="form-name-header" id="divEnquiryDetails" runat="server" visible="false">
                <h3>Enquiry Details
                 <div class="navigation">
                     <ul>                         
                         <li>Enquiry &nbsp; > &nbsp;</li>
                         <li>Enquiry Details </li>
                     </ul>
                 </div>
                    <h3></h3>
                </h3>
            </div>

            <div class="form-name-header" id="divEnqFoll" runat="server" visible="false">
                <h3>Enquiry Followup
                 <div class="navigation">
                     <ul>
                         <li>Followup &nbsp; > &nbsp;</li>
                         <li>Enquiry Followup &nbsp; > &nbsp;</li>
                     </ul>
                 </div>
                    <h3></h3>
                </h3>
            </div>

            <div class="divForm">
                <div id="divFormDetails" runat="server">
                    <%--Personal Details--%>
                    <div class="form-header">
                        <h4>&#10148; Personal Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Enquiry ID </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtEnquiryId" runat="server" CssClass="txt" TabIndex="1" OnTextChanged="txtEnquiryId_TextChanged" AutoPostBack="true"
                                                    onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtEnquiryId"
                                                    ErrorMessage="Enter ID " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Enquiry Date </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtEnqDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtEnqDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtEnqDate_CalendarExtender" TargetControlID="txtEnqDate" Format="dd-MM-yyyy" />
                                                <asp:RequiredFieldValidator ID="rfvRegDate" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtEnqDate"
                                                    ErrorMessage="Enter Date " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 57.5%;"><span class="lbl">Enquiry Time </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtEnqTime" runat="server" CssClass="txtTime" TabIndex="3" TextMode="Time"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> First Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" TabIndex="4" onChange="javascript:capFirst(this);" AutoCompleteType="Disabled"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtFirstName"
                                                    ErrorMessage="Enter First Name " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Last Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" TabIndex="5" onChange="javascript:capFirst(this);" AutoCompleteType="Disabled"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Gender</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="ddl" TabIndex="6">
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
                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">DOB </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtDOB" runat="server" CssClass="txt" TabIndex="7"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    BehaviorID="txtDOB_CalendarExtender" TargetControlID="txtDOB" Format="dd-MM-yyyy" />

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
                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                <asp:TextBox ID="txtContact1" runat="server" CssClass="txt" TabIndex="8" onchange="UserOrEmailAvailability();" 
                                                    onkeypress="return RestrictSpaceSpecial(event);" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                                                <%--<asp:TextBox ID="txtContact1" runat="server" CssClass="txt" TabIndex="8" OnTextChanged="txtContact1_TextChanged" AutoPostBack="true"
                                                    onkeypress="return RestrictSpaceSpecial(event);" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>--%>
                                                <asp:RequiredFieldValidator ID="rfvContact" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtContact1"
                                                    ErrorMessage="Enter Contact " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Contact 2</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtContact2" runat="server" CssClass="txt" TabIndex="9" AutoCompleteType="Disabled"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Whats App No</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtWhatsAppNo" runat="server" CssClass="txt" TabIndex="10" AutoCompleteType="Disabled"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Email ID</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <%--<asp:TextBox ID="txtEmail" runat="server" CssClass="txt" TabIndex="11" OnTextChanged="txtEmail_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" TabIndex="11" onchange='ValidateEmail();' AutoCompleteType="Disabled"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="margin-left: 88px;">
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Address </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="txt-add" TextMode="MultiLine" Rows="3" TabIndex="12"
                                                    onChange="javascript:capFirst(this);" Style="resize: none;"></asp:TextBox>
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
                                            <td style="width: 45%;"><span class="lbl">Enquiry Type</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlEnquiryType" runat="server" CssClass="ddl" TabIndex="13">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>

                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvEnquiryType" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlEnquiryType"
                                                ErrorMessage="Select Enquiry Type" SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Enquiry For</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlEnquiryFor" runat="server" CssClass="ddl" TabIndex="14">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>

                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlEnquiryFor"
                                                ErrorMessage="Select Enquiry For " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Source Of Enquiry</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlSourceOfEnquiry" runat="server" CssClass="ddl" TabIndex="15">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>

                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="rfvSourceOfEnquiry" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlSourceOfEnquiry"
                                                ErrorMessage="Select Source" SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Reference Details</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtReferennceDetails" runat="server" CssClass="txt" TabIndex="16" onChange="javascript:capFirst(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Rating</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <%--<asp:DropDownList ID="ddlRating" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlRating_SelectedIndexChanged" AutoPostBack="true" TabIndex="17">--%>
                                                <asp:DropDownList ID="ddlRating" runat="server" CssClass="ddl" onChange="ddlChange();" TabIndex="17">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                                    <asp:ListItem Value="Cold">Cold</asp:ListItem>
                                                    <asp:ListItem Value="Warm">Warm</asp:ListItem>
                                                    <asp:ListItem Value="Expected">Expected</asp:ListItem>
                                                    <asp:ListItem Value="Not Interested">Not Interested</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvStatus" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlRating"
                                                    ErrorMessage="Select Rating " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Call Respond</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlCallResponde" runat="server" CssClass="ddl" TabIndex="18">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <%--<asp:ListItem Value="Call not Receive">Call not Receive</asp:ListItem>
                                                <asp:ListItem Value="Busy">Busy</asp:ListItem>
                                                <asp:ListItem Value="Not Reacheble">Not Reacheble</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvddlCallResponde" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlCallResponde"
                                                ErrorMessage="Select CallRespond " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                              
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Next Followup Date</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtNextFollowupDate" runat="server" CssClass="txt" OnTextChanged="txtNettFollowupDate_TextChanged"
                                                    AutoPostBack="true" TabIndex="19"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    BehaviorID="txtNextFollowupDate_CalendarExtender" TargetControlID="txtNextFollowupDate" Format="dd-MM-yyyy" />
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
<%--                                                            <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged"
                                                                AutoPostBack="true" TabIndex="20" CssClass="chkBox" /></td>--%>
                                                            <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" TabIndex="20" CssClass="chkBox" onChange="ChExecutive();" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="21" Enabled="false">
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
                        <%--<table style="width: 100%; margin-top: 5px;">
                            <tr>
                                <td class="cols" style="padding-left: 70px;">
                                    <span style="padding-right: 10px; padding-left: 30px; font-size: 12.5px;">Image</span>
                                    <asp:FileUpload ID="fileLogo1" CssClass="btn-file" runat="server" onchange="ShowImagePreview(this);" TabIndex="23" />
                                </td>
                                <td class="cols" style="padding-left: 70px;">
                                    <div style="width: 100px; height: 80px; border: 1px solid silver;">
                                        <asp:Image ID="imgEquipment" runat="server" ImageUrl="" Style="width: 100px; height: 80px; border: 1px solid silver;"
                                            class="fileupload-preview thumbnail" TabIndex="24" />                                       
                                    </div>
                                    <asp:Button runat="server" ID="btnRemove" Text="Remove" TabIndex="25" CssClass="btn-remove" />
                                </td>
                                <td class="cols" visible="false"></td>
                            </tr>

                        </table>--%>

                        <table style="width:100%;margin-top:5px;">
                        <tr>
                            <td class="cols" style="padding-left:70px;">
                                <span style="padding-right:10px;padding-left:30px;font-size:12.5px;">Image</span>
                                <asp:FileUpload ID="fileLogo" runat="server" TabIndex="22" onchange="ShowImagePreview(this);"  CssClass="btn-file" />                                                              
                            </td>
                            <td class="cols" style="padding-left:70px;">
                                <div style="width:100px;height:80px;border:1px solid silver;">
                                    <asp:Image ID="imgMember" runat="server" style="width:100px;height:80px;border:1px solid silver;" TabIndex="23"/> 
                                </div>                                                                                                       
                                <asp:Button runat="server"  ID="btnRemove" Text="Remove"  TabIndex="24" OnClick="btnRemove_Click" CssClass="btn-remove"/>
                            </td>
                                <td class="cols" visible="false">                                    
                            </td>
                         </tr>   
               
                   </table>

                        <table>
                            <tr>
                                <td style="width:145px;text-align:right;padding-right:10px;"><span>Comment</span></td>
                                <td> 
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="txt" TabIndex="25" TextMode="MultiLine" onChange="javascript:capFirst(this);" Rows="4" 
                                        Style="resize: none;width:850px;"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" OnClick="btnSave_Click"  
                    OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" TabIndex="26" />
              <%--  <asp:Button ID="btnView" runat="server" Text="View" CssClass="form-btn" />--%>
                <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="27" OnClick="btnCancle_Click"  />
             </center>

                    </div>

                </div>
                <div id="divsearch" runat="server" visible="false">
                <div class="form-header">
                    <h4 style="float: left;">&#10148; Search Category
                    </h4>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>
                <center>
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
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="28"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="29"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                             <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" OnClick="btnSearch_Click" TabIndex="30" Text="Search" />
                       </td>
                        <td>
                            <%--<asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" TabIndex="31" AutoPostBack="true">--%>
                            <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="31">
                                <%--<asp:ListItem Value="All">All</asp:ListItem>--%>
                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                <asp:ListItem Value="Enquiry ID">Enquiry ID</asp:ListItem>
                                <asp:ListItem Value="Contact 1">Contact</asp:ListItem>
                                <asp:ListItem Value="First Name">First Name</asp:ListItem>
                                <asp:ListItem Value="Last Name">Last Name</asp:ListItem>                                
                                <asp:ListItem Value="Enquiry Type">Enquiry Type</asp:ListItem>
                                <asp:ListItem Value="Enquiry For">Enquiry For</asp:ListItem>
                                <asp:ListItem Value="Source Of Enquiry">Source Of Enquiry</asp:ListItem>
                                <asp:ListItem Value="Rating">Rating</asp:ListItem>
                                <asp:ListItem Value="Executive Name">Executive Name</asp:ListItem>

                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="true" TabIndex="32" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                       
                    </tr>

                </table>
              </center>

                <center style="margin-top: 20px;">
                    <asp:Button ID="btnEnquiryDate" runat="server" CssClass="form-btn"  TabIndex="33" Text="Enquiry Date" OnClick="btnEnquiryDate_Click" />
                    <asp:Button ID="btnFollowupDate" runat="server" CssClass="form-btn"  TabIndex="34" Text="Followup Date" OnClick="btnFollowupDate_Click" />
                    <asp:Button ID="btnEnqDtWithCategory" runat="server" CssClass="form-btn"  TabIndex="35" Text="Enq Date With Category" OnClick="btnEnqDtWithCategory_Click" />
                    <asp:Button ID="btnFollDtWithCategory" runat="server" CssClass="form-btn"  TabIndex="36" Text="Followup Date With Category" OnClick="btnFollDtWithCategory_Click" />                    
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="38" OnClick="btnClear_Click" />
                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export" CssClass="form-btn" TabIndex="39" OnClick="btnExportToExcel_Click" />
                </center>

                <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 20px;">
                    <div style="margin:10px 0px 10px 10px">
                        <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                        <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                    </div>                  
                    <asp:GridView ID="gvEnquiry" runat="server" AutoGenerateColumns="false" CellPadding="3"
                        DataKeyNames="Branch_AutoID" EmptyDataText="No record found." 
                        Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView GridView1" GridLines="None" 
                         AllowPaging="True" PageSize="20"
                        OnPageIndexChanging="gvEnquiry_PageIndexChanging" Width="1900px" >

                        <Columns>
                           <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false"  OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Enq_AutoID")%>' TabIndex="40" 
                                 style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="40"
                                        CommandArgument='<%#Eval("Enq_AutoID")%>'  OnCommand="btnDelete_Command"
                                        style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnFollowup" runat="server" CausesValidation="false" OnCommand="btnFollowup_Command" CommandArgument='<%#Eval("Enq_AutoID")%>' TabIndex="40"
                                        style="background-image:url('../NotificationIcons/arrow_top_right-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Followup"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAddMember" runat="server" CausesValidation="false"  OnCommand="btnAddMember_Command" CommandArgument='<%#Eval("Enq_AutoID")%>' TabIndex="40"
                                        style="background-image:url('../NotificationIcons/Add.png');background-size:100% 100%;padding-left:10px;padding-top:0px;padding-bottom:2px;" ToolTip="Add Member" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Enq.ID" DataField="Enq_ID1" HeaderStyle-HorizontalAlign="left" />
                            <asp:TemplateField ItemStyle-Width="70px" ControlStyle-Width="70px">
                                <HeaderTemplate>
                                    <b>Enquiry.Date</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("EnqDate","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="First Name" DataField="FName" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Last Name" DataField="LName" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Contact 1" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Comment" DataField="Comment" HeaderStyle-HorizontalAlign="left" />
                             <asp:BoundField HeaderText="Rating" DataField="Rating" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Contact 2" DataField="Contact2" HeaderStyle-HorizontalAlign="left" />
                            <%--<asp:BoundField HeaderText="DOB" DataField="DOB" HeaderStyle-HorizontalAlign="left" />--%>
                            <asp:BoundField HeaderText="Gender" DataField="Gender" HeaderStyle-HorizontalAlign="left" />
                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <b>DateOfBirth</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("DOB","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="WhatsApp No" DataField="WhatsAppNo" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="left" />
                           
                            <asp:BoundField HeaderText="Enquiry Type" DataField="Enquiry_Type" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Enquiry For" DataField="Enquiry_For" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Source Of Enquiry" DataField="Source_Enquiry" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Reference Details" DataField="ReferenceDetails" HeaderStyle-HorizontalAlign="left" />
                            <%--<asp:BoundField HeaderText="CallRespond" DataField="CallRespond" HeaderStyle-HorizontalAlign="left" />--%>
                            
                            <%--<asp:BoundField HeaderText="NextFollowupDate" DataField="NextFollowupDate" HeaderStyle-HorizontalAlign="left" />--%>
                            <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />
                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <b>NextFollowupDate</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Time" DataField="Time1" HeaderStyle-HorizontalAlign="left" />
                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <RowStyle Height="20px" />
                        <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>

                </div>
                    </div>
                <%--End Other Details--%>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
