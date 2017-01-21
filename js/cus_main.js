$(document).ready(function() {
    var reg = new RegExp("(^|&)i=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        $.ajax({
            url: "/sys_manage/getdata.aspx",
            type: "post",
            data: "com=get_right&c=" + r[2] + "&c" + new Date().getTime(),
            dataType: "json",
            success: function(da) {
               
                if (da != null) {
                    if (da.add == "0") $("div.btn_add").remove();
                    if (da.edit == "0") $("div.btn_edit").remove();
                    if (da.del == "0") $("div.btn_del").remove();
                    if (da.look == "0") $("div.btn_look").remove();
                }
            }
        });
    }
});
function GetOrderState(s) {
    switch (s) {
        case "0":
            return "<font color='red'>抢单中</font>";
            break;
        case "1":
//            return "<font color='red'>抢单待确认</font>";
//            break;
        case "2":
            return "<font color='red'>进行中</font>";
            break;
        case "3":
            return "<font color='green'>待评价</font>";
            break;
        case "4":
            return "<font>已完结</font>";
            break;
        case "5":
            return "<font>已取消</font>";
            break;
    }
}
function DateAdd(dtTmp, strInterval, Number) {
    switch (strInterval) {
        case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
        case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));
        case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
        case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
        case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
        case 'q': return new Date((new Date(dtTmp)).getFullYear(), ((new Date(dtTmp)).getMonth()) + Number * 3, (new Date(dtTmp)).getDate(), (new Date(dtTmp)).getHours(), (new Date(dtTmp)).getMinutes(), (new Date(dtTmp)).getSeconds());
        case 'm': return new Date((new Date(dtTmp)).getFullYear(), ((new Date(dtTmp)).getMonth()) + Number, (new Date(dtTmp)).getDate(), (new Date(dtTmp)).getHours(), (new Date(dtTmp)).getMinutes(), (new Date(dtTmp)).getSeconds());
        case 'y': return new Date(((new Date(dtTmp)).getFullYear() + Number), (new Date(dtTmp)).getMonth(), (new Date(dtTmp)).getDate(), (new Date(dtTmp)).getHours(), (new Date(dtTmp)).getMinutes(), (new Date(dtTmp)).getSeconds());        
    }
}

function better_time(strDateStart, strDateEnd) {
    var strSeparator = "-"; //日期分隔符
    var strDateArrayStart;
    var strDateArrayEnd;
    var intDay;
    strDateArrayStart = strDateStart.split(strSeparator);
    strDateArrayEnd = strDateEnd.split(strSeparator);
    var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
    var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
    intDay = (strDateE - strDateS) / (1000 * 3600 * 24);
    return intDay;
}
//---------------------------------------------------  
// 日期格式化  
// 格式 YYYY/yyyy/YY/yy 表示年份  
// MM/M 月份  
// W/w 星期  
// dd/DD/d/D 日期  
// hh/HH/h/H 时间  
// mm/m 分钟  
// ss/SS/s/S 秒  
//---------------------------------------------------  
Date.prototype.Format = function(formatStr) {
    var str = formatStr;
    var Week = ['日', '一', '二', '三', '四', '五', '六'];

    str = str.replace(/yyyy|YYYY/, this.getFullYear());
    str = str.replace(/yy|YY/, (this.getYear() % 100) > 9 ? (this.getYear() % 100).toString() : '0' + (this.getYear() % 100));

    str = str.replace(/MM/, (this.getMonth() + 1) > 9 ? (this.getMonth() + 1).toString() : '0' + (this.getMonth() + 1));
    str = str.replace(/M/g, this.getMonth() + 1);

    str = str.replace(/w|W/g, Week[this.getDay()]);

    str = str.replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : '0' + this.getDate());
    str = str.replace(/d|D/g, this.getDate());

    str = str.replace(/hh|HH/, this.getHours() > 9 ? this.getHours().toString() : '0' + this.getHours());
    str = str.replace(/h|H/g, this.getHours());
    str = str.replace(/mm/, this.getMinutes() > 9 ? this.getMinutes().toString() : '0' + this.getMinutes());
    str = str.replace(/m/g, this.getMinutes());

    str = str.replace(/ss|SS/, this.getSeconds() > 9 ? this.getSeconds().toString() : '0' + this.getSeconds());
    str = str.replace(/s|S/g, this.getSeconds());

    return str;
} 
function tofix(num) {
    var value = num;
    if (value == null || value == '') {
        return 0;
    }
    if (!isNaN(value)) {
        var re = /([0-9]+\.[0-9]{2})[0-9]*/;
        var aNew = value.toString().replace(re, "$1");
        return aNew;
    } else {
        return 0;
    }
}
function textdecode(str) {
    str = str.replace(/&amp;/gi, '&');
    str = str.replace(/&lt;/gi, '<');
    str = str.replace(/&gt;/gi, '>');
    str = str.replace(/&nbsp;/gi, ' ');
    str = str.replace(/''/gi, '"');
    str = str.replace(/<brbr>/gi, '\n');
    return str;
}
function textdecode1(str) {
    str = str.replace(/&amp;/gi, '&');
    str = str.replace(/&lt;/gi, '<');
    str = str.replace(/&gt;/gi, '>');
    str = str.replace(/&nbsp;/gi, ' ');
    str = str.replace(/''/gi, '"');
    str = str.replace(/<brbr>/gi, '<br />');
    return str;
}
function getClientHeight() //获得客户端当前浏览器的高度
{
    var ch = 0;
    if (typeof(window.innerWidth) == 'number') {
        ch = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        ch = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        ch = document.body.clientHeight;
    }
    return ch;
}
function getClientWidth() //获得客户端当前浏览器的宽度
{
    var ch = 0;
    if (typeof(window.innerWidth) == 'number') {
        ch = window.innerWidth;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        ch = document.documentElement.clientWidth;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        ch = document.body.clientWidth;
    }
    return ch;
}

