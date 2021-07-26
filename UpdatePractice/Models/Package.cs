using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatePractice.Models
{
    public class Package
    {
        public int seq { get; set; }
        public string appName { get; set; }
        public string appCmd { get; set; }
        public string version { get; set; }
        public string server { get; set; }
        public string path { get; set; }
        public string state { get; set; }
        public string memo { get; set; }
        public string protocol { get; set; }
        public string delYN { get; set; }

        //추가
        public string new_appName { get; set; }
        public string new_appCmd { get; set; }
        public string new_protocol{ get; set; }
        public string new_server { get; set; }
        public string new_path{ get; set; }
        public string new_memo { get; set; }
    }
}