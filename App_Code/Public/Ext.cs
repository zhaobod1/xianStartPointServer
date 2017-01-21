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

/// <summary>
///Ext 方法重写类
///by xel 2011-05-30
/// </summary>
public static class Ext
{
    #region 对象是否为null 或 Empty
    /// <summary>
    /// 对象是否为null
    /// </summary>
    public static bool IsNull(this object o) { return o == null; }
    public static bool IsNullOrEmpty(this string s) { return string.IsNullOrEmpty(s); }
    #endregion

    #region 重写Convert.ToInt32
    /// <summary>
    /// ToInt32
    /// </summary>
    public static int ToInt32(this object o)
    {
        try
        {
            return Convert.ToInt32(o);
        }
        catch (Exception)
        { return 0; }
    }
    #endregion

    #region 重写Convert.ToDateTime
    /// <summary>
    /// ToInt32
    /// </summary>
    public static DateTime ToDateTime(this object o)
    {
        try
        {
            return Convert.ToDateTime(o);
        }
        catch (Exception)
        { return DateTime.MinValue; }
    }
    #endregion
   

    #region 判断是否null或Empty

    public static bool IsNullOrEmpty(this object o)
    {
        if (o.IsNull())
            return true;
        else if (o is string)
            return o.ToS() == "";
        else
            return false;
    }
    #endregion

    #region 重写ToString方法,为null时返回 ""
    /// <summary>
    /// ToString，为null时返回 ""
    /// </summary>
    public static string ToS(this object o) { return (o ?? "").ToString(); }
    #endregion 

    #region 重写ToDouble方法,为null时返回0 
    /// <summary>
    /// ToString，为null时返回 ""
    /// </summary>
    public static double ToD(this object o)
    {
        try
        {
            return Convert.ToDouble(o);
        }
        catch (Exception)
        {
            return 0;
        }
    }
    #endregion 

    #region 重写ToString方法,为null时返回指定值
    /// <summary>
    /// ToString
    /// </summary>
    /// <param name="o"></param>
    /// <param name="s">this对象为 null 或 "" 时的返回值</param>
    /// <returns></returns>
    public static string ToS(this object o, string s)
    {
        if (o.IsNullOrEmpty() == false)
            return o.ToS();
        else
            return s;
    }
    #endregion

    #region 重写int型的ToString方法
    /// <summary>
    /// ToString 如：2.00，58.89   保留指定位小数
    /// </summary>
    /// <param name="n">保留几位小数</param>
    public static string ToS(this int i, int n) { return i.ToString(string.Concat("F", n.ToS())); }
    #endregion

    #region 返回指定字符在数组中的索引
    /// <summary>
    /// 返回指定字符在数组中的索引
    /// </summary>
    /// <param name="objs">要查找的数组</param>
    /// <param name="findObj">查找什么</param>
    /// <returns>查找到的位置</returns>
    public static int Find(this object[] objs, object findObj)
    {
        if (objs == null || findObj == null)
            return -1;
        else
            return Array.IndexOf(objs, findObj);
    }
    #endregion

    #region 重写Convent.ToBoolean()
    /// <summary>
    /// ToBoolean
    /// </summary>
    public static bool ToBool(this object o) { return Convert.ToBoolean(o); }
    #endregion

    #region 向System.Web.UI.Page对象注册客户端脚本
   /// <summary>
    /// 向System.Web.UI.Page对象注册客户端脚本
   /// </summary>
   /// <param name="p"></param>
   /// <param name="key">要注册客户端脚本的键</param>
   /// <param name="js">要注册客户端脚本文本</param>
    public static void Js(this Page p, string key, string js)
    {
        p.ClientScript.RegisterClientScriptBlock(p.GetType(), key, string.Concat("<script type=\"text/javascript\">", js, "</script>"), false);
    }
    #endregion

    #region 替换单引为两个单引
    /// <summary>
    /// 将一个单引号，替换成两个单引号，参数null 时返回 ""
    /// </summary>
    public static string ReplaceQm(this string s)
    {
        if (s.IsNull())
            return "";
        else
            return s.Replace("'", "''");
    }
    #endregion

    #region 返回是否为数字
    /// <summary>
    /// 返回是否为数字
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static bool IsNumber(this string o)
    {
        try
        {
            double.Parse(o);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
#endregion

    #region 返回是否为汉字
    public static bool IsChina(this string CString)
    {
        bool BoolValue = false;
        for (int i = 0; i < CString.Length; i++)
        {
            if (Convert.ToInt32(Convert.ToChar(CString.Substring(i, 1))) < Convert.ToInt32(Convert.ToChar(128)))
            {
                BoolValue = false;
            }
            else
            {
                return BoolValue = true;
            }
        }
        return BoolValue;
    }
    #endregion

    public delegate void TryFun();
    /// <summary>
    /// 包含可能会出错的代码块，try -- catch，成功返回true，出现异常返回false
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="tryfun">一个不带参数，无返回值 的方法</param>
    /// <param name="errfun">出现异常时要执行的方法</param>
    public static bool Try(this object obj, TryFun tryfun, TryFun errfun)
    {
        bool ok = true;
        try
        {
            if (tryfun != null)
                tryfun();
            ok = true;
        }
        catch (NullReferenceException) { ok = false; }
        catch (IndexOutOfRangeException) { ok = false; }
        catch (FormatException) { ok = false; }
        catch (Exception) { ok = false; }
        finally
        {
            if (!ok && errfun != null)
            {
                errfun();
            }
        }
        return ok;
    }

    #region 编码字符串
    /// <summary>
    /// 编码字符串
    /// </summary>
    public static string EnCode(this string s)
    {
        Page p = new Page();
        return p.Server.UrlEncode(s);
    }
    #endregion 

    #region 解码字符串
    /// <summary>
    /// 解码字符串
    /// </summary>
    public static string DeCode(this string s)
    {
        Page p = new Page();
        return p.Server.UrlDecode(s);
    }
    #endregion
}
