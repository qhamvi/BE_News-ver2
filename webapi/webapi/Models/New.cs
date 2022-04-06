using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class New
    {
        public int NewId { get; set; }

        public string NewTitle { get; set; }
        public string NewSummary { get; set; }
        public string NewContent { get; set; }
        public string NewStatus { get; set; }
        public string User { get; set; }
        public string Topic { get; set; }
        public string createDate { get; set; }
        public string publishDate { get; set; }
        public string ImageFileName { get; set; }
        public string Reason { get; set; }


    }
}
