using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;

public partial class order_getdata : System.Web.UI.Page
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
               
                #region 订单信息
                case "get_user":
                    {
                        #region 录入订单时查询乘车人姓名、PK
                        string user_tel = Request["user_tel"].ToS();
                        ds = DbHelperSQL.Query("select * from t_student where student_phone='"+ user_tel +"'");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            restr = "{\"user_pk\":\"" + ds.Tables[0].Rows[0]["student_pk"].ToS() + "\",\"user_name\":\"" + ds.Tables[0].Rows[0]["student_name"].ToS() + "\"}";
                        }
                        break;
                        #endregion
                    }
                case "search_order":
                    {
                        #region 查找订单
                        string user = Request["user"].ToS();
                        string coach = Request["coach"].ToS();
                        string order_state = Request["order_state"].ToS();
                        string order_sdate = Request["order_sdate"].ToS();
                        string order_edate = Request["order_edate"].ToS();

                        string strSQL = "select a.*,b.coach_name,b.coach_phone,b.coach_car_number" +
                            " from t_order a left join t_coach b on a.coach_pk=Convert(varchar(100),b.coach_pk) where 1=1 ";
                        if (user != "") strSQL += " and (a.user_name like '%" + user + "%' or a.user_tel like '%" + user + "%')";
                        if (order_state == "2") strSQL += " and a.order_state in ('1','2')";
                        else if (order_state != "") strSQL += " and a.order_state='" + order_state + "'";
                        if (order_sdate != "") strSQL += " and a.create_time>='" + order_sdate + "'";
                        if (order_edate != "") strSQL += " and a.create_time<='" + order_edate + "'";
                        if (coach != "")
                        {
                            strSQL += " and a.coach_pk in (select coach_pk from t_coach where coach_name like '%" + coach + "%' or coach_phone like '%" + coach + "%' or coach_car_number like '%" + coach + "%')";                            
                        }
                        if (Session["order_state"].ToS() != "") strSQL += " and order_state in (" + Session["order_state"].ToS() + ")"; 
                        strSQL += " order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
              
                case "get_order_info":
                    {
                        #region 获取单个订单信息
                        string order_pk = Request["order_pk"].ToS();
                        string strSQL = "select a.*,b.coach_name,b.coach_phone,b.coach_car_number,c.car_name" +
                            " from t_order a left join t_coach b on a.coach_pk=Convert(varchar(100),b.coach_pk)  left join t_car c on (select Convert(varchar(100),car_pk) from t_order_car where order_pk=a.order_pk)=Convert(varchar(100),c.car_pk) where a.order_pk='" + order_pk + "' ";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_order":
                    {
                        #region 保存订单信息
                        string order_pk = Request["order_pk"].ToS();
                        string user_pk = Request["user_pk"].ToS();
                        string user_name = Request["user_name"].ToS();
                        string user_tel = Request["user_tel"].ToS();
                        string user_sms = Request["user_sms"].ToS();
                        string order_type = Request["order_type"].ToS();
                        string order_datetime = Request["order_datetime"].ToS();
                        string start_address = Request["start_address"].ToS();
                        string start_lon = Request["start_lon"].ToS();
                        string start_lat = Request["start_lat"].ToS();
                        string end_address = Request["end_address"].ToS();
                        string end_lon = Request["end_lon"].ToS();
                        string end_lat = Request["end_lat"].ToS();
                        string order_away = Request["order_away"].ToS();
                        string order_time = Request["order_time"].ToS();
                        string car = Request["car"].ToS();
                        double car_meal_fee =0;
                        double order_away_fee = 0;
                        double order_time_fee = 0;
                        double order_far_away_fee = 0;
                        double order_cut_fee = 0;
                        double order_fee = 0;
                        string order_rem = Request["order_rem"].ToS();
                        ArrayList arr = new ArrayList();
                        string strSQL = "";
                        if (order_pk == "")
                        {
                            order_pk = Guid.NewGuid().ToS();
                            strSQL = "INSERT INTO [t_order]" +
                                "([order_pk]" +
                                ",[user_pk]" +
                                ",[user_name]" +
                                ",[user_tel]" +
                                ",[user_sms]" +
                                ",[order_type]" +
                                ",[order_datetime]" +
                                ",[start_address]" +
                                ",[start_lon]" +
                                ",[start_lat]" +
                                ",[end_address]" +
                                ",[end_lon]" +
                                ",[end_lat]" +
                                ",[order_away]" +
                                ",[order_time]" +
                                
                                ",[car_meal_fee]" +
                                ",[order_away_fee]" +
                                ",[order_time_fee]" +
                                ",[order_far_away_fee]" +
                                ",[order_cut_fee]" +
                                ",[order_fee]" +
                                ",[order_rem]" +
                                ",[order_state]" +
                                ",[create_time])" +
                            " VALUES" +
                                "('"+ order_pk+"'" +
                                ",'" + user_pk + "'" +
                                ",'" + user_name + "'" +
                                ",'" + user_tel + "'" +
                                ",'" + user_sms + "'" +
                                ",'" + order_type + "'" +
                                ",'" + order_datetime + "'" +
                                ",'" + start_address + "'" +
                                ",'" + start_lon + "'" +
                                ",'" + start_lat + "'" +
                                ",'" + end_address + "'" +
                                ",'" + end_lon + "'" +
                                ",'" + end_lat + "'" +
                                ",'" + order_away + "'" +
                                ",'" + order_time + "'" +
                              
                                "," + car_meal_fee + "" +
                                "," + order_away_fee + "" +
                                "," + order_time_fee + "" +
                                "," + order_far_away_fee + "" +
                                "," + order_cut_fee + "" +
                                "," + order_fee + "" +
                                ",'" + order_rem + "'" +
                                ",'0'" +
                                ",getdate())";
                            arr.Add(strSQL);
                            string[] car_arr = car.Split(',');
                            for (int i = 0; i < car_arr.Length; i++)
                            {
                                if (car_arr[i] == "") continue;
                                strSQL = "INSERT INTO [t_order_car]" +
                                            "([oc_pk]" +
                                            ",[order_pk]" +
                                            ",[car_pk]" +
                                            ",[create_time])" +
                                            " VALUES" +
                                            "(newid()" +
                                            ",'" + order_pk + "'" +
                                            ",'" + car_arr[i] + "'" +
                                            ",getdate())";
                                arr.Add(strSQL);

                            }
                        }
                        
                        int j = DbHelperSQL.ExecuteSqlTran(arr);
                        if (j > 0)
                        {
                            restr = "{\"result\":\"100\"}";
                            //推送
                            ds = DbHelperSQL.Query("select * from t_coach where coach_state='1'");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["coach_clientID"].ToS() == "") continue;
                                string[] arg = new string[2];
                                arg[0] = ds.Tables[0].Rows[i]["coach_clientID"].ToS();
                                arg[1] = order_pk;
                                Tools.StartProcess(Server.MapPath("/") + "/getui/GetuiServerApiSDKDemo.exe", arg);
                            }
                        }

                        break;
                        #endregion
                    }
              
             
                case "del_order":
                    {
                        #region 删除订单
                        string del_pk = Request["del_pk"].ToS();
                        string user = Request["user"].ToS();
                        string coach = Request["coach"].ToS();
                        string order_state = Request["order_state"].ToS();
                        string order_sdate = Request["order_sdate"].ToS();
                        string order_edate = Request["order_edate"].ToS();

                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_order where order_pk in (" + del_pk + ");delete from t_order_car where order_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                         
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除订单信息");

                            string strSQL = "select a.*,b.coach_name,b.coach_phone,b.coach_car_number" +
                                                " from t_order a left join t_coach b on a.coach_pk=Convert(varchar(100),b.coach_pk) where 1=1 ";
                            if (user != "") strSQL += " and (a.user_name like '%" + user + "%' or a.user_tel like '%" + user + "%')";
                            if (order_state == "2") strSQL += " and a.order_state in ('1','2')";
                            else if (order_state != "") strSQL += " and a.order_state='" + order_state + "'";
                            if (order_sdate != "") strSQL += " and a.create_time>='" + order_sdate + "'";
                            if (order_edate != "") strSQL += " and a.create_time<='" + order_edate + "'";
                            if (coach != "")
                            {
                                strSQL += " and a.coach_pk in (select coach_pk from t_coach where coach_name like '%" + coach + "%' or coach_phone like '%" + coach + "%' or coach_car_number like '%" + coach + "%')";
                            }
                            if (Session["order_state"].ToS() != "") strSQL += " and order_state in (" + Session["order_state"].ToS() + ")";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break; 
                        #endregion
                    }
                case "cog_order":
                    {
                        #region 取消订单
                        string del_pk = Request["del_pk"].ToS();
                        string user = Request["user"].ToS();
                        string coach = Request["coach"].ToS();
                        string order_state = Request["order_state"].ToS();
                        string order_sdate = Request["order_sdate"].ToS();
                        string order_edate = Request["order_edate"].ToS();

                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("update t_order  set order_state=5  where order_pk in (" + del_pk + ");");
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除订单信息");
                            string strSQL = "select a.*,b.coach_name,b.coach_phone,b.coach_car_number" +
                                                " from t_order a left join t_coach b on a.coach_pk=Convert(varchar(100),b.coach_pk) where 1=1 ";
                            if (user != "") strSQL += " and (a.user_name like '%" + user + "%' or a.user_tel like '%" + user + "%')";
                            if (order_state == "2") strSQL += " and a.order_state in ('1','2')";
                            else if (order_state != "") strSQL += " and a.order_state='" + order_state + "'";
                            if (order_sdate != "") strSQL += " and a.create_time>='" + order_sdate + "'";
                            if (order_edate != "") strSQL += " and a.create_time<='" + order_edate + "'";
                            if (coach != "")
                            {
                                strSQL += " and a.coach_pk in (select coach_pk from t_coach where coach_name like '%" + coach + "%' or coach_phone like '%" + coach + "%' or coach_car_number like '%" + coach + "%')";
                            }
                            if (Session["order_state"].ToS() != "") strSQL += " and order_state in (" + Session["order_state"].ToS() + ")";
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
