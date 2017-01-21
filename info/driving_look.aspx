<%@ Page Language="C#" AutoEventWireup="true" CodeFile="driving_look.aspx.cs" Inherits="info_driving_look" %>

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
                    <td style="width: 30%; background-color: #F4F2F3; text-align: right;">
                        门店名称：
                    </td>
                    <td>
                        <span id="driving_name"></span>
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        地址：
                    </td>
                    <td>
                        <span id="driving_address"></span>
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        电话：
                    </td>
                    <td>
                        <span id="driving_tel"></span>
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        图片：
                    </td>
                    <td>
                        <img id="driving_pic" style=" cursor:pointer; max-height:50px;" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        介绍：
                    </td>
                    <td>
                        <span id="driving_info"></span>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        经纬度：
                    </td>
                    <td>
                        <span id="driving_lat"></span>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        合车人数上限：
                    </td>
                    <td>
                        <span id="driving_merge_count"></span>
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
        $(document).ready(function() {
            $("#content").height(pageHeight - 2);

            $("#iframe_map").width(pageWidth * 80 / 100).height(pageHeight * 80 / 100);
            $("#driving_lat").focus(function() {
                if ($(this).val() == "双击在地图选取") {
                    $(this).val("").css("color", "#000");
                }
            }).blur(function() {
                if ($(this).val() == "") {
                    $(this).val("双击在地图选取").css("color", "#ccc");
                }
            }).dblclick(function() {
                var str = window.showModalDialog("map.htm?r=" + Math.random(), $("#driving_lat").val(), "dialogWidth=" + (screen.width - 10) + "px;dialogHeight=" + (screen.height - 70) + "px;");

                if (str != "" && str != null) $("#driving_lat").val(str).css("color", "#000");

            });



            $("#btn_rest").click(function() {
                parent.closePop();
            });
            if ("<%=edit_pk %>" != "") {
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_driving_info", driving_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];

                            $("#driving_name").html(info.driving_name);
                            $("#driving_address").html(info.p + info.c + info.q + info.driving_address);
                            $("#driving_tel").html(info.driving_tel);
                            $("#driving_pic").attr("src", info.driving_pic).click(function() {
                            window.open(info.driving_pic);
                            });
                            $("#driving_info").html(info.driving_info);
                            $("#driving_lat").html(info.driving_lat);
                            $("#driving_merge_count").html(info.driving_merge_count);

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
