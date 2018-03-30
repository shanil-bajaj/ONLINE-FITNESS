<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="NDOnlineGym_2017.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $.ajax({
                type: "POST",
                url: "WebForm2.aspx/GetCustomers",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });

        function OnSuccess(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var customers = xml.find("MemberDetails");
            var row = $("[id*=gvCustomers] tr:last-child").clone(true);
            $("[id*=gvCustomers] tr").not($("[id*=gvCustomers] tr:first-child")).remove();
            $.each(customers, function () {
                var customer = $(this);
                $("td", row).eq(0).html($(this).find("Member_ID1").text());
                $("td", row).eq(1).html($(this).find("FName").text());
                $("td", row).eq(2).html($(this).find("LName").text());
                $("td", row).eq(3).html($(this).find("Contact1").text());
                $("[id*=gvCustomers]").append(row);
                row = $("[id*=gvCustomers] tr:last-child").clone(true);
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="" />
    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" Font-Names="Arial"
    Font-Size="10pt" RowStyle-BackColor="#A1DCF2" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor = "White">
        <Columns>
            <asp:BoundField ItemStyle-Width="150px" DataField="Member_ID1" HeaderText="Member ID" />
            <asp:BoundField ItemStyle-Width="150px" DataField="FName" HeaderText="First Name" />
            <asp:BoundField ItemStyle-Width="150px" DataField="LName" HeaderText="Last Name" />
            <asp:BoundField ItemStyle-Width="150px" DataField="Contact1" HeaderText="Contact" />
        </Columns>
    </asp:GridView>
    </div>
    </form>
</body>
</html>
