<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reg_clause_manage.aspx.cs"
    Inherits="sys_manage_reg_clause_manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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

    <link rel="stylesheet" href="../css/zTreeStyle/zTreeStyle.css" type="text/css">

    <script type="text/javascript" src="../js/zTreeStyle/jquery.ztree.core-3.5.js"></script>

    <script type="text/javascript" src="../js/zTreeStyle/jquery.ztree.excheck-3.5.js"></script>

    <script type="text/javascript" src="../js/zTreeStyle/jquery.ztree.exhide-3.5.js"></script>

    <style>
        th
        {
            border-bottom: medium none;
            cursor: default;
            font-weight: normal;
            height: 34px;
            line-height: 34px;
            overflow: hidden;
            white-space: nowrap;
            font-weight: bold;
            text-align: center;
        }
        td
        {
            font-size: 12px;
            height: 32px;
            line-height: 32px;
            border-left: 1px solid #e8e7e1;
            border-top: 1px solid #e8e7e1;
        }
        td.cl
        {
            text-align: left;
            padding-left: 10px;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content">
            <table id="infobox" width="100%" border="0" cellpadding="0" cellspacing="0" style="border: none;">
                <tr>
                    <td align="left" valign="top" style="border-top: none;">
                        <table id="table_list" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th width="10%" style="border-left: none;">
                                        序号
                                    </th>
                                    <th width="89%" style="text-align: left; padding-left: 10px;">
                                        参数名称
                                    </th>
                                </tr>
                            </thead>
                            <tbody class="flexigrid">
                                <tr>
                                    <td align="center">
                                        1
                                    </td>
                                    <td class="cl" code="00001">
                                        注册条款
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        2
                                    </td>
                                    <td class="cl" code="00004">
                                        注册验证短信模板
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        3
                                    </td>
                                    <td class="cl" code="00005">
                                        重置密码短信模板
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="iframe_Mask" style="background: #333333; width: 100%; height: 100%; left: 0;
        top: 0; position: absolute; z-index: 9999; background-color: Gray; filter: alpha(opacity=80);
        -moz-opacity: 0.5; opacity: 0.5;" name="iframe_Mask">
    </div>
    <div id="pop_code" class="infobox" style="display: none;">
        <div class="tit">
            系统参数</div>
        <div class="close" onclick="closePop();">
        </div>
        <table cellpadding="0" cellspacing="0" style="background: #fff;">
            <tr>
                <td>
                    <iframe id="iframe_code" name="iframe_code" class="ifame" frameborder="0" width="619px"
                        height="396"></iframe>
                </td>
            </tr>
        </table>
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
        var isFirst = true;
        var t = true;
        //分页需要的变量
        var pageRows = parseInt(parseInt(pageHeight - 380) / 24); //每一页显示多少行
        pageRows = pageRows <= 0 ? 1 : pageRows;
        var currentPage = 1; //当前页
        var pagesCount = 1; //共几页
        var recordsCount = 0; //共有记录数
        var data;
        var sel_data_pk = "";
        $(document).ready(function() {
            $("#content").height(pageHeight);
            $("#infobox").width(pageWidth).height($("#content").height());
            $("#iframe_code").width(pageWidth * 80 / 100).height(pageHeight * 80 / 100);
            $("td.cl").mouseover(function() {
                $(this).css("color", "red");
            }).mouseout(function() {
                $(this).css("color", "#000");
            }).click(function() {
                $("#iframe_code").attr("src", "reg_clause_add.aspx?code=" + $(this).attr("code"));
                $("#pop_code").css({ "left": (pageWidth - $("#pop_code").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
                $("#iframe_Mask").show();
                $("#pop_code").show();
            });
            $("#iframe_Mask").hide();
            $("#divload").hide();
        });
       
        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_code").fadeOut();
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
