<%@ Page Title="" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSAddVendor.aspx.cs" Inherits="NDOnlineGym_2017.AddVendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>

    <style>
         /*.mydatagrid a:focus {  color: blue;cursor:pointer; }*/    /*FOR THE PAGING ICONS  HOVER STYLES*/
          /*.mydatagrid a:hover { background-color:none; color: blue; }*/  /* FOR THE PAGING ICONS  HOVER STYLES*/

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
             .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="sc">
    <div class="form-name-header">
        <h3>Add Vendor
            <div class="navigation">
                <ul>
                    <li>Vendor&nbsp; > &nbsp;</li>
                    <li>Add Vendor &nbsp; > &nbsp;</li>
                </ul>
            </div>
        </h3>
    </div>

    <div class="divForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--Member Details--%>

                <div class="form-header">
                    <h4>&#10148;Add Vendor </h4>
                </div>
                <div class="form-panel">
                    <table style="width: 100%;">
                        <tr>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Vendor ID </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtVendorID" runat="server" CssClass="txt" TabIndex="1" OnTextChanged="txtVendorID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCompanyID" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtVendorID"
                                                ErrorMessage="Enter Vendor ID" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Name </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtName"
                                             ErrorMessage="Enter Name" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Contact 1 </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtContact1" runat="server" CssClass="txt" TabIndex="3" onkeypress="return RestrictSpaceSpecial(event);" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rVLName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtContact1"
                                            ErrorMessage="Enter Contact" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>

                        <tr>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Contact 2 </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtContact2" runat="server" CssClass="txt" TabIndex="4"></asp:TextBox>

                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Address </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="txt" TabIndex="5"></asp:TextBox>

                                        </td>
                                    </tr>
                                </table>
                            </td> 

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">State </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtState" runat="server" CssClass="txt" TabIndex="6"></asp:TextBox>

                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>

                        <tr>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Pincode </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtPincode" runat="server" CssClass="txt" TabIndex="7"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">GST No. </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtGSTNo" runat="server" CssClass="txt" TabIndex="8"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>

                    </table>

                    <center class="btn-section">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="9" OnClick="btnSave_Click"
                            OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="10" OnClick="btnClear_Click" />                    
                    </center>

                </div>

            </ContentTemplate>      
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
            </Triggers>    --%> 
        </asp:UpdatePanel>

        <br />
        <div class="form-header">
            <h4 style="float: left;">&#10148; Search Category
                <div style="float: right; padding-right: 10px;"></div>
                <%--<h4></h4>--%>
            </h4>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="form-panel">
                  <table>
                    <tr>
                        <th>Form Date</th>
                        <th>To Date</th>
                        <th>Category</th>
                        <th>Search by</th>
                        <th></th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="28"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="29"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                              <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="10" AutoPostBack="true" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Vendor_ID">Vendor ID</asp:ListItem>
                                    <asp:ListItem Value="Name">Name</asp:ListItem>
                                    <asp:ListItem Value="GSTNo">GST No.</asp:ListItem>
                              </asp:DropDownList>
                        </td>
                        <td>
                              <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" CssClass="txt" Enabled="true" TabIndex="11"></asp:TextBox>
                        </td>
                        <td>
                              <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" TabIndex="12" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 50px;">

                    <asp:GridView ID="gvVender" runat="server" AutoGenerateColumns="false" DataKeyNames="Vendor_AutoID" EmptyDataText="No record found." Width="1000px"
                        CssClass="GridView" PagerStyle-CssClass="pager" PageSize="20" AllowPaging="True" GridLines="None" OnPageIndexChanging="gvVender_PageIndexChanging"
                         CellPadding="3" Font-Size="11px" >
                        <Columns>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" CommandArgument='<%#Eval("Vendor_AutoID")%>' OnCommand="btnEdit_Command" TabIndex="13"
                                        style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="13"
                                        CommandArgument='<%#Eval("Vendor_AutoID")%>' OnCommand="btnDelete_Command"
                                        style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"  ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>                           

                            <asp:BoundField HeaderText="Vendor ID" DataField="Vendor_ID" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Vendor Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="GSTNo" DataField="GSTNo" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Contact1 " DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Contact2" DataField="Contact2" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="State" DataField="State" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Pincode" DataField="Pincode" HeaderStyle-HorizontalAlign="left" />                            
                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <RowStyle Height="20px" />
                        <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>
                </div>
            </ContentTemplate>           
        </asp:UpdatePanel>

    </div>

</div>
</asp:Content>
