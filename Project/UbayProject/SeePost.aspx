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
        .titleArea {
            position: fixed;
           /* background-color:Window;*/
            padding-top:-5px;
        }
        .commentArea {
            position: fixed;
            bottom: 0px;
            width: 100%;
        }

        #commentSubmit {
            position: absolute;
            width: 120px;
            height: 35px;
            bottom: 10px;
            right: 20px;
        }
        #comment{
            width:100%;
        }
        #commentPostArea{
           position: fixed;
           padding-top:80px;
            border: dashed red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="titleArea row">
                <div class=" col-md-12 col-sm-12">
                    <asp:Label runat="server" ID="lblTitle"></asp:Label>
                </div>
                <div class=" col-md-10 col-sm-6">
                    <asp:Label runat="server" ID="lblInner"></asp:Label>
                </div>
                <div class=" col-md-1 col-sm-6">123</div>
                <div class=" col-md-1 col-sm-6">123</div>
                    </div>
                <div class="col-12" id="commentPostArea" runat="server">
                    <asp:Label runat="server" ID="lblComment"></asp:Label>
                </div>
                <div class="row commentArea" id="commentArea" runat="server">
                        <asp:TextBox runat="server" ID="commentInput" TextMode="MultiLine" Rows="2" CssClass="form-control" placeholder="回覆..."></asp:TextBox>
                    <asp:Button runat="server" ID="commentSubmit" CssClass="btn btn-outline-primary" Text="送出回覆" OnClick="commentSubmit_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
