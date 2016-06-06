using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class UserInfoService
    {
       UserInfoDal UserInfoDal = new UserInfoDal();
       public UserInfo GetUserInfo(string userName, string userPwd)//表现层调用业务层传过来是用户名密码
       {
           return UserInfoDal.GetUserInfo(userName,userPwd);
       }
    }
}
