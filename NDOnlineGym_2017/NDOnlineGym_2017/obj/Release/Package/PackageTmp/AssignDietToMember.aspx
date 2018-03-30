<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AssignDietToMember.aspx.cs" Inherits="NDOnlineGym_2017.AssignDietToMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <script type="text/javascript">
        function LimtCharacters(txtMsg, CharLength, indicator) {
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtMsg.value = txtMsg.value.substring(0, CharLength);
            }
        }
    </script>
    <script type="text/javascript">
        //Function to allow only numbers to textbox
        function validate(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            var phn = document.getElementById('txtPhn');
            //comparing pressed keycodes
            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                return false;
            }
            else {
                //Condition to check textbox contains ten numbers or not
                if (phn.value.length < 10) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
    <style>
        .GridView1 {
            width: max-content;
        }

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" runat="server">

    <div class="form-name-header" id="formhead" runat="server">
        <h3>Assign Diet
                 <div class="navigation">
                     <ul>
                         <li>Member &nbsp; > &nbsp;</li>
                         <li>Assign Diet  &nbsp; > &nbsp;</li>
                     </ul>
                 </div>
        </h3>
    </div>
    <div class="form-name-header" Visible="false" id="formheadDetails" runat="server">
        <h3>Diet Details
                 <div class="navigation">
                     <ul>
                         <li>Member &nbsp; > &nbsp;</li>
                         <li>Diet Details &nbsp; > &nbsp;</li>
                     </ul>
                 </div>
        </h3>
    </div>
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="sc">
                <div class="divForm">
                    <div id="divFormDetails" runat="server">
                        <div class="form-header" id="formheader1">
                            <h4>&#10148;Member Details </h4>
                        </div>
                        <div class="form-panel" id="formpanel1">
                            <table style="height: 80px;">
                                <tr>
                                    <asp:Label ID="Labeldt" runat="server" Text="Label" Visible="false"></asp:Label>
                                    <th>ID&nbsp;<span class="error">*</span></th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Gender</th>
                                    <th>Contact&nbsp;<span class="error">*</span></th>
                                    <th>Email</th>
                                    <th>Executive</th>
                                </tr>
                                <tr id="row1" runat="server">
                                    <td>

                                        <asp:TextBox ID="TxtID" runat="server" Style="width: 80px; padding: 3px 5px;" AutoPostBack="true" OnTextChanged="TxtID_TextChanged"
                                            onkeypress="return validate(event)" TabIndex="1"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvID" runat="server" Display="Dynamic" ControlToValidate="TxtID"
                                            SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirst" runat="server" Style="width: 130px; padding: 3px 5px;" TabIndex="2"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLast" runat="server" Style="width: 130px; padding: 3px 5px;" TabIndex="3"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="4">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact" runat="server" MaxLength="11" AutoPostBack="true" Style="width: 150px; padding: 3px 5px;" OnTextChanged="txtContact_TextChanged1" onkeypress="return validate(event)" TabIndex="5"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtmail" runat="server" Style="width: 170px; padding: 3px 5px;" TabIndex="6"></asp:TextBox>

                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" TabIndex="7" AutoPostBack="true" /></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="8" Enabled="false">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <h4><span>Pre-History</span></h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="width: 88%;">
                                        <asp:TextBox ID="txtPreHistory" CssClass="txt1" TextMode="MultiLine" runat="server" TabIndex="9" onkeyup="LimtCharacters(this,500,'Labehis');"></asp:TextBox>
                                        <br />
                                        Number of Characters Left:!
                        <label id="Labehis" style="background-color: #E2EEF1; color: Red; font-weight: bold;"></label>
                                    </td>
                                </tr>
                            </table>

                        </div>

                        <div class="form-header" id="Div1">
                            <h4>&#10148;Diet Details </h4>
                        </div>
                        <div class="form-panel" id="Div3">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table style="height: 80px;">
                                        <tr>

                                            <th>Dietitian Name</th>
                                            <th>Diet Date</th>
                                            <th>From Date</th>
                                            <th>To Date</th>

                                        </tr>
                                        <tr id="Tr1" runat="server">
                                            <td>

                                                <asp:TextBox ID="txtDietitian" runat="server" Style="width: 250px; padding: 3px 5px;" TabIndex="10"></asp:TextBox>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDietDate" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="11"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtDietDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtDietDate_CalendarExtender" TargetControlID="txtDietDate" Format="dd-MM-yyyy" />

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtfrmdte" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="12"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtfrmdte" Format="dd-MM-yyyy" />

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttodte" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="13"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtTodate_CalendarExtender" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txttodte" Format="dd-MM-yyyy" />

                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                        <%--  Diet 1--%>
                        <div class="form-header" id="formheader2">
                            <h4>&#10148; Assign Diet</h4>
                        </div>
                        <div class="form-panel" id="formpanel2">

                            <table style="width: 100%;">
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Meal </span></td>
                                                <td style="width: 55%; text-align: left;">

                                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl" TabIndex="14">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                        <asp:ListItem Value="Breakfast">Breakfast</asp:ListItem>
                                                        <asp:ListItem Value="Lunch">Lunch</asp:ListItem>
                                                        <asp:ListItem Value="Evining">Evining</asp:ListItem>
                                                        <asp:ListItem Value="Dinner">Dinner</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Time  </span></td>
                                                <td style="width: 55%; text-align: left;">

                                                    <asp:TextBox ID="txttime1" runat="server" TextMode="Time" CssClass="txt" TabIndex="15"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols"></td>

                                </tr>

                            </table>


                            <table style="width: 100%;">
                                <tr>
                                    <td></td>
                                    <td style="width: 88%;">
                                        <asp:TextBox ID="txtmeal1" runat="server" CssClass="txt1" TabIndex="16" TextMode="MultiLine" onkeyup="LimtCharacters(this,500,'lblcount');"></asp:TextBox>
                                        <br />
                                        Number of Characters Left:
                                    <label id="lblcount" style="background-color: #E2EEF1; color: Red; font-weight: bold;"></label>
                                    </td>

                                </tr>

                            </table>








                        </div>

                        <%-- End Diet 1--%>

                        <%--  Diet 2--%>

                        <div class="form-panel" id="Div2">

                            <table style="width: 100%;">
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Meal </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="ddl" TabIndex="17">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                        <asp:ListItem Value="Breakfast">Breakfast</asp:ListItem>
                                                        <asp:ListItem Value="Lunch">Lunch</asp:ListItem>
                                                        <asp:ListItem Value="Evining">Evening</asp:ListItem>
                                                        <asp:ListItem Value="Dinner">Dinner</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Time </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txttime2" runat="server" CssClass="txt" TextMode="Time" TabIndex="18"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols"></td>

                                </tr>

                            </table>


                            <table style="width: 100%;">
                                <tr>
                                    <td></td>
                                    <td style="width: 88%;">
                                        <asp:TextBox ID="txtmeal2" runat="server" CssClass="txt1" TabIndex="19" TextMode="MultiLine" onkeyup="LimtCharacters(this,500,'Label1');"></asp:TextBox>
                                        <br />
                                        Number of Characters Left:
                                    <label id="Label1" style="background-color: #E2EEF1; color: Red; font-weight: bold;"></label>
                                    </td>

                                </tr>

                            </table>








                        </div>
                        <%-- End Diet 2--%>

                        <%--  Diet 3--%>

                        <div class="form-panel" id="Div4">

                            <table style="width: 100%;">
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Meal </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:DropDownList ID="DropDownList4" runat="server" CssClass="ddl" TabIndex="20">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                        <asp:ListItem Value="Breakfast">Breakfast</asp:ListItem>
                                                        <asp:ListItem Value="Lunch">Lunch</asp:ListItem>
                                                        <asp:ListItem Value="Evining">Evening</asp:ListItem>
                                                        <asp:ListItem Value="Dinner">Dinner</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Time </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txttime3" runat="server" CssClass="txt" TextMode="Time" TabIndex="21"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols"></td>

                                </tr>

                            </table>


                            <table style="width: 100%;">
                                <tr>
                                    <td></td>
                                    <td style="width: 88%;">
                                        <asp:TextBox ID="txtmeal3" runat="server" CssClass="txt1" TabIndex="22" TextMode="MultiLine" onkeyup="LimtCharacters(this,500,'lblme');"></asp:TextBox>
                                        <br />
                                        Number of Characters Left:
                                    <label id="lblme" style="background-color: #E2EEF1; color: Red; font-weight: bold;"></label>
                                    </td>

                                </tr>

                            </table>








                        </div>
                        <%-- End Diet 3--%>

                        <%--  Diet 4--%>

                        <div class="form-panel" id="Div6">

                            <table style="width: 100%;">
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Meal</span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:DropDownList ID="DropDownList5" runat="server" CssClass="ddl" TabIndex="23">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                        <asp:ListItem Value="Breakfast">Breakfast</asp:ListItem>
                                                        <asp:ListItem Value="Lunch">Lunch</asp:ListItem>
                                                        <asp:ListItem Value="Evining">Evening</asp:ListItem>
                                                        <asp:ListItem Value="Dinner">Dinner</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Time </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txttime4" runat="server" TextMode="Time" CssClass="txt" TabIndex="24"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols"></td>

                                </tr>

                            </table>


                            <table style="width: 100%;">
                                <tr>
                                    <td></td>
                                    <td style="width: 88%;">
                                        <asp:TextBox ID="txtmeal4" runat="server" CssClass="txt1" TabIndex="25" TextMode="MultiLine" onkeyup="LimtCharacters(this,500,'Label3');"></asp:TextBox>
                                        <br />
                                        Number of Characters Left:
                                    <label id="Label3" style="background-color: #E2EEF1; color: Red; font-weight: bold;"></label>
                                    </td>

                                </tr>

                            </table>








                        </div>
                        <%-- End Diet 4--%>

                        <%--  Diet 5--%>

                        <div class="form-panel" id="Div8">

                            <table style="width: 100%;">
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Meal </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:DropDownList ID="DropDownList6" runat="server" CssClass="ddl" TabIndex="26">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                        <asp:ListItem Value="Breakfast">BrekFast</asp:ListItem>
                                                        <asp:ListItem Value="Lunch">Lunch</asp:ListItem>
                                                        <asp:ListItem Value="Evining">Evening</asp:ListItem>
                                                        <asp:ListItem Value="Dinner">Dinner</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Time </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txttime5" runat="server" CssClass="txt" TextMode="Time" TabIndex="27"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols"></td>

                                </tr>

                            </table>


                            <table style="width: 100%;">
                                <tr>
                                    <td></td>
                                    <td style="width: 88%;">
                                        <asp:TextBox ID="txtmeal5" runat="server" CssClass="txt1" TabIndex="28" TextMode="MultiLine" onkeyup="LimtCharacters(this,500,'Label4');"></asp:TextBox>
                                        <br />
                                        Number of Characters Left:
                                    <label id="Label4" style="background-color: #E2EEF1; color: Red; font-weight: bold;"></label>
                                    </td>

                                </tr>

                            </table>








                        </div>
                        <%-- End Diet 5--%>

                        <%--  Diet 6--%>
                        <div class="form-panel" id="Div5">

                            <table style="width: 100%;">
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Meal </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:DropDownList ID="DropDownList7" runat="server" CssClass="ddl" TabIndex="29">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                        <asp:ListItem Value="Breakfast">BrekFast</asp:ListItem>
                                                        <asp:ListItem Value="Lunch">Lunch</asp:ListItem>
                                                        <asp:ListItem Value="Evining">Evening</asp:ListItem>
                                                        <asp:ListItem Value="Dinner">Dinner</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Time </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txttime6" runat="server" CssClass="txt" TextMode="Time" TabIndex="30"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols"></td>

                                </tr>

                            </table>


                            <table style="width: 100%;">
                                <tr>
                                    <td></td>
                                    <td style="width: 88%;">
                                        <asp:TextBox ID="txtmeal6" runat="server" CssClass="txt1" TabIndex="31" TextMode="MultiLine" onkeyup="LimtCharacters(this,500,'Label5');"></asp:TextBox>
                                        <br />
                                        Number of Characters Left:
                                    <label id="Label5" style="background-color: #E2EEF1; color: Red; font-weight: bold;"></label>
                                    </td>

                                </tr>

                            </table>

                            <div class="form-panel" id="Div9">
                                <table>
                                    <tr>
                                        <td style="width: 19.2%;"></td>

                                        <td>
                                            <h4><span>Avoid </span></h4>
                                            <asp:TextBox ID="txtavoid" CssClass="txt1" TextMode="MultiLine" TabIndex="32" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                </table>
                            </div>







                            <center><asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="a" OnClick="btnSave_Click" CssClass="form-btn"  
            OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" TabIndex="33" />                                                                 
                  <asp:Button ID="btnCancel" runat="server" Text="Clear" TabIndex="34" CssClass="form-btn" OnClick="btnClear_Click" />   </center>

                        </div>
                        <%-- End Diet 6--%>
                    </div>
                    <div id="divSearchField" runat="server" visible="false">
                        <div class="divForm">
                            <div class="form-header">
                                <h4>&#10148;Search Category </h4>
                            </div>
                            <div class="form-panel">
                                <table style="width: 100%; margin-left: 150px">
                                    <tr>
                                        <th>From Date</th>
                                        <th>To Date</th>
                                        <th>Category</th>
                                        <th>Search By</th>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtFromDate1" runat="server" CssClass="txt" TabIndex="35"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtFromDate1_CalendarExtender" runat="server"
                                                BehaviorID="txtFromDate1_CalendarExtender" TargetControlID="txtFromDate1" Format="dd-MM-yyyy" />
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtToDate1" runat="server" CssClass="txt" TabIndex="36"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtToDate1_CalendarExtender" runat="server"
                                                BehaviorID="txtToDate1_CalendarExtender" TargetControlID="txtToDate1" Format="dd-MM-yyyy" />
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="37">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="MemberID">MemberID</asp:ListItem>
                                                <asp:ListItem Value="Name">Name</asp:ListItem>
                                                <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td style="width: 20%;">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" AutoPostBack="true" TabIndex="38" OnTextChanged="txtSearch_TextChanged" Enabled="true"></asp:TextBox>
                                        </td>

                                        <td style="width: 45%;"></td>
                                    </tr>
                                    </tr>
                                </table>
                                <center>
                 <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" TabIndex="39" CssClass="form-btn"  />
                                <asp:Button ID="btnDateWithCategory" runat="server" Text="Date With Category" OnClick="btnDateWithCategory_Click" TabIndex="40" CssClass="form-btn"  />
                                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" TabIndex="41" CssClass="form-btn"  />
                                <asp:Button ID="btnClearSearch" runat="server" Text="Clear" OnClick="btnClearSearch_Click" TabIndex="42" CssClass="form-btn"  />
             </center>
                            </div>

                            <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 50px;">

                                <asp:GridView ID="gvDiet" runat="server" AutoGenerateColumns="false" CellPadding="3" Font-Size="11px"
                                    DataKeyNames="Diet_AutoID" EmptyDataText="No record found." Width="1900px"
                                    PagerStyle-CssClass="pager" CssClass="GridView GridView1" GridLines="None"
                                    AllowPaging="True" PageSize="20">
                                    <Columns>
                                        <%--<asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit" >
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Diet_AutoID")%>' TabIndex="34"
                                 style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Diet_AutoID")%>' TabIndex="43"
                                                    Style="background-image: url('../NotificationIcons/edit.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnCommand="btnDelete_Command" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="35"
                                CommandArgument='<%#Eval("Diet_AutoID")%>' 
                                style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="43"
                                                    CommandArgument='<%#Eval("Diet_AutoID")%>' OnCommand="btnDelete_Command"
                                                    Style="background-image: url('../NotificationIcons/f-cross_256-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Member Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Member ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                                        <asp:TemplateField HeaderText="DietDate" ItemStyle-Width="180px" ControlStyle-Width="150px">
                                            <ItemTemplate>
                                                <%# Eval("DietDate","{0:dd-MM-yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Dietatian" DataField="Dietatian" HeaderStyle-HorizontalAlign="left" />

                                        <asp:TemplateField HeaderText="ToDate" ItemStyle-Width="150px" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <%# Eval("ToDate","{0:dd-MM-yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FromDate" ItemStyle-Width="150px" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <%# Eval("FromDate","{0:dd-MM-yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="PreHistory" DataField="PreHistory" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Avoid " DataField="Avoid" ItemStyle-Width="500px" />
                                        <asp:BoundField HeaderText="Meal1" DataField="Meal1Sta" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Time1" DataField="Time1" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Diet1" DataField="Meal1" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Meal2" DataField="Meal2Sta" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Time2" DataField="Time2" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Diet2" DataField="Meal2" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Meal3" DataField="Meal3Sta" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Time3" DataField="Time3" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Diet3" DataField="Meal3" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Meal4" DataField="Meal4Sta" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Time4" DataField="Time4" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Diet4" DataField="Meal4" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Meal5" DataField="Meal5Sta" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Time5" DataField="Time5" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Diet5" DataField="Meal5" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Meal6" DataField="Meal6Sta" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Time6" DataField="Time6" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Diet6" DataField="Meal6" HeaderStyle-HorizontalAlign="left" />

                                    </Columns>
                                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                    <RowStyle Height="20px" />
                                    <AlternatingRowStyle Height="20px" BackColor="White" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
