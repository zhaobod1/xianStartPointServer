<%@ Page Language="C#" AutoEventWireup="true" CodeFile="car_manage.aspx.cs" Inherits="info_car_manage" %>

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
        <div id="content">
            <div class="tools">
                <div class="btn_add" style="float: left;">
                    添 加</div>
                <div class="btn_edit" style="float: left;">
                    修 改</div>
                <div style="float: left;" class="btn_del">
                    删 除</div>
                <div style="float: left;" class="btn_look">
                    查 看</div>
            </div>
            <div id="infobox" style="width: 100%; position: relative; height: 95%;">
                <input id="sel_id" type="text" style="display: none;" value="" />
                <table id="table_list" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr>
                            <th style="display: none;" style="border-top: none;">
                                编号
                            </th>
                            <th width="20" style="border-left: none; border-top: none;">
                                <input name="chk_all_term" type="checkbox" />
                            </th>
                            <th width="20" style="border-top: none;">
                                序号
                            </th>
                            <th width="200" style="border-top: none;">
                                车型
                            </th>
                            <th width="400" style="border-top: none;">
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
                        <td id="car_pk" v="" style="display: none;">
                        </td>
                        <td id="rowindex" v="" style="text-align: center; border-left: none;">
                            <input v="car_pk" name="chk_term" type="checkbox" />
                        </td>
                        <td id="RowIndex" v="" style="text-align: center;">
                        </td>
                      
                        <td id="car_name" v="" style="text-align: center;">
                        </td>
                        <td id="car_rem" v="">
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
    <div id="pop_car" class="infobox" style="display: none;">
        <div class="tit">
            车型信息</div>
        <div class="close" onclick="closePop();">
        </div>
        <table cellpadding="0" cellspacing="0" style="background: #fff;">
            <tr>
                <td>
                    <iframe id="iframe_car" name="iframe_car" class="ifame" frameborder="0" width="619px"
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
        var pageRows = parseInt(parseInt(pageHeight - 156) / 33); //每一页显示多少行        
        pageRows = pageRows <= 0 ? 1 : pageRows;
        var currentPage = 1; //当前页
        var pagesCount = 1; //共几页
        var recordsCount = 0; //共有记录数
        var data;
        var sel_data_pk = "";
        $(document).ready(function() {
            $("#content").height(pageHeight - 10);
            $("#infobox").width(pageWidth - 2).height($("#content").height() - $(".tools").height() * 2 - 5);
            $("#iframe_car").width(pageWidth * 80 / 100).height(pageHeight * 80 / 100);

            $("div.btn_add").click(function() {
                $("#iframe_car").attr("src", "car_add.aspx");
                $("#pop_car").css({ "left": (pageWidth - $("#pop_car").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
                $("#iframe_Mask").show();
                $("#pop_car").show();
            });
            $("div.btn_edit").click(function() {
                if (sel_data_pk == "") { Popalert("请选择要修改的信息！"); return; }
                $("#iframe_car").attr("src", "car_add.aspx?edit_pk=" + sel_data_pk);
                $("#pop_car").css({ "left": (pageWidth - $("#pop_car").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
                $("#iframe_Mask").show();
                $("#pop_car").show();
            });
            $("div.btn_look").click(function() {
                if (sel_data_pk == "") { Popalert("请选择要查看的信息！"); return; }
                $("#iframe_car").attr("src", "car_look.aspx?edit_pk=" + sel_data_pk);
                $("#pop_car").css({ "left": (pageWidth - $("#pop_car").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
                $("#iframe_Mask").show();
                $("#pop_car").show();
            });
            $("div.btn_del").click(function() {
                var del_pk = "";
                if ($("#table_list input[name='chk_term']:checked").length > 0) {
                    $("#table_list input[name='chk_term']:checked").each(function() {
                        del_pk += "'" + $(this).val() + "',";
                    });
                }
                else {
                    if (sel_data_pk != "") del_pk = "'" + sel_data_pk + "',";
                }
                if (del_pk == "") { Popalert("请选择要删除的信息！"); return; }
                if (confirm("确认要删除？")) {
                    $.ajax({
                        url: "getdata.aspx",
                        type: "post",
                        data: "com=del_car&del_pk=" + del_pk + "&c" + new Date().getTime(),
                        dataType: "json",
                        error: function(err) {
                            $("#iframe_Mask").hide(); $("#divload").hide();
                            Popalert("请求错误！");
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
            });
            Bind();


        });
        function Bind() {
            $("#iframe_Mask").show();
            $("#divload").show();
            sel_data_pk = "";
            $.ajax({
                url: "getdata.aspx",
                type: "post",
                data: "com=search_car&c" + new Date().getTime(),
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
                            row.find("#" + attribute).html(info[attribute] + row.find("#" + attribute).attr("v"));
                        }
                        else {
                            row.find("#" + attribute).find("div").html(info[attribute] + row.find("#" + attribute).attr("v"));
                        }
                        row.find("input[v='car_pk']").val(info["car_pk"]);
                    }
                    row.appendTo($("#table_list"));
                    $("#table_list tbody tr:last").attr("id", "tr_" + i);
                }
            }
            disabledPageination();
            $("#lbl_RecordInfo").html("【共有记录" + recordsCount + "条，当前第" + page + "页/共" + pagesCount + "页】");
            $("#text_JumpTo").val(page);
            if (recordsCount > 0 && isFirst) $('#table_list').flexigrid({ height: 'auto', width: 'auto', striped: true, sortable: false, onChangeSort: false });
            $(".hDivBox input[name='chk_all_term']").click(function() {
                var val = $(this).attr("checked") == "checked" ? "checked" : false;
                $("#table_list input[name='chk_term']").attr("checked", val);

            });
           
            $("#table_list input[name='chk_term']").click(function() {
                $(".hDivBox input[name='chk_all_term']").attr("checked", false);
            });
            $("#table_list tbody tr").click(function() {
                $("#table_list tbody tr").removeClass("trSelected");
                $(this).addClass('trSelected');
                sel_data_pk = $(this).find("td").eq(0).text();
            });
            $("#table_list tbody tr td").click(function() {
                $("#table_list tbody tr").removeClass("trSelected");
                $(this).parent().addClass('trSelected');
                sel_data_pk = $(this).parent().find("td").eq(0).text();
                if ($(this).text() == $(this).parent().find("td:eq(3)").text()) {
                    $("#iframe_car").attr("src", "car_look.aspx?edit_pk=" + sel_data_pk);
                    $("#pop_car").css({ "left": (pageWidth - $("#pop_car").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
                    $("#iframe_Mask").show();
                    $("#pop_car").show();
                }


            }).mouseover(function() {
                if ($(this).text() == $(this).parent().find("td:eq(3)").text() ) {
                    if ($.trim($(this).text()) == "") return;
                    $(this).css({ "color": "red", "cursor": "pointer" });
                }
            }).mouseout(function() {
                if ($(this).text() == $(this).parent().find("td:eq(3)").text()) {
                    $(this).css("color", "#000");
                }
            });
            if (recordsCount > 0) isFirst = false;
            $("#sel_id").val("");
            $("#iframe_Mask").hide();
            $("#divload").hide();

        }
        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_car").fadeOut();
            $("#pop_pact").fadeOut();
            $("#pop_member").fadeOut();
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
