<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AllMasters.aspx.cs" Inherits="NDOnlineGym_2017.AllMasters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $("#formpanel1").show();
            $("#formpanel2").hide();
            $("#formpanel3").hide();
            $("#formpanel4").hide();
            $("#formpanel5").hide();
            $("#formpanel6").hide();
            $("#formpanel7").hide();
            $("#formpanel8").hide();
            $("#formpanel9").hide();

            $("#formheader1").click(function () {
                $("#formpanel1").toggle();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
                $("#formpanel6").hide();
                $("#formpanel7").hide();
                $("#formpanel8").hide();
                $("#formpanel9").hide();
            });
            $("#formheader2").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").toggle();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
                $("#formpanel6").hide();
                $("#formpanel7").hide();
                $("#formpanel8").hide();
                $("#formpanel9").hide();
            });
            $("#formheader3").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").toggle();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
                $("#formpanel6").hide();
                $("#formpanel7").hide();
                $("#formpanel8").hide();
                $("#formpanel9").hide();
            });
            $("#formheader4").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").toggle();
                $("#formpanel5").hide();
                $("#formpanel6").hide();
                $("#formpanel7").hide();
                $("#formpanel8").hide();
                $("#formpanel9").hide();
            });
            $("#formheader5").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").toggle();
                $("#formpanel6").hide();
                $("#formpanel7").hide();
                $("#formpanel8").hide();
                $("#formpanel9").hide();
            });
            $("#formheader6").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
                $("#formpanel6").toggle();
                $("#formpanel7").hide();
                $("#formpanel8").hide();
                $("#formpanel9").hide();
            });
            $("#formheader7").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
                $("#formpanel6").hide();
                $("#formpanel7").toggle();
                $("#formpanel8").hide();
                $("#formpanel9").hide();
            });
            $("#formheader8").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
                $("#formpanel6").hide();
                $("#formpanel7").hide();
                $("#formpanel8").toggle();
                $("#formpanel9").hide();
            });

            $("#formheader9").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
                $("#formpanel6").hide();
                $("#formpanel7").hide();
                $("#formpanel8").hide();
                $("#formpanel9").toggle();
            });

        });
    </script>
    <style>
        .ErrorBox {
            position: absolute;
            z-index: 1;
            font-weight: normal;
            border-radius: 3px;
            box-shadow: 10px 10px 10px rgba(0, 0, 0, 0.5);
            padding: 4px 20px;
            color: #a94442;
            background-color: #f2dede;
            border: 1px solid #ebccd1;
            margin-left: -177px;
            margin-top: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



    <div class="form-name-header">
        <h3>Masters 
                <div class="navigation">
                    <ul>
                        <li>File &nbsp; > &nbsp;</li>
                        <li>Branch Setting &nbsp; > &nbsp;</li>
                        <li>Branch Information </li>
                    </ul>
                </div>
        </h3>
    </div>

    <div class="divForm">

        <%--Designation master--%>
        <asp:UpdatePanel ID="UpdatePanelDesig" runat="server">
            <ContentTemplate>
                <div class="form-header" id="formheader1">
                    <h4>&#10148; Designation Master  </h4>
                </div>
                <div class="form-panel" id="formpanel1">
                    <table style="width: 50%; float: left;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Designation </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDesignation"
                                                ErrorMessage="Enter Designation" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnSaveDesignation" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="2" OnClick="btnSaveDesignation_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="btnClearDesignation" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearDesignation_Click" />

                    <%--GridView--%>
                    <div style="width: 500px; height:200px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver; margin-left: 80px;">
                        <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="Desig_AutoID"
                            AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" OnPageIndexChanging="gvDesignation_PageIndexChanging" PageSize="20">

                            <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditDesig" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditDesig_Command" CommandArgument='<%#Eval("Desig_AutoID")%>' TabIndex="4" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteDesig" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("Desig_AutoID")%>' Text="Delete" OnCommand="btnDeleteDesig_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDesigbnation" runat="server" Text='<%#Eval("Name") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblDesigbnation" runat="server" Text='<%#Eval("Name") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Designation" DataField="" />--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <%--End GridView--%>
                </div>
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveDesignation" />
        </Triggers>
        </asp:UpdatePanel>
        <%--End Designation master--%>

        <%--Department master--%>
        <asp:UpdatePanel ID="UpdatePanelDept" runat="server">
            <ContentTemplate>
                <div class="form-header" id="formheader2">
                    <h4>&#10148; Department Master  </h4>
                </div>
                <div class="form-panel" id="formpanel2">
                    <table style="width: 50%; float: left;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Department </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDepartment" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDepartment"
                                                ErrorMessage="Enter Department" SetFocusOnError="true" ValidationGroup="dept"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnSaveDepartment" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="dept" TabIndex="2" OnClick="btnSaveDepartment_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="btnClearDepartment" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearDepartment_Click" />


                    <%--GridView--%>
                    <div style="width: 500px; height: auto; overflow-x: scroll; margin-top: 20px; border: 1px solid silver; margin-left: 80px;">
                        <asp:GridView ID="gvDepartment" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="Dept_AutoID"
                            AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" PageSize="20">

                            <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditDept" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditDept_Command" CommandArgument='<%#Eval("Dept_AutoID")%>' TabIndex="4" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteDept" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("Dept_AutoID")%>' Text="Delete" OnCommand="btnDeleteDept_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Department" DataField="Name" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <%--End GridView--%>
                </div>
            </ContentTemplate>
           <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveDepartment" />
        </Triggers>--%>
        </asp:UpdatePanel>
        <%--End Department master--%>

        <%--Enquiry type master--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-header" id="formheader3">
                    <h4>&#10148; Enquiry Type Master </h4>
                </div>
                <div class="form-panel" id="formpanel3">
                    <table style="width: 50%; float: left;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Enquiry Type </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtEnquiryType" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtEnquiryType" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtEnquiryType"
                                                ErrorMessage="Enter Enquiry Type" SetFocusOnError="true" ValidationGroup="EnqType"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnSaveEnquiryType" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="EnqType" TabIndex="2" OnClick="btnSaveEnquiryType_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="btnClearEnquiryType" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearEnquiryType_Click" />


                    <%--GridView--%>
                    <div style="width: 500px; height: auto; overflow-x: scroll; margin-top: 20px; border: 1px solid silver; margin-left: 80px;">
                        <asp:GridView ID="gvEnquiryType" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="EnqType_AutoID"
                            AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" PageSize="20">

                            <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditEnquiryType" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditEnquiryType_Command" CommandArgument='<%#Eval("EnqType_AutoID")%>' TabIndex="4" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteEnquiryType" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("EnqType_AutoID")%>' Text="Delete" OnCommand="btnDeleteEnquiryType_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Enquiry Type" DataField="Name" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <%--End GridView--%>
                </div>
            </ContentTemplate>
           <%-- <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveEnquiryType" />
            </Triggers>--%>
        </asp:UpdatePanel>
        <%--End Enquiry type master--%>

        <%--Enquiry For master--%>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="form-header" id="formheader4">
                    <h4>&#10148; Enquiry For Master </h4>
                </div>
                <div class="form-panel" id="formpanel4">
                    <table style="width: 50%; float: left;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Enquiry For </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtEnquiryFor" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtEnquiryFor" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtEnquiryFor"
                                                ErrorMessage="Enter Enquiry For" SetFocusOnError="true" ValidationGroup="EnqFor"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnSaveEnquiryFor" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="EnqFor" TabIndex="2" OnClick="btnSaveEnquiryFor_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="btnClearEnquiryFor" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearEnquiryFor_Click" />


                    <%--GridView--%>
                    <div style="width: 500px; height: auto; overflow-x: scroll; margin-top: 20px; border: 1px solid silver; margin-left: 80px;">
                        <asp:GridView ID="gvEnquiryFor" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="EnqFor_AutoID"
                            AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" PageSize="20">

                            <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditEnquiryFor" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditEnquiryFor_Command" CommandArgument='<%#Eval("EnqFor_AutoID")%>' TabIndex="4" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteEnquiryFor" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("EnqFor_AutoID")%>' Text="Delete" OnCommand="btnDeleteEnquiryFor_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Enquiry Type" DataField="Name" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <%--End GridView--%>
                </div>
            </ContentTemplate>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnSaveEnquiryFor" />
            </Triggers>--%>
        </asp:UpdatePanel>
        <%--End Enquiry For master--%>

        <%--Occcupation master--%>
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="form-header" id="formheader5">
                    <h4>&#10148; Occupation Master </h4>
                </div>
                <div class="form-panel" id="formpanel5">
                    <table style="width: 50%; float: left;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Occupation </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtOccupation" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtOccupation" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtOccupation"
                                                ErrorMessage="Enter Occupation" SetFocusOnError="true" ValidationGroup="Occupation"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnSaveOccupation" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="Occupation" TabIndex="2" OnClick="btnSaveOccupation_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="btnClearOccupation" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearOccupation_Click" />


                    <%--GridView--%>
                    <div style="width: 500px; height: auto; overflow-x: scroll; margin-top: 20px; border: 1px solid silver; margin-left: 80px;">
                        <asp:GridView ID="gvOccupation" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="Occupation_AutoID"
                            AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" PageSize="20">

                            <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditOccupation" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditOccupation_Command" CommandArgument='<%#Eval("Occupation_AutoID")%>' TabIndex="4" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteOccupation" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("Occupation_AutoID")%>' Text="Delete" OnCommand="btnDeleteOccupation_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Occupation" DataField="Name" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <%--End GridView--%>
                </div>
            </ContentTemplate>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnSaveOccupation" />
            </Triggers>--%>
        </asp:UpdatePanel>
        <%--End Occcupation master--%>

        <%--Shift master--%>
        <div class="form-header" id="formheader6">
            <h4>&#10148; Shift Master  </h4>
        </div>
        <div class="form-panel" id="formpanel6">
            <table style="width: 50%; float: left;">
                <tr>
                    <td class="cols">
                        <table>
                            <tr>
                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Shift</span></td>
                                <td style="width: 55%; text-align: left;">
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDepartment"
                                        ErrorMessage="Enter Shift" SetFocusOnError="true" ValidationGroup="f"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Button ID="Button9" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="f" TabIndex="2" />
            <asp:Button ID="Button10" runat="server" Text="Cancel" CssClass="form-btn" TabIndex="3" />
            <%--GridView--%>
            <div style="width: 500px; height: auto; overflow-x: scroll; margin-top: 20px; border: 1px solid silver; margin-left: 80px;">
                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="true" CellPadding="5" Width="700px" GridLines="Horizontal"
                    CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows"
                    AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" PageSize="20">
                </asp:GridView>
            </div>

            <%--End GridView--%>
        </div>
        <%--End Shift master--%>

        <%--Source Of Enquiry master--%>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="form-header" id="formheader7">
                    <h4>&#10148; Source Of Enquiry Master </h4>
                </div>
                <div class="form-panel" id="formpanel7">
                    <table style="width: 50%; float: left;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Source Of Enquiry </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtSourceOfEnquiry" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtSourceOfEnquiry" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtSourceOfEnquiry"
                                                ErrorMessage="Enter Source Of Enquiry" SetFocusOnError="true" ValidationGroup="SourceOfEnquiry"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnSaveSourceOfEnquiry" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="SourceOfEnquiry" TabIndex="2" OnClick="btnSaveSourceOfEnquiry_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="btnClearSourceOfEnquiry" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearSourceOfEnquiry_Click" />


                    <%--GridView--%>
                    <div style="width: 500px; height: auto; overflow-x: scroll; margin-top: 20px; border: 1px solid silver; margin-left: 80px;">
                        <asp:GridView ID="gvSourceOfEnquiry" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="SourceOfEnq_AutoID"
                            AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" PageSize="20">

                            <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditSourceOfEnquiry" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditSourceOfEnquiry_Command" CommandArgument='<%#Eval("SourceOfEnq_AutoID")%>' TabIndex="4" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteSourceOfEnquiry" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("SourceOfEnq_AutoID")%>' Text="Delete" OnCommand="btnDeleteSourceOfEnquiry_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Source Of Enquiry" DataField="Name" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <%--End GridView--%>
                </div>
            </ContentTemplate>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnSaveSourceOfEnquiry" />
            </Triggers>--%>
        </asp:UpdatePanel>
        <%--End Source Of Enquiry Master--%>

        <%--Muscular Group master--%>
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>    
        <div class="form-header" id="formheader8">
            <h4>&#10148; Muscular Group Master  </h4>
        </div>
        <div class="form-panel" id="formpanel8">
            <table style="width:100%;float:left;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Muscular Group </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtMuscularGroup" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvtxtMuscularGroup" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtMuscularGroup"
                                ErrorMessage="Enter Muscular Group"  SetFocusOnError="true" ValidationGroup="MuscularGroup"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
               </tr>
            </table>
            <div style="margin-left:485px;">
            <asp:Button ID="btnSaveMuscularGroup" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="MuscularGroup" TabIndex="2" OnClick="btnSaveMuscularGroup_Click" UseSubmitBehavior="false"/>
            <asp:Button ID="btnClearMuscularGroup" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearMuscularGroup_Click"/></div>
                           
            
           <div style="width:500px;height:auto;overflow-x:scroll;margin-top:20px;border:1px solid silver;margin-left:80px;">
                 <asp:GridView ID="gvMuscularGroup" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                      CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="MuscularGroup_AutoID"
                    AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" OnPageIndexChanging="gvMuscularGroup_PageIndexChanging" PageSize="20">

                     <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditMuscularGroup" runat="server" CausesValidation="false" Text="Edit" 
                                            CommandArgument='<%#Eval("MuscularGroup_AutoID")%>'
                                            OnCommand="btnEditMuscularGroup_Command" TabIndex="4"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteMuscularGroup" runat="server" CausesValidation="false" OnCommand="btnDeleteMuscularGroup_Command" Text="Delete"
                                           CommandArgument='<%#Eval("MuscularGroup_AutoID")%>' OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                          ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Muscular Group" DataField="Name" />
                            </Columns>
                 </asp:GridView>
           </div>
           
            
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
        <%--End Muscular Group master--%>

        <%--Workout NAme master--%>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>    
        <div class="form-header" id="formheader9">
            <h4>&#10148; Workout Name Master  </h4>
        </div>
        <div class="form-panel" id="formpanel9">
            <table style="width:100%;float:left;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Workout Name </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtWorkoutName" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvtxtWorkoutName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtWorkoutName"
                                ErrorMessage="Enter Workout Name"  SetFocusOnError="true" ValidationGroup="WorkoutName"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
               
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Muscular Group </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:DropDownList ID="ddlMuscularGroup" runat="server" CssClass="ddl" TabIndex="6">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator  ID="rfvddlMuscularGroup" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlMuscularGroup"
                                ErrorMessage="Select Muscular Group"  SetFocusOnError="true" ValidationGroup="WorkoutName" InitialValue="--Select--"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                </tr>
            </table>
            <div style="margin-left:485px;">
                
             <asp:Button ID="btnSaveWorkoutName" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="WorkoutName" TabIndex="2" OnClick="btnSaveWorkoutName_Click" UseSubmitBehavior="false"/>
            <%--<asp:Button ID="btnClearWorkoutName" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearWorkoutPlan_Click" />--%></div>
                           
            
           <div style="width:500px;height:auto;overflow-x:scroll;margin-top:20px;border:1px solid silver;margin-left:80px;">
                 <asp:GridView ID="gvWorkoutName" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                      CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="WorkoutName_AutoID"
                    AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1"  PageSize="20" OnPageIndexChanging="gvWorkoutName_PageIndexChanging">

                     <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditWorkoutName" runat="server" CausesValidation="false" Text="Edit" CommandArgument='<%#Eval("WorkoutName_AutoID")%>'
                                             OnCommand="btnEditWorkoutName_Command" TabIndex="4"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteWorkoutName" runat="server" CausesValidation="false" OnCommand="btnDeleteWorkoutName_Command" CommandArgument='<%#Eval("WorkoutName_AutoID")%>'
                                            OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                          ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Workout Plan" DataField="Name" />
                            </Columns>
                 </asp:GridView>
           </div>
           
            
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
        <%--End NAme master--%>

        

        <%--Exercise Day master--%>
        <%--<asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>    
        <div class="form-header" id="formheader9">
            <h4>&#10148; Muscular group Master  </h4>
        </div>
        <div class="form-panel" id="formpanel9">
            <table style="width:50%;float:left;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Muscular group </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtExerciseDay" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvtxtExerciseDay" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtExerciseDay"
                                ErrorMessage="Enter Exercise Day"  SetFocusOnError="true" ValidationGroup="ExerciseDay"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSaveExerciseDay" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="ExerciseDay" TabIndex="2"  UseSubmitBehavior="false"/>
            <asp:Button ID="btnClearExerciseDay" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3"   />
                           
           
           <div style="width:500px;height:auto;overflow-x:scroll;margin-top:20px;border:1px solid silver;margin-left:80px;">
                 <asp:GridView ID="gvExerciseDay" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                      CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="ExerciseDay_AutoID"
                    AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1"  PageSize="10">

                     <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditExerciseDay" runat="server" CausesValidation="false" Text="Edit"  TabIndex="4"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteExerciseDay" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                             Text="Delete" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Exercise Day" DataField="Name" />
                            </Columns>
                 </asp:GridView>
           </div>
          
            
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
        <%--Exercise Day master--%>

        <%--Exercise master--%>
        <%--<asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>    
        <div class="form-header" id="formheader10">
            <h4>&#10148; Exercise Master  </h4>
        </div>
        <div class="form-panel" id="formpanel10">
            <table style="width:50%;float:left;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Exercise </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtExercise" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvExercise" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtExercise"
                                ErrorMessage="Enter Exercise"  SetFocusOnError="true" ValidationGroup="Exercise"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSaveExercise" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="Exercise" TabIndex="2" OnClick="btnSaveExercise_Click" UseSubmitBehavior="false"/>
            <asp:Button ID="btnClearExercise" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearExercise_Click"  />
                           
            
           <div style="width:500px;height:auto;overflow-x:scroll;margin-top:20px;border:1px solid silver;margin-left:80px;">
                 <asp:GridView ID="gvExercise" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                      CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="Exercise_AutoID"
                    AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" OnPageIndexChanging="gvExercise_PageIndexChanging" PageSize="10">

                     <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditExercise" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditExercise_Command" CommandArgument='<%#Eval("Exercise_AutoID")%>' TabIndex="4"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteExercise" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("Exercise_AutoID")%>' Text="Delete" OnCommand="btnDeleteExercise_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Exercise" DataField="Name" />
                            </Columns>
                 </asp:GridView>
           </div>
          
            
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
        <%--End Designation master--%>

        <%--Diet Avoid master--%>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>    
        <div class="form-header" id="formheader10">
            <h4>&#10148; Diet Avoid Master  </h4>
        </div>
        <div class="form-panel" id="formpanel10">
            <table style="width:50%;float:left;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Diet Avoid  </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtDietAvoid" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvDietAvoid" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDietAvoid"
                                ErrorMessage="Enter Diet Avoid"  SetFocusOnError="true" ValidationGroup="DietAvoid"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSaveDietAvoid" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="DietAvoid" TabIndex="2" OnClick="btnSaveDietAvoid_Click" UseSubmitBehavior="false"/>
            <asp:Button ID="btnClearDietAvoid" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearDietAvoid_Click"  />
                           
            
           <div style="width:500px;height:auto;overflow-x:scroll;margin-top:20px;border:1px solid silver;margin-left:80px;">
                 <asp:GridView ID="gvDietAvoid" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                      CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="DietAvoid_AutoID"
                    AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" OnPageIndexChanging="gvDietAvoid_PageIndexChanging" PageSize="20">

                     <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditDietAvoid" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditDietAvoid_Command"
                                             CommandArgument='<%#Eval("DietAvoid_AutoID")%>' TabIndex="4"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteDietAvoid" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("DietAvoid_AutoID")%>' Text="Delete" OnCommand="btnDeleteDietAvoid_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Diet Avoid" DataField="Name" />
                            </Columns>
                 </asp:GridView>
           </div>
           
            
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
        <%--End Designation master--%>

        <%--Diet Declaration master--%>
        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>    
        <div class="form-header" id="formheader11">
            <h4>&#10148; Diet Declaration Master  </h4>
        </div>
        <div class="form-panel" id="formpanel11">
            <table style="width:50%;float:left;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Diet Declaration </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtDietDeclaration" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvDietDeclaration" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDietDeclaration"
                                ErrorMessage="Enter Diet Declaration"  SetFocusOnError="true" ValidationGroup="DietDeclaration"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSaveDietDeclaration" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="DietDeclaration" TabIndex="2" OnClick="btnSaveDietDeclaration_Click" UseSubmitBehavior="false"/>
            <asp:Button ID="btnClearDietDeclaration" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearDietDeclaration_Click"  />
                           
            
           <div style="width:500px;height:auto;overflow-x:scroll;margin-top:20px;border:1px solid silver;margin-left:80px;">
                 <asp:GridView ID="gvDietDeclaration" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                      CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="DietDeclaration_AutoID"
                    AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" OnPageIndexChanging="gvDietDeclaration_PageIndexChanging" PageSize="10">

                     <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditDietDeclaration" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditDietDeclaration_Command" CommandArgument='<%#Eval("DietDeclaration_AutoID")%>' TabIndex="4"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteDietDeclaration" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("DietDeclaration_AutoID")%>' Text="Delete" OnCommand="btnDeleteDietDeclaration_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Diet Declaration" DataField="Name" />
                            </Columns>
                 </asp:GridView>
           </div>
           
            
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
        <%--End Designation master--%>

        <%--Payment Mode master--%>
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>    
        <div class="form-header" id="formheader12">
            <h4>&#10148; Payment Detail Master  </h4>
        </div>
        <div class="form-panel" id="formpanel12">
            <table style="width:50%;float:left;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Payment Detail </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtPaymentDetail" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvPaymentDetail" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtPaymentDetail"
                                ErrorMessage="Enter Payment Detail"  SetFocusOnError="true" ValidationGroup="PaymentDetail"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSavePaymentDetail" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="PaymentDetail" TabIndex="2" OnClick="btnSavePaymentDetail_Click" UseSubmitBehavior="false"/>
            <asp:Button ID="btnClearPaymentDetail" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClearPaymentDetail_Click"  />
                           
            
           <div style="width:500px;height:auto;overflow-x:scroll;margin-top:20px;border:1px solid silver;margin-left:80px;">
                 <asp:GridView ID="gvPaymentDetail" runat="server" AutoGenerateColumns="false" CellPadding="5" Width="700px" GridLines="Horizontal"
                      CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" DataKeyNames="PaymentMode_AutoID"
                    AllowPaging="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" OnPageIndexChanging="gvPaymentDetail_PageIndexChanging" PageSize="10">

                     <Columns>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditPaymentDetail" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditPaymentDetail_Command" 
                                            CommandArgument='<%#Eval("PaymentMode_AutoID")%>' TabIndex="4"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeletePaymentDetail" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="4"
                                            CommandArgument='<%#Eval("PaymentMode_AutoID")%>' Text="Delete" OnCommand="btnDeletePaymentDetail_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Payment Mode" DataField="Name" />
                            </Columns>
                 </asp:GridView>
           </div>
           
            
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
        <%--Payment Mode master--%>
    </div>
</asp:Content>
