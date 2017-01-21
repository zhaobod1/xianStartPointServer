<%@ Page Language="C#" AutoEventWireup="true" CodeFile="complain_manage.aspx.cs" Inherits="message_complain_manage" %>

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
        <div id="content"><div class="tools">                   
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
                            <th style="display: none;" style=" border-top: none;">
                                编号
                            </th>
                            <th width="48" style="border-left: none;border-top: none;">
                                <input name="chk_all_term" type="checkbox" />
                            </th>
                            <th width="100" style="border-top: none;">
                                被投诉人
                            </th>                           
                            <th width="300" style="border-top: none;">
                                简要
                            </th>
                             <th width="100" style="border-top: none;">
                                投诉人
                            </th>
                            <th width="150" style="border-top: none;">
                                手机号
                            </th>
                             <th width="100" style="border-top: none;">
                                状态
                            </th>
                            <th width="150" style="border-top: none;">
                                投诉时间
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
                        <td id="info_pk" style="display: none;">
                        </td>
                        <td id="rowindex" style="text-align: center; border-left: none;">
                            <input v="info_pk" name="chk_term" type="checkbox" />
                        </td>                     
                        <td id="info_obj"  align="center">
                        </td>
                        <td id="info_title">
                        </td>
                          <td id="user_name" align="center">
                        </td>
                          <td id="user_phone" align="center">
                        </td>
                          <td id="info_state" align="center">
                        </td>
                        <td id="create_time" align="center">
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
   <div id="pop_info" class="infobox" style="display: none;">
		<div class="tit">
			投诉信息</div>
		<div class="close" onclick="closePop();">
		</div>
		<table cellpadding="0" cellspacing="0" style="background: #fff;">
			<tr>
				<td>
					<iframe id="iframe_info" name="iframe_info" class="ifame" frameborder="0" width="619px" height="396"></iframe>
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
            $("#infobox").width(pageWidth - 2).height($("#content").height()-$(".tools").height());
            $("#iframe_info").width(pageWidth * 80 / 100).height(pageHeight * 80 / 100);           
          
            $("div.btn_look").click(function() {
                if (sel_data_pk == "") { Popalert("请选择要查看的信息！"); return; }
                $("#iframe_info").attr("src", "complain_look.aspx?edit_pk=" + sel_data_pk);
                $("#pop_info").css({ "left": (pageWidth - $("#pop_info").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
                $("#iframe_Mask").show();
                $("#pop_info").show();
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
                        data: "com=del_info&info_type=1&del_pk=" + del_pk + "&c" + new Date().getTime(),
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
                data: "com=search_info&info_type=1&c" + new Date().getTime(),
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
                            if (info["user_type"] == "2" && attribute == "student_real_name") {
                                row.find("#user_name").html(info[attribute]);
                            }
                            else if (info["user_type"] == "2" && attribute == "student_phone") {
                                row.find("#user_phone").html(info[attribute]);
                            }
                            else if (info["user_type"] == "1" && attribute == "coach_name") {
                                row.find("#user_name").html(info[attribute]);
                            }
                            else if (info["user_type"] == "1" && attribute == "coach_phone") {
                                row.find("#user_phone").html(info[attribute]);
                            }
                            else if (attribute == "info_state") {
                                row.find("#" + attribute).html(info[attribute]== "0" ? "<font color='red'>待处理</font>" : "<font color='green'>已处理</font>");
                            }
                            else {
                                row.find("#" + attribute).html(info[attribute]);
                            }
                        }
                        else {
                            if (info["user_type"] == "2" && attribute == "student_real_name") {
                                row.find("#user_name").find("div").html(info[attribute]);
                            }
                            else if (info["user_type"] == "2" && attribute == "student_phone") {
                                row.find("#user_phone").find("div").html(info[attribute]);
                            }
                            else if (info["user_type"] == "1" && attribute == "coach_name") {
                                row.find("#user_name").find("div").html(info[attribute]);
                            }
                            else if (info["user_type"] == "1" && attribute == "coach_phone") {
                                row.find("#user_phone").find("div").html(info[attribute]);
                            }
                            else if (attribute == "info_state") {
                                row.find("#" + attribute).find("div").html(info[attribute] == "0" ? "<font color='red'>待处理</font>" : "<font color='green'>已处理</font>");
                            }
                            else {
                                row.find("#" + attribute).find("div").html(info[attribute]);
                            }                           
                        }
                        row.find("input[v='info_pk']").val(info["info_pk"]);
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
                    $("#iframe_info").attr("src", "complain_look.aspx?edit_pk=" + sel_data_pk);
                    $("#pop_info").css({ "left": (pageWidth - $("#pop_info").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
                    $("#iframe_Mask").show();
                    $("#pop_info").show();
                }

            }).mouseover(function() {
                if ($(this).text() == $(this).parent().find("td:eq(3)").text()) {
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
            if ("<%=pk %>" != "") {
                $("#iframe_info").attr("src", "complain_look.aspx?edit_pk=" + "<%=pk %>");
                $("#pop_info").css({ "left": (pageWidth - $("#pop_info").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
                $("#iframe_Mask").show();
                $("#pop_info").show();
            }
        }
        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_info").fadeOut();
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
