<%@ Page Language="C#" AutoEventWireup="true" CodeFile="right.aspx.cs" Inherits="right" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" type="text/css" rel="stylesheet">

    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>

    <script src="js/cus_main.js" type="text/javascript"></script>

    <style>
        body
        {
            line-height: 25px;
        }
        /**************** 弹出窗口样式（参考效果、代码见 服务中心/系统帮助页面） ***********************/#div_msgInfoPop
        {
            line-height: 12px;
        }
        .Win_Top_L /**窗口左上角**/
        {
            width: 3px;
            height: 21px;
            background-image: url(images/Win_Top_L.gif);
            background-repeat: no-repeat;
        }
        .Win_Top_M /**窗口上中间**/
        {
            height: 21px;
            background-image: url(images/Win_Top.gif);
            background-repeat: repeat-x;
        }
        .Win_Top_R /**窗口右上角**/
        {
            width: 3px;
            height: 21px;
            background-image: url(images/Win_Top_R.gif);
            background-repeat: no-repeat;
        }
        .CloseMessage /**窗口右上角关闭按钮**/
        {
            cursor: hand;
            background-image: url("images/CloseXX.gif");
            background-repeat: no-repeat;
        }
        .Win_Left /**窗口中部左框线**/
        {
            width: 3px;
            background-image: url(images/Win_Left.gif);
            background-repeat: repeat-y;
        }
        .Win_Right /**窗口中部右框线**/
        {
            width: 3px;
            background-image: url(images/Win_Right.gif);
            background-repeat: repeat-y;
        }
        .Win_Bottom_L /**窗口左下角**/
        {
            width: 3px;
            height: 3px;
            background-image: url(images/Win_Bottom_L.gif);
            background-repeat: no-repeat;
        }
        .Win_Bottom_M /**窗口下中间**/
        {
            height: 3px;
            background-image: url(images/Win_Bottom.gif);
            background-repeat: repeat-x;
        }
        .Win_Bottom_R /**窗口右下角**/
        {
            width: 3px;
            height: 3px;
            background-image: url(images/Win_Bottom_R.gif);
            background-repeat: no-repeat;
        }
    </style>

    <script type="text/javascript">
        var pageHeight = getClientHeight();
        var pageWidth = getClientWidth();
        $(document).ready(function() {
            var maxheight = 0, maxheight1 = 0;
            $("#tab1").css("height", pageHeight - 24 - 20 - 99);
            if ($("#tab2").height() > $("#tab4").height() && $("#tab2").height() > $("#tab6").height())
                maxheight = $("#tab2").height();
            if ($("#tab4").height() > $("#tab2").height() && $("#tab4").height() > $("#tab6").height())
                maxheight = $("#tab4").height();
            if ($("#tab6").height() > $("#tab2").height() && $("#tab6").height() > $("#tab4").height())
                maxheight = $("#tab6").height();
            if (maxheight < $("#tab1").height() / 2 - 50) maxheight = $("#tab1").height() / 2 - 40;
            $("#tab2,#tab4,#tab6").css("height", maxheight);


            if ($("#tab3").height() > $("#tab5").height() && $("#tab3").height() > $("#tab7").height())
                maxheight1 = $("#tab3").height();
            if ($("#tab5").height() > $("#tab3").height() && $("#tab5").height() > $("#tab7").height())
                maxheight1 = $("#tab5").height();
            if ($("#tab7").height() > $("#tab3").height() && $("#tab7").height() > $("#tab5").height())
                maxheight1 = $("#tab7").height();

            if ($("#tab1").height() + 99 + 20 < maxheight + maxheight1 + 79 + 79 + 20 + 20 + 18) {
                $("#tab1").height(maxheight + maxheight1 + 79 + 79 + 20 + 20 + 18 - 99 - 20);

            }
            else if ($("#tab1").height() + 99 + 20 > maxheight + maxheight1 + 79 + 79 + 20 + 20 + 18) {
                maxheight1 = $("#tab1").height() + 99 + 20 - (maxheight + +79 + 79 + 20 + 20 + 18);
            }
            $("#tab3,#tab5,#tab7").css("height", maxheight1);


        });
    </script>

</head>
<body>
    <div style="height: 12px; overflow: hidden;">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="Center">
        <tr>
            <td width="10">
                &nbsp;
            </td>
            <td valign="top">
                
            </td>
        </tr>
    </table>
</body>
</html>
