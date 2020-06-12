using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Endpoint
{

    public class twitterEndpoint
    {

        private const string baseEndpoint = "https://api.twitter.com";
        private const string version = "1.1";
        


        private string signature = string.Empty;
        public void setAuth(string signature)
        {
            this.signature = signature;

        }
        public string getTimeline() 
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append($"/statuses");
            stringBuilder.Append("/home_timeline.json");

            return stringBuilder.ToString();
        }

        public string getFavourites() 
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append($"/favorites");
            stringBuilder.Append("/list.json");

            return stringBuilder.ToString();
        }
        public string getFriends() 
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append($"/friends");
            stringBuilder.Append("/list.json");

            return stringBuilder.ToString();
        }

        public string getFollower()
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append($"/followers");
            stringBuilder.Append("/list.json");

            return stringBuilder.ToString();
        }

        public string getSearch(string message) 
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append($"/search");
            stringBuilder.Append("/tweets.json");
            stringBuilder.Append($"?q={message}");

            return stringBuilder.ToString();
        }

        public string PostTwitter(string message) 
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append($"/statuses");
            stringBuilder.Append("/update.json?");
            stringBuilder.Append($"status={message}");
            return stringBuilder.ToString();
        }
        //headers in api
        public Dictionary<string, string> getHeader()
        {
            return new Dictionary<string, string>{
                {"Authorization",signature}
                
            };
        }
    }
}