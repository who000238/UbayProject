﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="UbayProject.CreateAccount"  ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>申請帳號</title>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        #divTitle {
            text-align: center;
        }

        #divMain {
            text-align: center;
            align-items: center;

        }

        body {
            background: url('https://source.unsplash.com/twukN12EN7c/1920x1080') no-repeat center center fixed;
            background-size: cover;
        }

          form {
            transform: translateY(0px);
            filter: drop-shadow( 1px 2px 4px hsl(0deg 0% 0% / 0.2) );
        }

        form:focus-within {
            transform: translateY(-4px);
            filter: drop-shadow( 2px 4px 16px hsl(0deg 0% 0% / 0.2) );
        }

        @media (prefers-reduced-motion: no-preference) {
            form {
                transition: filter 300ms, transform 300ms;
                will-change: transform;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12 col-md-12 col-sm-12" id="divTitle" style="padding-top: 180px">
                    <p>Welcome</p>
                </div>
                <div class="col-4"></div>
                <div class="col-4" id="divMain">
                    <asp:TextBox ID="txtAccount" class="form-control" placeholder="請輸入8~20位字元的帳號" runat="server"></asp:TextBox><br />
                    <asp:TextBox ID="txtPWD" class="form-control" placeholder="請輸入8~20位字元的密碼" TextMode="Password" runat="server"></asp:TextBox><br />
                    <asp:TextBox ID="txtPWDCheck" class="form-control" placeholder="請再次輸入密碼" TextMode="Password" runat="server"></asp:TextBox><br />
                    <asp:TextBox ID="txtMail" class="form-control" placeholder="電子信箱" TextMode="Email" runat="server"></asp:TextBox><br />
                    <asp:TextBox ID="txtUserName" class="form-control" placeholder="使用者名稱" runat="server"></asp:TextBox><br />
                    <asp:Button ID="btnSubmit" runat="server" Text="送出" class="btn btn-dark" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="清除" class="btn btn-dark" OnClick="btnClear_Click" />
                </div>
                <div class="col-4"></div>
            </div>
        </div>
    </form>
</body>
</html>
