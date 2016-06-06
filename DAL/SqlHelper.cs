using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace DAL
{
    public class SqlHelper
    {
        private static readonly string connstr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        public static DataTable GetTable(string sql, CommandType type, params SqlParameter[] pars)  //查询 CommandType type表示执行sql文本命令
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.SelectCommand.CommandType = type;
                    if (pars != null)
                    {
                        da.SelectCommand.Parameters.AddRange(pars);
                    }
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;

                }
            }
        }
        public static int ExecuteNonquery(string sql, CommandType type, params SqlParameter[] pars)   //增删改
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static object ExecuteScalare(string sql, CommandType type, params SqlParameter[] pars)//返回一行一列
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
