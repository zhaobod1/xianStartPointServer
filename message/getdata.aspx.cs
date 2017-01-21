using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;

public partial class message_getdata : System.Web.UI.Page
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

                #region 消息
                case "search_message":
                    {
                        #region 查找通知
                        string strSQL = "select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_message a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where 1=1";
                        strSQL += " order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_message_info":
                    {
                        #region 获取单个消息
                        string message_pk = Request["message_pk"].ToS();
                        string strSQL = "select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_message a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where message_pk='" + message_pk + "'";
                        strSQL += " order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson_Html(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_message":
                    {
                        #region 保存消息
                        string user_type = Request["user_type"].ToS();                     
                        string message_content = Request["message_content"].ToS();                       
                        string strSQL = "";
                        ArrayList arr = new ArrayList();
                        if (user_type == "0" || user_type=="1")
                        {
                            ds = DbHelperSQL.Query("select * from t_coach where coach_state='1'");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                strSQL = "INSERT INTO [t_message]" +
                                    "([message_pk]" +
                                    ",[message_type]" +
                                    ",[user_type]" +
                                    ",[user_pk]" +
                                    ",[message_content]" +
                                    ",[message_state]" +
                                    ",[create_time])" +
                                  " VALUES" +
                                    "(newid()" +
                                    ",1" +
                                    ",1" +
                                    ",'" + ds.Tables[0].Rows[i]["coach_pk"].ToS() + "'" +
                                    ",'" + message_content + "'" +
                                    ",0" +
                                    ",getdate())";
                                arr.Add(strSQL);

                            }
                        }
                        if (user_type == "0" || user_type == "2")
                        {
                            ds = DbHelperSQL.Query("select * from t_student");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                strSQL = "INSERT INTO [t_message]" +
                                    "([message_pk]" +
                                    ",[message_type]" +
                                    ",[user_type]" +
                                    ",[user_pk]" +
                                    ",[message_content]" +
                                    ",[message_state]" +
                                    ",[create_time])" +
                                  " VALUES" +
                                    "(newid()" +
                                    ",1" +
                                    ",2" +
                                    ",'" + ds.Tables[0].Rows[i]["student_pk"].ToS() + "'" +
                                    ",'" + message_content + "'" +
                                    ",0" +
                                    ",getdate())";
                                arr.Add(strSQL);
                            }
                           
                        }                        
                        int j = DbHelperSQL.ExecuteSqlTran(arr);
                        if (j > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "发送消息");

                            strSQL = "select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_message a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where 1=1";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_message":
                    {
                        #region 删除消息
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_message where message_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除消息");
                            string strSQL = "select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_message a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where 1=1";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                #endregion
                #region 公告信息
                case "search_notice":
                    {
                        #region 查找公告
                        string strSQL = "select * from t_message_notice a where [infor_user_pk]='" + Session["UserPK"].ToS() + "' or Convert(varchar(5000),[notice_to_user_pk]) like '%" + Session["UserPK"].ToS() + "%'";
                        strSQL += " order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_notice_info":
                    {
                        #region 获取单个公告信息
                        string notice_pk = Request["notice_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_message_notice where notice_pk='" + notice_pk + "'");
                        restr = Json.DataTableToJson_Html(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_notice":
                    {
                        #region 保存公告信息
                        string notice_pk = Request["notice_pk"].ToS();
                        string notice_title = Request["notice_title"].ToS();
                        string notice_type = Request["notice_type"].ToS();
                        string notice_content = Request["notice_content"].ToS();
                        string notice_to_user_pk = Request["notice_to_user_pk"].ToS();
                        string notice_to_user_name = Request["notice_to_user_name"].ToS();
                        string infor_user_pk = Request["infor_user_pk"].ToS();
                        string strSQL = "";
                        if (notice_pk == "")
                        {

                            strSQL = "INSERT INTO [t_message_notice]" +
                                        "([notice_pk]" +
                                        ",[notice_title]" +
                                        ",[notice_type]" +
                                        ",[notice_content]" +
                                        ",[notice_to_user_pk]" +
                                        ",[notice_to_user_name]" +
                                        ",[infor_user_pk]" +
                                        ",[create_time])" +
                                     " VALUES" +
                                        "(newid()" +
                                        ",'" + notice_title + "'" +
                                        ",'" + notice_type + "'" +
                                        ",'" + notice_content + "'" +
                                        ",'" + notice_to_user_pk + "'" +
                                        ",'" + notice_to_user_name + "'" +
                                        ",'" + Session["UserPK"].ToS() + "'" +
                                     ",getdate())";

                        }
                        else
                        {
                            strSQL = "UPDATE [t_message_notice]" +
                                   " SET [notice_title] = '" + notice_title + "'" +
                                      ",[notice_type] = '" + notice_type + "'" +
                                      ",[notice_content] = '" + notice_content + "'" +
                                      ",[notice_to_user_pk] = '" + notice_to_user_pk + "'" +
                                      ",[notice_to_user_name] = '" + notice_to_user_name + "'" +
                                 " WHERE [notice_pk]='" + notice_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            if (strSQL.IndexOf("INSERT") > -1)
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "发布公告信息");
                            }
                            else
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑公告信息");
                            }
                            if (notice_to_user_pk != "")
                            {
                                for (i = 0; i < notice_to_user_pk.Split(',').Length; i++)
                                {
                                    if (notice_to_user_pk.Split(',')[i] == "") continue;
                                    txtinit.IniWriteValue(Server.MapPath("../ini/bee.ini"), "消息提醒", notice_to_user_pk.Split(',')[i], "1");
                                }
                            }
                            else
                            {
                                ds = DbHelperSQL.Query("select * from t_user");
                                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    txtinit.IniWriteValue(Server.MapPath("../ini/bee.ini"), "消息提醒", ds.Tables[0].Rows[i]["user_pk"].ToS(), "1");
                                }
                            }
                            strSQL = "select * from t_message_notice a  where [infor_user_pk]='" + Session["UserPK"].ToS() + "' or Convert(varchar(5000),[notice_to_user_pk]) like '%" + Session["UserPK"].ToS() + "%'";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_notice":
                    {
                        #region 删除公告
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_message_notice where notice_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                           
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除公告信息");

                                string strSQL = "select * from t_message_notice a  where [infor_user_pk]='" + Session["UserPK"].ToS() + "' or Convert(varchar(5000),[notice_to_user_pk]) like '%" + Session["UserPK"].ToS() + "%'";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                #endregion
                #region 通讯录分组信息
                case "search_group":
                    {
                        #region 查找通讯录分组
                        string strSQL = "select * from t_book_group a where group_user='" + Session["UserPK"].ToS() + "'";
                        strSQL += " order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_group_info":
                    {
                        #region 获取单个通讯录分组信息
                        string group_pk = Request["group_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_book_group where group_user='" + Session["UserPK"].ToS() + "' and group_pk='" + group_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_group":
                    {
                        #region 保存通讯录分组信息
                        string group_pk = Request["group_pk"].ToS();
                        string group_name = Request["group_name"].ToS();
                        string strSQL = "";
                        if (group_pk == "")
                        {
                            if (DbHelperSQL.ExecuteSqlScalar("select count(*) from t_book_group where group_name='" + group_name + "' and group_user='" + Session["UserPK"].ToS() + "'").ToInt32()>0)
                            {
                                restr="{\"result\":\"-100\"}";
                                break;
                            }
                            strSQL = "INSERT INTO [t_book_group]" +
                                        "([group_pk]" +
                                        ",[group_name]" +
                                        ",[group_user]" +
                                        ",[create_time])" +
                                     " VALUES" +
                                        "(newid()" +
                                        ",'" + group_name + "'" +
                                        ",'" + Session["UserPK"].ToS() + "'" +
                                     ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_book_group]" +
                                   " SET [group_name] = '" + group_name + "'" +
                                 " WHERE [group_pk]='" + group_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            if (strSQL.IndexOf("INSERT") > -1)
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "添加通讯录分组信息");
                            }
                            else
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑通讯录分组信息");
                            }
                            strSQL = "select * from t_book_group a  where group_user='" + Session["UserPK"].ToS() + "'";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_group":
                    {
                        #region 删除通讯录分组
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_book_group  where group_user='" + Session["UserPK"].ToS() + "' and group_pk in (" + del_pk + ")");
                        if (i > 0)
                        {                           
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除通讯录分组信息");                            
                            string strSQL = "select * from t_book_group a  where group_user='" + Session["UserPK"].ToS() + "'";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_group_book":
                    {
                        #region 清空分组
                        string clear_pk = Request["clear_pk"].ToS();
                        if (clear_pk.Length > 0) clear_pk = clear_pk.Substring(0, clear_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_book  where book_user='" + Session["UserPK"].ToS() + "' and group_pk in (" + clear_pk + ")");
                        if (i > 0)
                        {
                           
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "清空分组下联系人信息");
                            
                            restr = "{\"state\":\"100\"}";
                        }
                        break;
                        #endregion
                    }

                #endregion
                #region 联系人信息
                case "search_book":
                    {
                        #region 查找联系人
                        string group_pk = Request["group_pk"].ToS();
                        string book_company = Request["book_company"].ToS();
                        string book_name = Request["book_name"].ToS();
                        string book_address = Request["book_address"].ToS();
                        string book_sex = Request["book_sex"].ToS();
                        string strSQL = "select (select group_name from t_book_group where convert(varchar(100),group_pk)=a.group_pk) as group_name,* from t_book a where book_user='" + Session["UserPK"].ToS() + "'";
                        if (group_pk != "") strSQL += " and group_pk='" + group_pk + "'";
                        if (book_company != "") strSQL += " and book_company like '%" + book_company + "%'";
                        if (book_name != "") strSQL += " and book_name like '%" + book_name + "%'";
                        if (book_address != "") strSQL += " and book_address like '%" + book_address + "%'";
                        if (book_sex != "") strSQL += " and book_sex='" + book_sex + "'";
                        strSQL += " order by create_time desc";

                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_book_info":
                    {
                        #region 获取单个联系人信息
                        string book_pk = Request["book_pk"].ToS();
                        ds = DbHelperSQL.Query("select (select group_name from t_book_group where convert(varchar(100),group_pk)=a.group_pk) as group_name,* from t_book a where book_user='" + Session["UserPK"].ToS() + "' and book_pk='" + book_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_book":
                    {
                        #region 保存联系人信息
                        string book_pk = Request["book_pk"].ToS();
                        string group_pk = Request["group_pk"].ToS();
                        string book_name = Request["book_name"].ToS();
                        string book_sex = Request["book_sex"].ToS();
                        string book_phone = Request["book_phone"].ToS();
                        string book_tel = Request["book_tel"].ToS();
                        string book_birth = Request["book_birth"].ToS();
                        string book_qq = Request["book_qq"].ToS();
                        string book_title = Request["book_title"].ToS();
                        string book_email = Request["book_email"].ToS();
                        string book_company = Request["book_company"].ToS();
                        string book_address = Request["book_address"].ToS();
                        string strSQL = "";
                        if (book_pk == "")
                        {

                            strSQL = "INSERT INTO [t_book]" +
                                       "([book_pk]" +
                                       ",[group_pk]" +
                                       ",[book_name]" +
                                       ",[book_sex]" +
                                       ",[book_phone]" +
                                       ",[book_tel]" +
                                       ",[book_birth]" +
                                       ",[book_qq]" +
                                       ",[book_title]" +
                                       ",[book_email]" +
                                       ",[book_company]" +
                                       ",[book_address]" +
                                       ",[book_user]" +
                                       ",[create_time])" +
                                 " VALUES" +
                                       "(newid()" +
                                       ",'" + group_pk + "'" +
                                       ",'" + book_name + "'" +
                                       ",'" + book_sex + "'" +
                                       ",'" + book_phone + "'" +
                                       ",'" + book_tel + "'" +
                                       ",'" + book_birth + "'" +
                                       ",'" + book_qq + "'" +
                                       ",'" + book_title + "'" +
                                       ",'" + book_email + "'" +
                                       ",'" + book_company + "'" +
                                       ",'" + book_address + "'" +
                                       ",'" + Session["UserPK"].ToS() + "'" +
                                       ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_book]" +
                                   " SET [group_pk] = '" + group_pk + "'" +
                                      ",[book_name] = '" + book_name + "'" +
                                      ",[book_sex] = '" + book_sex + "'" +
                                      ",[book_phone] = '" + book_phone + "'" +
                                      ",[book_tel] = '" + book_tel + "'" +
                                      ",[book_birth] = '" + book_birth + "'" +
                                      ",[book_qq] = '" + book_qq + "'" +
                                      ",[book_title] = '" + book_title + "'" +
                                      ",[book_email] = '" + book_email + "'" +
                                      ",[book_company] = '" + book_company + "'" +
                                      ",[book_address] = '" + book_address + "'" +
                                 " WHERE [book_pk]='" + book_pk + "'";
                        }

                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            if (strSQL.IndexOf("INSERT") > -1)
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "添加联系人信息");
                            }
                            else
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑联系人信息");
                            }
                            strSQL = "select (select group_name from t_book_group where convert(varchar(100),group_pk)=a.group_pk) as group_name,* from t_book a  where book_user='" + Session["UserPK"].ToS() + "'";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_book":
                    {
                        #region 删除联系人
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_book  where book_user='" + Session["UserPK"].ToS() + "' and book_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                            
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除联系人信息");
                            
                            string strSQL = "select (select group_name from t_book_group where convert(varchar(100),group_pk)=a.group_pk) as group_name,* from t_book a  where book_user='" + Session["UserPK"].ToS() + "'";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                #endregion
                #region 邮件信息
                case "search_in_email":
                    {
                        #region 获取收件箱邮件
                        string strSQL = "select (select User_name from t_user where convert(varchar(100),user_pk)=a.email_user_pk) as email_user_name,* from t_message_email a  where email_to_user_pk='" + Session["UserPK"].ToS() + "' and email_temp=0 and email_to_user_del=0";
                        strSQL += " order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "search_out_email":
                    {
                        #region 获取发件箱邮件
                        string strSQL = "select (select User_name from t_user where convert(varchar(100),user_pk)=a.email_to_user_pk) as email_to_user_name,* from t_message_email a where email_user_pk ='" + Session["UserPK"].ToS() + "' and email_temp=0 and email_user_del=0";
                        strSQL += " order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "search_temp_email":
                    {
                        #region 获取草稿箱
                        string strSQL = "select * from t_message_email a where email_user_pk ='" + Session["UserPK"].ToS() + "' and email_temp=1";
                        strSQL += " order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "search_rubbish_email":
                    {
                        #region 获取废件箱
                        string strSQL = "select (select User_name from t_user where convert(varchar(100),user_pk)=a.email_user_pk) as email_user_name,* from t_message_email a  where email_to_user_pk='" + Session["UserPK"].ToS() + "' and email_temp=0 and email_to_user_del=1";
                        strSQL += " order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_email_info":
                    {
                        #region 获取单个邮件信息
                        string email_pk = Request["email_pk"].ToS();
                        ds = DbHelperSQL.Query("update t_message_email set email_read=1 where email_pk='" + email_pk + "';select (select User_name from t_user where convert(varchar(100),user_pk)=a.email_user_pk) as email_user_name,(select User_name from t_user where convert(varchar(100),user_pk)=a.email_to_user_pk) as email_to_user_name,* from t_message_email a where email_pk='" + email_pk + "'");
                        restr = Json.DataTableToJson_Html(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_email_temp_info":
                    {
                        #region 获取单个草稿邮件信息
                        string email_pk = Request["email_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_message_email a where email_pk='" + email_pk + "'");
                        restr = Json.DataTableToJson_Html(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_email_out_info":
                    {
                        #region 获取单个邮件信息
                        string email_pk = Request["email_pk"].ToS();
                        ds = DbHelperSQL.Query("select (select User_name from t_user where convert(varchar(100),user_pk)=a.email_user_pk) as email_user_name,(select User_name from t_user where convert(varchar(100),user_pk)=a.email_to_user_pk) as email_to_user_name,* from t_message_email a where email_code='" + email_pk + "'");
                        restr = Json.DataTableToJson_Html(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_email":
                    {
                        #region 保存邮件信息
                        string email_pk = Request["email_pk"].ToS();
                        string email_title = Request["email_title"].ToS();
                        string email_content = Request["email_content"].ToS();
                        string email_affix = Request["email_affix"].ToS();
                        string[] email_to_user_pk = Request["email_to_user_pk"].ToS().Split(',');
                        string email_code = Guid.NewGuid().ToS();
                        string email_temp = Request["temp"].ToS();
                        ArrayList arr = new ArrayList();
                        string email_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        string strSQL = "";
                        if (email_pk != "")
                        {
                            arr.Add("delete from t_message_email where email_pk='" + email_pk + "'");
                        }
                        if (email_temp == "0")
                        {
                            for (int i = 0; i < email_to_user_pk.Length; i++)
                            {
                                if (email_to_user_pk[i].ToS() == "") continue;
                                strSQL = "INSERT INTO [t_message_email]" +
                                       "([email_pk]" +
                                       ",[email_title]" +
                                       ",[email_content]" +
                                       ",[email_affix]" +
                                       ",[email_date]" +
                                       ",[email_to_user_pk]" +
                                       ",[email_read]" +
                                       ",[email_user_pk]" +
                                       ",[email_code]" +
                                       ",[email_temp]" +
                                       ",[email_user_del]" +
                                       ",[email_to_user_del]" +
                                       ",[create_time])" +
                                    " VALUES" +
                                       "(newid()" +
                                       ",'" + email_title + "'" +
                                       ",'" + email_content + "'" +
                                        ",'" + email_affix + "'" +
                                       ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                       ",'" + email_to_user_pk[i].ToS() + "'" +
                                       ",0" +
                                       ",'" + Session["UserPK"].ToS() + "'" +
                                       ",'" + email_code + "'" +
                                       ",0" +
                                       ",0" +
                                       ",0" +
                                    ",'" + email_date + "')";
                                arr.Add(strSQL);

                            }
                        }
                        else
                        {
                            strSQL = "INSERT INTO [t_message_email]" +
                                       "([email_pk]" +
                                       ",[email_title]" +
                                       ",[email_content]" +
                                       ",[email_affix]" +
                                       ",[email_date]" +
                                       ",[email_to_user_pk]" +
                                       ",[email_to_user_name]" +
                                       ",[email_read]" +
                                       ",[email_user_pk]" +
                                       ",[email_code]" +
                                       ",[email_temp]" +
                                       ",[email_user_del]" +
                                       ",[email_to_user_del]" +
                                       ",[create_time])" +
                                    " VALUES" +
                                       "(newid()" +
                                       ",'" + email_title + "'" +
                                       ",'" + email_content + "'" +
                                        ",'" + email_affix + "'" +
                                       ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                                       ",'" + Request["email_to_user_pk"].ToS() + "'" +
                                       ",'" + Request["email_to_user_name"].ToS() + "'" +
                                       ",0" +
                                       ",'" + Session["UserPK"].ToS() + "'" +
                                       ",'" + email_code + "'" +
                                       ",1" +
                                       ",0" +
                                       ",0" +
                                    ",'" + email_date + "')";
                            arr.Add(strSQL);
                        }

                        //else
                        //{
                        //    strSQL = "UPDATE [t_message_email]" +
                        //           " SET [email_title] = '" + email_title + "'" +
                        //              ",[email_content] = '" + email_content + "'" +
                        //              ",[email_to_user_pk] = '" + email_to_user_pk + "'" +                                      
                        //         " WHERE [email_pk]='" + email_pk + "'";
                        //}
                        int c = DbHelperSQL.ExecuteSqlTran(arr);
                        if (c > 0)
                        {
                            if (email_temp == "0")
                            {
                                for (int i = 0; i < email_to_user_pk.Length; i++)
                                {                                   
                                    if (email_to_user_pk[i].ToS() == "") continue;
                                    txtinit.IniWriteValue(Server.MapPath("../ini/bee.ini"), "消息提醒", email_to_user_pk[i].ToS(), "1");
                                }
                            }
                            restr = "{\"result\":\"" + c + "\"}";
                        }
                        break;
                        #endregion
                    }
                case "del_in_email":
                    {
                        #region 删除邮件
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("update t_message_email set email_to_user_del=1 where email_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                            string strSQL = "select (select User_name from t_user where convert(varchar(100),user_pk)=a.email_user_pk) as email_user_name,* from t_message_email a  where email_to_user_pk='" + Session["UserPK"].ToS() + "' and email_temp=0 and email_to_user_del=0";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_out_email":
                    {
                        #region 删除邮件
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("update t_message_email set email_user_del=2 where email_code in (" + del_pk + ")");
                        if (i > 0)
                        {
                            string strSQL = "select (select User_name from t_user where convert(varchar(100),user_pk)=a.email_to_user_pk) as email_to_user_name,* from t_message_email a where email_user_pk ='" + Session["UserPK"].ToS() + "' and email_temp=0 and email_user_del=0";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_temp_email":
                    {
                        #region 删除邮件
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_message_email where email_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                            string strSQL = "select * from t_message_email a where email_user_pk ='" + Session["UserPK"].ToS() + "' and email_temp=1";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_rubbish_email":
                    {
                        #region 删除邮件
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("update t_message_email set email_to_user_del=2 where email_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                            string strSQL = "select (select User_name from t_user where convert(varchar(100),user_pk)=a.email_user_pk) as email_user_name,* from t_message_email a  where email_to_user_pk='" + Session["UserPK"].ToS() + "' and email_temp=0 and email_to_user_del=0";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "upload_email_affix":
                    {
                        #region 上传附件
                        HttpFileCollection files = Request.Files;
                        if (files.Count > 0)
                        {
                            try
                            {
                                string c = DateTime.Now.ToString("yyyyMMddHHmmss");
                                files[0].SaveAs(Server.MapPath("../upload") + "\\email\\" + c+files[0].FileName);
                                restr = "{\"filename\":\"" + files[0].FileName + "\",\"url\":\"" + c + files[0].FileName + "\"}";           
                            }
                            catch (Exception)
                            {
                                
                            }
                                          
                        }
                        break;
                        #endregion
                    }
                case "del_email_affix":
                    {
                        #region 删除附件
                        string file_name = Request["k"].ToS();
                        try
                        {
                            File.Delete(Server.MapPath(file_name));
                        }
                        catch (Exception)
                        { }
                        break;
                        #endregion
                    }
               
                #endregion
                #region 投诉

                case "search_info":
                    {
                        #region 查找投诉
                        string info_type = Request["info_type"].ToS();
                        string strSQL = "select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_info a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where [info_type]=" + info_type + "";
                        strSQL += " order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_info_info":
                    {
                        #region 获取单个投诉信息
                        string info_pk = Request["info_pk"].ToS();
                        ds = DbHelperSQL.Query("select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_info a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where info_pk='" + info_pk + "'");
                        restr = Json.DataTableToJson_Html(ds.Tables[0]);
                        break;
                        #endregion
                    }

              
                case "del_info":
                    {
                        #region 删除投诉
                        string info_type = Request["info_type"].ToS();
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_info where info_pk in (" + del_pk + ")");
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), (info_type == "1" ? "删除投诉信息" : "删除意见反馈信息"));

                            string strSQL = "select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_info a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where [info_type]=" + info_type + "";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "cog_info":
                    {
                        #region 处理投诉 提现
                        string info_type = Request["info_type"].ToS();
                        string cog_pk = Request["cog_pk"].ToS();
                        string info_rem = Request["info_rem"].ToS();
                      
                        int i = DbHelperSQL.ExecuteSql("update t_info set info_state=1,info_rem='"+ info_rem + "' where info_pk='" + cog_pk + "'");
                        if (i > 0)
                        {
                            if (info_type == "1" && info_rem!="")
                            {
                                ds = DbHelperSQL.Query("select * from t_info  where info_pk='" + cog_pk + "'");
                                if (ds.Tables[0].Rows.Count > 0)
                                {                                    
                                    //短信通知投诉处理结果
                                    string template = "0000700004";
                                    string phone = "";
                                    if (ds.Tables[0].Rows[0]["user_type"].ToS() == "1")
                                    {
                                        phone = DbHelperSQL.ExecuteSqlScalar("select coach_phone from t_coach where coach_pk='" + ds.Tables[0].Rows[0]["user_pk"].ToS() + "'").ToS();
                                    }
                                    else {
                                        phone = DbHelperSQL.ExecuteSqlScalar("select student_phone from t_student where student_pk='" + ds.Tables[0].Rows[0]["user_pk"].ToS() + "'").ToS();
                                    }
                                    if (phone != "")
                                    {
                                        string content = DbHelperSQL.ExecuteSqlScalar("select code_content from t_sys_code where code='" + template + "'").ToS();
                                        content = content.Replace("{complan_result}", info_rem);
                                        Tools.SendSMS(phone, content);
                                    }
                                }
                            }
                            else if (info_type == "3")
                            {
                                //提现审核失败
                                ds = DbHelperSQL.Query("select * from t_info where  info_pk='" + cog_pk + "'");
                                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                {
                                    DbHelperSQL.ExecuteSql("update t_coach set coach_money=coach_money-" + ds.Tables[0].Rows[j]["info_obj"].ToD() + " where coach_pk='" + ds.Tables[0].Rows[j]["user_pk"].ToS() + "'");
                                }

                            }
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), (info_type == "1" ? "处理投诉信息" : info_type == "2" ? "处理意见反馈信息":"处理提现信息"));

                            string strSQL = "select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_info a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where [info_type]=" + info_type + "";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "scrap_info":
                    {
                        #region 审核失败
                        string info_type = Request["info_type"].ToS();
                        string cog_pk = Request["cog_pk"].ToS();
                        string info_rem = Request["info_rem"].ToS();
                     
                        int i = DbHelperSQL.ExecuteSql("update t_info set info_state=2,info_rem='" + info_rem + "' where info_pk='" + cog_pk + "'");
                        if (i > 0)
                        {
                            if (info_type == "3")
                            { 
                                //提现审核失败
                                ds = DbHelperSQL.Query("select * from t_info where  info_pk='" + cog_pk + "'");
                                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                {
                                    DbHelperSQL.ExecuteSql("update t_coach set coach_money=coach_money+" + ds.Tables[0].Rows[j]["info_obj"].ToD() + " where coach_pk='"+ds.Tables[0].Rows[j]["user_pk"].ToS()+ "'");
                                }
                               
                            }


                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), (info_type == "1" ? "投诉信息审核失败" : info_type == "2" ? "意见反馈信息审核失败" : "提现信息审核失败"));

                            string strSQL = "select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_info a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where [info_type]=" + info_type + "";
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
