
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;
namespace DAL
{
   public class UserInfoDal
    {
       public UserInfo GetUserInfo(string userName, string userPwd)
       {
           string sql = "select * from UserInfo where UserName=@UserName and UserPwd=@UserPwd";
           SqlParameter[] pars = { 
                                 new SqlParameter("@UserName",SqlDbType.NVarChar,32),
                                   new SqlParameter("@UserPwd",SqlDbType.NVarChar,32),
                                 };
           pars[0].Value = userName;
           pars[1].Value = userPwd;
           DataTable da=SqlHelper.GetTable(sql, CommandType.Text, pars);
           UserInfo userInfo = null;
           if (da.Rows.Count > 0)
           {
               userInfo = new UserInfo();
               LoadEntity(userInfo,da.Rows[0]);
           }
           return userInfo;
       }




       public void LoadEntity(UserInfo userInfo, DataRow row)
       {
           userInfo.Id = Convert.ToInt32(row["Id"]);
           userInfo.UserName = row["UserName"] != DBNull.Value ? row["UserName"].ToString() : string.Empty;//字段不能为空
           userInfo.UserPwd = row["UserPwd"] != DBNull.Value ? row["UserPwd"].ToString() : string.Empty;
       }
    }
}
