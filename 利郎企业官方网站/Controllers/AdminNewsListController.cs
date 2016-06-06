using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using System.IO;
namespace 利郎企业官方网站.Controllers
{
    public class AdminNewsListController : BaseController
    {
        //
        // GET: /AdminNewsList/
        NewsInfoService NewsInfoService = new NewsInfoService();
        #region 分页列表
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;  //传页码值
            int pageSize = 5;
            int pageCount = NewsInfoService.GetPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex; //不能超出总页数
            List<NewsInfo> list = NewsInfoService.GetPageList(pageIndex, pageSize);//获取分页的数据 传到业务层
            ViewData["list"] = list;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        }

        #endregion

        #region 获取详细记录
        public ActionResult GetNewsInfoModel()
        {
            int id = int.Parse(Request["id"]);
            NewsInfo newInfo = NewsInfoService.GetModel(id);//获取详细信息.
            return Json(newInfo, JsonRequestBehavior.AllowGet);  //get方法请求的话会抛出异常
        }
        #endregion

        #region 删除信息
        public ActionResult DeletesNewsInfo()
        {
            int id = int.Parse(Request["id"]);  //id传到业务层再传到数据层
            if (NewsInfoService.DeleteInfo(id))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 展示添加信息的表单页面
        public ActionResult ShowAddInfo()
        {
            return View();
        }
        #endregion

        #region 文件上传
        public ActionResult FileUpload()
        {
            HttpPostedFileBase postFile = Request.Files["fileUp"];//接收文件数据
            if (postFile == null)
            {
                return Content("no:请选择上传文件");
            }
            else
            {
                string fileName = Path.GetFileName(postFile.FileName);//文件名称
                string fileExt = Path.GetExtension(fileName);//文件的扩展名称.
                if (fileExt == ".jpg")
                {
                    string dir = "/ImagePath/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";   //将上传的文件放在不同文件夹下
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹.
                    string newfileName = Guid.NewGuid().ToString();//新的文件名称.
                    string fullDir = dir + newfileName + fileExt;//完整的路径.
                    postFile.SaveAs(Request.MapPath(fullDir));//保存文件.
                    return Content("ok:" + fullDir);  //返回文件路径

                }
                else
                {
                    return Content("no:文件格式错误!!");
                }

            }
        }
        #endregion

        #region 添加信息
        [ValidateInput(false)]  //可以接受提交过来的Input标签
        public ActionResult AddNewsInfo(NewsInfo newInfo)  //文本框值会自动赋给相应实体属性
        {
            newInfo.SubDateTime = DateTime.Now;
            if (NewsInfoService.AddInfo(newInfo))    //调用业务层再调用数据层
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 展示要修改的数据
        public ActionResult ShowEdit()
        {
            int id = int.Parse(Request["id"]);
            NewsInfo newInfo = NewsInfoService.GetModel(id);
            ViewData.Model = newInfo;
            return View();
        }

        #endregion

        #region 完成信息修改
        public ActionResult EditNewsInfo(NewsInfo newInfo)
        {
            if (NewsInfoService.UpdateInfo(newInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");

            }
        }
        #endregion
    }

}
