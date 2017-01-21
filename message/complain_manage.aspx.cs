using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class message_complain_manage : System.Web.UI.Page
{
    public string pk = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        pk = Request["pk"].ToS();
    }
    
}
