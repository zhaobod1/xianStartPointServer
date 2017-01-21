using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public class Wx_Pay_Model1
{
    public int retcode { get; set; }
    public string retmsg { get; set; }
    public string appid { get; set; }
    public string noncestr { get; set; }
    public string package { get; set; }
    public string partnerid { get; set; }
    public string prepayid { get; set; }
    public string timestamp { get; set; }
    public string sign { get; set; }
}
public partial class web_Default : System.Web.UI.Page
{
    public static string mchid = "1297903501"; //mchid
    public static string appId = "wx4179af8e0ea8a387";//"wx6c40eaa5885ec6c4"; //appid
    public static string appsecret = "171ed53921919b0ed4da341ca61adeb8";//"589bc94d8a9328fafc0e522cd0cbcaa5"; //appsecret
    public static string appkey = "AC65F17172B2439FB330233DB990KOIU";//"F2C1B4CF9DC54AB9A559A44F656FHUJD"; //paysignkey(非appkey 在微信商户平台设置 (md5)111111111111)  
    public static string notify_url = "http://qidiancar.com/web/wx_notify_url.aspx"; //支付完成后的回调处理页面

    protected void Page_Load(object sender, EventArgs e)
    {

        var order_pk = "D5D19E765B3347D39C1B188778983AED";//D5D19E76-5B33-47D3-9C1B-188778983AED
        order_pk = order_pk.Substring(0, 8) + "-" + order_pk.Substring(8, 4) + "-" + order_pk.Substring(12, 4) + "-" + order_pk.Substring(16, 4) + "-" + order_pk.Substring(20, 12);
        Response.Write(order_pk);
        Response.End();

        //************************************************支付参数接收********************************
        ///获取金额
        string amount = "100";
        string sp_billno = Guid.NewGuid().ToS().Replace("-", "/");
        string detail = "测试支付接口";
        double dubamount;
        double.TryParse(amount, out dubamount);
        //根据appid和appappsecret获取refresh_token
        //string url_token = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appsecret);
        //string returnStr = tokenservice.GetToken(appId, appsecret);
        //时间戳
        var timeStamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToS();
        //随机验证码
        var nonceStr = Guid.NewGuid().ToS().Replace("-", "/");

        //****************************************************************获取预支付订单编号***********************
        //设置package订单参数
        Hashtable packageParameter = new Hashtable();
        packageParameter.Add("appid", appId);//开放账号ID  
        packageParameter.Add("mch_id", mchid); //商户号
        packageParameter.Add("nonce_str", nonceStr); //随机字符串
        packageParameter.Add("body", detail); //商品描述    
        packageParameter.Add("out_trade_no", sp_billno); //商家订单号 
        packageParameter.Add("total_fee", (dubamount * 100).ToString()); //商品金额,以分为单位    
        packageParameter.Add("spbill_create_ip", "12.36.32.23"); //订单生成的机器IP，指用户浏览器端IP  
        packageParameter.Add("notify_url", notify_url); //接收财付通通知的URL  
        packageParameter.Add("trade_type", "APP");//交易类型  
        packageParameter.Add("fee_type", "CNY"); //币种，1人民币   66  
                                                 //获取签名
        var sign = CreateMd5Sign("key", appkey, packageParameter, Request.ContentEncoding.BodyName);
        //拼接上签名
        packageParameter.Add("sign", sign);
        //生成加密包的XML格式字符串
        string data = parseXML(packageParameter);
        //调用统一下单接口，获取预支付订单号码
        string prepayXml = HttpService.Post(data, "https://api.mch.weixin.qq.com/pay/unifiedorder", 30); //HttpUtil.Send(data, "https://api.mch.weixin.qq.com/pay/unifiedorder");

        //获取预支付ID
        var prepayId = string.Empty;
        var xdoc = new XmlDocument();
        xdoc.LoadXml(prepayXml);
        XmlNode xn = xdoc.SelectSingleNode("xml");
        XmlNodeList xnl = xn.ChildNodes;
        if (xnl.Count > 7)
        {
            prepayId = xnl[7].InnerText;
        }

        //**************************************************封装调起微信客户端支付界面字符串********************
        //设置待加密支付参数并加密
        Hashtable paySignReqHandler = new Hashtable();
        paySignReqHandler.Add("appid", appId);
        paySignReqHandler.Add("partnerid", mchid);
        paySignReqHandler.Add("prepayid", prepayId);
        paySignReqHandler.Add("package", "Sign=WXPay");
        paySignReqHandler.Add("noncestr", nonceStr);
        paySignReqHandler.Add("timestamp", timeStamp);
        var paySign = CreateMd5Sign("key", appkey, paySignReqHandler, Request.ContentEncoding.BodyName);

        //设置支付包参数
        //Wx_Pay_Model1 wxpaymodel = new Wx_Pay_Model1();
        //wxpaymodel.retcode = 0;//5+固定调起参数
        //wxpaymodel.retmsg = "ok";//5+固定调起参数
        //wxpaymodel.appid = appId;//AppId,微信开放平台新建应用时产生
        //wxpaymodel.partnerid = mchid;//商户编号，微信开放平台申请微信支付时产生
        //wxpaymodel.prepayid = prepayId;//由上面获取预支付流程获取
        //wxpaymodel.package = "Sign=WXpay";//APP支付固定设置参数
        //wxpaymodel.noncestr = nonceStr;//随机字符串，
        //wxpaymodel.timestamp = timeStamp;//时间戳
        //wxpaymodel.sign = paySign;//上面关键参数加密获得
        //                          //将参数对象直接返回给客户端

        string restr = "{\"return_code\":\"OK\",";
        restr += "\"retcode\":\"0\",";
        restr += "\"retmsg\":\"ok\",";
        restr += "\"appid\":\"" + appId + "\",";
        restr += "\"partnerid\":\"" + mchid + "\",";
        restr += "\"prepayid\":\"" + prepayId + "\",";
        restr += "\"package\":\"Sign =WXpay\",";
        restr += "\"noncestr\":\"" + nonceStr + "\",";
        restr += "\"timestamp\":\"" + timeStamp + "\",";
        restr += "\"sign\":\"" + paySign + "\"";
        restr += "}";

        Response.Write(restr);
    }

