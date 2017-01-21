<%@ Page Language="C#" AutoEventWireup="true" CodeFile="product_add.aspx.cs" Inherits="product_product_add" %>

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

    <script src="../js/main.js" type="text/javascript"></script>

    <style>
        #preview
        {
            position: absolute;
            border: 1px solid #ccc;
            background: #333;
            padding: 5px;
            display: none;
            color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="content" style="overflow-y: auto;">
            <table width="100%" cellpadding="0" cellspacing="0">
                <input class="input" style="width: 150px;" id="product_pk" type="hidden" value="<%=edit_pk %>" />
               
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                       商品名称：
                    </td>
                    <td>
                        <input class="input" style="width: 250px;" id="product_name" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                       商品图片：
                    </td>
                    <td>
                         <span id="xgt"></span>
                        <input class="input" style="display: none;" id="product_pic" type="text" />
                        <input id="Button1" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />
                                                  
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        所需积分：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="product_credit" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        数量：
                    </td>
                    <td>
                        <input class="input" style="width: 150px;" id="product_count" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                        详细说明：
                    </td>
                    <td>
                        <textarea id="product_rem" class="input"  style=" width:400px; height:80px;"></textarea>                        
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
            $("#content").height(pageHeight - 2);
            $("#btn_save").click(function() {               
                if ($("#product_name").val() == "") { Popalert("商品名称不能为空！"); return; }             
                if ($("#product_credit").val() == "") { Popalert("所需积分不能为空！"); return; }
                if ($("#product_count").val() == "") { Popalert("数量不能为空！"); return; }
    
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_product", product_pk: $("#product_pk").val(), product_name: $("#product_name").val(), product_pic: $("#product_pic").val(), product_credit: $("#product_credit").val(), product_count: $("#product_count").val(), product_rem: $("#product_rem").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            Popalert("保存成功！");
                            parent.data = da;
                            parent.jumpToPage(parent.currentPage);
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
            if ("<%=edit_pk %>" != "") {
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "get_product_info", product_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];  
                            $("#product_pk").val(info.product_pk);
                            $("#product_name").val(info.product_name);
                            $("#product_pic").val(info.product_pic);
                            if (info.product_pic != "") $("#xgt").html("<img src='" + info.product_pic + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");

                            $("#product_credit").val(info.product_credit);
                            $("#product_count").val(info.product_count);
                            $("#product_rem").val(textdecode(info.product_rem));                           
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
            KindEditor.ready(function(K) {
                var editor = K.editor({
                    allowFileManager: false
                });
              
                K('#Button1').click(function() {
                    editor.loadPlugin('image', function() {
                        editor.plugin.imageDialog({
                            showRemote: false,
                            clickFn: function(url, title, width, height, border, align) {
                                K('#product_pic').val(url);
                                editor.hideDialog();
                                $("#xgt").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                                imagePreview();
                            }
                        });
                    });
                });
            });
        });

        function closePop() {
            $("#iframe_Mask").fadeOut();
            $("#pop_map").fadeOut();
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
