
        /************************************ dragedTableData.js *******************************/
        /*一个对表格进行拖拽来交换单元格数据的脚本
        * created by LxcJie 2004.4.12
        * 可以实现表格内容的内部拖动
        * 确保中间过度层的存在，id为指定
        * 修改者 邓华锋(博客：http://www.denghuafeng.com)
        * 功能实现：兼容火狐
        * 解决问题：
        * 1.event的获取
        * 2.event的event.srcElement ? event.srcElement: event.target;
        * 3.Table.cells不兼容火狐 改用遍历rows再遍历cells
        * 4.event的横纵标和纵坐标的获取。var x=event.x||event.pageX; var y=event.y||event.pageY;
        * 5.document.all改成document.getElementById
        */
        /*--------全局变量-----------*/
        var dragedTable_x0, dragedTable_y0, dragedTable_x1, dragedTable_y1;
        var dragedTable_movable = false;
        var dragedTable_preCell = null;
        var dragedTable_normalColor = null;
        //起始单元格的颜色
        var dragedTable_preColor = "lavender";
        //目标单元格的颜色
        var dragedTable_endColor = "#E1FBE7";
        var dragedTable_movedDiv = "dragedTable_movedDiv";
        var dragedTable_tableId = "";
        /*--------全局变量-----------*/
        function DragedTable(tableId) {
            dragedTable_tableId = tableId;
            var oTempDiv = document.createElement("div");
            oTempDiv.className = "sel_div";
            oTempDiv.id = dragedTable_movedDiv;
            oTempDiv.onselectstart = function() {
                return false
            };
            oTempDiv.style.cursor = "hand";
            oTempDiv.style.position = "absolute";
            oTempDiv.style.border = "1px dotted black";
            oTempDiv.style.backgroundColor = dragedTable_endColor;
            oTempDiv.style.display = "none";
            document.body.appendChild(oTempDiv);
            $("td").mousedown(function(e) {
                if ($(this).attr("jie") == "1") return;
                showDiv(e);
            }); 
        }
        function getEvent() {
            if (document.all) {
                return window.event; //如果是ie
            }
            func = getEvent.caller;
            while (func != null) {
                var arg0 = func.arguments[0];
                if (arg0) {
                    if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                        return arg0;
                    }
                }
                func = func.caller;
            }
            return null;
        }
        //得到控件的绝对位置
        function getPos(cell) {
            var pos = new Array();
            var t = cell.offsetTop;
            var l = cell.offsetLeft;
            while (cell = cell.offsetParent) {
                t += cell.offsetTop;
                l += cell.offsetLeft;
            }
            pos[0] = t;
            pos[1] = l;
            return pos;
        }
        //显示图层
        function showDiv() {
            var event = getEvent();
            var obj = event.srcElement ? event.srcElement : event.target;
            var pos = new Array();
            //获取过度图层
            var oDiv = document.getElementById(dragedTable_movedDiv);
            if (obj.tagName.toLowerCase() == "td") {
                obj.style.cursor = "hand";
                pos = getPos(obj);
                //计算中间过度层位置，赋值
                oDiv.style.width = obj.offsetWidth+"px";
                oDiv.style.height = obj.offsetHeight+"px";
                oDiv.style.top = pos[0]+"px";
                oDiv.style.left = pos[1]+"px";
                oDiv.innerHTML = obj.innerHTML;
                oDiv.style.display = "";
                dragedTable_x0 = pos[1];
                dragedTable_y0 = pos[0];
                dragedTable_x1 = event.clientX;
                dragedTable_y1 = event.clientY;
                //记住原td
                dragedTable_normalColor = obj.style.backgroundColor;
                obj.style.backgroundColor = dragedTable_preColor;                
                dragedTable_preCell = obj;
                dragedTable_movable = true;
            }
        }
        function dragDiv() {
            if (dragedTable_movable) {
                var event = getEvent();
                var oDiv = document.getElementById(dragedTable_movedDiv);
                var pos = new Array();
                var x = event.x || event.pageX;
                var y = event.y || event.pageY;
                var posX = 0;
                var posY = 0;
                var td;
                var oTable = document.getElementById(dragedTable_tableId);
                var rowslength = oTable.rows.length;
                var cellslength = 0;
                oDiv.style.top = (event.clientY - dragedTable_y1 + dragedTable_y0)+"px";
                oDiv.style.left = (event.clientX - dragedTable_x1 + dragedTable_x0)+"px";
                for (var j = 1; j < rowslength; j++) {
                    cellslength = oTable.rows[j].cells.length;
                    for (var i = 2; i < cellslength; i++) {
                        td = oTable.rows[j].cells[i];
                        if (td.tagName.toLowerCase() == "td") {
                            pos = getPos(td);
                            posX = pos[1];
                            posY = pos[0];
                            if (x > posX && x < posX + td.offsetWidth && y > posY && y < posY + td.offsetHeight) {
                                if (td != dragedTable_preCell) td.style.backgroundColor = dragedTable_endColor;
                            } else {
                                if (td != dragedTable_preCell) td.style.backgroundColor = dragedTable_normalColor;
                            }
                        }
                    }
                }
            }
        }
        function hideDiv() {
            if (dragedTable_movable) {
                var event = getEvent();
                var x = event.x || event.pageX;
                var y = event.y || event.pageY;
                var oTable = document.getElementById(dragedTable_tableId);
                var pos = new Array();
                var posX = 0;
                var posY = 0;
                var td;
                var rowslength = oTable.rows.length;
                var cellslength = 0;
                if (dragedTable_preCell != null) {
                    for (var j = 1; j < rowslength; j++) {
                        cellslength = oTable.rows[j].cells.length;
                        for (var i = 2; i < cellslength; i++) {
                            td = oTable.rows[j].cells[i];
                            pos = getPos(td);
                            posX = pos[1];
                            posY = pos[0];
                            //计算鼠标位置，是否在某个单元格的范围之内
                            if (x > posX && x < posX + td.offsetWidth && y > posY && y < posY + td.offsetHeight) {
                                if (oTable.rows[j].cells[i].tagName.toLowerCase() == "td") {
                                    //交换文本                                   
                                    dragedTable_preCell.innerHTML = td.innerHTML;
                                    td.innerHTML = document.getElementById(dragedTable_movedDiv).innerHTML;
                                    //清除原单元格和目标单元格的样式
                                    dragedTable_preCell.style.backgroundColor = dragedTable_normalColor;
                                    td.style.backgroundColor = dragedTable_normalColor;
                                    td.style.cursor = "";
                                    dragedTable_preCell.style.cursor = "";
                                    dragedTable_preCell.style.backgroundColor = dragedTable_normalColor;
                                }
                            }
                        }
                    }
                }
                dragedTable_movable = false;
                //清除提示图层
                document.getElementById(dragedTable_movedDiv).style.display = "none";
            }
        }
        document.onmouseup = function() {
            hideDiv();
            var oTable = document.getElementById(dragedTable_tableId);
            for (var j = 1; j < oTable.rows.length; j++) {
                for (var i = 2; i < oTable.rows[j].cells.length; i++) oTable.rows[j].cells[i].style.backgroundColor = dragedTable_normalColor;
            }
        }
        document.onmousemove = function() {
            dragDiv();
        }
        /************************************ dragedTableData.js 结束 *******************************/
