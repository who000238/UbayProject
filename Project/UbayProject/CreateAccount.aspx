<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="UbayProject.CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>CreateAccount</title>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        #divTitle {
            text-align: center;
        }
        #divMain{
            text-align:right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12 col-md-12 col-sm-12" id="divTitle">
                    <p>Create Account Page</p>
                </div>
            <div class="col-3"></div>
            <div class="col-4" id="divMain">
                帳號:<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />
                密碼:<asp:TextBox ID="txtPWD" TextMode="Password" runat="server"></asp:TextBox><br />
                確認密碼:<asp:TextBox ID="txtPWDCheck" TextMode="Password" runat="server"></asp:TextBox><br />
                電子信箱:<asp:TextBox ID="txtMail" TextMode="Email" runat="server"></asp:TextBox><br />
                使用者名稱:<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox><br />
                <asp:Button ID="btnSubmit" runat="server" Text="送出" class="btn btn-dark" OnClick="btnSubmit_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" Text="清除" class="btn btn-dark" OnClick="btnClear_Click" />
            </div>
            <div class="col-5"></div>
            </div>
        </div>
    </form>
</body>
</html>
