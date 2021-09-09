<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryActive.aspx.cs" Inherits="UbayProject.TryActive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="Literal1" runat="server">輸入你的驗證碼</asp:Literal><br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
            <asp:Button ID="Button1" runat="server" Text="確定" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
