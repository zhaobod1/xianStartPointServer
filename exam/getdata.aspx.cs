using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class exam_getdata : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = null;
        string corID = Session["CorID"].ToS();
        string com = Request["com"].ToS();
        string restr = "";
        try
        {
            switch (com)
            {

                #region 试题信息
                case "search_question":
                    {
                        #region 查找试题

                        string strSQL = "select case when question_sub='1' then '科目一' else '科目四' end as question_subject,* from t_question a";
                        strSQL += " order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
              
                case "get_question_info":
                    {
                        #region 获取单个试题信息
                        string question_pk = Request["question_pk"].ToS();
                        string strSQL = "select * from t_question a  where question_pk='" + question_pk + "'";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_question":
                    {
                        #region 保存试题信息
                        string question_pk = Request["question_pk"].ToS();
                        string question_sub = Request["question_sub"].ToS();
                        string question_type = Request["question_type"].ToS();
                        string question_title = Request["question_title"].ToS();
                        string question_answer = Request["question_answer"].ToS();
                        string question_right = Request["question_right"].ToS();
                        string question_state = Request["question_state"].ToS();
                        string question_rem = Request["question_rem"].ToS();
                        string question_pic = Request["question_pic"].ToS();
                        string strSQL = "";
                        if (question_pk == "")
                        {
                            strSQL = "INSERT INTO [t_question]" +
                            "([question_pk]" +
                            ",[question_sub]" +
                            ",[question_type]" +
                            ",[question_title]" +
                            ",[question_answer]" +
                            ",[question_right]" +
                            ",[question_state]" +
                            ",[question_pic]" +
                            ",[question_rem]" +
                            ",[create_time])" +
                            " VALUES" +
                            "(newid()" +
                            ",'" + question_sub + "'" +
                             ",'" + question_type + "'" +
                            ",'" + question_title + "'" +
                            ",'" + question_answer + "'" +
                            ",'" + question_right + "'" +
                            ",'" + question_state + "'" +
                             ",'" + question_pic + "'" +
                              ",'" + question_rem + "'" +
                            ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_question]" +
                            " SET [question_sub] = '" + question_sub + "'" +
                            ",[question_type] = '" + question_type + "'" +
                            ",[question_title] = '" + question_title + "'" +
                            ",[question_answer] = '" + question_answer + "'" +
                            ",[question_right] = '" + question_right + "'" +
                            ",[question_state] = '" + question_state + "'" +
                            ",[question_pic] = '" + question_pic + "'" +
                            ",[question_rem] = '" + question_rem + "'" +
                            " WHERE [question_pk]='" + question_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            strSQL = "select case when question_sub='1' then '科目一' else '科目四' end as question_subject,* from t_question a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }

                        break;
                        #endregion
                    }
                case "del_question":
                    {
                        #region 删除试题
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_question where question_pk in (" + del_pk + ")");
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除试题信息");

                            string strSQL = "select case when question_sub='1' then '科目一' else '科目四' end as question_subject,* from t_question a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
            
                #endregion
              
                default:
                    {
                        restr = "";
                        break;
                    }
            }
        }
        catch (Exception)
        {
            restr = "";
        }
        Response.Write(restr);
        Response.End();
    }
}
