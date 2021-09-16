<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryActive.aspx.cs" Inherits="UbayProject.TryActive" %>

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
    <form id="form2" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12 col-md-12 col-sm-12" id="divTitle" style="padding-top: 180px">
                    <p>Welcome</p>
                </div>
                <div class="col-4"></div>
                <div class="col-4" id="divMain">
                    <asp:Literal ID="Literal1" runat="server">輸入你的驗證碼</asp:Literal><br />
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
                    <asp:Button ID="Button1" runat="server" Text="確定" OnClick="Button1_Click" /><br />
                </div>
                <div class="col-4"></div>
            </div>
        </div>
    </form>
</body>
</html>

