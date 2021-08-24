<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="UbayProject.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MainPage</title>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        div {
            border: 1px solid #000000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-2">
                    <p>LOGO</p>
                </div>
                <div class="col-8">
                    <p>PHOTO/AD</p>
                </div>
                <div class="col-2">
                    <a href="http://localhost:54101/Login.aspx" id="a_Login" runat="server">login</a>
                    <asp:Button ID="btnLogout" runat="server" Text="登出" class="btn btn-dark" OnClick="btnLogout_Click" />
                    <a href="UserInfo.aspx">使用者資訊</a>
                </div>
            </div>
            <div class="row">
                <div class="col-2">
                    <ul>
                        <li><a href="SubPage/TempAPage.aspx">A版</a></li>
                        <li><a href="SubPage/TempBPage.aspx">B版</a></li>
                        <li>C版</li>
                    </ul>
                </div>
                <div class="col-8">
                    <p style="font-size: 30px; text-align: center">主頁面</p>
                </div>
                <div class="col-2">
                    <p>PHOTO/AD</p>
                </div>
            </div>
            <div>
                <form>
                    <div>
                        <label for="nickname" class="form-label" title="熱門文章" data-bs-toggle="tooltip" data-bs-placement="top">
                            熱門文章
                        </label>
                    </div>
                </form>
            </div>
        </div>
    </form>
</body>
</html>
