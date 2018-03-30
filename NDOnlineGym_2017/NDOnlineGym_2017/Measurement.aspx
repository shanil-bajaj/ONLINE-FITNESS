<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Measurement.aspx.cs" Inherits="NDOnlineGym_2017.Measurement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <style>
        table td {
            padding-bottom: 10px;
        }

        input:focus {
            border: 1px solid rgb(242, 137, 9);
        }

        .ddl:focus {
            border: 1px solid rgb(242, 137, 9);
        }

        .ErrorBox {
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

        .errorborder {
            border: 1px solid red;
        }

        .form-panel {
            width: 100%;
            padding: 0px;
        }

        .sc {
            width: 1021px;
        }

        @media screen and (min-width: 1400px) {
            .sc {
                width: 1100px;
            }
        }
    </style>

    <script>
        //Disable enable executive dropdown on check box
        function ChExecutive() {
            var _chkExecutive = document.getElementById('<%= chkExecutive.ClientID %>');
            document.getElementById('<%= ddlExecutive.ClientID %>').disabled = _chkExecutive.checked;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="sc">
                <div class="form-name-header">
                    <h3>Measurement
                 <div class="navigation">
                     <ul>
                         <li>Status &nbsp; > &nbsp;</li>
                         <li>Body Assessment  &nbsp; > &nbsp;</li>
                         <li>Measurement</li>
                     </ul>
                 </div>
                    </h3>
                </div>

                <div class="divForm">
                    <div class="form-header" id="formheader1">
                        <h4>&#10148;Member Details </h4>
                    </div>
                    <div class="form-panel" id="formpanel1">
                        <table style="height: 80px;">
                            <tr>
                                <th>ID<span class="error">*</span></th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Gender</th>
                                <th>Contact<span class="error">*</span></th>
                                <th>Email</th>
                            </tr>
                            <tr id="row1" runat="server">
                                <td>
                                    <asp:TextBox ID="txtMemberID" runat="server" Style="width: 100px; padding: 3px 5px;" AutoPostBack="true" OnTextChanged="txtMemberID_TextChanged" TabIndex="1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFirst" runat="server" Style="width: 150px; padding: 3px 5px;" Enabled="false" ></asp:TextBox>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtLast" runat="server" Style="width: 150px; padding: 3px 5px;" Enabled="false" ></asp:TextBox>

                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGender" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" Enabled="false">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtContact" runat="server" Style="width: 150px; padding: 3px 5px;" OnTextChanged="txtContact_TextChanged" AutoPostBack="true" TabIndex="2"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtmail" runat="server" Style="width: 200px; padding: 3px 5px;" Enabled="false" TabIndex="3"></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="form-header" id="Div1">
                        <h4>&#10148;Measurement Details </h4>
                    </div>
                    <div class="form-panel" id="Div3">
   <%--                     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>

                                <table style="height: 50px; margin-bottom: 10px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblArms" runat="server" Text="Arms"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblWeight" runat="server" Text="Weight"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblHeight" runat="server" Text="Height"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblWaist" runat="server" Text="Waist"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblThigh" runat="server" Text="Thigh"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Fat"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="M.Mass"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="B.Mass`"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDate" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="4"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server" BehaviorID="txtDate_CalendarExtender" Format="dd-MM-yyyy" TargetControlID="txtDate" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtArms" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="5"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWight" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="6"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHeight" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="7"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWaist" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="8"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtThigh" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFat" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="10"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMmass" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBmass" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="12"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <table style="height: 50px; margin-bottom: 10px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="BMI"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="DCI"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="M.AGE"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Water%"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="V.Fat"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Neck"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Upper Arm"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="For  Arm"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="Shoulder"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="Hips"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtBMI" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="13"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDCI" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="14"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMAge" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="15"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWater" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="16"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtVFat" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="17"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNeck" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="18"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUpperArms" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="19"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtForArms" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="20"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSholder" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="21"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHips" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="22"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <table style="height: 50px; margin-bottom: 10px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="Chest Normal"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="Chest Expended"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="Upper Abdomen"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="Lower Abdomen"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="Calf"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td>
                                            <span>Executive </span>
                                        </td>
                                        <td>
                                            <span>Programmer Name </span>
                                            <span class="error">*</span>
                                        </td>
                                        <td>
                                            <span>Next Measurement Date </span>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtChest" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="23"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtExpendedChest" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="24"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUpperAbdomen" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="25"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLowerAbdomen" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="26"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCalf" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="27"></asp:TextBox>
                                        </td>
                                        <td>
                                            <%--<asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" TabIndex="27" />--%>
                                            <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" onChange="ChExecutive();" TabIndex="28" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="29" Enabled="false" Style="width: 150px; padding: 3px 5px;">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DdlProgrammer" runat="server" CssClass="ddl" TabIndex="30" Style="width: 200px; padding: 3px 5px;">
                                                <asp:ListItem Value="All">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNxtFllwdte" runat="server" Style="width: 120px; padding: 3px 5px;" TabIndex="31"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtNxtFllwdte_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" Format="dd-MM-yyyy" TargetControlID="txtNxtFllwdte" />
                                        </td>
                                    </tr>
                                </table>

                                </div>
                <center class="btn-section">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" OnClick="btnSave_Click" TabIndex="32" />
                    <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="form-btn" OnClick="btnCancel_Click"  TabIndex="33" />
                </center>


                           <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <%--<asp:Label ID="lblMemberAutoID" runat="server"> </asp:Label>--%>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
