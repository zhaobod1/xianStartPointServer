<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stat_order_count.aspx.cs" Inherits="stat_stat_order_count" %>

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
<style>
#infobox td
{
padding:5px 0;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content">
        <div class="tools" style="min-width: 740px; height: 40px; background-image: none;
                background-color: #C8EBFF; border-top: solid 1px #8DBCDA;">
                <table cellpadding="0" cellspacing="0" border="0" style="min-width: 740px; ">
                    <tr>
                         <td align="right">
                            统计类型：
                        </td>
                        <td>                          
                            <select id="Text1" style="width: 100px; height: 26px; margin: 5px;" class="input">
                             <option value="0">按天统计</option>
                               <option value="1" selected>按月统计</option>         
                                <option value="2">按季度统计</option>    
                                 <option value="3">按年统计</option>                                                     
                            </select>
                        </td>
                            <td align="right">
                            统计时间：
                        </td>
                        <td>                          
                            <input class="input" style="width: 100px;" id="Text2" type="text" /> 至  <input class="input" style="width: 100px;" id="Text3" type="text" />
                        </td>
                                            
                        <td align="right">
                            状态：
                        </td>
                        <td>
                             <select id="Text4" style="width: 100px; height: 26px; margin: 5px;" class="input">
                             <option value="">全部状态</option>
                                <option value="0">抢单中</option>
                           
                                <option value="2">进行中</option>
                                <option value="3">待评价</option>
                                <option value="4" selected>已完结</option>
                                <option value="5">已取消</option>
                            </select>
                        </td>
                        <td rowspan="2">
                            <div class="btn_sel" style="width: 54px; margin-bottom: 5px;">
                                查 询</div>
                        </td>
                    </tr>                 
                </table>
            </div>
            <div class="tools" style=" display:none;">
                <div class="btn_add" style="float: left;">
                    添 加</div>
                <div class="btn_edit" style="float: left;">
                    修 改</div>
                <div style="float: left;" class="btn_del">
                    删 除</div>
                <div style="float: left;" class="btn_look">
                    查 看</div>
                     <div style="float: left;" class="btn_scrap">
                    锁 定</div>
            </div>
            <div id="infobox" style="width: 100%; position: relative; height: 95%;">
                <input id="sel_id" type="text" style="display: none;" value="" />
                <table id="table_list" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr>
                                                 
                            <th width="60" style="border-top: none;">
                                序号
                            </th>
                            <th width="200" style="border-top: none;" id="th_title">
                                月
                            </th>                            
                            <th width="200" style="border-top: none;">
                                数量
                            </th>                           
                        </tr>
                    </thead>
                    <tbody class="flexigrid">
                    </tbody>
                </table>
                <table class="noborder fenye" style="width: 100%; display:none;">
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
                       
                        <td id="RowIndex" v="" style="text-align: center;">
                        </td>
                        <td id="date_type" v="" style="text-align: center;">
                        </td>
                       
                        <td id="count" v="" style="text-align: center;">
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
            $("#iframe_student").width(pageWidth * 80 / 100).height(pageHeight * 80 / 100);
            $("#Text2,#Text3").attr("readonly", "true").datepicker().addClass("input_date").css("background-color", "#FFFFFF");
            $("div.btn_sel").click(function() {
                Bind();
            });
            $("#iframe_Mask").hide();
            $("#divload").hide();         
        });
        function Bind() {
            $("#iframe_Mask").show();
            $("#divload").show();

            var obj = isFirst ? $("#th_title"):$("#th_title").find("div:eq(0)") ;
            obj.html($("#Text1 option:selected").html().replace("按", "").replace("统计", ""));
            
            sel_data_pk = "";           
            $.ajax({
                url: "getdata.aspx",
                type: "post",
                data: "com=stat_order_count&stat=" + $("#Text1").val() + "&stat_sdate=" + $("#Text2").val() + "&stat_edate=" + $("#Text3").val() + "&order_state=" + $("#Text4").val() + "&c" + new Date().getTime(),
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

            for (var i = 0; i < recordsCount; i++) {
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
                        row.find("input[v='student_pk']").val(info["student_pk"]);
                    }
                    row.appendTo($("#table_list"));
                    $("#table_list tbody tr:last").attr("id", "tr_" + i);
                }
            }
            //disabledPageination();
           // $("#lbl_RecordInfo").html("【共有记录" + recordsCount + "条，当前第" + page + "页/共" + pagesCount + "页】");
            //$("#text_JumpTo").val(page);
            if (recordsCount > 0 && isFirst) $('#table_list').flexigrid({ height: 'auto', width: 'auto', striped: true, sortable: false, onChangeSort: false });
          
            $("#table_list input[name='chk_term']").click(function() {
                $(".hDivBox input[name='chk_all_term']").attr("checked", false);
            });
            $("#table_list tbody tr").click(function() {
                $("#table_list tbody tr").removeClass("trSelected");
                $(this).addClass('trSelected');
                sel_data_pk = $(this).find("td").eq(0).text();
            });
          
            if (recordsCount > 0) isFirst = false;
            $("#sel_id").val("");
            $("#iframe_Mask").hide();
            $("#divload").hide();

        }
        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_student").fadeOut();
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
