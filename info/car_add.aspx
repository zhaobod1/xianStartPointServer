<%@ Page Language="C#" AutoEventWireup="true" CodeFile="car_add.aspx.cs" Inherits="info_car_add" %>

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
                <input class="input" style="width: 150px;" id="car_pk" type="hidden" value="<%=edit_pk %>" />
              
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        车型：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="car_name" type="text" />
                    </td>
               
                    <td style="background-color: #F4F2F3; text-align: right;">
                        图片：
                    </td>
                    <td> <span id="car_img_span"></span>
                        <input class="input" style=" display:none;" id="car_img" type="text" />
                          <input id="car_img_button" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        起租价：
                    </td>
                    <td >
                        <input class="input" style="width: 150px;" id="car_start_price" type="text" />元
                    </td>
                     <td style="background-color: #F4F2F3; text-align: right;">
                        起租价包含里程：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="car_meal_away" type="text" />公里
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        里程费：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="car_away_price" type="text" />元/公里
                    </td>
               
                    <td style="background-color: #F4F2F3; text-align: right;">
                        时长费：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="car_time_price" type="text" />元/分钟
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        远途费：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="car_far_price" type="text" />元/公里
                    </td>
              
                    <td style="background-color: #F4F2F3; text-align: right;">
                        远途标准：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="car_far_away" type="text" />公里以上
                    </td>
                </tr>
                <tr style="display:none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        套餐价：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 150px;" id="car_meal_price" type="text" />元
                    </td>
                </tr>
                <tr style="display:none;">
                   
               
                    <td style="background-color: #F4F2F3; text-align: right;">
                        套餐包含时长：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="car_meal_time" type="text" />分钟
                    </td>
                </tr>
                <tr style="display:none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        超出套餐里程费：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="car_go_away_price" type="text" />元/公里
                    </td>
               
                    <td style="background-color: #F4F2F3; text-align: right;">
                        超出套餐时长费：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="car_go_time_price" type="text" />元/分钟
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        备注：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 80%;" id="car_rem" type="text" />
                    </td>
                </tr>
               
            </table>
            <div style="width: 258px; margin: 30px auto;">
                <div id="btn_save" class="submit" style="float: left;">
                    保 存</div>
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
            $("#btn_save").click(function() {              
                if ($("#car_name").val() == "") { Popalert("车型不能为空！"); return; }
                if ($("#car_img").val() == "") { Popalert("请上传车型图片！"); return; }
                if ($("#car_start_price").val() == "") { Popalert("起租价不能为空！"); return; }
                if ($("#car_away_price").val() == "") { Popalert("里程费不能为空！"); return; }
                if ($("#car_time_price").val() == "") { Popalert("时长费不能为空！"); return; }
                if ($("#car_far_price").val() == "") { Popalert("远途费不能为空！"); return; }
                if ($("#car_far_away").val() == "") { Popalert("远途标准不能为空！"); return; }
                if ($("#car_meal_price").val() == "") { Popalert("套餐价不能为空！"); return; }
                if ($("#car_meal_away").val() == "") { Popalert("套餐里程不能为空！"); return; }
                if ($("#car_meal_time").val() == "") { Popalert("套餐时长不能为空！"); return; }
                if ($("#car_go_away_price").val() == "") { Popalert("超出套餐里程费不能为空！"); return; }
                if ($("#car_go_time_price").val() == "") { Popalert("超出套餐时长费不能为空！"); return; }               
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_car", car_pk: $("#car_pk").val(), car_name: $("#car_name").val(), car_img: $("#car_img").val(), car_start_price: $("#car_start_price").val(), car_away_price: $("#car_away_price").val(), car_time_price: $("#car_time_price").val(), car_far_price: $("#car_far_price").val(), car_far_away: $("#car_far_away").val(), car_meal_price: $("#car_meal_price").val(), car_meal_away: $("#car_meal_away").val(), car_meal_time: $("#car_meal_time").val(), car_go_away_price: $("#car_go_away_price").val(), car_go_time_price: $("#car_go_time_price").val(), car_rem: $("#car_rem").val(),c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            parent.data = da;
                            parent.jumpToPage(parent.currentPage);
                            parent.closePop();    
                        }
                        else {
                            Popalert("操作失败，请重试！");
                        }
                        $("#iframe_Mask").hide();
                        $("#divload").hide();
                    }
                });
            });
            $("#btn_rest").click(function() {
                parent.closePop();
            });
            if ("<%=edit_pk %>" != "") {
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_car_info", car_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];                           
                            $("#car_name").val(info.car_name);
                            $("#car_img").val(info.car_img);
                            $("#car_start_price").val(info.car_start_price);
                            $("#car_away_price").val(info.car_away_price);
                            $("#car_time_price").val(info.car_time_price);
                            $("#car_far_price").val(info.car_far_price);
                            $("#car_far_away").val(info.car_far_away);
                            $("#car_meal_price").val(info.car_meal_price);
                            $("#car_meal_away").val(info.car_meal_away);
                            $("#car_meal_time").val(info.car_meal_time);
                            $("#car_go_away_price").val(info.car_go_away_price);
                            $("#car_go_time_price").val(info.car_go_time_price);
                            $("#car_rem").val(textdecode(info.car_rem));
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
        KindEditor.ready(function(K) {
            var editor = K.editor({
                allowFileManager: false
            });
            K('#car_img_button').click(function() {
                editor.loadPlugin('image', function() {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        clickFn: function(url, title, width, height, border, align) {
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
