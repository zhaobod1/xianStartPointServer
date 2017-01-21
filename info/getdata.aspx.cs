using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class info_getdata : System.Web.UI.Page
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

                #region 驾校信息
                case "search_driving":
                    {
                        #region 查找驾校
                        string p = Request["p"].ToS();
                        string c = Request["c"].ToS();
                        string q = Request["q"].ToS();
                        string strSQL = "select (select ProvinceName from S_Province where ProvinceID=a.p_id) as p,(select CityName from S_City where CityID=a.c_id) as c,(select DistrictName from S_District where DistrictID=a.area_id) as q,* from t_driving a where 1=1";
                        if (q != "" && q != "null") strSQL += " and area_id=" + q;
                        if (c != "" && c!="null") strSQL += " and c_id=" + c;
                        if (p != "" && p != "null") strSQL += " and p_id=" + p;
                        strSQL += " order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
              
                case "get_driving_info":
                    {
                        #region 获取单个驾校信息
                        string driving_pk = Request["driving_pk"].ToS();
                        string strSQL = "select (select ProvinceName from S_Province where ProvinceID=a.p_id) as p,(select CityName from S_City where CityID=a.c_id) as c,(select DistrictName from S_District where DistrictID=a.area_id) as q,* from t_driving a  where driving_pk='" + driving_pk + "'";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_driving":
                    {
                        #region 保存驾校信息
                        string driving_pk = Request["driving_pk"].ToS();
                        string driving_name = Request["driving_name"].ToS();
                        string driving_address = Request["driving_address"].ToS();
                        string driving_tel = Request["driving_tel"].ToS();
                        string driving_pic = Request["driving_pic"].ToS();
                        string driving_info = Request["driving_info"].ToS();
                        string driving_lat = Request["driving_lat"].ToS();
                        string driving_merge_count = Request["driving_merge_count"].ToS();
                        string area_id = Request["area_id"].ToS();
                        string p_id = Request["p_id"].ToS();
                        string c_id = Request["c_id"].ToS();
                        string strSQL = "";
                        if (driving_pk == "")
                        {
                            strSQL = "INSERT INTO [t_driving]" +
                            "([driving_pk]" +
                            ",[area_id]" +
                            ",[p_id]" +
                            ",[c_id]" +
                            ",[driving_name]" +
                            ",[driving_address]" +
                            ",[driving_tel]" +
                            ",[driving_pic]" +
                            ",[driving_info]" +
                            ",[driving_lat]" +
                            ",[driving_merge_count]" +
                            ",[create_time])" +
                            " VALUES" +
                            "(newid()" +
                            "," + area_id + "" +
                            "," + p_id + "" +
                            "," + c_id + "" +
                            ",'" + driving_name + "'" +
                            ",'" + driving_address + "'" +
                            ",'" + driving_tel + "'" +
                            ",'" + driving_pic + "'" +
                            ",'" + driving_info + "'" +
                            ",'" + driving_lat + "'" +
                            ",'" + driving_merge_count + "'" +
                            ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_driving]" +
                            " SET [driving_name] = '" + driving_name + "'" +
                            ",[driving_address] = '" + driving_address + "'" +
                            ",[driving_tel] = '" + driving_tel + "'" +
                            ",[driving_pic] = '" + driving_pic + "'" +
                            ",[area_id] = " + area_id + "" +
                            ",[p_id] = " + p_id + "" +
                            ",[c_id] = " + c_id + "" +
                            ",[driving_info] = '" + driving_info + "'" +
                            ",[driving_lat] = '" + driving_lat + "'" +
                            ",[driving_merge_count] = '" + driving_merge_count + "'" +
                            " WHERE [driving_pk]='" + driving_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                             strSQL = "select * from t_driving a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }

                        break;
                        #endregion
                    }
                case "del_driving":
                    {
                        #region 删除驾校
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_driving where driving_pk in (" + del_pk + ")");
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除驾校信息");

                            string strSQL = "select * from t_driving a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "get_Province":
                    {
                        #region 获取省份

                        ds = DbHelperSQL.Query("select * from S_Province");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_City":
                    {
                        #region 获取城市
                        string v = Request["v"].ToS();
                        ds = DbHelperSQL.Query("select * from S_City where ProvinceID=" + v + "");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_District":
                    {
                        #region 获取区县
                        string v = Request["v"].ToS();
                        ds = DbHelperSQL.Query("select * from S_District where CityID=" + v + "");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_Driving":
                    {
                        #region 获取驾校
                        string v = Request["v"].ToS();
                        ds = DbHelperSQL.Query("select * from t_driving where area_id=" + v + "");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                #endregion
                #region 教练信息
                case "search_coach":
                    {
                        #region 查找教练
                        string coach_name = Request["coach_name"].ToS();
                        string coach_phone = Request["coach_phone"].ToS();
                        string coach_state = Request["coach_state"].ToS();

                        string strSQL = "select case when DATEDIFF(SECOND,coah_actity_time,GETDATE())<10 then 1 else 0 end as coach_online,* from t_coach a where 1=1";
                        if (coach_name != "") strSQL += " and coach_name like '%" + coach_name + "%'";
                        if (coach_phone != "") strSQL += " and coach_phone like '%" + coach_phone + "%'";
                        if (coach_state != "") strSQL += " and coach_state = '" + coach_state + "'";

                        strSQL += " order by a.[create_time] desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "get_coach_info":
                    {
                        #region 获取单个教练信息
                        string coach_pk = Request["coach_pk"].ToS();
                        string strSQL = "select (select car_name from t_car where Convert(varchar(100),car_pk)=a.coach_car_type) as coach_car_name,* from t_coach a  where coach_pk='" + coach_pk + "'";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_coach":
                    {
                        #region 保存教练信息
                        string coach_pk = Request["coach_pk"].ToS();
                        string driving_pk = Request["driving_pk"].ToS();
                        string coach_incode = Request["coach_incode"].ToS();
                        string coach_city = Request["coach_city"].ToS();
                        string coach_car_number = Request["coach_car_number"].ToS();
                        string coach_car_type = Request["coach_car_type"].ToS();
                        string coach_name = Request["coach_name"].ToS();
                        string coach_pwd = Request["coach_pwd"].ToS();
                        string coach_sex = Request["coach_sex"].ToS();
                        string coach_age = Request["coach_age"].ToS();
                        string coach_phone = Request["coach_phone"].ToS();
                        string coach_long = Request["coach_long"].ToS();
                        string coach_number = Request["coach_number"].ToS();
                        string coach_teacher_number = Request["coach_teacher_number"].ToS();
                        string coach_myself = Request["coach_myself"].ToS();
                        string coach_pic = Request["coach_pic"].ToS();
                        string coach_teacher_pic = Request["coach_teacher_pic"].ToS();
                        string coach_driver_pic1 = Request["coach_driver_pic1"].ToS();
                        string coach_driver_pic2 = Request["coach_driver_pic2"].ToS();
                        string coach_card_pic1 = Request["coach_card_pic1"].ToS();
                        string coach_card_pic2 = Request["coach_card_pic2"].ToS();
                        string coach_subject = Request["coach_subject"].ToS();
                        string coach_price = Request["coach_price"].ToS();
                        string coach_score = Request["coach_score"].ToS();
                        string coach_service = Request["coach_service"].ToS();
                        string coach_type = Request["coach_type"].ToS();
                        string coach_order_range = Request["coach_order_range"].ToS();
                        string coach_place = Request["coach_place"].ToS();
                        string coach_state = Request["coach_state"].ToS();
                       
                        string strSQL = "";
                        if (coach_pk == "")
                        {
                            strSQL = "INSERT INTO [t_coach]" +
                                         "([coach_pk]" +
                                         ",[driving_pk]" +
                                         ",[coach_incode]" +
                                         ",[coach_city]" +
                                         ",[coach_car_number]" +
                                         ",[coach_car_type]" +
                                         ",[coach_name]" +
                                         ",[coach_pwd]" +
                                         ",[coach_sex]" +
                                         ",[coach_age]" +
                                         ",[coach_phone]" +
                                         ",[coach_long]" +
                                         ",[coach_number]" +
                                         ",[coach_teacher_number]" +
                                         ",[coach_myself]" +
                                         ",[coach_pic]" +
                                         ",[coach_teacher_pic]" +
                                         ",[coach_driver_pic1]" +
                                         ",[coach_driver_pic2]" +
                                         ",[coach_card_pic1]" +
                                         ",[coach_card_pic2]" +
                                         ",[coach_subject]" +
                                         ",[coach_price]" +
                                         ",[coach_score]" +
                                         ",[coach_service]" +
                                         ",[coach_type]" +
                                         ",[coach_order_range]" +
                                         ",[coach_place]" +
                                         ",[coach_state]" +
                                         ",[create_time])" +
                                    " VALUES" +
                                         "(newid()" +
                                         ",'" + driving_pk + "'" +
                                         ",'" + coach_incode + "'" +
                                         ",'" + coach_city + "'" +
                                         ",'" + coach_car_number + "'" +
                                         ",'" + coach_car_type + "'" +
                                         ",'" + coach_name + "'" +
                                         ",'" + coach_pwd + "'" +
                                         ",'" + coach_sex + "'" +
                                         ",'" + coach_age + "'" +
                                         ",'" + coach_phone + "'" +
                                         ",'" + coach_long + "'" +
                                         ",'" + coach_number + "'" +
                                         ",'" + coach_teacher_number + "'" +
                                         ",'" + coach_myself + "'" +
                                         ",'" + coach_pic + "'" +
                                         ",'" + coach_teacher_pic + "'" +
                                         ",'" + coach_driver_pic1 + "'" +
                                         ",'" + coach_driver_pic2 + "'" +
                                         ",'" + coach_card_pic1 + "'" +
                                         ",'" + coach_card_pic2 + "'" +
                                         ",'" + coach_subject + "'" +
                                         ",'" + coach_price + "'" +
                                         ",'" + coach_score + "'" +
                                         ",'" + coach_service + "'" +
                                         ",'" + coach_type + "'" +
                                         ",'" + coach_order_range + "'" +
                                         ",'" + coach_place + "'" +
                                         ",'" + coach_state + "'" +
                                         ",getdate())";

                        }
                        else
                        {
                            strSQL = "UPDATE [t_coach]" +
                                         " SET [coach_incode] = '" + coach_incode + "'" +
                                         ",[coach_city] = '" + coach_city + "'" +
                                         ",[coach_car_number] = '" + coach_car_number + "'" +
                                         ",[coach_car_type] = '" + coach_car_type + "'" +
                                         ",[coach_name] = '" + coach_name + "'" +
                                         ",[coach_pwd] = '" + coach_pwd + "'" +
                                         ",[coach_sex] = '" + coach_sex + "'" +
                                         ",[coach_age] = '" + coach_age + "'" +
                                         ",[coach_phone] = '" + coach_phone + "'" +
                                         ",[coach_long] = '" + coach_long + "'" +
                                         ",[coach_number] = '" + coach_number + "'" +
                                         ",[coach_teacher_number] = '" + coach_teacher_number + "'" +
                                         ",[coach_myself] = '" + coach_myself + "'" +
                                         ",[coach_pic] = '" + coach_pic + "'" +
                                         ",[coach_teacher_pic] = '" + coach_teacher_pic + "'" +
                                         ",[coach_driver_pic1] = '" + coach_driver_pic1 + "'" +
                                         ",[coach_driver_pic2] = '" + coach_driver_pic2 + "'" +
                                         ",[coach_card_pic1] = '" + coach_card_pic1 + "'" +
                                         ",[coach_card_pic2] = '" + coach_card_pic2 + "'" +
                                         ",[coach_subject] = '" + coach_subject + "'" +
                                         ",[coach_price] = '" + coach_price + "'" +
                                         ",[coach_score] = '" + coach_score + "'" +
                                         ",[coach_service] = '" + coach_service + "'" +
                                         ",[coach_type] = '" + coach_type + "'" +
                                         ",[coach_order_range] = '" + coach_order_range + "'" +
                                         ",[coach_place] = '" + coach_place + "'" +
                                         ",[coach_state] = '" + coach_state + "'" +
                                         " WHERE [coach_pk]='" + coach_pk + "'";

                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            strSQL = "select case when coach_type='1' then '教练' else '陪练' end as coach_teach,* from t_coach a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }


                        break;
                        #endregion
                    }
                case "del_coach":
                    {
                        #region 删除教练
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_coach where coach_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除教练信息");
                            string strSQL = "select case when coach_type='1' then '教练' else '陪练' end as coach_teach,* from t_coach a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "lock_coach":
                    {
                        #region 锁定教练
                        string coach_pk = Request["del_pk"].ToS();
                        if (coach_pk.Length > 0) coach_pk = coach_pk.Substring(0, coach_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("update t_coach set coach_state='2' where coach_pk in (" + coach_pk + ")");
                        if (i > 0)
                        {
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "锁定司机信息");
                            string strSQL = "select * from t_coach a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                #endregion
                #region 学员信息
                case "search_student":
                    {
                        #region 查找学员
                        string student_real_name = Request["student_real_name"].ToS();                   
                        string student_phone = Request["student_phone"].ToS();                   
                        string student_state = Request["student_state"].ToS();
                        string strSQL = "select *  from t_student a where 1=1";
                        if (student_phone != "") strSQL += " and student_phone like '%" + student_phone + "%'";
                        if (student_real_name != "") strSQL += " and student_real_name like '%" + student_real_name + "%'";
                        if (student_state != "") strSQL += " and student_state = '" + student_state + "'";
                        strSQL += " order by a.[create_time] desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "get_student_info":
                    {
                        #region 获取单个学员信息
                        string student_pk = Request["student_pk"].ToS();
                        string strSQL = "select * from t_student a  where student_pk='" + student_pk + "'";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_student":
                    {
                        #region 保存学员信息
                        string student_pk = Request["student_pk"].ToS();
                        string student_name = Request["student_name"].ToS();
                        string student_real_name = Request["student_real_name"].ToS();
                        string student_sex = Request["student_sex"].ToS();
                        string student_age = Request["student_age"].ToS();
                        string student_phone = Request["student_phone"].ToS();
                        string student_pic = Request["student_pic"].ToS();
                        string student_state = Request["student_state"].ToS();
                        string student_cancel_count = Request["student_cancel_count"].ToS();
                        string student_incode = Request["student_incode"].ToS();
                        string student_allow_car = Request["student_allow_car"].ToS();
                     
                        string strSQL = "";
                        if (student_pk == "")
                        {
                            strSQL = "INSERT INTO [t_student]" +
                            "([student_pk]" +
                            //",[student_name]" +
                            ",[student_real_name]" +
                            ",[student_sex]" +
                            //",[student_incode]" +
                            //",[student_allow_car]" +
                            //",[student_age]" +
                            ",[student_phone]" +
                            ",[student_pic]" +
                            ",[student_state]" +                          
                            ",[create_time])" +
                            " VALUES" +
                            "(newid()" +
                            //",'" + student_name + "'" +
                            ",'" + student_real_name + "'" +
                            ",'" + student_sex + "'" +
                            //",'" + student_incode + "'" +
                            //",'" + student_allow_car + "'" +
                            //",'" + student_age + "'" +
                            ",'" + student_phone + "'" +
                            ",'" + student_pic + "'" +
                            ",'" + student_state + "'" +                       
                            ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_student]" +
                            " SET "+//[student_name] = '" + student_name + "'" +
                            "[student_real_name] = '" + student_real_name + "'" +
                            ",[student_sex] = '" + student_sex + "'" +
                            //",[student_incode] = '" + student_incode + "'" +
                            //",[student_allow_car] = '" + student_allow_car + "'" +
                            //",[student_age] = '" + student_age + "'" +
                            ",[student_phone] = '" + student_phone + "'" +
                            ",[student_pic] = '" + student_pic + "'" +
                            ",[student_state] = '" + student_state + "'" +                           
                            " WHERE [student_pk]='" + student_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            strSQL = "select * from t_student a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }



                        break;
                        #endregion
                    }
                case "del_student":
                    {
                        #region 删除学员
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_student where student_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除学员信息");
                            string strSQL = "select * from t_student a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "lock_student":
                    {
                        #region 锁定用户
                        string student_pk = Request["del_pk"].ToS();
                        if (student_pk.Length > 0) student_pk = student_pk.Substring(0, student_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("update t_student set student_state='2' where student_pk in (" + student_pk + ")");
                        if (i > 0)
                        {
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "锁定用户信息");
                            string strSQL = "select * from t_student a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
                case "reg":
                    {
                        #region 领取礼包                                            
                        string student_phone = Request["student_phone"].ToS();                      
                        string student_incode = Request["student_incode"].ToS();                    
                        string strSQL = "";
                      
                            strSQL = "INSERT INTO [t_student]" +
                                "([student_pk]" +                          
                                ",[student_phone]" +                         
                                ",[student_state]" +
                                ",[create_time])" +
                            " VALUES" +
                                "(newid()" +                            
                                ",'" + student_phone + "'" +                              
                                ",'1'" +
                                ",getdate())";
                       
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            restr = "{\"result\":\"1\"}";
                        }



                        break;
                        #endregion
                    }
                #endregion
                #region 车型信息
                case "search_car":
                    {
                        #region 查找车型

                        string strSQL = "select *";
                        strSQL += " from t_car a order by a.[create_time] desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "get_car_info":
                    {
                        #region 获取单个车型信息
                        string car_pk = Request["car_pk"].ToS();
                        string strSQL = "select * from t_car a  where car_pk='" + car_pk + "'";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_car":
                    {
                        #region 保存车型信息
                        string car_pk = Request["car_pk"].ToS();
                        string car_name = Request["car_name"].ToS();
                        string car_img = Request["car_img"].ToS();
                        string car_start_price = Request["car_start_price"].ToS();
                        string car_away_price = Request["car_away_price"].ToS();
                        string car_time_price = Request["car_time_price"].ToS();
                        string car_far_price = Request["car_far_price"].ToS();
                        string car_far_away = Request["car_far_away"].ToS();
                        string car_meal_price = Request["car_meal_price"].ToS();
                        string car_meal_away = Request["car_meal_away"].ToS();
                        string car_meal_time = Request["car_meal_time"].ToS();
                        string car_go_away_price = Request["car_go_away_price"].ToS();
                        string car_go_time_price = Request["car_go_time_price"].ToS();
                        string car_rem = Request["car_rem"].ToS();                       
                        string strSQL = "";
                        if (car_pk == "")
                        {
                            strSQL = "INSERT INTO [t_car]" +
                                "([car_pk]" +
                                ",[car_name]" +
                                ",[car_img]" +
                                ",[car_start_price]" +
                                ",[car_away_price]" +
                                ",[car_time_price]" +
                                ",[car_far_price]" +
                                ",[car_far_away]" +
                                ",[car_meal_price]" +
                                ",[car_meal_away]" +
                                ",[car_meal_time]" +
                                ",[car_go_away_price]" +
                                ",[car_go_time_price]" +
                                ",[car_rem]" +
                                ",[create_time])" +
                            " VALUES" +
                                "(newid()" +
                                ",'" + car_name + "'" +
                                ",'" + car_img + "'" +
                                ",'" + car_start_price + "'" +
                                ",'" + car_away_price + "'" +
                                ",'" + car_time_price + "'" +
                                ",'" + car_far_price + "'" +
                                ",'" + car_far_away + "'" +
                                ",'" + car_meal_price + "'" +
                                ",'" + car_meal_away + "'" +
                                ",'" + car_meal_time + "'" +
                                ",'" + car_go_away_price + "'" +
                                ",'" + car_go_time_price + "'" +
                                ",'" + car_rem + "'" +
                                ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_car]" +
                                    " SET [car_name] = '" + car_name + "'" +
                                    ",[car_img] = '" + car_img + "'" +
                                    ",[car_start_price] = '" + car_start_price + "'" +
                                    ",[car_away_price] = '" + car_away_price + "'" +
                                    ",[car_time_price] = '" + car_time_price + "'" +
                                    ",[car_far_price] = '" + car_far_price + "'" +
                                    ",[car_far_away] = '" + car_far_away + "'" +
                                    ",[car_meal_price] = '" + car_meal_price + "'" +
                                    ",[car_meal_away] = '" + car_meal_away + "'" +
                                    ",[car_meal_time] = '" + car_meal_time + "'" +
                                    ",[car_go_away_price] = '" + car_go_away_price + "'" +
                                    ",[car_go_time_price] = '" + car_go_time_price + "'" +
                                    ",[car_rem] = '" + car_rem + "'" +
                                    " WHERE [car_pk]='" + car_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            strSQL = "select *";
                            strSQL += " from t_car a order by a.[create_time] desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }

                        break;
                        #endregion
                    }
                case "del_car":
                    {
                        #region 删除车型
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_car where car_pk in (" + del_pk + ")");
                        if (i > 0)
                        {
                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除车型信息");
                            string strSQL = "select * from t_car a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }

                #endregion
                #region 单页
                case "save_info":
                    {
                        #region 保存内容
                        string code = Request["code"].ToS();

                        string code_content = Request["code_content"].ToS();

                        string strSQL = "UPDATE [t_sys_code]" +
                                   " SET [code_content] = '" + code_content + "'" +

                                 " WHERE [code]='" + code + "'";

                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "编辑系统内容" + code);

                            restr = "{\"result\":\"1\"}";
                        }
                        break;
                        #endregion
                    }
                #endregion
                #region 长租车信息
                case "search_rental":
                    {
                        #region 查找长租车

                        string strSQL = "select * from t_rental a";
                        strSQL += " order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "get_rental_info":
                    {
                        #region 获取单个长租车信息
                        string rental_pk = Request["rental_pk"].ToS();
                        string strSQL = "select * from t_rental a  where rental_pk='" + rental_pk + "'";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }

                case "save_rental":
                    {
                        #region 保存长租车信息
                        string rental_pk = Request["rental_pk"].ToS();
                        string rental_start_address = Request["rental_start_address"].ToS();
                        string rental_end_address = Request["rental_end_address"].ToS();
                        string rental_time1 = Request["rental_time1"].ToS();
                        string rental_time2 = Request["rental_time2"].ToS();
                        string rental_name = Request["rental_name"].ToS();
                        string rental_number = Request["rental_number"].ToS();
                        string rental_tel = Request["rental_tel"].ToS();
                        string rental_rem = Request["rental_rem"].ToS();                       
                        string strSQL = "";
                        if (rental_pk == "")
                        {
                            strSQL = "INSERT INTO [t_rental]" +
                            "([rental_pk]" +
                            ",[rental_start_address]" +
                            ",[rental_end_address]" +
                            ",[rental_time1]" +
                            ",[rental_time2]" +
                            ",[rental_name]" +
                            ",[rental_number]" +
                            ",[rental_tel]" +
                            ",[rental_rem]" +
                            ",[create_time])" +
                            " VALUES" +
                            "(newid()" +
                            ",'" + rental_start_address + "'" +
                            ",'" + rental_end_address + "'" +
                            ",'" + rental_time1 + "'" +
                            ",'" + rental_time2 + "'" +
                            ",'" + rental_name + "'" +
                            ",'" + rental_number + "'" +
                            ",'" + rental_tel + "'" +
                            ",'" + rental_rem + "'" +
                            ",getdate())";
                        }
                        else
                        {
                            strSQL = "UPDATE [t_rental]" +
                            " SET [rental_start_address] = '" + rental_start_address + "'" +
                            ",[rental_end_address] = '" + rental_end_address + "'" +
                            ",[rental_time1] = '" + rental_time1 + "'" +
                            ",[rental_time2] = '" + rental_time2 + "'" +
                            ",[rental_number] = '" + rental_number + "'" +
                            ",[rental_name] = '" + rental_name + "'" +
                            ",[rental_tel] = '" + rental_tel + "'" +
                            ",[rental_rem] = '" + rental_rem + "'" +
                            " WHERE [rental_pk]='" + rental_pk + "'";
                        }
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            strSQL = "select * from t_rental a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }


                        break;
                        #endregion
                    }
                case "del_rental":
                    {
                        #region 删除长租车
                        string del_pk = Request["del_pk"].ToS();
                        if (del_pk.Length > 0) del_pk = del_pk.Substring(0, del_pk.Length - 1);
                        int i = DbHelperSQL.ExecuteSql("delete from t_rental where rental_pk in (" + del_pk + ")");
                        if (i > 0)
                        {

                            Tools.addlog(Session["UserPK"].ToS(), Session["ip1"].ToS(), Session["ip2"].ToS(), "删除长租车信息");

                            string strSQL = "select * from t_rental a";
                            strSQL += " order by a.create_time desc";
                            ds = DbHelperSQL.Query(strSQL);
                            restr = Json.DataTableToJson(ds.Tables[0]);
                        }
                        break;
                        #endregion
                    }
               
                #endregion
                #region 广告管理
                case "search_ad":
                    {
                        string info_type = Request["info_type"].ToS();
                        string strSQL = "select a.*,b.student_real_name,b.student_phone,c.coach_name,c.coach_phone from t_info a left join t_student b on a.user_pk=b.student_pk left join t_coach c on a.user_pk=c.coach_pk where [info_type]=" + info_type + "";
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
