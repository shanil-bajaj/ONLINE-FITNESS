<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ExpenseVoucher.aspx.cs" Inherits="NDOnlineGym_2017.ExpenseVoucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />       
    <script src="JS/OfflineJavaScript.js"></script>
    
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <link href="CSS/Voucher.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("#btnPrint").click(function () {
                var contents = $("#dvContainerPrint").html();
                var frame1 = $('<iframe />');
                frame1[0].name = "frame1";
                frame1.css({ "position": "absolute", "top": "-1000000px" });
                $("body").append(frame1);
                var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
                frameDoc.document.open();
                //Create a new HTML document.
                frameDoc.document.write('<html><head><title></title>');
                frameDoc.document.write('</head><body>');
                //Append the external CSS file.
                frameDoc.document.write(' <link href="CSS/Voucher.css" rel="stylesheet" type="text/css" />');
                frameDoc.document.write("<html><head><style> #" + Voucher + "</style>")
                //Append the DIV contents.
                frameDoc.document.write(contents);
                frameDoc.document.write('</body></html>');
                frameDoc.document.close();
                setTimeout(function () {
                    window.frames["frame1"].focus();
                    window.frames["frame1"].print();
                    frame1.remove();
                }, 500);
            });
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:1021px">
      <div class="form-name-header">
            <h3>Authority Chart
                 <div class="navigation" >
                    <ul>
                        <li>Member Setting &nbsp; > &nbsp;</li>
                        <li>Status  &nbsp; > &nbsp;</li>
                        <li>Termination</li>
                    </ul>
                 </div>
            </h3>       
    </div>
  
        <div class="form-header">
            <h4>&#10148; Authority Chart  </h4>
        </div>

      <div id="dvContainerPrint" style="width:calc(100% ); overflow-x:scroll;">
    <div id="dvContainer" runat="server" visible="true">
        <div class="print-container" >
            <div class="print-section">
            <div class="grandParentContaniner" id="Voucher">
            <div class="parentContainer">

            <div class="Expensemain">
                <div class="Expenseheading"><center><h2><asp:Label ID="Expenseheading" runat="server" Text="Label">Expense Voucher</asp:Label></h2></center>
                  <asp:LinkButton ID="btnBack" runat="server" style="float:right;padding-right: 13px;" OnClick="btnBack_Click" CssClass="btnlink">Back</asp:LinkButton>
                 </div>
                <div id="Expenseadd">
                    <center><asp:Label ID="lblCompanyName"  runat="server" Text="ghfh gfhgf fg gfhg" ></asp:Label><br/><center>
                    <asp:Label ID="lblAdd1" runat="server" Text="yufh fgghf ghf"></asp:Label><br />
                    <asp:Label ID="lblAdd2" runat="server" Text="gfhg ghh g gh" ></asp:Label>
                    <center><asp:Label ID="lblMobile" runat="server" ></asp:Label></center>     
                </div>
                <table>
                <tr>
                    <td>
                        <div class="l1">
                            <div class="innerlbl">
                            <asp:Label ID="lblName" runat="server" ></asp:Label>
                            </div>   
                        </div>
                    </td>
                    <td>
                        <div class="voucher">
                            <table id="vouch">
                                <tr>
                                    <th><asp:Label ID="Label6" runat="server" Text="Voucher No"></asp:Label></th>
                                    <th><asp:Label ID="lblExpNo" runat="server" ></asp:Label></th>
                                </tr>
                                <tr>
                                    <th><asp:Label ID="Label8" runat="server" Text="Voucher Date"></asp:Label></th>
                                    <th><asp:Label ID="lblDate" runat="server" style="float:left; padding-left:12px;" CssClass="lbl-data"></asp:Label></th>
                                        
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                </table>
                <table>
                <tr>
                    <td>
                        <div class="bill">
                            <table id="billTable">
                                <tr>
                                    <th class="backcol th1"><center><asp:Label ID="Label10" runat="server" Text="DESCRIPTION"></asp:Label></center></th>
                                    <th class="backcol"><asp:Label ID="Label11" runat="server" Text="AMOUNT"></asp:Label></th>
                                </tr>
                                <tr>
                                    <th><asp:Label ID="lblNote1" Width="320px"  runat="server" Style="word-wrap: normal; word-break: break-all;"  MaxLength="100" class="na"></asp:Label></th>
                                    <th rowspan="2"><asp:Label ID="lblAmount" runat="server" ></asp:Label></th>
                                </tr>
                                <tr>
                                    <th><asp:Label ID="lblNote2" Width="320px" runat="server"  Style="word-wrap: normal; word-break: break-all;" MaxLength="100" class="na"></asp:Label></th>
                                   <%-- <th></th>--%>
                                </tr>
                                
                            </table>
                        </div>
                    </td>
                </tr>
                </table>
                <table>
                    <tr>
                       <td class="td1" style=""></td>
                       <td class="td2" style=""><h3><asp:Label ID="Label1" runat="server" Text="Tax"></asp:Label></h3> </td>
                        <td> <h3><asp:Label ID="lblTaxAmount" runat="server"  CssClass="total" Text="0"></asp:Label></h3></td>
                    </tr>
                      <tr>
                        <td class="td1" style=""></td>
                        <td class="td2" style=""> <h3><asp:Label ID="Label15" runat="server" Text="Total"></asp:Label></h3> </td>
                        <td> <h3><asp:Label ID="lblTotalAmount" runat="server"  CssClass="total" Text="0"></asp:Label></h3></td>
                        </tr>

                   
                </table><br /><br /><br /><br />
                <div class="Footer">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" CssClass="AUTHORIZEDBY" runat="server" Text="AUTHORIZED BY"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="SIGNATURE" CssClass="SIGNATURE"></asp:Label>
                        </td>
                    </tr>
                </table>
                </div>
            </div>

            </div>
            </div>
            </div>

        </div>
    </div>
  </div>

       <center class="btn-section">      
                      
            <input type="button" value="Print" id="btnPrint" class="form-btn" />
       </center>
    </div>
</asp:Content>
