<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EmailStatus.aspx.cs" Inherits="NDOnlineGym_2017.EmailStatus" %>
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
            <h3>Email Status
                 <div class="navigation" >
                    <ul>
                        <li>File &nbsp; > &nbsp;</li>
                        <li>Email  &nbsp; > &nbsp;</li>
                        <li>Email Status</li>
                    </ul>
                 </div>
            </h3>       
    </div>
    <div class="divForm">
       <div class="form-header">
            <h4>&#10148; Email Status</h4>
        </div>
         <div class="form-panel" id="formpanel5">              
              <table style="width:100%;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl">Status</span></td>
                            <td style="width:55%;text-align:left;">
                             <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" TabIndex="1">
                                   <asp:ListItem Value="OFF">OFF</asp:ListItem> 
                                 <asp:ListItem Value="ON">ON</asp:ListItem>                                                              
                                </asp:DropDownList>
                               
                            </td>
                         </tr></table>
                    </td>
                    </tr>

              </table>            

        </div>
          <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" OnClick="btnSave_Click"  OnClientClick="this.disabled = true;" UseSubmitBehavior="false"  />                
             </center>   
        </div>
        </div>
</asp:Content>
