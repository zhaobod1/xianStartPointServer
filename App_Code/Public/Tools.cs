using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using PostMsg_Net;
using PostMsg_Net.common;
using System.Diagnostics;
/// <summary>
///Tools 工具类
///by xel 2011-05-30
/// </summary>
public class Tools
{
	public Tools()
	{
    }
    public static int SendSMS1(string phone,string content)
    {
        DataSet ds=DbHelperSQL.Query("select * from t_system");        
        PostMsg postMsg = new PostMsg();
        postMsg.SetUser(ds.Tables[0].Rows[0]["sms_user"].ToS(), ds.Tables[0].Rows[0]["sms_pass"].ToS());
        postMsg.SetGateWay(ds.Tables[0].Rows[0]["sms_send_url"].ToS(), ds.Tables[0].Rows[0]["sms_succ_code"].ToInt32(), LinkType.SHORTLINK);
        MessageData[] Messagedatas = new MessageData[1];
        Messagedatas[0] = new MessageData();
        Messagedatas[0].Content = content;
        Messagedatas[0].Phone = phone;
        return postMsg.Post(postMsg.GetAccount(), Messagedatas, "0001");

    }
    public static int SendSMS(string phone, string content)
    {
        DataSet ds = DbHelperSQL.Query("select * from t_system");
        string sms_user = ds.Tables[0].Rows[0]["sms_user"].ToS();
        string sms_pass = ds.Tables[0].Rows[0]["sms_pass"].ToS();
        string sms_send_url = ds.Tables[0].Rows[0]["sms_send_url"].ToS();
      
        //huo15com
        WebClient client = new WebClient();
        string url = "http://121.40.119.148/api.php";
        url +=  "?mobile=" + phone + "&content=" + System.Web.HttpUtility.UrlEncode(content, Encoding.GetEncoding("GB2312")) + "&key=" + sms_user +"&bianma=gbk";
        Stream data = client.OpenRead(url);
        StreamReader reader = new StreamReader(data, Encoding.Default);
        string s = reader.ReadToEnd();
        s = s.Trim().Substring(0, 1);
        data.Close();
        reader.Close();
        return s.ToInt32();

    }
    public static bool QRCode(string qrdata, string savepath)
    {
        try
        {
            ThoughtWorks.QRCode.Codec.QRCodeEncoder encoder = new ThoughtWorks.QRCode.Codec.QRCodeEncoder();
            encoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
            encoder.QRCodeScale = 8;//大小(值越大生成的二维码图片像素越高)
            encoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
            encoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;//错误效验、错误更正(有4个等级)    
            System.Drawing.Bitmap image = encoder.Encode(qrdata.ToString(), Encoding.GetEncoding("GB2312"));
            image.Save(savepath);
            image.Dispose(); //释放资源  
            return true;
        }
        catch (Exception)
        {

            return false;
        }


    }
    public static bool StartProcess(string filename, string[] args)
    {
        try
        {
            string s = "";
            foreach (string arg in args)
            {
                s = s + arg + " ";
            }
            s = s.Trim();
            Process myprocess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(filename, s);
            myprocess.StartInfo = startInfo;

            //通过以下参数可以控制exe的启动方式，具体参照 myprocess.StartInfo.下面的参数，如以无界面方式启动exe等
            myprocess.StartInfo.UseShellExecute = false;
            myprocess.Start();
            return true;
        }
        catch (Exception ex)
        {

        }
        return false;
    }
    #region 发送电子邮件
    /// <summary>
    /// 发送电子邮件
    /// </summary>
    /// <param name="mailServerName">用于SMTP事务的主机的名称或IP地址</param>
    /// <param name="mailFrom">发送凭证用户名</param>
    /// <param name="mailFromName">与发送电子邮件地址关联的显示名</param>
    /// <param name="mailFromPwd">发送凭证用户名的密码</param>
    /// <param name="mailTo">电子邮件收件人的地址</param>
    /// <param name="subject">电子邮件主题</param>
    /// <param name="body">电子邮件正文</param>
    /// <returns>返回邮件发送状态：True 成功；False 失败；</returns>
    public static bool SendEmail(string mailServerName, string mailFrom, string mailFromName, string mailFromPwd, string mailTo, string subject, string body)
    {
        try
        {
            using (MailMessage message = new MailMessage(mailFrom, mailTo))
            {

                message.From = new MailAddress(mailFrom, mailFromName);

                message.Subject = subject;
                message.Body = body;
                //SmtpClient是发送邮件的主体，这个构造函数是告知SmtpClient发送邮件时使用哪个SMTP服务器
                SmtpClient mailClient = new SmtpClient(mailServerName);
                //将认证实例赋予mailClient,也就是访问SMTP服务器的用户名和密码
                mailClient.Credentials = new NetworkCredential(mailFrom, mailFromPwd);
                //最终的发送方法
                mailClient.Send(message);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
    #endregion

    #region 产生随机汉字
    /// <summary>
    /// 产生随机汉字
    /// </summary>
    /// <param name="CharCount">要产生随机汉字的个数</param>
    /// <returns>返回随机产生的汉字</returns>
    public static string GetChar(int CharCount)
    {
        //获取GB2312编码页（表） 
        Encoding gb = Encoding.GetEncoding("gb2312");

        //调用函数产生4个随机中文汉字编码 
        object[] bytes = CreateRegionCode(CharCount);

        //根据汉字编码的字节数组解码出中文汉字 
        string str1 = gb.GetString((byte[])Convert.ChangeType(bytes[0], typeof(byte[])));
        string str2 = gb.GetString((byte[])Convert.ChangeType(bytes[1], typeof(byte[])));
        string str3 = gb.GetString((byte[])Convert.ChangeType(bytes[2], typeof(byte[])));
        string str4 = gb.GetString((byte[])Convert.ChangeType(bytes[3], typeof(byte[])));

        //输出的控制台 
        return str1 + str2 + str3 + str4;
    }
    public static object[] CreateRegionCode(int strlength)
    {
        //定义一个字符串数组储存汉字编码的组成元素 
        string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

        Random rnd = new Random();

        //定义一个object数组用来 
        object[] bytes = new object[strlength];

        /**/
        /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中 
        每个汉字有四个区位码组成 
        区位码第1位和区位码第2位作为字节数组第一个元素 
        区位码第3位和区位码第4位作为字节数组第二个元素 
        */
        for (int i = 0; i < strlength; i++)
        {
            //区位码第1位 
            int r1 = rnd.Next(11, 14);
            string str_r1 = rBase[r1].Trim();

            //区位码第2位 
            rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的 种子避免产生重复值 
            int r2;
            if (r1 == 13)
            {
                r2 = rnd.Next(0, 7);
            }
            else
            {
                r2 = rnd.Next(0, 16);
            }
            string str_r2 = rBase[r2].Trim();

            //区位码第3位 
            rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
            int r3 = rnd.Next(10, 16);
            string str_r3 = rBase[r3].Trim();

            //区位码第4位 
            rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
            int r4;
            if (r3 == 10)
            {
                r4 = rnd.Next(1, 16);
            }
            else if (r3 == 15)
            {
                r4 = rnd.Next(0, 15);
            }
            else
            {
                r4 = rnd.Next(0, 16);
            }
            string str_r4 = rBase[r4].Trim();

            //定义两个字节变量存储产生的随机汉字区位码 
            byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
            byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
            //将两个字节变量存储在字节数组中 
            byte[] str_r = new byte[] { byte1, byte2 };

            //将产生的一个汉字的字节数组放入object数组中 
            bytes.SetValue(str_r, i);

        }

        return bytes;

    }

    #endregion

    #region MD5加密
    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="strChar">要加密的字符串</param>
    /// <returns>返回加密后的字符串</returns>
    public static string MD5(string strChar)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(strChar, "md5");
    }
    #endregion

    #region 网站与子系统传值加密

    /// <summary>
    ///不输入Key,使用默认key(XAHYKJ029)解密
    /// </summary>
    /// <param name="str">要解密的字符串</param>
    /// <returns></returns>
    public static string Decode(string str)
    {
        return DecryptString(str, "XAHYKJ02");
    }

    /// <summary>
    /// Des 解密字符串 UTF8 
    /// </summary>
    /// <param name="sInputString">要解密的字符串</param>
    /// <param name="sKey">解密使用的key</param>
    /// <returns></returns>
    public static string DecryptString(string sInputString, string sKey)
    {
        string[] textArray = sInputString.Split("-".ToCharArray());
        byte[] inputBuffer = new byte[textArray.Length];
        for (int i = 0; i < textArray.Length; i++)
        {
            inputBuffer[i] = byte.Parse(textArray[i], NumberStyles.HexNumber);
        }
        DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
        provider.Key = Encoding.ASCII.GetBytes(sKey);
        provider.IV = Encoding.ASCII.GetBytes(sKey);
        byte[] bytes = provider.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
        return Encoding.UTF8.GetString(bytes);
    }
    /// <summary>
    /// 不输入Key,使用默认key(XAHYKJ029)加密
    /// </summary>
    /// <param name="str">要加密的字符串</param>
    /// <returns></returns>
    public static string Encode(string str)
    {
        return EncryptString(str, "XAHYKJ02");
    }

    /// <summary>
    /// Des 加密 ,可以处理汉字 UTF8 
    /// </summary>
    /// <param name="sInputString">要加密的字符串</param>
    /// <param name="sKey">加密使用的key</param>
    /// <returns></returns>
    public static string EncryptString(string sInputString, string sKey)
    {
        byte[] inputBuffer = Encoding.UTF8.GetBytes(sInputString);
        DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
        provider.Key = Encoding.ASCII.GetBytes(sKey);
        provider.IV = Encoding.ASCII.GetBytes(sKey);
        return BitConverter.ToString(provider.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
    }

    #endregion

    #region 字母大小写转换
    /// <summary>
    /// 字母大小写转换
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="str">指示大写->小写或小写->大写;0 大写->小写,1 小写->大写</param>
    /// <returns>返回转换后的字符串</returns>
    public static string GetM(string str,int strIndex)
    {
        int i;
        string[] D = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string[] X = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        for (i = 0; i < X.Length; i++)
        {
            if(strIndex==0) str=str.Replace(D[i], X[i]);
            else str = str.Replace(X[i], D[i]);
        }     
        return str;
    }
    #endregion

    #region 过滤特殊字符
    /// <summary>
    /// 过滤标记
    /// </summary>
    /// <param name="NoHTML">包括HTML，脚本，数据库关键字，特殊字符的源码 </param>
    /// <returns>已经去除标记后的文字</returns>
    public static string NoHtml(string Htmlstring)
    {
        if (Htmlstring == null)
        {
            return "";
        }
        else
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);

            //删除与数据库相关的词
            Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "count''", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net user", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "or", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "script", "", RegexOptions.IgnoreCase);

            //特殊的字符
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("*", "");
            Htmlstring = Htmlstring.Replace("?", "");
            Htmlstring = Htmlstring.Replace("'", "''");
            Htmlstring = Htmlstring.Replace(",", "");
            Htmlstring = Htmlstring.Replace("/", "");
            Htmlstring = Htmlstring.Replace(";", "");
            Htmlstring = Htmlstring.Replace("*/", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }
    }
    #endregion

    #region 过滤HTML代码
    /// <summary>
    /// 过滤HTML代码
    /// </summary>
    /// <param name="NoHTML">包括HTML，脚本，数据库关键字，特殊字符的源码 </param>
    /// <returns>已经去除标记后的文字</returns>
    public static string checkStr(string html)
    {
        System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        html = regex1.Replace(html, ""); //过滤<script></script>标记 
        html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
        html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
        html = regex4.Replace(html, ""); //过滤iframe 
        html = regex5.Replace(html, ""); //过滤frameset 
        html = regex6.Replace(html, ""); //过滤frameset 
        html = regex7.Replace(html, ""); //过滤frameset 
        html = regex8.Replace(html, ""); //过滤frameset 
        html = regex9.Replace(html, "");
        html = html.Replace(" ", "");
        html = html.Replace("</strong>", "");
        html = html.Replace("<strong>", "");
        return html;
    }
    #endregion

    
    public static string sub_str(string str, int len)
    {
        str = Tools.NoHtml(str);
        str = str.Length > len ? str.Substring(0, len) + "..." : str;
        return str;
    }

    public static void addlog(string UserPK, string IP, string IP1, string v)
    {
        DbHelperSQL.ExecuteSql("INSERT INTO [t_log]([log_pk]  ,[log_title]  ,[user_pk]  ,[log_ip] ,[log_ip1] ,[create_time]) VALUES(newid(),'" + v + "','" + UserPK + "','" + IP + "','" + IP1 + "',getdate())");
    }
}
