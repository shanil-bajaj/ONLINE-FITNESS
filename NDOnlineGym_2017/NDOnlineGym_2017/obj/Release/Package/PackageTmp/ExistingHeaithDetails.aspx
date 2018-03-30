<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ExistingHeaithDetails.aspx.cs" Inherits="NDOnlineGym_2017.ExistingHeaithDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <%-- <link rel="icon" type="image/png" href="Logo/NDLogo.png" />--%>
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script type="text/javascript">
        function capFirst(oTextBox) {
            oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
        }
    </script>
    <style>
        input:focus {
            border: 1px solid rgb(242, 137, 9);
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

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
                color: #fff;
                padding: 5px 5px 5px 5px;
            }

        .remove {
            font-size: 18px;
            font-weight: bold;
        }

        .GridView1 {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
            width: max-content;
        }

            .GridView1 a /** FOR THE PAGING ICONS  **/ {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
            }

                .GridView1 a:focus {
                    color: orangered;
                }

                .GridView1 a:hover {
                    color: orangered;
                }

            .GridView1 span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .hideGridColumn {
            display: none;
        }

        .GridView a:focus {
            color: orangered;
        }

        .GridView a:hover {
            color: orangered;
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

        function RestrictSpaceSpecial(e) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>

    <script>
        function showConfirmation(myurl) {
            if (confirm("Do You Want To Edit ?") == true) {
                window.parent.location.href = myurl;
            }
        }

        function showConfirmation1(myurl) {
            if (confirm("Do You Want To Continue ?") == true) {
                window.parent.location.href = myurl;
            }
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="sc">
            <div class="divForm">
                <%--Member Details--%>
                <div class="form-header">
                    <h4>&#10148; Existing Health Details  </h4>
                </div>
                <div class="form-panel">

                    <table class="MemberDetails">
                        <tr>
                            <td class="section">
                                <strong>Member ID : </strong>
                                <asp:TextBox ID="txtMemberID" runat="server" onkeypress="return RestrictSpaceSpecial(event);" OnTextChanged="txtMemberID_TextChanged"
                                    AutoPostBack="true" AutoCompleteType="Disabled" TabIndex="1" width="150px"></asp:TextBox>
                            </td>
                            <td class="section">
                                <strong>Member Name : </strong>
                            </td>
                            <td>                                
                                <asp:DropDownList ID="ddlMemberName" CssClass="ddl" runat="server" TabIndex="2" width="200px" OnSelectedIndexChanged="ddlMemberName_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="section">
                                <strong>Contact No : </strong>
                                <asp:TextBox ID="txtContact" runat="server" onkeypress="return RestrictSpaceSpecial(event);" width="150px" OnTextChanged="txtContact_TextChanged"
                                    AutoPostBack="true" AutoCompleteType="Disabled" TabIndex="3"></asp:TextBox>
                            </td>
                            <td>
                                 <asp:Button ID="btnRefresh" runat="server" Text="Clear" CssClass="form-btn" OnClick="btnRefresh_Click" TabIndex="4" />
                            </td>
                        </tr>
                    </table>

                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 5px;">
                        <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </div>   
                        <asp:GridView ID="gvExistingHealthDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found." AllowPaging="True" PageSize="20"
                            TabIndex="110" PagerStyle-CssClass="pager" CssClass="GridView1" GridLines="None" CellPadding="3" Font-Size="11px" 
                            OnPageIndexChanging="gvExistingHealthDetails_PageIndexChanging" >
                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Member_AutoID1")%>' TabIndex="4" OnCommand="btnEdit_Command"
                                            style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Member_AutoID1")%>' OnClientClick="return confirm('Are you sure you want to delete?')" 
                                            TabIndex="4" OnCommand="btnDelete_Command" style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;
                                            padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPreview" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Member_ID1")%>' OnCommand="btnPreview_Command" TabIndex="4"
                                            style="background-image:url('../NotificationIcons/images (5).png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Preview" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ControlStyle-Width="75px" HeaderText="Status" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnStatus" runat="server" CausesValidation="false" Text='<%#Eval("Helstatus")%>' CommandArgument='<%#Eval("Member_AutoID1")%>' 
                                            OnCommand="btnStatus_Command" TabIndex="4" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:BoundField HeaderText="Status" DataField="Helstatus" HeaderStyle-HorizontalAlign="left" />--%>
                                <asp:BoundField HeaderText="Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Mem Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Doctor Care Status" DataField="DoctorCareStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Doctor Care Reason" DataField="DoctorCareReason" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="135px" />
                                <asp:BoundField HeaderText="Physical Examination" DataField="PhysicalExamination" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="135px" />
                                <asp:BoundField HeaderText="Exercise Stress Status" DataField="ExerciseStressStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Stress Result" DataField="StressResult" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Medication Status" DataField="MedicationStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Medication Reason" DataField="MedicationReason" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="135px" />
                                <asp:BoundField HeaderText="Hospitalized Status" DataField="HospitalizedStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Hospitalized Reason" DataField="HospitalizedReason" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="135px" />
                                <asp:BoundField HeaderText="Smoke Status" DataField="SmokeStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Pregnant Status" DataField="PregnantStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Alcohol Status" DataField="AlcoholStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Stress Level Status" DataField="StressLevelStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Moderately Status" DataField="ModeratelyStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="BP Status" DataField="BPStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Cholestrol Status" DataField="CholestrolStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Heart Disease Status" DataField="HeartDiseaseStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Rheumati Disease Status" DataField="RheumatiDiseaseStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Chest Pain Status" DataField="ChestPainStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Heart Beat Status" DataField="HeartBeatStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Faint Status" DataField="FaintStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Breath Status" DataField="BreathStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Cramping Status" DataField="CrampingStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Emphysema Status" DataField="EmphysemaStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Metabolic Status" DataField="MetabolicStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Epilepsy Status" DataField="EpilepsyStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Asthma Status" DataField="AsthmaStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Pain Status" DataField="PainStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Other Pain Status" DataField="OtherPainStatus" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Muscle Pain Status" DataField="MusclePainStatus" HeaderStyle-HorizontalAlign="left" />


                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>

                    </div>

                </div>
            </div>

        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
