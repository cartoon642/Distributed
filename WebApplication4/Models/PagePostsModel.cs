using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class PagePostsModel
    {
        public List<Data> data { get; set; }
    }

    public class Data
    {
        public string created_time { get; set; }
        public string message { get; set; }
        public string id { get; set; }
    }

    


    //public class facebookModel
    //{
    //    public string name { get; set; }
    //    public string id { get; set; }
    //}


}
