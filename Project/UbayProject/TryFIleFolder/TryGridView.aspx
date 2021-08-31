<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryGridView.aspx.cs" Inherits="UbayProject.TryGri" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="標題" DataField="postTitle" />
                <asp:BoundField HeaderText="發文時間" DataField="createDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:TemplateField HeaderText="超連結標題">
                    <ItemTemplate>
                        <a href="SeePost.aspx?postID=<%# Eval("postID") %>"><%# Eval("postTitle") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="發文者">
                    <ItemTemplate>
                        <a href="UserInfo.aspx?userID=<%# Eval("userID") %>"><%# Eval("userName") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />

        </asp:GridView>
    </form>
</body>
</html>
