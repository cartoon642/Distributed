using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class TwitterFriendModel
    {
        public List<twitterFriend> users { get; set; }
    }

    public class twitterFriend
    {
        public string name { get; set; }
      
    }
}