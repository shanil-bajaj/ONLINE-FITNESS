<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="BranchInformation.aspx.cs" Inherits="NDOnlineGym_2017.BranchInformation" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script type="text/javascript">
        function capFirst(oTextBox) {
            oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
        }
    </script>
    <script  type="text/javascript">

        function AddComma(txt) {
            if (txt.value.length == 0) {
                txt.value += "";
            }

            for (var i = txt.value.length; i <= txt.value.length; i = i + 10) {
                if (i == 0) {
                    txt.value += "";
                }
                else if (i == 10 || i == 22 || i == 34 || i == 46 || i == 58) {
                    txt.value += ", ";
                }
            }
        }

 </script>
    <script>
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgBranch.ClientID%>').prop('src', e.target.result)
                };
                reader.readAsDataURL(input.files[0]);
                }
            }

    </script>
    <script type="text/javascript">
        function LimtCharacters(txtMsg, CharLength, indicator) {
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtMsg.value = txtMsg.value.substring(0, CharLength);
            }
        }
    </script>

    <script type="text/javascript">
        //Function to allow only numbers to textbox
        function validate(key, txt) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;

            //comparing pressed keycodes
            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57) && !(keycode == 32 || keycode == 44)) {
                return false;
            }
            else {
                //Condition to check textbox contains ten numbers or not
                if (txt.value.length == 0) {
                    txt.value += "";
                }

                for (var i = txt.value.length; i <= txt.value.length; i = i + 10) {
                    if (i == 0) {
                        txt.value += "";
                    }
                    else if (i == 10 || i == 21 || i == 32 || i == 43 || i == 54 || i == 65 || i == 76 || i == 87 || i == 98 || i == 109
                         || i == 120 || i == 131 || i == 142) {
                        txt.value += ",";
                    }
                }

                return true;
            }

        }
    </script>

    <script type="text/javascript">
        //Function to allow only numbers to textbox
        function validate1(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            var phn = document.getElementById('txtPhn');
            //comparing pressed keycodes
            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                return false;
            }
            else {
                //Condition to check textbox contains ten numbers or not
                if (phn.value.length < 10) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
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
         function ShowCharCount() {
             document.getElementById("lblcount").innerHTML = document.getElementById("txtTermsAndConditions").value.length;
         }
     </script>
    <style>
        .btn-remove { background-color: rgb(248, 45, 70); color: white; border: 1px solid rgb(248, 45, 70); margin-top: 3px; }
        .btn-remove:focus { border:1px solid black;cursor:pointer  }
        .btn-file:focus  { border:1px solid silver;cursor:pointer  }
         input[type="checkbox"]:focus {  border-color: #ffffcc; }
          .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
    <div class="form-name-header">
            <h3>Branch Information
                <div class="navigation" >
                    <ul>
                        <li>File &nbsp; > &nbsp;</li>
                        <li>Branch Setting &nbsp; > &nbsp;</li>
                        <li>Branch Information </li>
                    </ul>
                 </div>
                <%--<h3></h3>
                <h3></h3>--%>
            </h3>
    </div>

    <div class="divForm">

    <%--Basic Details--%>

        <div class="form-header">
            <h4>&#10148; Basic Details  </h4>
        </div>
        <div class="form-panel">
            <table style="width:100%;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Branch ID </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtBranchID" runat="server" CssClass="txt" TabIndex="1" onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvBranchID" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtBranchID"
                                ErrorMessage="Enter Branch ID"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Company Name </span></td>
                            <td style="width:55%;text-align:left;">
                             <asp:DropDownList ID="ddlCompany" runat="server" CssClass="ddl" TabIndex="2">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             <asp:RequiredFieldValidator  ID="rfvddlCompany" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlCompany"
                             ErrorMessage="Select Company Name"  SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Branch Name </span></td>
                            <td style="width:55%;text-align:left;">
                             <asp:TextBox ID="txtBranchName" runat="server" CssClass="txt" TabIndex="3" onChange="javascript:capFirst(this);" OnTextChanged="txtBranchName_TextChanged" ></asp:TextBox>
                             <asp:RequiredFieldValidator  ID="rfvBranchName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtBranchName"
                             ErrorMessage="Enter Branch Name"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                    
                </tr>
                
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl">Status</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" TabIndex="4">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                </asp:DropDownList>
                               
                            </td>
                         </tr></table>
                    </td>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> GST No </span></td>
                            <td style="width:55%;text-align:left;">
                                 <asp:TextBox ID="txtGSTNo" runat="server" CssClass="txt" TabIndex="5"></asp:TextBox>
                            </td>
                         </tr></table>
                    </td>                  
                </tr>             
            </table>

            
        </div>

    <%--End Basic Details--%>

   

    <%--Address Details--%>

        <div class="form-header">
            <h4>&#10148; Address Details  </h4>
        </div>
        <div class="form-panel">
            <table style="width:100%;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Address 1</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="txt" TabIndex="6" onChange="javascript:capFirst(this);"></asp:TextBox>
                               <asp:RequiredFieldValidator  ID="rfvAddress1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtAddress1"
                              ErrorMessage="Enter Address 1"  SetFocusOnError="true" ValidationGroup="a" ></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Address 2</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="txt" TabIndex="7" onChange="javascript:capFirst(this);"></asp:TextBox>
                            </td>
                         </tr></table>
                    </td>
                   <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">State</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtState" runat="server" CssClass="txt" TabIndex="8" onChange="javascript:capFirst(this);"></asp:TextBox>
                            </td>
                         </tr></table>
                    </td>
                  
                </tr>
                <tr>

                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>City</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="txt" TabIndex="9" onChange="javascript:capFirst(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvtxtCity" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtCity"
                              ErrorMessage="Enter City"  SetFocusOnError="true" ValidationGroup="a" ></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                  </td>
                  <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Location</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtLocation" runat="server" CssClass="txt" TabIndex="10" onChange="javascript:capFirst(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvtxtLocation" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtLocation"
                              ErrorMessage="Enter Location"  SetFocusOnError="true" ValidationGroup="a" ></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                
                
               
               </tr>
            </table>

           
        </div>
    <%--End Address Details--%>

     <%--Contact Details--%>

        <div class="form-header">
            <h4>&#10148; Contact Details  </h4>
        </div>
        <div class="form-panel">
            <table style="width:100%;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Contact 1 </span></td>
                            <td style="width:55%;text-align:left;">
                              <asp:TextBox ID="txtContact1" runat="server" CssClass="txt" TabIndex="11" onkeypress="return validate(event,this)" AutoCompleteType="Disabled"></asp:TextBox>
                              <asp:RequiredFieldValidator  ID="rfvContact" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtContact1"
                              ErrorMessage="Enter Contact 1"  SetFocusOnError="true" ValidationGroup="a" ></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Contact 2</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtContact2" runat="server" CssClass="txt" TabIndex="12" onkeypress="return validate(event,this)" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                         </tr></table>
                    </td>
                   <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Land Line</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtLandLine" runat="server" CssClass="txt" TabIndex="13"></asp:TextBox>
                            </td>
                         </tr></table>
                    </td>
                </tr>
                <tr>
                     <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Email ID</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="txt" TabIndex="14"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revtxtEmailID" Display="Dynamic" CssClass="ErrorBox" runat="server"
                                    ControlToValidate="txtEmailID"
                              ErrorMessage="Enter Valid Email"  SetFocusOnError="true" ValidationGroup="a"
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*" 
                                    ></asp:RegularExpressionValidator>
                            </td>
                         </tr></table>
                    </td>
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Website</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="txt" TabIndex="15"></asp:TextBox>
                            </td>
                         </tr></table>
                    </td>
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Collection SMS No</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtCollectionSMSNo" runat="server" CssClass="txt" onkeypress="return validate(event,this)" AutoCompleteType="Disabled" TabIndex="16"></asp:TextBox>
                            </td>
                         </tr></table>
                    </td>
                </tr>
               
            </table>

             
            
        </div>

    <%--End Contact Details--%>
     
    <%--Tearms And Condition Details--%>
        <div class="form-header">
            <h4  style="float:left;">&#10148; Terms & Conditions Details </h4>
        </div>
        <div class="form-panel">
          <table style="width:100%;">
                 <tr>
                     <td style="width:14%;"><span class="lbl">Terms & Conditions</span>
                          
                     </td>
                      <td>                                                 
                                <asp:TextBox ID="txtTermsAndConditions" runat="server" TextMode="MultiLine" TabIndex="17" Width="830" Height="90px" 
                                     onkeyup="LimtCharacters(this,2000,'lblcount');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Number of Characters Left:
                                    <label id="lblcount" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label>                                
                            </td>   

                     
                               
                 </tr>   
               
           </table>
           <table style="width:100%;margin-top:5px;">
                 <tr>
                                <td class="cols" style="padding-left:70px;">
                                    <span style="padding-right:10px;padding-left:30px;font-size:12.5px;">Logo</span><asp:FileUpload ID="FileUploadeLogo" runat="server" TabIndex="18" onchange="ShowImagePreview(this);" CssClass="btn-file" />
                                </td>
                                <td class="cols" style="padding-left:70px;">
                                   <div style="width:100px;height:80px;border:1px solid silver;">
                                       <asp:Image ID="imgBranch" runat="server" ImageUrl="" style="width:100px;height:80px;border:1px solid silver;"
                                                class="fileupload-preview thumbnail" TabIndex="19" />
                                   </div>
                                    <asp:Button runat="server"  ID="btnRemove" Text="Remove"  TabIndex="20" OnClick="btnRemove_Click"
                                        CssClass="btn-remove" />
                                </td>
                                 <td class="cols" visible="false">
                                    
                                </td>
                 </tr>   
               
           </table>
        </div>

     <%--End Tearms And Condition Details--%>

     <%--Owner and Contact Details--%>


       <div class="form-header">
            <h4  style="float:left;">&#10148; Owner And Contact Details </h4>
        </div>
       <div class="form-panel">
            <table style="width:100%;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl">Owner Name </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtOwnername" runat="server" CssClass="txt" TabIndex="21" onChange="javascript:capFirst(this);" ></asp:TextBox>
                           </td>
                         </tr></table>
                    </td>
                   <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl">Owner Contact</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtOwnerContact" runat="server" CssClass="txt" TabIndex="22"  ></asp:TextBox>
                            </td>
                         </tr></table>
                    </td>
                   <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Owner Email ID</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtOwnerEmailID" runat="server" CssClass="txt" TabIndex="23"  ></asp:TextBox>
                             </td>
                         </tr></table>
                    </td>
                </tr> 
                 <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl">Contact Person </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="txt" TabIndex="24" onChange="javascript:capFirst(this);" ></asp:TextBox>
                           </td>
                         </tr></table>
                    </td>
                   <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl">Contact Person No</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtContactPersonNo" runat="server" CssClass="txt" TabIndex="25"  ></asp:TextBox>
                            </td>
                         </tr></table>
                    </td>
                   <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Contact Person Email</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtContactPersonEmail" runat="server" CssClass="txt" TabIndex="26"  ></asp:TextBox>
                             </td>
                         </tr></table>
                    </td>
                </tr>
                
            </table>
            
         

             <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="27" OnClick="btnSave_Click" 
                    OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false"/>
                <%--<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="28" OnClick="btnClear_Click" />--%>
                
             </center>
            
           
        </div>
        <%--<div id="divsearch" runat="server">
        <div class="form-header">
               <h4  style="float:left;">&#10148; Search Category
            </h4>
        </div>
                <table style="width:100%;">
                <tr>
                    <td style="width:20%;">
                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="29">
                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                            <asp:ListItem Value="Branch ID">Branch ID</asp:ListItem>
                            <asp:ListItem Value="Branch Name">Branch Name</asp:ListItem>
                            <asp:ListItem Value="Contact 1">Contact 1</asp:ListItem>
                            <asp:ListItem Value="Contact 2">Contact 2</asp:ListItem>
                            <asp:ListItem Value="State">State</asp:ListItem>
                            <asp:ListItem Value="City">City</asp:ListItem>
                            <asp:ListItem Value="Owner Name">Owner Name</asp:ListItem>
                            <asp:ListItem Value="Owner Contact">Owner Contact</asp:ListItem>
                            <asp:ListItem Value="Status">Status</asp:ListItem>
                            <asp:ListItem Value="Company ID">Company Name</asp:ListItem>
                            <asp:ListItem Value="Contact Person">Contact Person</asp:ListItem>
                            <asp:ListItem Value="Contact Person No">Contact Person No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:20%;">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="True" TabIndex="30"></asp:TextBox>
                    </td>
                    <td style="width:45%;">
                        <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" OnClick="btnSearch_Click" Text="Search" TabIndex="31"/>
                    </td>
                    </tr>
                </table>
        <div style="width:1000px;height:auto;overflow-x:scroll;margin-top:50px;">
              
                <asp:GridView ID="gvBranchInfo" runat="server" AutoGenerateColumns="false" 
                DataKeyNames="Branch_AutoID" EmptyDataText="No record found." Width="1000px" 
                    CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" AllowPaging="True" OnPageIndexChanging="gvBranchInfo_PageIndexChanging" >

                    <Columns>
                        <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit" >
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEdit_Command" 
                                CommandArgument='<%#Eval("Branch_AutoID")%>' TabIndex="32" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete" >
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')"
                                CommandArgument='<%#Eval("Branch_AutoID")%>' Text="Delete" OnCommand="btnDelete_Command" TabIndex="32"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Branch ID" DataField="Branch_ID1" HeaderStyle-HorizontalAlign="left" ControlStyle-Width="80px"/>
                    <asp:BoundField HeaderText="Branch Name" DataField="BranchName" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Contact 1" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Contact 2" DataField="Contact2" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Landline" DataField="Landline" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Website" DataField="Website" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Address 1" DataField="Address1" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Address 2" DataField="Address2" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Location" DataField="Location" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="State" DataField="State" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="City" DataField="City" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Collection SMS" DataField="CollectionSMS" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="GSTNo" DataField="GSTNo" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Owner Name" DataField="OwnerName" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Owner Contact" DataField="OwnerContact" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Owner Email" DataField="OwnerEmail" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Company Name" DataField="CompanyName" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Contact Person No" DataField="ContactPersonNo" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Contact Person Email" DataField="ContactPersonEmail" HeaderStyle-HorizontalAlign="left" />
                    
                </Columns>
                </asp:GridView>
                      
           </div>
            </div>--%>
   </div>
                </div>
            </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        </asp:UpdatePanel>
</asp:Content>

