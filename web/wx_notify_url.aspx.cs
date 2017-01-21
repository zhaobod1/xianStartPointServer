using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class web_wx_notify_url : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();
        m_values["return_code"] = "SUCCESS";
        m_values["return_msg"] = "OK";
        var requestContent = "";
        using (StreamReader sr = new StreamReader(Request.InputStream))
        {
            requestContent = sr.ReadToEnd();
        }
        Core.LogResult("微信通知-" + requestContent);
//        requestContent="<xml><appid><![CDATA[wx4179af8e0ea8a387]]></appid>"+
//"<bank_type><![CDATA[CFT]]></bank_type> " +
//"<cash_fee><![CDATA[1]]></cash_fee> "+
//"<fee_type><![CDATA[CNY]]></fee_type> "+
//"<is_subscribe><![CDATA[N]]></is_subscribe> "+
//"<mch_id><![CDATA[1297903501]]></mch_id> "+
//"<nonce_str><![CDATA[A0B4F4F8542C2DBD44666FEAD120736F]]></nonce_str> "+
//"<openid><![CDATA[onyoZszqjktYmo_uOZtECF5kjmns]]></openid> "+
//"<out_trade_no><![CDATA[33DA163BA3BF41769826FB759F30567D]]></out_trade_no> " +
//"<result_code><![CDATA[SUCCESS]]></result_code> "+
//"<return_code><![CDATA[SUCCESS]]></return_code> "+
//"<sign><![CDATA[29B1CAE42B56AEE774B5AE178F2A86AD]]></sign> "+
//"<time_end><![CDATA[20160307222037]]></time_end> "+
//"<total_fee>1</total_fee> "+
//"<trade_type><![CDATA[APP]]></trade_type> "+
//"<transaction_id><![CDATA[1000120517201603073808698452]]></transaction_id> "+
//"</xml>";
        var sPara = GetRequestPostByXml(requestContent);
        if (sPara.Count <= 0)
        {         
            Response.Write("fail");
            Response.End();
        }
        if (sPara["return_code"].ToS() == "SUCCESS" && sPara["return_code"].ToS() == "SUCCESS")
        {
            //签名验证                     
            var sign = sPara["sign"].ToS();

            var signValue = WxPayCore.CreateMd5Sign("key", Wx_Pay_Model.appkey, sPara);
            bool isVerify = sign == signValue;
            if (isVerify)
            {
                //TODO 商户处理订单逻辑： 1.注意交易单不要重复处理；2.注意判断返回金额        
                //交易号            
                var order_pk = sPara["out_trade_no"].ToS();
                order_pk = order_pk.Substring(0, 8) + "-" + order_pk.Substring(8, 4) + "-" + order_pk.Substring(12, 4) + "-" + order_pk.Substring(16, 4) + "-" + order_pk.Substring(20, 12);
                if (UpdateTransaction(order_pk))
                {
                    Response.Write(ToXml(m_values));
                    Response.End();
                }


               
                //TODO:postData中携带该次支付的用户相关信息，这将便于商家拿到openid，以便后续提供更好的售后服务，譬如：微信公众好通知用户付款成功。如果不提供服务则可以删除此代码                 
                //var openid = sPara["openid"]; 

               
            }
            else
            {
               // log.Add(LogType.Edit, "微信支付签名验证失败:" + requestContent + ",Code:" + Bug_Code, 0);
            }
        }
        else
        {
            //log.Add(LogType.Edit, "微信支付失败:" + requestContent + ",Code:" + Bug_Code, 0);
        }
        Response.Write("fail");
        Response.End();
    }
    /// <summary>
    /// 获取微信通知参数
    /// </summary>
    /// <param name="xmlString"></param>
    /// <returns></returns>
    private Hashtable GetRequestPostByXml(string xmlString)
    {
        Hashtable dic = new Hashtable();    
        System.Xml.XmlDocument document = new System.Xml.XmlDocument();
        document.LoadXml(xmlString);

        var nodes = document.ChildNodes[0].ChildNodes;

        foreach (System.Xml.XmlNode item in nodes)
        {
            dic.Add(item.Name, item.InnerText);
        }
        return dic;
    }

    public string ToXml(SortedDictionary<string, object> m_values)
    {
        //数据为空时不能转化为xml格式
        if (0 == m_values.Count)
        {
            throw new Exception("WxPayData数据为空!");
        }

        string xml = "<xml>";
        foreach (KeyValuePair<string, object> pair in m_values)
        {
            //字段值不能为null，会影响后续流程
            if (pair.Value == null)
            {
                throw new Exception("WxPayData内部含有值为null的字段!");
            }

            if (pair.Value.GetType() == typeof(int))
            {
                xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
            }
            else if (pair.Value.GetType() == typeof(string))
            {
                xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
            }
            else//除了string和int类型不能含有其他数据类型
            {
                throw new Exception("WxPayData字段数据类型错误!");
            }
        }
        xml += "</xml>";
        return xml;
    }
    public bool UpdateTransaction(string out_trade_no)
    {
        DataSet transaction_ds = DbHelperSQL.Query("select * from t_transaction where transaction_pk='" + out_trade_no + "'");
        if (transaction_ds.Tables[0].Rows.Count > 0)
        {
            if (transaction_ds.Tables[0].Rows[0]["isfinish"].ToInt32() == 0)
            {
                //更新交易记录
                if (DbHelperSQL.ExecuteSql("update t_transaction set isfinish=1,finish_time=getdate() where transaction_pk='" + out_trade_no + "'") > 0)
                {
                    if (transaction_ds.Tables[0].Rows[0]["order_pk"].ToS() != "")
                    {
                        //更新订单状态
                        DataSet ds = DbHelperSQL.Query("select * from t_order where order_pk='" + transaction_ds.Tables[0].Rows[0]["order_pk"].ToS() + "'");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["order_state"].ToS() == "1" || ds.Tables[0].Rows[0]["order_state"].ToS() == "2")
                            {
                                if (DbHelperSQL.ExecuteSql("update t_order  set order_state='3' where order_pk='" + transaction_ds.Tables[0].Rows[0]["order_pk"].ToS() + "' and order_state in ('1','2')") > 0)
                                {
                                    DbHelperSQL.ExecuteSql("update t_coach  set coach_money=coach_money+" + ds.Tables[0].Rows[0]["order_fee"].ToD() + "  where coach_pk='" + ds.Tables[0].Rows[0]["coach_pk"].ToS() + "'");
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        //充值
                        string[] djq = DbHelperSQL.ExecuteSqlScalar("select sms_result_start_code from t_system").ToS().Split(';');
                        double recharge_money = transaction_ds.Tables[0].Rows[0]["amount"].ToD();
                        for (int i = djq.Length-1; i >=0 ; i--)
                        {
                            string[] re = djq[i].Split('-');
                            if (re.Length == 2)
                            {
                                if (recharge_money >= re[0].ToD())
                                {
                                    recharge_money += re[1].ToD();
                                    break;
                                }
                            }
                        }
                        if (DbHelperSQL.ExecuteSql("update  t_student set student_money=student_money+" + recharge_money + " where student_pk='" + transaction_ds.Tables[0].Rows[0]["user_pk"].ToS() + "'") > 0)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}