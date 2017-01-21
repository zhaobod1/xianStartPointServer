<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sel_edit_member.aspx.cs" Inherits="sel_edit_member" %>

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

    <script src="../js/MoveDiv.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content" style="overflow: hidden;">
            <div class="tools">
                <span style="float: left; margin-right: 10px;">客户名称：
                    <input class="input" style="width: 90px; margin: 0;" id="Text1" type="text" />
                    联系人：
                    <input class="input" style="width: 90px; margin: 0;" id="Text2" type="text" />
                    
                </span>
                <div class="btn_sel" style="float: left;">
                    查 询</div>
                <div class="btn_ok" style="float: left;">
                    选 中</div>
            </div>
            <div id="infobox" style="width: 100%; position: relative; height: 95%;">
                <input id="sel_id" type="text" style="display: none;" value="" />
                <table id="table_list" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr>
                            <th style="display: none;" style="border-top: none;">
                                编号
                            </th>
                            <th width="48" style="border-left: none; border-top: none;">
                                <input name="chk_all_term" type="checkbox" />
                            </th>
                            <th width="100" style="border-top: none;">
                                客户名称
                            </th>
                            <th width="60" style="border-top: none;">
                                联系人
                            </th>
                            <th width="60" style="border-top: none;">
                                手机
                            </th>
                            <th width="100" style="border-top: none;">
                                电话
                            </th>
                            <th width="150" style="border-top: none;">
                                地址
                            </th>
                            <th width="60" style="border-top: none;">
                                邮编
                            </th>
                            <th width="80" style="border-top: none;">
                                Email
                            </th>
                            <th width="80" style="border-top: none;">
                                客户等级
                            </th>
                            <th width="80" style="border-top: none;">
                                备注
                            </th>
                        </tr>
                    </thead>
                    <tbody class="flexigrid">
                    </tbody>
                </table>
                <table class="noborder fenye" style="width: 100%;">
                    <tr>
                        <td align="center">
                            <!--**********表格下页面跳转的按钮布局*****************-->
                            <a id="a_firstPage" href="javascript:" onclick="if(this.disabled==false)jumpToPage(1);"
                                class="AppPaginationBtn" onmouseover="this.style.color='red';" onmouseout="this.style.color='#7c7c7c';">
                                首页</a> <a id="a_prevPage" href="javascript:" onclick=" if(this.disabled==false) jumpToPage(currentPage-1);"
                                    class="AppPaginationBtn" onmouseover="this.style.color='red';" onmouseout="this.style.color='#7c7c7c';">
                                    上一页</a>
                            <input type="text" id="text_JumpTo" class="text " style="width: 20px; height: 12px;
                                vertical-align: middle; margin-top: 0px;" onpaste="return false;" onkeypress="LimitNumber()" />
                            <a id="a_goPage" href="javascript:" onclick="if(this.disabled==false) jumpToPage(trim($('#text_JumpTo').val()));"
                                class="AppPaginationBtn" onmouseover="this.style.color='red';" onmouseout="this.style.color='#7c7c7c';">
                                跳转</a> <a id="a_nextPage" href="javascript:" onclick="if(this.disabled==false)jumpToPage(currentPage+1);"
                                    class="AppPaginationBtn" onmouseover="this.style.color='red';" onmouseout="this.style.color='#7c7c7c';">
                                    下一页</a> <a id="a_lastPage" href="javascript:" onclick="if(this.disabled==false)jumpToPage(pagesCount);"
                                        class="AppPaginationBtn" onmouseover="this.style.color='red';" onmouseout="this.style.color='#7c7c7c';">
                                        末页</a> <a id="lbl_RecordInfo">【共有记录0条，当前第0页/共0页】</a>
                        </td>
                    </tr>
                </table>
                <table style="display: none;">
                    <tr id="template">
                        <td id="member_pk" style="display: none;">
                        </td>
                        <td id="rowindex" style="text-align: center; border-left: none;">
                            <input v="member_pk" name="chk_term" type="checkbox" />
                        </td>
                        <td id="member_name">
                        </td>
                        <td id="member_linkman" align="center">
                        </td>
                        <td id="member_fax" align="center">
                        </td>
                        <td id="member_tel" align="center">
                        </td>
                        <td id="member_addr">
                        </td>
                        <td id="member_post" align="center">
                        </td>
                        <td id="member_email">
                        </td>
                        <td id="member_level" align="center">
                        </td>
                        <td id="member_rem">
                        </td>
                    </tr>
                </table>
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

        var pageHeight = parent.pageHeight * 80 / 100;
        var pageWidth = parent.pageWidth * 80 / 100;
        var isFirst = true;
        var t = true;
        //分页需要的变量
        var pageRows = parseInt(parseInt(parent.pageHeight * 80 / 100 - 156) / 33); //每一页显示多少行

        pageRows = pageRows <= 0 ? 1 : pageRows;
        var currentPage = 1; //当前页
        var pagesCount = 1; //共几页
        var recordsCount = 0; //共有记录数
        var data;
        var sel_data_pk = "";
        var sel_data_name = "";
        var sel_pk = "";
        $(document).ready(function() {
            $("#content").height(pageHeight - 10);
            $("#infobox").width(pageWidth - 2).height($("#content").height() - $(".tools").height());
            $("div.btn_ok").click(function() {
                
//                if ($("#table_list input[name='chk_term']:checked").length > 0) {
//                    $("#table_list input[name='chk_term']:checked").each(function() {
//                        sel_pk += "'" + $(this).val() + "',";
//                    });
//                }
//                else {
//                    if (sel_data_pk != "") sel_pk = "'" + sel_data_pk + "',";
//                }
                if (sel_pk == "") { Popalert("请至少选择一条收入信息！"); return; }
                parent.set_member(sel_pk, data);
                parent.closePop();
            });
            $("div.btn_sel").click(function() {
                Bind();
            });
            Bind();
        });
        function set_member(pks) {
            for (var i = 0; i < $("#table_list tbody tr").length; i++) {
                if (pks.indexOf($("#table_list tbody tr:eq(" + i + ")").find("td:eq(0)").text()) > -1) {
                    $("#table_list tbody tr:eq(" + i + ")").find("td:eq(1)").find("input[name='chk_term']").attr("checked", true);
                    $("#table_list tbody tr:eq(" + i + ")").addClass('trSelected');
                }
            }
        }
        function Bind() {
            $("#iframe_Mask").show();
            $("#divload").show();
            sel_data_pk = "";
            $.ajax({
                url: "/sys_manage/getdata.aspx",
                type: "post",
                data: "com=search_member&member_name=" + $("#Text1").val() + "&member_linkman=" + $("#Text2").val() + "&c" + new Date().getTime(),
                dataType: "json",
                error: function(err) {
                    $("#iframe_Mask").hide(); $("#divload").hide();
                    Popalert("请求的信息不存在！");
                },
                success: function(da) {

                    if (da != null) {
                        data = da;
                        jumpToPage(currentPage);
                    }
                    else {
                        Popalert("操作失败，请重试！");
                    }
                    $("#iframe_Mask").hide();
                    $("#divload").hide();
                }
            });
        }
        function jumpToPage(page) {
            $("#iframe_Mask").show();
            $("#divload").show();
            currentPage = page;
            if (t) {
                if ($("#table_list tbody tr").length > 0) {
                    $("#template").html($("#table_list tbody tr:first").html());
                    t = false;
                }
            }
            $("#table_list tbody tr").remove();
            recordsCount = data.Table.length;
            pagesCount = pagesCount = Math.ceil(recordsCount / pageRows);
            var startRows = pageRows * (page - 1)
            var endRows = pageRows * page;

            for (var i = startRows; i < endRows; i++) {
                if (i >= recordsCount)
                    break;
                else {
                    var row = $("#template").clone();
                    var info = data.Table[i];
                    for (var attribute in info) {
                        if (isFirst) {
                            row.find("#" + attribute).html(info[attribute]);
                        }
                        else {
                            row.find("#" + attribute).find("div").html(info[attribute]);
                        }
                        row.find("input[v='member_pk']").val(info["member_pk"]);
                        if (sel_pk != "" && sel_pk.indexOf("'" + info["member_pk"] + "',") > -1) {
                            row.find("input[v='member_pk']").attr("checked", "checked");
                        }
                        else {
                            row.find("input[v='member_pk']").attr("checked", false);
                        }
                    }
                    row.appendTo($("#table_list"));
                    $("#table_list tbody tr:last").attr("id", "tr_" + i);
                }
            }
            disabledPageination();
            $("#lbl_RecordInfo").html("【共有记录" + recordsCount + "条，当前第" + page + "页/共" + pagesCount + "页】");
            $("#text_JumpTo").val(page);
            if (recordsCount > 0 && isFirst) $('#table_list').flexigrid({ height: 'auto', width: 'auto', striped: true });
            $(".hDivBox input[name='chk_all_term']").click(function() {
                var val = $(this).attr("checked") == "checked" ? "checked" : false;
                $("#table_list input[name='chk_term']").attr("checked", val);

            });
            $("#table_list input[name='chk_term']").click(function() {
                $(".hDivBox input[name='chk_all_term']").attr("checked", false);
                if ($(this).attr("checked") == "checked" && sel_pk.indexOf("'" + $(this).val() + "',") < 0) {
                    sel_pk += "'" + $(this).val() + "',";
                }
                else {
                    sel_pk = sel_pk.replace("'" + $(this).val() + "',", "");
                }
            });
            $("#table_list tbody tr").click(function() {
                $("#table_list tbody tr").removeClass("trSelected");
                $(this).addClass('trSelected');
                sel_data_pk = $(this).find("td").eq(0).text();
                sel_data_name = $(this).find("td").eq(2).text();
            });
            if (recordsCount > 0) isFirst = false;
            $("#sel_id").val("");
            $("#iframe_Mask").hide();
            $("#divload").hide();

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
