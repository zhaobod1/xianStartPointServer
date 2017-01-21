/**
* Styleswitch stylesheet switcher built on jQuery
* Under an Attribution, Share Alike License
* By Kelvin Luck ( http://www.kelvinluck.com/ )
**/

$(document).ready(function() {
	$('.styleswitch').click(function()
	{		
		var r=this.getAttribute("rel");
		switchStylestyle(r);						
		 if($("#intab_tableData").length>0)
		 {
			 $("#intab_tableData").jscroll({
			   W:"15px"
			  ,BgUrl:"url(../images/" + r + ".gif)"
			  ,Bg:"right 0 repeat-y"
			  ,Bar:{Bd:{Out:"#a3c3d5",Hover:"#b7d5e6"}
					,Bg:{Out:"-45px 0 repeat-y",Hover:"-45px 0 repeat-y",Focus:"-45px 0 repeat-y"}}
					,Btn:{btn:true
						  ,uBg:{Out:"0 0",Hover:"-15px 0",Focus:"-30px 0"}
						  ,dBg:{Out:"0 -15px",Hover:"-15px -15px",Focus:"-30px -15px"}}
			  ,Fn:function(){}
			 });	
		 }
		return false;
	});
	var c = readCookie('style');
	if (c) switchStylestyle(c);
});

function switchStylestyle(styleName)
{
	$("#Systheme").attr("href","/css/" + styleName + ".css");
	changestyle(parent,styleName);
	createCookie('style', styleName, 365);
}
function changestyle(obj,styleName)
{
	for(var i=0;i<obj.frames.length;i++)
	{	
		try
		{
			obj.frames[i].document.getElementById("Systheme").href="/css/" + styleName + ".css";
			if(obj.frames[i].frames.length>0) changestyle(obj.frames[i],styleName);
		}
		catch(e)
		{}
		
	}
}
function createCookie(name,value,days)
{
	if (days)
	{
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
}
function readCookie(name)
{
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++)
	{
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}
function eraseCookie(name)
{
	createCookie(name,"",-1);
}
// /cookie functions