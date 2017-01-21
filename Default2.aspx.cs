using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet p_ds = DbHelperSQL.Query("select * from S_Province");
        DataSet c_ds = DbHelperSQL.Query("select * from S_City");
        DataSet q_ds = DbHelperSQL.Query("select * from S_District");
        string str_json = "[";
        for (int i = 0; i < p_ds.Tables[0].Rows.Count; i++)
        {
            str_json += "\r\n   {";
            str_json += "\r\n       value:'" + p_ds.Tables[0].Rows[i]["ProvinceID"].ToS() + "',";
            str_json += "\r\n       text:'" + p_ds.Tables[0].Rows[i]["ProvinceName"].ToS() + "',";
            str_json += "\r\n       children:[";
            DataRow[] dr = c_ds.Tables[0].Select("ProvinceID=" + p_ds.Tables[0].Rows[i]["ProvinceID"].ToS());
            for (int j = 0; j < dr.Length; j++)
            {
                str_json += "\r\n                   {";
                str_json += "\r\n                       value:'" + dr[j]["CityID"].ToS() + "',";
                str_json += "\r\n                       text:'" + dr[j]["CityName"].ToS() + "',";
                str_json += "\r\n                       children:[";
                DataRow[] dr2 = q_ds.Tables[0].Select("CityID=" + dr[j]["CityID"].ToS());
                for (int z = 0; z < dr2.Length; z++)
                {
                    str_json += "\r\n                                   {";
                    str_json += "value:'" + dr2[z]["DistrictID"].ToS() + "',";
                    str_json += "text:'" + dr2[z]["DistrictName"].ToS() + "'";
                    str_json += "}";
                    if (z < dr2.Length-1) str_json += ",";
                }
                str_json += "\r\n                               ]";
                str_json += "\r\n                   }";
                if (j < dr.Length-1) str_json += ",";
            }
            str_json += "\r\n               ]";
            str_json += "\r\n   }";
            if(i < p_ds.Tables[0].Rows.Count-1) str_json += ",";
        }
        str_json += "\r\n]";
        Response.Write(str_json);
    }
}