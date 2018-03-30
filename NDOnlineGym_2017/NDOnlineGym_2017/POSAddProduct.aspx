<%@ Page Title="" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSAddProduct.aspx.cs" Inherits="NDOnlineGym_2017.POSAddProduct" %>
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
            <h3>Add Product
                 <div class="navigation" >
                    <ul>
                        <li>Inventory &nbsp; > &nbsp;</li>
                        <li>Add Product &nbsp; > &nbsp;</li>
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
            <h4>&#10148;Add Product </h4>
        </div>
        <div class="form-panel">
            <table style="width:100%;">
                 <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Product Code </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtProductCode" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>
                               <asp:RequiredFieldValidator  ID="rfvCompanyID" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtProductCode"
                                ErrorMessage="Enter Member ID"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>

                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Product Name </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtProductName" runat="server" CssClass="txt" TabIndex="2" ></asp:TextBox>
                                <asp:RequiredFieldValidator  ID="rfvName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtProductName"
                                ErrorMessage="Enter First Name"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                            </td>
                         </tr></table>
                    </td>

                      <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> UOM</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtUOM" runat="server" CssClass="txt" TabIndex="3" ></asp:TextBox>
                              
                            </td>
                         </tr></table>
                    </td>

                   
                   
                </tr>

                  <tr>
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Quantity </span></td>
                            <td style="width:55%;text-align:left;">
                             <asp:TextBox ID="txtQuantity" runat="server" CssClass="txt" TabIndex="4"></asp:TextBox>
                            
                            </td>
                         </tr></table>
                    </td>
                  
                    <td class="cols">
                         <table><tr>
                            <td style="width:45%;"><span class="lbl">Purchase Rate </span></td>
                            <td style="width:55%;text-align:left;">
                             <asp:TextBox ID="txtPurchaseRate" runat="server" CssClass="txt" TabIndex="5"></asp:TextBox>
                               
                            </td>
                         </tr></table>
                    </td>       
                      
                        <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> Selling Rate </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtSellingRate" runat="server" CssClass="txt" TabIndex="6" ></asp:TextBox>
                               
                            </td>
                         </tr></table>
                    </td>            
                </tr>
                </table>
            </div>
                <div class="form-header">
            <h4>&#10148; GST Details </h4>
        </div>
                 <div class="form-panel">
            <table style="width:100%;">
                 <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> GST % </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtGst" runat="server" CssClass="txt" TabIndex="7" OnTextChanged="txtGst_TextChanged" AutoPostBack="true" ></asp:TextBox>
                              
                            </td>
                         </tr></table>
                    </td>

                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> CGST % </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtCGSTNo" runat="server" CssClass="txt" TabIndex="8" Enabled="false"></asp:TextBox>
                           
                            </td>
                         </tr></table>
                    </td>

                      <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> SGST % </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtSGSTNo" runat="server" CssClass="txt" TabIndex="9" Enabled="false" ></asp:TextBox>
                             
                            </td>
                         </tr></table>
                    </td>
                               
                </tr>
                <tr>
                <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> IGST % </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtIGSTNo" runat="server" CssClass="txt" TabIndex="10" ></asp:TextBox>
                             
                            </td>
                            </tr>
                           
                        
                        </table>
                    </td>     
                    
                       <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> HSN Code </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txthsnCode" runat="server" CssClass="txt" TabIndex="11" ></asp:TextBox>
                             
                            </td>
                            </tr>
                          
                        
                        </table>
                    </td>                           
                </tr>

                </table>

              <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="12" OnClick="btnSave_Click"  />
                <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="form-btn" TabIndex="13"/>
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
            <%--<table style="width:100%;">
                <tr>
                    <td style="width:20%;">
                       
                    </td>
                    <td style="width:20%;">
                        
                    </td>
                    <td style="width:45%;">
                       
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
                              <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="13" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                    <asp:ListItem Value="ProductCode">Product Code</asp:ListItem>
                                    <asp:ListItem Value="ProductName">Product Name</asp:ListItem>
                                    <asp:ListItem Value="HSNCode">HSNCode</asp:ListItem>
                                    <asp:ListItem Value="GST">GST %</asp:ListItem>
                              </asp:DropDownList>
                        </td>
                        <td>
                                 <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" CssClass="txt" Enabled="True" TabIndex="14"></asp:TextBox>
                        </td>
                        <td>
                                  <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" TabIndex="15 " OnClick="btnSearch_Click"/>
                        </td>
                    </tr>
                </table>
        </div>

   <div style="width:1000px;height:auto;overflow-x:scroll;margin-top:50px;">
              
                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" DataKeyNames="Auto_ID" EmptyDataText="No record found." Width="700px" 
                    CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" PageSize="20"  RowStyle-CssClass="gv-rows" AllowPaging="True"
                     CellPadding="3" Font-Size="11px" >

                    <Columns>                  
                                                      
                        <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Auto_ID")%>' TabIndex="5"
                                         style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="5"
                                        CommandArgument='<%#Eval("Auto_ID")%>' OnCommand="btnDelete_Command"
                                        style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>  
           
                    <asp:BoundField HeaderText="Product Code" DataField="ProductCode" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Product Name " DataField="ProductName" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Quantity" DataField="Quantity" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="UOM" DataField="UOM" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="HSNCode" DataField="HSNCode" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Purchase Rate" DataField="PurchaseRate" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="SellingRate" DataField="SellingRate" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="GST" DataField="GST" HeaderStyle-HorizontalAlign="left" />              
                    <asp:BoundField HeaderText="CGST" DataField="CGST" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="SGST" DataField="SGST" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="IGST" DataField="IGST" HeaderStyle-HorizontalAlign="left" />
                  
                     
                </Columns>
                </asp:GridView>
             </div>
                </ContentTemplate>
            </asp:UpdatePanel>       

        </div>
</div>
</asp:Content>
