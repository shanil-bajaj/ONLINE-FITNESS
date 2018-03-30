<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EmailTemplates.aspx.cs" Inherits="NDOnlineGym_2017.EmailTemplates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
      <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <script type="text/javascript" >
        function capFirst(oTextBox) {
            oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
        }
</script>
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
            <h3>Email Templates
                 <div class="navigation" >
                    <ul>
                        <li>File &nbsp; > &nbsp;</li>
                        <li>Email  &nbsp; > &nbsp;</li>
                        <li>Email Templates</li>
                    </ul>
                 </div>
            </h3>       
    </div>
     <div class="divForm">
       <div class="form-header">
            <h4>&#10148; Email Templates</h4>
        </div>
         <div class="form-panel" >              
              <table style="width:100%;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Email Header</span></td>
                            <td style="width:55%;text-align:left;">
                              <asp:TextBox ID="txtEmailHeader" runat="server" CssClass="txt1" TabIndex="1"  TextMode="MultiLine" onChange="javascript:capFirst(this);"></asp:TextBox>
                               
                            </td>
                         </tr></table>
                    </td>
                     <td class="cols">
                           <asp:Button ID="btnEmailHeader" runat="server" Text="Save" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" OnClick="btnEmailHeader_Click"  />
                         </td>
                    </tr>

                  <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Email Footer</span></td>
                            <td style="width:55%;text-align:left;">
                              <asp:TextBox ID="txtEmailFooter" runat="server" CssClass="txt1" TabIndex="3"  TextMode="MultiLine" onChange="javascript:capFirst(this);"></asp:TextBox>
                               
                            </td>
                         </tr></table>
                    </td>
                     <td class="cols">
                           <asp:Button ID="btnEmailFooter" runat="server" Text="Save" CssClass="form-btn"  ValidationGroup="a" TabIndex="4" OnClick="btnEmailFooter_Click"    />
                         </td>
                    </tr>

              </table>            

        </div>
         
               
              
        </div>     
</div>
</asp:Content>
