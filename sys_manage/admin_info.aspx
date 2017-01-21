<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_info.aspx.cs" Inherits="sys_manage_admin_info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script src="../JS/MoveDiv.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style=" display:none;">
                    <td width="300" style="background-color: #F4F2F3; text-align: right;">
                        权限范围：
                    </td>
                    <td>
                    <input class="input" style=" display:none;" id="Text0" type="text" />
                        <select id="Text1" style="width: 100px; height: 26px; margin: 5px;" class="input">
                            <option value="0">本人</option>
                            <option value="0">本部门</option>
                            <option value="0">本学校</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        真实姓名：
                    </td>
                    <td>
                        <input readonly="readonly" disabled="disabled" class="input" style="width: 150px;" id="Text2" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        登录名：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="Text3" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        密码：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="Text4" type="password" />
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        手机号码：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="Text5" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        Email：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="Text6" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        所在部门：
                    </td>
                    <td>
                        <input  class="input" style=" display:none "  id="Text7" type="text" />
                        <input readonly="readonly" disabled="disabled" class="input" style="width: 150px;" id="Text71" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        兼职部门：
                    </td>
                    <td>
                        <input class="input" style=" display:none "id="Text8" type="text" />
                         <input readonly="readonly" disabled="disabled" class="input" style="width: 150px;"  id="Text81" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        对应角色：
                    </td>
                    <td>
                        <input class="input" style="display:none " id="Text9" type="text" />
                        <input readonly="readonly" disabled="disabled" class="input" style="width: 150px;" id="Text91" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        排序号：
                    </td>
                    <td>
                         <input class="input" style="width: 150px;" id="Text10" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        备注：
                    </td>
                    <td>
                         <input class="input" style="width: 150px;" id="Text11" type="text" />
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                       是否锁定：
                    </td>
                    <td>
                        <input id="Radio1" disabled="disabled" value="0" name="user_lock" type="radio" />是&nbsp;&nbsp;
                        <input id="Radio2" disabled="disabled"  value="1" checked="checked" name="user_lock" type="radio" />否
                    </td>
                </tr>
            </table>
            <div style="width: 219px; margin: 30px auto;">
                <div id="btn_save" class="submit" style="float: left;">
                    保 存</div>
                
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
					<iframe id="iframe_SelOrg" name="iframe_SelOrg" src="../sel_org.aspx" class="ifame" frameborder="0" width="264px" height="396">
					</iframe>
				</td>
			</tr>
		</table>
	</div>
	<div id="pop_role" class="infobox" style="display: none;">
		<div class="tit">
			选择角色</div>
		<div class="close" onclick="closePop();">
		</div>
		<table cellpadding="0" cellspacing="0" style="background: #fff;">
			<tr>
				<td>
					<iframe id="iframe_SelRole" name="iframe_SelRole" src="../sel_role.aspx" class="ifame" frameborder="0" width="264px" height="396">
					</iframe>
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
        var change_pwd = "0";
        $(document).ready(function() {
            
            $("#Text4").change(function() { change_pwd = "1"; });
            if ("<%=edit_pk %>" != "") {
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_user_info", user_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            $("#Text0").val("<%=edit_pk %>");
                            $("#Text1").val(da.Table[0].user_scope);
                            $("#Text2").val(da.Table[0].user_name);
                            $("#Text3").val(da.Table[0].user_login_name);
                            $("#Text4").val(da.Table[0].user_login_pwd);
                            $("#Text5").val(da.Table[0].user_tel);
                            $("#Text6").val(da.Table[0].user_email);
                            $("#Text7").val(da.Table[0].org_pk);
                            $("#Text71").val(da.Table[0].org_name);
                            $("#Text8").val(da.Table[0].user_part);
                            $("#Text81").val(da.Table[0].user_part_name);
                            $("#Text9").val(da.Table[0].user_role);
                            $("#Text91").val(da.Table[0].role_name);
                            $("#Text10").val(da.Table[0].user_ord);
                            $("#Text11").val(da.Table[0].user_rem);
                            if (da.Table[0].user_lock == "0")
                                $("input[name='user_lock']:eq(0)").attr("checked", 'checked');
                            else
                                $("input[name='user_lock']:eq(1)").attr("checked", 'checked');
                            $.ajax({
                                url: "getdata.aspx",
                                type: "post",
                                data: { com: "search_role_name", role_code: $("#Text9").val(), c: new Date().getTime() },
                                dataType: "json",
                                error: function(err) {
                                },
                                success: function(da) {
                                    $("#Text91").val(da.result);
                                }
                            });
                        }
                        else {
                            Popalert("操作失败，请重试！");
                        }
                        $("#iframe_Mask").hide();
                        $("#divload").hide();
                    }
                });
            }
            else {
                $("#iframe_Mask").hide();
                $("#divload").hide();
            }
            $("#btn_save").click(function() {
                if ($("#Text2").val() == "") { Popalert("请输入真实姓名！"); return; };
                if ($("#Text4").val() == "") { Popalert("请输入密码！"); return; };
                if ($("#Text7").val() == "") { Popalert("请选择所在部门！"); return; };
                if ($("#Text9").val() == "") { Popalert("请选择对应角色！"); return; };
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_user", change_pwd: change_pwd, user_pk: $("#Text0").val(), org_pk: $("#Text7").val(), user_scope: $("#Text1").val(), user_name: $("#Text2").val(), user_login_name: $("#Text3").val(), user_login_pwd: $("#Text4").val(), user_tel: $("#Text5").val(), user_email: $("#Text6").val(), user_part: $("#Text8").val(), user_role: $("#Text9").val(), user_ord: $("#Text10").val(), user_rem: $("#Text11").val(), user_lock: $("input[name='user_lock']:checked").val(), c: new Date().getTime() },
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
                parent.closePop();
            });
        });
        function sel_org(ix) {
            input_index = ix;
            if ($("#iframe_SelOrg").attr("src") == undefined) $("#iframe_SelOrg").attr("src", "../sel_org.aspx");
            $("#pop_org").css({ "left": (pageWidth - $("#pop_org").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
            if ($("#iframe_SelOrg")[0].contentWindow.set_org != null && $("#iframe_SelOrg")[0].contentWindow.set_org != undefined)
                $("#iframe_SelOrg")[0].contentWindow.set_org($("#Text" + input_index).val());
            $("#iframe_Mask").show();            
            $("#pop_org").show();
        }
        function sel_role(ix) {
            input_index = ix;
            if ($("#iframe_SelRole").attr("src") == undefined) $("#iframe_SelRole").attr("src", "../sel_role.aspx");
            $("#pop_role").css({ "left": (pageWidth - $("#pop_role").width()) / 2 + "px", "top": (document.documentElement.scrollTop + 40) + "px" });
            if ($("#iframe_SelRole")[0].contentWindow.set_role != null && $("#iframe_SelRole")[0].contentWindow.set_role != undefined)
                $("#iframe_SelRole")[0].contentWindow.set_role($("#Text" + input_index).val());
            $("#iframe_Mask").show();
            $("#pop_role").show();
        }
        var input_index;
        function set_org(val) {
            var v = jQuery.parseJSON(val);
            $("#Text" + input_index).val(v.org_pk);
            $("#Text" + input_index + "1").val(v.org_name);
        }
        function set_role(val) {
            var v = jQuery.parseJSON(val);
            $("#Text" + input_index).val(v.role_pk);
            $("#Text" + input_index + "1").val(v.role_name);
        }
        
        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_org").fadeOut();
            $("#pop_role").fadeOut();
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
