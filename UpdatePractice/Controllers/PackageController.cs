using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpdatePractice.DB;
using UpdatePractice.Models;

namespace UpdatePractice.Controllers
{
    public class PackageController : Controller
    {
        DBManager _manager = new DBManager();

        // GET: Package
        public ActionResult Index()
        {
            if(Session["UserInfo"] != null) {
                return View();
            }
            else
            {
                TempData["message"] = "로그인 하세요";
            }
            return RedirectToAction("", "Home");
        }

        [HttpPost]
        public JsonResult InsertNewApp(string appName, string appCmd, string server, string path, string memo, string protocol)
        {
            bool result = false;
            try
            {
                Package package = new Package();
                package.appName = appName;
                package.appCmd = appCmd;
                package.server = server;
                package.path = path;
                package.memo = memo;
                package.protocol = protocol;

                result = _manager.InsertApp(package);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Controller InsertNewApp : " + ex.Message);
            }


            return Json(result);
        }

        [HttpPost]
        public JsonResult GetAppList()
        {
            List<Package> appList = null;
            try
            {
                appList = _manager.GetAppList();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Controller GetAppList : " + ex.Message);
            }
            return Json(appList);
        }
        public JsonResult UpdateApp(int seq,string appName,string appCmd,string server,string path,string memo,string protocol,string version)
        {
            bool result = false;
            try
            {
                Package package = new Package();
                package.seq = seq;
                package.appName = appName;
                package.appCmd = appCmd;
                package.version = version;
                package.server = server;
                package.path = path;
                package.memo = memo;
                package.protocol = protocol;

                result = _manager.UpdateApp(package);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Controller UpdateApp : " + ex.Message);
            }


            return Json(result);
        }
        public JsonResult StopState(int seq)
        {
            bool result = false;

            try
            {
                result = _manager.StopState(seq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Controller StopState : " + ex.Message);
            }

            return Json(result);
        }
        public JsonResult UpdateState(int seq)
        {
            bool result = false;

            try
            {
                result = _manager.UpdateState(seq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Controller UpdateState : " + ex.Message);
            }

            return Json(result);
        }
        public JsonResult DeleteApp(int seq)
        {
            bool result = false;

            try
            {
                result = _manager.DeleteApp(seq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Controller DeleteApp : " + ex.Message);
            }

            return Json(result);
        }
        
    }

    
    
}