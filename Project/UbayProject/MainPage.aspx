﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="UbayProject.MainPage" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首頁</title>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        #div1 {
            height: 180px;
        
        }
        
div.container2
{
    font-family: Raleway;
    margin: 0 auto;
	padding: 10px 1em;
	text-align: center;
}

div.container2 
{
    color: #000000;
    text-decoration: none;
    font: 20px Raleway;
    margin: 0px 1px;
    padding: 1px 1px;
    position: relative;
    z-index: 0;
    cursor: pointer;
}
 Top and Bottom borders go out 
div.topBotomBordersOut a:before, div.topBotomBordersOut a:after
{
    position: absolute;
    left: 0px;
    width: 100%;
    height: 2px;
    background:#000000;
    content: "";
    opacity: 0;
    transition: all 0.3s;
}

div.topBotomBordersOut a:before
{
    top: 0px;
    transform: translateY(10px);
}

div.topBotomBordersOut a:after
{
    bottom: 0px;
    transform: translateY(-10px);
}

div.topBotomBordersOut a:hover:before, div.topBotomBordersOut a:hover:after
{
    opacity: 1;
    transform: translateY(0px);
}
        
        #div3 {
            height: 550px;
            background-color:rgba(255,255,255,0.5);
      
        }

        body {
            background: url('https://source.unsplash.com/twukN12EN7c/1920x1080') no-repeat center center fixed;
            background-size: cover;
        }

        #Logo {
            width: 200px;
            height: 200px
        }
       
        .postInner {
            max-width: 150px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div id="div1" class="row container2 topBotomBordersOut">
                <div class="col-2 ">
                    <img src="Pics/messageImage_1630311093428-removebg-preview.png" id="Logo" />
                </div>
                <%--  --%>
                <nav class="navbar navbar-expand-lg navbar-light">
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                    data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false"
                    aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="col-8">
                </div>
                <div class="col-2 collapse navbar-collapse justify-content-end" id="navbarNavDropdown"  >
                    <a href="http://localhost:54101/Login.aspx" id="a_Login" runat="server">Login</a> &nbsp;&nbsp;
                    <asp:LinkButton ID="linkLogout" runat="server" OnClick="linkLogout_Click">Logout</asp:LinkButton>&nbsp;&nbsp;
                    <a href="UserInfo.aspx" runat="server" id="UserInfoLink">使用者資訊</a> &nbsp;&nbsp;
                    <a href ="Complaint.aspx">申訴</a>
                </div>
                </nav>
                <%--  --%>
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
                <div class="col-9" runat="server" id="HotPost">
                 <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                            <div class="row">
                                <div class="col-2">標題</div>
                                <div class="col-2">發文者</div>
                                <div class="col-1">分類名稱</div>
                                <div class="col-2">內文</div>
                                <div class="col-3">發佈時間</div>
                                <div class="col-2">瀏覽人次</div>
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <div class="row">
                                <div class="col-2">
                                    <a href="/SeePost.aspx?postID=<%# Eval("postID") %>&actionName=reLoad"><%# Eval("postTitle") %></a>

                                </div>
                                <div class="col-2">
                                    <a href="/UserInfo.aspx?userID=<%# Eval("userID") %>"><%# Eval("userName") %></a>
                                </div>
                                <div class="col-1 ">
                                    <%#Eval("subCategoryName") %>
                                </div>
                                <div class="col-2 postInner" >
                                    <%#Eval("postText") %>
                                </div>
                                <div class="col-3">
                                    <td><%# Eval("createDate") %></td>
                                </div>
                                <div class="col-2">
                                    <td><%# Eval("countOfViewers") %>人</td>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                           
                </div>
                <div class="col-1">
                </div>
            </div>
        </div>
   
    </form>
</body>
</html>
