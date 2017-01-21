<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student_add.aspx.cs" Inherits="info_student_add" %>

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
                <input class="input" style="width: 150px;" id="student_pk" type="hidden" value="<%=edit_pk %>" />
             
               <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        手机：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="student_phone" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        真实姓名：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="student_real_name" type="text" />
                    </td>
                </tr>
                <tr style="display:none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        性别：
                    </td>
                    <td>
                         <input id="Radio1" type="radio" checked="checked" name="student_sex" value="男" />男&nbsp;&nbsp;&nbsp;<input
                            id="Radio2" name="student_sex" value="女" type="radio" />女
                    </td>
                </tr>
                
               
                
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        图像：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="student_pic" type="text" />
                    </td>
                </tr>
                
                 <tr style=" display:none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        邀请码：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="student_incode" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        状态：
                    </td>
                    <td>
                        <select id="student_state" style="width: 150px; height: 26px; margin: 5px;" class="input">
                            <option value="0" style="color: Red;">待审核</option>
                            <option value="1" style="color: Green;">正常</option>
                            <option value="2" style="color: Red;">锁定</option>
                        </select>
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

            
                if ($("#student_name").val() == "") { Popalert("昵称不能为空！"); return; }
                if ($("#student_real_name").val() == "") { Popalert("真实姓名不能为空！"); return; }                          
                if ($("#student_phone").val() == "") { Popalert("电话不能为空！"); return; }                         
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_student", student_pk: $("#student_pk").val(),  student_real_name: $("#student_real_name").val(), student_sex: $("input[name='student_sex']:checked").val(), student_age: $("#student_age").val(), student_phone: $("#student_phone").val(), student_pic: $("#student_pic").val(), student_state: $("#student_state").val(),student_incode:$("#student_incode").val(), c: new Date().getTime() },
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
                    data: { com: "get_student_info", student_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];                           
                            $("#student_name").val(info.student_name);
                            $("#student_real_name").val(info.student_real_name);
                            $("input[value='" + info.student_sex + "']").attr("checked", true); 
                            $("#student_age").val(info.student_age);
                            $("#student_phone").val(info.student_phone);
                            $("#student_pic").val(info.student_pic);
                            $("#student_state").val(info.student_state);
                            $("#student_incode").val(info.student_incode);
                            $("#student_allow_car").val(info.student_allow_car);                           
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
