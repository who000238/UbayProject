<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrySerach.aspx.cs" Inherits="UbayProject.TySerach" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        form {
            border: 1px solid blue;
        }

        div {
            border: 1px solid red
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            ASP:<asp:TextBox ID="txtASP_input" runat="server"></asp:TextBox>
            <asp:Button ID="btnASP" runat="server" Text="搜尋" OnClick="btnASP_Click" />
            <br />
            HTML:<input id="txtHTML_input" type="text" runat="server" />
            <input id="btnHTML" type="submit" value="搜尋" runat="server" />
        </div>
        <div>
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
        <div>
            <asp:Label runat="server" ID="lbl"></asp:Label>
        </div>
    </form>
</body>
</html>
