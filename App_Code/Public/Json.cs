using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
///Json 的摘要说明
///格式化JSON数据
/// </summary>
public abstract class Json
{
	public Json()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static string DataTableToJson(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append(dt.TableName.ToS());
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            jsonBuilder.Append("\"RowIndex\":\"" + (i+1) + "\",");

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                if (dt.Rows[i][j].ToString().IndexOf('<') > -1) jsonBuilder.Append(Tools.NoHtml(dt.Rows[i][j].ToString()));
                else jsonBuilder.Append(ChangeString(dt.Rows[i][j].ToString()));
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        if (dt.Rows.Count > 0) jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        jsonBuilder.Append("}");
        return jsonBuilder.ToS();
    }
    public static string DataTableToJson_Html(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append(dt.TableName.ToS());
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            jsonBuilder.Append("\"RowIndex\":\"" + (i + 1) + "\",");

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                if (dt.Rows[i][j].ToString().IndexOf('<') > -1) jsonBuilder.Append(ChangeString(dt.Rows[i][j].ToString()));
                else jsonBuilder.Append(ChangeString(dt.Rows[i][j].ToString()));
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        if (dt.Rows.Count > 0) jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        jsonBuilder.Append("}");
        return jsonBuilder.ToS();
    }
    public static string ChangeString(string str)
    {
        //str含有HTML标签的文本
        str = str.Replace("&", "&amp;");
        str = str.Replace("<", "&lt;");
        str = str.Replace(">", "&gt;");
        str = str.Replace(" ", "&nbsp;");
        str = str.Replace("\n", "<brbr>");       
        str = str.Replace("\"", "''");
        str = str.Replace("	", "");

        return str;
    }
    public static string DataSetToJson(DataSet ds)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");
        for (int z = 0; z < ds.Tables.Count; z++)
        {
            DataTable dt = ds.Tables[z];
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName.ToS());
            jsonBuilder.Append("\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                jsonBuilder.Append("\"RowIndex\":\"" + (i + 1) + "\",");

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    if (dt.Rows[i][j].ToString().IndexOf('<') > -1) jsonBuilder.Append(Compress(dt.Rows[i][j].ToString().Replace("\"", "&quot;")));
                    else jsonBuilder.Append(ChangeString(dt.Rows[i][j].ToString()));
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0) jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("},");
        }
        if (ds.Tables.Count > 0) jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToS();
    }
    #region 压缩HTML
    /// <summary>
    /// 压缩HTML
    /// </summary>
    /// <returns></returns>
    public static string Compress(string strHTML)
    {
        strHTML = Regex.Replace(strHTML, @"\s", "");
     
        return strHTML;
    }
    #endregion

}
