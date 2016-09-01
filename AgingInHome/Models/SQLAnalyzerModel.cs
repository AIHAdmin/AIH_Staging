using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class SQLAnalyzerModel
    {
        [DataType(DataType.MultilineText)]
        public string body { get; set; }
    }
}