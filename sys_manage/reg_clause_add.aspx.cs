using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class sys_manage_reg_clause_add : System.Web.UI.Page
{
    public string edit_pk = "";
    public string code_name = "";
    public string code_content = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        edit_pk = Request["code"].ToS();

        code_name = edit_pk == "00001" ? "注册条款" : edit_pk == "00002" ? "陪练注册条款" : edit_pk == "00003" ? "学员注册条款" : edit_pk == "00004" ? "注册验证短信模板" : edit_pk == "00005" ? "重置密码短信模板" : "";
        DataSet ds = DbHelperSQL.Query("select code_content from t_sys_code where code='" + edit_pk + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            code_content =Json.ChangeString(ds.Tables[0].Rows[0][0].ToS());
        }
        else
        {
            DbHelperSQL.ExecuteSql("insert into t_sys_code(code_pk,code) values(newid(),'" + edit_pk + "')");
        }

    }
}
