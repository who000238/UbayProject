<%@ Page Title="" Language="C#" MasterPageFile="~/SubSubMasterPage.Master" AutoEventWireup="true" CodeBehind="FGO.aspx.cs" Inherits="UbayProject.SubPage.FGO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="/MainPage.aspx">主頁</a></li>
                    <li class="breadcrumb-item"><a href="/SubPage/GAME.aspx?mainCategoryID=1">Game</a></li>
                    <li class="breadcrumb-item active" aria-current="page">FGO</li>
                    
                </ol>
            </nav>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
</asp:Content>
