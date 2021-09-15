<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="UbayProject.ForgetPassword" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>忘記密碼</title>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        body {
            background: url('https://source.unsplash.com/twukN12EN7c/1920x1080') no-repeat center center fixed;
            background-size: cover;
        }        
        div {
            text-align: center;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12 col-md-12 col-sm-12" id="divTitle">
                    <br/>
                    <h1>忘記密碼</h1>
                    <br/>
                </div>
                <div class="col-12" id="divMain">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    帳號:<asp:TextBox ID="txtAccount" runat="server" placeholder="請輸入帳號!" Width="297px"></asp:TextBox><br /><br />
                    電子信箱:<asp:TextBox ID="txtMail" TextMode="Email" runat="server" placeholder="請輸入註冊帳號時的信箱!" Width="302px"></asp:TextBox><br />
                    <br/>
                    <asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click" class="btn btn-dark"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="清除" OnClick="btnCancel_Click" class="btn btn-dark" />
                </div>
                <div class="col-12"></div>
            </div>
        </div>
    </form>
</body>
</html>
