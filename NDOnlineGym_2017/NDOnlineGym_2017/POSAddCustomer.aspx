
<%@ Page Title="" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSAddCustomer.aspx.cs" Inherits="NDOnlineGym_2017.AddCustomer" %>
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
            <h3>Add Customer
                 <div class="navigation" >
                    <ul>
                        <li>Customer&nbsp; > &nbsp;</li>
                        <li>Add Customer &nbsp; > &nbsp;</li>
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
            <h4>&#10148;Add Customer </h4>
        </div>
        <div class="form-panel">
            <table style="width:100%;">
                 <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Customer ID </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtCustomerID" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                               <asp:RequiredFieldValidator  ID="rfvCompanyID" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtCustomerID"
                                ErrorMessage="Enter Member ID"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>

                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Name </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtName" runat="server" CssClass="txt" TabIndex="2" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtName"
                                ErrorMessage="Enter First Name"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Contact 1 </span></td>
                            <td style="width:55%;text-align:left;">
                             <asp:TextBox ID="txtContact1" runat="server" CssClass="txt" TabIndex="4"></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rVLName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtContact1"
                                ErrorMessage="Enter Last Name"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>
                   
                </tr>

                  <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> Contact 2 </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtContact2" runat="server" CssClass="txt" TabIndex="5" ></asp:TextBox>
                              
                            </td>
                         </tr></table>
                    </td>

                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> Address </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="txt" TabIndex="6" ></asp:TextBox>
                               
                            </td>
                         </tr></table>
                    </td>
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">State </span></td>
                            <td style="width:55%;text-align:left;">
                             <asp:TextBox ID="txtState" runat="server" CssClass="txt" TabIndex="7"></asp:TextBox>
                               
                            </td>
                         </tr></table>
                    </td>                   
                </tr>

                 <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> Pincode </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtPincode" runat="server" CssClass="txt" TabIndex="8" ></asp:TextBox>
                              
                            </td>
                         </tr></table>
                    </td>

                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> GST No. </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtGSTNo" runat="server" CssClass="txt" TabIndex="9" ></asp:TextBox>

                            </td>
                         </tr></table>
                    </td>
                               
                </tr>

                </table>
             <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="10" OnClick="btnSave_Click"  />
                <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="form-btn" TabIndex="11" OnClick="btnCancel_Click"  />
                <%-- <asp:Button ID="btnView" runat="server"  Text="View" CssClass="form-btn" OnClick="btnview_Click" />--%>

             </center>
            </div>

                  <div class="form-header">
               <h4  style="float:left;">&#10148; Search Category
                <div style="float:right;padding-right:10px;">
               
                </div>                


            </h4>
        </div>
    

    <div class="form-panel">
           <%-- <table style="width:100%;">
                <tr>
                    <td style="width:20%;">
                       
                    </td>
                    
                    </tr>
                </table>--%>
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
                             <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="12" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" AutoPostBack="true">
                                 <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                 <asp:ListItem Value="All">All</asp:ListItem>
                                 <asp:ListItem Value="Customer_ID">Customer ID</asp:ListItem>
                                 <asp:ListItem Value="Name">Name</asp:ListItem>                           
                                 <asp:ListItem Value="Contact1">Contact 1</asp:ListItem>                           
                                 <asp:ListItem Value="GSTNo">GST No</asp:ListItem>                           
                             </asp:DropDownList>
                        </td>
                        <td>
                                 <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" CssClass="txt" Enabled="True" TabIndex="13"></asp:TextBox>
                        </td>
                        <td>
                                 <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" TabIndex="14" OnClick="btnSearch_Click"/>
                        </td>
                    </tr>
                </table>
        </div>

   <div style="width:1000px;height:auto;overflow-x:scroll;margin-top:50px;">
              
             <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="false"  DataKeyNames="Auto_ID" EmptyDataText="No record found." Width="700px"
                  CellPadding="3" Font-Size="11px" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" PageSize="20"  
                 RowStyle-CssClass="gv-rows" AllowPaging="True" >

                    <Columns>     
                         <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Auto_ID")%>' TabIndex="5"
                                     style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="5"
                                        CommandArgument='<%#Eval("Auto_ID")%>' OnCommand="btnDelete_Command"
                              style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                    <asp:BoundField HeaderText="Name " DataField="Name" HeaderStyle-HorizontalAlign="left" />             
                    <asp:BoundField HeaderText="Customer ID " DataField="Customer_ID" HeaderStyle-HorizontalAlign="left" />             
                    <asp:BoundField HeaderText="Contact 1 " DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Contact 2" DataField="Contact2" HeaderStyle-HorizontalAlign="left" />                   
                    <asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-HorizontalAlign="left" />                   
                    <asp:BoundField HeaderText="State" DataField="State" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Pincode" DataField="Pincode" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="GSTNo" DataField="GSTNo" HeaderStyle-HorizontalAlign="left" />                 
                    
                </Columns>
                </asp:GridView>
             </div>

                </ContentTemplate>
            </asp:UpdatePanel>

          </div>

          </div>
</asp:Content>
