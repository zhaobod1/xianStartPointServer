using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;

public partial class sys_manage_getdata : System.Web.UI.Page
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
                #region 媒体类型信息
                case "search_type":
                    {
                        #region 以树数据格式返回媒体类型信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");           
                        string t = Request["t"].ToS();
                        ds = DbHelperSQL.Query("select * from t_m_type where type=" + t + " order by type_ord,create_time desc");
                        string zNodes = "[";
                        for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            if (ds.Tables[0].Select("parent_type_code='" + ds.Tables[0].Rows[j]["type_code"].ToS() + "'").Length > 0)
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["type_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_type_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["type_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/gg1.png\"}";
                            else zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["type_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_type_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["type_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/gg2.png\"}";
                            if (j < ds.Tables[0].Rows.Count - 1)
                                zNodes += ",";
                        }
                        zNodes += "]";
                        restr = zNodes;
                        break;
                        #endregion
                    }
                case "search_sel_type":
                    {
                        #region 以树数据格式返回媒体类型信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");           
                        string t = Request["t"].ToS();
                        string strSQL="select * from t_m_type where type=" + t;
                        if (t == "1" || t == "3" || t == "4" || t == "6") strSQL += " and type_org like '%" + Session["UserDept"].ToS() + "%'";
                        strSQL+=" order by type_ord,create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        
                        string zNodes = "[";
                        for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            if (ds.Tables[0].Select("parent_type_code='" + ds.Tables[0].Rows[j]["type_code"].ToS() + "'").Length > 0)
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["type_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_type_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["type_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/gg1.png\"}";
                            else zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["type_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_type_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["type_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/gg2.png\"}";
                            if (j < ds.Tables[0].Rows.Count - 1)
                                zNodes += ",";
                        }
                        zNodes += "]";
                        restr = zNodes;
                        break;
                        #endregion
                    }
                case "search_msource_type":
                    {
                        #region 以JSON格式返回媒体类型信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");           
                        string t = Request["t"].ToS();
                        string type_pk = Request["type_pk"].ToS();
                        string strSQL = "select (select count(*) from t_m_type where type_code=a.parent_type_code) as parent_count,(select count(*) from t_m_type where parent_type_code=a.type_code) as child_count,* from t_m_type a where type=" + t;
                        if (type_pk != "") strSQL += " and (type_pk='" + type_pk + "' or parent_type_code in (select type_code from t_m_type where type_pk='" + type_pk + "'))";
                        strSQL += " order by type_code,type_ord";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_type_info":
                    {
                        #region 获取单条媒体类型信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        string edit_pk = Request["type_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_m_type  where type_pk='" + edit_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "save_type":
                    {
                        #region 保存媒体类型信息
                        string t = Request["t"].ToS();
                        string type_pk = Request["type_pk"].ToS();
                        string parent_type_code = Request["parent_type_code"].ToS();
                        string type_name = Request["type_name"].ToS();
                        string type_ord = Request["type_ord"].ToInt32().ToS();
                        string type_org = Request["type_org"].ToS();
                        string type_org_name = Request["type_org_name"].ToS();
                        string type_code = "";
                        string strSQL = "";
                        if (type_pk == "")
                        {
                            ds = DbHelperSQL.Query("select max(type_code) from t_m_type where type_code like '" + parent_type_code + "%' and len(type_code)=" + (parent_type_code.Length + 5).ToS() + "");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string ID = ds.Tables[0].Rows[0][0].ToS();
                                if (ID == "")
                                    type_code = parent_type_code + "00001";
                                else
                                    type_code = parent_type_code + (ID.Substring(ID.Length - 5, 5).ToInt32() + 1).ToS().PadLeft(5, '0');
                            }
                            else
                            {
                                type_code = parent_type_code + "00001";
                            }
                            strSQL = "INSERT INTO [t_m_type]" +
                                       "([type_pk]" +
                                       ",[type_code]" +
                                       ",[parent_type_code]" +
                                       ",[type_name]" +
                                       ",[type]" +
                                       ",[type_ord]" +
                                       ",[type_org]" +
                                       ",[type_org_name]" +
                                       ",[create_time])" +
                                 " VALUES" +
                                       "(newid()" +
                                       ",'" + type_code + "'" +
                                       ",'" + parent_type_code + "'" +
                                       ",'" + type_name + "'" +
                                       "," + t +
                                       "," + type_ord +
                                       ",'" + type_org + "'" +
                                       ",'" + type_org_name + "'" +
                                       ",getdate())";

                        }
                        else
                        {
                            strSQL = "UPDATE [t_m_type]" +
                                       "SET [type_name] = '" + type_name + "'" +
                                       ",[type_ord] = " + type_ord +
                                       ",[type_org] = '" + type_org + "'" +
                                       ",[type_org_name] = '" + type_org_name + "'" +
                                     " WHERE type_pk='" + type_pk + "';update [t_m_type] set [type_org] = '" + type_org + "',[type_org_name] = '" + type_org_name + "' where  type_pk in (select type_pk from t_m_type where parent_type_code like (select type_code from t_m_type where type_pk='"+type_pk+"')+'%')";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            string v = "";
                            if (t == "1")
                            {
                                v = "媒体类型信息";
                            }
                            else if (t == "2")
                            {
                                v = "媒体形式信息";
                            }
                            else if (t == "3")
                            {
                                v = "合同类型信息";
                            }
                            else if (t == "4")
                            {
                                v = "收入类型信息";
                            }
                            else if (t == "5")
                            {
                                v = "媒体上下行信息";
                            }
                            else if (t == "6")
                            {
                                v = "经营业务类型信息";
                            }

                            if (strSQL.IndexOf("INSERT") > -1)
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "添加" + v);
                            }
                            else
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑" + v);
                            }
                            ds = DbHelperSQL.Query("select * from t_m_type where type=" + t + " order by type_ord,create_time desc");
                            string zNodes = "[";
                            for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                if (ds.Tables[0].Select("parent_type_code='" + ds.Tables[0].Rows[j]["type_code"].ToS() + "'").Length > 0)
                                    zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["type_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_type_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["type_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/gg1.png\"}";
                                else zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["type_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_type_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["type_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/gg2.png\"}";
                                if (j < ds.Tables[0].Rows.Count - 1)
                                    zNodes += ",";
                            }
                            zNodes += "]";
                            restr = zNodes;
                        }
                        break;
                        #endregion
                    }
                case "del_type":
                    {
                        #region 删除媒体类型信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string t = Request["t"].ToS();
                        string del_pk = Request["del_pk"].ToS();
                        DataSet dsa = DbHelperSQL.Query("select parent_type_code,(select count(type_pk) from t_m_type where parent_type_code=a.type_code) as child_type_count from t_m_type a where type_pk='" + del_pk + "'");
                        if (dsa.Tables[0].Rows.Count > 0)
                        {
                            if (dsa.Tables[0].Rows[0][0].ToS() == "-1")
                            {
                                restr = "{\"state\":\"-100\"}";
                                break;
                            }
                            if (dsa.Tables[0].Rows[0][1].ToInt32() > 0)
                            {
                                restr = "{\"state\":\"-99\"}";
                                break;
                            }
                        }
                        string strSQL = "delete from t_m_type where type_pk='" + del_pk + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            string v = "";
                            if (t == "1")
                            {
                                v = "媒体类型信息";
                            }
                            else if (t == "2")
                            {
                                v = "媒体形式信息";
                            }
                            else if (t == "3")
                            {
                                v = "合同类型信息";
                            }
                            else if (t == "4")
                            {
                                v = "收入类型信息";
                            }
                            else if (t == "5")
                            {
                                v = "媒体上下行信息";
                            }
                            else if (t == "6")
                            {
                                v = "经营业务类型信息";
                            }


                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除" + v);

                            ds = DbHelperSQL.Query("select * from t_m_type where type=" + t + " order by type_ord,create_time desc");
                            string zNodes = "[";
                            for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                if (ds.Tables[0].Select("parent_type_code='" + ds.Tables[0].Rows[j]["type_code"].ToS() + "'").Length > 0)
                                    zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["type_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_type_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["type_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/gg1.png\"}";
                                else zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["type_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_type_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["type_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["type_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/gg2.png\"}";
                                if (j < ds.Tables[0].Rows.Count - 1)
                                    zNodes += ",";
                            }
                            zNodes += "]";
                            restr = zNodes;
                        }
                        break;
                        #endregion
                    }
                #endregion
                case "save_code":
                    {
                        #region 保存注册条款
                        string code = Request["code"].ToS();
                     
                        string code_content = Request["code_content"].ToS();
                     
                        string strSQL  = "UPDATE [t_sys_code]" +
                                   " SET [code_content] = '" + code_content + "'" +
                                   
                                 " WHERE [code]='" + code + "'";
                        
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑注册条款");

                            restr = "{\"result\":\"1\"}";
                        }
                        break;
                        #endregion
                    }
                #region 路段信息信息
                case "search_load":
                    {
                        #region 获取路段信息信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        ds = DbHelperSQL.Query("select * from t_m_load order by load_ord,create_time desc");
                        string zNodes = "[";
                        for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["load_pk"].ToS() + "\", \"pId\":\"-1\", \"name\":\"" + ds.Tables[0].Rows[j]["load_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["load_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["load_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/load.png\"}";
                            if (j < ds.Tables[0].Rows.Count - 1)
                                zNodes += ",";
                        }
                        zNodes += "]";
                        restr = zNodes;
                        break;
                        #endregion
                    }
                case "search_msource_load":
                    {
                        #region 以JSON格式获取路段信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表"); 
                        string load_pk = Request["load_pk"].ToS();
                        string strSQL = "select * from t_m_load where 1=1";
                        if (load_pk != "") strSQL += " and load_pk='" + load_pk + "'";
                        strSQL += " order by load_ord,create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_load_info":
                    {
                        #region 获取单条路段信息信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        string edit_pk = Request["load_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_m_load  where load_pk='" + edit_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "save_load":
                    {
                        #region 保存路段信息信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string load_pk = Request["load_pk"].ToS();
                        string load_name = Request["load_name"].ToS();
                        string load_ord = Request["load_ord"].ToInt32().ToS();
                        string strSQL = "";
                        if (load_pk == "")
                        {
                            strSQL = "INSERT INTO [t_m_load]" +
                                       "([load_pk]" +
                                       ",[load_name]" +
                                       ",[load_ord]" +
                                       ",[create_time])" +
                                 " VALUES" +
                                       "(newid()" +
                                       ",'" + load_name + "'" +
                                       "," + load_ord +
                                       ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_m_load]" +
                                       "SET [load_name] = '" + load_name + "'" +
                                       ",[load_ord] = " + load_ord +
                                     " WHERE load_pk='" + load_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            if (strSQL.IndexOf("INSERT") > -1)
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "添加路段信息");
                            }
                            else
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑路段信息");
                            }
                            ds = DbHelperSQL.Query("select * from t_m_load order by load_ord,create_time desc");
                            string zNodes = "[";
                            for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["load_pk"].ToS() + "\", \"pId\":\"-1\", \"name\":\"" + ds.Tables[0].Rows[j]["load_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["load_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["load_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/load.png\"}";
                                if (j < ds.Tables[0].Rows.Count - 1)
                                    zNodes += ",";
                            }
                            zNodes += "]";
                            restr = zNodes;
                        }
                        break;
                        #endregion
                    }
                case "del_load":
                    {
                        #region 删除路段信息信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string del_pk = Request["del_pk"].ToS();
                        string strSQL = "delete from t_m_load where load_pk='" + del_pk + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除路段信息");
                            ds = DbHelperSQL.Query("select * from t_m_load order by load_ord,create_time desc");
                            string zNodes = "[";
                            for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["load_pk"].ToS() + "\", \"pId\":\"-1\", \"name\":\"" + ds.Tables[0].Rows[j]["load_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["load_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["load_pk"].ToS() + "\",\"icon\":\"../images/zTreeStyle/diy/load.png\"}";
                                if (j < ds.Tables[0].Rows.Count - 1)
                                    zNodes += ",";
                            }
                            zNodes += "]";
                            restr = zNodes;
                        }
                        break;
                        #endregion
                    }
                #endregion
                #region 客户信息
                case "search_member":
                    {
                        #region 查找客户
                        string member_name = Request["member_name"].ToS();
                        string member_linkman = Request["member_linkman"].ToS();
                        string member_tel = Request["member_tel"].ToS();
                        string member_addr = Request["member_addr"].ToS();   
                        string strSQL = "select (select member_level from t_member_level where member_pk=Convert(varchar(100),a.member_pk) and user_pk='" + Session["UserPK"].ToS() + "') as member_level,* from t_member a  where 1=1";
                        if (member_name != "") strSQL += " and member_name like '%" + member_name + "%'";
                        if (member_linkman != "") strSQL += " and member_linkman like '%" + member_linkman + "%'";
                        if (member_tel != "") strSQL += " and (member_tel like '%" + member_tel + "%' or member_fax like '%" + member_tel + "%')";
                        if (member_addr != "") strSQL += " and member_addr like '%" + member_addr + "%'";
                        strSQL+=" and member_user = '" + Session["UserDept"].ToS() + "'";
                        strSQL += " order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_member_info":
                    {
                        #region 获取单个客户信息
                        string member_pk = Request["member_pk"].ToS();
                        ds = DbHelperSQL.Query("select (select member_level from t_member_level where member_pk=Convert(varchar(100),a.member_pk) and user_pk='" + Session["UserPK"].ToS() + "') as member_level,* from t_member a where member_pk='" + member_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_member":
                    {
                        #region 保存客户信息
                        string member_pk = Request["member_pk"].ToS();
                        string member_level = Request["member_level"].ToS();
                        string member_number = Request["member_number"].ToS();
                        string member_name = Request["member_name"].ToS();
                        string member_linkman = Request["member_linkman"].ToS();
                        string member_addr = Request["member_addr"].ToS();
                        string member_post = Request["member_post"].ToS();
                        string member_tel = Request["member_tel"].ToS();
                        string member_email = Request["member_email"].ToS();
                        string member_fax = Request["member_fax"].ToS();
                        string member_num = Request["member_num"].ToS();
                        string member_bankname = Request["member_bankname"].ToS();
                        string member_bankno = Request["member_bankno"].ToS();
                        string member_rem = Request["member_rem"].ToS();
                        string member_content = Request["member_content"].ToS();
                        string member_affix = Request["member_affix"].ToS();
                        string edit_pk = Request["edit_pk"].ToS();
                        string strSQL = "";
                        string w = "";
                        ArrayList arr = new ArrayList();
                        if (member_pk == "")
                        {
                            member_pk = Guid.NewGuid().ToS();
                            strSQL = "INSERT INTO [t_member]" +
                                     "([member_pk]" +
                                     ",[member_number]" +
                                     ",[member_name]" +
                                     ",[member_linkman]" +
                                     ",[member_addr]" +
                                     ",[member_post]" +
                                     ",[member_tel]" +
                                     ",[member_email]" +
                                     ",[member_fax]" +
                                     ",[member_num]" +
                                     ",[member_bankname]" +
                                     ",[member_bankno]" +
                                     ",[member_rem]" +
                                     ",[member_content]" +
                                     ",[member_affix]" +
                                     ",[member_user]" +
                                     ",[create_time])" +
                               " VALUES" +
                                     "('" + member_pk + "'" +
                                     ",'" + member_number + "'" +
                                     ",'" + member_name + "'" +
                                     ",'" + member_linkman + "'" +
                                     ",'" + member_addr + "'" +
                                     ",'" + member_post + "'" +
                                     ",'" + member_tel + "'" +
                                     ",'" + member_email + "'" +
                                     ",'" + member_fax + "'" +
                                     ",'" + member_num + "'" +
                                     ",'" + member_bankname + "'" +
                                     ",'" + member_bankno + "'" +
                                     ",'" + member_rem + "'" +
                                     ",'" + member_content + "'" +
                                     ",'" + member_affix + "'" +
                                     ",'" + Session["UserDept"].ToS() + "'" +                                     
                                     ",getdate())";
                            w = "1";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_member]" +
                                   " SET [member_number] = '" + member_number + "'" +
                                      ",[member_name] = '" + member_name + "'" +
                                      ",[member_linkman] = '" + member_linkman + "'" +
                                      ",[member_addr] = '" + member_addr + "'" +
                                      ",[member_post] = '" + member_post + "'" +
                                      ",[member_tel] = '" + member_tel + "'" +
                                      ",[member_email] = '" + member_email + "'" +
                                      ",[member_fax] = '" + member_fax + "'" +
                                      ",[member_num] = '" + member_num + "'" +
                                      ",[member_bankname] = '" + member_bankname + "'" +
                                      ",[member_bankno] = '" + member_bankno + "'" +
                                      ",[member_rem] = '" + member_rem + "'" +
                                      ",[member_content] = '" + member_content + "'" +
                                      ",[member_affix] = '" + member_affix + "'" +
                                 " WHERE [member_pk]='" + member_pk + "'";
                        }
                        arr.Add(strSQL);
                        strSQL = "delete from t_member_level where member_pk='" + member_pk + "' and user_pk='" + Session["UserPK"].ToS() + "'";
                        arr.Add(strSQL);
                        strSQL = "insert into t_member_level(ml_pk,member_pk,user_pk,member_level) values(newid(),'" + member_pk + "','" + Session["UserPK"].ToS() + "','" + member_level + "')";
                        arr.Add(strSQL);
                        int i = DbHelperSQL.ExecuteSqlTran(arr);
                        if (i > 0)
                        {
                            if (w=="1")
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "添加客户信息");
                                strSQL = "select (select member_level from t_member_level where member_pk=Convert(varchar(100),a.member_pk) and user_pk='" + Session["UserPK"].ToS() + "') as member_level,* from t_member a  where 1=1";
                                strSQL += " and member_user='" + Session["UserDept"].ToS() + "'";
                                strSQL += " order by create_time desc";
                            }
                            else
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑客户信息");
                                DbHelperSQL.ExecuteSql("update t_edit set edit_show=1 where edit_pk='" + edit_pk + "'");
                                strSQL = "select '' as edit_x_pk,'' as edit_x_field,(select user_name from t_user where convert(varchar(100),user_pk)=a.edit_user) as edit_user_name,case when edit_type=1 then '媒体信息' when edit_type=2 then '合同信息' when edit_type=3 then '收入信息' else '未知' end  as edit_type_name,a.*,b.msource_km,c.pact_number from t_edit a";
                                strSQL += " left join t_msource b on a.edit_m_pk=Convert(varchar(100),b.msource_pk)";
                                strSQL += " left join t_pact c on a.edit_m_pk=Convert(varchar(100),c.pact_pk)";
                                strSQL += " left join t_income d on a.edit_m_pk=Convert(varchar(100),d.income_pk)";
                                strSQL += " where 1=1 ";
                                strSQL += " order by a.create_time desc";
                            }
                           
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_member":
                    {
                        #region 删除客户
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_member where member_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除客户信息");
                            string strSQL = "select (select member_level from t_member_level where member_pk=Convert(varchar(100),a.member_pk) and user_pk='" + Session["UserPK"].ToS() + "') as member_level,* from t_member a where 1=1";
                            strSQL += " and member_user ='" + Session["UserDept"].ToS() + "'";
                            strSQL += " order by create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_member_affix":
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
                #region 组织机构信息
                case "search_org":
                    {
                        #region 获取组织机构信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        ds = DbHelperSQL.Query("select * from t_organization order by org_ord,create_time desc");
                        string zNodes = "[";
                        for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["org_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_org_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["org_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["org_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["org_pk"].ToS() + "\"}";
                            if (j < ds.Tables[0].Rows.Count - 1)
                                zNodes += ",";
                        }
                        zNodes += "]";
                        restr = zNodes;
                        break;
                        #endregion
                    }
                case "get_org_info":
                    {
                        #region 获取单条组织机构信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        string edit_pk = Request["org_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_organization  where org_pk='" + edit_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "save_org":
                    {
                        #region 保存组织机构信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string org_pk = Request["org_pk"].ToS();
                        string parent_org_code = Request["parent_org_code"].ToS();
                        string org_name = Request["org_name"].ToS();
                        string org_role = Request["org_role"].ToS();
                        string org_man = Request["org_man"].ToS();
                        string org_phone = Request["org_phone"].ToS();
                        string org_address = Request["org_address"].ToS();
                        string org_post = Request["org_post"].ToS();
                        string org_fax = Request["org_fax"].ToS();
                        string org_rem = Request["org_rem"].ToS();
                        string org_ord = Request["org_ord"].ToInt32() == 0 ? "1" : Request["org_ord"].ToS();
                        string org_rank = Request["org_rank"].ToS();
                        string org_lock = Request["org_lock"].ToS();
                        string org_code = "";
                        string strSQL = "";
                        if (org_pk == "")
                        {
                            ds = DbHelperSQL.Query("select max(org_code) from t_organization where org_code like '" + parent_org_code + "%' and len(org_code)=" + (parent_org_code.Length + 5).ToS() + "");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string ID = ds.Tables[0].Rows[0][0].ToS();
                                if (ID == "")
                                    org_code = parent_org_code + "00001";
                                else
                                    org_code = parent_org_code + (ID.Substring(ID.Length - 5, 5).ToInt32() + 1).ToS().PadLeft(5, '0');
                            }
                            else
                            {
                                org_code = parent_org_code + "00001";
                            }
                            strSQL = "INSERT INTO [t_organization]" +
                                       "([org_pk]" +
                                       ",[org_code]" +
                                       ",[parent_org_code]" +
                                       ",[org_name]" +
                                       ",[org_role]" +
                                       ",[org_man]" +
                                       ",[org_phone]" +
                                       ",[org_address]" +
                                       ",[org_post]" +
                                       ",[org_fax]" +
                                       ",[org_rem]" +
                                       ",[org_ord]" +
                                       ",[org_rank]" +
                                       ",[org_lock]" +
                                       ",[create_time])" +
                                 " VALUES" +
                                       "(newid()" +
                                       ",'" + org_code + "'" +
                                       ",'" + parent_org_code + "'" +
                                       ",'" + org_name + "'" +
                                       ",'" + org_role + "'" +
                                       ",'" + org_man + "'" +
                                       ",'" + org_phone + "'" +
                                       ",'" + org_address + "'" +
                                       ",'" + org_post + "'" +
                                       ",'" + org_fax + "'" +
                                       ",'" + org_rem + "'" +
                                       "," + org_ord +
                                       "," + org_rank +
                                       "," + org_lock +
                                       ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_organization]" +
                                       "SET [org_name] = '" + org_name + "'" +
                                          ",[org_role] = '" + org_role + "'" +
                                          ",[org_man] = '" + org_man + "'" +
                                          ",[org_phone] = '" + org_phone + "'" +
                                          ",[org_address] = '" + org_address + "'" +
                                          ",[org_post] = '" + org_post + "'" +
                                          ",[org_fax] = '" + org_fax + "'" +
                                          ",[org_rem] = '" + org_rem + "'" +
                                          ",[org_ord] = " + org_ord +
                                          ",[org_rank] = " + org_rank +
                                          ",[org_lock] = " + org_lock +
                                     " WHERE org_pk='" + org_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            if (strSQL.IndexOf("INSERT") > -1)
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "添加组织机构信息");
                            }
                            else
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑组织机构信息");
                            }
                            ds = DbHelperSQL.Query("select * from t_organization order by org_ord,create_time desc");
                            string zNodes = "[";
                            for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["org_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_org_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["org_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["org_name"].ToS() + "\", \"checked\":\"true\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["org_pk"].ToS() + "\"}";
                                if (j < ds.Tables[0].Rows.Count - 1)
                                    zNodes += ",";
                            }
                            zNodes += "]";
                            restr = zNodes;
                        }
                        break;
                        #endregion
                    }
                case "del_org":
                    {
                        #region 删除组织机构信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string del_pk = Request["del_pk"].ToS();
                        DataSet dsa = DbHelperSQL.Query("select parent_org_code,(select count(org_pk) from t_organization where parent_org_code=a.org_code) as child_org_count from t_organization a where org_pk='" + del_pk + "'");
                        if (dsa.Tables[0].Rows.Count > 0)
                        {
                            if (dsa.Tables[0].Rows[0][0].ToS() == "-1")
                            {
                                restr = "{\"state\":\"-100\"}";
                                break;
                            }
                            if (dsa.Tables[0].Rows[0][1].ToInt32() > 0)
                            {
                                restr = "{\"state\":\"-99\"}";
                                break;
                            }
                        }
                        string strSQL = "delete from t_organization where org_pk='" + del_pk + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除组织机构信息");

                            ds = DbHelperSQL.Query("select * from t_organization order by org_ord,create_time desc");
                            string zNodes = "[";
                            for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["org_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_org_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["org_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["org_name"].ToS() + "\", \"checked\":\"true\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["org_pk"].ToS() + "\"}";
                                if (j < ds.Tables[0].Rows.Count - 1)
                                    zNodes += ",";
                            }
                            zNodes += "]";
                            restr = zNodes;
                        }
                        break;
                        #endregion
                    }
                #endregion
                #region 用户帐号信息
                case "search_user":
                    {
                        #region 获取用户帐号信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        string org_pk = Request["org_pk"].ToS();
                        string strSQL = "select * from t_user where 1=1";
                        if (org_pk != "") strSQL += " and org_pk='" + org_pk + "'";
                        strSQL += " order by user_ord,create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_user_info":
                    {
                        #region 获取单条用户帐号信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        string edit_pk = Request["user_pk"].ToS();
                        ds = DbHelperSQL.Query("select (select org_name from t_organization where CONVERT(varchar(100),org_pk)=a.org_pk) as org_name,(select org_name from t_organization where CONVERT(varchar(100),org_pk)=a.user_part) as user_part_name,(select role_name from t_role where CONVERT(varchar(100),role_pk)=a.user_role) as role_name,* from t_user a   where user_pk='" + edit_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "save_user":
                    {
                        #region 保存用户帐号信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string user_pk = Request["user_pk"].ToS();
                        string org_pk = Request["org_pk"].ToS();
                        string user_scope = Request["user_scope"].ToS();
                        string user_name = Request["user_name"].ToS();
                        string user_login_name = Request["user_login_name"].ToS();
                        string user_login_pwd = Tools.EncryptString(Request["user_login_pwd"].ToS(), "15802929");
                        string user_tel = Request["user_tel"].ToS();
                        string user_email = Request["user_email"].ToS();
                        string user_part = Request["user_part"].ToS();
                        string user_role = Request["user_role"].ToS();
                        string user_ord = Request["user_ord"].ToInt32().ToS();
                        string user_rem = Request["user_rem"].ToS();
                        string user_lock = Request["user_lock"].ToS();
                        string change_pwd = Request["change_pwd"].ToS();
                        string strSQL = "";
                        if (user_pk == "")
                        {
                            strSQL = "INSERT INTO [t_user]" +
                                       "([user_pk]" +
                                       ",[org_pk]" +
                                       ",[user_scope]" +
                                       ",[user_name]" +
                                       ",[user_login_name]" +
                                       ",[user_login_pwd]" +
                                       ",[user_tel]" +
                                       ",[user_email]" +
                                       ",[user_part]" +
                                       ",[user_role]" +
                                       ",[user_ord]" +
                                       ",[create_time]" +
                                       ",[user_rem]" +
                                       ",[user_lock])" +
                                 " VALUES" +
                                       "(newid()" +
                                       ",'" + org_pk + "'" +
                                       "," + user_scope +
                                       ",'" + user_name + "'" +
                                       ",'" + user_login_name + "'" +
                                       ",'" + user_login_pwd + "'" +
                                       ",'" + user_tel + "'" +
                                       ",'" + user_email + "'" +
                                       ",'" + user_part + "'" +
                                       ",'" + user_role + "'" +
                                       "," + user_ord +
                                       ",getdate()" +
                                       ",'" + user_rem + "'" +
                                       "," + user_lock + ")";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_user]" +
                                      "SET [org_pk]='" + org_pk + "'" +
                                          ",[user_scope] ='" + user_scope + "'" +
                                          ",[user_name] ='" + user_name + "'" +
                                          ",[user_login_name] ='" + user_login_name + "'" +
                                          ",[user_tel] ='" + user_tel + "'" +
                                          ",[user_email] ='" + user_email + "'" +
                                          ",[user_part] ='" + user_part + "'" +
                                          ",[user_role] ='" + user_role + "'" +
                                          ",[user_ord] ='" + user_ord + "'" +
                                          ",[user_rem] ='" + user_rem + "'" +
                                          ",[user_lock] ='" + user_lock + "'";
                            if (change_pwd == "1") strSQL += ",[user_login_pwd] ='" + user_login_pwd + "'";
                            strSQL += " WHERE user_pk='" + user_pk + "'";
                        }

                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            if (strSQL.IndexOf("INSERT") > -1)
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "添加用户帐号信息");
                            }
                            else
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑用户帐号信息");
                            }
                            ds = DbHelperSQL.Query("select * from t_user  order by user_ord,create_time desc");
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "del_user":
                    {
                        #region 删除用户帐号信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        string strSQL = "delete from t_user where user_pk in (" + del_pk + ")";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除用户帐号信息");

                            ds = DbHelperSQL.Query("select * from t_user order by user_ord,create_time desc");
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                #endregion
                #region 角色信息
                case "save_role_rights":
                    {
                        #region 保存角色权限信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string role_pk = Request["role_pk"].ToS();
                        string rig_val = Request["rig_val"].ToS();
                        string rig_org = Request["rig_org"].ToS();
                        DbHelperSQL.ExecuteSql("delete from t_role_rights where role_pk='" + role_pk + "'");

                        string strSQL = ("INSERT INTO [t_role_rights]" +
                                        "([rig_pk]" +
                                        ",[role_pk]" +
                                        ",[rig_val]" +
                                        ",[rig_org])" +
                                  " VALUES" +
                                        "(newid()" +
                                        ",'" + role_pk + "'" +
                                        ",'" + rig_val + "'" +
                                        ",'" + rig_org + "')");
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑角色权限信息");

                            if (role_pk == Session["UserRole"].ToS())
                                Session["RightVal"] = rig_val;
                            restr = "{\"result\":\"1\"}";
                        }
                        break;
                        #endregion
                    }
                case "get_role_rights":
                    {
                        #region 获取角色权限信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string role_pk = Request["role_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_role_rights  where role_pk='" + role_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_right":
                    {
                        #region 根据栏目号判断各操作按钮权限
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string menu_code = Request["c"].ToS();
                        string right_val = Session["RightVal"].ToS();
                        restr = "{";
                        if (right_val.IndexOf("A0" + menu_code) > -1 || right_val.IndexOf("A1" + menu_code) > -1)
                            restr += "\"add\":\"1\",";
                        else
                            restr += "\"add\":\"0\",";
                        if (right_val.IndexOf("A0" + menu_code) > -1 || right_val.IndexOf("A2" + menu_code) > -1)
                            restr += "\"edit\":\"1\",";
                        else
                            restr += "\"edit\":\"0\",";

                        if (right_val.IndexOf("A0" + menu_code) > -1 || right_val.IndexOf("A3" + menu_code) > -1)
                            restr += "\"del\":\"1\",";
                        else
                            restr += "\"del\":\"0\",";
                        if (right_val.IndexOf("B0" + menu_code) > -1 || right_val.IndexOf("B1" + menu_code) > -1 || right_val.IndexOf("B2" + menu_code) > -1)
                            restr += "\"look\":\"1\",";
                        else
                            restr += "\"look\":\"0\",";
                        restr += "\"lm\":\"\"";
                        restr += "}";
                        break;
                        #endregion
                    }
                case "search_menu":
                    {
                        #region 获取栏目信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                                              
                        string strSQL = "select * from t_role_menu where menu_lock=0";
                        if (Request["t"].ToS() != "1")
                        {
                            strSQL += " and (PATINDEX('%A0'+menu_code+'%','" + Session["RightVal"].ToS() + "')>0 or ";
                            strSQL += " PATINDEX('%A1'+menu_code+'%','" + Session["RightVal"].ToS() + "')>0 or ";
                            strSQL += " PATINDEX('%A2'+menu_code+'%','" + Session["RightVal"].ToS() + "')>0 or ";
                            strSQL += " PATINDEX('%A3'+menu_code+'%','" + Session["RightVal"].ToS() + "')>0 or ";
                            strSQL += " PATINDEX('%A0'+menu_code+'%','" + Session["RightVal"].ToS() + "')>0 or ";
                            strSQL += " PATINDEX('%B0'+menu_code+'%','" + Session["RightVal"].ToS() + "')>0 or ";
                            strSQL += " PATINDEX('%B1'+menu_code+'%','" + Session["RightVal"].ToS() + "')>0 or ";
                            strSQL += " PATINDEX('%B1'+menu_code+'%','" + Session["RightVal"].ToS() + "')>0 )";
                        }
                        strSQL += " order by menu_ord";
                        ds = DbHelperSQL.Query(strSQL);
                        string zNodes = "[";
                        for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            if (Request["t"].ToS() == "1")
                            {
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_menu_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"checked\":\"false\", \"open\":\"false\",\"flag\":\"" + ds.Tables[0].Rows[j]["menu_pk"].ToS() + "\",\"code\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\",\"l\":\"0\"}";
                            }
                            else
                            {
                                if (ds.Tables[0].Rows[j]["menu_type"].ToS() == "1")
                                    zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_menu_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"checked\":\"false\", \"open\":\"false\",\"flag\":\"" + ds.Tables[0].Rows[j]["menu_pk"].ToS() + "\",\"code\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\",\"iconOpen\":\"/images/zTreeStyle/diy/xjt.png\", \"iconClose\":\"/images/zTreeStyle/diy/sjt.png\",\"l\":\"0\",\"url\":\"/info/info_update.aspx?code="+ ds.Tables[0].Rows[j]["menu_code"].ToS() + "\"}";
                                else if (ds.Tables[0].Rows[j]["menu_type"].ToS() == "2")
                                    zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_menu_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"checked\":\"false\", \"open\":\"false\",\"flag\":\"" + ds.Tables[0].Rows[j]["menu_pk"].ToS() + "\",\"code\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\",\"iconOpen\":\"/images/zTreeStyle/diy/xjt.png\", \"iconClose\":\"/images/zTreeStyle/diy/sjt.png\",\"l\":\"0\",\"url\":\"/info/info_single_update.aspx?code=" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\"}";
                                else if (ds.Tables[0].Rows[j]["parent_menu_code"].ToS() == "-1")
                                    zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_menu_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"checked\":\"false\", \"open\":\"false\",\"flag\":\"" + ds.Tables[0].Rows[j]["menu_pk"].ToS() + "\",\"code\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\",\"iconOpen\":\"/images/zTreeStyle/diy/xjt.png\", \"iconClose\":\"/images/zTreeStyle/diy/sjt.png\",\"l\":\"0\",\"url\":\"" + ds.Tables[0].Rows[j]["menu_url"].ToS() + "\"}";
                                else if (ds.Tables[0].Select("parent_menu_code='" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "'").Length > 0)
                                    zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_menu_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"checked\":\"false\", \"open\":\"false\",\"flag\":\"" + ds.Tables[0].Rows[j]["menu_pk"].ToS() + "\",\"code\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\",\"iconOpen\":\"/images/zTreeStyle/diy/open.png\", \"iconClose\":\"/images/zTreeStyle/diy/close.png\",\"l\":\"0\",\"url\":\"" + ds.Tables[0].Rows[j]["menu_url"].ToS() + "\"}";
                                else
                                    zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_menu_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["menu_name"].ToS() + "\", \"checked\":\"false\", \"open\":\"false\",\"flag\":\"" + ds.Tables[0].Rows[j]["menu_pk"].ToS() + "\",\"code\":\"" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\", \"icon\":\"/images/LeftMenuBg5.gif\",\"l\":\"1\",\"url\":\"" + ds.Tables[0].Rows[j]["menu_url"].ToS() + (ds.Tables[0].Rows[j]["menu_url"].ToS().IndexOf('?') > -1 ? "&" : "?") + "i=" + ds.Tables[0].Rows[j]["menu_code"].ToS() + "\"}";
                            }
                            if (j < ds.Tables[0].Rows.Count - 1) zNodes += ",";
                        }
                        zNodes += "]";
                        restr = zNodes;
                        break;
                        #endregion
                    }
                case "search_role":
                    {
                        #region 获取角色信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        ds = DbHelperSQL.Query("select * from t_role order by role_ord");
                        string zNodes = "[";
                        for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["role_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_role_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["role_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["role_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["role_pk"].ToS() + "\",\"type\":\"" + ds.Tables[0].Rows[j]["role_type"].ToS() + "\"}";
                            if (j < ds.Tables[0].Rows.Count - 1)
                                zNodes += ",";
                        }
                        zNodes += "]";
                        restr = zNodes;
                        break;
                        #endregion
                    }
                case "get_role_info":
                    {
                        #region 获取单条角色信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        string edit_pk = Request["role_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_role  where role_pk='" + edit_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "save_role":
                    {
                        #region 保存角色信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查询客户信息");
                        string role_code = "";
                        string role_pk = Request["role_pk"].ToS();
                        string parent_role_code = Request["parent_role_code"].ToS();
                        string role_name = Request["role_name"].ToS();
                        string role_rem = Request["role_rem"].ToS();
                        string role_ord = Request["role_ord"].ToInt32() == 0 ? "1" : Request["role_ord"].ToS();
                        string role_type = Request["role_type"].ToS();
                        if (role_type == "1") parent_role_code = "00000";
                        else parent_role_code = parent_role_code.Substring(0, 10);
                        string strSQL = "";
                        if (role_pk == "")
                        {

                            ds = DbHelperSQL.Query("select max(role_code) from t_role where role_code like '" + parent_role_code + "%' and len(role_code)=" + (parent_role_code.Length + 5).ToS() + "");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string ID = ds.Tables[0].Rows[0][0].ToS();
                                if (ID == "")
                                    role_code = parent_role_code + "00001";
                                else
                                    role_code = parent_role_code + (ID.Substring(ID.Length - 5, 5).ToInt32() + 1).ToS().PadLeft(5, '0');
                            }
                            else
                            {
                                role_code = parent_role_code + "00001";
                            }

                            strSQL = "INSERT INTO [t_role]" +
                                       "([role_pk]" +
                                       ",[role_code]" +
                                       ",[parent_role_code]" +
                                       ",[role_name]" +
                                       ",[role_rem]" +
                                       ",[role_ord]" +
                                       ",[role_type])" +
                                 " VALUES" +
                                       "(newid()" +
                                       ",'" + role_code + "'" +
                                       ",'" + parent_role_code + "'" +
                                       ",'" + role_name + "'" +
                                       ",'" + role_rem + "'" +
                                       "," + role_ord +
                                       "," + role_type + ")";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_role]" +
                                       "SET [role_name] = '" + role_name + "'" +
                                          ",[role_rem] = '" + role_rem + "'" +
                                          ",[role_ord] = '" + role_ord + "'" +
                                          ",[role_type] = '" + role_type + "'" +
                                     " WHERE role_pk='" + role_pk + "'";
                        }

                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            if (strSQL.IndexOf("INSERT") > -1)
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "添加角色信息");
                            }
                            else
                            {
                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑角色信息");
                            }
                            ds = DbHelperSQL.Query("select * from t_role order by role_ord");
                            string zNodes = "[";
                            for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["role_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_role_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["role_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["role_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["role_pk"].ToS() + "\"}";
                                if (j < ds.Tables[0].Rows.Count - 1)
                                    zNodes += ",";
                            }
                            zNodes += "]";
                            restr = zNodes;
                        }
                        break;
                        #endregion
                    }
                case "del_role":
                    {
                        #region 删除角色信息
                        string del_pk = Request["del_pk"].ToS();
                        DataSet dsa = DbHelperSQL.Query("select parent_role_code,(select count(role_pk) from t_role where parent_role_code=a.role_code) as child_role_count from t_role a where role_pk='" + del_pk + "'");
                        if (dsa.Tables[0].Rows.Count > 0)
                        {
                            if (dsa.Tables[0].Rows[0][0].ToS() == "-1")
                            {
                                restr = "{\"state\":\"-100\"}";
                                break;
                            }
                            if (dsa.Tables[0].Rows[0][1].ToInt32() > 0)
                            {
                                restr = "{\"state\":\"-99\"}";
                                break;
                            }
                        }
                        string strSQL = "delete from t_role where role_pk='" + del_pk + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除角色信息");
                            ds = DbHelperSQL.Query("select * from t_role order by role_ord");
                            string zNodes = "[";
                            for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["role_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_role_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["role_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["role_name"].ToS() + "\", \"checked\":\"" + (j == 0 ? "true" : "false") + "\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["role_pk"].ToS() + "\"}";
                                if (j < ds.Tables[0].Rows.Count - 1)
                                    zNodes += ",";
                            }
                            zNodes += "]";
                            restr = zNodes;
                        }
                        break;
                        #endregion
                    }
                case "search_role_name":
                    {
                        #region 获取角色完整名
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        string role_code = Request["role_code"];
                        string role_name="";
                        ds= DbHelperSQL.Query("select * from t_role where role_pk='" + role_code + "'");
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            role_name = ds.Tables[0].Rows[0]["role_name"].ToS();
                            if (ds.Tables[0].Rows[0]["role_code"].ToS().Length>=15)
                                role_name = DbHelperSQL.ExecuteSqlScalar("select role_name from t_role where role_code='" + ds.Tables[0].Rows[0]["role_code"].ToS().Substring(0, 10) + "'").ToS() + "-" + role_name;
                        }
                        restr = "{\"result\":\"" + role_name + "\"}";
                        break;
                        #endregion
                    }
                #endregion
                #region 系统设置
                case "get_sys_manage":
                    {
                        #region 获取系统设置
                        ds = DbHelperSQL.Query("select * from t_system");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "save_sys_manage":
                    {
                        #region 保存系统设置
                        string sys_title = Request["sys_title"].ToS();
                        string sys_cancel = Request["sys_cancel"].ToS();
                        string sys_logo = Request["sys_logo"].ToS();
                        string pact_long = Request["pact_long"].ToInt32().ToS();
                        string strSQL = "update t_system set sys_title='" + sys_title + "',sys_cancel='" + sys_cancel + "',sys_logo='" + sys_logo + "',pact_long='" + pact_long + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑系统设置信息");

                            Session["Sys_Title"] = sys_title;
                            Session["sys_logo"] = sys_logo;
                            Session["sys_sucase"] = sys_cancel;
                            Session["pact_long"] = pact_long;
                            ds = DbHelperSQL.Query("select * from t_system");
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "save_sms_manage":
                    {
                        #region 短信接口
                        string sms_user = Request["sms_user"].ToS();
                        string sms_pass = Request["sms_pass"].ToS();
                        string sms_pass_en = Request["sms_pass_en"].ToS();
                        string sms_send_url = Request["sms_send_url"].ToS();
                        string sms_succ_code = Request["sms_succ_code"].ToS();
                        string sms_sel_url = Request["sms_sel_url"].ToS();
                        string sms_result_start_code = Request["sms_result_start_code"].ToS();
                        string sms_result_end_code = Request["sms_result_end_code"].ToS();
                        string strSQL = "update t_system set ";
                        strSQL += "sms_user = '" + sms_user + "',";
                        strSQL += "sms_pass = '" + sms_pass + "',";
                        strSQL += "sms_pass_en = '" + sms_pass_en + "',";
                        strSQL += "sms_send_url = '" + sms_send_url + "',";
                        strSQL += "sms_succ_code = '" + sms_succ_code + "',";
                        strSQL += "sms_sel_url = '" + sms_sel_url + "',";
                        strSQL += "sms_result_start_code = '" + sms_result_start_code + "',";
                        strSQL += "sms_result_end_code = '" + sms_result_end_code + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            ds = DbHelperSQL.Query("select * from t_system");
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "upload_logo":
                    {
                        #region 上传logo
                        HttpFileCollection files = Request.Files;
                        if (files.Count > 0)
                        {
                            string hz = files[0].FileName.Substring(files[0].FileName.LastIndexOf('.'));
                            if (hz.ToUpper() == ".PNG")
                            {
                                string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + hz;
                                files[0].SaveAs(Server.MapPath("../uploadfile") + "\\sys\\" + filename);
                                restr = "{\"result\":\"" + filename + "\"}";
                                string strSQL = "update t_system set sys_logo='" + filename + "'";
                                DbHelperSQL.ExecuteSql(strSQL);

                                Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑上传系统LOGO");

                            }
                            else restr = "{\"result\":\"-99\"}";

                        }
                        break;
                        #endregion
                    }
                #endregion
                #region 操作日志
                case "search_log":
                    {
                        #region 获取操作日志
                        string log_sdate = Request["log_sdate"].ToS();
                        string log_edate = Request["log_edate"].ToS();
                        string log_user = Request["log_user"].ToS();
                     
                        string strSQL = "select (select user_name from t_user where Convert(varchar(100),user_pk)= a.user_pk) as user_name,* from t_log a where 1=1";
                        if (log_sdate != "") strSQL += " and [create_time]>='" + log_sdate + " 0:00:00'";
                        if (log_edate != "") strSQL += " and [create_time]<='" + log_edate + " 23:59:59'";
                        if (log_user != "") strSQL += " and [user_pk] in (" + log_user + ")";
                        strSQL += " order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                #endregion
                #region 数据库备份
                case "database_bak":
                    {
                        #region 数据库备份
                        if (!Directory.Exists(Server.MapPath("../data_bak")))
                            Directory.CreateDirectory(Server.MapPath("../data_bak"));
                        using (SqlConnection connection = new SqlConnection(DbHelperSQL.Connectionstring))
                        {
                            using (SqlCommand comm = new SqlCommand("use master;backup database @name to disk=@path;", connection))
                            {
                                try
                                {
                                    connection.Open();
                                    comm.Parameters.AddWithValue("@name", (DbHelperSQL.Connectionstring.Split(';')[1]).Split('=')[1]);
                                    comm.Parameters.AddWithValue("@path", Server.MapPath("../data_bak") + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak");
                                    comm.ExecuteNonQuery();

                                    restr = "{\"result\":\"1\"}";
                                }
                                catch (SqlException)
                                {
                                    restr = "{\"result\":\"0\"}";
                                }
                                finally
                                {
                                    comm.Dispose();
                                    connection.Close();
                                }
                            }
                        }
                        break;
                        #endregion
                    }
                case "database_res":
                    {
                        #region 数据库还原
                        string file_name = Request["file_name"].ToS();

                        if(SQLBackupAndRestore.SQLRestoreDB((DbHelperSQL.Connectionstring.Split(';')[0]).Split('=')[1], (DbHelperSQL.Connectionstring.Split(';')[2]).Split('=')[1], (DbHelperSQL.Connectionstring.Split(';')[3]).Split('=')[1], Server.MapPath("../data_bak") + "\\" + file_name, (DbHelperSQL.Connectionstring.Split(';')[1]).Split('=')[1]))

                            restr = "{\"result\":\"1\"}";
                        else
                            restr = "{\"result\":\"0\"}";
                        break;
                        #endregion
                    }

                #endregion
                case "search_org_user":
                    {
                        #region 按组织机构获取用户信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        ds = DbHelperSQL.Query("select * from t_organization order by org_ord,create_time desc");
                        DataSet user_ds = DbHelperSQL.Query("SELECT *  FROM [t_user] order by user_ord,create_time desc");
                        string zNodes = "[";
                        for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["org_code"].ToS() + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["parent_org_code"].ToS() + "\", \"name\":\"" + ds.Tables[0].Rows[j]["org_name"].ToS() + "\", \"title\":\"" + ds.Tables[0].Rows[j]["org_name"].ToS() + "\", \"checked\":\"false\", \"open\":\"true\",\"flag\":\"" + ds.Tables[0].Rows[j]["org_pk"].ToS() + "\",\"u\":\"0\",\"icon\":\"/images/icons/folder.png\"},";
                            DataRow[] dr = user_ds.Tables[0].Select("org_pk='" + ds.Tables[0].Rows[j]["org_pk"].ToS() + "'");
                            int y = 0;
                            foreach (DataRow user_dr in dr)
                            {
                                y++;
                                zNodes += "{\"id\":\"" + ds.Tables[0].Rows[j]["org_code"].ToS() + y.ToS().PadLeft(5, '0') + "\", \"pId\":\"" + ds.Tables[0].Rows[j]["org_code"].ToS() + "\", \"name\":\"" + user_dr["user_name"].ToS() + "\", \"title\":\"" + user_dr["user_name"].ToS() + "\", \"checked\":\"false\", \"open\":\"true\",\"flag\":\"" + user_dr["user_pk"].ToS() + "\",\"u\":\"1\",\"icon\":\"/images/icons/user.png\"},";
                            }
                            if (j == ds.Tables[0].Rows.Count - 1)
                                zNodes = zNodes.Substring(0, zNodes.Length - 1);

                        }
                        zNodes += "]";
                        restr = zNodes;
                        break;
                        #endregion
                    }
                case "search_users":
                    {
                        #region 根据多个PK返回用户名信息
                        //Tools.addlog(Session["CorID"].ToS(), Session["userName"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "查看客户列表");                       
                        string[] user_pks = Request["user_pk"].ToS().Split(',');
                        string userPKs = "";
                        string users = "";
                        for (int i = 0; i < user_pks.Length; i++)
                        {
                            userPKs += "'" + user_pks[i] + "'";
                            if (i > user_pks.Length - 1) userPKs += ",";
                        }
                        ds = DbHelperSQL.Query("select * from t_user where user_pk in (" + userPKs + ") order by create_time desc");
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            users += ds.Tables[0].Rows[i]["user_name"].ToS();
                            if (i > ds.Tables[0].Rows.Count - 1) users += ",";
                        }
                        restr = users;
                        break;
                        #endregion
                    }
                case "get_update":
                    {
                        #region top小秘书更新
                        restr = get_xms();
                        break;
                        #endregion
                    }
                case "search_update":
                    {
                        #region top判断用户是否有更新
                        if (txtinit.IniReadValue(Server.MapPath("../ini/bee.ini"), "消息提醒", Session["UserPK"].ToS()) == "1")
                        {
                            txtinit.IniWriteValue(Server.MapPath("../ini/bee.ini"), "消息提醒", Session["UserPK"].ToS(), "0");
                            restr = get_xms();
                        }
                        else
                            restr = "{\"result\":\"0\"}";
                        break;
                        #endregion
                    }
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
    private string get_xms()
    {
        string s1 = "0", s2 = "0", s3 = "0", s4 = "0", strSQL = "";
        if (Session["RightVal"].ToS().IndexOf("A00001000004") > -1 || Session["RightVal"].ToS().IndexOf("A10001000004") > -1 || Session["RightVal"].ToS().IndexOf("A20001000004") > -1 || Session["RightVal"].ToS().IndexOf("A30001000004") > -1 || Session["RightVal"].ToS().IndexOf("B00001000004") > -1)
        {
            strSQL = "select count(*) from t_message_email a  where email_to_user_pk='" + Session["UserPK"].ToS() + "' and email_temp=0 and email_to_user_del=0 and email_read=0";
            s1 = DbHelperSQL.ExecuteSqlScalar(strSQL).ToS();

        }
        if (Session["RightVal"].ToS().IndexOf("A00000400002") > -1 || Session["RightVal"].ToS().IndexOf("A10000400002") > -1 || Session["RightVal"].ToS().IndexOf("A20000400002") > -1 || Session["RightVal"].ToS().IndexOf("A30000400002") > -1 )
        {
            strSQL = "select count(*) from t_flow a where flow_state=1  and flow_curr_user='" + Session["UserPK"].ToS() + "'";
            s2 = DbHelperSQL.ExecuteSqlScalar(strSQL).ToS();
        }
        if (Session["RightVal"].ToS().IndexOf("A000002") > -1 || Session["RightVal"].ToS().IndexOf("A100002") > -1 || Session["RightVal"].ToS().IndexOf("A200002") > -1 || Session["RightVal"].ToS().IndexOf("A300002") > -1 || Session["RightVal"].ToS().IndexOf("B000002") > -1)
        {
            strSQL = "select count(*) from t_pact a where pact_zs=1 and pact_edate>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            strSQL += " and DATEDIFF(day,'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.Year + "'+substring(pact_fdate,5,7))<=" + Session["sys_sucase"].ToS() + " and DATEDIFF(day,'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.Year + "'+substring(pact_fdate,5,7))>0 and (select count(*) from t_income where year(income_date)='" + DateTime.Now.Year + "' and pact_pk=convert(varchar(100),a.pact_pk))=0";
            s3 = DbHelperSQL.ExecuteSqlScalar(strSQL).ToS();
        }

        strSQL = "select 0";
        if (Session["RightVal"].ToS().IndexOf("A00001000002") > -1 || Session["RightVal"].ToS().IndexOf("A10001000002") > -1 || Session["RightVal"].ToS().IndexOf("A20001000002") > -1 || Session["RightVal"].ToS().IndexOf("A30001000002") > -1 || Session["RightVal"].ToS().IndexOf("B00001000002") > -1)
        {
            strSQL = "select (select count(*) from t_message_notice a where ([infor_user_pk]='" + Session["UserPK"].ToS() + "' or Convert(varchar(5000),[notice_to_user_pk]) like '%" + Session["UserPK"].ToS() + "%' or Convert(varchar(5000),notice_to_user_name)='全部') and (select count(*) from t_data_look where [data_pk]=Convert(varchar(100),a.notice_pk) and [user_pk]='" + Session["UserPK"].ToS() + "')=0)";
        }
        
        strSQL += "+";

        if (Session["RightVal"].ToS().IndexOf("A00001000001") > -1 || Session["RightVal"].ToS().IndexOf("A10001000001") > -1 || Session["RightVal"].ToS().IndexOf("A20001000001") > -1 || Session["RightVal"].ToS().IndexOf("A30001000001") > -1 || Session["RightVal"].ToS().IndexOf("B00001000001") > -1)
        {
            strSQL += "(select count(*) from t_message_inform a where ([infor_user_pk]='" + Session["UserPK"].ToS() + "' or Convert(varchar(5000),[inform_to_user_pk]) like '%" + Session["UserPK"].ToS() + "%' or Convert(varchar(5000),inform_to_user_name)='全部') and (select count(*) from t_data_look where [data_pk]=Convert(varchar(100),a.inform_pk) and [user_pk]='" + Session["UserPK"].ToS() + "')=0)";
        }
        else
        {
            strSQL += "0";
        }
        s4 = DbHelperSQL.ExecuteSqlScalar(strSQL).ToS();

        return "{\"result\":\"1\",\"result1\":\"" + s1 + "\",\"result2\":\"" + s2 + "\",\"result3\":\"" + s3 + "\",\"result4\":\"" + s4 + "\"}";
    }
}
