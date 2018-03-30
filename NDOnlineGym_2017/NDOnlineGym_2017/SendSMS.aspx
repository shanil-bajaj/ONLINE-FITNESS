<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SendSMS.aspx.cs" Inherits="NDOnlineGym_2017.SendSMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <script src="JS/OfflineJavaScript.js"></script>
    <script src="JS/Enquiry.js"></script>

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
     <style>
        .txt1 { width:500px; }
    </style>
    <script>
        $(document).ready(function () {
            $("#formpanel1").show();
            $("#formpanel2").hide();
            $("#formpanel3").hide();
            $("#formpanel4").hide();
            $("#formpanel5").hide();
          

            $("#formheader1").click(function () {
                $("#formpanel1").toggle();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
               
            });
            $("#formheader2").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").toggle();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
              
            });
            $("#formheader3").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").toggle();
                $("#formpanel4").hide();
                $("#formpanel5").hide();
               
            });
            $("#formheader4").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").toggle();
                $("#formpanel5").hide();
              
            });
            $("#formheader5").click(function () {
                $("#formpanel1").hide();
                $("#formpanel2").hide();
                $("#formpanel3").hide();
                $("#formpanel4").hide();
                $("#formpanel5").toggle();
               
            });

        });
    </script>

    <script type="text/javascript">
        function CountCharacters(txtMsg, indicator) {
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = chars;

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

     <div class="form-name-header">
        <h3>Send SMS
            <div class="navigation" >
                <ul>
                    <li>File &nbsp; > &nbsp;</li>
                    <li>SMS &nbsp; > &nbsp;</li>
                    <li>Send SMS</li>
                </ul>
            </div>
        </h3>
    </div>


    <div class="divForm">
        
       <%--Individual--%>
        <span class="error" style="font-size:12px;font-weight: bold;">Note:</span>       
        <span class="error" style="font-size:12px;">Length for 1 SMS 160 Character.</span><br />
        <span class="error" style="font-size:12px;padding-left:34px;">Send SMS when Internet is Connected.</span>        
 
        <div class="form-header" id="formheader1">
            <h4>&#10148; Individual</h4>            
        </div>
        <div class="form-panel" id="formpanel1">
            <h4>Send SMS To Individual Mobile Number</h4>
            <table style="width:100%;">
                
                <tr>
                    <td class="cols">                        
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Name</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:DropDownList ID="ddlName" runat="server" CssClass="ddl" TabIndex="1" AutoPostBack="true" ValidationGroup="Individual"
                                     OnSelectedIndexChanged="ddlName_SelectedIndexChanged" >
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>                       
                                <asp:RequiredFieldValidator  ID="rfvName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlName"
                                ErrorMessage="Select Name"  SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="Individual"></asp:RequiredFieldValidator>
                            </td>
                        </tr></table>
                    </td>

                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Contact Number</span></td>
                            <td style="width:55%;text-align:left;">                             
                                <asp:TextBox ID="txtContactnum" runat="server" CssClass="txt numericOnly" TabIndex="2"  pattern="[0-9]*" MaxLength="10"
                                    OnTextChanged="txtContactnum_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled" ></asp:TextBox>                             
                                <asp:RequiredFieldValidator  ID="RequiredFieldValidator5" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtContactnum"
                                    ErrorMessage="Enter Contact Number"  SetFocusOnError="true" ValidationGroup="Individual"></asp:RequiredFieldValidator>                                
                            </td>
                         </tr></table>
                    </td>
                    
                    <td class="cols">                        
                    </td>

                </tr>   
                  
             </table> 

            <table style="width:100%;">
                <tr>  
                   <td style="width:14%; vertical-align:top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>                     
                    <td style="width:85.9%;" >
                        <asp:TextBox ID="txtIndividual" runat="server" CssClass="txt1" TabIndex="3"  TextMode="MultiLine" onkeyup="CountCharacters(this,'lblIndividual');" ></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvtxtIndividual" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                            ControlToValidate="txtIndividual" Display="Dynamic" CssClass="ErrorBox"  ValidationGroup="Individual"></asp:RequiredFieldValidator>                       
                         
                           <%-- <label id="" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>--%>
                            <h4>Count : <label id="lblIndividual" >0</label></h4>
                    </td>
                </tr>
                <tr>
                    <td></td>                            
                    <td style="width:50%; ">
                        <asp:Button ID="btnSendIndividual" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="Individual" TabIndex="4" OnClick="btnSendIndividual_Click" />
                        <asp:Button ID="btnClearIndi" runat="server" Text="Clear" CssClass="form-btn" TabIndex="5" OnClick="btnClearIndi_Click" />                                                                     
                    </td>
                </tr>                                                                                                                    
            </table>                                                                      
        </div>

         
    <%--End Individual--%>

           
    <%--Group--%>             

        <div class="form-header" id="formheader2">
            <h4>&#10148; Group</h4>          
        </div>
        <div class="form-panel" id="formpanel2">
            <h4> Common SMS Send To Group Members</h4>
            
            <table style="width:100%;">                
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Status</span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:DropDownList ID="ddlGroupStatus" runat="server" CssClass="ddl" TabIndex="6" ValidationGroup="GroupStatus" >
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                               <asp:RequiredFieldValidator  ID="rfvddlGroupStatus" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlGroupStatus"
                                ErrorMessage="Select Group"  SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="GroupStatus"></asp:RequiredFieldValidator>
                            </td>
                        </tr></table>
                    </td>                    
                </tr>                     
            </table> 

            <table style="width:100%;">
                <tr>      
                    <td style="width:15%; vertical-align:top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>                 
                    <td style="width:85%;" >
                        <asp:TextBox ID="txtGroupSMS" runat="server" CssClass="txt1" TabIndex="7"  TextMode="MultiLine" onkeyup="CountCharacters(this,'lblCharCntGroup');"></asp:TextBox> 
                       <br/>
                        <asp:RequiredFieldValidator ID="rfvtxtGroupSMS" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                                ControlToValidate="txtGroupSMS" Display="Dynamic" CssClass="ErrorBox"  ValidationGroup="GroupStatus"></asp:RequiredFieldValidator>      
                        
                         <%-- <span> Number of Characters:
                            <label id="lblCharCntGroup" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>     --%>
                          <h4>Count : <label id="lblCharCntGroup" >0</label></h4>               
                    </td>
                </tr>
                <tr>
                    <td></td>                            
                    <td style="width:50%; ">
                        <asp:Button ID="btnSendGropuSMS" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="GroupStatus" TabIndex="8" OnClick="btnSendGropuSMS_Click" />
                        <asp:Button ID="btnClearGroup" runat="server" Text="Clear" CssClass="form-btn" TabIndex="9" OnClick="btnClearGroup_Click" />
                    </td>
                </tr>                       
                                                    
                                         
            </table>
        </div>       
            
    <%--End Group--%>


    <%--Enquiry--%>    

        <div class="form-header" id="formheader3">
            <h4>&#10148; Enquiry</h4>
        </div>
        <div class="form-panel" id="formpanel3">
            <h4>Common SMS Send To Enquiry Members</h4>
            <table style="width:100%;">
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"><span class="error">*</span>Status</span></td>                            
                            <td style="width:55%;text-align:left;">
                                <asp:DropDownList ID="ddlEnquiry" runat="server" CssClass="ddl" TabIndex="10" ValidationGroup="Enquiry" >
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                    <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                    <asp:ListItem Value="Cold">Cold</asp:ListItem>
                                    <asp:ListItem Value="Expected">Expected</asp:ListItem>
                                    <asp:ListItem Value="Not Interested">Not Interested</asp:ListItem>
                                </asp:DropDownList>  
                                <br/>
                                <asp:RequiredFieldValidator  ID="rfvddlEnquiry" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlEnquiry"
                                ErrorMessage="Select Enquiry"  SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="Enquiry"></asp:RequiredFieldValidator>                             
                            </td>
                        </tr></table>
                    </td>
                </tr>
            </table>

            <table style="width:100%;">
                <tr>      
                    <td style="width:15%; vertical-align:top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>                   
                    <td style="width:85%;" >
                        <asp:TextBox ID="txtEnquiry" runat="server" CssClass="txt1" TabIndex="11"  TextMode="MultiLine" onkeyup="CountCharacters(this,'lblCharCntEnquiry');" ></asp:TextBox>    
                        <br/>
                        <asp:RequiredFieldValidator ID="rfvtxtEnquiry" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                                    ControlToValidate="txtEnquiry" Display="Dynamic" CssClass="ErrorBox"  ValidationGroup="Enquiry"></asp:RequiredFieldValidator>
                        
                         <%-- <span> Number of Characters:
                            <label id="lblCharCntEnquiry" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>  --%> 
                        
                         <h4>Count : <label id="lblCharCntEnquiry" >0</label></h4>                         
                    </td>
                </tr>
                <tr>
                    <td></td>                            
                    <td style="width:50%; ">
                        <asp:Button ID="btnSendEnquiry" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="Enquiry" TabIndex="12" OnClick="btnSendEnquiry_Click" />
                        <asp:Button ID="btnClearEnquiry" runat="server" Text="Clear" CssClass="form-btn" TabIndex="13" OnClick="btnClearEnquiry_Click" />
                    </td>
                </tr>                                                                                                                    
            </table>

        </div>
                    
    <%--End Enquiry--%>

    <%--Balance--%>       

        <div class="form-header" id="formheader4">
            <h4>&#10148; Balance</h4>
        </div>

        <div class="form-panel" id="formpanel4">
            <h4>Common SMS Send To Balance Payment Members</h4>
            <table style="width:100%;">
                <tr>    
                    <td style="width:15%; vertical-align:top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>                      
                    <td style="width:85%;" >
                        <asp:TextBox ID="txtContentBalance" runat="server" CssClass="txt1" TabIndex="14"  TextMode="MultiLine" ValidationGroup="Balance" onkeyup="CountCharacters(this,'lblCharCntBalance');" ></asp:TextBox> 
                        <br/>
                        <asp:RequiredFieldValidator ID="rfvtxtContentBalance" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                            ControlToValidate="txtContentBalance" Display="Dynamic" CssClass="ErrorBox"  ValidationGroup="Balance"></asp:RequiredFieldValidator>
                         
                        <%--  <span> Number of Characters:
                            <label id="lblCharCntBalance" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>--%>

                        <h4>Count : <label id="lblCharCntBalance" >0</label></h4>  
                    </td>
                </tr>
                <tr>
                    <td></td>                            
                    <td style="width:50%; ">
                        <asp:Button ID="btnBalance" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="Balance" TabIndex="15" OnClick="btnBalance_Click"/>
                        <asp:Button ID="btnClearBalance" runat="server" Text="Clear" CssClass="form-btn" TabIndex="16" OnClick="btnClearBalance_Click" />
                    </td>
                </tr>                                                                                                                    
            </table>
        </div>
        
    <%--End Balance--%>


    <%--Gender--%>   

        <div class="form-header" id="formheader5">
            <h4>&#10148; Gender</h4>
        </div>
        
        <div class="form-panel" id="formpanel5">
            <h4>Common SMS Send To Group Members</h4>
                <table style="width:100%;">
                    <tr>
                        <td class="cols">
                            <table><tr>
                                <td style="width:45%;"><span class="lbl"><span class="error">*</span>Gender</span></td>
                                <td style="width:55%;text-align:left;">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="ddl" TabIndex="17">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                    </asp:DropDownList>  
                                    <br/>
                                    <asp:RequiredFieldValidator  ID="rfvddlGender" runat="server" ErrorMessage="Select Gender" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorBox" 
                                    ControlToValidate="ddlGender" InitialValue="--Select--"  ValidationGroup="Gender" ></asp:RequiredFieldValidator>                              
                                </td>
                            </tr></table>
                        </td>
                    </tr>

            </table>
            <table style="width:100%;">
                <tr>    
                    <td style="width:15%; vertical-align:top;"><span class="lbl"><span class="error">*</span>Text Message</span></td>                       
                    <td style="width:85%;" >
                        <asp:TextBox ID="txtGender" runat="server" CssClass="txt1" TabIndex="18"  TextMode="MultiLine" ValidationGroup="Gender" onkeyup="CountCharacters(this,'lblGender');" ></asp:TextBox> 
                        <br/> 
                        <asp:RequiredFieldValidator ID="rfvtxtGender" runat="server" ErrorMessage="Enter Message Here!" SetFocusOnError="true"
                            ControlToValidate="txtGender" Display="Dynamic" CssClass="ErrorBox"  ValidationGroup="Gender"></asp:RequiredFieldValidator>
                        
                         <%-- <span> Number of Characters:
                            <label id="lblGender" style="background-color:#E2EEF1;color:Red;font-weight:bold;" >0</label> </span>  --%>
                            <h4>Count : <label id="lblGender" >0</label></h4>                      
                    </td>
                </tr>
                <tr>
                    <td></td>                            
                    <td style="width:50%; ">
                        <asp:Button ID="btnGender" runat="server" Text="Send" CssClass="form-btn" ValidationGroup="Gender" TabIndex="19" OnClick="btnGender_Click" />
                        <asp:Button ID="btnClearGender" runat="server" Text="Clear" CssClass="form-btn" TabIndex="20" OnClick="btnClearGender_Click" />
                    </td>
                </tr>                                                                    
                                        
            </table>
        </div>
       
    <%--End Gender--%>

    </div>

       <%--     </ContentTemplate>
         </asp:UpdatePanel>--%>

</asp:Content>