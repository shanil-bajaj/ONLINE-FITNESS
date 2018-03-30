<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EmailLogin.aspx.cs" Inherits="NDOnlineGym_2017.EmailLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
     <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <script>
        function open123() {
            window.open("http://173.45.76.226", "_newwindow");
        }
    </script>
    <style>
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
     <div class="form-name-header">
            <h3>Email Login
                 <div class="navigation" >
                    <ul>
                        <li>File &nbsp; > &nbsp;</li>
                        <li>Email  &nbsp; > &nbsp;</li>
                        <li>Email Login</li>
                    </ul>
                 </div>
            </h3>       
    </div>
    <div class="divForm">
       <div class="form-header">
            <h4>&#10148; Email Login </h4>
        </div>

     <div class="form-panel">
            <table style="width:100%;">
                           <tr>                              

                               <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Email ID </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtUsername1" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvUsername" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtUsername1"
                                ErrorMessage="Enter Email ID"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="ErrorBox" runat="server" ErrorMessage="Enter Valid Email-ID"  
                                  ControlToValidate="txtUsername1"    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="a"></asp:RegularExpressionValidator>
                            </td>
                         </tr></table>
                                   </td>
                               <td></td>
                               <td></td>
                                   
               </tr>
                <tr>
                    
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>  Email Password </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtPassword1" runat="server" CssClass="txt" TabIndex="2" TextMode="Password" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvtxtOPassword" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtPassword1"
                                ErrorMessage="Enter Password "  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                        </td>
                    
                        </tr>
                </table>
                </div>
    <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn"  ValidationGroup="a" TabIndex="3" OnClick="btnSave_Click"  OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" />
               
             </center>   
    </div>
        </div>
</asp:Content>
           