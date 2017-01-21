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
///DbHelperSQL ���ݿ����
///by huyi xel 2011-05-30
/// </summary>
public abstract class DbHelperSQL
{
    public static string Connectionstring = ConfigurationManager.ConnectionStrings["SqlConnStr"].ToString();
	public DbHelperSQL()
    {
        
    }
    
    #region ִ��SQL��䣬����DataSet���ݼ�
    /// <summary>
    /// ִ��SQL��䣬����DataSet���ݼ�
    /// </summary>
    /// <param name="StrSQL">Ҫִ�е�SQL���</param>
    /// <returns>����DataSet���ݼ�</returns>
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

    #region ִ��SQL��䣬���ص�һ��DataTable���ݱ�
    /// <summary>
    /// ִ��SQL��䣬���ص�һ��DataTable���ݱ�
    /// </summary>
    /// <param name="StrSQL">Ҫִ�е�SQL���</param>
    /// <returns>����DataTable���ݱ�</returns>
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

    #region ִ��SQL��䣬����Ӱ��ļ�¼��
    /// <summary>
    /// ִ��SQL��䣬����Ӱ��ļ�¼��
    /// </summary>
    /// <param name="StrSQL">Ҫִ�е�SQL���</param>
    /// <returns>����Ӱ��ļ�¼��</returns>
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

    #region ִ��SQL��䣬���ص�һ�е�һ��
    /// <summary>
    /// ִ��SQL��䣬���ص�һ�е�һ��
    /// </summary>
    /// <param name="StrSQL">Ҫִ�е�SQL���</param>
    /// <returns>���ص�һ�е�һ��</returns>
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

    #region ִ��SQL��䣬����SqlDataAdapter
    /// <summary>
    /// ִ��SQL��䣬����SqlDataAdapter
    /// </summary>
    /// <param name="StrSQL">Ҫִ�е�SQL���</param>
    /// <returns>����SqlDataAdapter</returns>
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

    #region ִ��SQL��䣬����SqlDataReader
    /// <summary>
    /// ִ��SQL��䣬����SqlDataReader
    /// </summary>
    /// <param name="StrSQL">Ҫִ�е�SQL���</param>
    /// <returns>����SqlDataReader</returns>
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

    #region ִ�ж���SQL��䣬ʵ�����ݿ��������˳��ִ��
    /// <summary>
    /// ִ�ж���SQL��䣬ʵ�����ݿ��������˳��ִ��
    /// </summary>
    /// <param name="SQLStringList">����SQL���</param>	
    /// <returns>����Ӱ���¼��</returns>
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

    #region ִ�д�һ���洢���̲����ĵ�SQL���,����Ӱ���¼��
    /// <summary>
    /// ִ�д�һ���洢���̲����ĵ�SQL��䡣����һ���ֶ��Ǹ�ʽ���ӵ����£���������ţ�����ͨ�������ʽ���
    /// </summary>
    /// <param name="SQLString">SQL���</param>
    /// <param name="content">��������</param>
    /// <returns>Ӱ��ļ�¼��</returns>
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

    #region  ִ��һ�������ѯ�����䣬���ز�ѯ�����object��
    /// <summary>
    /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
    /// </summary>
    /// <param name="SQLString">�����ѯ������</param>
    /// <returns>��ѯ�����object��</returns>
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

    #region ִ�д洢���̣�����DataSet���ݼ�
    /// <summary>
    /// ִ�д洢���̣�����DataSet���ݼ�
    /// </summary>
    /// <param name="storedProcName">�洢������</param>
    /// <param name="parameters">�洢���̲���</param>
    /// <returns>����DataSet���ݼ�</returns>
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

    #region ִ�д洢���̣�����Ӱ���¼��
    /// <summary>
    /// ִ�д洢���̣�����Ӱ���¼��
    /// </summary>
    /// <param name="storedProcName">�洢������</param>
    /// <param name="parameters">�洢���̲���</param>
    /// <returns>����Ӱ���¼��</returns>
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

    #region ִ�д洢���� ����SqlDataReader�����
    /// <summary>
    /// ִ�д洢���� ����SqlDataReader�����
    /// </summary>
    /// <param name="storedProcName">�洢������</param>
    /// <param name="parameters">�洢���̲���</param>
    /// <returns>����SqlDataReader�����</returns>
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

    #region ����һ�� SqlCommand ����
    /// <summary>
    /// ���� SqlCommand ����
    /// </summary>
    /// <param name="connection">���ݿ�����</param>
    /// <param name="storedProcName">�洢������</param>
    /// <param name="parameters">�洢���̲���</param>
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
    /// �����ݵ�DROPDOWNLIST
    /// </summary>
    /// <param name="dropname">DROPDOWNLIST��</param>
    /// <param name="tablename">����</param>
    /// <param name="txtfield">��ʾ���ֶ���</param>
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
    /// �����ݵ�DROPDOWNLIST
    /// </summary>
    /// <param name="dropname">DROPDOWNLIST��</param>
    /// <param name="tablename">����</param>
    /// <param name="txtfield">��ʾ���ֶ���</param>
    /// <param name="valfield">ID</param>
    /// <param name="strWhere">����</param>
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
    /// �������ݱ�
    /// </summary>
    /// <param name="Sql">SQL���</param>
    /// <param name="TableName">���ı���</param>
    /// <returns>DataTable����</returns>

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
    /// ���ݸ����ֶβ�ѯĳһ�ֶ�
    /// </summary>
    /// <param name="Displayfield">��Ҫ����ֶ�</param>
    /// <param name="TableName">����</param>
    /// <param name="conField">�����ֶ�</param>
    /// <param name="conValues">����ֵ</param>
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
    /// ���ݸ����ֶλ�ȡ��һ�ֶ�
    /// </summary>
    /// <param name="Displayfield">Ҫ��ȡ���ֶ���</param>
    /// <param name="TableName">����</param>
    /// <param name="conField">�����ֶ���</param>
    /// <param name="conValues">�����ֶ�ֵ</param>
    /// <param name="conField1">�����ֶ���2</param>
    /// <param name="conValues1">�����ֶ�ֵ2</param>
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
    /// ���ݸ����ֶλ�ȡ��һ�ֶ�
    /// </summary>
    /// <param name="Displayfield">Ҫ��ȡ���ֶ���</param>
    /// <param name="TableName">����</param>
    /// <param name="conField">�����ֶ���</param>
    /// <param name="conValues">�����ֶ�ֵ</param>
    /// <param name="conField1">�����ֶ���2</param>
    /// <param name="conValues1">�����ֶ�ֵ2</param>
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
    /// ���ݸ����ֶβ�ѯĳһ�ֶ�
    /// </summary>
    /// <param name="Displayfield">��Ҫ����ֶ�</param>
    /// <param name="TableName">����</param>
    /// <param name="conField">�����ֶ�</param>
    /// <param name="conValues">����ֵ</param>
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
