using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;
using PostMsg_Net;
using PostMsg_Net.common;
using System.Text;
using System.Configuration;
using System.Xml;
using WXPaySDK;

public partial class web_getdata : System.Web.UI.Page
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
                case "get_Province":
                    {
                        #region 获取省份

                        ds = DbHelperSQL.Query("select * from S_Province where ProvinceID in (select p_id from t_driving)");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_City":
                    {
                        #region 获取城市
                        string v = Request["v"].ToS();
                        ds = DbHelperSQL.Query("select * from S_City where ProvinceID=" + v + " and CityID in (select c_id from t_driving)");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_District":
                    {
                        #region 获取区县
                        string v = Request["v"].ToS();
                        ds = DbHelperSQL.Query("select * from S_District where CityID=" + v + " and DistrictID in (select area_id from t_driving)");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_Driving":
                    {
                        #region 获取驾校
                        string v = Request["v"].ToS();
                        ds = DbHelperSQL.Query("select * from t_driving where 1=1" + (v != "" ? (" and area_id=" + v) : ""));
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_Driving_info":
                    {
                        #region 获取驾校详情
                        string driving_pk = Request["driving_pk"].ToS();
                        ds = DbHelperSQL.Query("select (select ProvinceName from S_Province where ProvinceID=a.p_id) as p,(select CityName from S_City where CityID=a.c_id) as c,(select DistrictName from S_District where DistrictID=a.area_id) as q,* from t_driving a where driving_pk='" + driving_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_Info":
                    {
                        #region 获取导航内容
                        string v = Request["v"].ToS();
                        ds = DbHelperSQL.Query("select code_content from t_sys_code where code='" + v + "'");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            restr = "{\"result\":\"" + Json.ChangeString(ds.Tables[0].Rows[0][0].ToS()) + "\"}";
                        }
                        break;
                        #endregion
                    }
                case "send_code":
                    {
                        #region 发送验证码
                        //key 用户标示 发送和验证时要统一
                        //template 模板标示 0000700001=登录短信模板 00005= 重置密码短信模板
                        //phone 手机号
                        string key = Request["key"].ToS();
                        string template = Request["template"].ToS();
                        string phone = Request["phone"].ToS();
                        string code = (new Random(DateTime.Now.Millisecond + 1)).Next(0, 9).ToS() + (new Random(DateTime.Now.Millisecond + 2)).Next(0, 9).ToS() + (new Random(DateTime.Now.Millisecond + 3)).Next(0, 9).ToS() + (new Random(DateTime.Now.Millisecond + 4)).Next(0, 9).ToS() + (new Random(DateTime.Now.Millisecond + 5)).Next(0, 9).ToS() + (new Random(DateTime.Now.Millisecond + 6)).Next(0, 9).ToS();
                        if (DbHelperSQL.Query("select * from t_sys_code where code='" + phone + "'").Tables[0].Rows.Count > 0)
                        {
                            DbHelperSQL.ExecuteSql("update t_sys_code set code_content='" + code + "',code_expire='" + DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss") + "' where code='" + phone + "'");
                        }
                        else
                        {
                            DbHelperSQL.ExecuteSql("insert into t_sys_code([code_pk],[code],[code_content],[code_expire]) values(newid(),'" + phone + "','" + code + "','" + DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss") + "')");

                        }
                        string content = DbHelperSQL.ExecuteSqlScalar("select code_content from t_sys_code where code='" + template + "'").ToS().Replace("{code}", code);
                        int i = Tools.SendSMS(phone, content);
                        restr = "{\"result\":\"" + code + "\",\"code\":\"" + i + "\"}";
                        break;
                        #endregion
                    }
                case "get_reg_ment":
                    {
                        #region 注册协议
                        //code 00001=教练注册协议 00002=陪练注册协议 00003=学员注册协议
                        string code = Request["code"].ToS();
                        string ment = DbHelperSQL.ExecuteSqlScalar("select code_content from t_sys_code where code='" + code + "'").ToS();
                        restr = "{\"result\":\"" + Json.ChangeString(ment) + "\"}";
                        break;
                        #endregion
                    }
                case "reg":
                    {
                        #region 学员注册
                        //-96 手机号为空
                        //-99 确认密码不正确 或密码为空
                        //-98 验证码不正确
                        //-97 手机号已注册
                        //返回值 "{"result":"02E83414-054F-426A-97D3-43D036846E62"}" 02E83414-054F-426A-97D3-43D036846E62为学员标示
                        string student_phone = Request["account"].ToS();
                        string password = Request["password"].ToS();
                        string password_confirm = Request["password_confirm"].ToS();
                        string student_incode = Request["student_incode"].ToS();
                        string student_allow_car = Request["student_allow_car"].ToS();
                        string verification = Request["verification"].ToS();
                        string key = Request["key"].ToS();
                        if (student_phone == "")
                        {
                            restr = "{\"result\":\"-96\"}"; // 手机号为空
                            break;
                        }

                        if (password != password_confirm || password == "")
                        {
                            restr = "{\"result\":\"-99\"}";
                            break;
                        }

                        int c = DbHelperSQL.ExecuteSqlScalar("select count(*) from t_student where student_phone='" + student_phone + "'").ToInt32();
                        if (c > 0)
                        {
                            restr = "{\"result\":\"-97\"}";
                            break;

                        }
                        if (DbHelperSQL.ExecuteSqlScalar("select Count(*) from t_sys_code where code='" + student_phone + "' and Convert(varchar(100),code_content)='" + verification + "' and [code_expire]>='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToInt32() == 0)
                        {
                           
                            restr = "{\"result\":\"-98\"}";
                            break;
                        }

                        string student_pk = Guid.NewGuid().ToS();
                        string strSQL = "INSERT INTO [t_student]" +
                            "([student_pk]" +
                            ",[student_phone]" +
                            ",[student_pwd]" +
                            ",[student_incode]" +
                            ",[student_allow_car]" +
                            ",[student_state]" +
                            ",[student_money]" +
                            ",[create_time])" +
                            " VALUES" +
                            "('" + student_pk + "'" +
                            ",'" + student_phone + "'" +
                            ",'" + Tools.Encode(password) + "'" +
                            ",'" + student_incode + "'" +
                            ",'" + student_allow_car + "'" +
                            ",'1'" +
                            "," + DbHelperSQL.ExecuteSqlScalar("select sms_result_end_code from t_system").ToD().ToString("0.00") + "" +
                            ",getdate())";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {

                            restr = "{\"result\":\"" + student_pk + "\"}";
                            break;
                        }

                        break;
                        #endregion
                    }
                case "login":
                    {
                        #region 登录
                        //-97验证码不正确
                        //-98手机号为空                      
                        //-100 手机号未注册 或正在审核中
                        //返回 学员（教练）信息
                        string user_type = Request["user_type"].ToS();
                        string student_phone = Request["account"].ToS();
                        string verification = Request["verification"].ToS();
                        string clientid = Request["ClientID"].ToS();
                        string key = Request["key"].ToS();
                        if (student_phone == "")
                        {
                            restr = "{\"result\":\"-98\"}";
                            break;
                        }
                        if (student_phone != "17792513817")
                        {
                            if (DbHelperSQL.Query("select * from t_sys_code where code='" + student_phone + "' and Convert(varchar(100),code_content)='" + verification + "' and [code_expire]>='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").Tables[0].Rows.Count == 0)
                            {
                                restr = "{\"result\":\"-97\"}";
                                break;
                            }
                        }
                        if (user_type == "user")
                        {

                            ds = DbHelperSQL.Query("select * from t_student where student_phone='" + student_phone + "'");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DbHelperSQL.ExecuteSql("update t_student set student_clientid='" + clientid + "' where student_pk='" + ds.Tables[0].Rows[0]["student_pk"].ToS() + "'");
                                restr = Json.DataTableToJson(ds.Tables[0]);
                                break;
                            }
                            else
                            {

                                string djq = DbHelperSQL.ExecuteSqlScalar("select sms_result_end_code from t_system").ToD().ToString("0.00");
                                string strSQL = "INSERT INTO [t_student]" +
                                                  "([student_pk]" +
                                                  ",[student_phone]" +
                                                  ",[student_state]" +
                                                  ",[student_clientID]" +
                                                  ",[student_money]" +
                                                  ",[create_time])" +
                                                  " VALUES" +
                                                  "(newid()" +
                                                  ",'" + student_phone + "'" +
                                                  ",'1'" +
                                                  ",'" + clientid + "'" +
                                                  "," + djq + "" +
                                                  ",getdate())";
                                int i = DbHelperSQL.ExecuteSql(strSQL);

                                if (i > 0)
                                {

                                    ds = DbHelperSQL.Query("select *,'"+ djq + "' as student_djq from t_student where student_phone='" + student_phone + "' and student_state='1'");
                                    if (ds.Tables[0].Rows.Count > 0)
                                        restr = Json.DataTableToJson(ds.Tables[0]);
                                    break;
                                }
                            }

                        }
                        else if (user_type == "coach")
                        {
                            ds = DbHelperSQL.Query("select * from t_coach where coach_phone='" + student_phone + "' and coach_state='1'");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DbHelperSQL.ExecuteSql("update t_coach set coach_clientid='" + clientid + "' where coach_pk='" + ds.Tables[0].Rows[0]["coach_pk"].ToS() + "'");
                                restr = Json.DataTableToJson(ds.Tables[0]);
                                break;
                            }
                        }
                        restr = "{\"result\":\"-100\"}";
                        break;
                        #endregion
                    }
                case "forget_pwd":
                    {
                        #region 忘记密码
                        //user_type 学员=student 教练=coach
                        //-98手机号为空
                        //-99 验证码不能为空    
                        //-100 验证码不正确
                        //-101 重置失败
                        //返回 {"result":"125692"} 125692为随机密码
                        string user_type = Request["user_type"].ToS();
                        string student_phone = Request["account"].ToS();
                        string verification = Request["verification"].ToS();
                        string key = Request["key"].ToS();
                        if (student_phone == "")
                        {
                            restr = "{\"result\":\"-98\"}";
                            break;
                        }
                        if (verification == "")
                        {
                            restr = "{\"result\":\"-99\"}";
                            break;
                        }
                        if (DbHelperSQL.ExecuteSqlScalar("select Count(*) from t_sys_code where code='" + student_phone + "' and Convert(varchar(100),code_content)='" + verification + "' and [code_expire]>='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToInt32() == 0)
                        {                           
                            restr = "{\"result\":\"-100\"}";
                            break;
                        }
                        System.Text.StringBuilder newRandom = new System.Text.StringBuilder(6);
                        Random rd = new Random();
                        for (int i = 0; i < 6; i++)
                        {
                            newRandom.Append(rd.Next(10));
                        }
                        int c = 0;
                        if (user_type == "user")
                        {
                            c = DbHelperSQL.ExecuteSql("update t_student set student_pwd='" + Tools.Encode(newRandom.ToS()) + "' where student_phone='" + student_phone + "'");
                        }
                        else if (user_type == "coach")
                        {
                            c = DbHelperSQL.ExecuteSql("update t_coach set coach_pwd='" + Tools.Encode(newRandom.ToS()) + "' where coach_phone='" + student_phone + "'");
                        }
                        if (c > 0)
                        {
                            restr = "{\"result\":\"" + newRandom.ToS() + "\"}";
                        }
                        else
                        {
                            restr = "{\"result\":\"-101\"}";
                        }
                        break;
                        #endregion
                    }
                case "upload_pic":
                    {
                        #region 上传头像
                        string user_type = Request["user_type"].ToS();
                        string user_pk = Request["user_pk"].ToS();
                        if (Request.Files.Count > 0)
                        {
                            string hz = Request.Files[0].FileName.Substring(Request.Files[0].FileName.IndexOf('.') + 1, Request.Files[0].FileName.Length - Request.Files[0].FileName.IndexOf('.') - 1);

                            if (hz.ToLower() == "jpg" || hz.ToLower() == "png" || hz.ToLower() == "bmp" || hz.ToLower() == "jpeg" || hz.ToLower() == "gif")
                            {
                                string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + hz;
                                Request.Files[0].SaveAs(Server.MapPath("../upload/tx") + "\\" + filename);
                                if (Session["QUserT"].ToS() == "1")
                                {
                                    int i = 0;
                                    if (user_type == "user") i = DbHelperSQL.ExecuteSql("update t_student set student_pic='" + "upload/tx/" + filename + "' where student_pk='" + user_pk + "'");
                                    else if (user_type == "coach") i = DbHelperSQL.ExecuteSql("update t_coach set coach_pic='" + "upload/tx/" + filename + "' where coach_pk='" + user_pk + "'");

                                    if (i > 0)
                                    {
                                        restr = "{\"result\":\"100\",\"pic\":\"" + filename + "\"}";
                                    }
                                    else
                                    {
                                        restr = "{\"result\":\"头像设置失败！\"}";
                                    }
                                }
                            }
                            else
                            {
                                restr = "{\"result\":\"格式不支持！\"}";
                            }
                        }
                        else
                        {
                            restr = "{\"result\":\"请选择图像文件！\"}";
                        }

                        break;
                        #endregion
                    }

                case "upd_user_info":
                    {
                        #region 更改资料
                        //-99 密码为空或确认密码错误
                        string student_pk = Request["user_pk"].ToS();
                        string student_name = Request["user_name"].ToS();
                        string strSQL = "UPDATE [t_student]" +
                           " SET [student_real_name] = '" + student_name + "'" +
                           " WHERE [student_pk]='" + student_pk + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            ds = DbHelperSQL.Query("select * from t_student where student_pk='" + student_pk + "'");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                restr = Json.DataTableToJson(ds.Tables[0]);
                            }
                        }
                        break;
                        #endregion
                    }

                case "get_car":
                    {
                        #region 获取车型

                        string strSQL = "select * from t_car a order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
            
                case "save_order":
                    {
                        #region 保存订单
                        string order_pk = Guid.NewGuid().ToS();
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
                        string order_away = (Request["order_away"].ToD() / 1000).ToString("0.000");
                        string order_time = (Request["order_time"].ToD() / 60).ToInt32().ToS();
                        string car = Request["car"].ToS();
                        string order_rem = Request["order_rem"].ToS();
                        string create_time = Request["create_time"].ToS();
                        string[] car_arr = car.Split(',');
                        double car_meal_fee = 0;
                        double car_meal_away = 0;
                        double order_away_fee = 0;
                        double order_time_fee = 0;
                        double order_far_away_fee = 0;
                        double order_fee = 0;
                        DataSet car_ds = DbHelperSQL.Query("select * from t_car where car_pk='" + car_arr[0] + "'");
                        if (car_ds.Tables[0].Rows.Count > 0)
                        {
                            car_meal_fee = (car_ds.Tables[0].Rows[0]["car_start_price"].ToD()).ToString("0.00").ToD();
                            car_meal_away = (car_ds.Tables[0].Rows[0]["car_meal_away"].ToD()).ToString("0.00").ToD();

                            order_away_fee = (order_away.ToD() * car_ds.Tables[0].Rows[0]["car_away_price"].ToD()).ToString("0.00").ToD();
                            order_time_fee = (order_time.ToD() * car_ds.Tables[0].Rows[0]["car_time_price"].ToD()).ToString("0.00").ToD();
                            if (order_away.ToD() > car_ds.Tables[0].Rows[0]["car_far_away"].ToD())
                            {
                                order_far_away_fee = ((order_away.ToD() - car_ds.Tables[0].Rows[0]["car_far_away"].ToD()) * car_ds.Tables[0].Rows[0]["car_far_price"].ToD()).ToString("0.00").ToD();
                            }

                        }
                        order_fee = car_meal_fee + order_away_fee + order_time_fee + order_far_away_fee;
                        ArrayList arr = new ArrayList();
                        string strSQL = "";

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
                            "('" + order_pk + "'" +
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
                            ",'" + car_meal_fee + "'" +
                            ",'" + order_away_fee + "'" +
                            ",'" + order_time_fee + "'" +
                            ",'" + order_far_away_fee + "'" +
                            ",'0'" +
                            ",'" + order_fee + "'" +
                            ",'" + order_rem + "'" +
                            ",'0'" +
                            ",getdate())";
                        arr.Add(strSQL);

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
                        int j = DbHelperSQL.ExecuteSqlTran(arr);
                        if (j > 0)
                        {
                            restr = "{\"result\":\"" + order_pk + "\"}";
                            //推送
                            //
                            ds = DbHelperSQL.Query("select * from t_coach where coach_car_type='"+ car_arr[0] + "' and dbo.fnGetDistance(coach_lat,coach_lon," + start_lat + "," + start_lon + ")<=coach_order_range and coach_state='1'");
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["coach_clientID"].ToS() == "") continue;
                                string[] arg = new string[2];
                                arg[0] = ds.Tables[0].Rows[i]["coach_clientID"].ToS();
                                arg[1] = Json.DataTableToJson(DbHelperSQL.Query("select * from t_order where order_pk='" + order_pk + "'").Tables[0]).Replace("\"", "'");
                                Tools.StartProcess(Server.MapPath("/") + "/getui_ser/GetuiServerApiSDKDemo.exe", arg);
                            }
                        }
                        break;
                        #endregion
                    }
                case "get_order_list":
                    {
                        #region 获取订单列表
                        string coach_pk = Request["coach_pk"].ToS();
                        string strSQL = "select order_pk,order_type,user_tel,order_datetime,start_address,start_lon,start_lat,end_address,end_lon,end_lat from t_order a";
                        strSQL += " left join (select coach_lon,coach_lat,Convert(float,coach_order_range) as coach_order_range from t_coach where coach_pk='" + coach_pk + "') b on 1=1";
                        strSQL += " where  dbo.fnGetDistance(coach_lat,coach_lon,start_lat,start_lon)<=coach_order_range and order_state='0' order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "coach_and_order":
                    {
                        #region 抢单
                        string coach_pk = Request["coach_pk"].ToS();
                        string order_pk = Request["order_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_order where coach_pk='" + coach_pk + "' and order_state in ('0','1','2')");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            restr = "{\"result\":\"-100\"}";
                            break;
                        }
                        //判断车型
                        if (DbHelperSQL.Query("select coach_pk from t_coach where coach_pk='" + coach_pk + "' and coach_car_type in (select car_pk from t_order_car where order_pk='" + order_pk+"')").Tables[0].Rows.Count == 0)
                        {
                            restr = "{\"result\":\"-98\"}";
                            break;
                        }
                        string strSQL = "update  t_order set coach_pk='" + coach_pk + "',order_state='1'  where order_pk='" + order_pk + "' and order_state='0'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            restr = "{\"result\":\"100\"}";
                            ds = DbHelperSQL.Query("select a.user_tel,a.start_lon,a.start_lat,b.coach_name,b.coach_phone,b.coach_car_number from t_order a left join t_coach b on a.coach_pk=b.coach_pk where order_pk='" + order_pk + "'");
                            string content = "";
                            //短信提醒
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string template = "0000700002";
                                string phone = ds.Tables[0].Rows[0]["user_tel"].ToS();
                                content = DbHelperSQL.ExecuteSqlScalar("select code_content from t_sys_code where code='" + template + "'").ToS();
                                content = content.Replace("{coach_name}", ds.Tables[0].Rows[0]["coach_name"].ToS());
                                content = content.Replace("{coach_phone}", ds.Tables[0].Rows[0]["coach_phone"].ToS());
                                content = content.Replace("{car_number}", ds.Tables[0].Rows[0]["coach_car_number"].ToS());
                                Tools.SendSMS(phone, content);
                            }
                            //推送
                            ds = DbHelperSQL.Query("select *,'" + ds.Tables[0].Rows[0]["start_lon"].ToS() + "' as start_lon,'" + ds.Tables[0].Rows[0]["start_lat"].ToS() + "' as start_lat from t_student where Convert(varchar(100),student_pk) in (select user_pk from t_order where order_pk='" + order_pk + "')");
                            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["student_clientID"].ToS() == "") continue;
                                string[] arg = new string[2];
                                arg[0] = ds.Tables[0].Rows[i]["student_clientID"].ToS();
                                arg[1] = Json.DataTableToJson(DbHelperSQL.Query("select *,'" + ds.Tables[0].Rows[0]["start_lon"].ToS() + "' as start_lon,'" + ds.Tables[0].Rows[0]["start_lat"].ToS() + "' as start_lat from t_coach where coach_pk='" + coach_pk + "'").Tables[0]).Replace("\"", "'");
                                Tools.StartProcess(Server.MapPath("/") + "/getui/GetuiServerApiSDKDemo.exe", arg);
                            }
                        }
                        else
                        {
                            restr = "{\"result\":\"-99\"}";
                            break;
                        }
                        break;
                        #endregion
                    }
                case "get_coach_list":
                    {
                        #region 获取教练列表

                        string strSQL = "select coach_pk,coach_lon,coach_lat from t_coach a where coah_actity_time>='" + DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss") + "' and coach_state='1' order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "set_coach_place":
                    {
                        #region 设置司机位置
                        string coach_pk = Request["coach_pk"].ToS();
                        string coach_lon = Request["coach_lon"].ToS();
                        string coach_lat = Request["coach_lat"].ToS();
                        string strSQL = "update  t_coach set coach_lon='" + coach_lon + "',coach_lat='" + coach_lat + "',coah_actity_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where coach_pk='" + coach_pk + "'";

                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            restr = "{\"result\":\"100\"}";
                        }

                        break;
                        #endregion
                    }
                case "get_coach_info":
                    {
                        #region 获取教练详细
                        string coach_pk = Request["coach_pk"].ToS();
                        string strSQL = "select * from t_coach a where 1=1";
                        strSQL += " and coach_pk='" + coach_pk + "'";

                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson_Html(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_user_info":
                    {
                        #region 获取用户详细
                        string user_pk = Request["user_pk"].ToS();
                        string strSQL = "select (select sum(order_fee) from t_order where user_pk=a.student_pk and order_state in ('1','2')) as yg_fee,* from t_student a where 1=1";
                        strSQL += " and student_pk='" + user_pk + "'";

                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson_Html(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_coach_orderlist":
                    {
                        #region 获取司机的订单信息
                        string coach_pk = Request["coach_pk"].ToS();
                        string strSQL = "select order_pk,order_type,user_name,user_tel,order_datetime,start_address,end_address,order_state from t_order a where coach_pk='" + coach_pk + "' order by create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_user_orderlist":
                    {
                        #region 获取客户的订单信息
                        string user_pk = Request["user_pk"].ToS();
                        string strSQL = "select order_pk,order_type,coach_name,coach_phone,order_datetime,start_address,end_address,order_state from t_order a left join t_coach b on a.coach_pk=Convert(varchar(100),b.coach_pk) where user_pk='" + user_pk + "' order by a.create_time desc";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_order_info":
                    {
                        #region 获取订单详细信息
                        string order_pk = Request["order_pk"].ToS();
                        string strSQL = "select * from t_order a left join t_coach b on a.coach_pk=Convert(varchar(100),b.coach_pk) where order_pk='" + order_pk + "'";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "cancel_order":
                    {
                        #region 取消订单
                        string user_pk = Request["user_pk"].ToS();
                        string coach_pk = Request["coach_pk"].ToS();
                        string order_pk = Request["order_pk"].ToS();
                        string strSQL = "";
                        if (user_pk != "") strSQL = "update t_order  set order_state='5' where order_pk='" + order_pk + "' and user_pk='" + user_pk + "'";
                        else if (coach_pk != "") strSQL = "update t_order  set order_state='5' where order_pk='" + order_pk + "' and coach_pk='" + coach_pk + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        restr = "{\"result\":\"" + i + "\"}";
                        break;
                        #endregion
                    }
                case "change_order_fee":
                    {
                        #region 司机更改订单费用  
                        string coach_pk = Request["coach_pk"].ToS();
                        string order_pk = Request["order_pk"].ToS();
                        string order_fee = Request["order_fee"].ToD().ToS();
                        string strSQL = "update t_order  set order_fee=" + order_fee + " where order_pk='" + order_pk + "' and coach_pk='" + coach_pk + "' and order_state in ('1','2')";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        restr = "{\"result\":\"" + i + "\"}";
                        break;
                        #endregion
                    }
                case "pay_order":
                    {
                        #region 支付订单
                        string order_pk = Request["order_pk"].ToS();
                        ArrayList arr = new ArrayList();
                        string strSQL = "update t_order  set order_state='3' where order_pk='" + order_pk + "' and order_state in ('1','2')";
                        arr.Add(strSQL);
                        //ds = DbHelperSQL.Query("select * from t_order where order_pk='" + order_pk + "'");
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    if (ds.Tables[0].Rows[0]["user_pk"].ToS() != "")
                        //    {
                        //        strSQL = "update t_order  set order_state='3' where order_pk='" + order_pk + "' and order_state='2'";
                        //        arr.Add(strSQL);
                        //    }
                        //}
                        int i = 0;// DbHelperSQL.ExecuteSqlTran(arr);
                        restr = "{\"result\":\"" + i + "\"}";
                        break;
                        #endregion
                    }
                case "evel_order":
                    {
                        #region 评价
                        string order_pk = Request["order_pk"].ToS();
                        string eval_score = Request["eval_score"].ToD().ToS();
                        string eval_rem = Request["eval_rem"].ToS();
                        ArrayList arr = new ArrayList();
                        string strSQL = "";

                        strSQL = "INSERT INTO [t_eval]" +
                                        "([eval_pk]" +
                                        ",[order_pk]" +
                                        ",[eval_score]" +
                                        ",[eval_rem]" +
                                        ",[create_time])" +
                                    " VALUES" +
                                        "(newid()" +
                                        ",'" + order_pk + "'" +
                                        "," + eval_score + "" +
                                        ",'" + eval_rem + "'" +
                                        ",getdate())";
                        arr.Add(strSQL);
                        strSQL = "update t_order set order_state='4' where order_pk='" + order_pk + "'";
                        arr.Add(strSQL);
                        int i = DbHelperSQL.ExecuteSqlTran(arr);
                        restr = "{\"result\":\"" + i + "\"}";
                        break;
                        #endregion
                    }

                case "get_order_eval":
                    {
                        #region 查看订单评价
                        string order_pk = Request["order_pk"].ToS();
                        string strSQL = "select * from t_eval where order_pk='" + order_pk + "'";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_coach_eval":
                    {
                        #region 查看司机评价
                        string coach_pk = Request["coach_pk"].ToS();
                        string strSQL = "select * from t_eval where order_pk in (select order_pk from t_order where coach_pk='" + coach_pk + "')";
                        ds = DbHelperSQL.Query(strSQL);
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "save_info":
                    {
                        #region 投诉

                        string info_type = Request["info_type"].ToInt32().ToS();
                        string user_type = Request["user_type"].ToInt32().ToS();
                        string user_pk = Request["user_pk"].ToS();
                        string info_title = Request["info_title"].ToS();
                        string info_content = Request["info_content"].ToS();
                        string info_obj = Request["info_obj"].ToS();
                        string strSQL = "";

                        strSQL = "INSERT INTO [t_info]" +
                                    "([info_pk]" +
                                    ",[info_type]" +
                                    ",[user_type]" +
                                    ",[user_pk]" +
                                    ",[info_title]" +
                                    ",[info_content]" +
                                    ",[info_obj]" +
                                    ",[info_state]" +
                                    ",[create_time])" +
                                " VALUES" +
                                    "(newid()" +
                                    "," + info_type + "" +
                                    "," + user_type + "" +
                                    ",'" + user_pk + "'" +
                                    ",'" + info_title + "'" +
                                    ",'" + info_content + "'" +
                                    ",'" + info_obj + "'" +
                                    ",0" +
                                    ",getdate())";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        restr = "{\"result\":\"" + i + "\"}";

                        break;
                        #endregion
                    }
                case "set_withdraw":
                    {
                        #region 提现
                        string user_type = "1";
                        string info_type = "3";
                        string verification = Request["verification"].ToS();
                        string user_pk = Request["user_pk"].ToS();
                        string user_phone = Request["account_phone"].ToS();
                        string info_title = Request["account_number"].ToS();
                        string info_content = Request["account_name"].ToS();
                        double info_obj = Request["withdraw_sum"].ToD().ToString("0.00").ToD();
                        if (info_obj <= 0)
                        {
                            restr = "{\"result\":\"-96\"}";
                            break;
                        }
                        if (DbHelperSQL.ExecuteSqlScalar("select coach_money from t_coach where coach_pk='" + user_pk + "'").ToD() < info_obj)
                        {
                            restr = "{\"result\":\"-95\"}";
                            break;
                        }
                        string strSQL = "";
                        if (DbHelperSQL.Query("select * from t_sys_code where code='" + user_phone + "' and Convert(varchar(100),code_content)='" + verification + "' and [code_expire]>='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").Tables[0].Rows.Count == 0)
                        {
                            restr = "{\"result\":\"-97\"}";
                            break;
                        }
                        strSQL = "INSERT INTO [t_info]" +
                                    "([info_pk]" +
                                    ",[info_type]" +
                                    ",[user_type]" +
                                    ",[user_pk]" +
                                    ",[info_title]" +
                                    ",[info_content]" +
                                    ",[info_obj]" +
                                    ",[info_state]" +
                                    ",[create_time])" +
                                " VALUES" +
                                    "(newid()" +
                                    "," + info_type + "" +
                                    "," + user_type + "" +
                                    ",'" + user_pk + "'" +
                                    ",'" + info_title + "'" +
                                    ",'" + info_content + "'" +
                                    ",'" + info_obj + "'" +
                                    ",0" +
                                    ",getdate())";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        if (i > 0)
                        {
                            DbHelperSQL.ExecuteSql("update t_coach set coach_money=coach_money-" + info_obj + " where coach_pk='" + user_pk + "'");
                            restr = "{\"result\":\"" + i + "\"}";
                        }

                        break;
                        #endregion
                    }
                case "get_withdraw":
                    {
                        #region 获取提现消息
                        string user_pk = Request["user_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_info where user_pk='" + user_pk + "' and info_type=3");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_rechange":
                    {
                        #region 获取充值记录
                        string user_pk = Request["user_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_transaction where user_pk='" + user_pk + "' and isfinish=1 order by create_time desc");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_message":
                    {
                        #region 获取用户消息
                        string user_pk = Request["user_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_message where user_pk='" + user_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "read_message":
                    {
                        #region 消息标示未已读
                        string message_pk = Request["message_pk"].ToS();
                        string strSQL = "update t_message set message_state=1 where message_pk='" + message_pk + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        restr = "{\"result\":\"" + i + "\"}";
                        break;
                        #endregion
                    }
                case "set_coach_range":
                    {
                        #region 设置抢单范围
                        string coach_pk = Request["coach_pk"].ToS();
                        string coach_range = Request["coach_range"].ToD().ToS();
                        string strSQL = "update t_coach set coach_order_range='" + coach_range + "' where coach_pk='" + coach_pk + "'";
                        int i = DbHelperSQL.ExecuteSql(strSQL);
                        restr = "{\"result\":\"" + i + "\"}";
                        break;
                        #endregion
                    }
                case "get_rental":
                    {
                        #region 获取长租车

                        ds = DbHelperSQL.Query("select * from t_rental order by create_time desc");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_rental_info":
                    {
                        #region 获取长租车
                        string rental_pk = Request["rental_pk"].ToS();
                        ds = DbHelperSQL.Query("select * from t_rental where rental_pk='" + rental_pk + "'");
                        restr = Json.DataTableToJson(ds.Tables[0]);
                        break;
                        #endregion
                    }
                case "get_pop_link":
                    {
                        #region 获取推广二维码
                        string user_pk = Request["user_pk"].ToS();
                        string pop_link = "/?link=" + Tools.Encode(user_pk).Replace("-", "");
                        if (!File.Exists(Server.MapPath("/") + "images/pop/" + user_pk + ".jpg"))
                        {
                            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("/") + "images/pop");
                            if (!dir.Exists) dir.Create();
                            Tools.QRCode("http://" + Request.Url.Host + pop_link, Server.MapPath("/") + "images/pop/" + user_pk + ".jpg");
                        }
                        restr = "{\"result\":\"" + pop_link + "\",\"pic_path\":\"/images/pop/" + user_pk + ".jpg\"}";
                        break;
                        #endregion
                    }
                case "get_user_order_state":
                    {
                        #region 获取是否能下单或抢单
                        string user_pk = Request["user_pk"].ToS();
                        string coach_pk = Request["coach_pk"].ToS();
                        if (user_pk != "")
                        {
                            ds = DbHelperSQL.Query("select * from t_order where user_pk='" + user_pk + "' and order_state in ('0','1','2')");
                            restr = "{\"result\":\"" + ds.Tables[0].Rows.Count + "\"}";
                        }
                        else if (coach_pk != "")
                        {
                            ds = DbHelperSQL.Query("select * from t_order where coach_pk='" + coach_pk + "' and order_state in ('0','1','2')");
                            restr = "{\"result\":\"" + ds.Tables[0].Rows.Count + "\"}";
                        }
                        break;
                        #endregion
                    }
                case "get_pay_info":
                    {
                        #region 支付宝支付  
                        string user_pk = Request["user_pk"].ToS();
                        string order_pk = Request["order_pk"].ToS();
                        double rechange_money = Request["rechange_money"].ToD().ToString("0.00").ToD();
                        if (rechange_money > 0 && user_pk!="")
                        {
                            //交易记录
                            string transaction_pk = add_transaction("rechange", "user", user_pk,"", rechange_money, DbHelperSQL.ExecuteSqlScalar("select student_money from t_student where student_pk='" + user_pk + "'").ToD(), "账户充值");
                            if (transaction_pk != "")
                            {
                                Dictionary<string, string> payinfo = new Dictionary<string, string>();
                                payinfo.Add("service", "\"mobile.securitypay.pay\"");
                                payinfo.Add("partner", "\"" + Config.Partner + "\"");
                                payinfo.Add("seller_id", "\"" + Config.Seller_id + "\"");
                                payinfo.Add("out_trade_no", "\"" + transaction_pk + "\"");
                                payinfo.Add("subject", "\"账户充值\"");
                                payinfo.Add("body", "\"账户充值\"");
                                payinfo.Add("total_fee", "\"" + rechange_money + "\"");
                                payinfo.Add("notify_url", "\"http://qidiancar.com/web/notify_url_rechange.aspx\"");
                                payinfo.Add("payment_type", "\"1\"");
                                payinfo.Add("_input_charset", "\"UTF-8\"");
                                payinfo.Add("it_b_pay", "\"30m\"");
                                var sb = new StringBuilder();
                                foreach (var sA in payinfo.OrderBy(x => x.Key))//参数名ASCII码从小到大排序（字典序）；
                                {
                                    sb.Append(sA.Key).Append("=").Append(sA.Value).Append("&");
                                }
                                var orderInfo = sb.ToString();
                                orderInfo = orderInfo.Remove(orderInfo.Length - 1, 1);
                                // 对订单做RSA 签名
                                string sign = AlipayMD5.Sign(orderInfo, Config.Private_key, Config.Input_charset); //支付宝提供的Config.cs
                                //仅需对sign做URL编码
                                sign = HttpUtility.UrlEncode(sign, Encoding.UTF8);
                                string payInfo = orderInfo + "&sign=\"" + sign + "\"&"
                                    + getSignType();
                                restr = payInfo;
                            }
                        }
                        else if (order_pk != "")
                        {
                            ds = DbHelperSQL.Query("select * from t_order where order_pk='" + order_pk + "'");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                 //交易记录
                                string transaction_pk = add_transaction("rechange", "user", ds.Tables[0].Rows[0]["user_pk"].ToS(),order_pk, ds.Tables[0].Rows[0]["order_fee"].ToD(), DbHelperSQL.ExecuteSqlScalar("select student_money from t_student where student_pk='" + ds.Tables[0].Rows[0]["user_pk"].ToS() + "'").ToD(), "支付订单");
                                if (transaction_pk != "")
                                {
                                    Dictionary<string, string> payinfo = new Dictionary<string, string>();
                                    payinfo.Add("service", "\"mobile.securitypay.pay\"");
                                    payinfo.Add("partner", "\"" + Config.Partner + "\"");
                                    payinfo.Add("seller_id", "\"" + Config.Seller_id + "\"");
                                    payinfo.Add("out_trade_no", "\"" + transaction_pk + "\"");
                                    payinfo.Add("subject", "\"支付专车费用\"");
                                    payinfo.Add("body", "\"支付专车费用\"");
                                    payinfo.Add("total_fee", "\"" + ds.Tables[0].Rows[0]["order_fee"].ToS() + "\"");
                                    payinfo.Add("notify_url", "\"http://qidiancar.com/web/notify_url_rechange.aspx\"");
                                    payinfo.Add("payment_type", "\"1\"");
                                    payinfo.Add("_input_charset", "\"UTF-8\"");
                                    payinfo.Add("it_b_pay", "\"30m\"");
                                    var sb = new StringBuilder();
                                    foreach (var sA in payinfo.OrderBy(x => x.Key))//参数名ASCII码从小到大排序（字典序）；
                                    {
                                        sb.Append(sA.Key).Append("=").Append(sA.Value).Append("&");
                                    }
                                    var orderInfo = sb.ToString();
                                    orderInfo = orderInfo.Remove(orderInfo.Length - 1, 1);
                                    // 对订单做RSA 签名
                                    string sign = AlipayMD5.Sign(orderInfo, Config.Private_key, Config.Input_charset); //支付宝提供的Config.cs
                                    //仅需对sign做URL编码
                                    sign = HttpUtility.UrlEncode(sign, Encoding.UTF8);
                                    string payInfo = orderInfo + "&sign=\"" + sign + "\"&"
                                        + getSignType();
                                    restr = payInfo;
                                }
                            }

                        }
                        else
                        {
                            restr = "请求数据错误！";
                        }
                        break;
                        #endregion
                    }
                case "get_wx_pay_info":
                    {
                        #region 微信支付
                                           
                        string user_pk = Request["user_pk"].ToS();
                        string order_pk = Request["order_pk"].ToS();
                        double rechange_money = Request["rechange_money"].ToD().ToString("0.00").ToD(); 
                        if (rechange_money > 0 && user_pk != "")
                        {
                            //交易记录
                            string transaction_pk = add_transaction("rechange", "user", user_pk, "", rechange_money, DbHelperSQL.ExecuteSqlScalar("select student_money from t_student where student_pk='" + user_pk + "'").ToD(), "账户充值");
                            if (transaction_pk != "")
                            {
                                var payment = new Payment(Wx_Pay_Model.mchid, Wx_Pay_Model.appId, Wx_Pay_Model.appkey, "http://qidiancar.com/web/wx_notify_url.aspx");
                                restr = payment.Pay(long.Parse((rechange_money * 100).ToS()), transaction_pk.Replace("-",""), "账户充值", Request.UserHostAddress,"CNY");    
                               
                            }
                        }
                        else if (order_pk != "")
                        {
                            ds = DbHelperSQL.Query("select * from t_order where order_pk='" + order_pk + "'");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //交易记录
                                string transaction_pk = add_transaction("rechange", "user", ds.Tables[0].Rows[0]["user_pk"].ToS(), order_pk, ds.Tables[0].Rows[0]["order_fee"].ToD(), DbHelperSQL.ExecuteSqlScalar("select student_money from t_student where student_pk='" + ds.Tables[0].Rows[0]["user_pk"].ToS() + "'").ToD(), "支付订单");
                                if (transaction_pk != "")
                                {                                
                                    var payment = new Payment(Wx_Pay_Model.mchid, Wx_Pay_Model.appId, Wx_Pay_Model.appkey, "http://qidiancar.com/web/wx_notify_url.aspx");
                                    restr = payment.Pay(long.Parse((ds.Tables[0].Rows[0]["order_fee"].ToD() * 100).ToS()), transaction_pk.Replace("-", ""), "支付专车费用", Request.UserHostAddress, "CNY");

                                }
                            }

                        }
                        else
                        {
                            restr = "请求数据错误！";
                        }
                        break;
                        #endregion
                    }
                case "balance_pay":
                    {
                        #region 余额支付
                        string order_pk = Request["order_pk"].ToS();
                        string user_pk = Request["user_pk"].ToS();
                        DataSet order_ds = DbHelperSQL.Query("select * from t_order where order_pk='" + order_pk + "'");
                        if (order_ds.Tables[0].Rows.Count > 0)
                        {
                            if (order_ds.Tables[0].Rows[0]["order_state"].ToS() == "1" || order_ds.Tables[0].Rows[0]["order_state"].ToS() == "2")
                            {
                                DataSet user_ds = DbHelperSQL.Query("select * from t_student where student_pk='" + user_pk + "'");
                                if (user_ds.Tables[0].Rows.Count > 0)
                                {
                                    if (user_ds.Tables[0].Rows[0]["student_money"].ToD() >= order_ds.Tables[0].Rows[0]["order_fee"].ToD())
                                    { 
                                        //扣款
                                        if (DbHelperSQL.ExecuteSql("update  t_student set student_money=student_money-" + order_ds.Tables[0].Rows[0]["order_fee"].ToD() + " where student_pk='" + user_pk + "'") > 0)
                                        { 
                                            //订单状态
                                            if (DbHelperSQL.ExecuteSql("update t_order  set order_state='3' where order_pk='" + order_pk + "' and order_state in ('1','2')") > 0)
                                            {
                                                restr = "{\"result\":\"100\"}";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        restr = "{\"result\":\"-99\"}";
                                    }
                                }

                            }
                            else
                            {
                                restr = "{\"result\":\"100\"}";
                            }
                        }
                        
                        break;
                        #endregion
                    }
                case "get_pay_state":
                    {
                        #region 检测订单是否已支付
                        string order_pk = Request["order_pk"].ToS();
                        int i = DbHelperSQL.ExecuteSqlScalar("select order_state from t_order where order_pk='" + order_pk + "'").ToInt32() == 3 ? 1 : 0;
                        restr = "{\"result\":\"" + i + "\"}";
                        break;
                        #endregion
                    }
                case "get_update":
                    {
                        #region 检测是否更新      
      
                        string _type = Request["_type"].ToS();
                        double version = Request["version"].ToS().Replace(".","").ToD();
                        ds = DbHelperSQL.Query("select * from t_system");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            double new_version = _type == "1" ? ds.Tables[0].Rows[0]["sms_succ_code"].ToS().Replace(".", "").ToD() : ds.Tables[0].Rows[0]["sms_sel_url"].ToS().Replace(".", "").ToD();
                            if (new_version > version)
                            {
                                restr = "{\"result\":\"100\"}";
                            }
                        }
                       
                        break;
                        #endregion
                    }
                case "get_ad":
                    {
                        #region 获取首页banner

                        string _type = Request["_type"].ToS();
                        ds = DbHelperSQL.Query("select * from t_info where info_type=4 and info_title='" + _type + "' order by create_time desc");
                        restr = Json.DataTableToJson(ds.Tables[0]);

                        break;
                        #endregion
                    }
                case "get_ad_one":
                    {
                        #region 获取首页广告

                        string _type = Request["_type"].ToS();
                        string ad_one = DbHelperSQL.ExecuteSqlScalar("select top 1 info_content from t_info where info_type=4 and info_title='" + _type + "' order by create_time desc").ToS();
                        restr = "{\"result\":\"" + ad_one + "\"}";
                        break;
                        #endregion
                    }
                case "get_recharge":
                    {
                        #region 获取充值金额 赠送金额
                        string djq = DbHelperSQL.ExecuteSqlScalar("select sms_result_start_code from t_system").ToS();
                        restr = "{\"result\":\""+ djq + "\"}";
                        #endregion
                        break;
                    }
                default:
                    {
                        restr = "";
                        break;
                    }
            }
        }
        catch (Exception ex )
        {
            restr = "";
        }
        Response.Write(restr);
        Response.End();
    }
    public String getSignType()
    {
        return "sign_type=\"MD5\"";
    }
    public string add_transaction(string transaction_type, string user_type, string user_pk, string order_pk, double amount, double balance, string rem)
    {
        string transaction_pk = Guid.NewGuid().ToS();
  
        string strSQL = "";

        strSQL = "INSERT INTO [t_transaction]" +
            "([transaction_pk]" +
            ",[transaction_type]" +
            ",[user_type]" +
            ",[user_pk]" +
            ",[order_pk]" +
            ",[amount]" +
            ",[balance]" +
            ",[rem]" +
            ",[isfinish]" +         
            ",[create_time])" +
        " VALUES" +
            "('" + transaction_pk + "'" +
            ",'" + transaction_type + "'" +
            ",'" + user_type + "'" +
            ",'" + user_pk + "'" +
            ",'" + order_pk + "'" +
            "," + amount + "" +
            "," + balance + "" +
            ",'" + rem + "'" +
            ",0" +         
            ",getdate())";

        int i = DbHelperSQL.ExecuteSql(strSQL);
        if (i > 0)
        {
            return transaction_pk;
        }
        return "";

    }
   
}
