<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="UbayProject.CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        div{
            text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>Create Account Page</p>
            <div>
                帳號:<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />
                密碼:<asp:TextBox ID="txtPWD" TextMode="Password" runat="server"></asp:TextBox><br />
                確認密碼:<asp:TextBox ID="txtPWDCheck" TextMode="Password" runat="server"></asp:TextBox><br />
                電子信箱:<asp:TextBox ID="txtMail" TextMode="Email" runat="server"></asp:TextBox><br />
                使用者名稱:<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox><br />
                <asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click"/> &nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" Text="清除" OnClick="btnClear_Click" />
            </div>
        </div>
    </form>
</body>
</html>
