using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class order_order_look : System.Web.UI.Page
{
    public string edit_pk = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        edit_pk = Request["edit_pk"].ToS();

        //DataSet ds = DbHelperSQL.Query("select group_pk,group_name from t_book_group order by create_time desc");
        //Repeater1.DataSource = ds;
        //Repeater1.DataBind();
    }
}
