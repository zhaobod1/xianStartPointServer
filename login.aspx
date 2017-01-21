<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title><%=Session["Sys_Title"].ToS()%></title>
    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>

    <script src="js/cus_main.js" type="text/javascript"></script>

    <link href="css/style.css" type="text/css" rel="stylesheet">
      <style>
        .MessageTS /*页面提示消息样式*/
        {
            color: Red;
            border: solid 1px #fb9b04;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#ffffdd, EndColorStr=#fcfc02,opacity=90);
        }
        
    </style>
</head>
<body id="main_body" class="indexBg" scroll="no" style="height: 100%;">
    <form id="form1" runat="server" style="margin: 0; padding: 0;">
    <table id="main_tab" width="100%" height="349" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <table border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            
                        </td>
                    </tr>
                </table>
                <table width="478" height="272" border="0" align="center" cellpadding="0" cellspacing="0" style=" background:url(images/login_center.png); background-repeat:no-repeat;">
                    <tr>
                        <td width="478" height="272" valign="top" >
                         
                            <div style="height: 87px; overflow: hidden">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr height="32">
                                    <td width="130">
                                        &nbsp;
                                    </td>
                                    <td  align="left">
                                        <input type="text" style="width:200px; height:27px; line-height:27px;  border: solid 1px #8C99A9;
                                            outline: none; margin:1px 0 0 0 ;" id="Text1" runat="server" />
                                    </td>
                                </tr>
                                <tr height="60">
                                    <td >
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <input name="text" type="password" id="Text2" style="width:200px; height:27px;line-height:27px;  border: solid 1px #8C99A9;
                                            outline: none; margin:0 ;" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <div style="height: 30px; overflow: hidden">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="29%">
                                        &nbsp;
                                    </td>
                                    <td width="34%">
                                        <div style="float: left; cursor: pointer; width: 103px; height: 38px; background: url(images/login.jpg);
                                            border: none;" onclick="check_form();">
                                        </div>
                                        <div style="display: none;">
                                            <asp:Button ID="Button1" OnClick="Button1_click" runat="server" Text="Button" /></div>
                                    </td>
                                    <td width="37%">
                                        <div style="float: left; cursor: pointer; width: 103px; height: 38px; background: url(images/loginMap.jpg);
                                            border: none;" onclick="window.open('/web');">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                          
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var pageHeight = getClientHeight();
        var pageWidth = getClientWidth();
        $(document).ready(function() {
            $("#main_tab").css("margin-top", (pageHeight - 349) / 2);
        });
        function check_form() {           
            if ($("#Text1").val() == "") {
                Popalert("请输入用户名！");
                return false;
            } else if ($("#Text2").val() == "") {
                Popalert("请输入密码！"); return false;
            }
            $("#Button1").click();
            return true;
//            else if ($("#Text3").val() == "") {
//                Popalert("请输入验证码！"); return false;
//            }           
        }
        document.onkeydown = function(event) {
            e = event ? event : (window.event ? window.event : null);
            if (e.keyCode == 13) {              
                check_form();
            }
        }
     
        function rest() {
            $("#Text1").val("");
            $("#Text2").val("");           
        }
        function Popalert(str) {
            PopupDiv(document.getElementById('main_body'), str, 2000, "yellow", 300, 25);
        }
    </script>

    </form>
</body>
</html>
