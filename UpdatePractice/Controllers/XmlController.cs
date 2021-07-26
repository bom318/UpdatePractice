using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpdatePractice.DB;
using UpdatePractice.Manager;
using UpdatePractice.Models;

namespace UpdatePractice.Controllers
{
    public class XmlController : Controller
    {
        // GET: Xml
        public ActionResult Index()
        {
            

            return View();
        }
        [HttpPost]
        public void GetXml(string state, string seq,string appName,string version, string protocol, string server, string path)
        {
            if (state.Equals("중지"))
            {
                Response.Write("UPDATE가 중지되어 있습니다.");
            }
            else
            {
                Package package = new Package();
                package.seq = int.Parse(seq);
                package.appName = appName;
                package.version = version;
                package.protocol = protocol;
                package.server = server;
                package.path = path;

                DBManager manager = new DBManager();

                List<FileInfo> file = manager.GetFileList(package.seq);

                XmlManager xmlManager = new XmlManager();
                xmlManager.xmlCreate(package, file);
            }
        }
    }
}