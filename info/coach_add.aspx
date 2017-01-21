<%@ Page Language="C#" AutoEventWireup="true" CodeFile="coach_add.aspx.cs" Inherits="info_coach_add" %>

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
                <input class="input" style="width: 150px;" id="coach_pk" type="hidden" value="<%=edit_pk %>" />
                <tr style="display: none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        所属驾校：
                    </td>
                    <td>
                        <select id="driving_pk" style="width: 150px; height: 26px; margin: 5px;" class="input">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <option value="<%#Eval("driving_pk") %>">
                                        <%#Eval("driving_name")%></option>
                                </ItemTemplate>
                            </asp:Repeater>
                        </select>
                    </td>
                </tr>
                <tr>
                    <%--<td style="background-color: #F4F2F3; text-align: right;">
                        城市：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_city" type="text" />
                    </td>--%>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        姓名：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 150px;" id="coach_name" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        性别：
                    </td>
                    <td>
                        <input id="Radio1" type="radio" checked="checked" name="coach_sex" value="男" />男&nbsp;&nbsp;&nbsp;<input
                            id="Radio2" name="coach_sex" value="女" type="radio" />女
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        年龄：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_age" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        身份证号：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_number" type="text" />
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        手机：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_phone" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        自我介绍：
                    </td>
                    <td colspan="3">
                        <textarea class="input" style="width: 80%; height: 60px;" id="coach_myself"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        车型：
                    </td>
                    <td >
                        <select id="coach_car_type" style="width: 150px; height: 26px; margin: 5px;" class="input">
                            <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <option value="<%#Eval("car_pk") %>" ><%#Eval("car_name") %></option>
                                
                            </ItemTemplate>
                        </asp:Repeater>
                             
                         
                        </select>
                       
                    </td> 
                    <td style="background-color: #F4F2F3; text-align: right;">
                        车牌号：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_car_number" type="text" />
                    </td>
                   
                  
                </tr>
               <tr>
                    <td style="background-color: #F4F2F3; text-align: right;" >
                        驾龄：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 150px;" id="coach_long" type="text" />
                    </td>
                       <%--<td style="background-color: #F4F2F3; text-align: right;">
                        教练员证编号：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_teacher_number" type="text" />
                    </td>
                </tr>--%>
                </tr>
                <tr>
                     <td style="background-color: #F4F2F3; text-align: right;" >
                       星级：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 150px;" id="coach_star" type="text" />
                    </td>
                </tr>
                   
                   <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        本人头像：
                    </td>
                    <td colspan="3">
                       <span id="coach_pic_span"></span>
                        <input class="input" style="display: none;" id="coach_pic" type="text" />
                        <input id="coach_pic_button" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />
                      
                    </td>
                   <%-- <td style="background-color: #F4F2F3; text-align: right;">
                        教练证正面：
                    </td>
                    <td>
                     <span id="coach_teacher_pic_span"></span>
                        <input class="input" style="display: none;" id="coach_teacher_pic" type="text" />
                        <input id="coach_teacher_button" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />                                                 
                    </td>--%>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        驾驶证正面：
                    </td>
                    <td>
                    <span id="coach_driver_pic1_span"></span>
                        <input class="input" style="display: none;" id="coach_driver_pic1" type="text" />
                        <input id="coach_driver_pic1_button" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />
                      
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        行驶证正面：
                    </td>
                    <td>
                     <span id="coach_driver_pic2_span"></span>
                        <input class="input" style="display: none;" id="coach_driver_pic2" type="text" />
                        <input id="coach_driver_pic2_button" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />
                      
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        身份证正面：
                    </td>
                    <td>
                     <span id="coach_card_pic1_span"></span>
                        <input class="input" style="display: none;" id="coach_card_pic1" type="text" />
                        <input id="coach_card_pic1_button" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />
                       
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        身份证反面：
                    </td>
                    <td>
                     <span id="coach_card_pic2_span"></span>
                        <input class="input" style="display: none;" id="coach_card_pic2" type="text" />
                        <input id="coach_card_pic2_button" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />
                     
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        服务：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 80%;" id="coach_service" type="text" />
                    </td>
                </tr>
                <%--<tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        邀请码：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_incode" type="text" />
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        类型：
                    </td>
                    <td>
                        <select id="coach_type" style="width: 150px; height: 26px; margin: 5px;" class="input">
                            <option value="1">教练</option>
                            <option value="2">陪练</option>
                        </select>
                    </td>
                </tr>--%>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        抢单范围：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_order_range" type="text" />公里
                    </td>
                     <td style="background-color: #F4F2F3; text-align: right;">
                        状态：
                    </td>
                    <td>
                        <select id="coach_state" style="width: 150px; height: 26px; margin: 5px;" class="input">
                            <option value="0" style="color: Red;">待审核</option>
                            <option value="1" style="color: Green;">正常</option>
                              <option value="2" style="color: Red;">锁定</option>
                        </select>
                    </td>
                   <%-- <td style="background-color: #F4F2F3; text-align: right;">
                        默认练车位置：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_place" type="text" />
                    </td>--%>
                </tr>
                 <%--<tr>
                <td style="background-color: #F4F2F3; text-align: right;">
                        单学时费用：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="coach_price" type="text" />元
                    </td>
                   
                </tr>--%>
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
                if ($("#coach_name").val() == "") { Popalert("姓名不能为空！"); return; }
                if ($("#coach_phone").val() == "") { Popalert("手机不能为空！"); return; }
              
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_coach", coach_pk: $("#coach_pk").val(), driving_pk: $("#driving_pk").val(), coach_incode: $("#coach_incode").val(), coach_city: $("#coach_city").val(), coach_car_number: $("#coach_car_number").val(), coach_car_type: $("#coach_car_type").val(), coach_name: $("#coach_name").val(), coach_sex: $("input[name='coach_sex']:checked").val(), coach_age: $("#coach_age").val(), coach_phone: $("#coach_phone").val(), coach_long: $("#coach_long").val(), coach_number: $("#coach_number").val(), coach_teacher_number: $("#coach_teacher_number").val(), coach_subject: $("#coach_subject").val(), coach_myself: $("#coach_myself").val(), coach_pic: $("#coach_pic").val(), coach_teacher_pic: $("#coach_teacher_pic").val(), coach_driver_pic1: $("#coach_driver_pic1").val(), coach_driver_pic2: $("#coach_driver_pic2").val(), coach_card_pic1: $("#coach_card_pic1").val(), coach_card_pic2: $("#coach_card_pic2").val(), coach_score: $("#coach_score").val(), coach_service: $("#coach_service").val(), coach_type: $("#coach_type").val(), coach_order_range: $("#coach_order_range").val(), coach_place: $("#coach_place").val(), coach_price: $("#coach_price").val(), coach_state: $("#coach_state").val(), c: new Date().getTime() },
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
                    data: { com: "get_coach_info", coach_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];                           
                            $("#coach_pk").val(info.coach_pk);
                            $("#driving_pk").val(info.driving_pk);
                            $("#coach_incode").val(info.coach_incode);
                            $("#coach_city").val(info.coach_city);
                            $("#coach_car_number").val(info.coach_car_number);
                            $("#coach_car_type").val(info.coach_car_type);
                            $("#coach_name").val(info.coach_name);
                            $("#coach_pwd").val(info.coach_pwd);
                            $("input[value='" + info.coach_sex + "']").attr("checked", true);
                            $("#coach_age").val(info.coach_age);
                            $("#coach_phone").val(info.coach_phone);
                            $("#coach_long").val(info.coach_long);
                            $("#coach_number").val(info.coach_number);
                            $("#coach_teacher_number").val(info.coach_teacher_number);
                            $("#coach_myself").val(info.coach_myself);
                            $("#coach_pic").val(info.coach_pic);
                            $("#coach_teacher_pic").val(info.coach_teacher_pic);
                            $("#coach_driver_pic1").val(info.coach_driver_pic1);
                            $("#coach_driver_pic2").val(info.coach_driver_pic2);
                            $("#coach_card_pic1").val(info.coach_card_pic1);
                            $("#coach_card_pic2").val(info.coach_card_pic2);
                            $("#coach_subject").val(info.coach_subject);
                            $("#coach_price").val(info.coach_price);
                            $("#coach_score").val(info.coach_score);
                            $("#coach_service").val(info.coach_service);
                            $("#coach_type").val(info.coach_type);
                            $("#coach_order_range").val(info.coach_order_range);
                            $("#coach_place").val(info.coach_place);
                            $("#coach_state").val(info.coach_state);
                            if (info.coach_pic != "") $("#coach_pic_span").html("<img src='" + info.coach_pic + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_teacher_pic != "") $("#coach_teacher_pic_span").html("<img src='" + info.coach_teacher_pic + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_driver_pic1 != "") $("#coach_driver_pic1_span").html("<img src='" + info.coach_driver_pic1 + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_driver_pic2 != "") $("#coach_driver_pic2_span").html("<img src='" + info.coach_driver_pic2 + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_card_pic1 != "") $("#coach_card_pic1_span").html("<img src='" + info.coach_card_pic1 + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_card_pic2 != "") $("#coach_card_pic2_span").html("<img src='" + info.coach_card_pic2 + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");


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
            K('#coach_pic_button').click(function() {
                editor.loadPlugin('image', function() {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        clickFn: function(url, title, width, height, border, align) {
                            K('#coach_pic').val(url);
                            editor.hideDialog();
                            $("#coach_pic_span").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            imagePreview();
                        }
                    });
                });
            });
            K('#coach_teacher_pic_button').click(function() {
                editor.loadPlugin('image', function() {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        clickFn: function(url, title, width, height, border, align) {
                            K('#coach_teacher_pic').val(url);
                            editor.hideDialog();
                            $("#coach_teacher_pic_span").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            imagePreview();
                        }
                    });
                });
            });
            K('#coach_driver_pic1_button').click(function() {
                editor.loadPlugin('image', function() {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        clickFn: function(url, title, width, height, border, align) {
                            K('#coach_driver_pic1').val(url);
                            editor.hideDialog();
                            $("#coach_driver_pic1_span").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            imagePreview();
                        }
                    });
                });
            });
            K('#coach_driver_pic2_botton').click(function() {
                editor.loadPlugin('image', function() {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        clickFn: function(url, title, width, height, border, align) {
                            K('#coach_driver_pic2').val(url);
                            editor.hideDialog();
                            $("#coach_driver_pic2_span").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            imagePreview();
                        }
                    });
                });
            });
            K('#coach_card_pic1_button').click(function() {
                editor.loadPlugin('image', function() {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        clickFn: function(url, title, width, height, border, align) {
                            K('#coach_card_pic1').val(url);
                            editor.hideDialog();
                            $("#coach_card_pic1_span").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            imagePreview();
                        }
                    });
                });
            });
            K('#coach_card_pic2_button').click(function() {
                editor.loadPlugin('image', function() {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        clickFn: function(url, title, width, height, border, align) {
                            K('#coach_card_pic2').val(url);
                            editor.hideDialog();
                            $("#coach_card_pic2_span").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
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
