using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Diagnostics;

public partial class login : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {       
        Session["Sys_Title"] = DbHelperSQL.ExecuteSqlScalar("select sys_title from t_system");
        Session["Sys_Title"] = Session["Sys_Title"].ToS() == "" ? "起点后台管理系统" : Session["Sys_Title"].ToS();
    }
   
    protected void Button1_click(object sender, EventArgs e)
    {
        //if (Session["serverCode"].ToS() == Text3.Value && DateTime.Now<=DateTime.Parse("2014-11-01"))
        //{
            string user_name = Text1.Value.Replace("'", "").Replace("+", "").Replace(",", "").Replace("|", "").Replace("&", "").Replace("|", "").Replace("or", "");
            string user_pwd = Tools.EncryptString(Text2.Value, "15802929");
            DataSet ds = DbHelperSQL.Query("select * from t_user where user_login_name='" + user_name + "' and user_login_pwd='" + user_pwd + "'");
            if (ds.Tables[0].Rows.Count == 1)
            {
              
                Session["UserPK"] = ds.Tables[0].Rows[0]["user_pk"].ToS();
                Session["User_Login_Name"] = ds.Tables[0].Rows[0]["user_login_name"].ToS();
                Session["UserName"] = ds.Tables[0].Rows[0]["user_name"].ToS();
                Session["UserDept"] = ds.Tables[0].Rows[0]["org_pk"].ToS();
                Session["UserDeptName"] = DbHelperSQL.ExecuteSqlScalar("select org_name from t_organization where org_pk='" + ds.Tables[0].Rows[0]["org_pk"].ToS() + "'").ToS();
                Session["UserRole"] = ds.Tables[0].Rows[0]["user_role"].ToS();
                Session["RightVal"] = DbHelperSQL.ExecuteSqlScalar("select rig_val from t_role_rights where role_pk='" + ds.Tables[0].Rows[0]["user_role"].ToS() + "'").ToS();
                Session["sys_sucase"] = DbHelperSQL.ExecuteSqlScalar("select sys_sucase from t_system").ToInt32().ToS();
                Session["sys_sucase"] = Session["sys_sucase"].ToInt32() == 0 ? "15" : Session["sys_sucase"].ToS();
                System.Net.IPAddress addr;
                addr = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
                Session.Add("ip1", addr.ToS());
                Session.Add("ip2", MyPublicIP());           
                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "登陆系统");
                Response.Redirect("/?webadmin");
            }
            else
            { this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "Popalert('用户名或密码错误!');", true); }
        //}
        //else
        //{
        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "Popalert('验证码不正确,请重新输入!');", true);
        //}
    }
    static string MyPublicIP()
    {
        using (System.Net.WebClient wc = new System.Net.WebClient())
        {
            string html = wc.DownloadString("http://ip.qq.com");
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(html, "<span class=\"red\">([^<]+)</span>");
            if (m.Success) return m.Groups[1].Value;

            return "0.0.0.0";
        }
    }
}
