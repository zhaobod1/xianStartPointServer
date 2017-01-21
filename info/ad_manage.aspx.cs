using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class info_ad_manage : System.Web.UI.Page
{
    public DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        ds = DbHelperSQL.Query("select * from t_info where info_type=4 and info_title='" + Request["i"].ToS() + "' order by create_time desc");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (car_img.Value == "")
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "Popalert('请上传图片!');", true);

        }
        else
        {
           string strSQL = "INSERT INTO [t_info]" +
                                  "([info_pk]" +
                                  ",[info_type]" +                                                                
                                  ",[info_title]" +
                                  ",[info_content]" +    
                                  ",[create_time])" +
                              " VALUES" +
                                  "(newid()" +
                                  ",4" +
                                  ",'" + Request["i"].ToS() + "'" +                              
                                  ",'" + car_img.Value + "'" +                              
                                  ",getdate())";
            int i = DbHelperSQL.ExecuteSql(strSQL);
            if (i > 0)
            {
                Response.Redirect("/info/ad_manage.aspx?i=" + Request["i"].ToS());
            }
        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (DbHelperSQL.ExecuteSql("delete from t_info where info_pk='" + car_img.Value.Trim() + "'") > 0)
        {
            Response.Redirect("/info/ad_manage.aspx?i=" + Request["i"].ToS());

        }
        else
        {
            car_img.Value = "";
        }
    }
}
