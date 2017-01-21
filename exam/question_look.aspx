<%@ Page Language="C#" AutoEventWireup="true" CodeFile="question_look.aspx.cs" Inherits="exam_question_look" %>

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

    <link rel="stylesheet" href="../editor/themes/default/default.css" />
    <link rel="stylesheet" href="../editor/plugins/code/prettify.css" />

    <script charset="utf-8" src="../editor/kindeditor.js"></script>

    <script charset="utf-8" src="../editor/lang/zh_CN.js"></script>

    <script charset="utf-8" src="../editor/plugins/code/prettify.js"></script>

    <script src="../js/main.js" type="text/javascript"></script>

    <style>td
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
        #preview
        {
            position: absolute;
            border: 1px solid #ccc;
            background: #333;
            padding: 5px;
            display: none;
            color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content" style="overflow-y: auto;">
            <table width="100%" cellpadding="0" cellspacing="0">
               
                <tr>
                    <td style=" width:20%; background-color: #F4F2F3; text-align: right;">
                        试题类型：
                    </td>
                    <td>
                        <span id="question_type"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        题目：
                    </td>
                    <td>
                        <span id="question_title"></span>
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        图片：
                    </td>
                    <td>
                         <span id="xgt"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        备选答案：
                    </td>
                    <td>
                        <span id="question_answer"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        正确答案：
                    </td>
                    <td>
                        <span id="question_right"></span>
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        解析：
                    </td>
                    <td>
                        <span id="question_rem"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        状态：
                    </td>
                    <td>
                        <span id="question_state"></span>
                    </td>
                </tr>
               
            </table>
            <div style="width: 129px; margin: 30px auto;">
                <div id="btn_rest" class="rest" style="margin-left: 40px; float: left;">
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
        var index = new Array("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z");

        $(document).ready(function() {
            $("#content").height(pageHeight - 2);

            $("#btn_rest").click(function() {
                parent.closePop();
            });
            if ("<%=edit_pk %>" != "") {
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_question_info", question_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];

                            $("#question_type").html(info.question_type);
                            $("#question_title").html(info.question_title);
                            if (info.question_pic != "") $("#xgt").html("<img src='" + info.question_pic + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");

                            var answer = info.question_answer.split('∶');
                            for (var i = 0; i < answer.length; i++) {
                                $("#question_answer").append(index[i] + "、" + answer[i] + "<br>");
                            }
                            if (info.question_type == "单选") {
                                $("#question_right").html(index[info.question_right - 1]);
                            }
                            else {
                                var right = info.question_right.split('∶');
                                for (var i = 0; i < right.length; i++) {
                                    if (right[i] == "") continue;
                                    $("#question_right").append(index[right[i] - 1] + "&nbsp;");
                                }
                            }
                            $("#question_rem").html(info.question_rem);
                            $("#question_state").html(info.question_state == "1" ? "<font color='green'>启用</font>" : "<font color='red'>禁用</font>");

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

        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_map").fadeOut();
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
