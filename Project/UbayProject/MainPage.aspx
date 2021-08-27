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
           /* border: 1px solid #000000;*/
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
                    <a href="http://localhost:54101/Login.aspx" id="a_Login" runat="server">Login</a>
                    <asp:LinkButton ID="linkLogout" runat="server" OnClick="linkLogout_Click">Logout</asp:LinkButton>
                    <a href="UserInfo.aspx">使用者資訊</a>
                </div>
            </div>
            <div class="row">
                <div class="col-2"></div>
                <div class="col-8">
                    <asp:TextBox runat="server" ID="SearchBar" class="form-control"  placeholder="搜尋"></asp:TextBox>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2" id="BoardLink" runat="server">
                </div>
                <div class="col-8">
                    <p style="font-size: 30px; text-align: center">主頁面</p>
                </div>
                <div class="col-2">
                    <p>PHOTO/AD</p>
                </div>
            </div>
            <div>
                <div>
                    <label for="nickname" class="form-label" title="熱門文章" data-bs-toggle="tooltip" data-bs-placement="top">
                        熱門文章
                    </label>
                </div>
            </div>
        </div>
        <div>
            <p>testArea</p>
            <a href="TryGridView.aspx">TryGridView.aspx</a><br />
            <a href="TryMainPage.aspx">TryMainPage.aspx</a>
        </div>
    </form>
</body>
</html>
