<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SendSMSNew.aspx.cs" Inherits="NDOnlineGym_2017.SendSMSNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <script src="JS/OfflineJavaScript.js"></script>
    <script src="JS/Enquiry.js"></script>

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script type="text/javascript">
        function CountCharacters(txtMsg, indicator) {
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = chars;

        }

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
    <style>
        .divMemberInformation {
            padding: 5px;
            width: 1000px;
        }

        .divHeader {
            width: 100%;
            height: 25px;
        }

        .btn-information-section {
            padding: 7px 10px;
            border: none;
            width: 20%;
            float: left;
            font-weight: bold;
            font-size: 14px;
            border-bottom: 1px solid black;
            background-color: silver;
        }

        .btn-information-section-selected {
            border-bottom: none;
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-top: 1px solid black;
            background-color: rgb(128, 128, 128);
        }

        .divContent {
            width: 100%;
            height: 320px;
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-bottom: 1px solid black;
            margin-top: -10px;
        }

        .Information {
            width: 100%;
            font-size: 14px;
            padding-top: 30px;
            padding-left: 20px;
            padding-bottom: 30px;
        }

            .Information td {
                padding: 5px 10px;
            }
             .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="sc">
    <div class="form-name-header">
        <h3>Send SMS
            <div class="navigation">
                <ul>
                    <li>File &nbsp; > &nbsp;</li>
                    <li>SMS &nbsp; > &nbsp;</li>
                    <li>Send SMS</li>
                </ul>
            </div>
        </h3>
    </div>


    <div class="divForm">

        <div class="form-panel">
            <table>
                <tr>
                    <td>
                        <div class="divMemberInformation">
                            <div class="divHeader">
                                <asp:Button ID="btnIndividual" runat="server" Text="Individual" CssClass="btn-information-section btn-information-section-selected" OnClick="btnIndividual_Click" />
                                <asp:Button ID="btnGroup" runat="server" Text="Group" CssClass="btn-information-section" OnClick="btnGroup_Click" />
                                <asp:Button ID="btnEnquiry" runat="server" Text="Enquiry" CssClass="btn-information-section" OnClick="btnEnquiry_Click" />
                                <asp:Button ID="btnBalance1" runat="server" Text="Balance" CssClass="btn-information-section" OnClick="btnBalance_Click1" />
                                <asp:Button ID="btnGender1" runat="server" Text="Gender" CssClass="btn-information-section" OnClick="btnGender_Click1" />
                            </div>
                            <div class="divContent">
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="View1" runat="server">
                                        <h4 class="msgHeading">Send SMS To Individual Mobile Number</h4>
                                        <table style="width: 100%;">

                                            <tr>
                                                <td class="cols">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Name</span></td>
                                                            <td style="width: 55%; text-align: left;">
                                                                <asp:DropDownList ID="ddlName" runat="server" CssClass="ddl" TabIndex="1" AutoPostBack="true" ValidationGroup="Individual"
                                                                    OnSelectedIndexChanged="ddlName_SelectedIndexChanged">
                                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlName"
                                                                    ErrorMessage="Select Name" SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="Individual"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>

                                                <td class="cols">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Contact Number</span></td>
                                                            <td style="width: 55%; text-align: left;">
                                                                <asp:TextBox ID="txtContactnum" runat="server" CssClass="txt numericOnly" TabIndex="2" pattern="[0-9]*" MaxLength="10"
                                                                    OnTextChanged="txtContactnum_TextChanged"  onkeypress="return RestrictSpaceSpecial(event);" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtContactnum"
                                                                    ErrorMessage="Enter Contact Number" SetFocusOnError="true" ValidationGroup="Individual"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>

                                                <td class="cols"></td>

                                            </tr>

                                        </table>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 14%; vertical-align: top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>
                                                <td style="width: 85.9%;">
                                                    <asp:TextBox ID="txtIndividual" runat="server" CssClass="txt1" TabIndex="3" TextMode="MultiLine" onkeyup="CountCharacters(this,'lblIndividual');" style="resize:none" ></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvtxtIndividual" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                                                        ControlToValidate="txtIndividual" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="Individual"></asp:RequiredFieldValidator>

                                                    <%-- <label id="" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>--%>
                                                    <h4>Count :
                                                        <label id="lblIndividual">0</label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="width: 50%;">
                                                    <asp:Button ID="btnSendIndividual" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="Individual" TabIndex="4"
                                                        OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" OnClick="btnSendIndividual_Click" />
                                                    <asp:Button ID="btnClearIndi" runat="server" Text="Clear" CssClass="form-btn" TabIndex="5" OnClick="btnClearIndi_Click" />
                                                </td>
                                            </tr>
                                        </table>

                                    </asp:View>
                                    <asp:View ID="View2" runat="server">
                                        <h4>Common SMS Send To Group Members</h4>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="cols">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Status</span></td>
                                                            <td style="width: 55%; text-align: left;">
                                                                <asp:DropDownList ID="ddlGroupStatus" runat="server" CssClass="ddl" TabIndex="6" ValidationGroup="GroupStatus">
                                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="All">All</asp:ListItem>
                                                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                                                    <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvddlGroupStatus" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlGroupStatus"
                                                                    ErrorMessage="Select Group" SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="GroupStatus"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 15%; vertical-align: top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>
                                                <td style="width: 85%;">
                                                    <asp:TextBox ID="txtGroupSMS" runat="server" CssClass="txt1" TabIndex="7" TextMode="MultiLine" onkeyup="CountCharacters(this,'lblCharCntGroup');" style="resize:none"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvtxtGroupSMS" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                                                        ControlToValidate="txtGroupSMS" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="GroupStatus"></asp:RequiredFieldValidator>

                                                    <%-- <span> Number of Characters:
                                                        <label id="lblCharCntGroup" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>     --%>
                                                    <h4>Count :
                                                        <label id="lblCharCntGroup">0</label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="width: 50%;">
                                                    <asp:Button ID="btnSendGropuSMS" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="GroupStatus" TabIndex="8" 
                                                        OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" OnClick="btnSendGropuSMS_Click" />
                                                    <asp:Button ID="btnClearGroup" runat="server" Text="Clear" CssClass="form-btn" TabIndex="9" OnClick="btnClearGroup_Click" />
                                                </td>
                                            </tr>


                                        </table>
                                    </asp:View>
                                    <asp:View ID="View3" runat="server">
                                        <h4>Common SMS Send To Enquiry Members</h4>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="cols">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Status</span></td>
                                                            <td style="width: 55%; text-align: left;">
                                                                <asp:DropDownList ID="ddlEnquiry" runat="server" CssClass="ddl" TabIndex="10" ValidationGroup="Enquiry">
                                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="All">All</asp:ListItem>
                                                                    <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                                                    <asp:ListItem Value="Cold">Cold</asp:ListItem>
                                                                    <asp:ListItem Value="Expected">Expected</asp:ListItem>
                                                                    <asp:ListItem Value="Not Interested">Not Interested</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvddlEnquiry" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlEnquiry"
                                                                    ErrorMessage="Select Enquiry" SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="Enquiry"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 15%; vertical-align: top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>
                                                <td style="width: 85%;">
                                                    <asp:TextBox ID="txtEnquiry" runat="server" CssClass="txt1" TabIndex="11" TextMode="MultiLine" onkeyup="CountCharacters(this,'lblCharCntEnquiry');" style="resize:none"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvtxtEnquiry" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                                                        ControlToValidate="txtEnquiry" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="Enquiry"></asp:RequiredFieldValidator>

                                                    <%-- <span> Number of Characters:
                            <label id="lblCharCntEnquiry" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>  --%>

                                                    <h4>Count :
                                                        <label id="lblCharCntEnquiry">0</label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="width: 50%;">
                                                    <asp:Button ID="btnSendEnquiry" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="Enquiry" TabIndex="12" 
                                                        OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" OnClick="btnSendEnquiry_Click" />
                                                    <asp:Button ID="btnClearEnquiry" runat="server" Text="Clear" CssClass="form-btn" TabIndex="13" OnClick="btnClearEnquiry_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="View4" runat="server">
                                        <h4>Common SMS Send To Balance Payment Members</h4>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 15%; vertical-align: top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>
                                                <td style="width: 85%;">
                                                    <asp:TextBox ID="txtContentBalance" runat="server" CssClass="txt1" TabIndex="14" TextMode="MultiLine" ValidationGroup="Balance" onkeyup="CountCharacters(this,'lblCharCntBalance');" style="resize:none"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvtxtContentBalance" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                                                        ControlToValidate="txtContentBalance" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="Balance"></asp:RequiredFieldValidator>

                                                    <%--  <span> Number of Characters:
                                                        <label id="lblCharCntBalance" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>--%>

                                                    <h4>Count :
                                                        <label id="lblCharCntBalance">0</label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="width: 50%;">
                                                    <asp:Button ID="Button1" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="Balance" TabIndex="15" 
                                                        OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" OnClick="btnBalance_Click" />
                                                    <asp:Button ID="btnClearBalance" runat="server" Text="Clear" CssClass="form-btn" TabIndex="16" OnClick="btnClearBalance_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="View5" runat="server">
                                        <h4>Common SMS Send To Group Members</h4>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="cols">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Gender</span></td>
                                                            <td style="width: 55%; text-align: left;">
                                                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="ddl" TabIndex="17">
                                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="All">All</asp:ListItem>
                                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvddlGender" runat="server" ErrorMessage="Select Gender" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorBox"
                                                                    ControlToValidate="ddlGender" InitialValue="--Select--" ValidationGroup="Gender"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                        </table>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 15%; vertical-align: top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>
                                                <td style="width: 85%;">
                                                    <asp:TextBox ID="txtGender" runat="server" CssClass="txt1" TabIndex="18" TextMode="MultiLine" ValidationGroup="Gender" onkeyup="CountCharacters(this,'lblGender');" style="resize:none"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvtxtGender" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                                                        ControlToValidate="txtGender" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="Gender"></asp:RequiredFieldValidator>

                                                    <%-- <span> Number of Characters:
                            <label id="lblGender" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>  --%>
                                                    <h4>Count :
                                                        <label id="lblGender">0</label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="width: 50%;">
                                                    <asp:Button ID="Button2" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="Gender" TabIndex="19"
                                                        OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" OnClick="btnGender_Click" />
                                                    <asp:Button ID="btnClearGender" runat="server" Text="Clear" CssClass="form-btn" TabIndex="20" OnClick="btnClearGender_Click" />
                                                </td>
                                            </tr>

                                        </table>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>

        </div>

        <center><span class="error" style="font-size:14px;font-weight: bold;">Note:</span>       
        <span class="error" style="font-size:14px;">Length for 1 SMS 160 Character.</span><br />
        <span class="error" style="font-size:14px;padding-left:78px;">Send SMS when Internet is Connected.</span> </center>
    </div>
          </div>
</asp:Content>
