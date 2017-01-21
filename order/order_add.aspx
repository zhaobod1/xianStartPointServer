<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="order_add.aspx.cs"
    Inherits="order_order_add" %>

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

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.5&ak=FC9c13967bf240823ed03d702d883e83"></script>
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
        <div id="content">
            <table class="ie7_table" width="100%" cellpadding="0" cellspacing="0" style="overflow: hidden;">
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right; width: 20%;">
                        乘车人手机：
                    </td>
                    <td style="width: 30%;">
                        <input class="input" style="width: 150px;" id="user_tel" type="text" /><input id="user_sms"
                            type="checkbox" />短信通知乘车人
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right; width: 20%;">
                        乘车人姓名：
                    </td>
                    <td style="width: 30%;">
                        <input class="input" id="order_pk" type="hidden" />
                        <input class="input" style="width: 150px;" id="user_pk" type="hidden" />
                        <input class="input" style="width: 150px;" id="user_name" type="text" />
                    </td>
                    
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        预约类型：
                    </td>
                    <td>
                        <select id="order_type" class="input">
                            <option value="0">立即叫车 </option>
                            <option value="1">预约 </option>
                            <option value="2">半日租 </option>
                            <option value="3">日租 </option>
                        </select>
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        用车时间：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="order_datetime" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        上车点：
                    </td>
                    <td>
                        <input class="input" style="width: 150px; color: #ccc;" id="start_address" value="单击地图选取"
                            type="text" />
                            <div id="SartResultPanel" style="border:1px solid #C0C0C0;width:150px;height:auto; display:none;"></div>
                        <input class="input" id="start_lon" type="hidden" />
                        <input class="input" id="start_lat" type="hidden" />
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        下车点：
                    </td>
                    <td>
                        <input class="input" style="width: 150px; color: #ccc;" id="end_address" value="单击地图选取"
                            type="text" />
                            <div id="EndResultPanel" style="border:1px solid #C0C0C0;width:150px;height:auto; display:none;"></div>
                        <input class="input" id="end_lon" type="hidden" />
                        <input class="input" id="end_lat" type="hidden" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        里程：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="order_away" type="text" />公里
                    </td>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        时长：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="order_time" type="text" />分钟
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        车型：
                    </td>
                    <td colspan="3">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <label style=" width:100px;clear:none;" for="check<%# Container.ItemIndex +1%>"><input id="check<%# Container.ItemIndex +1%>" name="car" value="<%#Eval("car_pk") %>" type="checkbox" /><%#Eval("car_name") %></label>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        备注：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 80%;" id="order_rem" type="text" />
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
     <div id="dituContent" style="display:none;">
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
        var st = null;
        $(document).ready(function() {
            $("#content").height(pageHeight - 2);
            $("#infobox").width(pageWidth - 34).height($("#content").height() - 435);
            $("#order_datetime").attr("readonly", "true").datepicker().addClass("input_date").css("background-color", "#FFFFFF");
            $("#btn_rest").click(function() {
                parent.closePop();
            });
            $("#start_address,#end_address").change(function() {

            });
            $("#user_tel").change(function() {
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_user", user_tel: $("#user_tel").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            $("#user_pk").val(da.user_pk);
                            $("#user_name").val(da.user_name);
                        }

                    }
                });
            });
            $("#start_address,#end_address").focus(function() {
                if ($(this).val() == "单击地图选取") {
                    $(this).val("").css("color", "#000");
                }
            }).blur(function() {
                if ($(this).val() == "") {
                    $(this).val("单击地图选取").css("color", "#ccc");
                }
            });
            $("#start_address").focus(function() {
                var str = window.showModalDialog("../map.htm?r=" + Math.random(), $("#driving_lat").val(), "dialogWidth=" + (screen.width - 10) + "px;dialogHeight=" + (screen.height - 70) + "px;");
                if (str != "" && str != null) {
                   
                    $("#start_address").val(str.split('|')[0]).css("color", "#000");
                    $("#start_lon").val(str.split('|')[1]).css("color", "#000");
                    $("#start_lat").val(str.split('|')[2]).css("color", "#000");
                    if ($("#start_lon").val() != "" && $("#start_lat").val() != "" && $("#end_lon").val() != "" && $("#end_lat").val() != "")
                        transit.search(new BMap.Point($("#start_lon").val(), $("#start_lat").val()), new BMap.Point($("#end_lon").val(), $("#end_lat").val()));
                }
            });
            $("#end_address").focus(function() {
                var str = window.showModalDialog("../map.htm?r=" + Math.random(), $("#driving_lat").val(), "dialogWidth=" + (screen.width - 10) + "px;dialogHeight=" + (screen.height - 70) + "px;");
                if (str != "" && str != null) {
                    $("#end_address").val(str.split('|')[0]).css("color", "#000");
                    $("#end_lon").val(str.split('|')[1]).css("color", "#000");
                    $("#end_lat").val(str.split('|')[2]).css("color", "#000");
                    if ($("#start_lon").val() != "" && $("#start_lat").val() != "" && $("#end_lon").val() != "" && $("#end_lat").val() != "")
                        transit.search(new BMap.Point($("#start_lon").val(), $("#start_lat").val()), new BMap.Point($("#end_lon").val(), $("#end_lat").val()));
                }
            });

            $("#btn_save").click(function() {
                var result = new Array();
                $("[name = car]:checkbox").each(function() {
                    if ($(this).is(":checked")) {
                        result.push($(this).attr("value"));
                    }
                });

                if ($("#user_tel").val() == "") { Popalert("乘车人不能为空！"); return; }
                if ($("#order_datetime").val() == "") { Popalert("用车时间不能为空！"); return; }
                if ($("#start_address").val() == "") { Popalert("上车点不能为空！"); return; }
                if ($("#end_address").val() == "") { Popalert("下车点不能为空！"); return; }
                if (result.join(",") == "") { Popalert("请选择车型！"); return; }
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_order", order_pk: $("#order_pk").val(), car: result.join(","), user_pk: $("#user_pk").val(), user_name: $("#user_name").val(), user_tel: $("#user_tel").val(), user_sms: ($("#user_sms").is(":checked") ? "1" : "0"), order_type: $("#order_type").val(), order_datetime: $("#order_datetime").val(), start_address: $("#start_address").val(), start_lon: $("#start_lon").val(), start_lat: $("#start_lat").val(), end_address: $("#end_address").val(), end_lon: $("#end_lon").val(), end_lat: $("#end_lat").val(), order_away: $("#order_away").val(), order_time: $("#order_time").val(), order_rem: $("#order_rem").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            parent.Bind();
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
            $("#iframe_Mask").hide();
            $("#divload").hide();

        });
        var map = new BMap.Map("dituContent");
        map.centerAndZoom("西安", 12); 

        var searchComplete = function(results) {
            if (transit.getStatus() != BMAP_STATUS_SUCCESS) {
                return;
            }
            var plan = results.getPlan(0);

            $("#order_time").val((plan.getDuration(true)).replace("分钟", ""));

            $("#order_away").val((plan.getDistance(true)).replace("公里", ""));
        }
        var transit = new BMap.DrivingRoute(map, { renderOptions: { map: map },
            onSearchComplete: searchComplete,
            onPolylinesSet: function() { }
        });
        var ac = new BMap.Autocomplete(    //建立一个自动完成的对象
		{
		    "input": "start_address1"
		    , "location": map
		});
		ac.addEventListener("onhighlight", function(e) {  //鼠标放在下拉列表上的事件
		    var str = "";
		    var _value = e.fromitem.value;
		    var value = "";
		    if (e.fromitem.index > -1) {
		        value = _value.province + _value.city + _value.district + _value.street + _value.business;
		    }
		    str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;

		    value = "";
		    if (e.toitem.index > -1) {
		        _value = e.toitem.value;
		        value = _value.province + _value.city + _value.district + _value.street + _value.business;
		    }
		    str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;
		    $("#SartResultPanel").html(str);
		});
		var myValue;
		ac.addEventListener("onconfirm", function(e) {    //鼠标点击下拉列表后的事件
		    var _value = e.item.value;
		    myValue = _value.province + _value.city + _value.district + _value.street + _value.business;
		    $("#StartResultPanel").html("onconfirm<br />index = " + e.item.index + "<br />myValue = " + myValue);
		    $("#start_lon").val(e.currentTarget.md.src.Te.lng);
		    $("#start_lat").val(e.currentTarget.md.src.Te.lat);

		});



		var ac1 = new BMap.Autocomplete(    //建立一个自动完成的对象
		{
		    "input": "end_address2"
		    , "location": map
        });
        ac1.addEventListener("onhighlight", function(e) {  //鼠标放在下拉列表上的事件
            var str = "";
            var _value = e.fromitem.value;
            var value = "";
            if (e.fromitem.index > -1) {
                value = _value.province + _value.city + _value.district + _value.street + _value.business;
            }
            str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;

            value = "";
            if (e.toitem.index > -1) {
                _value = e.toitem.value;
                value = _value.province + _value.city + _value.district + _value.street + _value.business;
            }
            str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;
            $("#EndResultPanel").html(str);
        });
      
        ac1.addEventListener("onconfirm", function(e) {    //鼠标点击下拉列表后的事件
            var _value = e.item.value;
            myValue = _value.province + _value.city + _value.district + _value.street + _value.business;
            $("#EndResultPanel").html("onconfirm<br />index = " + e.item.index + "<br />myValue = " + myValue);
            $("#end_lon").val(e.currentTarget.md.src.Te.lng);
            $("#end_lat").val(e.currentTarget.md.src.Te.lat);

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
