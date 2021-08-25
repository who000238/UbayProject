<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeePost.aspx.cs" Inherits="UbayProject.SeePost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <asp:Label runat="server" ID="lblTitle" ></asp:Label>
                </div>
                <div class="col-md-10 col-sm-6">
                    <asp:Label runat="server" ID="lblInner"></asp:Label>
                </div>
                <div class="col-md-1 col-sm-6">123</div>
                <div class="col-md-1 col-sm-6">123</div>
            </div>
        </div>
    </form>
</body>
</html>
