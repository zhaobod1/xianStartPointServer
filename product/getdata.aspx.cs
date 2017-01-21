using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;

public partial class product_getdata : System.Web.UI.Page
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
               
                #region 商品信息
                case "search_product":
                    {
                        #region 查找商品

                        string strSQL = "select (select count(*) from [t_credit] where [product_pk]=a.[product_pk]) as product_volume,* from t_product a  where 1=1 ";                      
                        strSQL += " order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
              
                case "get_product_info":
                    {
                        #region 获取单个商品信息
                        string product_pk = Request["product_pk"].ToS();
                        string strSQL = "select (select count(*) from [t_credit] where [product_pk]=a.[product_pk]) as product_volume,* from t_product a where product_pk='" + product_pk + "' ";
                       
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_product":
                    {
                        #region 保存商品信息
                        string product_pk = Request["product_pk"].ToS();
                        string product_name = Request["product_name"].ToS();
                        string product_pic = Request["product_pic"].ToS();
                        string product_credit = Request["product_credit"].ToS();
                        string product_count = Request["product_count"].ToS();
                        string product_rem = Request["product_rem"].ToS();
                
                        string strSQL = "";
                        if (product_pk == "")
                        {
                            strSQL = "INSERT INTO [t_product]" +
                            "([product_pk]" +
                            ",[product_name]" +
                            ",[product_pic]" +
                            ",[product_credit]" +
                            ",[product_count]" +
                            ",[product_rem]" +
                            ",[create_time])" +
                            " VALUES" +
                            "(newid()" +
                            ",'" + product_name + "'" +
                            ",'" + product_pic + "'" +
                            ",'" + product_credit + "'" +
                            ",'" + product_count + "'" +
                            ",'" + product_rem + "'" +
                            ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_product]" +
                            " SET [product_name] = '" + product_name + "'" +
                            ",[product_pic] = '" + product_pic + "'" +
                            ",[product_credit] = '" + product_credit + "'" +
                            ",[product_count] = '" + product_count + "'" +
                            ",[product_rem] = '" + product_rem + "'" +
                            " WHERE [product_pk]='" + product_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            strSQL = "select (select count(*) from [t_credit] where [product_pk]=a.[product_pk]) as product_volume,* from t_product a where 1=1 ";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }

                        break;
                        #endregion
                    }
              
             
                case "del_product":
                    {
                        #region 删除商品
                        string del_pk = Request["del_pk"].ToS();
                        string z = Request["z"].ToS();

                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_product where product_pk in (" + del_pk + ");");
                        if (i > 0)
                        {
                            string strSQL = "select (select count(*) from [t_credit] where [product_pk]=a.[product_pk]) as product_volume,* from t_product a where 1=1 ";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break; 
                        #endregion
                    }
            
                #endregion                                       
                #region 兑换记录
                case "search_convert":
                    {
                     
                        string key = Request["key"].ToS();
                        string strSQL = "select b.student_real_name,b.student_phone,(select product_name from t_product where product_pk=a.product_pk) as product_name,a.*" +
                            " from t_credit a left join t_student b on a.user_pk=b.student_pk where 1=1 ";
                      
                        if (key != "")
                        {
                            strSQL += " and (a.user_pk in (select student_pk from t_student where student_name like '%" + key + "%' or student_real_name like '%" + key + "%' or student_phone like '%" + key + "%')";
                            strSQL += " or a.product_pk in (select product_pk from t_product where product_name like '%" + key + "%'))";
                        }

                        strSQL += " order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                      
                        break;
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
