<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="NDOnlineGym_2017.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
        .ErrorBox  { position:relative; z-index:1; font-weight:normal; border-radius:3px; box-shadow: 10px 10px 10px rgba(0, 0, 0, 0.5);
                        padding: 4px 10px; color: #a94442; background-color: #f2dede; border: 1px solid #ebccd1; }
     .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
    <div class="form-name-header">
            <h3>Change Password
                 <div class="navigation" >
                    <ul>
                        <li>File &nbsp; > &nbsp;</li>
                        <li>User &nbsp; > &nbsp;</li>
                        <li> Change Password </li>
                    </ul>
                 </div>
            </h3>
  </div>
    
   
    <div class="divForm">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
            <%--<ContentTemplate>--%>
    <%--Personal Details--%>

        <div class="form-header">
            <h4>&#10148; Change Password  </h4>
        </div>
        <div class="form-panel">
            <table style="width:100%;">
               <tr>
                   <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Username </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="txt" TabIndex="1" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvUsername" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtUsername"
                                ErrorMessage="Enter Username"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                   </td>
             
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Old Password </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="txt" TabIndex="2" OnTextChanged="txtOldPassword_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvtxtOldPassword" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtOldPassword"
                                ErrorMessage="Enter Old Password "  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                        </td>
                       
                    
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> New Password </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txt" TabIndex="3" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvtxtNewPassword" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtNewPassword"
                                ErrorMessage="Enter New Password"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                               </tr>
                <tr>
                    <td class="cols">
                         <table><tr>
                            <td style="width:55%;"><span class="lbl"><span class="error">*</span>Confirm Password</span></td>
                            <td style="width:55%;text-align:left;">
                             <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txt" TabIndex="4"></asp:TextBox>
                                 <asp:RequiredFieldValidator  ID="rfvtxtConfirmPassword" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtConfirmPassword"
                                ErrorMessage="Enter Confirm Password"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                <br/>
                               <asp:CompareValidator ID="cmpfvtxtConfirmPassword" runat="server"  Display="Dynamic" CssClass="ErrorBox"  ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" 
                                    ErrorMessage="Password Does Not Match." ValidationGroup="a"></asp:CompareValidator>

                            </td>
                         </tr></table>
                    </td>
                  
                   
                </tr>
            </table>
        </div>

    <%--End Personal Details--%>`
             <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn"  ValidationGroup="a" TabIndex="5" OnClick="btnSave_Click"
                    OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" />
                <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="6" OnClick="btnCancle_Click"/>
             </center>           
        
    <%--</ContentTemplate>--%>
            <%--</asp:UpdatePanel>--%>
   </div>
        
          </div>
    </asp:Content>
