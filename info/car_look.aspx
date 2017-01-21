<%@ Page Language="C#" AutoEventWireup="true" CodeFile="car_look.aspx.cs" Inherits="info_car_look" %>

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

    <style>
        #preview {
            position: absolute;
            border: 1px solid #ccc;
            background: #333;
            padding: 5px;
            display: none;
            color: #fff;
        }

        td {
            height: 34px;
        }

        span {
            width: 90%;
            font-size: 14px;
            line-height: 20px;
            margin: 5px;
            padding: 2px;
            display: block;
        }

        #preview {
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
                    <input class="input" style="width: 150px;" id="car_pk" type="hidden" value="<%=edit_pk %>" />

                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">车型：
                        </td>
                        <td>
                            <span id="car_name"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">图片：
                        </td>
                        <td>
                            <span id="car_img_span"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">起租价：
                        </td>
                        <td>
                            <span id="car_start_price"></span>
                        </td>
                        <td style="background-color: #F4F2F3; text-align: right;">起租价包含里程：
                        </td>
                        <td>
                            <span id="car_meal_away"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">里程费：
                        </td>
                        <td>
                            <span id="car_away_price"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">时长费：
                        </td>
                        <td>
                            <span id="car_time_price"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">远途费：
                        </td>
                        <td>
                            <span id="car_far_price"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">远途标准：
                        </td>
                        <td>
                            <span id="car_far_away"></span>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td style="background-color: #F4F2F3; text-align: right;">套餐价：
                        </td>
                        <td colspan="3">
                            <span id="car_meal_price"></span>
                        </td>
                    </tr>
                    <tr style="display: none;">


                        <td style="background-color: #F4F2F3; text-align: right;">套餐包含时长：
                        </td>
                        <td>
                            <span id="car_meal_time"></span>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td style="background-color: #F4F2F3; text-align: right;">超出套餐里程费：
                        </td>
                        <td>
                            <span id="car_go_away_price"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">超出套餐时长费：
                        </td>
                        <td>
                            <span id="car_go_time_price"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">备注：
                        </td>
                        <td colspan="3">
                            <span id="car_rem"></span>
                        </td>
                    </tr>

                </table>
                <div style="width: 219px; margin: 30px auto;">

                    <div id="btn_rest" class="rest" style="margin-left: 40px; float: left;">
                        返 回
                    </div>
                </div>
            </div>
        </div>
        <div id="iframe_Mask" style="background: #333333; width: 100%; height: 100%; left: 0; top: 0; position: absolute; z-index: 9999; background-color: Gray; filter: alpha(opacity=80); -moz-opacity: 0.5; opacity: 0.5;"
            name="iframe_Mask">
        </div>
        <div id="divload" style="z-index: 10000; border: solid 1px #B68A07; position: absolute; left: 40%; top: 45%; background-color: #ffffdd;">
            <table class="noborder" width="150px" height="50px">
                <tr>
                    <td>
                        <table class="noborder">
                            <tr>
                                <td align="right">
                                    <img alt="" src="../images/loading.gif" />
                                </td>
                                <td align="left">&nbsp;&nbsp;正在加载...
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
        $(document).ready(function () {
            $("#content").height(pageHeight - 2);

            $("#btn_rest").click(function () {
                parent.closePop();
            });
            if ("<%=edit_pk %>" != "") {
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_car_info", car_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function (err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function (da) {
                        if (da != null) {
                            var info = da.Table[0];
                            $("#car_name").html(info.car_name);
                            $("#car_start_price").html(info.car_start_price + "元");
                            $("#car_away_price").html(info.car_away_price + "元/公里");
                            $("#car_time_price").html(info.car_time_price + "元/分钟");
                            $("#car_far_price").html(info.car_far_price + "元/公里");
                            $("#car_far_away").html(info.car_far_away + "公里以上");
                            $("#car_meal_price").html(info.car_meal_price + "元");
                            $("#car_meal_away").html(info.car_meal_away + "公里");
                            $("#car_meal_time").html(info.car_meal_time + "分钟");
                            $("#car_go_away_price").html(info.car_go_away_price + "元/公里");
                            $("#car_go_time_price").html(info.car_go_time_price + "元/分钟");
                            $("#car_rem").html(textdecode1(info.car_rem));

                            if (info.car_img != "") $("#car_img_span").html("<img src='" + info.car_img + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");

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
        KindEditor.ready(function (K) {
            var editor = K.editor({
                allowFileManager: false
            });
            K('#car_img_button').click(function () {
                editor.loadPlugin('image', function () {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        clickFn: function (url, title, width, height, border, align) {
                            K('#car_img').val(url);
                            editor.hideDialog();
                            $("#car_img_span").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            imagePreview();
                        }
                    });
                });
            });

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
