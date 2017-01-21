using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
///databse 的摘要说明
/// </summary>
 public class SQLBackupAndRestore
    {

        #region "构造函数"
        /// <summary>
        /// 不带参数的构造函数
        /// </summary>
        public SQLBackupAndRestore()
        {
           
        }
        #endregion

        #region "数据库参数返回"
        /// <summary>
        /// 返回服务器列表
        /// </summary>
        /// <returns>返回服务器列表的ArrayList对象</returns>
        public static ArrayList SqlServerNameList()
        {
            try
            {
                SQLDMO.NameList sqlServers = sqlApp.ListAvailableSQLServers();
                for (int i = 0; i < sqlServers.Count; i++)
                {
                    object srvname = sqlServers.Item(i + 1);
                    if (srvname != null)
                    {
                        sqlServerName.Add(srvname);
                    }
                }
                return sqlServerName;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 数据库服务器连接
        /// </summary>
        /// <param name="sServer">服务器地址</param>
        /// <param name="sUserName">数据库用户名</param>
        /// <param name="sPwd">数据库密码</param>
        /// <returns>true/false</returns>
        public static bool SqlCon(string sServer, string sUserName, string sPwd)
        {
            sqlServer = sServer;
            sqlUserName = sUserName;
            sqlPwd = sPwd;
            try
            {
                srv = new SQLDMO.SQLServerClass();
                srv.Connect(sqlServer, sqlUserName, sqlPwd);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 返回数据库对象
        /// </summary>
        /// <returns>sqlTableName.ToArray()</returns>
        public static ArrayList SqlDBNameList()
        {
            foreach (SQLDMO.Database db in srv.Databases)
            {
                if (db.Name != null)
                {
                    sqlDBName.Add(db.Name);
                }
            }
            return sqlDBName;
        }

        /// <summary>
        /// 返回数据库表对象
        /// </summary>
        /// <param name="sqlDataBase">数据库名</param>
        /// <returns>返回数据库表对象</returns>
        public static ArrayList SqlTableNameList(string sqlDataBase)
        {
            for (int i = 0; i < srv.Databases.Count; i++)
            {
                if (srv.Databases.Item(i + 1, "dbo").Name == sqlDataBase)
                {
                    SQLDMO._Database db = srv.Databases.Item(i + 1, "dbo");
                    sqlTableName.Clear();
                    for (int j = 0; j < db.Tables.Count; j++)
                    {
                        sqlTableName.Add(db.Tables.Item(j + 1, "dbo").Name);
                    }
                }
            }
            return sqlTableName;
        }

        /// <summary>
        /// 返回数据库存储过程对象
        /// </summary>
        /// <param name="sqlDataBase">数据库名</param>
        /// <returns>返回数据库存储过程对象</returns>
        public static ArrayList SqlStoredProceduresNameList(string sqlDataBase)
        {
            for (int i = 0; i < srv.Databases.Count; i++)
            {
                if (srv.Databases.Item(i + 1, "dbo").Name == sqlDataBase)
                {
                    SQLDMO._Database db = srv.Databases.Item(i + 1, "dbo");
                    sqlStoredProceduresName.Clear();
                    for (int j = 0; j < db.StoredProcedures.Count; j++)
                    {
                        sqlStoredProceduresName.Add(db.StoredProcedures.Item(j + 1, "dbo").Name);
                    }
                }
            }
            return sqlStoredProceduresName;
        }

        /// <summary>
        /// 数据库视图对象
        /// </summary>
        /// <param name="sqlDataBase">数据库名</param>
        /// <returns>数据库视图对象</returns>
        public static ArrayList SqlViewsNameList(string sqlDataBase)
        {
            for (int i = 0; i < srv.Databases.Count; i++)
            {
                if (srv.Databases.Item(i + 1, "dbo").Name == sqlDataBase)
                {
                    SQLDMO._Database db = srv.Databases.Item(i + 1, "dbo");
                    sqlViewName.Clear();
                    for (int j = 0; j < db.Views.Count; j++)
                    {
                        sqlViewName.Add(db.Views.Item(j + 1, "dbo").Name);
                    }
                }
            }
            return sqlViewName;
        }
        #endregion

        #region "数据库备份"
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="sServer">服务器名</param>
        /// <param name="sUserName">用户名</param>
        /// <param name="sPwd">密码</param>
        /// <param name="BackUpDB">要备份的数据库名</param>
        /// <param name="FilePath">备份文件存放路径</param>
        /// <param name="BakcUpDBName">备份文件名</param>
        /// <param name="TruncateLog">备份日志选项。其选项有：NoLog - 不备份交易日志/0。NoTruncate - 备份交易日志。日志里提供时间标记/1。Truncate - 备份交易日志，但不保留交易纪录/2。</param>
        /// <returns>true/false</returns>
        public static bool SqlBackUp(string sServer, string sUserName, string sPwd, string BackUpDB, string FilePath, string BakcUpDBName, TruncateLog TLog)
        {
            //实例对象
            bkps = new SQLDMO.BackupClass();
            if (SqlCon(sServer, sUserName, sPwd))
            {
                try
                {
                    bkps.Database = BackUpDB; //指定需备份的数据库
                    bkps.Action = 0;
                    bkps.BackupSetName = BakcUpDBName;
                    bkps.Files = @FilePath + @"\" + BakcUpDBName; //指定备份文件路径
                    bkps.Initialize = true; //如设置为真(True)，该备份装置将取代其他备份媒介而成为首选。
                    bkps.TruncateLog = (SQLDMO.SQLDMO_BACKUP_LOG_TYPE)TLog;
                    bkps.SQLBackup(srv);
                    bkps = null;
                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    errorMessage = "";
                    return false;
                }
                finally
                {
                    srv.DisConnect();
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 枚举备份日志类型
        /// </summary>
        public enum TruncateLog
        {
            NoLog = SQLDMO.SQLDMO_BACKUP_LOG_TYPE.SQLDMOBackup_Log_NoLog, //不备份交易日志
            NoTruncate = SQLDMO.SQLDMO_BACKUP_LOG_TYPE.SQLDMOBackup_Log_NoTruncate, //备份交易日志。日志里提供时间标记
            Truncate = SQLDMO.SQLDMO_BACKUP_LOG_TYPE.SQLDMOBackup_Log_Truncate //备份交易日志，但不保留交易纪录
        }
        #endregion

        #region "数据库还原"
        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="sServer">服务器名</param>
        /// <param name="sUserName">用户名</param>
        /// <param name="sPwd">密码</param>
        /// <param name="FilePath">要还原的数据库文件路径/param>
        /// <param name="RestoreDBName">要还原的数据库名</param>
        /// <returns>true/false</returns>
        public static bool SQLRestoreDB(string sServer, string sUserName, string sPwd, string FilePath, string RestoreDBName)
        {
            oRestore = new SQLDMO.RestoreClass();
            oRestore.Action = 0;
            if (SqlCon(sServer, sUserName, sPwd))
            {
                try
                {
                    SQLDMO.QueryResults qr = srv.EnumProcesses(-1);
                    int iColPIDNum = -1;
                    int iColDbName = -1;
                    //杀死其它的连接进程
                    for (int i = 1; i <= qr.Columns; i++)
                    {
                        string strName = qr.get_ColumnName(i);
                        if (strName.ToUpper().Trim() == "SPID")
                        {
                            iColPIDNum = i;
                        }
                        else if (strName.ToUpper().Trim() == "DBNAME")
                        {
                            iColDbName = i;
                        }
                        if (iColPIDNum != -1 && iColDbName != -1)
                            break;
                    }
                    for (int i = 1; i <= qr.Rows; i++)
                    {
                        int lPID = qr.GetColumnLong(i, iColPIDNum);
                        string strDBName = qr.GetColumnString(i, iColDbName);
                        if (strDBName.ToUpper() == "CgRecord".ToUpper())
                            srv.KillProcess(lPID);
                    }
                    oRestore.Action = SQLDMO.SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
                    oRestore.Database = RestoreDBName;
                    oRestore.Files = @FilePath;
                    oRestore.FileNumber = 1;
                    oRestore.ReplaceDatabase = true;
                    oRestore.SQLRestore(srv);
                    return true;
                }
                catch (System.Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
                finally
                {
                    srv.DisConnect();
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region "内部函数"
        /// <summary>
        /// 服务器地址
        /// </summary>
        private static object sqlServer;
        /// <summary>
        /// 数据库用户名
        /// </summary>
        private static object sqlUserName;
        /// <summary>
        /// 数据库密码
        /// </summary>
        private static object sqlPwd;
        /// <summary>
        /// 数据库备份对象
        /// </summary>
        private static SQLDMO.Backup bkps;
        /// <summary>
        /// 数据库还原对象
        /// </summary>
        private static SQLDMO.Restore oRestore;
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private static SQLDMO.SQLServer srv;
        private static SQLDMO.Application sqlApp = new SQLDMO.ApplicationClass();
        /// <summary>
        /// 错误消息
        /// </summary>
        private static string errorMessage;
        /// <summary>
        /// 服务器对象
        /// </summary>
        private static ArrayList sqlServerName = new ArrayList();
        /// <summary>
        /// 数据库表对象
        /// </summary>
        private static ArrayList sqlTableName = new ArrayList();
        /// <summary>
        /// 数据库对象
        /// </summary>
        private static ArrayList sqlDBName = new ArrayList();
        /// <summary>
        /// 数据库存储过程对象
        /// </summary>
        private static ArrayList sqlStoredProceduresName = new ArrayList();
        /// <summary>
        /// 数据库视图对象
        /// </summary>
        private static ArrayList sqlViewName = new ArrayList();
        #endregion

    }
