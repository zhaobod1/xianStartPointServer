<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title><%=Session["Sys_Title"].ToS()%></title>
</head>

<frameset rows="121,*" cols="*" frameborder="0" framespacing="0">
  <frame name="top"  src="top.aspx" noresize="noresize" scrolling="no" />
  <frameset cols="20%,*">
    <frame name="left" src="left.aspx" noresize="noresize" scrolling="no" />
    <frame id="right" name="right" src="right.aspx" marginheight="0" marginwidth="0" oresize="noresize" scrolling="yes"/>
  </frameset>
</frameset>
<noframes>
</noframes>
<noframes>
</noframes>
</html>
