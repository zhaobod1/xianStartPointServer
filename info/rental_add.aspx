<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rental_add.aspx.cs" Inherits="info_rental_add" %>

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
                <input class="input" style="display: none;" id="rental_pk" type="text" />
                <tr>
                    <td style=" width:200px; background-color: #F4F2F3; text-align: right;">
                        起点：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 150px;" id="rental_start_address" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        终点：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 150px;" id="rental_end_address" type="text" />
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        联系人：
                    </td>
                    <td colspan="2">
                        <input class="input" style="width: 150px;" id="rental_name" type="text" />
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        联系方式：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 150px;" id="rental_tel" type="text" />
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        车牌号：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 150px;" id="rental_number" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        时间：
                    </td>
                    <td style=" text-align:center; width:150px;">
                        往<br /><textarea  class="input" style="width: 150px; height:300px;" id="rental_time1"></textarea>
                    </td>
                     <td style=" text-align:center;width:150px;">
                        返<br /><textarea  class="input" style="width: 150px;height:300px;" id="rental_time2"></textarea>
                    </td>
                    <td style="color:Red;">
                    各时间之间请用半角分号【;】隔开
                    </td>
                </tr>
               
                <tr style="display: none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        ：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="rental_rem" type="text" />
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
        var p_id ="" ,c_id = "", a_id = "";
        $(document).ready(function() {
            $("#content").height(pageHeight - 2);
            $("#btn_save").click(function() {
               
                if ($("#rental_start_address").val() == "") { Popalert("不能为空！"); return; }
                if ($("#rental_end_address").val() == "") { Popalert("不能为空！"); return; }
                if ($("#rental_time").val() == "") { Popalert("不能为空！"); return; }
                if ($("#rental_tel").val() == "") { Popalert("不能为空！"); return; }              
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_rental", rental_pk: $("#rental_pk").val(), rental_start_address: $("#rental_start_address").val(), rental_end_address: $("#rental_end_address").val(), rental_number: $("#rental_number").val(), rental_name: $("#rental_name").val(), rental_time1: $("#rental_time1").val(), rental_time2: $("#rental_time2").val(), rental_tel: $("#rental_tel").val(), rental_rem: $("#rental_rem").val(), create_time: $("#create_time").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            Popalert("保存成功！");
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
                    data: { com: "get_rental_info", rental_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];
                            $("#rental_pk").val(info.rental_pk);
                            $("#rental_start_address").val(info.rental_start_address);
                            $("#rental_end_address").val(info.rental_end_address);
                            $("#rental_time1").val(textdecode(info.rental_time1));
                            $("#rental_time2").val(textdecode(info.rental_time2));
                            $("#rental_tel").val(info.rental_tel);
                            $("#rental_name").val(info.rental_name);
                            $("#rental_number").val(info.rental_number);
                            $("#rental_rem").val(info.rental_rem);
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
