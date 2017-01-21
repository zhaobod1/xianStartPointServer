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
using System.Data.SqlClient;
using System.Collections;
/// <summary>
///DbHelperSQL 数据库操作
///by huyi xel 2011-05-30
/// </summary>
public abstract class DbHelperSQL
{
    public static string Connectionstring = ConfigurationManager.ConnectionStrings["SqlConnStr"].ToString();
	public DbHelperSQL()
    {
        
    }
    
    #region 执行SQL语句，返回DataSet数据集
    /// <summary>
    /// 执行SQL语句，返回DataSet数据集
    /// </summary>
    /// <param name="StrSQL">要执行的SQL语句</param>
    /// <returns>返回DataSet数据集</returns>
    public static DataSet Query(string StrSQL)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(StrSQL, connection);
                da.Fill(ds);
                return ds;
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

    }
    #endregion

    #region 执行SQL语句，返回第一个DataTable数据表
    /// <summary>
    /// 执行SQL语句，返回第一个DataTable数据表
    /// </summary>
    /// <param name="StrSQL">要执行的SQL语句</param>
    /// <returns>返回DataTable数据表</returns>
    public static DataTable QueryTable(string StrSQL)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(StrSQL, connection);
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

    }
    #endregion

    #region 执行SQL语句，返回影响的记录数
    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="StrSQL">要执行的SQL语句</param>
    /// <returns>返回影响的记录数</returns>
    public static int ExecuteSql(string StrSQL)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            using (SqlCommand comm = new SqlCommand(StrSQL, connection))
            {
                try
                {
                    connection.Open();
                    int Rows = comm.ExecuteNonQuery();
                 
                    return Rows;
                }
                catch (SqlException)
                {
                    return 0;
                }
                finally
                {
                    comm.Dispose();
                    connection.Close();
                }
            }
        }

    }
    #endregion

    #region 执行SQL语句，返回第一行第一列
    /// <summary>
    /// 执行SQL语句，返回第一行第一列
    /// </summary>
    /// <param name="StrSQL">要执行的SQL语句</param>
    /// <returns>返回第一行第一列</returns>
    public static string ExecuteSqlScalar(string StrSQL)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            using (SqlCommand comm = new SqlCommand(StrSQL, connection))
            {
                try
                {
                    connection.Open();
                    string objstr = comm.ExecuteScalar().ToS();
                    return objstr;
                }
                catch (SqlException)
                {
                    return "";
                }
                finally
                {
                    comm.Dispose();
                    connection.Close();
                }
            }
        }

    }
    #endregion

    #region 执行SQL语句，返回SqlDataAdapter
    /// <summary>
    /// 执行SQL语句，返回SqlDataAdapter
    /// </summary>
    /// <param name="StrSQL">要执行的SQL语句</param>
    /// <returns>返回SqlDataAdapter</returns>
    public static SqlDataAdapter QueryAdapter(string StrSQL)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {           
            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(StrSQL, connection);              
                return da;
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

    }
    #endregion

    #region 执行SQL语句，返回SqlDataReader
    /// <summary>
    /// 执行SQL语句，返回SqlDataReader
    /// </summary>
    /// <param name="StrSQL">要执行的SQL语句</param>
    /// <returns>返回SqlDataReader</returns>
    public static SqlDataReader QueryReader(string StrSQL)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            using (SqlCommand comm = new SqlCommand(StrSQL, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    return reader;
                }
                catch (SqlException)
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
    #endregion

    #region 执行多条SQL语句，实现数据库事务。添加顺序执行
    /// <summary>
    /// 执行多条SQL语句，实现数据库事务。添加顺序执行
    /// </summary>
    /// <param name="SQLStringList">多条SQL语句</param>	
    /// <returns>返回影响记录数</returns>
    public static int ExecuteSqlTran(ArrayList SQLStringList)
    {        
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            using(SqlCommand comm=new SqlCommand())
            {
                connection.Open();

                comm.Connection = connection;
                SqlTransaction tx = connection.BeginTransaction();
                comm.Transaction = tx;
                
                try
                {
                    int i = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            comm.CommandText = strsql;
                            i+=comm.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return i;
                }
                catch (SqlException)
                {
                    tx.Rollback();
                    return 0;
                }
                finally
                {
                    comm.Dispose();
                    connection.Close();
                }
            }
        }
        
    }
    #endregion

    #region 执行带一个存储过程参数的的SQL语句,返回影响记录数
    /// <summary>
    /// 执行带一个存储过程参数的的SQL语句。比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <param name="content">参数内容</param>
    /// <returns>影响的记录数</returns>
    public static int ExecuteSql(string StrSQL, string content)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            SqlCommand comm = new SqlCommand(StrSQL, connection);
            SqlParameter myParameter = new SqlParameter("@content", SqlDbType.Text);
            myParameter.Value = content;
            comm.Parameters.Add(myParameter);
            try
            {
                connection.Open();
                int rows = comm.ExecuteNonQuery();
                return rows;
            }
            catch (SqlException)
            {
                return 0;
            }
            finally
            {
                comm.Dispose();
                connection.Close();
            }
        }
    }

    #endregion

    #region  执行一条计算查询结果语句，返回查询结果（object）
    /// <summary>
    /// 执行一条计算查询结果语句，返回查询结果（object）。
    /// </summary>
    /// <param name="SQLString">计算查询结果语句</param>
    /// <returns>查询结果（object）</returns>
    public static object GetSingle(string StrSQL)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            using (SqlCommand comm = new SqlCommand(StrSQL, connection))
            {
                try
                {
                    connection.Open();
                    object obj = comm.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (SqlException)
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
    #endregion

    #region 执行存储过程，返回DataSet数据集
    /// <summary>
    /// 执行存储过程，返回DataSet数据集
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>返回DataSet数据集</returns>
    public static DataSet RunQueryProcedure(string storedProcName, IDataParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            DataSet dataSet = new DataSet();
            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                da.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
    #endregion

    #region 执行存储过程，返回影响记录数
    /// <summary>
    /// 执行存储过程，返回影响记录数
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>返回影响记录数</returns>
    public static int RunExecuteProcedure(string storedProcName, IDataParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            DataSet dataSet = new DataSet();
            try
            {
                connection.Open();
                SqlCommand comm = BuildQueryCommand(connection, storedProcName, parameters);
                int rowsAffected = comm.ExecuteNonQuery();
                connection.Close();
                return rowsAffected;
            }
            catch (SqlException)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }

        }
    }
    #endregion

    #region 执行存储过程 返回SqlDataReader结果集
    /// <summary>
    /// 执行存储过程 返回SqlDataReader结果集
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>返回SqlDataReader结果集</returns>
    public static SqlDataReader RunDataReaderProcedure(string storedProcName, IDataParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(Connectionstring))
        {
            try
            {
                connection.Open();
                SqlCommand comm = BuildQueryCommand(connection, storedProcName, parameters);
                SqlDataReader reader = comm.ExecuteReader();
                return reader;
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }

        }
    }
    #endregion

    #region 构建一个 SqlCommand 对象
    /// <summary>
    /// 构建 SqlCommand 对象
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>MySqlCommand</returns>
    private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
    {
        try
        {
            SqlCommand comm = new SqlCommand(storedProcName, connection);
            comm.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                comm.Parameters.Add(parameter);
            }
            return comm;
        }
        catch (SqlException)
        {
            return null;
        }
    }
    #endregion


    /// <summary>
    /// 绑定数据到DROPDOWNLIST
    /// </summary>
    /// <param name="dropname">DROPDOWNLIST名</param>
    /// <param name="tablename">表名</param>
    /// <param name="txtfield">显示的字段名</param>
    /// <param name="valfield">ID</param>
    public static void DropDataBind(DropDownList dropname, string tablename, string txtfield, string valfield)
    {
        string strSql = "";
        strSql = "select * from " + tablename + "";
        dropname.DataSource = GetDataSource(strSql, "'" + tablename + "'");
        dropname.DataTextField = "" + txtfield + "";
        dropname.DataValueField = "" + valfield + "";
        dropname.DataBind();
    }

    /// <summary>
    /// 绑定数据到DROPDOWNLIST
    /// </summary>
    /// <param name="dropname">DROPDOWNLIST名</param>
    /// <param name="tablename">表名</param>
    /// <param name="txtfield">显示的字段名</param>
    /// <param name="valfield">ID</param>
    /// <param name="strWhere">条件</param>
    public static void DropDataBind(DropDownList dropname, string tablename, string txtfield, string valfield, string strWhere)
    {
        string strSql = "";
        if (strWhere == "")
        {
            strSql = "select * from " + tablename + "";
        }
        else
        {
            strSql = "select * from " + tablename + " where " + strWhere + "";
        }
        dropname.DataSource = GetDataSource(strSql, "'" + tablename + "'");
        dropname.DataTextField = "" + txtfield + "";
        dropname.DataValueField = "" + valfield + "";
        dropname.DataBind();
    }
    /// <summary>
    /// 生成数据表
    /// </summary>
    /// <param name="Sql">SQL语句</param>
    /// <param name="TableName">填充的表名</param>
    /// <returns>DataTable对象</returns>

    public static DataTable GetDataSource(string Sql, string TableName)
    {
        SqlConnection conn = new SqlConnection(Connectionstring);
        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter adapter = new SqlDataAdapter(Sql, conn);
            DataTable table = new DataTable(TableName);
            adapter.Fill(table);
            return table;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }
    /// <summary>
    /// 根据给定字段查询某一字段
    /// </summary>
    /// <param name="Displayfield">所要查的字段</param>
    /// <param name="TableName">表名</param>
    /// <param name="conField">条件字段</param>
    /// <param name="conValues">条件值</param>
    /// <returns></returns>
    public static object GetField(string Displayfield, string TableName, string conField, string conValues)
    {
        SqlConnection conn = new SqlConnection(Connectionstring);

        string strSql = "select " + " " + Displayfield + " " + " from " + " " + TableName + " " + "where" + " " + conField + " = '" + conValues + "'";

        SqlCommand comm = new SqlCommand(strSql, conn);
        object obj;

        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            obj = comm.ExecuteScalar();

            return obj;
        }
        catch (SqlException ex)
        {
            string error = ex.Message.ToString();
            return null;
        }
        finally
        {
            conn.Close();
        }
    }
    /// <summary>
    /// 根据给定字段获取另一字段
    /// </summary>
    /// <param name="Displayfield">要获取的字段名</param>
    /// <param name="TableName">表名</param>
    /// <param name="conField">条件字段名</param>
    /// <param name="conValues">条件字段值</param>
    /// <param name="conField1">条件字段名2</param>
    /// <param name="conValues1">条件字段值2</param>
    /// <returns></returns>
    public static object GetField(string Displayfield, string TableName, string conField, string conValues, string conField1, int conValues1)
    {
        SqlConnection conn = new SqlConnection(Connectionstring);

        string strSql = "select " + " " + Displayfield + " " + " from " + " " + TableName + " " + "where" + " " + conField + " = '" + conValues + "' and " + conField1 + "=" + conValues1 + "";

        SqlCommand comm = new SqlCommand(strSql, conn);
        object obj;

        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            obj = comm.ExecuteScalar();

            return obj;
        }
        catch (SqlException ex)
        {
            string error = ex.Message.ToString();
            return null;
        }
        finally
        {
            conn.Close();
        }
    }
    /// <summary>
    /// 根据给定字段获取另一字段
    /// </summary>
    /// <param name="Displayfield">要获取的字段名</param>
    /// <param name="TableName">表名</param>
    /// <param name="conField">条件字段名</param>
    /// <param name="conValues">条件字段值</param>
    /// <param name="conField1">条件字段名2</param>
    /// <param name="conValues1">条件字段值2</param>
    /// <returns></returns>
    public static object GetField(string Displayfield, string TableName, string conField, string conValues, string conField1, string conValues1)
    {
        SqlConnection conn = new SqlConnection(Connectionstring);

        string strSql = "select " + " " + Displayfield + " " + " from " + " " + TableName + " " + "where" + " " + conField + " = '" + conValues + "' and " + conField1 + "='" + conValues1 + "'";

        SqlCommand comm = new SqlCommand(strSql, conn);
        object obj;

        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            obj = comm.ExecuteScalar();

            return obj;
        }
        catch (SqlException ex)
        {
            string error = ex.Message.ToString();
            return null;
        }
        finally
        {
            conn.Close();
        }
    }
    /// <summary>
    /// 根据给定字段查询某一字段
    /// </summary>
    /// <param name="Displayfield">所要查的字段</param>
    /// <param name="TableName">表名</param>
    /// <param name="conField">条件字段</param>
    /// <param name="conValues">条件值</param>
    /// <returns></returns>
    public static object GetField(string Displayfield, string TableName, string conField, int conValues)
    {
        SqlConnection conn = new SqlConnection(Connectionstring);

        string strSql = "select " + " " + Displayfield + " " + " from " + " " + TableName + " " + "where" + " " + conField + " = " + conValues + "";

        SqlCommand comm = new SqlCommand(strSql, conn);
        object obj;

        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            obj = comm.ExecuteScalar();

            return obj;
        }
        catch (SqlException ex)
        {
            string error = ex.Message.ToString();
            return null;
        }
        finally
        {
            conn.Close();
        }
    }

}
