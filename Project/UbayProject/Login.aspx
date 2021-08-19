<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UbayProject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
   <title>Login</title>
    <style>
        #divTitle {
            text-align: center;
        }
        #divInput{
            text-align:center;
            align-items:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <a></a>
        <div id="divTitle" style="padding-top:200px">
            <p>Welcome</p>
        </div>
        <div id="divInput">
            Account :
            <asp:TextBox runat="server" ID="txtAccount"></asp:TextBox>
            <br />
            Password : 
            <asp:TextBox runat="server" ID="txtPassowrd" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" />
            <br />
            <a href="CreateAccount.aspx" style="text-align: right">沒有帳號嗎?點擊申請!</a>
        </div>
    </form>
</body>
</html>
