using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class order_order_manage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = Request["s"].ToS();
        s = s != "" ? (s == "2" ? "'1','2'" : ("'" + s + "'")) : "";
        Session["order_state"] = s;
    }
    
}
