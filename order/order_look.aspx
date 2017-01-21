<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="order_look.aspx.cs"
    Inherits="order_order_look" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../css/newcss.css" rel="stylesheet" type="text/css" />
    <link id="Systheme" rel="stylesheet" type="text/css" href="../css/theme.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link href="../css/flexigrid.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />

    <script src="../js/main.js" type="text/javascript"></script>

    <script src="../js/jquery-1.7.2.js" type="text/javascript"></script>

    <script src="../js/jquery-ui.min.js" type="text/javascript" charset="gb2312"></script>

    <script src="../JS/styleswitch.js" type="text/javascript"></script>

    <script src="../JS/cus_main.js" type="text/javascript"></script>

    <script src="../JS/flexigrid.js" type="text/javascript"></script>

    <script src="../JS/MoveDiv.js" type="text/javascript"></script>

    <script src="../js/province.js" type="text/javascript"></script>

    <style>
        #table_list td {
            padding: 10px 0;
        }

        #table_list thead th {
            padding: 5px;
            font-weight: bold;
            text-align: center;
        }
    </style>
    <style>
        td {
            height: 34px;
        }

        #table_list td {
            height: 12px;
            padding: 0;
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

        .table_list th {
            font-weight: bold;
            padding: 5px;
            text-align: center;
            color: #194264;
            height: 24px;
            background-color: #DBF1FB;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <div id="content">
                <table class="ie7_table" width="100%" cellpadding="0" cellspacing="0" style="overflow: hidden;">
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right; width:20%;">订单类型：
                        </td>
                        <td style="width:30%;">
                            <span id="order_type"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;width:20%;">乘车时间：
                        </td>
                        <td style="width:30%;">
                            <span id="order_datetime"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">客户：
                        </td>
                        <td>
                            <span id="user_name"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">电话：
                        </td>
                        <td>
                            <span id="user_tel"></span> <span id="user_sms"></span>
                        </td>
                    </tr>
                     <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">上车地点：
                        </td>
                        <td>
                            <span id="start_address"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">下车地点：
                        </td>
                        <td>
                            <span id="end_address"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">司机：
                        </td>
                        <td>
                            <span id="coach_name"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">电话：
                        </td>
                        <td>
                            <span id="coach_phone"></span>
                        </td>
                    </tr>
                   
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">车型：
                        </td>
                        <td>
                            <span id="car_type"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">车牌号：
                        </td>
                        <td>
                            <span id="coach_car_number"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">里程：
                        </td>
                        <td>
                            <span id="order_away"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">时长：
                        </td>
                        <td>
                            <span id="order_time"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">起租/套餐价：
                        </td>
                        <td>
                            <span id="car_meal_fee"></span>
                        </td>

                          <td style="background-color: #F4F2F3; text-align: right;">实付：
                        </td>
                        <td>
                            <span id="order_fee"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">里程费：
                        </td>
                        <td>
                            <span id="order_away_fee"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">时长费：
                        </td>
                        <td>
                            <span id="order_time_fee"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">远途费：
                        </td>
                        <td>
                            <span id="order_far_away_fee"></span>
                        </td>                      
                   
                        <td style="background-color: #F4F2F3; text-align: right;">优惠：
                        </td>
                        <td>
                            <span id="order_cut_fee"></span>
                        </td>

                      
                    </tr>
                    <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">备注：
                        </td>
                        <td colspan="3">
                            <span id="order_rem"></span>
                        </td>                     
                    </tr>
                     <tr>
                        <td style="background-color: #F4F2F3; text-align: right;">状态：
                        </td>
                        <td>
                            <span id="order_state"></span>
                        </td>

                        <td style="background-color: #F4F2F3; text-align: right;">创建日期：
                        </td>
                        <td>
                            <span id="create_time"></span>
                        </td>
                    </tr>
                </table>
                <div style="width: 219px; margin: 30px auto; position: relative;">
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
        var st = null;
        $(document).ready(function () {
            $("#content").height(pageHeight - 2);
            $("#infobox").width(pageWidth - 34).height($("#content").height() - 435);
            //$("#table_list thead tr th:eq(6),#table_list thead tr th:eq(7)").width(($("#infobox").width() - 660) / 2);
            $("#btn_rest").click(function () {
                parent.closePop();
            });
            if ("<%=edit_pk %>" != "") {

                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_order_info", order_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function (err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function (da) {

                        if (da != null) {
                            var info = da.Table[0];
                            $("#order_type").html(info.order_type == "0" ? "立即叫车" : info.order_type == "1" ? "预约" : info.order_type == "2" ? "半日租" : "日租");
                            $("#order_datetime").html(info.order_datetime);
                            $("#user_name").html(info.user_name);
                            $("#user_tel").html(info.user_tel);
                            $("#user_sms").html(info.user_sms=="1"?"短信通知":"");
                            $("#coach_name").html(info.coach_name);
                            $("#coach_phone").html(info.coach_phone);
                            $("#start_address").html(info.start_address + "(" + info.start_lon + "," + info.start_lat + ")");
                            $("#end_address").html(info.end_address + "(" + info.end_lon + "," + info.end_lat + ")");
                            $("#car_type").html(info.car_name);
                            $("#coach_car_number").html(info.coach_car_number);
                            $("#order_away").html(info.order_away+"公里");
                            $("#order_time").html(info.order_time + "分钟");
                            $("#car_meal_fee").html("￥"+info.car_meal_fee);
                            $("#order_away_fee").html("￥" + info.order_away_fee);
                            $("#order_time_fee").html("￥" + info.order_time_fee);
                            $("#order_far_away_fee").html("￥" + info.order_far_away_fee);
                            $("#order_cut_fee").html("￥" + info.order_cut_fee);
                            $("#order_fee").html("￥" + info.order_fee);
                            $("#order_rem").html(info.order_rem);
                            $("#order_state").html(GetOrderState(info.order_state));
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
