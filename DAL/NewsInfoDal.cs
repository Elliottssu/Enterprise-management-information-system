using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
   public class NewsInfoDal
    {
       /// <summary>
       /// 获取分页数据
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public List<NewsInfo> GetPageList(int start,int end)
       {
           string sql = "select * from (select row_number() over(order by id) as num,* from NewsInfo) as t where t.num>=@start and t.num<=@end"; //先把所有的产品筛选出来，然后对这些产品进行编号。然后在where子句中进行过滤。 
           SqlParameter[] pars = { 
                                 new SqlParameter("@start",SqlDbType.Int),
                                 new SqlParameter("@end",SqlDbType.Int)
                                 };
           pars[0].Value = start;
           pars[1].Value = end;
           DataTable da=SqlHelper.GetTable(sql, CommandType.Text, pars);
           List<NewsInfo> list = null; 
           if (da.Rows.Count > 0)
           {
               list = new List<NewsInfo>();
               NewsInfo newInfo = null;
               foreach (DataRow row in da.Rows) //datatable数据装到list集合返回  遍历所有行
               {
                   newInfo = new NewsInfo();
                   LoadEntity(row,newInfo);  
                   list.Add(newInfo);
               }
           }
           return list;
       }
       /// <summary>
       /// 行中的数据赋给实体类中的属性
       /// </summary>
       /// <param name="row"></param>
       /// <param name="newInfo"></param>
       private void LoadEntity(DataRow row, NewsInfo newInfo)
       {
           newInfo.Id = Convert.ToInt32(row["ID"]);
           newInfo.Author = row["Author"] != DBNull.Value ? row["Author"].ToString() : string.Empty;
           newInfo.Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty;
           newInfo.Msg = row["Msg"] != DBNull.Value ? row["Msg"].ToString() : string.Empty;
           newInfo.ImagePath = row["ImagePath"] != DBNull.Value ? row["ImagePath"].ToString() : string.Empty;
           newInfo.SubDateTime = Convert.ToDateTime(row["SubDateTime"]);
       }

       /// <summary>
       /// 获取总的记录数
       /// </summary>
       /// <returns></returns>
       public int GetRecordCount()
       {
           string sql = "select count(*) from NewsInfo";
         return Convert.ToInt32 (SqlHelper.ExecuteScalare(sql, CommandType.Text));
       }
       /// <summary>
       /// 获取一条记录
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public NewsInfo GetModel(int id)
       {
           string sql = "select * from NewsInfo where id=@id";
           SqlParameter[] pars = { 
                                 new SqlParameter("@id",SqlDbType.Int)
                                 };
           pars[0].Value = id;
            DataTable da=SqlHelper.GetTable(sql, CommandType.Text, pars);
            NewsInfo newInfo = null;
            if (da.Rows.Count > 0)
            {
                newInfo = new NewsInfo();
                LoadEntity(da.Rows[0], newInfo);
            }
            return newInfo;
       }
       /// <summary>
       /// 删除一条记录
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeleteInfo(int id)
       {
           string sql = "delete from NewsInfo where id=@id";
           return SqlHelper.ExecuteNonquery(sql, CommandType.Text, new SqlParameter("@id",id));
       }

       /// <summary>
       /// 添加一条记录
       /// </summary>
       /// <param name="newInfo"></param>
       /// <returns></returns>
       public int AddInfo(NewsInfo newInfo) //添加一条记录
       {
           string sql = "insert into NewsInfo(Author,Title,Msg,ImagePath,SubDateTime) values(@Author,@Title,@Msg,@ImagePath,@SubDateTime)";
           SqlParameter[] pars = { 
                                 new SqlParameter("@Author",SqlDbType.NVarChar,32),
                                  new SqlParameter("@Title",SqlDbType.NVarChar,32),
                               new SqlParameter("@Msg",SqlDbType.NVarChar),
                                new SqlParameter("@ImagePath",SqlDbType.NVarChar,100),
                                      new SqlParameter("@SubDateTime",SqlDbType.DateTime)
                                 };
           pars[0].Value = newInfo.Author;
           pars[1].Value = newInfo.Title;
           pars[2].Value = newInfo.Msg;
           pars[3].Value = newInfo.ImagePath;
           pars[4].Value = newInfo.SubDateTime;
         return   SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
       }

       public int UpdateInfo(NewsInfo newInfo)  //修改一条记录
       {
           string sql = "update NewsInfo set Title=@Title,Msg=@Msg,Author=@Author,SubDateTime=@SubDateTime,ImagePath=@ImagePath where Id=@Id";
           SqlParameter[] pars = { 
                                 new SqlParameter("@Title",SqlDbType.NVarChar,32),
                                  new SqlParameter("@Author",SqlDbType.NVarChar,32),
                                 new SqlParameter("@Msg",SqlDbType.NVarChar),
                                   new SqlParameter("@SubDateTime",SqlDbType.DateTime),
                                   new SqlParameter("@ImagePath",SqlDbType.NVarChar,100),
                                     new SqlParameter("@Id",SqlDbType.Int,4)
                                 };
           pars[0].Value = newInfo.Title;
           pars[1].Value = newInfo.Author;
           pars[2].Value = newInfo.Msg;
           pars[3].Value = newInfo.SubDateTime;
           pars[4].Value = newInfo.ImagePath;
           pars[5].Value = newInfo.Id;
           return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
       }
    }
}

