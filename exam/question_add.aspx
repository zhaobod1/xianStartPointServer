<%@ Page Language="C#" AutoEventWireup="true" CodeFile="question_add.aspx.cs" Inherits="exam_question_add" %>

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
                <input class="input" style="width: 150px;" id="question_pk" type="hidden" value="<%=edit_pk %>" />
                <tr>
                    <td style=" width:20%; background-color: #F4F2F3; text-align: right;padding:15px 0;" colspan="2">
                        科目：
                    </td>
                    <td>
                       <select id="question_sub" class="input">
                       <option value="1">科目一</option>
                       <option value="4">科目四</option>
                       </select>
                    </td>
                </tr>
                <tr>
                    <td style=" width:20%; background-color: #F4F2F3; text-align: right;padding:15px 0;" colspan="2">
                        试题类型：
                    </td>
                    <td>
                        <input id="Radio1" type="radio" checked="checked" name="question_type" value="单选" />单选&nbsp;&nbsp;&nbsp;<input
                            id="Radio2" name="question_type" value="多选" type="radio" />多选
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;" colspan="2">
                        题目：
                    </td>
                    <td>
                    <textarea class="input" style="width: 80%; height:50px;" id="question_title" ></textarea>                     
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right; padding:15px 0;" colspan="2">
                       图片：
                    </td>
                    <td>
                         <span id="xgt"></span>
                        <input class="input" style="display: none;" id="question_pic" type="text" />
                        <input id="Button1" type="button" value="上传" style="color: White; border: solid 1px #3A8DD3;
                            background-color: #9DBEFF; font-size: 12px;" />
                                                  
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;" id="bxda" rowspan="4">
                        备选答案：
                    </td>
                     <td style="background-color: #F4F2F3; text-align: right;">
                        A、
                    </td>
                    <td>
                       <textarea class="input question_answer" style="width: 80%; height:50px;" id="Textarea1" ></textarea>                       
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                     B、
                    </td>
                    <td>
                         <textarea class="input question_answer" style="width: 80%; height:50px;" id="Textarea2" ></textarea>
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                     C、
                    </td>
                    <td>
                        <textarea class="input question_answer" style="width: 80%; height:50px;" id="Textarea3" ></textarea>
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right;">
                     D、
                    </td>
                    <td>
                        <textarea class="input question_answer" style="width: 80%; height:50px;" id="Textarea4" ></textarea>
                    </td>
                </tr>
                 <tr id="Add">
                    <td style="background-color: #F4F2F3; text-align: right; padding:20px 0;" colspan="2">
                    
                    </td>
                    <td>
                       <a href="javascript:void(0);" onclick="javascript:AddAnswer();">添加备选项</a>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right; padding:15px 0;" colspan="2">
                        正确答案：
                    </td>
                    <td id="answer">
                         <input id="Radio3" type="radio" checked="checked" name="answer" value="1" />A&nbsp;&nbsp;&nbsp;
                         <input id="Radio4" name="answer" value="2" type="radio" />B&nbsp;&nbsp;&nbsp;
                         <input id="Radio5" type="radio"  name="answer" value="3" />C&nbsp;&nbsp;&nbsp;
                         <input id="Radio6" name="answer" value="4" type="radio" />D&nbsp;&nbsp;&nbsp;                
                         
                    </td>
                </tr>
                 <tr>
                    <td style="background-color: #F4F2F3; text-align: right; padding:15px 0;" colspan="2">
                        解析：
                    </td>
                    <td>
                        <textarea class="input" style="width: 80%; height:50px;" id="question_rem" ></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F4F2F3; text-align: right;" colspan="2">
                        状态：
                    </td>
                    <td>
                        <select id="question_state" style="width: 150px; height: 26px; margin: 5px;" class="input">
                         <option value="1" style="color: Green;">启用</option>
                            <option value="0" style="color: Red;">禁用</option>
                           
                        </select>
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
        var curr_obj = null;
        var index = new Array("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z");


        $(document).ready(function() {
            $("#content").height(pageHeight - 2);
            $("input[name='question_type']").click(function() {
                if ($("input[name='question_type']:checked").val() == "单选") {
                    $("#answer").html("");
                    $(".question_answer").each(function(i) {
                        $("#answer").append("<input type=\"radio\" " + (i == 0 ? "checked=\"checked\"" : "") + " name=\"answer\" value=\"" + (i + 1) + "\" />" + index[i] + "&nbsp;&nbsp;&nbsp;");
                    });
                }
                else {
                    $("#answer").html("");
                    $(".question_answer").each(function(i) {
                        $("#answer").append("<input type=\"checkbox\" name=\"answer\" value=\"" + (i + 1) + "\" />" + index[i] + "&nbsp;&nbsp;&nbsp;");
                    });
                }
            });

            $("#btn_save").click(function() {
                if ($("#question_title").val() == "") { Popalert("题目不能为空！"); return; }
                var question_answer = "";
                $(".question_answer").each(function() {
                    question_answer += $(this).val() + "∶";
                });

                if ($("#question_answer").val() == "") { Popalert("备选答案不能为空！"); return; }
                else question_answer = question_answer.substring(0, question_answer.length - 1);

                var question_right = "";
                if ($("input[name='question_type']:checked").val() == "单选") {
                    question_right = $('input[name="answer"]:checked ').val();
                }
                else {
                    $("input[name='answer']").each(function() {                    
                        if ($(this).attr('checked') == "checked") {
                            question_right += $(this).val() + "∶";
                        }
                    });
                }              
                $.ajax({
                    url: "getdata.aspx",
                    type: "post",
                    data: { com: "save_question",question_sub:$("#question_sub").val(), question_pk: $("#question_pk").val(), question_type: $("input[name='question_type']:checked").val(), question_title: $("#question_title").val(), question_pic: $("#question_pic").val(), question_answer: question_answer, question_right: question_right, question_rem: $("#question_rem").val(), question_state: $("#question_state").val(), c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
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
                    data: { com: "get_question_info", question_pk: "<%=edit_pk %>", c: new Date().getTime() },
                    dataType: "json",
                    error: function(err) {
                        $("#iframe_Mask").hide(); $("#divload").hide();
                        Popalert("请求错误！");
                    },
                    success: function(da) {
                        if (da != null) {
                            var info = da.Table[0];
                            $("#question_sub").val(info.question_sub);
                            $("#question_title").val(info.question_title);
                            var answer = info.question_answer.split('∶');
                            for (var i = 0; i < answer.length; i++) {
                                if (i > 3) AddAnswer();
                                $(".question_answer:eq(" + i + ")").val(answer[i]);
                            }
                            $("input[value='" + info.question_type + "']").attr("checked", true).click();

                            if (info.question_type == "单选") {
                                $('input[name="answer"][value="' + info.question_right + '"]').attr("checked", 'checked');
                            }
                            else {
                                $("[name='answer']").each(function() {
                                    if (info.question_right.indexOf($(this).val() + "∶") > -1) {
                                        $(this).attr("checked", 'true');
                                    }
                                });
                            }


                            $("#question_state").val(info.question_state);
                            $("#question_rem").val(info.question_rem);
                            if (info.question_pic != "") {
                                $("#question_pic").val(info.question_pic);
                                $("#xgt").html("<img src='" + info.question_pic + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
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
                                K('#question_pic').val(url);
                                editor.hideDialog();
                                $("#xgt").html("<img src='" + url + "' align=\"absmiddle\" style=\" border:solid 1px #ccc; max-height:32px; max-width:100px;\" />");
                                imagePreview();
                            }
                        });
                    });
                });
            });
        });
        function AddAnswer() {
            if ($(".question_answer").length >= 20) { Popalert("最多20个备选答案！"); return; }
            var answer_code = "<tr><td style=\"background-color: #F4F2F3; text-align: right;\">" + index[$(".question_answer").length] + "、</td>";
            answer_code += "<td><textarea class=\"input question_answer\" style=\"width: 80%; height:50px;\" id=\"Textarea4\" ></textarea></td></tr>"
            $("#Add").before(answer_code);
            if ($("input[name='question_type']:checked").val() == "单选") {
                $("#answer").append("<input  name=\"answer\" value=\"" + $(".question_answer").length + "\" type=\"radio\" />" + index[$(".question_answer").length - 1] + "&nbsp;&nbsp;&nbsp;");
            }
            else {
                $("#answer").append("<input  name=\"answer\" value=\"" + $(".question_answer").length + "\" type=\"checkbox\" />" + index[$(".question_answer").length - 1] + "&nbsp;&nbsp;&nbsp;");
            }
            $("#bxda").attr("rowspan", $(".question_answer").length);
        }
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
