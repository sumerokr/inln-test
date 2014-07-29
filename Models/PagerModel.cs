using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inln_test.Models
{
    public class PagerModel
    {
        public int ActivePage { get; set; }
        public int TotalPages { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}