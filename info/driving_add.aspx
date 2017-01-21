<%@ Page Language="C#" AutoEventWireup="true" CodeFile="driving_add.aspx.cs" Inherits="info_driving_add" %>

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
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        省份：
                    </td>
                    <td style="padding: 5px 0;">
                        <select id="Select1" class="input" style="width: 200px;">
                            <option value="">请选择</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        城市：
                    </td>
                    <td style="padding: 5px 0;">
                        <select id="Select2" class="input" style="width: 200px;">
                            <option value="">请选择</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        区县：
                    </td>
                    <td style="padding: 5px 0;">
                        <select id="Select3" class="input" style="width: 200px;">
                            <option value="">请选择</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; background-color: #F4F2F3; text-align: right;">
                        门店名称：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="driving_pk" type="hidden" value="<%=edit_pk %>" />
                        <input class="input" style="width: 150px;" id="driving_name" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; background-color: #F4F2F3; text-align: right;">
                        具体地址：
                    </td>
                    <td>
                        <input class="input" style="width: 60%;" id="driving_address" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; background-color: #F4F2F3; text-align: right;">
                        联系电话：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="driving_tel" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; background-color: #F4F2F3; text-align: right;">
                        图片：
                    </td>
                    <td>
                        <span id="driving_pic_span"></span>
                        <input class="input" style="display: none;" id="driving_pic" type="text" />
                        <input id="driving_pic_button" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        介绍：
                    </td>
                    <td>
                        <textarea class="input" style="width: 60%; height: 100px;" id="driving_info"></textarea>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        经纬度：
                    </td>
                    <td>
                        <input class="input" style="width: 150px; color: #ccc;" id="driving_lat" type="text"
                            value="双击选取" />
                    </td>
                </tr>
                <tr style="display: none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        合车人数上限：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="driving_merge_count" type="text" />
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
        var s1, s2, s3;
        var pageHeight = getClientHeight();
        var pageWidth = getClientWidth();
        var p_id = "", c_id = "", a_id = "";
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


            $("#btn_save").click(function() {

                if ($("#driving_name").val() == "") { Popalert("门店名称不能为空！"); return; }
                //if ($("#driving_lat").val() == "") { Popalert("经纬度不能为空！"); return; }
                //if ($("#driving_merge_count").val() == "") { Popalert("合车人数上限不能为空！"); return; }
                if ($("#Select3").val() == "") { Popalert("请选择门店所在区县！"); return; }
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_driving", p_id: $("#Select1").val(), c_id: $("#Select2").val(), area_id: $("#Select3").val(), driving_pk: $("#driving_pk").val(), driving_name: $("#driving_name").val(), driving_address: $("#driving_address").val(), driving_tel: $("#driving_tel").val(), driving_pic: $("#driving_pic").val(), driving_info: $("#driving_info").val(), driving_lat: $("#driving_lat").val(), driving_merge_count: $("#driving_merge_count").val(), c: new Date().getTime() },
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
                    data: { com: "get_driving_info", driving_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];
                            $("#driving_name").val(info.driving_name);
                            $("#driving_address").val(info.driving_address);
                            $("#driving_tel").val(info.driving_tel);
                            $("#driving_pic").val(info.driving_pic);
                            $("#driving_info").val(info.driving_info);
                            $("#driving_lat").val(info.driving_lat).css({ "color": "#000" });
                            $("#driving_merge_count").val(info.driving_merge_count);
                            p_id = info.p_id;
                            c_id = info.c_id;
                            a_id = info.area_id;
                            set_p();
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
                set_p();
                $("#iframe_Mask").hide();
                $("#divload").hide();
            }

            $("#Select1").change(function() {
                if (s1) return;
                s1 = true;
                $("#Select2 option").remove();

                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_City", v: $("#Select1").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                    },
                    success: function(da) {
                        if (da != null) {
                            $("#Select2").append("<option value=\"\">请选择</option>");
                            for (var i = 0; i < da.Table.length; i++) {
                                var info = da.Table[i];
                                $("#Select2").append("<option value=\"" + info["CityID"] + "\">" + info["CityName"] + "</option>");
                            }
                            if (c_id != "") {
                                $("#Select2").val(c_id).change();
                                c_id = "";
                            }
                            s1 = false;
                        }
                    }
                });
            });
            $("#Select2").change(function() {
                if (s2) return;
                s2 = true;
                $("#Select3 option").remove();
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_District", v: $("#Select2").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                    },
                    success: function(da) {
                        if (da != null) {
                            $("#Select3").append("<option value=\"\">请选择</option>");
                            for (var i = 0; i < da.Table.length; i++) {
                                var info = da.Table[i];
                                $("#Select3").append("<option  value=\"" + info["DistrictID"] + "\">" + info["DistrictName"] + "</option>");
                            }
                            if (a_id != "") {
                                $("#Select3").val(a_id);
                                a_id = "";
                            }

                            s2 = false;
                        }
                    }
                });
            });


        });
        function set_p() {
            $.ajax({
                url: "getdata.aspx",
                type: "post",
                data: { com: "get_Province", c: new Date().getTime() },
                dataType: "json",
                error: function(err) {
                },
                success: function(da) {
                    if (da != null) {
                        $("#Select1 option").remove();
                        $("#Select1").append("<option value=\"\">请选择</option>");
                        for (var i = 0; i < da.Table.length; i++) {
                            var info = da.Table[i];

                            $("#Select1").append("<option value=\"" + info["ProvinceID"] + "\">" + info["ProvinceName"] + "</option>");
                        }
                        if (p_id != "") {
                            $("#Select1").val(p_id).change();
                            p_id = "";
                        }
                    }
                }
            });
        }
        KindEditor.ready(function(K) {
            var editor = K.editor({
                allowFileManager: false
            });
            K('#driving_pic_button').click(function() {
                editor.loadPlugin('image', function() {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        clickFn: function(url, title, width, height, border, align) {
                            K('#driving_pic').val(url);
                            editor.hideDialog();
                            $("#driving_pic_span").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
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
