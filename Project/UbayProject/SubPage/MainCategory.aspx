<%@ Page Title="" Language="C#" MasterPageFile="~/SubMasterPage.Master" AutoEventWireup="true" CodeBehind="MainCategory.aspx.cs" Inherits="UbayProject.SubPage.MainCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Breadcrumbs" runat="server">
     <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/MainPage.aspx">主頁</a></li>
            <li class="breadcrumb-item active" aria-current="page">
                <asp:Label ID="lblMainCategoryName" runat="server" Text=""></asp:Label>
            </li>
        </ol>
    </nav>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
