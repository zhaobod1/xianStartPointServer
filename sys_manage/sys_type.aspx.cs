using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sys_manage_sys_type : System.Web.UI.Page
{
    public string t = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        t = Request["t"].ToS();
        if (t == "")
        {
            Response.End();
        }
    }
}
