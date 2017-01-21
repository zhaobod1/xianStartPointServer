<%@ Page Language="C#" AutoEventWireup="true" CodeFile="database_res.aspx.cs" Inherits="sys_manage_database_res" %>

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

    <style>
        table, td
        {
            border: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content">
            
            
            <div class="tools" style="min-width: 740px; ">
                数据库还原
            </div>
            <table width="600" style="margin: 0 auto;">
                <tr>
                    <td rowspan="2" width="270">
                        <img src="../images/u=1427804029,2573170478&fm=23&gp=0.jpg" />
                    </td>
                    <td>
                        <div style="width: 258px; margin: 30px 0;">
                            <div>
                                请选择还原文件：
                            </div>
                            <div>
                                <select id="Text1" style="width: 150px; height: 26px; margin: 5px 5px 5px 0px;" class="input">
                                    <%                           
                                        string[] filenames = System.IO.Directory.GetFiles(Server.MapPath("../data_bak"));
                                        foreach (string fname in filenames)
                                        {
                                    %>
                                    <option>
                                        <%=fname.Substring(fname.LastIndexOf("\\")+1) %></option>
                                    <%
                                        } %>
                                </select>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div id="btn_res" class="submit" style="float: left;">
                                还 原</div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 20px;">
                        还原说明：<br />
                        a.要求所有用户都退出本系统,否则可能导致还原失败!<br />
                        b.可能会造成一些数据的丢失,请谨慎执行 .
                    </td>
                </tr>
            </table>
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

            $("#btn_res").click(function() {
                if (!confirm("确定要执行数据库还原操作吗？\r\n\r\n本操作可能会造成部分数据丢失.")) return;
                if (!confirm("请再次确认？")) return;
                $("#iframe_Mask").show();
                $("#divload").show();
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "database_res", file_name: $("#Text1").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            if (da.result == "1")
                                Popalert("还原成功！");
                            else if (da.result == "-99")
                                Popalert("文件不存在！")
                            else
                                Popalert("还原失败！");
                        }
                        else {
                            Popalert("还原失败，请重试！");
                        }
                        $("#iframe_Mask").hide();
                        $("#divload").hide();
                    }
                });
            });
            $("#iframe_Mask").hide();
            $("#divload").hide();
        });
       
        function OnFaild() {
            Popalert("网络或服务器出现异常，请稍后再试!"); //调用 Popalert函数显示页面提示筐
        }
        function Popalert(str) {
            PopupDiv(document.getElementById('container'), str, 2000, "yellow", 240, 25);
        }
    </script>

</body>
</html>
