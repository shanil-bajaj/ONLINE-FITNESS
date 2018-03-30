<%@ Page Title="" Language="C#" MasterPageFile="~/PTMaster.Master" AutoEventWireup="true" CodeBehind="PTAttendance.aspx.cs" Inherits="NDOnlineGym_2017.PTAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <%-- <link rel="icon" type="image/png" href="Logo/NDLogo.png" />--%>
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <style>
        table td
        {
            padding-bottom: 10px;
        }

        input:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ddl:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ErrorBox
        {
            position: relative;
            z-index: 1;
            font-weight: normal;
            border-radius: 3px;
            box-shadow: 10px 10px 10px rgba(0, 0, 0, 0.5);
            padding: 5px 7px;
            color: #a94442;
            background-color: #f2dede;
            border: 1px solid #ebccd1;
        }

        .errorborder
        {
            border: 1px solid red;
        }

        .form-panel
        {
            width: 100%;
            padding: 0px;
        }

        .GridView
        {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

            .GridView a /** FOR THE PAGING ICONS  **/
            {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
            }

                .GridView a:focus
                {
                    color: orangered;
                }

                .GridView a:hover
                {
                    color: orangered;
                }

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
            {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .remove
        {
            font-size: 18px;
            font-weight: bold;
        }

        .hideGridColumn
        {
            display: none;
        }

        .sc
        {
            width: 1021px;
        }

        @media screen and (min-width: 1400px)
        {
            .sc
            {
                width: 1100px;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
        <div class="form-name-header">
            <h3>P.T Attendace
            <div class="navigation">
                <ul>
                    <li>Attendance &nbsp; > &nbsp;</li>
                    <li>P.T Attendace</li>
                </ul>
            </div>
            </h3>
        </div>
        <div class="divForm">
            <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-header" id="formheader1">
                        <h4>&#10148;Member Details </h4>
                    </div>
                    <div class="form-panel" id="formpanel1">
                        <table style="height: 80px;">
                            <tr>
                                <th>ID</th>
                                <th>Contact</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Gender</th>
                                <th>Email ID</th>
                            </tr>
                            <tr id="row1" runat="server">
                                <td>
                                    <asp:TextBox ID="txtMemberID" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="1" AutoPostBack="true" OnTextChanged="txtMemberID_TextChanged"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:TextBox ID="txtContact" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="2" AutoPostBack="true" OnTextChanged="txtContact_TextChanged"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:TextBox ID="txtFirst" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="3" Enabled="false"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtLast" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="4" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGender" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="5" Enabled="false">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtmail" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="6" Enabled="false"></asp:TextBox>
                                    <asp:Label ID="lblMemberAutoID" runat="server" Visible="false"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="form-header" id="Div1">
                        <h4>&#10148;Instructor Details </h4>
                    </div>
                    <div class="form-panel" id="Div2">
                        <table style="height: 80px;">
                            <tr>
                                <th>Instructor ID</th>
                                <th>Instructor Name</th>
                                <th>Alternative Instructor Name</th>
                                <th>Note</th>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td>
                                    <asp:TextBox ID="txtInstructorID" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="7" AutoPostBack="true" OnTextChanged="txtInstructorID_TextChanged"></asp:TextBox>
                                     <asp:Label ID="lblinstructor" runat="server" Visible="false"></asp:Label>
                                </td>

                                <td>
                                    <asp:TextBox ID="txtInstructorName" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="8" Enabled="false"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:TextBox ID="txtAlternativeName" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="9"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtNote" runat="server" Style="width: 250px; padding: 3px 5px;" TabIndex="10"></asp:TextBox>
                                </td>

                            </tr>
                        </table>

                    </div>
                    <center>
                          <asp:Button ID="btnPresent" runat="server" Text="Present" CssClass="form-btn"  TabIndex="11" OnClick="btnPresent_Click"/>
                          <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn"  TabIndex="12" OnClick="btnClear_Click"/>
                 </center>
                    </div>
    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnPresent" />
                </Triggers>
            </asp:UpdatePanel>
</asp:Content>
