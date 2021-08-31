<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeePost.aspx.cs" Inherits="UbayProject.SeePost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="/js/jquery-3.6.0.min.js"></script>
     
    <style>
        div {
              border: 1px solid #000000;
        }

        .titleArea {
            position: relative;
            /* background-color:Window;*/
            padding-top: -5px;
            border: double blue
        }

        #commentPostArea {
            position: relative;
            padding-bottom: 120px;
            border: dashed yellow;
        }

        

        .commentArea {
            border: dashed green;
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

        .normal {
            word-break: break-all;
        }

        /*        #comment {
            width: 100%;
        }
*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid" runat="server">

            <input type="hidden" id="hfpostID" runat="server" />

            <div class="row" runat="server">
                <div class="titleArea row" runat="server">
                    <div class=" col-md-12 col-sm-12" runat="server">
                        <asp:Label runat="server" ID="lblTitle"></asp:Label>
                    </div>
                    <div class=" col-md-8 col-sm-6" runat="server">
                        <asp:Label runat="server" ID="lblInner"></asp:Label>
                    </div>
                    <div class=" col-md-2 col-sm-6" runat="server">
                        LIKE數:<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class=" col-md-2 col-sm-6" runat="server">
                        貼文瀏覽人數:
                    <asp:Label runat="server" ID="lblViewer"></asp:Label>
                    </div>
                </div>
                <div class="col-12 normal" id="commentPostArea" runat="server">
                </div>
                <div class="row commentArea" id="commentArea" runat="server">
                    <asp:TextBox runat="server" ID="commentInput" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="回覆..."></asp:TextBox>
                    <asp:Button runat="server" ID="commentSubmit" CssClass="btn btn-outline-primary" Text="送出回覆" OnClick="commentSubmit_Click" />
                    <asp:Button runat="server" ID="BtnLike" CssClass="btn btn-outline-primary" Text="LIKE" OnClick="BtnLike_Click" />
                </div>
            </div>
        </div>
        <table>
            <tr>
                <td style="width:20%"></td>
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
                    var table = '<table border="1" class="table-primary" id="commentPostTable">';
                    table += '<tr><th>留言者</th><th>留言內容</th><th>留言時間</th></tr>';
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<tr>
                                    <td  style="width:20%">${obj.userName}</td>
                                    <td  style="width:60%">${obj.comment}</td>
                                    <td  style="width:20%">${obj.createDate}</td>
        
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
