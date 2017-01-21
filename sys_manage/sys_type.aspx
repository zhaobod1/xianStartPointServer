<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sys_type.aspx.cs" Inherits="sys_manage_sys_type" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/newcss.css" rel="stylesheet" type="text/css" />
    <link id="Systheme" rel="stylesheet" type="text/css" href="../css/theme.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-1.7.2.js" type="text/javascript"></script>

    <script src="../JS/cus_main.js" type="text/javascript"></script>

    <script src="../JS/flexigrid.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/zTreeStyle/jquery.ztree.core-3.5.js"></script>

    <script type="text/javascript" src="../js/zTreeStyle/jquery.ztree.excheck-3.5.js"></script>

    <script type="text/javascript" src="../js/zTreeStyle/jquery.ztree.exhide-3.5.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content">
            <div class="tools">
                <div title="单击一次新增选择节点的子节点，单击两次新增根节点" style="float: left;" class="btn_add">
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
                    <td align="left" valign="top" style="border-top: none;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="40%" style="background-color: #F4F2F3; border-left: none; text-align: right;">
                                    <%if (t == "1") Response.Write("媒体类型");
                                      else if (t == "2") Response.Write("媒体形式");
                                      else if (t == "3") Response.Write("合同类型");
                                      else if (t == "4") Response.Write("收入类型");
                                      else if (t == "5") Response.Write("媒体上下行名称");
                                      else if (t == "6") Response.Write("经营业务类型");%>：
                                </td>
                                <td>
                                    <input class="input" style="display:none;" id="Text0" type="text" /><input class="input"
                                        style="display:none;" id="Text00" value="00000" type="text" />
                                    <input class="input" style="width: 150px;" id="Text1" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" style="background-color: #F4F2F3; border-left: none; text-align: right;">
                                    排序号：
                                </td>
                                <td>
                                    <input class="input" style="width: 150px;" id="Text2" type="text" />
                                </td>
                            </tr>
                            <%if (t == "1" || t=="3" || t=="4" || t=="6"){%>
                            <tr>
                                <td width="40%" style="background-color: #F4F2F3; border-left: none; text-align: right;">
                                    授权部门：
                                </td>
                                <td>
                                    <textarea class="input" style="width:200px; height:100px;" id="Text31"></textarea>
                                    <input class="input" style="display: none;" id="Text3" type="text" />
                                </td>
                            </tr>
                            <%} %>
                            <tr>
                                <td colspan="2" style="border-left: none; height: 70px;">
                                    <div style="width: 256px; margin: 0 auto;">
                                        <div id="btn_save" class="submit" style="float: left;">
                                            保 存</div>
                                        <div id="btn_rest" class="rest" style="margin-left: 40px; float: left;">
                                            取 消</div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div style="clear: both;">
            </div>
        </div>
    </div>
    <div id="iframe_Mask" style="background: #333333; width: 100%; height: 100%; left: 0;
        top: 0; position: absolute; z-index: 9999; background-color: Gray; filter: alpha(opacity=80);
        -moz-opacity: 0.5; opacity: 0.5;" name="iframe_Mask">
    </div>
    <div id="pop_org" class="infobox" style="display: none;">
        <div class="tit">
            选择部门</div>
        <div class="close" onclick="closePop();">
        </div>
        <table cellpadding="0" cellspacing="0" style="background: #fff;">
            <tr>
                <td>
                    <iframe id="iframe_SelOrg" name="iframe_SelOrg" src="../sel_mul_org.aspx" class="ifame"
                        frameborder="0" width="264px" height="396"></iframe>
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
        var setting = {
            view: {
                showIcon: true
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
        var type = "<%=t %>";
        var recordsCount = 0; //共有记录数
        var data;
        var sel_data_pk = "";
        $(document).ready(function() {
            $("#content").height(pageHeight - 12);
            $("#infobox").width(pageWidth - 2).height($("#content").height() - $(".tools").height());
            $("#Text31").attr("readonly", "true").click(function() { sel_org(3); });
            Bind();
            $("#btn_save").click(function() {
                if ($("#Text1").val() == "") { Popalert("请输入媒体类型！"); return; };
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_type", t: type, type_pk: $("#Text0").val(), parent_type_code: $("#Text00").val(), type_name: $("#Text1").val(), type_ord: $("#Text2").val(), type_org: $("#Text3").val(), type_org_name: $("#Text31").val(), c: new Date().getTime()
                    },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            Popalert("保存成功！");
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
                if ($("#Text1").val() == "") { $("#Text00").val("00000"); $("a").removeClass("curSelectedNode"); }
                $("#Text0").val("");
                $("#Text1").val("");
                $("#Text2").val("");
                $("#Text3").val("");
                $("#Text31").val("");
                if ("<%=t %>" == "5") $("#Text00").val("00000");
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
                        data: "com=del_type&t=" + type + "&del_pk=" + del_pk + "&c" + new Date().getTime(),
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
                                    $("#btn_rest").click();
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
                data: "com=search_type&t=" + type + "&c" + new Date().getTime(),
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
                data: "com=get_type_info&type_pk=" + sel_data_pk + "&c" + new Date().getTime(),
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
                        $("#Text0").val(data.type_pk);
                        $("#Text00").val(data.type_code);
                        $("#Text1").val(data.type_name);
                        $("#Text2").val(data.type_ord);
                        $("#Text3").val(data.type_org);
                        $("#Text31").val(data.type_org_name);                       
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
        function sel_org(ix) {
            input_index = ix;
            if ($("#iframe_SelOrg").attr("src") == undefined) $("#iframe_SelOrg").attr("src", "../sel_mul_org.aspx");
            $("#pop_org").css({ "left": (pageWidth - $("#pop_org").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
            if ($("#iframe_SelOrg")[0].contentWindow.set_org != null && $("#iframe_SelOrg")[0].contentWindow.set_org != undefined)
                $("#iframe_SelOrg")[0].contentWindow.set_org($("#Text" + input_index).val());
            $("#iframe_Mask").show();
            $("#pop_org").show();
        }
        var input_index;
        function set_org(val) {
            var v = jQuery.parseJSON(val);
            $("#Text" + input_index).val("");
            $("#Text" + input_index + "1").val("");
            for (var i = 0; i < v.org.length; i++) {
                if ($("#Text" + input_index).val().indexOf(v.org[i].org_pk) > -1) continue;
                if ($("#Text" + input_index).val() != "") $("#Text" + input_index).val($("#Text" + input_index).val() + "," + v.org[i].org_pk);
                else $("#Text" + input_index).val(v.org[i].org_pk);
                if ($("#Text" + input_index + "1").val() != "")
                    $("#Text" + input_index + "1").val($("#Text" + input_index + "1").val() + "," + v.org[i].org_name);
                else
                    $("#Text" + input_index + "1").val(v.org[i].org_name);
            }
        }
        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_org").fadeOut();
           
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
