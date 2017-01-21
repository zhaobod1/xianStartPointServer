<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ad_manage.aspx.cs" Inherits="info_ad_manage" %>

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

    <link rel="stylesheet" href="../editor/themes/default/default.css" />
    <link rel="stylesheet" href="../editor/plugins/code/prettify.css" />

    <script charset="utf-8" src="../editor/kindeditor.js"></script>

    <script charset="utf-8" src="../editor/lang/zh_CN.js"></script>

    <script charset="utf-8" src="../editor/plugins/code/prettify.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content">
            <div class="tools" style=" background:#C8EBFF; height:50px;">
                <input runat="server" class="input" readonly="readonly" id="car_img" type="text" />
                <input id="car_img_button" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                    background-color: #9DBEFF; font-size: 12px;" />
                <asp:Button ID="Button1"  OnClick="Button1_Click" CssClass="submit" runat="server" Text="保存" />
            </div>
            <div id="infobox" style="width: 100%; position: relative; height: 95%;">
                <input id="sel_id" type="text" style="display: none;" value="" />
                <table id="table_list" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr>
                            <th style="display: none;" style="border-top: none;">
                                编号
                            </th>
                            <th width="30" style="border-top: none;">
                                序号
                            </th>
                            <th width="200" style="border-top: none;">
                                图片
                            </th>
                             <th width="80" style="border-top: none;">
                                删除
                            </th>
                        </tr>
                    </thead>
                    <tbody class="flexigrid">
                        <%for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                          {
                        %>
                        <tr>
                            <td style="display: none;">
                                <%=ds.Tables[0].Rows[i]["info_pk"].ToS() %>
                            </td>
                            <td style=" text-align:center;">
                                <%=i+1 %>
                            </td>
                            <td  style=" text-align:center;">
                              <a href="<%=ds.Tables[0].Rows[i]["info_content"].ToS() %>" target="_blank"><img style="height: 50px; border:none;" src="<%=ds.Tables[0].Rows[i]["info_content"].ToS() %>" /></a>
                            </td>
                            <td>
                             <asp:Button ID="Button2" OnClientClick="return check(this);"  OnClick="Button2_Click" CssClass="submit" runat="server" Text="删除" />
                            </td>
                        </tr>
                        <%
                            } %>
                    </tbody>
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
            $("#iframe_driving,#iframe_pact,#iframe_member").width(pageWidth * 80 / 100).height(pageHeight * 80 / 100);

            KindEditor.ready(function(K) {
                var editor = K.editor({
                    allowFileManager: false
                });
                K('#car_img_button').click(function() {
                    editor.loadPlugin('image', function() {
                        editor.plugin.imageDialog({
                            showRemote: false,
                            clickFn: function(url, title, width, height, border, align) {
                                K('#car_img').val(url);
                                editor.hideDialog();
                                $("#car_img_span").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                                imagePreview();
                            }
                        });
                    });
                });

            });
           
            $("#iframe_Mask").hide();
            $("#divload").hide();

        });
        function check(obj) {
            $("#car_img").val($(obj).parent().parent().find("td:eq(0)").html());
           return  confirm("确定要删除广告图片吗？");               
        }
        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_driving").fadeOut();
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
