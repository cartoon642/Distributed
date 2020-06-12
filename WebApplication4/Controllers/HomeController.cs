using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;

namespace WebApplication4.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
       
        public ActionResult Register()
        {
            return View();
        }
      
        public ActionResult Login()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult TwitterAuth()
        //{
        //    string Key = "WRtoRdP34J4WVpn5inbKjKJId";
        //    string Secret = "jDKpG7W7iCV0E9eKkTFKnK1W1JQwVyTIuvhvA5NXvBSzLfbR6b";

        //    TwitterService service = new TwitterService(Key, Secret);

        //    //Obtaining a request token
        //    OAuthRequestToken requestToken = service.GetRequestToken("https://localhost:44338/Home/TwitterCallback");

        //    Uri uri = service.GetAuthenticationUrl(requestToken);

        //    //Redirecting the user to Twitter Page
        //    return Redirect(uri.ToString());
        //}
        //public ActionResult TwitterCallback(string oauth_token, string oauth_verifier)
        //{
        //    var requestToken = new OAuthRequestToken { Token = oauth_token };

        //    string Key = "WRtoRdP34J4WVpn5inbKjKJId";
        //    string Secret = "jDKpG7W7iCV0E9eKkTFKnK1W1JQwVyTIuvhvA5NXvBSzLfbR6b";

        //    try
        //    {
        //        TwitterService service = new TwitterService(Key, Secret);

        //        //Get Access Tokens
        //        OAuthAccessToken accessToken =
        //                   service.GetAccessToken(requestToken, oauth_verifier);

        //        service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);

        //        VerifyCredentialsOptions option = new VerifyCredentialsOptions();

        //        //According to Access Tokens get user profile details
        //        TwitterUser user = service.VerifyCredentials(option);

        //        return View("index");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
    }
