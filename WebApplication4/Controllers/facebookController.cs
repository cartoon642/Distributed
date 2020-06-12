using WebApplication4.Endpoint;
using WebApplication4.Models;
using WebApplication4;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using WebApplication4.RestClient;

namespace WebApplication4.Controllers
{
    
    public class PagePostsController : AbstractController
    {

        private facebookEndpoint facebookEndpoint;

        public PagePostsController() : base()
        { 
            facebookEndpoint = new facebookEndpoint();
        }

        public JsonResult getPosts(string access_token)
        {
            
            
            List<Data> dataList = new List<Data>();
            restClient.endpoint = facebookEndpoint.getPosts(access_token);
            string response = restClient.makeRequest(RestClient.HttpVerb.GET);
            PagePostsModel deserializedfbModel = new PagePostsModel();
            deserializedfbModel = JsonConvert.DeserializeObject<PagePostsModel>(response);
            foreach (Data d in deserializedfbModel.data)
            {
                dataList.Add(d);
            }
            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getLikes(string access_token)
        {
            System.Diagnostics.Debug.WriteLine(access_token);
            List<Data1> dataList = new List<Data1>();
            restClient.endpoint = facebookEndpoint.getLikes(access_token);

            string response = restClient.makeRequest(RestClient.HttpVerb.GET);
            PageLikeModels deserializedfbModel = new PageLikeModels();
            
            deserializedfbModel = JsonConvert.DeserializeObject<PageLikeModels>(response);
            foreach (Data1 d in deserializedfbModel.data)
            {
                dataList.Add(d);
            }
            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getFeed(string a)
        {
            List<Data> dataList = new List<Data>();
            restClient.endpoint = facebookEndpoint.getFeed(a);
            string response = restClient.makeRequest(RestClient.HttpVerb.GET);
            PagePostsModel deserializedfbModel = new PagePostsModel();
            deserializedfbModel = JsonConvert.DeserializeObject<PagePostsModel>(response);
            foreach (Data d in deserializedfbModel.data)
            {
                dataList.Add(d);
            }
            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getPhotos(string access_token)
        {
            List<Data2> dataList = new List<Data2>();
            restClient.endpoint = facebookEndpoint.getPhoto(access_token);
            string response = restClient.makeRequest(RestClient.HttpVerb.GET);
            response = response.Replace(@"\/", "/");
            PagePhotoModel deserializedfbModel = new PagePhotoModel();
            deserializedfbModel = JsonConvert.DeserializeObject<PagePhotoModel>(response);
            foreach (Data2 d in deserializedfbModel.data)
            {
                dataList.Add(d);
            }
            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getPageAccessPoint(string accessToken)
        {
            System.Diagnostics.Debug.WriteLine(accessToken);
            List<DataPageToken> dataList = new List<DataPageToken>();
            
            restClient.endpoint = facebookEndpoint.getPageAccessPoint(accessToken);
            
            string response = restClient.makeRequest(RestClient.HttpVerb.GET);
            
            PageTokenModel deserializedfbModel = new PageTokenModel();
            deserializedfbModel = JsonConvert.DeserializeObject<PageTokenModel>(response);
            foreach (DataPageToken d in deserializedfbModel.data)
            {
                dataList.Add(d);
            }
            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult postCommentOnPage(string postId, string message, string access_token) 
        {
          
            List<Data2> dataList = new List<Data2>();
            restClient.endpoint = facebookEndpoint.postComment(postId, message, access_token);
            string response = restClient.makeRequest(RestClient.HttpVerb.POST);
            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult postPostOnPage(string message, string access_token) 
        {
            //List<DataPageToken> dataList = new List<DataPageToken>();

            List<Data2> dataList = new List<Data2>();
            restClient.endpoint = facebookEndpoint.postPost(message, access_token);
            string response = restClient.makeRequest(RestClient.HttpVerb.POST);
            return Json(new { success = true, dataList = dataList }, JsonRequestBehavior.AllowGet);
        }
    }
}
