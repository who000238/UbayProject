<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Complaint.aspx.cs" Inherits="UbayProject.complaint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        div{
            text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>申訴頁面</p>
             <div>
                帳號:<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />
                申訴標題:<asp:TextBox ID="txtAreaTitle" runat="server"></asp:TextBox><br />
                申訴內文:<textarea id="txtAreaInside" runat="server"></textarea><br />
                <asp:Button ID="btnSubmit" runat="server" Text="Button" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </form>
</body>
</html>