    /// <summary>
    /// 将类对象拼接成调起支付字符串
    /// </summary>
    /// <param name="_model"></param>
    /// <returns></returns>
    private string ReSetPayString(Wx_Pay_Model1 _model)
    {
        StringBuilder strpay = new StringBuilder();
        PropertyInfo[] props = _model.GetType().GetProperties();
        strpay.Append("{");
        foreach (PropertyInfo property in props)
        {
            strpay.Append(property.Name + ":\"" + property.GetValue(_model, null).ToString() + "\",");
        }
        strpay.Remove(strpay.Length - 1, 1);
        strpay.Append("}");
        return strpay.ToString();
    }
    /// <summary>
    /// 输出XML
    /// </summary>
    /// <returns></returns>
    public string parseXML(Hashtable _parameters)
    {
        var sb = new StringBuilder();
        sb.Append("<xml>");
        var akeys = new ArrayList(_parameters.Keys);
        foreach (string k in akeys)
        {
            var v = (string)_parameters[k];
            if (Regex.IsMatch(v, @"^[0-9.]$"))
            {
                sb.Append("<" + k + ">" + v + "</" + k + ">");
            }
            else
            {
                sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
            }
        }
        sb.Append("</xml>");
        return sb.ToString();
    }
    /// <summary>
    /// 创建package签名
    /// </summary>
    /// <param name="key">密钥键</param>
    /// <param name="value">财付通商户密钥（自定义32位密钥）</param>
    /// <returns></returns>
    public virtual string CreateMd5Sign(string key, string value, Hashtable parameters, string _ContentEncoding)
    {
        var sb = new StringBuilder();
        //数组化键值对，并排序
        var akeys = new ArrayList(parameters.Keys);
        akeys.Sort();
        //循环拼接包参数
        foreach (string k in akeys)
        {
            var v = (string)parameters[k];
            if (null != v && "".CompareTo(v) != 0
                && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
            {
                sb.Append(k + "=" + v + "&");
            }
        }
        //最后拼接商户自定义密钥
        sb.Append(key + "=" + value);
        //加密
        string sign = GetMD5(sb.ToString()).ToUpper();
        //返回密文
        return sign;
    }
    private string GetMD5(string src)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] data = Encoding.UTF8.GetBytes(src);
        byte[] md5data = md5.ComputeHash(data);
        md5.Clear();
        var retStr = BitConverter.ToString(md5data);
        retStr = retStr.Replace("-", "").ToUpper();
        return retStr;
    }
}