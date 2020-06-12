using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class PageLikeModels
    {
        public List<Data1> data { get; set; }
    }

    public class Data1
    {
        public string id { get; set; }
        public string name { get; set; }
    }   


    //public class facebookModel
    //{
    //    public string name { get; set; }
    //    public string id { get; set; }
    //}


}
