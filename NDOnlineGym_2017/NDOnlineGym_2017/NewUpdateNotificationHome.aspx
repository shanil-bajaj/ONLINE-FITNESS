<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="NewUpdateNotificationHome.aspx.cs" Inherits="NDOnlineGym_2017.NewUpdateNotificationHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
     <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <script>
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgUpdate.ClientID%>').prop('src', e.target.result)
                };
                reader.readAsDataURL(input.files[0]);
                }
            }

    </script>

    <script type="text/javascript">
        function LimtCharacters(txtNotification, CharLength, indicator) {
            chars = txtNotification.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtNotification.value = txtNotification.value.substring(0, CharLength);
            }
        }
</script>
    <script type="text/javascript">
        function ShowCharCount() {
            document.getElementById("cntCharacter").innerHTML = document.getElementById("txtNotification").value.length;
        }
     </script>

    <style>
        .txt1 {
            width: 500px;resize:none;
        }
        .auto-style1 {
            width: 14%;
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
            input:focus { border:1px solid orangered }
        .btn-remove { background-color:rgb(248, 45, 70);color:white;border:1px solid rgb(248, 45, 70);margin-top:3px;  }
            .btn-remove:focus { border:1px solid black;cursor:pointer  }
        .btn-file:focus  { border:1px solid silver;cursor:pointer  }
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
    <div class="form-name-header">
        <h3>New Updates
            <div class="navigation">
                <ul>
                    <li>File &nbsp; > &nbsp;</li>
                    <li>New Updates</li>
                </ul>
            </div>
        </h3>
    </div>
    <div class="divForm">
        <%--Quick SMS--%>

        <div class="form-header" id="formheader1">
            <h4>&#10148;New Updates</h4>            
        </div>

        <div class="form-panel" id="formpanel1">

            <table style="margin-left:110px;">           
                            <tr>
                                <td><span class="lbl"><span class="error">*</span>Title</span></td>
                                <td>                                    
                                   
                                    <asp:TextBox ID="txtHeading" runat="server" style="padding:3px;margin-top:10px" TabIndex="1" ValidationGroup="QuickSms" ></asp:TextBox>
                                   
                                    <asp:RequiredFieldValidator ID="rfvtxtHeading" runat="server" ErrorMessage="Enter Heading" SetFocusOnError="true"
                                        ControlToValidate="txtHeading" CssClass="ErrorBox" ValidationGroup="QuickSms"></asp:RequiredFieldValidator>
                                </td>
                                <td><span class="lbl"><span class="error">*</span>Date</span></td>
                                <td>                                    
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="18"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                <asp:RequiredFieldValidator ID="rfvtxtFromDate" runat="server" ErrorMessage="Enter Date" SetFocusOnError="true"
                                        ControlToValidate="txtFromDate" CssClass="ErrorBox" ValidationGroup="QuickSms"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>   
            </table>
            <table  style="margin-left:110px;">                                             
                            <tr>
                                  
                                <td style="width:40%;margin-left:100px ">
                                    <span style="padding-right:10px;font-size:12.5px;">Image</span><asp:FileUpload ID="FileUploadeImg" onchange="ShowImagePreview(this);" 
                                        runat="server" TabIndex="2" CssClass="btn-file"  />
                                </td>
                                <td  style="width:10%; ">
                                   <div style="width:90px;height:70px;border:1px solid silver;">
                                       <asp:Image ID="imgUpdate" runat="server" ImageUrl="Icons/IdproofLogo.png" style="width:90px;height:70px;border:1px solid silver;"
                                        class="fileupload-preview thumbnail" /></div>
                                    <asp:Button runat="server"  ID="btnRemove" Text="Remove"  TabIndex="3"
                                        style="" CssClass="btn-remove"/>
                                </td>
                                
                                <td>

                                </td>
                            </tr>
                            
                        
            </table>

            <table style="width: 100%;">
                <tr>
                    <td style="width:15%; vertical-align:top;"><span class="lbl"><span class="error">*</span>Update  Message</span></td>
                    <td style="width: 85%;">                                                
                       <%-- <asp:TextBox ID="txtUpdateMsg" runat="server" CssClass="txt1" TabIndex="4" TextMode="MultiLine" onkeyup="CountCharacters(this,'cntCharacter');"></asp:TextBox>                          
                        <br />
                        <asp:RequiredFieldValidator ID="rfvtxtUpdateMsg" runat="server" ErrorMessage="Enter Message Here" SetFocusOnError="true"
                            ControlToValidate="txtUpdateMsg" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="QuickSms"></asp:RequiredFieldValidator>
                        <h4>Count :
                            <label id="cntCharacter" style="background-color: #E2EEF1; color: Red; font-weight: bold;">0</label>                            
                        </h4>--%>
                        <asp:TextBox ID="txtNotification" runat="server" TextMode="MultiLine" TabIndex="4" Width="450" Height="90px" 
                                     onkeyup="LimtCharacters(this,2000,'lblcount');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Number of Characters Left:
                                    <label id="lblcount" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label>  
                        <asp:RequiredFieldValidator ID="rfvtxtNotification" runat="server" ErrorMessage="Enter Message Here" SetFocusOnError="true"
                            ControlToValidate="txtNotification" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="QuickSms"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style1"></td>
                    <td style="width: 85.7%;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="QuickSms" TabIndex="5" OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="6" OnClick="btnClear_Click"/> 
                    </td>
                </tr>
            </table>
            <div class="form-header" id="divformheader" runat="server" style="margin-top:5px">
                    <h4 style="float: left;">&#10148; Search Category</h4>
                </div>
                <div class="form-panel" id="divformpanel" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 20%;">
                                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="7">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Heading">Heading</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%;">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="True" TabIndex="8"></asp:TextBox>
                            </td>
                            <td style="width: 45%;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" OnClick="btnSearch_Click" TabIndex="9" />
                            </td>
                        </tr>
                    </table>
                    <div></div>
                    <div style="width: 1000px; height: auto; overflow-x: scroll;">
                        <asp:GridView ID="gvNotification" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="SUpdate_AutoID" Width="1000px"
                            Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True"
                            OnPageIndexChanging="gvNotification_PageIndexChanging" PageSize="20">

                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command"
                                            CommandArgument='<%#Eval("SUpdate_AutoID")%>' TabIndex="10"  
                                            style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')"
                                             TabIndex="10" OnCommand="btnDelete_Command" CommandArgument='<%#Eval("SUpdate_AutoID")%>' 
                                            style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDate" runat="server" Text='<%#Eval("Date") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Heading" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtHeading" runat="server" Text='<%#Eval("Heading") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblHeading" runat="server" Text='<%#Eval("Heading") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Information" ControlStyle-Width="650px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtInformation" runat="server" Text='<%#Eval("Information") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblInformation" runat="server" Text='<%#Eval("Information") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>
                    </div>
                    <%--End Software handling--%>
                </div>
        </div>
        <%--End Individual--%>
    </div>
                </div>
            </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnSave" />
         </Triggers>
         </asp:UpdatePanel>
</asp:Content>
