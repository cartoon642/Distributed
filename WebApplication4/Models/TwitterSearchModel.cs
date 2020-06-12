using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class TwitterSearchModel
    {
        public List<twittersearch> statuses { get; set; }
    }

    public class twittersearch
    {
        public string created_at { get; set; }
        public string text { get; set; }
    }
}