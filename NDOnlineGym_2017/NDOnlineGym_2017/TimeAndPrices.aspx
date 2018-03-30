<%@ Page Title="" Language="C#" MasterPageFile="~/TimeSlotBooking.Master" AutoEventWireup="true" CodeBehind="TimeAndPrices.aspx.cs" Inherits="NDOnlineGym_2017.TimeAndPrices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
     <style>
        .GridView {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

            .GridView a /** FOR THE PAGING ICONS  **/ {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
            }

                .GridView a:focus {
                    color: orangered;
                }

                .GridView a:hover {
                    color: orangered;
                }

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .txtTime {
            border: 1px solid silver;
            padding-left: 5px;
            float: left;
            width: 173px;
        }

        .btn-remove {
            background-color: rgb(248, 45, 70);
            color: white;
            border: 1px solid rgb(248, 45, 70);
            margin-top: 3px;
        }

            .btn-remove:focus {
                border: 1px solid black;
                cursor: pointer;
            }

        .btn-file:focus {
            border: 1px solid silver;
            cursor: pointer;
        }

        input[type="checkbox"]:focus {
            border-color: #ffffcc;
        }
        .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
         .pricetable { width:100%;border:1px solid silver;border-collapse:collapse; }
         .pricetable th, .pricetable td { border:1px solid silver;padding:3px 5px; }
         .heading-table { background-color:rgb(128, 128, 128);color:white; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="sc">
            <div class="form-name-header">
                <h3>Custom Time and Price
                 <div class="navigation">
                     <ul>
                         <li>Time And Prices &nbsp; > &nbsp;</li>
                         <li>Custom Time and Price</li>
                     </ul>
                 </div>

                </h3>
            </div>

            <div class="divForm">
                
                    <%--Personal Details--%>
                    <div class="form-header">
                        <h4>&#10148; Slot Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width:100%;" >
                            <tr>
                                <td style="width:48%;border-right:1px solid silver;border-bottom:1px solid silver;">
                                     <table class="pricetable" style="">
                            <tr class="heading-table">
                                <th>Start Time</th>
                                <th>End Time</th>
                                <th>Price(Member)</th>
                                <th>Price(Non Member)</th>
                            </tr>
                            <tr>
                                <td>12 AM</td>
                                <td>1 AM</td>
                                <td><asp:TextBox ID="slot1" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="TextBox24" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>1 AM</td>
                                <td>2 AM</td>
                                <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="TextBox25" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>2 AM</td>
                                <td>3 AM</td>
                                <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox26" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>3 AM</td>
                                <td>4 AM</td>
                                <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox27" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>4 AM</td>
                                <td>5 AM</td>
                                <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox28" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>5 AM</td>
                                <td>6 AM</td>
                                <td><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox29" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>6 AM</td>
                                <td>7 AM</td>
                                <td><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="TextBox30" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>7 AM</td>
                                <td>8 AM</td>
                                <td><asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="TextBox31" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>8 AM</td>
                                <td>9 AM</td>
                                <td><asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox32" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>9 AM</td>
                                <td>10 AM</td>
                                <td><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox33" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>10 AM</td>
                                <td>11 AM</td>
                                <td><asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox34" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>11 AM</td>
                                <td>12 PM</td>
                                <td><asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox35" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                                </td>
                                <td style="width:48%;border-right:1px solid silver;border-bottom:1px solid silver;">
                                    <table class="pricetable" style="">
                            <tr class="heading-table">
                                <th>Start Time</th>
                                <th>End Time</th>
                                <th>Price(Member)</th>
                                <th>Price(Non Member)</th>
                            </tr>
                            <tr>
                                <td>12 AM</td>
                                <td>1 AM</td>
                                <td><asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="TextBox36" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>1 AM</td>
                                <td>2 AM</td>
                                <td><asp:TextBox ID="TextBox13" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="TextBox37" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>2 AM</td>
                                <td>3 AM</td>
                                <td><asp:TextBox ID="TextBox14" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox38" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>3 AM</td>
                                <td>4 AM</td>
                                <td><asp:TextBox ID="TextBox15" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox39" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>4 AM</td>
                                <td>5 AM</td>
                                <td><asp:TextBox ID="TextBox16" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox40" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>5 AM</td>
                                <td>6 AM</td>
                                <td><asp:TextBox ID="TextBox17" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox41" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>6 AM</td>
                                <td>7 AM</td>
                                <td><asp:TextBox ID="TextBox18" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="TextBox42" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>7 AM</td>
                                <td>8 AM</td>
                                <td><asp:TextBox ID="TextBox19" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="TextBox43" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>8 AM</td>
                                <td>9 AM</td>
                                <td><asp:TextBox ID="TextBox20" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox44" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>9 AM</td>
                                <td>10 AM</td>
                                <td><asp:TextBox ID="TextBox21" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox45" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>10 AM</td>
                                <td>11 AM</td>
                                <td><asp:TextBox ID="TextBox22" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox46" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>11 AM</td>
                                <td>12 PM</td>
                                <td><asp:TextBox ID="TextBox23" runat="server"></asp:TextBox></td>
                                 <td><asp:TextBox ID="TextBox47" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                 <center class="btn-section">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" UseSubmitBehavior="false" TabIndex="26" />
                    <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="27" />
                </center>
            </div>
    </div>
</asp:Content>
