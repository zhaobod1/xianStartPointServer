<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="message_add.aspx.cs" Inherits="message_message_add" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content" style="overflow-y: auto;">
            <table width="100%" cellpadding="0" cellspacing="0">               
                <tr>
                    <td  style="background-color: #F4F2F3; text-align: right; padding:10px 0; width:25%;">
                        接受对象：
                    </td>
                    <td>
                        <input id="Radio1" type="radio" checked="checked" name="sex" value="0" />全部&nbsp;&nbsp;&nbsp;<input
                            id="Radio2" name="sex" value="1" type="radio" />司机&nbsp;&nbsp;&nbsp;<input
                            id="Radio3" name="sex" value="2" type="radio" />用户
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        内容：
                    </td>
                    <td style=" padding:5px;">
                       <textarea id="Text3" cols="100" rows="8" style="width:80%;height:100px; border:solid 1px #ccc;"></textarea>
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
      
        var pageHeight = getClientHeight();
        var pageWidth = getClientWidth();
         var st=null;
         $(document).ready(function() {
             $("#content").height(pageHeight - 2);
             $("#btn_save").click(function() {
                 if ($("#Text3").val() == "") { Popalert("请输入消息内容！"); return; };
                 $.ajax({
                     url: "getdata.aspx",
                     type: "post",
                     data: { com: "save_message", user_type: $("input[name='sex']:checked").val(), message_content: $("#Text3").val(), c: new Date().getTime() },
                     dataType: "json",
                     error: function(err) {
                         $("#iframe_Mask").hide(); $("#divload").hide();
                         Popalert("请求错误！");
                     },
                     success: function(da) {
                         if (da != null) {
                             Popalert("保存成功！");
                             location.href = location.href;
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
