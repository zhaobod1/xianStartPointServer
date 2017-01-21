<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reg.aspx.cs" Inherits="_reg" %>

<!DOCTYPE html>
<html class="ui-page-login">

<head>
   <meta charset="utf-8">
		<meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no">
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		<title>����ר�����</title>
		<link href="css/mui.min.css" rel="stylesheet" />
	
    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>

    <script src="js/layer/layer.js" type="text/javascript"></script>
    <style>
        .ui-page-login {
    width: 100%;
	height: 100%;
	margin: 0px;
	padding: 0px;
	background:url(images/wa.png) center no-repeat #99D4FE; 
	background-size: 100% 100%;
    background-origin: content-box;
    
}
body {
	width: 100%;
	height: 100%;
	margin: 0px;
	padding: 0px;
	background-size: 100% 100%;
    background-origin: content-box;
   
}
.mui-content{height: 100%; background: none;}

        .warp {
            margin-top:40%;
        }

        .Transparency {
            opacity: 0.8;
        }


        .mui-input-group:first-child {
            margin-top: 20px;
        }

        .mui-input-group label {
            width: 26%;
            text-align: right;
        }

        .mui-input-row label ~ input,
        .mui-input-row label ~ select,
        .mui-input-row label ~ textarea {
            width: 74%;
        }

        .mui-checkbox input[type=checkbox],
        .mui-radio input[type=radio] {
            top: 6px;
        }

        .mui-content-padded {
            margin-top: 25px;
        }

        .mui-btn {
            padding: 10px;
        }

        .mui-col-xs-6 button {
            width: 96%;
            margin: 0 auto;
        }
      
    </style>
    <script type="text/javascript">      
        var ajax = false;
        $(function() {           
            $("#reg").click(function () {
                if ($("#phone").val() == "") {
                    layer.msg("�ֻ��Ų���Ϊ�գ�"); return false;
                }
                if (!$("#phone").val().match(/^(((13[0-9]{1})|147|145|159|158|157|156|155|153|152|151|150|178|177|176|189|188|187|186|185|184|183|182|181|180)+\d{8})$/)) {
                    layer.msg("�ֻ������ʽ����ȷ��");
                    return false;
                }
               
                if (ajax) return;
                ajax = true;
                $.ajax({
                    url:'info/getdata.aspx?com=reg',
                    data: {
                        phone: $("#phone").val()                       
                    },
                    type: "post",
                    dataType: "json",
                    error: function (err) {
                        ajax = false;
                        layer.msg("�������");
                    },
                    success: function (da) {
                        ajax = false;
                        if (da != null) {
                            if (da.result == "1") {
                                layer.msg("��ȡ�ɹ�");
                                location.href = "/down.aspx";
                                return;
                            }
                            else {
                                layer.msg(da.result);
                                return;
                            }
                        }                                      
                    }
                });
            });
        });
    </script>
</head>
<body>
    <div class="mui-content">
        <div class="warp Transparency">
            <div class="mui-card area Transparency">
                <form class="mui-input-group">
                    <div class="mui-input-row">
                        <label>��&nbsp;&nbsp;&nbsp;&nbsp;��</label>
                        <input id='phone' type="text" class="mui-input-clear mui-input" placeholder="�������ֻ���">
                    </div>
       

                </form>
                <div class="mui-content-padded">
                    <div class="mui-col-xs-10" style=" margin:0 auto;">
                        <button id='reg' class="mui-btn mui-btn-block mui-btn-primary">��ȡ���</button>
                    </div>                    
                </div>

                <div class="mui-content-padded" style=" text-align:center; padding:10% 0;">
                    <a href="down.aspx" style=" text-decoration:underline;">�������ר��APP</a>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
