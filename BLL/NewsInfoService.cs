using Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class NewsInfoService
    {
       NewsInfoDal NewInfoDal = new NewsInfoDal();
     /// <summary>
     /// 获取分页数据
     /// </summary>
     /// <param name="pageIndex">当前页码值</param>
     /// <param name="pageSize">每页显示的记录数</param>
     /// <returns></returns>
       public List<NewsInfo> GetPageList(int pageIndex,int pageSize)
       {
           int start = (pageIndex - 1) * pageSize + 1;
           int end = pageIndex * pageSize;    //计算起始位置和终止位置
           List<NewsInfo> list = NewInfoDal.GetPageList(start, end);
           return list;
       }
       /// <summary>
       /// 获取总的页数
       /// </summary>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public int GetPageCount(int pageSize)
       {
           int recordCount = NewInfoDal.GetRecordCount();
          int pageCount=Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));//天花板函数
          return pageCount;
       }
       /// <summary>
       /// 获取一条记录
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public NewsInfo GetModel(int id)
       {
           return NewInfoDal.GetModel(id);
       }
       /// <summary>
       /// 删除一条记录
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public bool DeleteInfo(int id)
       {
           return NewInfoDal.DeleteInfo(id)>0;  //返回bool类型 判断所影响行数是否>0
       }

       public bool AddInfo(NewsInfo newInfo)   //添加
       {
           return NewInfoDal.AddInfo(newInfo) > 0;
       }
       public bool UpdateInfo(NewsInfo newInfo)  //修改
       {
           return NewInfoDal.UpdateInfo(newInfo) > 0;
       }
    }
}
