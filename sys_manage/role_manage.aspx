<%@ Page Language="C#" AutoEventWireup="true" CodeFile="role_manage.aspx.cs" Inherits="sys_manage_role_manage" %>

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

</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content">
            <div class="tools">
                <div style="float: left;" class="btn_add">
                    新 增</div>
                <div style="float: left;" class="btn_del">
                    删 除</div>
            </div>
            <table id="infobox" width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="200" align="left" valign="top">
                        <div class="zTreeDemoBackground left">
                            <ul id="treeDemo" class="ztree">
                            </ul>
                        </div>
                    </td>
                    <td align="left" valign="top">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style=" border-left:none; border-bottom:none;">
                            <tr>
                                <td width="300" style="background-color: #F4F2F3; text-align: right;border-left:none;">
                                    角色名称：
                                </td>
                                <td>
                                    <input class="input" style="display: none;" id="Text0" type="text" /><input class="input"
                                        style="display: none;" id="Text00" value="00000" type="text" />
                                    <input class="input" style="width: 150px;" id="Text1" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #F4F2F3; text-align: right;border-left:none;">
                                    角色描述：
                                </td>
                                <td>
                                    <input class="input" style="width: 150px;" id="Text2" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #F4F2F3; text-align: right;border-left:none;">
                                    排序号：
                                </td>
                                <td>
                                    <input class="input" style="width: 150px;" id="Text3" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #F4F2F3; text-align: right;border-left:none;">
                                    角色类型：
                                </td>
                                <td>
                                    <select id="Text4" style="height: 26px; margin: 5px;" class="input">
                                        <option value="0">角色</option>
                                        <option value="1">分组</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-left: none; height: 70px;">
                                    <div style="width: 256px; margin: 0 auto;">
                                        <div id="btn_save" class="submit" style="float: left;">
                                            保 存</div>
                                        <div id="btn_rest" class="rest" style="margin-left: 40px; float: left;">
                                            返 回</div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div style="clear: both;">
                        </div>
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
        var setting = {
            view: {
                showIcon: false
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
                onClick: onClick
            }
        };
        var pageHeight = getClientHeight();
        var pageWidth = getClientWidth();
        var isFirst = true;
        var t = true;
        var recordsCount = 0; //共有记录数
        var data;
        var sel_data_pk = "";
        $(document).ready(function() {
            $("#content").height(pageHeight);
            $("#infobox").width(pageWidth).height($("#content").height() - $(".tools").height()-10);
            Bind();
            $("#btn_save").click(function() {
                if ($("#Text1").val() == "") { Popalert("请输入组织名称！"); return; };
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_role", role_pk: $("#Text0").val(), parent_role_code: $("#Text00").val(), role_name: $("#Text1").val(), role_rem: $("#Text2").val(), role_ord: $("#Text3").val(), role_type: $("#Text4").val(), c: new Date().getTime()
                    },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            Bind();
                            $("#btn_rest").click();
                        }
                        else {
                            Popalert("操作失败，请重试！");
                        }
                        $("#iframe_Mask").hide();
                        $("#divload").hide();
                    }
                });
            });
            $("#btn_rest").click(function() {
                $("#Text0").val("");
                $("#Text1").val("");
                $("#Text2").val("");
                $("#Text3").val("");
                $("#Text4").val("角色");
            });
            $("div.btn_add").click(function() {
                $("#btn_rest").click();
            });
            $("div.btn_del").click(function() {
                var del_pk = sel_data_pk;
                if (del_pk == "") { Popalert("请选择要删除的节点！"); return; }
                if (confirm("确认要删除？")) {
                    $.ajax({
                        url: "getdata.aspx",
                        type: "post",
                        data: "com=del_role&del_pk=" + del_pk + "&c" + new Date().getTime(),
                        dataType: "json",
                        error: function(err) {
                            $("#iframe_Mask").hide(); $("#divload").hide();
                            Popalert("请求错误！");
                        },
                        success: function(da) {
                            if (da != null) {
                                if (da.state == "-100") {
                                    Popalert("内置数据不能删除！");
                                }
                                else if (da.state == "-99") {
                                    Popalert("该项下有子集，不能删除！");
                                }
                                else {
                                    data = da;
                                    jumpToPage();
                                }
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
        });
        function Bind() {
            $("#iframe_Mask").show();
            $("#divload").show();
            sel_data_pk = "";
            $.ajax({
                url: "getdata.aspx",
                type: "post",
                data: "com=search_role&c" + new Date().getTime(),
                dataType: "json",
                error: function(err) {
                    $("#iframe_Mask").hide(); $("#divload").hide();
                    Popalert("请求的信息不存在！");
                },
                success: function(da) {
                    if (da != null) {
                        data = da;
                        jumpToPage();
                    }
                    else {
                        Popalert("操作失败，请重试！");
                    }
                    $("#iframe_Mask").hide();
                    $("#divload").hide();
                }
            });
        }
        function jumpToPage() {
            $("#iframe_Mask").show();
            $("#divload").show();
            $.fn.zTree.init($("#treeDemo"), setting, data);
            $("#hideNodesBtn").bind("click", { type: "rename" }, hideNodes);
            $("#showNodesBtn").bind("click", { type: "icon" }, showNodes);
            setTitle();
            count();
            isFirst = false;
            $("#sel_id").val("");
            $("#iframe_Mask").hide();
            $("#divload").hide();

        }
        function setTitle(node) {
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
            var nodes = node ? [node] : zTree.transformToArray(zTree.getNodes());
            for (var i = 0, l = nodes.length; i < l; i++) {
                var n = nodes[i];
                n.title = "[" + n.id + "] isFirstNode = " + n.isFirstNode + ", isLastNode = " + n.isLastNode;
                zTree.updateNode(n);
            }
        }
        function count() {
            function isForceHidden(node) {
                if (!node.parentTId) return false;
                var p = node.getParentNode();
                return !!p.isHidden ? true : isForceHidden(p);
            }
            var zTree = $.fn.zTree.getZTreeObj("treeDemo"),
			checkCount = zTree.getCheckedNodes(true).length,
			nocheckCount = zTree.getCheckedNodes(false).length,
			hiddenNodes = zTree.getNodesByParam("isHidden", true),
			hiddenCount = hiddenNodes.length;

            for (var i = 0, j = hiddenNodes.length; i < j; i++) {
                var n = hiddenNodes[i];
                if (isForceHidden(n)) {
                    hiddenCount -= 1;
                } else if (n.isParent) {
                    hiddenCount += zTree.transformToArray(n.children).length;
                }
            }

            $("#isHiddenCount").text(hiddenNodes.length);
            $("#hiddenCount").text(hiddenCount);
            $("#checkCount").text(checkCount);
            $("#nocheckCount").text(nocheckCount);
        }
        function onClick(event, treeId, treeNode, clickFlag) {
            sel_data_pk = treeNode.flag;

            $("#iframe_Mask").show();
            $("#divload").show();
            $.ajax({
                url: "getdata.aspx",
                type: "post",
                data: "com=get_role_info&role_pk=" + sel_data_pk + "&c" + new Date().getTime(),
                dataType: "json",
                error: function(err) {
                    $("#iframe_Mask").hide(); $("#divload").hide();
                    $("#iframe_Mask").hide();
                    $("#divload").hide();
                    Popalert("请求错误！");
                },
                success: function(da) {
                    if (da != null) {
                        data = da.Table[0];
                        $("#Text0").val(data.role_pk);
                        $("#Text00").val(data.role_code);
                        $("#Text1").val(data.role_name);
                        $("#Text2").val(data.role_rem);
                        $("#Text3").val(data.role_ord);
                        $("#Text4").val(data.role_type);
                    }
                    else {
                        Popalert("操作失败，请重试！");
                    }
                    $("#iframe_Mask").hide();
                    $("#divload").hide();
                }
            });
        }
        function hideNodes() {
            var zTree = $.fn.zTree.getZTreeObj("treeDemo"),
			nodes = zTree.getSelectedNodes();
            if (nodes.length == 0) {
                alert("请至少选择一个节点");
                return;
            }
            zTree.hideNodes(nodes);
            setTitle();
            count();
        }
        function showNodes() {
            var zTree = $.fn.zTree.getZTreeObj("treeDemo"),
			nodes = zTree.getNodesByParam("isHidden", true);
            zTree.showNodes(nodes);
            setTitle();
            count();
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
