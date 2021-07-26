using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpdatePractice.DB;
using UpdatePractice.Models;

namespace UpdatePractice.Controllers
{
    public class FileController : Controller
    {
        DBManager _manager = new DBManager();
        // GET: File
        
        public ActionResult Index()
        {
           

            return View();
        }
        [HttpPost]
        public JsonResult GetFileList(string seq)
        {
            int num = int.Parse(seq);
            List<Models.FileInfo> appList = null;
            try
            {
                appList = _manager.GetFileList(num);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Controller GetAppList : " + ex.Message);
            }
            return Json(appList);
        }
        [HttpPost]
        public JsonResult InsertNewFile()
        {
            bool result = false;
            try
            {
                //파일 저장
               if ( Request.Files.Count > 0 )
                {

                    var file1 = Request.Files[0];
                    Models.FileInfo file = new Models.FileInfo();

                    var pathRoot = @"E:\practice\UpdatePractice\UpdatePractice\UpdatePractice\Storage\";
                    
                    
                    file.appSeq = int.Parse(Request.Form["seq"]);
                    var pathMid = file.appSeq + @"\";
                    file.tagName = System.IO.Path.GetFileNameWithoutExtension(file1.FileName);
                    file.type = file1.ContentType;
                    file.name = file1.FileName;
                    file.path = pathRoot + pathMid + file1.FileName;
                    file.size = (file1.ContentLength).ToString();
                    
                    file.local = Request.Form["local"];
                    file.version = DateTime.Now.ToString("yyyyMMddHHmmss");

                    result = _manager.InsertFile(file);

                    

                    var folderPath = pathRoot + pathMid;
                    DirectoryInfo di = new DirectoryInfo(folderPath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    file1.SaveAs(pathRoot + pathMid + file1.FileName);

                    
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Controller InsertNewFile : " + ex.Message);
            }


            return Json(result);
        }
        public JsonResult DeleteFile(string seq,string appSeq,string name)
        {
            bool result = false;

            try
            {
                
                var pathRoot = @"E:\practice\UpdatePractice\UpdatePractice\UpdatePractice\Storage\";
                var pathMid = appSeq + @"\";
                //파일 삭제
                if (System.IO.File.Exists(pathRoot +pathMid+ name))
                {
                    System.IO.File.Delete(pathRoot + pathMid + name);
                }
                int num = int.Parse(seq);
                result = _manager.DeleteFile(num);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Controller DeleteFile : " + ex.Message);
            }

            return Json(result);
        }
        public JsonResult UpdateNewFile()
        {
            bool result = false;
            try
            {
               
                var file1 = Request.Files[0];

                var pathRoot = @"E:\practice\UpdatePractice\UpdatePractice\UpdatePractice\Storage\";
                var pathMid = Request.Form["seq"] + @"\";
                var folderPath = pathRoot + pathMid;

                //파일 삭제
                if (System.IO.File.Exists(pathRoot + pathMid + file1.FileName))
                {
                    System.IO.File.Delete(pathRoot + pathMid + file1.FileName);
                }
                else
                {
                    TempData["message"] = "등록되어있지 않은 파일입니다.";
                }

                //파일 재등록

                Models.FileInfo file = new Models.FileInfo();
                file.tagName = System.IO.Path.GetFileNameWithoutExtension(file1.FileName);
                file.type = file1.ContentType;
                file.name = file1.FileName;
                file.path = folderPath + file1.FileName;
                file.size = (file1.ContentLength).ToString();
                file.appSeq = int.Parse(Request.Form["seq"]);
                file.local = Request.Form["local"];
                file.version = DateTime.Now.ToString("yyyyMMddHHmmss");
                file.reg = Request.Form["reg"];

                

                file1.SaveAs(folderPath + file1.FileName);


                result = _manager.UpdateNewFile(file);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Controller UpdateFile : " + ex.Message);
            }


            return Json(result);
        }
        public JsonResult UpdateFile(string seq,string path,string name,string local, string tagName, string version, string type, string reg)
        {
            bool result = false;
            try
            {

                var pathRoot = @"E:\practice\UpdatePractice\UpdatePractice\UpdatePractice\Storage\";
                var pathMid = seq + @"\";
                var folderPath = pathRoot + pathMid;

                Models.FileInfo file = new Models.FileInfo();
                file.tagName = tagName;
                file.type = type;
                file.name = name;
                file.path = folderPath + name;
                file.local = local;
                file.version = version;
                file.reg = reg;

                
                
                var newPath = folderPath + name;

                System.IO.File.Move(path, newPath);


                result = _manager.UpdateFile(file);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Controller UpdateNewFile : " + ex.Message);
            }


            return Json(result);
        }
    }
}