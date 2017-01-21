<%@ Page Language="C#" AutoEventWireup="true" CodeFile="database_bak.aspx.cs" Inherits="sys_manage_database_bak" %>

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
            <div class="tools" style="min-width: 740px;">
                数据库备份
            </div>
            <table width="600" style="margin: 20px auto;">
                <tr>
                    <td rowspan="2" width="270">
                        <img src="../images/u=1297639523,3205181150&fm=90&gp=0.jpg" />
                    </td>
                    <td>
                        <div id="btn_bak" class="submit" style="float: left;">
                            备 份</div>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 20px;">
                        备份说明:<br />
                        1、本操作将数据库文件按日期序号的形式备份在服务器上。<br />
                        2、建议每周至少执行一次数据库备份。
                        <br />
                        3、用于备份的目录：(软件目录\data_bak\ )
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
            $("#btn_bak").click(function() {
                $("#iframe_Mask").show();
                $("#divload").show();
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "database_bak", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            if (da.result == "1")
                                Popalert("备份成功！");
                            else
                                Popalert("备份失败！");
                        }
                        else {
                            Popalert("备份失败，请重试！");
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
