using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpdatePractice.DB;
using UpdatePractice.Models;

namespace UpdatePractice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userId,string userPw)
        {
            DBManager manager = new DBManager();
            string ip = HttpContext.Request.ServerVariables["REMOTE_ADDR"];

            User sessionInfo = manager.GetUserInfo(userId, userPw);


            if (sessionInfo != null)
            {
                sessionInfo.userIP = ip;
                Session["UserInfo"] = sessionInfo;
                //다음 페이지로
                return RedirectToAction("", "Package");
            }
            else
            {
                TempData["message"] = "로그인 실패";
            }

            return View();
            
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();

            return RedirectToAction("", "Home");
        }
    }
}