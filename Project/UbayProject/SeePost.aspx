<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeePost.aspx.cs" Inherits="UbayProject.SeePost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>貼文</title>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="/js/jquery-3.6.0.min.js"></script>

    <style>
        #commentPostArea {
            position: relative;
            padding-bottom: 120px;
        }

        #commentArea {
            position: fixed;
            bottom: -100px;
            width: 100%;
            transition: 100ms;
        }

            #commentArea:hover {
                bottom: 0px;
                transition: 100ms;
            }

        #commentSubmit {
            position: absolute;
            width: 120px;
            height: 35px;
            bottom: 10px;
            right: 20px;
        }

        #BtnLike {
            position: absolute;
            width: 60px;
            height: 35px;
            bottom: 50px;
            right: 80px;
        }

        #BtnDisLike {
            position: absolute;
            width: 60px;
            height: 35px;
            bottom: 50px;
            right: 20px;
        }
              body {
            background: url('https://source.unsplash.com/twukN12EN7c/1920x1080') no-repeat center center fixed;
            background-size: cover;
        }

        .normal {
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid" runat="server">
            <input type="hidden" id="hfpostID" runat="server" />
            <div class="row" runat="server">
                <table class="table table-striped table-hover">
                    <tr>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblTitle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td  style="width:70%">
                            <asp:Label runat="server" ID="lblInner"></asp:Label>
                        </td>
                        <td style="width:15%;text-align:right">
                            讚數:<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            噓數:<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width:15%;text-align:right">
                            貼文瀏覽人數:<asp:Label runat="server" ID="lblViewer"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div class="col-12 normal" id="commentPostArea" runat="server">
                </div>
                <div class="row commentArea" id="commentArea" runat="server">
                    <asp:TextBox runat="server" ID="commentInput" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="回覆..."></asp:TextBox>
                    <asp:Button runat="server" ID="commentSubmit" CssClass="btn btn-outline-primary" Text="送出回覆" OnClick="commentSubmit_Click" />
                    <asp:Button runat="server" ID="BtnLike" CssClass="btn btn-outline-primary" Text="讚" OnClick="BtnLike_Click" />
                    <asp:Button ID="BtnDisLike" runat="server" CssClass="btn btn-outline-primary" Text="噓" OnClick="BtnDislike_Click" />
                </div>
            </div>
        </div>
        <table>
            <tr>
                <td style="width: 20%"></td>
            </tr>
        </table>
    </form>

    <script>
        $(function () {

            $(document).on("click", "#commentSubmit", function () {
                var postID = $("#hfpostID").val();

                $.ajax({

                    url: "http://localhost:54101/AJAXSeePost.ashx?actionName=Load",
                    type: "POST",
                    data: {
                        "postID": postID
                    },
                    success: function (result) {
                        $("hfpostID").val(result["postID"]);
                        $("commentID").val(result["commentID"]);
                        $("comment").val(result["comment"]);
                        $("userID").val(result["userID"]);
                        $("createDate").val(result["createDate"]);
                    }
                });
            });
            var postID = $("#hfpostID").val();
            $.ajax({
                url: "http://localhost:54101/AJAXSeePost.ashx?actionName=Load",
                type: "GET",
                data: {
                    "postID": postID
                },
                success: function (result) {
                    var table = '<table class="table table-striped table-hover" id="commentPostTable">';
                    table += '<tr><th>留言者</th><th>留言內容</th><th>留言時間</th></tr>';
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<tr>
                                    <td  style="width:20%">
                                        <a href="/UserInfo.aspx?userID=${obj.userID}">${obj.userName}</a>
                                    </td>
                                    <td  style="width:60%">${obj.comment}</td>
                                    <td  style="width:20%">${obj.CreateDateText}</td>
        
                                </tr>`;
                        table += htmlText;
                    }
                    table += "</table>";
                    $("#commentPostArea").append(table);
                }
            });
        });
    </script>

</body>
</html>
