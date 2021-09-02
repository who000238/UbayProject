<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryRepeater.aspx.cs" Inherits="UbayProject.TryReapeater" %>

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
            /*border: 1px solid #000000;*/
        }

        #postInner {
            width: 100%;
            height: 100%;
        }

        #postArea {
            position: fixed;
            bottom: -110px;
            width: 100%;
            height: 20%;
            transition: 100ms;
        }

            #postArea:hover {
                bottom: 0px;
                transition: 100ms;
            }

        #postSubmit {
            position: absolute;
            width: 60px;
            height: 35px;
            bottom: 10px;
            right: 20px;
        }
        /*input[id*=postSubmit]{
             position: absolute;
            width: 60px;
            height: 35px;
            bottom: 10px;
            right: 20px;
        }*/

        #div1 {
            height: 180px;
        }

        #div2 {
            background-color: orange
        }

        #div3 {
            height: 550px;
            margin-top: 10px;
            background: -webkit-linear-gradient(yellow,red);
            background: -o-linear-gradient(yellow,red);
            background: -moz-linear-gradient(yellow,red);
            background: linear-gradient(yellow,red);
        }

        body {
            background: url('https://source.unsplash.com/twukN12EN7c/1920x1080') no-repeat center center fixed;
            background-size: cover;
        }

        #Logo {
            width: 200px;
            height: 200px
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row" id="div1">
                <div class="col-2">
                    <img src="/Pics/messageImage_1630311093428-removebg-preview.png" id="Logo" />
                </div>
                <div class="col-8">
                    <p>PHOTO/AD</p>
                </div>
                <div class="col-2">
                    <a href="http://localhost:54101/Login.aspx" id="a_Login" runat="server">Login</a>
                    <asp:LinkButton ID="linkLogout" runat="server" OnClick="linkLogout_Click">Logout</asp:LinkButton>
                    <a href="/UserInfo.aspx">使用者資訊</a>
                </div>
            </div>
            <div class="input-group mb-3">
                <asp:TextBox runat="server" ID="SearchBar" class="form-control" placeholder="想找甚麼...?"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-outline-secondary" Text="搜尋" OnClick="btnSearch_Click" />
                </div>
            </div>
          
            <div class="row" id="div3">
                <div class="col-2" id="BoardLink" runat="server">
                </div>
                <div class="col-8">
                    <div>
                        <div>

                            <asp:Repeater ID="Repeater1" runat="server">
                                <HeaderTemplate>
                                    <div class="row">
                                        <div class="col-3">標題</div>
                                        <div class="col-5">發文者</div>
                                        <div class="col-4">發佈時間</div>
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <div class="row">
                                    <div class="col-3">
                                        <a href="/SeePost.aspx?postID=<%# Eval("postID") %>&actionName=reLoad"><%# Eval("postTitle") %></a>

                                    </div>
                                    <div class="col-5">
                                        <a href="/UserInfo.aspx?userID=<%# Eval("userID") %>"><%# Eval("userName") %></a>
                                    </div>
                                    <div class="col-4">
                                        <td><%# Eval("createDate") %></td>
                                    </div>
                                        </div>
                                </ItemTemplate>
                            </asp:Repeater>
                           
                        </div>
                    </div>
                </div>
                <div class="col-2">
                    <p>PHOTO/AD</p>
                </div>
            </div>
            <div style="text-align: center">
            </div>
        </div>
        <div class="row postArea" id="postArea" runat="server">
            <div class="col-12">
                <asp:TextBox runat="server" ID="postTitle" TextMode="SingleLine" CssClass="form-control" placeholder="輸入標題"></asp:TextBox>

            </div>
            <div>
                <asp:TextBox runat="server" ID="postInner" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="輸入內文"></asp:TextBox>
            </div>
            <asp:Button runat="server" ID="postSubmit" CssClass="btn btn-outline-primary" Text="送出" OnClick="postSubmit_Click" />
        </div>
    </form>
</body>
</html>
