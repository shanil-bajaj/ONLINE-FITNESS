<%@ Page Title="" Language="C#" MasterPageFile="~/TimeSlotBooking.Master" AutoEventWireup="true" CodeBehind="CourtBooking.aspx.cs" Inherits="NDOnlineGym_2017.CourtBooking" %>
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
         .CourtDetailsTable { width:100%;border:1px solid silver;border-collapse:collapse;margin-top:20px; }
         .CourtDetailsTable th { border:1px solid silver;padding:5px;  }
         .CourtDetailsTable td { border:1px solid silver;padding:5px;  }
         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <div class="sc">
            <div class="form-name-header">
                <h3>Court Booking
                 <div class="navigation">
                     <ul>
                         <li>Booking &nbsp; > &nbsp;</li>
                         <li>Court Booking</li>
                     </ul>
                 </div>

                </h3>
            </div>

            <div>
                
                    <%--Personal Details--%>
                    <div class="form-header">
                        <h4>&#10148; Court Booking  </h4>
                    </div>
                    <div class="form-panel">
                        <table>
                            <tr>
                                <th>Court</th>
                                <th>Date</th>
                                <th></th>
                                <th></th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" CssClass="ddl">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="input-txt"></asp:TextBox>
                                      <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtDate_CalendarExtender" TargetControlID="txtDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                      <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" ValidationGroup="a" UseSubmitBehavior="false" TabIndex="26" />
                    
                                </td>
                                <td>
                                    <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="27" />
                                </td>
                            </tr>
                        </table>

                        <table class="CourtDetailsTable" style="">
                            <tr style="background-color:rgb(247, 247, 247)">
                                <th>Time</th>
                                <th>ID</th>
                                <th>Name</th>
                                <th>Contact</th>
                                <th>Price</th>
                                <th>Status</th>
                                <th></th>
                            </tr>
                            <tr>
                                <td>12AM to 1AM</td>
                                <td>
                                    <asp:Label runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label1" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label2" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label4" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                 <td style="width:70px;">
                                    <asp:ImageButton runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>1AM to 2AM</td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label6" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label7" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label9" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                 <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>2AM to 3AM</td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label14" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" Visible="false" />
                                    <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" Visible="false" />
                                     <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" OnClick="ImageButton8_Click" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>3AM to 4AM</td>
                                 <td>
                                    <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" Visible="false" />
                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" Visible="false" />
                                     <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>4AM to 5AM</td>
                                 <td>
                                    <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label24" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" Visible="false" />
                                    <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" Visible="false" />
                                     <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>5AM to 6AM</td>
                                <td>
                                    <asp:Label ID="Label25" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label26" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label27" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label28" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label29" runat="server" Text="Confirm"></asp:Label>
                                </td>
                               <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton16" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton17" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>6AM to 7AM</td>
                                <td>
                                    <asp:Label ID="Label30" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label31" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label32" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label33" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label34" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                 <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton19" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton20" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>7AM to 8AM</td>
                                <td>
                                    <asp:Label ID="Label35" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label36" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label37" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label38" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label39" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton21" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton22" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton23" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>8AM to 9AM</td>
                                 <td>
                                    <asp:Label ID="Label40" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label41" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label42" runat="server" Text=""></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label43" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label44" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton24" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" Visible="false" />
                                    <asp:ImageButton ID="ImageButton25" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" Visible="false" />
                                     <asp:ImageButton ID="ImageButton26" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>9AM to 10AM</td>
                                <td>
                                    <asp:Label ID="Label45" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label46" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label47" runat="server" Text=""></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label48" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label49" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton27" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" Visible="false" />
                                    <asp:ImageButton ID="ImageButton28" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" Visible="false" />
                                     <asp:ImageButton ID="ImageButton29" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>10AM to 11AM</td>
                                <td>
                                    <asp:Label ID="Label50" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label51" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label52" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label53" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label54" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                 <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton30" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton31" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton32" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>11AM to 12PM</td>
                                <td>
                                    <asp:Label ID="Label55" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label56" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label57" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label58" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label59" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton33" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton34" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton35" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>12PM to 1PM</td>
                                 <td>
                                    <asp:Label ID="Label60" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label61" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label62" runat="server" Text=""></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label63" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label64" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton36" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" Visible="false" />
                                    <asp:ImageButton ID="ImageButton37" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" Visible="false" />
                                     <asp:ImageButton ID="ImageButton38" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>1PM to 2PM</td>
                                <td>
                                    <asp:Label ID="Label65" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label66" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label67" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label68" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label69" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton39" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton40" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton41" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>2PM to 3PM</td>
                                <td>
                                    <asp:Label ID="Label70" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label71" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label72" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label73" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label74" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton42" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton43" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton44" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>3PM to 4PM</td>
                                 <td>
                                    <asp:Label ID="Label75" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label76" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label77" runat="server" Text=""></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label78" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label79" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton45" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" Visible="false" />
                                    <asp:ImageButton ID="ImageButton46" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" Visible="false" />
                                     <asp:ImageButton ID="ImageButton47" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>4PM to 5PM</td>
                                <td>
                                    <asp:Label ID="Label80" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label81" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label82" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label83" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label84" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                 <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton48" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton49" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton50" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>5PM to 6PM</td>
                                <td>
                                    <asp:Label ID="Label85" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label86" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label87" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label88" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label89" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton51" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton52" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton53" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>6PM to 7PM</td>
                                <td>
                                    <asp:Label ID="Label90" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label91" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label92" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label93" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label94" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                 <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton54" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton55" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton56" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>7PM to 8PM</td>
                                <td>
                                    <asp:Label ID="Label95" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label96" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label97" runat="server" Text=""></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label98" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label99" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton57" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" Visible="false" />
                                    <asp:ImageButton ID="ImageButton58" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" Visible="false" />
                                     <asp:ImageButton ID="ImageButton59" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>8PM to 9PM</td>
                                <td>
                                    <asp:Label ID="Label100" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label101" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label102" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label103" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label104" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton60" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton61" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton62" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>9PM to 10PM</td>
                                <td>
                                    <asp:Label ID="Label105" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label106" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label107" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label108" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label109" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                 <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton63" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton64" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton65" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>10PM to 11PM</td>
                                <td>
                                    <asp:Label ID="Label110" runat="server" Text="1"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label111" runat="server" Text="AAA"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label112" runat="server" Text="9881675678"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label113" runat="server" Text="1500"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label114" runat="server" Text="Confirm"></asp:Label>
                                </td>
                                 <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton66" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" />
                                    <asp:ImageButton ID="ImageButton67" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" />
                                     <asp:ImageButton ID="ImageButton68" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" Visible="false" /> 
                                </td>
                            </tr>
                            <tr style="background-color:rgb(247, 247, 247)">
                                <td>11PM to 12PM</td>
                                 <td>
                                    <asp:Label ID="Label115" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label116" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label117" runat="server" Text=""></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="Label118" runat="server" Text=""></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label119" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:70px;">
                                    <asp:ImageButton ID="ImageButton69" runat="server" ImageUrl="../NotificationIcons/edit.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Edit" Visible="false" />
                                    <asp:ImageButton ID="ImageButton70" runat="server" ImageUrl="../NotificationIcons/f-cross_256-128.png" 
                                        style="width:17px;height:17px;background-color:none;margin-right:5px;" ToolTip="Delete" Visible="false" />
                                     <asp:ImageButton ID="ImageButton71" runat="server" ImageUrl="../NotificationIcons/Add.png" 
                                        style="width:17px;height:17px;background-color:none" ToolTip="Book" /> 
                                </td>
                            </tr>
                        </table>
                    </div>
            </div>
        </div>
</asp:Content>
