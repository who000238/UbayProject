<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryOTP.aspx.cs" Inherits="UbayProject.TryOTP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="0" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="寄信" OnClick="Button2_Click" Style="height: 27px" />
            <asp:TextBox ID="txtAccount" class="form-control" placeholder="帳號" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="txtPWD" class="form-control" placeholder="密碼" TextMode="Password" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="txtPWDCheck" class="form-control" placeholder="確認密碼" TextMode="Password" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="txtMail" class="form-control" placeholder="電子信箱" TextMode="Email" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="txtUserName" class="form-control" placeholder="使用者名稱" runat="server"></asp:TextBox><br />
        </div>
    </form>
</body>
</html>
