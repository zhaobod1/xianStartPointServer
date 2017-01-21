var _move = false;
var _x, _y;
$(document).ready(function() {
    $(".tit").mousedown(function(e) {
        $(".tit").removeClass("current_element");
        $(this).addClass("current_element");
        _move = true;
        _x = e.pageX - parseInt($(this).parent().css("left"));
        _y = e.pageY - parseInt($(this).parent().css("top"));
    });

    $(document).mousemove(function(e) {
        if (_move) {
            var x = e.pageX - _x;
            var y = e.pageY - _y;
            $(".current_element").parent().css({
                top: y,
                left: x
            });
        }
    }).mouseup(function() {
		$(".tit").removeClass("current_element");
        _move = false;
    });
});