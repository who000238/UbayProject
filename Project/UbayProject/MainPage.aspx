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

        #logoPhoto {
            width: 100px;
            height: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-2">
                    <img src="Images/logo.jpeg" id="logoPhoto" />
                </div>
                <div class="col-8">
                    <p>PHOTO/AD</p>
                </div>
                <div class="col-2">
                    <a href="http://localhost:54101/Login.aspx" id="a_Login" runat="server">Login</a>
                    <asp:LinkButton ID="linkLogout" runat="server" OnClick="linkLogout_Click">Logout</asp:LinkButton>
                    <a href="UserInfo.aspx" runat="server" id="UserInfoLink">使用者資訊</a>
                </div>
            </div>
            <div class="row">
                <div class="col-2"></div>
                <div class="col-6">
                    <asp:TextBox runat="server" ID="SearchBar" class="form-control" placeholder="搜尋"></asp:TextBox>
                </div>
                <div class="col-2">
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />

                </div>
                <div class="col-2"></div>
            </div>
            <div class="row">
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
