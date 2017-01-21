<%@ Page Language="C#" AutoEventWireup="true" CodeFile="role_give.aspx.cs" Inherits="sys_manage_role_give" %>

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
                <h3>
                    角色列表</h3>
            </div>
            <table id="infobox" width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="200" align="left" valign="top">
                        <div class="zTreeDemoBackground left" style="float: left; border-right: none;">
                            <ul id="treeDemo" class="ztree">
                            </ul>
                        </div>
                    </td>
                    <td align="left" valign="top">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-bottom: none;">
                            <tr>
                                <td width="200" style="border-left: none; border-top: none; background-color: #F4F2F3;
                                    text-align: right;">
                                    选择权限类型：
                                </td>
                                <td style="padding: 5px; border-top: none; border-right: none;">
                                    <input id="Radio1" value="A" checked="checked" name="ext_type" checked="checked"
                                        type="radio" />操作按钮权限&nbsp;&nbsp;
                                    <input id="Radio2" value="B" name="ext_type" type="radio" />查看数据权限范围
                                </td>
                            </tr>
                            <tr id="ext_w">
                                <td style="margin: 5px; border-left: none; background-color: #F4F2F3; text-align: right;">
                                    操作按钮类型：
                                </td>
                                <td style="padding: 5px; border-right: none;">
                                    <input id="Radio3" value="0" name="ext_w" checked="checked" type="radio" />全部&nbsp;&nbsp;
                                    <input id="Radio4" value="1" name="ext_w" type="radio" />新增&nbsp;&nbsp;
                                    <input id="Radio5" value="2" name="ext_w" type="radio" />修改&nbsp;&nbsp;
                                    <input id="Radio6" value="3" name="ext_w" type="radio" />删除
                                </td>
                            </tr>
                            <tr id="ext_s">
                                <td style="margin: 5px; border-left: none; background-color: #F4F2F3; text-align: right;">
                                    查看部门：
                                </td>
                                <td style="padding: 5px; border-right: none;">
                                    <input id="Radio7" value="0" name="ext_s" checked="checked" type="radio" />本人&nbsp;&nbsp;
                                    <input id="Radio8" value="1" name="ext_s" type="radio" />本部门&nbsp;&nbsp;
                                    <input id="Radio9" value="2" name="ext_s" type="radio" />本学校&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="background-color: #F4F2F3; border-left: none; text-align: right;">
                                    <div class="zTreeDemoBackground left" style="float: left; border: solid 1px #E8E7E1;
                                        border-right: none;">
                                        <ul id="treeDemo1" class="ztree">
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-left: none; border-bottom: none; height: 70px;">
                                    <div style="width: 256px; margin: 0 auto;">
                                        <div id="btn_save" class="submit" style="float: left;">
                                            保 存</div>
                                        <div id="btn_rest" class="rest" style="margin-left: 40px; float: left;">
                                            取 消</div>
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
        var setting_menu = {
            check: {
                enable: true
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
                onCheck: onCheck
            }
        };

        var pageHeight = getClientHeight();
        var pageWidth = getClientWidth();
        var isFirst = true;
        var t = true;
        var recordsCount = 0; //共有记录数
        var data;
        var sel_data_pk = "";
        var ext = "A0"
        var exts = ""
        $(document).ready(function() {
            $("#ext_w").css("display", "");
            $("#ext_s").css("display", "none");
            $("#content").height(pageHeight);
            $("#infobox").width(pageWidth).height($("#content").height() - $(".tools").height() - 10);
            Bind();
            $("input[name='ext_type']").change(function() {
                ext = $("input[name='ext_type']:checked").val();
                if (ext == "A") {
                    ext = $("input[name='ext_type']:checked").val() + $("input[name='ext_w']:checked").val();
                    $("#ext_w").css("display", "");
                    $("#ext_s").css("display", "none");
                }
                else if (ext == "B") {
                    ext = $("input[name='ext_type']:checked").val() + $("input[name='ext_s']:checked").val();
                    //$("#ext_s").css("display", "");
                    $("#ext_w").css("display", "none");
                }
                set_ext();
            });

            $("input[name='ext_w']").change(function() {
                ext = $("input[name='ext_type']:checked").val() + $("input[name='ext_w']:checked").val();
                set_ext();
            });
            $("input[name='ext_s']").change(function() {
                ext = $("input[name='ext_type']:checked").val() + $("input[name='ext_s']:checked").val();
                set_ext();
            });
            $("#btn_save").click(function() {

                if (sel_data_pk == "") { Popalert("请选择角色！"); return; };
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_role_rights", role_pk: sel_data_pk, rig_val: exts, rig_org: "", c: new Date().getTime()
                    },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            Popalert("保存成功！");
                            
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
                $("input[name='ext_type']:eq(0)").attr("checked", 'checked');
                $("input[name='ext_w']:eq(0)").attr("checked", 'checked');
                $("#ext_w").css("display", "");
                $("#ext_s").css("display", "none");
                exts = "";
                $(".checkbox_true_full").removeClass("checkbox_true_full");
                var ztree = $.fn.zTree.getZTreeObj("treeDemo1");
                var treeNode = ztree.getNodes();
                $.fn.zTree.getZTreeObj("treeDemo1").expandAll(false);
                ext = "A0";
            });
        });
        function set_ext() {
            if (exts != "") {
                var ztree = $.fn.zTree.getZTreeObj("treeDemo1");
                var treeNode = ztree.getNodes();
                for (var i = 0; i < treeNode.length; i++) {
                    for_set(treeNode[i]);
                }
            }
        }
        function for_set(TreeNode) {
            var ztree = $.fn.zTree.getZTreeObj("treeDemo1");
            if (!TreeNode.children) {
                if (get_cond(TreeNode.code)) {
                    ztree.checkNode(TreeNode, true, true, false);
                }
                else
                    ztree.checkNode(TreeNode, false, true, false);
            }
            else {
                for (var j = 0; j < TreeNode.children.length; j++) {
                    var treeNode1 = TreeNode.children[j];
                    for_set(treeNode1);
                }
            }

        }
        function get_cond(v) {
            if ($("input[name='ext_type']:checked").val() == "A") {
                if ($("input[name='ext_w']:checked").val() == "0") {
                    return exts.indexOf(ext + v + ",") > -1 ? true : false;
                }
                else if ($("input[name='ext_w']:checked").val() == "1" || $("input[name='ext_w']:checked").val() == "2" || $("input[name='ext_w']:checked").val() == "3") {
                    if (exts.indexOf("A0" + v + ",") > -1) return true;
                    else return exts.indexOf(ext + v + ",") > -1 ? true : false;
                }
            }
            else if ($("input[name='ext_type']:checked").val() == "B") {
                return exts.indexOf(ext + v + ",") > -1 ? true : false;
            }
        }
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
                        $("#iframe_Mask").show();
                        $("#divload").show();
                        $.fn.zTree.init($("#treeDemo"), setting, da);
                        $("#hideNodesBtn").bind("click", { type: "rename" }, hideNodes);
                        $("#showNodesBtn").bind("click", { type: "icon" }, showNodes);
                        setTitle();
                        count();
                        isFirst = false;
                        $("#sel_id").val("");
                        $("#iframe_Mask").hide();
                        $("#divload").hide();
                    }
                    else {
                        Popalert("操作失败，请重试！");
                    }
                    $("#iframe_Mask").hide();
                    $("#divload").hide();
                }
            });
            $.ajax({
                url: "getdata.aspx",
                type: "post",
                data: "com=search_menu&t=1&c" + new Date().getTime(),
                dataType: "json",
                error: function(err) {
                    $("#iframe_Mask").hide(); $("#divload").hide();
                    Popalert("请求的信息不存在！");
                },
                success: function(da) {
                    if (da != null) {
                        $("#iframe_Mask").show();
                        $("#divload").show();
                        $.fn.zTree.init($("#treeDemo1"), setting_menu, da);
                        $("#hideNodesBtn").bind("click", { type: "rename" }, hideNodes);
                        $("#showNodesBtn").bind("click", { type: "icon" }, showNodes);
                        setTitle();
                        count();
                        isFirst = false;
                        $("#sel_id").val("");
                        $("#iframe_Mask").hide();
                        $("#divload").hide();
                    }
                    else {
                        Popalert("操作失败，请重试！");
                    }
                    $("#iframe_Mask").hide();
                    $("#divload").hide();
                }
            });
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

        function onCheck(event, treeId, treeNode, clickFlag) {
            for_check(treeNode, treeNode.checked);
        }
        function for_check(TreeNode, b) {
            if (!TreeNode.children) {
                if (b) {
                    if (exts.indexOf(ext + TreeNode.code + ",") < 0) exts += ext + TreeNode.code + ",";
                }
                else {
                    exts = exts.replace(ext + TreeNode.code + ",", "");
                }
            }
            else {
                for (var i = 0; i < TreeNode.children.length; i++) {
                    var treeNode1 = TreeNode.children[i];
                    for_check(treeNode1, b);
                }
            }
        }
        function onClick(event, treeId, treeNode, clickFlag) {
            if (treeNode.type == "1") { $("a").removeClass("curSelectedNode"); sel_data_pk = ""; return }
            sel_data_pk = treeNode.flag;
            $("#iframe_Mask").show();
            $("#divload").show();
            $.ajax({
                url: "getdata.aspx",
                type: "post",
                data: "com=get_role_rights&role_pk=" + sel_data_pk + "&c" + new Date().getTime(),
                dataType: "json",
                error: function(err) {
                    $("#iframe_Mask").hide(); $("#divload").hide();
                    $("#iframe_Mask").hide();
                    $("#divload").hide();
                    Popalert("请求错误！");
                },
                success: function(da) {
                    if (da != null) {
                        $("input[name='ext_type']:eq(0)").attr("checked", 'checked');
                        $("input[name='ext_w']:eq(0)").attr("checked", 'checked');
                        $("#ext_w").css("display", "");
                        $("#ext_s").css("display", "none");
                        if (da.Table.length > 0) {
                            data = da.Table[0];
                            exts = data.rig_val;
                            if (exts == "") $.fn.zTree.getZTreeObj("treeDemo1").checkAllNodes(false);
                            else set_ext();
                        }
                        else {
                            exts = "";
                            $.fn.zTree.getZTreeObj("treeDemo1").checkAllNodes(false);
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
