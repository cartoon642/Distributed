using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class PageTokenModel
    {
        public List<DataPageToken> data { get; set; }
    }

    public class DataPageToken
    {
        public string access_token { get; set; }

        public string id { get; set; }
    }

}
