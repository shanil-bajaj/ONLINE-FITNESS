<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SMSLogin.aspx.cs" Inherits="NDOnlineGym_2017.SMSLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <script src="JS/OfflineJavaScript.js"></script>

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
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
                <h3>SMS Login
                 <div class="navigation">
                     <ul>
                         <li>File &nbsp; > &nbsp;</li>
                         <li>SMS  &nbsp; > &nbsp;</li>
                         <li>SMS Login</li>
                     </ul>
                 </div>
                </h3>
            </div>

            <div class="divForm">

                <%--SMS Details--%>

                <div class="form-header">
                    <h4>&#10148; SMS Login  </h4>
                </div>

                <div class="form-panel">
                    <table style="width: 100%;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Username</span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtUsername" runat="server" CssClass="txt" TabIndex="1" AutoCompleteType="Disabled" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtUsername"
                                                ErrorMessage="Enter Username" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Password</span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="txt" TabIndex="2" AutoCompleteType="Disabled" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtPassword"
                                                ErrorMessage="Enter Password" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Sender ID </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtSenderID" runat="server" CssClass="txt" TabIndex="3" AutoCompleteType="Disabled" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSenderID" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtSenderID"
                                                ErrorMessage="Enter Sender ID" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>

                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Route </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtRoute" runat="server" CssClass="txt" TabIndex="4" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRoute" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtRoute"
                                                ErrorMessage="Enter Route" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Status</span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" TabIndex="5">
                                                <asp:ListItem Value="ON">ON</asp:ListItem>
                                                <asp:ListItem Value="OFF">OFF</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Auto Status</span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:DropDownList ID="ddlSMSStatus" runat="server" CssClass="ddl" TabIndex="6">
                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>

                        <tr>


                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">SMS With Name</span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:DropDownList ID="ddlSMSWithName" runat="server" CssClass="ddl" TabIndex="7">
                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                    </table>
                    <%--End SMS others Details--%>
                    <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Edit" CssClass="form-btn" ValidationGroup="a" TabIndex="8" OnClick="btnSave_Click"
                    OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" />             
                
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="9" OnClick="btnClear_Click" />

                <asp:Button ID="btnSMSWebsite" runat="server" Text="SMS Website" CssClass="form-btn" TabIndex="10" OnClientClick="window.open('http://173.45.76.226/')" />
             </center>
                </div>
</div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
