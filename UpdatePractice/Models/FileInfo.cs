using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdatePractice.Models
{
    public class FileInfo
    {
        public int seq { get; set; }
        public string name { get; set; }
        public string local { get; set; }
        public string tagName { get; set; }
        public string version { get; set; }
        public string type { get; set; }
        public string reg { get; set; }
        public string size { get; set; }
        public int appSeq { get; set; }
        public string path { get; set; }

    }
}