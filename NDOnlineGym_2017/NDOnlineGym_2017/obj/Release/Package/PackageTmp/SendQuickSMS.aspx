<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SendQuickSMS.aspx.cs" Inherits="NDOnlineGym_2017.SendQuickSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>

    <style>
        .txt1 {
            width: 500px;
        }
        .auto-style1 {
            width: 14%;
        }
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>

    <script type="text/javascript">

        function AddComma(txt) {

            if (txt.value.length == 0) {
                txt.value += "";
            }

            for (var i = txt.value.length; i <= txt.value.length; i = i + 10) {
                if (i == 0) {
                    txt.value += "";
                }
                else if (i == 10 || i == 21 || i == 32 || i == 43 || i == 54 || i == 65 || i == 76 || i == 87 || i == 98) {
                    txt.value += ",";
                }
            }
        }

    </script>

    <script type="text/javascript">
        function CountCharacters(txtMsg, indicator) {
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = chars;

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              <div class="sc">
    <div class="form-name-header">
        <h3>Send Quick SMS
            <div class="navigation">
                <ul>
                    <li>File &nbsp; > &nbsp;</li>
                    <li>SMS &nbsp; > &nbsp;</li>
                    <li>Send Quick SMS</li>
                </ul>
            </div>
        </h3>
    </div>
    <div class="divForm">
        <%--Quick SMS--%>

        <div class="form-header" id="formheader1">
            <h4>&#10148; Quick SMS</h4>            
        </div>

        <div class="form-panel" id="formpanel1">

           
            <table style="width: 100%;">              
                <tr>
                    <td class="cols">
                        <table>                                                        
                            <tr>
                                <td style="width: 43.5%;"><span class="lbl"><span class="error">*</span>Contact Number</span></td>
                                
                                <td style="width: 57%; text-align: left;">                                    
                                    <%-- <asp:TextBox ID="txtContactnum" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>--%>
                                    <asp:TextBox ID="txtContactnum" runat="server" CssClass="input-txt numericOnly txt" TabIndex="1"
                                        pattern="[0-9, ]*" onkeypress="AddComma(this)" MaxLength="109" ValidationGroup="QuickSms" AutoCompleteType="Disabled" 
                                        style="margin-top:15px;margin-left:2px" ></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvTxtMobileNumber" runat="server" ErrorMessage="Enter Contact Number" SetFocusOnError="true"
                                        ControlToValidate="txtContactnum" CssClass="ErrorBox" ValidationGroup="QuickSms"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                    <td class="cols">                                 
                            </td>

                </tr>
            </table>

            <table style="width: 100%;">
                <tr>
                    <td style="width:15%; vertical-align:top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>
                    <td style="width: 85%;">                                                
                        <asp:TextBox ID="txtQuickSMS" runat="server" CssClass="txt1 txt" TabIndex="2" TextMode="MultiLine" onkeyup="CountCharacters(this,'cntCharacter');" style="resize:none"></asp:TextBox>                          
                        <br />                        
                        <asp:RequiredFieldValidator ID="rfvtxtQuickSMS" runat="server" ErrorMessage="Enter Message Here" SetFocusOnError="true"
                            ControlToValidate="txtQuickSMS" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="QuickSms"></asp:RequiredFieldValidator> 
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                    <h4>Count :
                            <label id="cntCharacter" style="background-color: #E2EEF1; color: Red; font-weight: bold;">0</label>                            
                        </h4>                                                                    
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td style="width: 85.7%;">
                        <asp:Button ID="btnQuickSMS" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="QuickSms" TabIndex="3" 
                            OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" OnClick="btnQuickSMS_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="4" OnClick="btnClear_Click" /> 
                    </td>
                </tr>
            </table>

        </div>
        <%--End Individual--%>
                <br /><br />
                <div style="margin-left:120px">   
                <span class="error" style="font-size:13px;font-weight: bold;">Note:</span>       
                <span class="error" style="font-size:13px;">Length For 1 SMS 160 Character.</span>
                <span class="error" style="font-size:13px;">Enter Maximum 10 Contact Number.</span>
                <span class="error" style="font-size:13px;">Send SMS When Internet Is Connected.</span>       
                </div>   
           
    </div>
                  </div>
            </ContentTemplate>
<%--         <Triggers>
             <asp:PostBackTrigger ControlID="btnQuickSMS" />
         </Triggers>--%>
         </asp:UpdatePanel>

</asp:Content>
