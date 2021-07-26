using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using UpdatePractice.Models;

namespace UpdatePractice.Manager
{
    public class XmlManager
    {
        public void xmlCreate(Models.Package package,List<FileInfo> file)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", "no");
            XmlElement updateInfo = doc.CreateElement("update_info");
            updateInfo.SetAttribute("app_name",package.appName);
            updateInfo.SetAttribute("version", package.version);
            updateInfo.SetAttribute("count", "0");

            foreach (var f in file)
            {
                XmlElement files = doc.CreateElement(f.name);
                files.SetAttribute("version",f.version);
                files.SetAttribute("size", f.size);
                files.SetAttribute("reg", f.reg);
                files.SetAttribute("type", f.type);

                XmlElement remote = doc.CreateElement("remote");
                remote.InnerText = package.protocol + package.server + package.path + '/' + f.name;
                XmlElement local = doc.CreateElement("local");
                local.InnerText = f.local;
                XmlElement fileName = doc.CreateElement("file");
                fileName.InnerText = f.name;

                files.AppendChild(remote);
                files.AppendChild(local);
                files.AppendChild(fileName);


                updateInfo.AppendChild(files);
            }
            
            //string _xmlPath = AppDomain.CurrentDomain.BaseDirectory + "/XML/";
            //doc.Save(_xmlPath + package.appName + ".xml");

    }

    }
}