using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 利郎企业官方网站.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        //执行控制器中方法之前先执行该方法。
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["userInfo"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}