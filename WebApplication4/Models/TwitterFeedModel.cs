using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class TwitterFeedModel
    {
        public List<TwitterPost> data { get; set; }
    }

    public class TwitterPost
    {
        public string created_at { get; set; }
        public string text { get; set; }
    }
}