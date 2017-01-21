<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>

    <link href="css/style.css" type="text/css" rel="stylesheet">
    <style>
    #xms table,#xms table td
    {
    	 border:none;
    	}
    </style>
</head>
<body>
    <table width="100%" height="86" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="86" background="images/index_01.jpg" style="background-repeat: no-repeat;">
                <table border="0" align="right" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="80">
                            <a href="right.aspx" target="right" class="TopText">
                                <img src="images/Icon01.png" border="0" /><br />
                                管理首页</a>
                        </td>
                        <td width="80">
                            <a href="sys_manage/admin_info.aspx" target="right" class="TopText">
                                <img src="images/Icon02.png" border="0" /><br />
                                修改密码</a>
                        </td>
                       
                        <td width="80">
                            <a href="javascript:void(0);" onclick="javascript:if(confirm('确定要退出吗?')) top.location.href='clear_out.aspx?c=%25d2&';"
                                class="TopText">
                                <img src="images/Icon04.png" border="0" /><br />
                                退出系统</a>
                        </td>
                        <td width="20">
                            <a href="#" class="TopText"></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" height="35" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height="35" background="images/index_16.jpg">
                <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%">
                            <img src="images/icon05.jpg" width="16" height="16" align="absmiddle" />&nbsp;&nbsp;&nbsp;
                            用户：<%=Session["UserName"].ToS() %> <span id="localtime"></span>

                            <script type="text/javascript">
                                function showLocale(objD) {
                                    var str, colorhead, colorfoot;
                                    var yy = objD.getYear();
                                    if (yy < 1900) yy = yy + 1900;
                                    var MM = objD.getMonth() + 1;
                                    if (MM < 10) MM = '0' + MM;
                                    var dd = objD.getDate();
                                    if (dd < 10) dd = '0' + dd;
                                    var hh = objD.getHours();
                                    if (hh < 10) hh = '0' + hh;
                                    var mm = objD.getMinutes();
                                    if (mm < 10) mm = '0' + mm;
                                    var ss = objD.getSeconds();
                                    if (ss < 10) ss = '0' + ss;
                                    var ww = objD.getDay();
                                    if (ww == 0) colorhead = "<font color=\"#FF0000\">";
                                    if (ww > 0 && ww < 6) colorhead = "<font color=\"#373737\">";
                                    if (ww == 6) colorhead = "<font color=\"#008000\">";
                                    if (ww == 0) ww = "星期日";
                                    if (ww == 1) ww = "星期一";
                                    if (ww == 2) ww = "星期二";
                                    if (ww == 3) ww = "星期三";
                                    if (ww == 4) ww = "星期四";
                                    if (ww == 5) ww = "星期五";
                                    if (ww == 6) ww = "星期六";
                                    colorfoot = "</font>"
                                    str = colorhead + yy + "-" + MM + "-" + dd + " " + hh + ":" + mm + ":" + ss + "  " + ww + colorfoot;
                                    return (str);
                                }
                                function tick() {
                                    var today;
                                    today = new Date();
                                    document.getElementById("localtime").innerHTML = showLocale(today);
                                    window.setTimeout("tick()", 1000);
                                }
                                tick();
                            </script>

                        </td>
                        <td id="xms" width="50%" align="right" style="line-height: 19px; height: 19px;">
                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var sI1 = null;
        var sI2 = null;
        var sI3 = null;
        $(document).ready(function() {
            update();
            $("#xms a").click(function() {
                if (sI2) sI2 = clearInterval(sI2);
                $("#sc").show();
                parent.document.title = "<%=Session["Sys_Title"].ToS() %>";
                if (sI3 == null || sI3 == undefined) sI3 = setInterval("update()", 1000*60*10);
                if (sI1 == null || sI1 == undefined) sI1 = setInterval("sucase()", 5000);
            });

        });
        function sucase() {
            $.ajax({
                url: "sys_manage/getdata.aspx",
                type: "post",
                data: "com=search_update&c" + new Date().getTime(),
                dataType: "json",
                error: function(err) {
                },
                success: function(da) {
                    if (da != null) {
                        if (da.result == "1") {
                             $("#s1").html(da.result1);
                            $("#s2").html(da.result2);
                            $("#s3").html(da.result3);
                            $("#s4").html(da.result4);
                            if (sI1) sI1= clearInterval(sI1);
                            if (sI2 == null || sI2 == undefined) sI2 = setInterval("doc()", 300);
                        }
                    }
                }
            });
        }
        function doc() {
            $("#sc").toggle();
            if (parent.document.title.indexOf("【您有新消息】") > -1)
               { parent.document.title = "【　　　　　】"+"<%=Session["Sys_Title"].ToS() %>";}
            else
                {parent.document.title = "【您有新消息】" +  "<%=Session["Sys_Title"].ToS() %>";}
        }
        function update() {
            $.ajax({
                url: "sys_manage/getdata.aspx",
                type: "post",
                data: "com=get_update&c" + new Date().getTime(),
                dataType: "json",
                error: function(err) {
                },
                success: function(da) {
                    if (da != null) {
                        $("#s1").html(da.result1);
                        $("#s2").html(da.result2);
                        $("#s3").html(da.result3);
                        $("#s4").html(da.result4);
                        if (parseInt(da.result1) + parseInt(da.result2) + parseInt(da.result3) + parseInt(da.result4)) {
                            if (sI2 == null || sI2 == undefined) sI2 = setInterval("doc()", 300);
                            if (sI3) sI3=clearInterval(sI3);
                            if (sI1) sI1=clearInterval(sI1);
                        }
                        else {
                            if (sI3 == null || sI3 == undefined) sI3 = setInterval("update()", 1000 * 60 * 10);
                            if (sI1 == null || sI1 == undefined) sI1 = setInterval("sucase()", 5000);
                        }
                    }
                }
            });
        }
    </script>

</body>
</html>
