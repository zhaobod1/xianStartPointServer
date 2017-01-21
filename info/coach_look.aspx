<%@ Page Language="C#" AutoEventWireup="true" CodeFile="coach_look.aspx.cs" Inherits="info_coach_look" %>

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
                   <%-- <td style="background-color: #F4F2F3; text-align: right;">
                        城市：
                    </td>
                    <td>
                        <span id="coach_city"></span>
                    </td>--%>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        姓名：
                    </td>
                    <td >
                        <span id="coach_name"></span>
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        账户余额：
                    </td>
                    <td >
                        <span id="coach_money"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        性别：
                    </td>
                    <td>
                        <span id="coach_sex"></span>
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        年龄：
                    </td>
                    <td>
                        <span id="coach_age"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        身份证号：
                    </td>
                    <td>
                        <span id="coach_number"></span>
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        手机：
                    </td>
                    <td>
                        <span id="coach_phone"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        自我介绍：
                    </td>
                    <td colspan="3">
                        <span id="coach_myself"></span>
                    </td>
                </tr>
                <tr>
                     <td style="background-color: #F4F2F3; text-align: right;">
                        车型：
                    </td>
                    <td>
                        <span id="coach_car_type"></span>
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        车牌号：
                    </td>
                    <td>
                        <span id="coach_car_number"></span>
                    </td>
                 
                   
                </tr>
              <tr>
                       <td style="background-color: #F4F2F3; text-align: right;">
                        驾龄：
                    </td>
                    <td colspan="3">
                        <span id="coach_long"></span>
                    </td>
                    <%--  <td style="background-color: #F4F2F3; text-align: right;">
                        教练员证编号：
                    </td>
                    <td>
                        <span id="coach_teacher_number"></span>
                    </td>
                </tr>--%>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        本人头像：
                    </td>
                    <td colspan="3">
                        <span id="coach_pic"></span>
                    </td>
                   <%-- <td style="background-color: #F4F2F3; text-align: right;">
                        教练证正面：
                    </td>
                    <td>
                        <span id="coach_teacher_pic"></span>
                    </td>--%>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        驾驶证正面：
                    </td>
                    <td>
                        <span id="coach_driver_pic1"></span>
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        行驶证正面：
                    </td>
                    <td>
                        <span id="coach_driver_pic2"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        身份证正面：
                    </td>
                    <td>
                        <span id="coach_card_pic1"></span>
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        身份证反面：
                    </td>
                    <td>
                        <span id="coach_card_pic2"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        服务：
                    </td>
                    <td colspan="3">
                        <span id="coach_service"></span>
                    </td>
                </tr>
               <%-- <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        邀请码：
                    </td>
                    <td>
                        <span id="coach_incode"></span>
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        类型：
                    </td>
                    <td>
                        <span id="coach_type"></span>
                    </td>
                </tr>--%>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        抢单范围：
                    </td>
                    <td>
                        <span id="coach_order_range"></span>
                    </td>
                       <td style="background-color: #F4F2F3; text-align: right;">
                        状态：
                    </td>
                    <td>
                        <span id="coach_state"></span>
                    </td>
                   
                </tr>
                <%--<tr> <td style="background-color: #F4F2F3; text-align: right;">
                        默认练车位置：
                    </td>
                    <td>
                        <span id="coach_place"></span>
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        单学时费用：
                    </td>
                    <td>
                        <span id="coach_price"></span>
                    </td>
                 
                </tr>--%>
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
                            $("#coach_pk").html(info.coach_pk);
                            $("#driving_pk").html(info.driving_pk);
                            $("#coach_incode").html(info.coach_incode);
                            $("#coach_city").html(info.coach_city);
                            $("#coach_car_number").html(info.coach_car_number);
                            $("#coach_car_type").html(info.coach_car_name);
                            $("#coach_name").html(info.coach_name);
                            $("#coach_money").html(info.coach_money + "元"); 
                            $("#coach_pwd").html(info.coach_pwd);
                            $("#coach_sex").html(info.coach_sex);
                            
                            $("#coach_age").html(info.coach_age);
                            $("#coach_phone").html(info.coach_phone);
                            $("#coach_long").html(info.coach_long);
                            $("#coach_number").html(info.coach_number);
                            $("#coach_teacher_number").html(info.coach_teacher_number);
                            $("#coach_myself").html(info.coach_myself);
                           
                            $("#coach_subject").html(info.coach_subject);
                            $("#coach_price").html(info.coach_price+"元");
                            $("#coach_score").html(info.coach_score);
                            $("#coach_service").html(info.coach_service);
                            $("#coach_type").html(info.coach_teach);
                            $("#coach_order_range").html(info.coach_order_range+"公里");
                            $("#coach_place").html(info.coach_place);
                            $("#coach_state").html(info.coach_state == "0" ? "<font color='red'>待审核</font>" : info.coach_state == "1" ? "<font color='green'>正常</font>" : "<font color='red'>锁定</font>");
                            if (info.coach_pic != "") $("#coach_pic").html("<img src='" + info.coach_pic + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_teacher_pic != "") $("#coach_teacher_pic").html("<img src='" + info.coach_teacher_pic + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_driver_pic1 != "") $("#coach_driver_pic1").html("<img src='" + info.coach_driver_pic1 + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_driver_pic2 != "") $("#coach_driver_pic2").html("<img src='" + info.coach_driver_pic2 + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_card_pic1 != "") $("#coach_card_pic1").html("<img src='" + info.coach_card_pic1 + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                            if (info.coach_card_pic2 != "") $("#coach_card_pic2").html("<img src='" + info.coach_card_pic2 + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");


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
