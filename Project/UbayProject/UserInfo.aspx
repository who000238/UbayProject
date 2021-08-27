<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="UbayProject.UserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>使用者資訊頁面</title>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        table {
            border-collapse: separate;
            border-radius: 10px;
            -moz-border-radius: 6px;
            box-shadow: 15px 5px 5px rgba(0,0,0,0.8);
        }

        td {
            background-clip: padding-box;
            border-radius: 10px;
        }

        #title {
            margin: 50px;
            text-align: center;
            color: snow;
            background-color: #212529;
            transform: perspective(250px) rotateX(-10deg);
            box-shadow: 0px 5px 5px rgba(0,0,0,0.8);
            box-shadow: 3px 5px 5px rgba(0,0,0,0.8);
            box-shadow: -3px 5px 5px rgba(0,0,0,0.8);
        }
        #divImg {
            border-radius: 10px;
            text-align: center;
            background-color: #212529;
            box-shadow: -15px 5px 5px rgba(0,0,0,0.8);
        }

        @media screen and (min-width:768px) {
            table {
                border-collapse: separate;
                border-radius: 10px;
                -moz-border-radius: 6px;
                box-shadow: 15px 5px 5px rgba(0,0,0,0.8);
            }

            td {
                background-clip: padding-box;
                border-radius: 10px;
            }

            #title {
                margin: 50px;
                text-align: center;
                color: snow;
                background-color: #212529;
                transform: perspective(250px) rotateX(-10deg);
                box-shadow: 0px 5px 5px rgba(0,0,0,0.8);
                box-shadow: 3px 5px 5px rgba(0,0,0,0.8);
                box-shadow: -3px 5px 5px rgba(0,0,0,0.8);
            }

            #tableUserInfo {
            }

            #divImg {
                border-radius: 10px;
                text-align: center;
                background-color: #212529;
                transform: perspective(250px) rotateY(5deg);
                transition: 3s;
                transition-delay: 0.5s;
                opacity: 0.3;
                box-shadow: -15px 5px 5px rgba(0,0,0,0.8);
            }

                #divImg:hover {
                    text-align: center;
                    background-color: #212529;
                    transform: perspective(250px) rotateY(0deg);
                    transition: 500ms;
                    opacity: 1;
                }

            #tableUserInfo {
                transform: perspective(250px) rotateY(-5deg);
                transition: 3s;
                transition-delay: 0.5s;
                opacity: 0.3;
            }

                #tableUserInfo:hover {
                    transform: perspective(250px) rotateY(0deg);
                    transition: 500ms;
                    opacity: 1;
                }

            #trUserName {
                padding: 10px;
                position: relative;
                right: 0px;
                bottom: 0px;
                transition: 500ms;
            }

                #trUserName:hover {
                    padding: 10px;
                    position: relative;
                    right: 20px;
                    bottom: 20px;
                    transition: 500ms;
                }

            #trUserSex {
                padding: 10px;
                position: relative;
                right: 0px;
                bottom: 0px;
                transition: 500ms;
            }

                #trUserSex:hover {
                    padding: 10px;
                    right: 20px;
                    bottom: 20px;
                    transition: 500ms;
                }

            #trUserBirthday {
                padding: 10px;
                position: relative;
                right: 0px;
                bottom: 0px;
                transition: 500ms;
            }

                #trUserBirthday:hover {
                    padding: 10px;
                    position: relative;
                    right: 20px;
                    bottom: 20px;
                    transition: 500ms;
                }

            #trUserIntro {
                padding: 10px;
                position: relative;
                right: 0px;
                bottom: 0px;
                transition: 500ms;
            }

                #trUserIntro:hover {
                    padding: 10px;
                    position: relative;
                    right: 20px;
                    bottom: 20px;
                    transition: 500ms;
                }

            td {
                LINE-HEIGHT: 50px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" id="divUserInfo">
            <div class="row">
                <div class="col-12 col-md-12 col-sm-12 ">
                    <h1 id="title">USERINFO</h1>
                </div>
            </div>
            <div class="row justify-content-md-center">
                <div class="col-12 col-md-4 col-sm-12 align-self-start " id="divImg">
                    <div>
                        <asp:Image ID="userImg" runat="server" ImageUrl="https://freerangestock.com/thumbnail/35900/red-question-mark.jpg" />
                    </div>
                    <div>
                        <asp:Button class="btn btn-outline-light" ID="btnUpdateUserPhoto" runat="server" Text="修改頭像" OnClick="btnUpdateUserPhoto_Click" />
                    </div>
                </div>
                <div class="col-12 col-md-6 col-sm-12">
                    <table class="table table-dark table-hover" id="tableUserInfo">
                        <tbody>
                            <tr id="trUserName">
                                <td class="col-md-2">暱稱</td>
                                <td><asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label></td>
                                <td class="col-md-2">
                                    <asp:Button class="btn btn-outline-light" ID="btnUpdateUserName" runat="server" Text="修改暱稱" OnClick="btnUpdateUserName_Click" />
                                </td>
                            </tr>
                            <tr id="trUserSex">
                                <td>性別</td>
                                <td ><asp:Label ID="lblUserSex" runat="server" Text="Label"></asp:Label></td>
                                <td>
                                    <asp:Button class="btn btn-outline-light" ID="btnUpdateUserSex" runat="server" Text="設定性別" OnClick="btnUpdateUserSex_Click" />
                                </td>

                            </tr>
                            <tr id="trUserBirthday">
                                <td>生日</td>
                                <td><asp:Label ID="lblUserBirthday" runat="server" Text="Label"></asp:Label></td>
                                <td>
                                    <asp:Button class="btn btn-outline-light" ID="btnUpdateUserBirthday" runat="server" Text="設定生日" OnClick="btnUpdateUserBirthday_Click" />
                                </td>
                            </tr>
                            <tr id="trUserIntro">
                                <td>自我介紹</td>
                                <td>
                                    <asp:TextBox ID="txtUserIntro" runat="server"  style="width:80%" Text="null" ReadOnly="True" TextMode="MultiLine" MaxLength="4000" BackColor="#212529" ForeColor="White" Rows="5"></asp:TextBox></td>
                                <td>
                                    <asp:Button class="btn btn-outline-light" ID="btnUpdateUserIntro" runat="server" Text="修改自介" OnClick="btnUpdateUserIntro_Click" UseSubmitBehavior="True" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>


    </form>
</body>
</html>
