<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="reg_clause_add.aspx.cs" Inherits="sys_manage_reg_clause_add" %>

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

    <script src="../js/province.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../editor/themes/default/default.css" />
	<link rel="stylesheet" href="../editor/plugins/code/prettify.css" />
	<script charset="utf-8" src="../editor/kindeditor.js"></script>
	<script charset="utf-8" src="../editor/lang/zh_CN.js"></script>
	<script charset="utf-8" src="../editor/plugins/code/prettify.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content" style="overflow-y: auto;">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="150"  style="background-color: #F4F2F3; height:50px; line-height:50px; ">
                        &nbsp;&nbsp;&nbsp;<%=code_name %>
                    </td>
                                 
                </tr>
                
                <tr>
                 
                    <td style=" padding:5px;">
                       <textarea id="Text3" cols="100" rows="8" style="width:100%;height:300px;visibility:hidden;"></textarea>
                    </td>
                </tr>               
               
            </table>
            <div style="width: 258px; margin: 30px auto;">
                <div id="btn_save" class="submit" style="float: left;">
                    保 存</div>
                <div id="btn_rest" class="rest" style="margin-left: 40px; float: left;">
                    返 回</div>
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
        var editor1;
		KindEditor.ready(function(K) {
		        editor1 = K.create('#Text3', {
		        cssPath: '../editor/plugins/code/prettify.css',
		        uploadJson: '../editor/asp.net/upload_json.ashx',
		        fileManagerJson: '../editor/asp.net/file_manager_json.ashx',
				allowFileManager : true,
				afterCreate : function() {
					var self = this;
					K.ctrl(document, 13, function() {
						self.sync();
						K('form[name=example]')[0].submit();
					});
					K.ctrl(self.edit.doc, 13, function() {
						self.sync();
						K('form[name=example]')[0].submit();
					});
				}
			});
			prettyPrint();
		});
        var pageHeight = getClientHeight();
        var pageWidth = getClientWidth();
         var st=null;
         $(document).ready(function() {
             $("#content").height(pageHeight - 2);
             $("#Text51").attr("readonly", "true").click(function() { sel_user(5); });
             $("#Text3").val(textdecode("<%=code_content %>"));
             st = setInterval("s()", 100);
             $("#btn_save").click(function() {
                 $("#Text3").val(editor1.html());                
                 $.ajax({
                     url: "getdata.aspx",
                     type: "post",
                     data: { com: "save_code",code: "<%=edit_pk %>",  code_content: $("#Text3").val(), c: new Date().getTime() },
                     dataType: "json",
                     error: function(err) {
                         $("#iframe_Mask").hide(); $("#divload").hide();
                         Popalert("请求错误！");
                     },
                     success: function(da) {
                         if (da != null) {
                             Popalert("保存成功！");                            
                             parent.closePop();
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
            
                 $("#iframe_Mask").hide();
                 $("#divload").hide();
             
         });
         function textdecode(str) {
             str = str.replace(/&amp;/gi, '&');
             str = str.replace(/&lt;/gi, '<');
             str = str.replace(/&gt;/gi, '>');
             str = str.replace(/&nbsp;/gi, ' ');
             str = str.replace(/''/gi, '"');
             str = str.replace(/<brbr>/gi, '\n');
             return str;
         }
         function s() {
             if (editor1 != null) {
                 editor1.html($("#Text3").val());
                 clearInterval(st);
             }
         }
       
        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_user").fadeOut();
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
