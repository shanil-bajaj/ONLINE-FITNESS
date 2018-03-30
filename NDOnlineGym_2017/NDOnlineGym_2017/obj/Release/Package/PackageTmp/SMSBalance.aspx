<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SMSBalance.aspx.cs" Inherits="NDOnlineGym_2017.SMSBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
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
        <h3>Balance SMS 
                 <div class="navigation">
                     <ul>
                         <li>File &nbsp; > &nbsp;</li>
                         <li>SMS  &nbsp; > &nbsp;</li>
                         <li>Balance SMS </li>
                     </ul>
                 </div>
        </h3>
    </div>

    <div class="divForm">
        <div class="form-header">
            <h4>&#10148; Balance SMS </h4>
        </div>
        <div class="form-panel">

            <table>
                <tr>

                    <td style="text-align: Right; width: 60%;"><span class="lbl">Available SMS Balance</span></td>
                    <td style="text-align: left; width: 60%;">
                        <asp:Label ID="lblSMSBalance" runat="server" Text="" class="lbl" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                </tr>

            </table>
        </div>
    </div>
          </div>
</asp:Content>
