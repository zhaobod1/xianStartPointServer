<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="advise_look.aspx.cs"
    Inherits="message_advise_look" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../css/newcss.css" rel="stylesheet" type="text/css" />
    <link id="Systheme" rel="stylesheet" type="text/css" href="../css/theme.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link href="../css/flexigrid.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-1.7.2.js" type="text/javascript"></script>

    <script src="../js/jquery-ui.min.js" type="text/javascript" charset="gb2312"></script>

    <script src="../JS/styleswitch.js" type="text/javascript"></script>

    <script src="../JS/cus_main.js" type="text/javascript"></script>

    <script src="../JS/flexigrid.js" type="text/javascript"></script>

    <script src="../JS/MoveDiv.js" type="text/javascript"></script>

    <script src="../js/province.js" type="text/javascript"></script>

    <style>
        td
        {
            height: 34px;
        }
        span
        {
            width: 90%;
            font-size: 14px;
            line-height: 20px;
            margin: 5px;
            padding: 2px;
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content" style="overflow-y: auto;">
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right; width:30%;">
                        反馈人：
                    </td>
                    <td>
                        <span id="user_name"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        电话：
                    </td>
                    <td>
                        <span id="user_phone"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        简要：
                    </td>
                    <td>
                        <span id="info_title"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        详情：
                    </td>
                    <td>
                        <span id="info_content"></span>
                    </td>
                </tr>
              
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        状态：
                    </td>
                    <td>
                        <span id="info_state"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        时间：
                    </td>
                    <td>
                        <span id="create_time"></span>
                    </td>
                </tr>
            </table>
            <div style="width: 129px; margin: 30px auto;">
                <div id="btn_rest" class="rest" style="float: left;">
                    返 回</div>
            </div>
        </div>
    </div>
    <div id="iframe_Mask" style="background: #333333; width: 100%; height: 100%; left: 0;
        top: 0; position: absolute; z-index: 9999; background-color: Gray; filter: alpha(opacity=80);
        -moz-opacity: 0.5; opacity: 0.5;" name="iframe_Mask">
    </div>
    <div id="divload" style="z-index: 10000; border: solid 1px #B68A07; position: absolute;
        left: 40%; top: 45%; background-color: #ffffdd;">
        <table class="noborder" width="150px" height="50px">
            <tr>
                <td>
                    <table class="noborder">
                        <tr>
                            <td align="right">
                                <img alt="" src="../images/loading.gif" />
                            </td>
                            <td align="left">
                                &nbsp;&nbsp;正在加载...
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script type="text/javascript">
        var pageHeight = getClientHeight();
        var pageWidth = getClientWidth();
        var st = null;
        $(document).ready(function() {
            $("#content").height(pageHeight - 2);

            $("#btn_rest").click(function() {
                parent.closePop();
            });
            if ("<%=edit_pk %>" != "") {
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_info_info", info_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];

                            if (info.user_type == "1") {
                                $("#user_name").html(info.coach_name);
                                $("#user_phone").html(info.coach_phone);
                            }
                            else if (info.user_type == "2") {
                                $("#user_name").html(info.student_real_name);
                                $("#user_phone").html(info.student_phone);
                            }

                            $("#info_title").html(info.info_title);
                            $("#info_content").html(info.info_content);
                          
                            $("#info_state").html(info.info_state == "0" ? "<font color='red'>待处理</font>" : "<font color='green'>已处理</font>");

                            $("#create_time").html(info.create_time);
                        }
                        else {
                            Popalert("操作失败，请重试！");
                        }
                        $("#iframe_Mask").hide();
                        $("#divload").hide();
                    }
                });
            }
            else {
                $("#iframe_Mask").hide();
                $("#divload").hide();
            }

        });
        function textdecode(str) {
            str = str.replace(/&amp;/gi, '&');
            str = str.replace(/&lt;/gi, '<');
            str = str.replace(/&gt;/gi, '>');
            str = str.replace(/&nbsp;/gi, ' ');
            str = str.replace(/''/gi, '"');
            str = str.replace(/<brbr>/gi, '\n');
            return str;
        }
        function OnFaild() {
            Popalert("网络或服务器出现异常，请稍后再试!"); //调用 Popalert函数显示页面提示筐
        }
        function Popalert(str) {
            PopupDiv(document.getElementById('container'), str, 2000, "yellow", 240, 25);
        }
    </script>

</body>
</html>