//去左空格;
function ltrim(s) {
    return s.replace(/^\s*/, "");
}
//去右空格;
function rtrim(s) {
    return s.replace(/\s*$/, "");
}
//去左右空格;
function trim(s) {
    return rtrim(ltrim(s));
}  
//分页存储过程中调用的禁用各个按钮的事件
function disabledPageination() {
    if (pagesCount == 1) {
        $("#a_firstPage").prop("disabled",true);
        $("#a_prevPage").prop("disabled",true);
        $("#a_nextPage").prop("disabled",true);
        $("#a_lastPage").prop("disabled",true);
        $("#text_JumpTo").prop("disabled",true);
        $("#a_goPage").prop("disabled",true);
    } else if (currentPage == 1) {
        $("#a_firstPage").prop("disabled",true);
        $("#a_prevPage").prop("disabled",true);
        $("#a_nextPage").prop("disabled",false);
        $("#a_lastPage").prop("disabled",false);
        $("#a_goPage").prop("disabled",false);
        $("#text_JumpTo").prop("disabled",false);
    } else if (currentPage > 1 && currentPage < pagesCount) {
        $("#a_firstPage").prop("disabled",false);
        $("#a_prevPage").prop("disabled",false);
        $("#a_nextPage").prop("disabled",false);
        $("#a_lastPage").prop("disabled",false);
        $("#a_goPage").prop("disabled",false);
        $("#text_JumpTo").prop("disabled",false);
    } else if (currentPage == pagesCount) {
        $("#a_firstPage").prop("disabled",false);
        $("#a_prevPage").prop("disabled",false);
        $("#a_nextPage").prop("disabled",true);
        $("#a_lastPage").prop("disabled",true);
        $("#a_goPage").prop("disabled",false);
        $("#text_JumpTo").prop("disabled",false);
    }
} 
function LimitNumber()
{
    if(event.keyCode<47 || event.keyCode>57)//如果从46开始，带小数点
        event.returnValue=false;
}   
//乘法计算
function mul(v1,v2,fc)
{
	var a=parseFloat(v1);
	var b=parseFloat(v2);
	
	if(isNaN(a)) return parseFloat(0).toFixed(fc);
	if(isNaN(b)) return parseFloat(0).toFixed(fc);
	return (a*b).toFixed(fc);
}  
function f(o)
{
	if(isNaN(parseFloat(o))) return 0;
	else return parseFloat(o);
	return 0;
}
//弹出的提示框，并自动隐藏
var hiddenPopWin = null;
function PopupDiv(parentObj, tipStr, time, bgcolor, width, height, left, top) {
    if (time == null || time == undefined) time = 5000;

    var div_Popup = $("#div_Popup")[0];   
    if (div_Popup == null || div_Popup == undefined) {        
        div_Popup = document.createElement("div");
        div_Popup.id = "div_Popup";
    }
    if (bgcolor == null || bgcolor == undefined) bgcolor = "yellow";
    if (width == null || width == undefined) width = 240;
    if (height == null || height == undefined) height = 35;
    if (left == null || left == undefined) left = (getClientWidth() - width) / 2;
    if (top == null || top == undefined) top = (getClientHeight() - height) / 2 - 20+document.documentElement.scrollTop;

    div_Popup.style.cssText = "position:absolute;z-index:9999;cursor:hand;text-align:center;width:" + width + "px;height:" + height + "px; left:" + left + "px;top:" + top + "px; background-color:" + bgcolor + ";font-size:12px; padding-top:8px;";
    div_Popup.className = "MessageTS";
    div_Popup.innerHTML = tipStr;
    div_Popup.onmouseenter = new Function("ClearPopWin()");
    div_Popup.onmouseleave = new Function("OutPopWin()");
    div_Popup.onclick = new Function("$('#div_Popup').fadeOut();ClearPopWin()");
    parentObj.appendChild(div_Popup);
    if (time != null && time != undefined && !isNaN(time)) hiddenPopWin = setTimeout("$('#div_Popup').fadeOut();ClearPopWin()", time);
    else hiddenPopWin = setTimeout("$('#div_Popup').fadeOut();ClearPopWin();", 2000);

}  
function OutPopWin()
{
   hiddenPopWin=setTimeout("$('#div_Popup').fadeOut();ClearPopWin()",1000);
}
function  ClearPopWin()
{
  window.clearTimeout(hiddenPopWin);
} 	
function Arabia_to_Chinese(Num) {

    for (i = Num.length - 1; i >= 0; i--) {
        Num = Num.replace(",", ""); //替换tomoney()中的“,”
        Num = Num.replace(" ", ""); //替换tomoney()中的空格
    }
    Num = Num.replace("￥", ""); //替换掉可能出现的￥字符
    if (isNaN(Num)) { //验证输入的字符是否为数字
        alert("请检查小写金额是否正确");
        return;
    }
    //---字符处理完毕，开始转换，转换采用前后两部分分别转换---//
    part = String(Num).split(".");
    newchar = "";
    //小数点前进行转化
    for (i = part[0].length - 1; i >= 0; i--) {
        if (part[0].length > 10) {
            alert("位数过大，无法计算");
            return "";
        } //若数量超过拾亿单位，提示
        tmpnewchar = "";
        perchar = part[0].charAt(i);
        switch (perchar) {
        case "0":
            tmpnewchar = "零" + tmpnewchar;
            break;
        case "1":
            tmpnewchar = "壹" + tmpnewchar;
            break;
        case "2":
            tmpnewchar = "贰" + tmpnewchar;
            break;
        case "3":
            tmpnewchar = "叁" + tmpnewchar;
            break;
        case "4":
            tmpnewchar = "肆" + tmpnewchar;
            break;
        case "5":
            tmpnewchar = "伍" + tmpnewchar;
            break;
        case "6":
            tmpnewchar = "陆" + tmpnewchar;
            break;
        case "7":
            tmpnewchar = "柒" + tmpnewchar;
            break;
        case "8":
            tmpnewchar = "捌" + tmpnewchar;
            break;
        case "9":
            tmpnewchar = "玖" + tmpnewchar;
            break;
        }
        switch (part[0].length - i - 1) {
        case 0:
            tmpnewchar = tmpnewchar + "元";
            break;
        case 1:
            if (perchar != 0) tmpnewchar = tmpnewchar + "拾";
            break;
        case 2:
            if (perchar != 0) tmpnewchar = tmpnewchar + "佰";
            break;
        case 3:
            if (perchar != 0) tmpnewchar = tmpnewchar + "仟";
            break;
        case 4:
            tmpnewchar = tmpnewchar + "万";
            break;
        case 5:
            if (perchar != 0) tmpnewchar = tmpnewchar + "拾";
            break;
        case 6:
            if (perchar != 0) tmpnewchar = tmpnewchar + "佰";
            break;
        case 7:
            if (perchar != 0) tmpnewchar = tmpnewchar + "仟";
            break;
        case 8:
            tmpnewchar = tmpnewchar + "亿";
            break;
        case 9:
            tmpnewchar = tmpnewchar + "拾";
            break;
        }
        newchar = tmpnewchar + newchar;
    }
    //小数点之后进行转化
    if (Num.indexOf(".") != -1) {
        if (part[1].length > 2) {
            alert("小数点之后只能保留两位,系统将自动截段");
            part[1] = part[1].substr(0, 2);
        }
        for (i = 0; i < part[1].length; i++) {
            tmpnewchar = "";
            perchar = part[1].charAt(i);
            switch (perchar) {
            case "0":
                tmpnewchar = "零" + tmpnewchar;
                break;
            case "1":
                tmpnewchar = "壹" + tmpnewchar;
                break;
            case "2":
                tmpnewchar = "贰" + tmpnewchar;
                break;
            case "3":
                tmpnewchar = "叁" + tmpnewchar;
                break;
            case "4":
                tmpnewchar = "肆" + tmpnewchar;
                break;
            case "5":
                tmpnewchar = "伍" + tmpnewchar;
                break;
            case "6":
                tmpnewchar = "陆" + tmpnewchar;
                break;
            case "7":
                tmpnewchar = "柒" + tmpnewchar;
                break;
            case "8":
                tmpnewchar = "捌" + tmpnewchar;
                break;
            case "9":
                tmpnewchar = "玖" + tmpnewchar;
                break;
            }
            if (i == 0) tmpnewchar = tmpnewchar + "角";
            if (i == 1) tmpnewchar = tmpnewchar + "分";
            newchar = newchar + tmpnewchar;
        }
    }
    //替换所有无用汉字
    while (newchar.search("零零") != -1) newchar = newchar.replace("零零", "零");
    newchar = newchar.replace("零亿", "亿");
    newchar = newchar.replace("亿万", "亿");
    newchar = newchar.replace("零万", "万");
    newchar = newchar.replace("零元", "元");
    newchar = newchar.replace("零角", "");
    newchar = newchar.replace("零分", "");

    if (newchar.charAt(newchar.length - 1) == "元" || newchar.charAt(newchar.length - 1) == "角") newchar = newchar + "整";
    //  document.write(newchar);
    return newchar;

}
