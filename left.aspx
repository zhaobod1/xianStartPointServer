<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>

    <script src="js/cus_main.js" type="text/javascript"></script>

    <script src="js/zTreeStyle/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script src="js/zTreeStyle/jquery.ztree.excheck-3.5.js" type="text/javascript"></script>

    <script src="js/zTreeStyle/jquery.ztree.exhide-3.5.js" type="text/javascript"></script>

    <link href="css/style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" href="../css/zTreeStyle/zTreeStyle.css" type="text/css">
    
    
    <script src="js/jquery.mCustomScrollbar.js" type="text/javascript"></script>

    <link href="css/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />
    <style>
           
        li.level0
        {
            background-image: url(images/LeftMenuBg2.jpg);
            background-repeat: no-repeat;
            color: #055588;
            font-size: 12px;
            font-weight: bold;
            color: #055588;
            
        }
       
        .ztree li ul.level0
        {
            margin: 5px 0 0 0;
        }
        .ztree li a
        {
            height: 27px;
            line-height: 27px;
            padding: 0;
            margin-left: 8px;
        }
        .ztree li a span
        {
            margin-left: 10px;
            color:#055588;
        }
        .ztree li a span:hover
        {            
            color:#FF0000;
            text-decoration:none;   
        }
        .ztree li span.button.ico_docu
        {
            vertical-align: middle;
        }
        .ztree li span
        {
            line-height: 26px;
        }
        .ztree li span.button.ico_close, .ztree li span.button.ico_open
        {
            vertical-align: middle;
        }
        .ztree li span.button.center_docu
        {
            background-position: center center;
        }
        .ztree li span.button.bottom_docu
        {
            background-position: center center;
        }
        .ztree li span.button.switch
        {
            height: 26px;
        }
        .ztree li span.button.noline_open, .ztree li span.button.noline_close
        {
            background: none;
        }
        
        
    </style>
</head>
<body>
    <table id="main_tab" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" height="28" border="0">
                    <tbody><tr>
                        <td height="28" class="LeftMenu1">
                            功能菜单
                        </td>
                    </tr>
                </tbody></table>
            </td>
            <td width="10" rowspan="3" class="indexCenterBg">
            </td>
        </tr>
        <tr valign="top">
            <td >
                <div class="zTreeDemoBackground left" style="float: left;  width: 100%; overflow:auto; border-right: none;">
                    <ul id="treeDemo" class="ztree" style="padding:0;">
                    </ul>
                </div>
            </td>            
        </tr>
        <tr>
            <td >
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var curExpandNode = null;
        var setting = {
            view: {
                showIcon: true,
                showLine: false,
                dblClickExpand: false
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: { 
                onNodeCreated: zTreeOnNodeCreated,
                beforeExpand: beforeExpand,
                onExpand: onExpand,
                onClick: onClick          
            }
        };
        function zTreeOnNodeCreated(event, treeId, treeNode) {
            $("ul.level0 li").each(function() {
                if ($(this).find("ul").length > 0)
                    $(this).find("span").css("font-weight", "bold");
                else
                    $(this).find("span").css("font-weight", "normal");
            });
        };
        var pageHeight = getClientHeight();
        var pageWidth = getClientWidth();
        $(document).ready(function() {
            $(window).load(function() {
                $(".zTreeDemoBackground").mCustomScrollbar({ theme: "minimal-dark" });
            });
            $("#main_tab").height(pageHeight);
            $(".zTreeDemoBackground").height(pageHeight - 28);
            $.ajax({
                url: "/sys_manage/getdata.aspx",
                type: "post",
                data: "com=search_menu&c" + new Date().getTime(),
                dataType: "json",
                error: function(err) {
                    $("#iframe_Mask").hide(); $("#divload").hide();
                    Popalert("请求错误！");
                },
                success: function(da) {
                    if (da != null) {
                        $.fn.zTree.init($("#treeDemo"), setting, da);
                        $("li.level0").each(function() {
                            $(this).find("span").eq(0).remove();
                        });
                        
                    }
                    else {
                        Popalert("操作失败，请重试！");
                    }
                    $("#iframe_Mask").hide();
                    $("#divload").hide();
                }
            });

        });
      

        function onClick(event, treeId, treeNode, clickFlag) {
            $("a").removeClass("curSelectedNode");
            var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
            if (treeNode.open=="1")
                treeObj.expandNode(treeNode, false, true, true, true);
            else
            treeObj.expandNode(treeNode, true, true, true, true);
            $("a").attr("target", "right");

        }
    
        function beforeExpand(treeId, treeNode) {
            var pNode = curExpandNode ? curExpandNode.getParentNode() : null;
            var treeNodeP = treeNode.parentTId ? treeNode.getParentNode() : null;
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
            for (var i = 0, l = !treeNodeP ? 0 : treeNodeP.children.length; i < l; i++) {
                if (treeNode !== treeNodeP.children[i]) {
                    zTree.expandNode(treeNodeP.children[i], false);
                }
            }
            while (pNode) {
                if (pNode === treeNode) {
                    break;
                }
                pNode = pNode.getParentNode();
            }
            if (!pNode) {
                singlePath(treeNode);
            }

        }
        function singlePath(newNode) {
            if (newNode === curExpandNode) return;
            if (curExpandNode && curExpandNode.open == true) {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                if (newNode.parentTId === curExpandNode.parentTId) {
                    zTree.expandNode(curExpandNode, false);
                } else {
                    var newParents = [];
                    while (newNode) {
                        newNode = newNode.getParentNode();
                        if (newNode === curExpandNode) {
                            newParents = null;
                            break;
                        } else if (newNode) {
                            newParents.push(newNode);
                        }
                    }
                    if (newParents != null) {
                        var oldNode = curExpandNode;
                        var oldParents = [];
                        while (oldNode) {
                            oldNode = oldNode.getParentNode();
                            if (oldNode) {
                                oldParents.push(oldNode);
                            }
                        }
                        if (newParents.length > 0) {
                            zTree.expandNode(oldParents[Math.abs(oldParents.length - newParents.length) - 1], false);
                        } else {
                            zTree.expandNode(oldParents[oldParents.length - 1], false);
                        }
                    }
                }
            }
            curExpandNode = newNode;
        }

        function onExpand(event, treeId, treeNode) {
            curExpandNode = treeNode;
        }
    </script>


<style>
    #treeDemo_18,#treeDemo_19 {
        display: none;
    }
</style>
</body>
</html>
