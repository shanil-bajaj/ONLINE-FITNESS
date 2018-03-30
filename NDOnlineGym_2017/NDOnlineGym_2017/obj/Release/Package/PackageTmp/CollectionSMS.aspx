<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="CollectionSMS.aspx.cs" Inherits="NDOnlineGym_2017.CollectionSMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
    <link href="CSS/Form.css" rel="stylesheet" />
    <script src="JS/OfflineJavaScript.js"></script>
    <script src="JS/Enquiry.js"></script>

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>

    <script type="text/javascript">
        
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
                    else if (i == 10 || i == 22 || i == 34 || i == 46 || i == 58 || i == 70 || i == 82 || i == 94 || i == 106 || i == 118
                        || i == 130 || i == 142 || i == 154) {
                        txt.value += ", ";
                    }
                }

                return true;
            }

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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
            <div class="form-name-header">
                    <h3>Collection SMS
                         <div class="navigation" >
                            <ul>
                                <li>File &nbsp; > &nbsp;</li>
                                <li>SMS  &nbsp; > &nbsp;</li>
                                <li>Collection SMS</li>
                            </ul>
                         </div>
                    </h3>       
            </div>

            <div class="divForm">
                <div class="form-header">
                    <h4>&#10148; Collection SMS </h4>
                </div>
                <div class="form-panel" >  
                    <center>  
                        <table style="width:100%; text-align:center">
                               
                            <tr>
                                <td class="cols" style="text-align:center;" >
                                    <table><tr>
                                        <th> Mobile No : </th>
                                        <td style="width:55%;text-align:center;">
                                            <asp:TextBox ID="txtCollectionSMS" runat="server" CssClass="txt" onkeypress="return validate(event,this)"
                                                TabIndex="1" Width="150px" ></asp:TextBox>
                           
                                        </td>
                                    </tr></table>
                                </td>
                            </tr>

                           <tr>
                                <td class="cols" style="text-align:center;" >
                                    <table><tr>
                           
                                        <td >
                                          <asp:Button ID="btnSendDailyCollection" runat="server" Text="Send Daily Collection" CssClass="form-btn" TabIndex="2" width="200px" 
                                             OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnSendDailyCollection_Click"  />
                           
                                        </td>
                                        <td >
                                          <asp:Button ID="btnSendMonthlyCollection" runat="server" Text="Send Monthly Collection" CssClass="form-btn" TabIndex="3" width="200px"
                                              OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnSendMonthlyCollection_Click"  />
                           
                                        </td>
                           
                                    </tr></table>
                                </td>
                            </tr>
 
                  
                        </table>
                    </center>
                </div>
            </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
