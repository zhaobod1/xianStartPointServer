using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// WxPayCore 的摘要说明
/// </summary>
public static class WxPayCore
{ 
    /// <summary>
    /// 输出XML
    /// </summary>
    /// <returns></returns>
    public static string parseXML(Hashtable _parameters)
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
    public static string CreateMd5Sign(string key, string value, Hashtable parameters)
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
        string sign = MD5(sb.ToString()).ToUpper();
        //返回密文
        return sign;
    }
    public static string MD5(string encypStr )
    {
        string charset = "UTF-8";
        string retStr;
        MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

        //创建md5对象
        byte[] inputBye;
        byte[] outputBye;

        //使用GB2312编码方式把字符串转化为字节数组．
        try
        {
            inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
        }
        catch
        {
            inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
        }
        outputBye = m5.ComputeHash(inputBye);

        retStr = System.BitConverter.ToString(outputBye);
        retStr = retStr.Replace("-", "").ToUpper();
        return retStr;
    }
}