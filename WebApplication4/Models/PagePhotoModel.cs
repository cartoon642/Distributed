using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class PagePhotoModel
    {
        public List<Data2> data { get; set; }
    }

    public class Data2
    {
        public string picture { get; set; }
        public string id { get; set; }
    }


    //public class facebookModel
    //{
    //    public string name { get; set; }
    //    public string id { get; set; }
    //}


}
