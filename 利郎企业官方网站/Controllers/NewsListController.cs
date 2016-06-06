using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace 利郎企业官方网站.Controllers
{
    public class NewsListController : Controller
    {
        //
        // GET: /NewsList/

        BLL.NewsInfoService NewInfoService = new BLL.NewsInfoService();
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 4;
            int pageCount = NewInfoService.GetPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<NewsInfo> list = NewInfoService.GetPageList(pageIndex, pageSize);
            ViewData["list"] = list;
            ViewBag.PageIndex = pageIndex;  //ViewBag动态表达式 运行时候会解析pageIndex
            ViewBag.PageCount = pageCount;
            return View();
        }

        /// <summary>
        /// 详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowDetail()
        {
            int id = int.Parse(Request["id"]);
            NewsInfo newInfo = NewInfoService.GetModel(id);
            ViewData.Model = newInfo;
            return View();
        }

    }
}