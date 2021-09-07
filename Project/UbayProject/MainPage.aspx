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
        #div1 {
            height: 180px;
        }
/**/
div.container2
{
    font-family: Raleway;
    margin: 0 auto;
	padding: 10px 1em;
	text-align: center;
}

div.container2 a
{
    color: #ff0000;
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
/**/
        #div2 {
            background-color: orange;
        }
        #div3 {
            height: 550px;
            /* margin-top: 10px;
            background: -webkit-linear-gradient(yellow,red);
            background: -o-linear-gradient(yellow,red);
            background: -moz-linear-gradient(yellow,red);
            background: linear-gradient(yellow,red);*/
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
            <img src="Pics/messageImage_1630311093428-removebg-preview.png" id="Logo" />
            <div id="div1" class="row container2 topBotomBordersOut">
                <div class="col-2">
                    <%--<img src="Pics/messageImage_1630311093428-removebg-preview.png" id="Logo" />--%>
                </div>
                <%--  --%>
                <nav class="navbar navbar-expand-lg navbar-light">
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                    data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false"
                    aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="col-8">
                    <p>PHOTO/AD</p>
                </div>
                <div class="col-2">
                    <a href="http://localhost:54101/Login.aspx" id="a_Login" runat="server">Login</a>
                    <asp:LinkButton ID="linkLogout" runat="server" OnClick="linkLogout_Click">Logout</asp:LinkButton>
                    <a href="UserInfo.aspx" runat="server" id="UserInfoLink">使用者資訊</a>
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
                <div class="col-8" runat="server" id="HotPost">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="超連結標題">
                                <ItemTemplate>
                                    <a href="/SeePost.aspx?postID=<%# Eval("postID") %>"><%# Eval("postTitle") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="發文者">
                                <ItemTemplate>
                                    <a href="/UserInfo.aspx?userID=<%# Eval("userID") %>"><%# Eval("userName") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="瀏覽人次" DataField="countOfViewers" />
                            <asp:BoundField HeaderText="發文時間" DataField="createDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="超連結標題">
                                <ItemTemplate>
                                    <a href="/SeePost.aspx?postID=<%# Eval("postID") %>"><%# Eval("postTitle") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="發文者">
                                <ItemTemplate>
                                    <a href="/UserInfo.aspx?userID=<%# Eval("userID") %>"><%# Eval("userName") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="瀏覽人次" DataField="countOfViewers" />
                            <asp:BoundField HeaderText="發文時間" DataField="createDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-2">
                    <p>PHOTO/AD</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
