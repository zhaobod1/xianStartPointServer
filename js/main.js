/*

 * Image preview script

 * powered by jQuery (http://www.jquery.com)

 *

 * written by Alen Grakalic (http://cssglobe.com)

 *

 * for more info visit http://cssglobe.com/post/1695/easiest-tooltip-and-image-preview-using-jquery

 *

 */


this.imagePreview = function() {

    /* CONFIG */


    xOffset = 10;

    yOffset = 30;


    // these 2 variable determine popup's distance from the cursor

    // you might want to adjust to get the right result


    /* END CONFIG */

    $("#fj img,#pmt img,#xgt img,#zl img").hover(function(e) {
        this.t = this.title;

        this.title = "";

        var c = (this.t != "") ? "<br/>" + this.t : "";

        $("body").append("<p id='preview'><img src='" + $(this).attr("src") + "' alt='Image preview' />" + c + "</p>");
        var top = 0;
        var left = 0;
        var zh = getClientHeight();
        var zw = getClientWidth();

        $("#preview img").css("max-width", (zw * 90 / 100) + "px").css("max-height", (zh * 90 / 100) + "px");
        
              
        if (zw > $("#preview").width()) {
            left = (getClientWidth() - $("#preview").width()) / 2;
        }
        if (zh > $("#preview").height()) {
            top = (getClientHeight() - $("#preview").height()) / 2;
        }
        

        $("#preview")

            .css("top", top + "px")

            .css("left", left + "px")

            .fadeIn("fast");
     
        $("#preview").click(function() {
            $("#preview").remove();
        });

    },

    function() {
        this.title = this.t;

        //        $("#preview").remove();

    });

    $("#fj img,#pmt img,#xgt img").mousemove(function(e) {
        var top = 0;
        var left = 0;
        var zh = getClientHeight();
        var zw = getClientWidth();
        $("#preview img")

            .css("max-width", (zw * 90 / 100) + "px")

            .css("max-height", (zh * 90 / 100) + "px");
     
        if (zw > $("#preview").width()) {
            left = (getClientWidth() - $("#preview").width()) / 2;
        }
        if (zh > $("#preview").height()) {
            top = (getClientHeight() - $("#preview").height()) / 2;
        }
        $("#preview")

            .css("top", top + "px")

            .css("left", left + "px");
            
       
    });

};


