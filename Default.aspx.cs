using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PostMsg_Net;
using PostMsg_Net.common;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Request["link"].ToS() != "")
        {
            string str_code = Request["link"].ToS();
            if (str_code.Length > 0 && str_code.Length % 2 == 0)
            {
                string code = "";
                //两位加横杠
                for (int i = 0; i < str_code.Length; i = i + 2)
                {
                    code += str_code.Substring(i, 2) + "-";
                }
                //解密
                code = Tools.Decode(code.Substring(0, code.Length - 1));
                Session["pop"] = code;
                //string userAgent = Request.UserAgent;
                //if (userAgent.ToLower().Contains("micromessenger"))
                //    Response.Redirect("wachat/reg.aspx");
                //else
                Response.Redirect("reg.aspx");
            }
        }
        if (Session["UserPK"].ToS() == "") Response.Redirect("login.aspx");
        //TimeSpan ts = DateTime.Parse("2014-10-10") - DateTime.Parse("2014-09-29");
        //Response.Write(ts.Days);
        //Response.End();
    }
}
