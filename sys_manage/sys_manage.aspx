<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sys_manage.aspx.cs" Inherits="sys_manage_sys_manage" %>

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
    <script src="../js/ajaxfileupload.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display:none;">
                    <td width="35%" style="background-color: #F4F2F3; text-align: right;">
                        系统logo：
                    </td>
                    <td>
                        <input class="input" style="width: 200px;" id="Text2" type="text" />
                    </td>
                    <td rowspan="2">
                        <img id="logo" height="107" src="" />
                    </td>
                </tr>
                
                <tr  style="display:none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                    </td>
                    <td>
                        <input id="File1" name="File1" type="file" style="border: solid 1px #CDCDCD; width: 200px;
                            background-color: #F4F2F3;" /><input id="Button1" type="button" style="border: solid 1px #CDCDCD;
                                background-color: #F4F2F3; font-size: 12px;" value="上传" /><span style="color: Red;
                                    font-size: 12px;">注：只能上传PNG格式文件，图片最佳宽度173 * 高度107</span>
                    </td>
                </tr>
                
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        系统标题：
                    </td>
                    <td colspan="2">
                        <input class="input" style="width:40%;" id="Text1" type="text" />
                    </td>
                </tr>
                  <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                       预约设置：
                    </td>
                    <td colspan="2">
                        &nbsp;&nbsp;爽约<input class="input" style="width:50px;" id="Text3" type="text" />次锁定账号
                    </td>
                </tr>
                 <tr style=" display:none;">
                    <td style="background-color: #F4F2F3; text-align: right;">
                        中短期合同：
                    </td>
                    <td colspan="2">
                        小于<input class="input" style="width:50px;" id="Text4" type="text" />天合同
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
        $(document).ready(function() {           
            Bind();

            $("#btn_save").click(function() {
              
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_sys_manage", sys_title: $("#Text1").val(), sys_cancel: $("#Text3").val(), sys_logo: $("#Text2").val(), pact_long: $("#Text4").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            $("#Text1").val(da.Table[0].sys_title);
                            $("#Text2").val(da.Table[0].sys_logo);
                            $("#Text3").val(da.Table[0].sys_cancel);
                            $("#Text4").val(da.Table[0].pact_long);
                            //$("#logo").attr("src", "../uploadfile/sys/" + da.Table[0].sys_logo);
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
            $("#Button1").click(function() {
                if ($("#File1").val() == "") return;
                $.ajaxFileUpload({
                    url: "getdata.aspx?com=upload_logo",
                    secureuri: false,
                    fileElementId: 'File1',
                    dataType: 'json',
                    success: function(da) {
                        if (da != null) {
                            if (da.result == "-99") {
                                Popalert("文件类型不支持，请重试！"); return;
                            }
                            $("#Text2").val(da.result);                           
                        }
                        else {
                            Popalert("操作失败，请重试！");
                        }
                    }
                });
            });
            $("#btn_rest").click(function() {
                Bind();
            });
        });
        function Bind() {
            $.ajax({
                url: "getdata.aspx",
                type: "post",
                data: { com: "get_sys_manage", c: new Date().getTime() },
                dataType: "json",
                error: function(err) {
                    $("#iframe_Mask").hide(); $("#divload").hide();
                    Popalert("请求错误！");
                },
                success: function(da) {
                    if (da != null) {
                        $("#Text1").val(da.Table[0].sys_title);
                        $("#Text2").val(da.Table[0].sys_logo);
                        $("#Text3").val(da.Table[0].sys_cancel);
                        $("#Text4").val(da.Table[0].pact_long);
                        $("#logo").attr("src", "../uploadfile/sys/"+da.Table[0].sys_logo);
                        
                    }
                    else {
                        Popalert("操作失败，请重试！");
                    }
                    $("#iframe_Mask").hide();
                    $("#divload").hide();
                }
            });
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
