<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Complaint.aspx.cs" Inherits="UbayProject.complaint" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>申訴頁面</title>
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


        .auto-style1 {
            flex: 0 0 auto;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12 col-md-12 col-sm-12" id="divTitle">
                    <br/>
                    <h1>申訴頁面</h1>
                    <br/>
                </div>
                <div class="auto-style1" id="divMain">

                    申訴標題:
                <asp:TextBox ID="txtTitle" runat="server" Width="488px"></asp:TextBox><br /><br/>
                    申訴內文:
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" MaxLength="500" Width="487px"></asp:TextBox> <br />
                <asp:Button ID="Button1" runat="server" Text="送出" class="btn btn-dark" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text="清除" class="btn btn-dark" OnClick="btnClear_Click" />
                </div>
                <div class="col-5"></div>
            </div>
        </div>
    </form>
</body>
</html>
