<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryGridView.aspx.cs" Inherits="UbayProject.TryGri" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="標題" DataField="postTitle" />
                <asp:BoundField HeaderText="發文時間" DataField="createDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
              <%--  <asp:TemplateField HeaderText="Act">
                    <ItemTemplate>
                        <a href="TryGridView.aspx?ID=<%# Eval("ID") %>">進入貼文</a>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>

        </asp:GridView>
    </form>
</body>
</html>
