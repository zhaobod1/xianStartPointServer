﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class message_message_look : System.Web.UI.Page
{
    public string edit_pk = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        edit_pk = Request["edit_pk"].ToS();
    
    }
}
