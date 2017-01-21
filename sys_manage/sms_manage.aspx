<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sms_manage.aspx.cs" Inherits="sys_manage_sms_manage" %>

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
                <tr>
                    <td width="30%" style="background-color: #F4F2F3; font-weight:bold; height:35px; line-height:35px;" colspan="4">
                        短信设置：
                    </td>
                   
                   
                </tr>    
                <tr>
                    <td width="30%" style="background-color: #F4F2F3; text-align: right;">
                        短信平台账号：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 200px;" id="Text1" type="text" /><font size="2" color="red">*短信平台商提供</font>
                    </td>
                   
                </tr>               
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        短信平台密码：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 200px;" id="Text2" type="text" /><font size="2" color="red">*短信平台商提供</font>
                    </td>
                   
                </tr>
                 <tr>
                    <td  style="background-color: #F4F2F3; text-align: right; border-bottom:none;">
                        末尾签名：
                    </td>
                    <td colspan="3" style="border-bottom:none;">
                        <input class="input" style="width: 200px;" id="Text4" type="text" />
                    </td>
                   
                </tr>   
                <tr>
                    <td width="30%" style="background-color: #F4F2F3; font-weight:bold;height:35px; line-height:35px;" colspan="4">
                        APP升级：
                    </td>                                      
                </tr>    
                 <tr>
                    <td  style="background-color: #F4F2F3; text-align: right;">
                        客户端版本：
                    </td>
                    <td colspan="3">
                        <input class="input" style="width: 200px;" id="Text5" type="text" />
                    </td>
                   
                </tr>  
                 <tr >
                    <td  style="background-color: #F4F2F3; text-align: right;border-bottom:none;">
                        司机端版本：
                    </td>
                    <td colspan="3" style="border-bottom:none;">
                        <input class="input" style="width:200px" id="Text6" type="text" />
                    </td>
                   
                </tr> 
                 <tr>
                    <td width="30%" style="background-color: #F4F2F3; font-weight:bold;height:35px; line-height:35px;" colspan="4">
                       其它设置：
                    </td>                                      
                </tr>    
                 <tr >
                    <td  style="background-color: #F4F2F3; text-align: right;  ">
                        注册赠送：
                    </td>
                    <td colspan="3">
                      <input class="input" style="width: 200px;" id="Text8" type="text" />元
                    </td>
                   
                </tr>   
                 <tr >
                    <td style="background-color: #F4F2F3; text-align: right;">
                        充值赠送：
                    </td>
                    <td colspan="3">
                         <input class="input" style="width: 400px;" id="Text7" type="text" />
                     
                     
                    </td>
                    
                </tr>  
                     <tr style=" display:none;">
                    <td  style="background-color: #F4F2F3; text-align: right;">
                       
                    </td>
                    <td colspan="3" style=" padding:5px;">
                        <div id="btn_save1" class="submit" style="float: left;">
                    查询短信余额</div> <span style=" line-height:32px;"></span>
                    </td>
                   
                </tr> 
                           
            </table>
            <div style="width: 40px; margin: 30px auto;">
                <div id="btn_save" class="submit" style="float: left;">
                    保 存</div>             
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
            $("#btn_save1").click(function() {
                $.ajax({
                    url: "getdata.aspx",
                    data: { com: "sel_sms_result",url:$("#Text6").val(), sms_user: $("#Text1").val(), sms_pass: $("#Text2").val(),c: new Date().getTime() },
                    type: "post",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        alert(da);
                        $("#iframe_Mask").hide();
                        $("#divload").hide();
                    }
                });
            });
            $("#btn_save").click(function() {

                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_sms_manage", sms_user: $("#Text1").val(), sms_pass: $("#Text2").val(), sms_pass_en: $("#Text3").val(), sms_send_url: $("#Text4").val(), sms_succ_code: $("#Text5").val(), sms_sel_url: $("#Text6").val(), sms_result_start_code: $("#Text7").val(), sms_result_end_code: $("#Text8").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            $("#Text1").val(da.Table[0].sms_user);
                            $("#Text2").val(da.Table[0].sms_pass);
                            $("#Text3").val(da.Table[0].sms_pass_en);
                            $("#Text4").val(da.Table[0].sms_send_url);
                            $("#Text5").val(da.Table[0].sms_succ_code);
                            $("#Text6").val(da.Table[0].sms_sel_url);
                            $("#Text7").val(da.Table[0].sms_result_start_code);
                            $("#Text8").val(da.Table[0].sms_result_end_code);
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
                        $("#Text1").val(da.Table[0].sms_user);
                        $("#Text2").val(da.Table[0].sms_pass);
                        $("#Text3").val(da.Table[0].sms_pass_en);
                        $("#Text4").val(da.Table[0].sms_send_url);
                        $("#Text5").val(da.Table[0].sms_succ_code);
                        $("#Text6").val(da.Table[0].sms_sel_url);
                        $("#Text7").val(da.Table[0].sms_result_start_code);
                        $("#Text8").val(da.Table[0].sms_result_end_code);                                               
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
