<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateUserInfo.aspx.cs" Inherits="UbayProject.UpdateUserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改個人資料</title>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        input,select,textarea {
            border-radius: 10px;
        }
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
                opacity:1;
                box-shadow: -15px 5px 5px rgba(0,0,0,0.8);
            }



            #trUserName {
                padding: 10px;
                position: relative;
                right: 0px;
                bottom: 0px;
            }

            #trUserSex {
                padding: 10px;
                position: relative;
                right: 0px;
                bottom: 0px;
            }

            #trUserBirthday {
                padding: 10px;
                position: relative;
                right: 0px;
                bottom: 0px;
            }

            #trUserIntro {
                padding: 10px;
                position: relative;
                right: 0px;
                bottom: 0px;
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
                    <h1 id="title">
                        <asp:ImageButton ID="ibtnToMain" runat="server" ImageUrl="/Pics/BackArrow.png" Height="45px" ImageAlign="Left" OnClick="ibtnToMain_Click" ToolTip="回到首頁" />
                        USERINFO
                    </h1>
                </div>
            </div>
            <div class="row justify-content-md-center">
                <div class="col-12 col-md-5 col-sm-12 align-self-start " id="divImg">
                    <div>
                        <asp:Image ID="userImg" runat="server" ImageUrl="data:image/jpeg;base64,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" Width="225" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtImg" runat="server" Text="" placeholder="請輸入圖片連結!" TextMode="Url" MaxLength="199" Enabled="False" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox>
                        <button type="button" id="btnViewImg" >預覽圖片</button> 
                    </div>
                </div>
                <div class="col-12 col-md-6 col-sm-12">
                    <table class="table table-dark table-hover" id="tableUserInfo">
                        <tbody>
                            <tr id="trUserName">
                                <td class="col-md-2">暱稱</td>
                                <td>
                                    <asp:TextBox ID="txtUserName" runat="server" placeholder="暱稱" MaxLength="20" Rows="5" Enabled="False" onkeydown = "return (event.keyCode!=13);"></asp:TextBox></td>
                            </tr>
                            <tr id="trUserSex">
                                <td>性別</td>
                                <td >
                                    <asp:DropDownList ID="ddlUserSex" runat="server" Enabled="False">
                                        <asp:ListItem Selected="True" Value="男">男</asp:ListItem>
                                        <asp:ListItem Value="女">女</asp:ListItem>
                                        <asp:ListItem Value="無">不公開</asp:ListItem>
                                    </asp:DropDownList>    
                                </td>

                            </tr>
                            <tr id="trUserBirthday">
                                <td>生日</td>
                                <td>
                                    <asp:TextBox ID="txtUserBirthday" runat="server" Enabled="False" onkeydown = "return (event.keyCode!=13);"></asp:TextBox></td>
                            </tr>
                            <tr id="trUserIntro">
                                <td>自我介紹</td>
                                <td>
                                    <asp:TextBox ID="txtUserIntro" runat="server" MaxLength="4000" TextMode="MultiLine" style="width: 95%;" Enabled="False"  BorderStyle="Dotted">white</asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            
            <div class="row justify-content-md-center" >
                <div class="col-4 col-md-4 col-sm-6" style="text-align:center"" >
                    <asp:Button ID="btnSubmit" runat="server" Text="確認修改" OnClick="btnSubmit_Click"  />
                </div>
                <div class="col-4 col-md-4 col-sm-6" style="text-align:center" >
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>


    </form>

    <script>
        $(document).ready(function () {
            $("#btnViewImg").click(function () {
                $("#userImg").attr("src", $("#txtImg").val());
            }
            );

            if (getUrlVars()["mode"] == "UpdateUserPhoto") {
                $("#btnViewImg").show();
            }
            else {
                $("#btnViewImg").hide();
            }
        })
        
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }
    </script>
</body>
</html>
