using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class stat_getdata : System.Web.UI.Page
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


                case "stat_coach_count":
                    {
                        #region 统计司机数量
                        string stat = Request["stat"].ToS();
                        string stat_sdate = Request["stat_sdate"].ToS();
                        string stat_edate = Request["stat_edate"].ToS();
                        string coach_state = Request["coach_state"].ToS();
                        string strSQL = "", strWhere="";
                        if (stat_sdate != "")
                        {
                            strWhere += " and create_time>='" + stat_sdate + "'";
                        }
                        if (stat_edate != "")
                        {
                            strWhere += " and create_time<='" + stat_edate + "'";
                        }
                        if (coach_state != "")
                        {
                            strWhere += " and coach_state='" + coach_state + "'";
                        }
                        if (stat == "0")
                            strSQL = "select Convert(varchar(100),create_time,23) as date_type,count(*) as count from t_coach where 1=1 " + strWhere + " group by Convert(varchar(100),create_time,23) order by Convert(varchar(100),create_time,23) desc";
                        else if (stat == "1")
                            strSQL = "select Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(MONTH,create_time)) as date_type,count(*) as count from t_coach where 1=1 " + strWhere + " group by  Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(MONTH,create_time)) order by Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(MONTH,create_time)) desc";
                        else if (stat == "2")
                            strSQL = "select Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(quarter,create_time)) as date_type,count(*) as count from t_coach where 1=1 " + strWhere + " group by  Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(quarter,create_time)) order by Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(quarter,create_time)) desc";
                        else if (stat == "3")
                            strSQL = "select Convert(varchar(100),datepart(YEAR,create_time)) as date_type,count(*) as count from t_coach group by  Convert(varchar(100),datepart(YEAR,create_time)) order by Convert(varchar(100),datepart(YEAR,create_time)) desc";
                       
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "stat_student_count":
                    {
                        #region 统计客户注册量
                        string stat = Request["stat"].ToS();
                        string stat_sdate = Request["stat_sdate"].ToS();
                        string stat_edate = Request["stat_edate"].ToS();
                        string student_state = Request["student_state"].ToS();
                        string strSQL = "", strWhere = "";
                        if (stat_sdate != "")
                        {
                            strWhere += " and create_time>='" + stat_sdate + "'";
                        }
                        if (stat_edate != "")
                        {
                            strWhere += " and create_time<='" + stat_edate + "'";
                        }
                        if (student_state != "")
                        {
                            strWhere += " and student_state='" + student_state + "'";
                        }
                        if (stat == "0")
                            strSQL = "select Convert(varchar(100),create_time,23) as date_type,count(*) as count from t_student where 1=1 " + strWhere + " group by Convert(varchar(100),create_time,23) order by Convert(varchar(100),create_time,23) desc";
                        else if (stat == "1")
                            strSQL = "select Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(MONTH,create_time)) as date_type,count(*) as count from t_student where 1=1 " + strWhere + " group by  Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(MONTH,create_time)) order by Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(MONTH,create_time)) desc";
                        else if (stat == "2")
                            strSQL = "select Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(quarter,create_time)) as date_type,count(*) as count from t_student where 1=1 " + strWhere + " group by  Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(quarter,create_time)) order by Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(quarter,create_time)) desc";
                        else if (stat == "3")
                            strSQL = "select Convert(varchar(100),datepart(YEAR,create_time)) as date_type,count(*) as count from t_student group by  Convert(varchar(100),datepart(YEAR,create_time)) order by Convert(varchar(100),datepart(YEAR,create_time)) desc";

                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "stat_order_count":
                    {
                        #region 统计订单数量
                        string stat = Request["stat"].ToS();
                        string stat_sdate = Request["stat_sdate"].ToS();
                        string stat_edate = Request["stat_edate"].ToS();
                        string order_state = Request["order_state"].ToS();
                        string strSQL = "", strWhere = "";
                        if (stat_sdate != "")
                        {
                            strWhere += " and create_time>='" + stat_sdate + "'";
                        }
                        if (stat_edate != "")
                        {
                            strWhere += " and create_time<='" + stat_edate + "'";
                        }
                        if (order_state != "")
                        {
                            strWhere += " and order_state='" + order_state + "'";
                        }
                        if (stat == "0")
                            strSQL = "select Convert(varchar(100),create_time,23) as date_type,count(*) as count from t_order where 1=1 " + strWhere + " group by Convert(varchar(100),create_time,23) order by Convert(varchar(100),create_time,23) desc";
                        else if (stat == "1")
                            strSQL = "select Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(MONTH,create_time)) as date_type,count(*) as count from t_order where 1=1 " + strWhere + " group by  Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(MONTH,create_time)) order by Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(MONTH,create_time)) desc";
                        else if (stat == "2")
                            strSQL = "select Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(quarter,create_time)) as date_type,count(*) as count from t_order where 1=1 " + strWhere + " group by  Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(quarter,create_time)) order by Convert(varchar(100),datepart(YEAR,create_time))+'-'+Convert(varchar(100),datepart(quarter,create_time)) desc";
                        else if (stat == "3")
                            strSQL = "select Convert(varchar(100),datepart(YEAR,create_time)) as date_type,count(*) as count from t_order group by  Convert(varchar(100),datepart(YEAR,create_time)) order by Convert(varchar(100),datepart(YEAR,create_time)) desc";

                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "stat_order":
                    {
                        #region 统计司机数量
                        string coach = Request["coach"].ToS();
                        string stat_sdate = Request["stat_sdate"].ToS();
                        string stat_edate = Request["stat_edate"].ToS();
                        string order_state = Request["order_state"].ToS();
                        string strSQL = "", strWhere = "",strField="";
                        if (coach != "")
                        {
                            strWhere += " and (b.coach_name like '%"+coach+ "%' or b.coach_phone like '%" + coach + "%')";
                        }
                        if (stat_sdate != "")
                        {
                            strWhere += " and a.create_time>='" + stat_sdate + "'";
                        }
                        if (stat_edate != "")
                        {
                            strWhere += " and a.create_time<='" + stat_edate + "'";
                        }
                        if (order_state != "")
                        {
                            strWhere += " and a.order_state='" + order_state + "'";
                        }
                        strField += "sum(Convert(float,isnull(order_away,0))) as order_away,";
                        strField += "sum(Convert(float,isnull(order_time,0))) as order_time,";
                        strField += "sum(Convert(float,isnull(order_away_fee,0))) as order_away_fee,";
                        strField += "sum(Convert(float,isnull(order_time_fee,0))) as order_time_fee,";
                        strField += "sum(Convert(float,isnull(order_far_away_fee,0))) as order_far_away_fee,";
                        strField += "sum(Convert(float,isnull(order_cut_fee,0))) as order_cut_fee,";
                        strField += "sum(Convert(float,isnull(order_fee,0))) as order_fee";


                        strSQL = "select b.coach_name,b.coach_phone," + strField + " from t_order a  left join t_coach b on a.coach_pk=Convert(varchar(100),b.coach_pk) where 1=1 " + strWhere + " group by b.coach_name,b.coach_phone order by sum(Convert(float,isnull(order_fee,0))) desc";
                      
                            
                       
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
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
}
