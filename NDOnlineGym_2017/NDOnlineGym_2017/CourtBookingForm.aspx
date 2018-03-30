<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourtBookingForm.aspx.cs" Inherits="NDOnlineGym_2017.CourtBookingForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
          .timeslot { border:1px solid black;border-collapse:collapse }
          .timeslot th,.timeslot td { text-align:left;padding:5px 7px;border:1px solid black; }
           
          .heading-table { background-color:silver }
    </style>
</head>
<body>
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div style="width:1000px">
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
             <div class="divForm">
          <div class="form-header">
               <h4>&#10148; Personal Details  </h4>
          </div>
          <div class="form-panel">
               <table style="width: 50%;">
                        <tr>
                            <td>
                                <asp:RadioButton GroupName="ch" runat="server" ID="rbtnSingle"  />&nbsp;<span style="font-size: 14px; font-weight: bold" tabindex="3">Member</span>
                            </td>
                            <td>
                                <asp:RadioButton GroupName="ch" runat="server" ID="rbtnCouple"  />&nbsp;<span style="font-size: 14px; font-weight: bold" tabindex="4">Non Member</span>
                            </td>
                          
                        </tr>
                </table>
              <table id="tableInfo" runat="server" style="margin-top: 30px;" >
                        <tr>
                            <th>ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Gender</th>
                            <th>Contact</th>
                            <th>EmailID</th>
                        </tr>

                        <tr id="row1" runat="server" >
                            <td>
                                <asp:TextBox ID="txtId1" runat="server" Style="width: 100px; padding: 3px 5px;" OnTextChanged="txtId1_TextChanged" AutoPostBack="true" TabIndex="7" ></asp:TextBox>

                            </td>
                            <td>
                                <asp:TextBox ID="txtFirstName1" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled"  TabIndex="8" ></asp:TextBox>


                            </td>
                            <td>
                                <asp:TextBox ID="txtLastName1" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled"  TabIndex="9"></asp:TextBox>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGender1" runat="server" Style="width: 100px; padding: 3px 5px;"  CssClass="ddl" TabIndex="10">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>
                                <asp:TextBox ID="txtContact1" runat="server" Style="width: 150px; padding: 3px 5px;" AutoCompleteType="Disabled"  TabIndex="11"  MaxLength="11" ></asp:TextBox>



                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail1" runat="server" Style="width: 200px; padding: 3px 5px;" AutoCompleteType="Disabled"  TabIndex="12"></asp:TextBox>

                            </td>
                        </tr>
                </table>
                <fieldset style="width:920px;margin-top:20px;padding:5px 10px;">
                    <legend style="padding:3px 5px;color:white;background-color:rgb(0, 51, 102);width:150px">Booking Details</legend>
                    <table style="width:100%" >
                        <tr>
                            <td style="width:60%" >
                                <div style="width:500px;height:410px;" >
                                    <table>
                                        <tr>
                                            <th style="width:120px;text-align:left;">Booking ID : </th>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtID" runat="server" style="width:187px;padding:5px;border:1px solid silver;border-radius:2px" ></asp:TextBox>

                                            </td>
                                        </tr>
                                          <tr>
                                            <th style="width:120px;text-align:left;">Date: </th>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtDate" runat="server" CssClass="input-txt" style="width:188px;padding:5px;border:1px solid silver;border-radius:2px" ></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtDate_CalendarExtender" TargetControlID="txtDate" Format="dd-MM-yyyy" />

                                            </td>
                                        </tr>
                                         <tr>
                                            <th style="width:120px;text-align:left;">Booking Date: </th>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtBookingDate" runat="server" CssClass="input-txt" style="width:188px;padding:5px;border:1px solid silver;border-radius:2px" ></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    BehaviorID="txtDate_CalendarExtender" TargetControlID="txtBookingDate" Format="dd-MM-yyyy" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <th style="width:120px;text-align:left;">Court : </th>
                                            <td style="text-align:left;">
                                                <asp:DropDownList ID="ddlCourtType" runat="server" style="width:200px;padding:5px;border:1px solid silver;border-radius:2px" >
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                         <tr>
                                            <th style="width:120px;text-align:left;">Status : </th>
                                            <td style="text-align:left;">
                                                <asp:DropDownList ID="ddlStatus" runat="server" style="width:200px;padding:5px;border:1px solid silver;border-radius:2px" >
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem>Pending</asp:ListItem>
                                                    <asp:ListItem>Confirm</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr>
                                            <th style="width:130px;text-align:left;">Available Time Slots : </th>
                                            <td style="text-align:left;">
                                                

                                            </td>
                                        </tr>
                                    </table>
                                  <%--  <table >
                                        <tr>
                                            <td>
                                                <table class="timeslot" style="margin-top:10px;">
                                                     <tr class="heading-table">
                                                      
                                                        <th>Start Time</th>
                                                        <th>End Time</th>
                                                        <th>Price(Member)</th>
                                                        <th>Price(Non Member)</th>
                                                        <th></th>
                                                    </tr>
                                                    <tr>
                                                        
                                                        <td>12 AM</td>
                                                        <td>1 AM</td>
                                                        <td><asp:TextBox ID="slot1" runat="server" style="width:120px"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="TextBox24" runat="server" style="width:120px"></asp:TextBox></td>
                                                        <td><asp:Button ID="btnCancle" runat="server" Text="X" 
                                                            style="font-size:15px;font-weight:bold;width:30px;height:22px;" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        
                                    </table>--%>
                                    <div style="width:470px;border:1px solid black;overflow-y:auto;height:120px;overflow-x:scroll">
                                         <asp:GridView ID="gvCourse" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                                            EmptyDataText="No record found." Width="970px" Font-Size="13px" PagerStyle-CssClass="pager"
                                            AllowPaging="True" PageSize="20" TabIndex="69" CssClass="GridView" >
                                            <Columns>

                                   

                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                    <strong>Assign Slots</strong>
                                     <div style="width:470px;border:1px solid black;overflow-y:auto;height:120px;overflow-x:scroll">
                                         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                                            EmptyDataText="No record found." Width="970px" Font-Size="13px" PagerStyle-CssClass="pager"
                                            AllowPaging="True" PageSize="20" TabIndex="69" CssClass="GridView" >
                                            <Columns>

                                   

                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                    
                                </div>
                            </td>
                            <td style="width:40%" >
                                <div style="width:400px;height:410px;" >
                                    <table>
                                        <tr>
                                           <th style="width:120px;text-align:left;">Payment Mode : </th>
                                            <td style="text-align:left;">
                                                <asp:DropDownList ID="ddlPaymentType" runat="server" style="width:200px;padding:5px;border:1px solid silver;border-radius:2px" >
                                                    <asp:ListItem>Cash</asp:ListItem>
                                                    <asp:ListItem>Card</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                 </asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr>
                                            <th style="width:120px;text-align:left;">Price : </th>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="TextBox1" runat="server" style="width:187px;padding:5px;border:1px solid silver;border-radius:2px" ></asp:TextBox>

                                            </td>
                                        </tr>
                                         <tr>
                                            <th style="width:120px;text-align:left;">Additional Charge : </th>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="TextBox2" runat="server" style="width:187px;padding:5px;border:1px solid silver;border-radius:2px" ></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <th style="width:120px;text-align:left;">Tax : </th>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="TextBox4" runat="server" style="width:187px;padding:5px;border:1px solid silver;border-radius:2px" ></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <th style="width:120px;text-align:left;">Discount : </th>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="TextBox3" runat="server" style="width:187px;padding:5px;border:1px solid silver;border-radius:2px" ></asp:TextBox>

                                            </td>
                                        </tr>
                                         <tr>
                                            <th style="width:120px;text-align:left;">Deposite : </th>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="TextBox6" runat="server" style="width:187px;padding:5px;border:1px solid silver;border-radius:2px" ></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <th style="width:120px;text-align:left;">Total : </th>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="TextBox5" runat="server" style="width:187px;padding:5px;border:1px solid silver;border-radius:2px" ></asp:TextBox>

                                            </td>
                                        </tr>
                                       
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                                    <center class="btn-section">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" UseSubmitBehavior="false" TabIndex="26" />
                                        <asp:Button ID="Button1" runat="server" Text="Clear" CssClass="form-btn" TabIndex="27" />
                                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                    </center>
                </fieldset>
          </div>
    </div>
    </div>
    </form>
</body>
</html>
