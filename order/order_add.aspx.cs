using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class order_order_add : System.Web.UI.Page
{
    public string edit_pk = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        edit_pk = Request["edit_pk"].ToS();

        DataSet ds = DbHelperSQL.Query("select * from t_car order by create_time");
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
    }
}
