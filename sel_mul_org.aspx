<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sel_mul_org.aspx.cs" Inherits="sel_mul_org" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="css/newcss.css" rel="stylesheet" type="text/css" />
    <link id="Systheme" rel="stylesheet" type="text/css" href="css/theme.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link href="css/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>

    <script src="js/cus_main.js" type="text/javascript"></script>

    <link rel="stylesheet" href="css/zTreeStyle/zTreeStyle.css" type="text/css">
	<script type="text/javascript" src="js/zTreeStyle/jquery.ztree.core-3.5.js"></script>
	<script type="text/javascript" src="js/zTreeStyle/jquery.ztree.excheck-3.5.js"></script>
	<script type="text/javascript" src="js/zTreeStyle/jquery.ztree.exhide-3.5.js"></script>
</head>
<body style=" margin:0; padding:0;">
    <form id="form1" runat="server">
    <div id="container">
        <div id="content">
            <div id="infobox" style="width: 99%; height: 100%;">
                <div class="zTreeDemoBackground left">
                    <ul id="treeDemo" class="ztree">
                    </ul>
                </div>
            </div>
            
        </div>
        <div style="width: 238px; margin: 10px auto;">
                <div id="btn_save" class="submit" style="float: left;">
                    保 存</div>
                <div id="btn_rest" class="rest" style=" float: left;">
                    返 回</div>
            </div>
    </div>
    </form>

    <script type="text/javascript">
        var setting = {
            check: {
                enable: true
            },
            view: {
                showIcon: false,
                selectedMulti: true
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
        var data;
        var sel_data_pk = "";
        $(document).ready(function() {
            $("#content").height(pageHeight - 100);
            Bind();
            $("#btn_save").click(function() {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                checkNode = zTree.getCheckedNodes(true);
                if (checkNode.length == 0) { Popalert("请选择部门！"); return false; }
                sel_data_pk = "{\"org\":["
                for (var i = 0; i < checkNode.length; i++) {
                  
                    if (checkNode[i].id == "00000") continue;
                    sel_data_pk += "{\"org_pk\":\"" + checkNode[i].flag + "\",";
                    sel_data_pk += "\"org_name\":\"" + checkNode[i].name + "\"}";
                    if (i < checkNode.length - 1) sel_data_pk += ",";
                }
                sel_data_pk += "]}"

                parent.set_org(sel_data_pk);
                parent.closePop();
            });
            $("#btn_rest").click(function() {
                parent.closePop();
            });
        });
        function set_org(pks) {
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");           
            var treeNode = zTree.getNodes();
            for (var i = 0; i < treeNode.length; i++) {
                for_set(treeNode[i], pks);
            }
        }
        function for_set(TreeNode, pks) {
            var ztree = $.fn.zTree.getZTreeObj("treeDemo");
            if (!TreeNode.children) {
                if (pks.indexOf(TreeNode.flag) > -1) {
                    ztree.selectNode(TreeNode, true, true, false);
                }              
            }
            else {
                for (var j = 0; j < TreeNode.children.length; j++) {
                    var treeNode1 = TreeNode.children[j];
                    for_set(treeNode1, pks);
                }
            }

        }
        function Bind() {
            $("#iframe_Mask").show();
            $("#divload").show();
            sel_data_pk = "";
            $.ajax({
                url: "sys_manage/getdata.aspx",
                type: "post",
                data: "com=search_org&c" + new Date().getTime(),
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
            sel_data_pk = "{\"org_pk\":\"" + treeNode.flag+"\",";
            sel_data_pk += "\"org_name\":\"" + treeNode.name+"\"}";
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
