﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UbayProject.Default" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <!--  -->
    <link rel="stylesheet" href="css/bootstrap.css">
    <!--  -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        div {
            border: 1px solid #000000;
        }
    </style>
</head>

<body>
    <div class="container">
        <div class="row">
            <div class="col-2">
                <p>LOGO</p>

            </div>
            <div class="col-8">
                <p>PHOTO/AD</p>                
            </div>
            <div class="col-2">
                  <a>user</a>
                <a href="http://localhost:54101/Login.aspx">login</a><br />
                <a href="UserInfo.aspx">使用者資訊</a>
                  <br />
            </div>
        </div>
        <div class="row">
            <div class="col-2" id="BoardLink" runat="server">
            </div>
            <div class="col-8">
               <p style="font-size:30px ; text-align:center" >主頁面</p>
            </div>
            <div class="col-2">
                <p>PHOTO/AD</p>                
            </div>
        </div>
        <div>
            <form>
                <div>
                    <label for="nickname" class="form-label" title="熱門文章" data-bs-toggle="tooltip" data-bs-placement="top"> 
                        熱門文章
                    </label>
                </div>
            </form>
        </div>
    </div>
</body>
</html>