using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Endpoint
{
    public class facebookEndpoint
    {
        private const string baseEndpoint = "https://graph.facebook.com";
        private const string version = "v7.0";
        


        public string getPosts(string accessToken)
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append("/me/posts?");
            
                stringBuilder.Append($"access_token={accessToken}");
            
            return stringBuilder.ToString();
        }

        public string getLikes(string accessToken)
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            
            stringBuilder.Append("/me/likes?");
            
            stringBuilder.Append($"access_token={accessToken}");
            
            return stringBuilder.ToString();
        }
        public string getFeed(string accessToken)
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append("/me/feed?");
            if (!string.IsNullOrEmpty(accessToken))
            {
                stringBuilder.Append($"/?access_token={accessToken}");
            }
            return stringBuilder.ToString();
        }
        //getPhoto
        public string getPhoto(string accessToken)
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append("/me/photos?");
            stringBuilder.Append("fields=picture&");
            
            stringBuilder.Append($"access_token={accessToken}");
           
            return stringBuilder.ToString();
        }
        public string getPageAccessPoint(string accessToken)
        {
            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append($"/me/accounts?fields=access_token");
            
            stringBuilder.Append($"&access_token={accessToken}");
           
            return stringBuilder.ToString();
        }
        public string postComment(string postId, string message, string access_token)
        {

            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append($"/{postId}");
            stringBuilder.Append("/comments?");
            stringBuilder.Append($"message={message}");
            
            stringBuilder.Append($"&access_token={access_token}");
            System.Diagnostics.Debug.WriteLine(stringBuilder);
            return stringBuilder.ToString();
        }


        public string postPost(string message, string access_token)
        {

            StringBuilder stringBuilder = new StringBuilder(baseEndpoint);
            stringBuilder.Append($"/{version}");
            stringBuilder.Append("/me/feed?");
            stringBuilder.Append($"message={message}");

            stringBuilder.Append($"&access_token={access_token}");
            System.Diagnostics.Debug.WriteLine(stringBuilder);
            return stringBuilder.ToString();
        }
    }
}
