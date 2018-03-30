<%@ Page Title="" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSInvoice.aspx.cs" Inherits="NDOnlineGym_2017.POSInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
        .top { width:100%; }
        .BillTo { width:50%; }
        .InvoiceInfo { width:50%; }
        .ddl { width:80%; }
        .inp-txt { width:80%; }
        legend { font-weight:bold;font-size:13px;color:rgb(128, 128, 128) }
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="sc">
<div class="form-name-header">
                <h3>Invoice
                 <div class="navigation">
                     <ul>
                         <li>Sales &nbsp; > &nbsp;</li>
                         <li>Invoice &nbsp; > &nbsp;</li>
                         <li>Invoice </li>
                     </ul>
                 </div>
              </h3>
</div>

    <div class="divForm">
        <table style="width:100%">
            <tr>
                <td>
                    <table class="top">
                        <tr>
                            <td class="BillTo">
                                 <fieldset>
                                    <legend >Bill To</legend>
                                    <table style="width:100%">
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>Customer Name</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:DropDownList ID="dllCustName" runat="server" style="width:80%">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>Address</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:TextBox ID="txtAddresss" runat="server" style="width:80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>Contact</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:TextBox ID="txtContact" runat="server" style="width:80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>Emai ID</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:TextBox ID="txtEmailId" runat="server" style="width:80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>GSTIN #</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:TextBox ID="txtBillGSTIn" runat="server" style="width:80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td class="InvoiceInfo">
                                <fieldset>
                                    <legend>Invoice Information</legend>
                                        <table style="width:100%">
                                    
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>Invoice Date</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:TextBox ID="txtInvoiceDate" runat="server" style="width:80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>Invoice No</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" style="width:80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>Company</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:DropDownList ID="ddlCompany" runat="server" style="width:80%">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>GSTIN #</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:TextBox ID="txtInvoiceGSTIn" runat="server" style="width:80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left">
                                                <span>State</span>
                                            </td>
                                            <td style="width:70%;text-align:left">
                                                <asp:TextBox ID="txtState" runat="server" style="width:80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <div class="divForm" style="margin-top:10px">
        <table >
            <tr>
                <th>Code</th>
                <th>Goods</th>
                <th>HSN/SAC</th>
                <th>QTY</th>
                <th>Rate</th>
                <th>Total</th>
                <th>Disc</th>
                <th>Taxable Amt</th>
                 <th>GST Type</th>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtCode" runat="server" style="width:80px;"></asp:TextBox></td>
                <td><asp:DropDownList ID="ddlGoods" runat="server" style="width:150px">
                        <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><asp:TextBox ID="TextBox1" runat="server" style="width:80px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox2" runat="server" style="width:70px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox3" runat="server" style="width:100px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox4" runat="server" style="width:120px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox5" runat="server" style="width:100px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox6" runat="server" style="width:100px;"></asp:TextBox></td>
                <td><asp:DropDownList ID="DropDownList3" runat="server" style="width:150px">
                        <asp:ListItem>Including</asp:ListItem>
                        <asp:ListItem>Excluding</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
  
    <div class="divForm" style="margin-top:10px">
        <table >
            <tr>
                <th>GST %</th>
                <th>GST Amt</th>
                <th>CGST %</th>
                <th>CGST Amt</th>
                <th>SGST %</th>
                <th>SGST Amt</th>
                <th>IGST %</th>
                <th>IGST Amt</th>
                <th>Amt With GST</th>
                <th>UOM</th>
                <th></th>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox7" runat="server" style="width:80px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox14" runat="server" style="width:100px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox8" runat="server" style="width:80px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox9" runat="server" style="width:100px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox10" runat="server" style="width:80px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox11" runat="server" style="width:100px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox12" runat="server" style="width:80px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox13" runat="server" style="width:100px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox15" runat="server" style="width:100px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox16" runat="server" style="width:75px;"></asp:TextBox></td>
                <td><asp:LinkButton ID="lbtnAddInvoice" runat="server" Text="+"
                    style="text-decoration:none;font-size:25px;font-weight:bold;margin-left:10px;color:rgb(25, 75, 17)"  ></asp:LinkButton></td>
            </tr>
        </table>
    </div>

    <div class="divForm" style="margin-top:10px">
        <table >
            <tr>
                <td>
                    <div style="width:1000px;height:150px;overflow-x:auto;overflow-y:auto;border:1px solid silver">
                          <asp:GridView ID="gvPackage" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="Branch_AutoID" EmptyDataText="No record found." Width="1000px"
                        CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="gv-header" RowStyle-CssClass="gv-rows" AllowPaging="True" TabIndex="15">
             
                    </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <div class="divForm" style="margin-top:10px">
        <table >
            <tr>
                <th>Amount</th>
                <th>GST</th>
                <th>Total</th>
                <th>Paid</th>
                <th>Pay Mode</th>
                <th>Pay Details</th>
                <th>Sale Person</th>
               
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox17" runat="server" style="width:120px;"></asp:TextBox><span style="margin-left:7px;font-weight:bold;font-size:14px">+</span></td>
                <td><asp:TextBox ID="TextBox18" runat="server" style="width:120px;"></asp:TextBox><span style="margin-left:7px;font-weight:bold;font-size:14px">=</span></td>
                <td><asp:TextBox ID="TextBox19" runat="server" style="width:120px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox20" runat="server" style="width:120px;"></asp:TextBox></td>
                <td><asp:DropDownList ID="DropDownList1" runat="server" style="width:150px">
                        <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><asp:TextBox ID="TextBox21" runat="server" style="width:150px;"></asp:TextBox></td>
                <td><asp:DropDownList ID="DropDownList2" runat="server" style="width:150px">
                        <asp:ListItem>--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
               
            </tr>
        </table>
    </div>

    <div class="divForm" style="margin-top:10px">
        <table >
            <tr>
                <th>Balance</th>
                <th>Balance Pay Date</th>
                <th>Pre Balance</th>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox22" runat="server" style="width:200px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox23" runat="server" style="width:200px;"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox24" runat="server" style="width:200px;"></asp:TextBox></td>
            </tr>
        </table>

         <center class="btn-section">
           <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="24" />
           <asp:Button ID="Button1" runat="server" Text="View" CssClass="form-btn" ValidationGroup="a" TabIndex="24" />
           <asp:Button ID="Button2" runat="server" Text="Print" CssClass="form-btn" ValidationGroup="a" TabIndex="24" />
           <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="25"   />
         </center>
    </div>
          </div>
</asp:Content>  
