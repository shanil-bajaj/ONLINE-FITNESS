<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NDOnlineGym_2017.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <link rel="icon" type="image/png" href="Logo/NDLogo.png" />
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
<head id="Head1" runat="server">
    <title></title>
    <%--<meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />  --%>
    <style>
        *{ box-sizing: border-box; }
        body { height: 100%;margin: 0;padding: 0;color: #000000; font-family: Verdana, Geneva, sans-serif; font-size: 11px; text-align: left;}
       .bgleft {  width: 13%;  background-color:rgb(180, 180, 180);height:662px;} 
       .bgright { width: 13%; background-color: rgb(180, 180, 180);height:662px;}
       table { display: table;border-collapse: separate; border-color: grey; }
       .inp-txt { padding:3px 8px;float:left;margin-top:3px;width:250px;  }
        .inp-txt:focus { border:1px solid rgb(242, 137, 9);  }
       .divMiddle { width:100%;height:445px;bottom:0px; }
       .divLogin { width:310px;height:270px;border:1px solid silver;border-radius:3px;background-image:url('../Icon/images (2).jpg')}
       .innerDiv { width:290px;height:255px;border:1px solid silver;border-radius:3px;margin:7px;background-color:rgb(228, 228, 228); }
       .innerSubDiv { width:290px;height:253px;float:left;border:1px solid silver;border-radius:3px;float:left;margin-right:0px;
                      background-color:white;background-image:url('../Icon/Untitled19.png');background-size:cover }
       .lbl { font-size:12px;float:left  }
       .btn { padding:5px 10px;margin-top:20px;float:right;margin-right:25px;font-size:14px;  }
        .btn:focus { border:1px solid rgb(242, 137, 9); cursor:pointer  }
       .divLogoSection { width:183px;height:243px;border:1px solid silver;border-radius:3px;float:left;margin:5px;background-color:white;  }
       .footer { width:100%;height:100px;bottom:0px;background-image:url('../NotificationIcons/footerImg.png');background-size:100% 100%; }
       .header { width:100%;height:100px;bottom:0px;background-image:url('../NotificationIcons/header_bg.png');background-size:100% 100%; }
       .lblDate { color:white;font-weight:bold;font-size:11px;  }
        .btnlink:focus { color:blue;cursor:pointer }
        .btnlink:focus { color:blue;cursor:pointer }
       .btnlink{ font-size:12px;float:left;color:rgb(213, 86, 1);padding-right:25px;margin-top:20px;padding-left:7px;}
       .ErrorBox { position: absolute; z-index: 1; font-weight: normal; border-radius: 3px; box-shadow: 10px 10px 10px rgba(0, 0, 0, 0.5); padding: 4px 20px;
    color: #a94442; background-color: #f2dede; border: 1px solid #ebccd1; margin-top: 38px;margin-left: -158px; }
        .div-outer-logo { width:138px;height:87px;border:1px solid silver;margin:0px;padding:3px;margin-left:30px;margin-top:-80px;background-image:url('../Icon/images (2).jpg') }
        .div-inner-logo { width:128px;height:77px;border:1px solid silver;margin:0px;background-color:white; }
        .Company-logo { background-size:cover;width:128px;height:77px; }
        .ndlogo { height:32px }
         @media screen and (max-width: 430px) {
           
        .div-outer-logo { width:100px;height:67px;border:1px solid silver;margin:0px;padding:3px;margin-left:10px;margin-top:-100px;background-image:url('../Icon/images (2).jpg') }
        .div-inner-logo { width:90px;height:57px;border:1px solid silver;margin:0px;background-color:white; }
        .Company-logo { background-size:cover;width:90px;height:57px; }
        .lblDate { color:white;font-weight:bold;font-size:9px;  }
         .ndlogo { height:45px }
        }

    </style>

     <script type="text/javascript">
         function date_time(id) {
             date = new Date;
             year = date.getFullYear();
             month = date.getMonth();
             months = new Array('Jan', 'Feb', 'March', 'April', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec');
             d = date.getDate();
             day = date.getDay();
             days = new Array('Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat');
             //h = date.getHours();
             //if (h < 10) {
             //    h = "0" + h;
             //}
             //m = date.getMinutes();
             //if (m < 10) {
             //    m = "0" + m;
             //}
             //s = date.getSeconds();
             //if (s < 10) {
             //    s = "0" + s;
             //}

             var hours = date.getHours();
             var minutes = date.getMinutes();
             var sec = date.getSeconds();
             var ampm = hours >= 12 ? 'pm' : 'am';
             hours = hours % 12;
             hours = hours ? hours : 12; // the hour '0' should be '12'
             minutes = minutes < 10 ? '0' + minutes : minutes;
             // var strTime = hours + ':' + minutes + ' ' + ampm;

             result = '' + days[day] + ',' + ' ' + d + ' ' + months[month] + ' ' + year + ' ' + hours + ':' + minutes + ':' + sec + ' ' + ampm;
             document.getElementById(id).innerHTML = result;
             setTimeout('date_time("' + id + '");', '1000');
             return true;
         }
  </script>

     
</head>
<body>

      <form id="form1" runat="server">
     <table id="Table1" runat="server" width="100%" height="100%" cellspacing="0" cellpadding="0" border="0" >
         <tr>
             <td class="bgleft"></td>
             <td style="width:74%;vertical-align:top;background-color:white;">
                <table style="width:100%;">
                <tr>
                    <td style="width:100%;padding:0px">
                    <div class="header" >
                         
                            <div style="float:right;padding:10px;">
                               <%--   Display Time And Date--%>
                                   
                                <asp:Label ID="date_time" runat="server" Text="" CssClass="lblDate" ></asp:Label>
                                <script type="text/javascript">window.onload = date_time('date_time');</script> 
                              <%--   Display Time And Date--%>
                            </div>
                    </div>
                    <div class="div-outer-logo">
                           <div class="div-inner-logo">
                                <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="Icons/IdproofLogo.png" style="width:128px;height:77px;border:1px solid silver;"
                                                class="fileupload-preview thumbnail" />
                           </div>          
                    </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                     <div class="divMiddle" >
                         <center style="padding-top:20px;">
                                 <img src="../Icon/NDfitnessPlusGym.png" height="45" />
                             </center>
                         <center style="padding-top:15px;">
                             
                            <div class="divLogin" >
                                <div class="innerDiv" >
                                   <%-- <div class="divLogoSection">
                                         <div style="height:120px;width:120px;padding-top:20px;">
                                            <asp:Image ID="imgClientLogo" runat="server" ImageUrl="../Icon/NDLogo.png" style="height:100%;width:100%;" />
                                         </div>
                                    </div>--%>

                                    <div id="divLogin" runat="server"  class="innerSubDiv" >
                                        <div style="float:left;padding:5px 10px;margin-top:20px;">
                                            <span class="lbl" >Username </span><br />
                                            <asp:TextBox ID="txtUsername" EnableViewState="false" runat="server" CssClass="inp-txt" OnTextChanged="txtUsername_TextChanged" TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                                              <asp:RequiredFieldValidator ID="rfvtxtUsername" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtUsername"
                                                ErrorMessage="Enter Username " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </div>
                                        <div style="float:left;padding:5px 10px;margin-top:0px;">
                                            <span class="lbl">Password </span><br />
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="inp-txt" TabIndex="2" TextMode="Password"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtPassword"
                                                ErrorMessage="Enter Password " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                             <asp:LinkButton ID="btnForgotPassword" runat="server" style="" OnClick="btnForgotPassword_Click"  TabIndex="4" CssClass="btnlink">Forgot Password</asp:LinkButton>
                                        </div>
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btn" Text="Login" OnClick="btnLogin_Click"  ValidationGroup="a" TabIndex="3" />
                                       
                                    </div>
                                    
                                     <div id="divForgotPassword" runat="server" class="innerSubDiv" visible="false" >
                                        <div style="float:left;padding:5px 10px;margin-top:10px;">
                                            <span class="lbl" >Username </span><br />
                                            <asp:TextBox ID="txtForgotUsername" runat="server" CssClass="inp-txt" OnTextChanged="txtForgotUsername_TextChanged" TabIndex="1" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="rfvtxtForgotUsername" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtForgotUsername"
                                                ErrorMessage="Enter Username " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </div>
                                        <div style="float:left;padding:5px 10px;margin-top:0px;">
                                            <span class="lbl" >Email ID </span><br />
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="inp-txt" TabIndex="2"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtEmail"
                                                ErrorMessage="Enter Email" SetFocusOnError="true" ValidationGroup="a" style="margin-left:-155px"></asp:RequiredFieldValidator>

                                        </div>
                                        <div style="float:left;padding:5px 10px;margin-top:0px;">
                                            <span class="lbl" >Mobile </span><br />
                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="inp-txt" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtMobile" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtMobile"
                                                ErrorMessage="Enter Mobile" SetFocusOnError="true" ValidationGroup="a" style="margin-left:-150px"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                             <asp:LinkButton ID="btnLoginLink" runat="server" style="" OnClick="btnLoginLink_Click" TabIndex="5"  CssClass="btnlink">Back to Login</asp:LinkButton>
                                        </div>
                                        <asp:Button ID="btnSend" runat="server" CssClass="btn" Text="Recover Password" OnClick="btnSend_Click"  ValidationGroup="a" TabIndex="4"/>
                                       
                                    </div>
                                   
                                </div>
                            </div>
                            <div style="padding-top:5px;">
                                <div style="color:rgb(238, 37, 47);font-size:13px;">Note : <asp:Label ID="lblLoginNote" runat="server" text="Welcome to Gym Software" ></asp:Label></div>
                            </div>
                         </center>
                     </div>
                    </td>
                </tr>
                 <tr>
                    <td style="padding:0px;">
                     <div class="footer" id="divLowerSection" >
                         <center style="padding-top:20px;">
                            <table style="margin-top:35px;"> 
                            <tr>
                                <td>
                                     <div style="float:left;">
                                         <img src="../Icon/NDLogoImg.png"  class="ndlogo" />
                                     </div>
                                </td>
                                <td>
                                     <div style="margin-left:10px;border-left:1px solid white;height:17px;float:left;margin-top:10px;">
                                 <h4 style="color:white;margin-top:0px;margin-left:10px;"> Navkar Dreamsoft Pvt. Ltd. &nbsp;&nbsp; Contact : 09156184755</h4>
                                </div>
                                </td>
                            </tr>
                            </table>
                         </center>
                     </div>
                     </td>
                </tr>
                </table>
             </td>
             <td class="bgright"></td>
         </tr>
     </table>
    </form>
</body>
</html>
