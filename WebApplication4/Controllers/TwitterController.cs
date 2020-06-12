using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using WebApplication4.Endpoint;
using WebApplication4.Models;
using WebApplication4.Parser;
using WebApplication4.RestClientTwitter;

namespace WebApplication4.Controllers
{
    public class TwitterController : AbstractController
    {
        private const string consumer_key = "YfzEU2DIsSwGTqHcgjjwf37Vz";
        private const string consumer_secret = "agCDwCl5aMOPAZkvbvbCrNwNp0wLXjfkKVucG7hFmi9INnzSzS";
        private const string access_token = "1953266850-FCg6UnbuKlccYwX98IZyS3uAmlB5ckYYiQ0IzgS";
        private const string token_secret = "bSTLXAeWmEFxmvLpP3Pd6OSVAfOPj4YplJpfgEzjeIGHG";
        private twitterEndpoint twEndpoint;
        public const string OauthSignatureMethod = "HMAC-SHA1";
        public const string OauthVersion = "1.0";
        public string AuthSignature = "";

        protected RestClientTwitter.RestClientTwitter restClient = new RestClientTwitter.RestClientTwitter();

        private string createHeader(string resourceURL, Method method, string msg = "")
        {
            var authNonce = CreateOauthNonce();
            var authTime = CreateOAuthTimestamp();
            var sig = CreateOauthSignature(resourceURL, method, authNonce, authTime, msg);

            StringBuilder b = new StringBuilder();
            b.Append($"OAuth oauth_consumer_key={consumer_key},");
            b.Append($"oauth_nonce={authNonce},");
            b.Append($"oauth_signature_method={OauthSignatureMethod},");
            b.Append($"oauth_timestamp={authTime},");
            b.Append($"oauth_token={access_token},");
            b.Append($"oauth_version={OauthVersion},");
            b.Append($"oauth_signature={sig}");

            var sigBaseString = b.ToString();

            return sigBaseString;

        }

        private string CreateOauthSignature(string resourceUrl, Method method, string oauthNonce, string oauthTimestamp, string msg)
        {
            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                            "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}";

            if (msg != "")
            {
                baseFormat += "&status=" + Uri.EscapeDataString(msg);
            }

            var baseString = string.Format(baseFormat,
                                        consumer_key,
                                        oauthNonce,
                                        OauthSignatureMethod,
                                        oauthTimestamp,
                                        access_token,
                                        OauthVersion
                                        );

            baseString = string.Concat(method + "&", Uri.EscapeDataString(resourceUrl), "&", Uri.EscapeDataString(baseString));



            var compositeKey = string.Concat(Uri.EscapeDataString(consumer_secret),
                                    "&", Uri.EscapeDataString(token_secret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            string autht = Uri.EscapeDataString(oauth_signature);
            return autht;

        }

        private static string CreateOAuthTimestamp()
        {

            var nowUtc = DateTime.UtcNow;
            var timeSpan = nowUtc - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            return timestamp;
        }
        public enum Method
        {
            POST,
            GET
        }

        private string CreateOauthNonce()
        {
            var oauthNonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture)));
            return oauthNonce;
        }
        public TwitterController() : base()
        {
            twEndpoint = new twitterEndpoint();
        }

        public JsonResult getTimeline()
        {
           
            AuthSignature = createHeader(twEndpoint.getTimeline(), Method.GET);
            twEndpoint.setAuth(AuthSignature);
            restClient.endpoint = twEndpoint.getTimeline();
            string response = restClient.makeRequest(RestClientTwitter.HttpVerb.GET, twEndpoint.getHeader());  
            List<TwitterPost> dataList = new List<TwitterPost>();

            

            TwitterFeedModel deserializedfbModel = new TwitterFeedModel();
            string addeddata = "{\"data\": ";
            string addeddata2 = "}";
            response = addeddata + response + addeddata2;

            deserializedfbModel = JsonConvert.DeserializeObject<TwitterFeedModel>(response);

            foreach (TwitterPost t in deserializedfbModel.data)
            {
                dataList.Add(t);
            }

            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }
        private string searchTweetsWithQuery(string query)
        {
            var oauth_token = "1953266850-FCg6UnbuKlccYwX98IZyS3uAmlB5ckYYiQ0IzgS";
            var oauth_token_secret = "bSTLXAeWmEFxmvLpP3Pd6OSVAfOPj4YplJpfgEzjeIGHG";
            var oauth_consumer_key = "YfzEU2DIsSwGTqHcgjjwf37Vz";
            var oauth_consumer_secret = "agCDwCl5aMOPAZkvbvbCrNwNp0wLXjfkKVucG7hFmi9INnzSzS";

            // oauth implementation details
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";

            // unique request details
            var oauth_nonce = Convert.ToBase64String(
                new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow
                - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            // message api details
            var status = "Updating status via REST API if this works";
            //working example: https://dev.twitter.com/discussions/12758
            var resource_url = "https://api.twitter.com/1.1/search/tweets.json";
            var screen_name = "ENGAGEinterreg";
            // create oauth signature

            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                            "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&q={6}";

            var baseString = string.Format(baseFormat,
                                        oauth_consumer_key,
                                        oauth_nonce,
                                        oauth_signature_method,
                                        oauth_timestamp,
                                        oauth_token,
                                        oauth_version,
                                        query
                                        );



            baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                    "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            // create the request header
            var headerFormat = "OAuth oauth_consumer_key=\"{3}\", oauth_nonce=\"{0}\", oauth_signature=\"{5}\", oauth_signature_method=\"{1}\", " +
                               "oauth_timestamp=\"{2}\",  " +
                               "oauth_token=\"{4}\", " +
                               "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(oauth_consumer_key),
                                    Uri.EscapeDataString(oauth_token),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_version)
                            );


      

