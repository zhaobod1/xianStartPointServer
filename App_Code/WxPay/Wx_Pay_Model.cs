public class Wx_Pay_Model
{
    public static string mchid = "1297903501"; //mchid
    public static string appId = "wx4179af8e0ea8a387";//"wx6c40eaa5885ec6c4"; //appid
    public static string appsecret = "171ed53921919b0ed4da341ca61adeb8";//"589bc94d8a9328fafc0e522cd0cbcaa5"; //appsecret
    public static string appkey = "AC65F17172B2439FB330233DB990KOIU";//"F2C1B4CF9DC54AB9A559A44F656FHUJD"; //paysignkey(非appkey 在微信商户平台设置 (md5)111111111111)  
    public static string notify_url = "http://qidiancar.com/web/wx_notify_url.aspx"; //支付完成后的回调处理页面
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