            ServicePointManager.Expect100Continue = false;

            var postBody = "screen_name=" + Uri.EscapeDataString(screen_name);//
            resource_url += "?q=" + query;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";


            WebResponse response = request.GetResponse();
            string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseData;
        }
        public JsonResult getSearch(string search)
        {
            string response = searchTweetsWithQuery(search);

           
            List<twittersearch> dataList = new List<twittersearch>();



            TwitterSearchModel deserializedfbModel = new TwitterSearchModel();
            

            deserializedfbModel = JsonConvert.DeserializeObject<TwitterSearchModel>(response);

            foreach (twittersearch t in deserializedfbModel.statuses)
            {
                dataList.Add(t);
            }

            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getFriends()
        {

            AuthSignature = createHeader(twEndpoint.getFriends(), Method.GET);
            twEndpoint.setAuth(AuthSignature);
            restClient.endpoint = twEndpoint.getFriends();
            string response = restClient.makeRequest(RestClientTwitter.HttpVerb.GET, twEndpoint.getHeader());
            List<twitterFriend> dataList = new List<twitterFriend>();



            TwitterFriendModel deserializedfbModel = new TwitterFriendModel();


            deserializedfbModel = JsonConvert.DeserializeObject<TwitterFriendModel>(response);

            foreach (twitterFriend t in deserializedfbModel.users)
            {
                dataList.Add(t);
            }

            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getFollowers()
        {

            AuthSignature = createHeader(twEndpoint.getFollower(), Method.GET);
            twEndpoint.setAuth(AuthSignature);
            restClient.endpoint = twEndpoint.getFollower();
            string response = restClient.makeRequest(RestClientTwitter.HttpVerb.GET, twEndpoint.getHeader());
            List<twitterFriend> dataList = new List<twitterFriend>();



            TwitterFriendModel deserializedfbModel = new TwitterFriendModel();


            deserializedfbModel = JsonConvert.DeserializeObject<TwitterFriendModel>(response);

            foreach (twitterFriend t in deserializedfbModel.users)
            {
                dataList.Add(t);
            }

            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }


        public void PostTwitter(string message)
        {
           
            AuthSignature = createHeader(twEndpoint.PostTwitter(message), Method.POST);
            twEndpoint.setAuth(AuthSignature);
            restClient.endpoint = twEndpoint.PostTwitter(message);
            string response = restClient.makeRequest(RestClientTwitter.HttpVerb.POST, twEndpoint.getHeader());   
       
        }


        public JsonResult getfavourites()
        {

            AuthSignature = createHeader(twEndpoint.getFavourites(), Method.GET);
            twEndpoint.setAuth(AuthSignature);
            restClient.endpoint = twEndpoint.getFavourites();
            string response = restClient.makeRequest(RestClientTwitter.HttpVerb.GET, twEndpoint.getHeader());
            List<TwitterPost> dataList = new List<TwitterPost>();



            TwitterFeedModel deserializedfbModel = new TwitterFeedModel();
            string addeddata = "{\"data\": ";
            string addeddata2 = "}";
            response = addeddata + response + addeddata2;

            deserializedfbModel = JsonConvert.DeserializeObject<TwitterFeedModel>(response);

            foreach (TwitterPost t in deserializedfbModel.data)
            {
                dataList.Add(t);
            }

            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }

    }

}