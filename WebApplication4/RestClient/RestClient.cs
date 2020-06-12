using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication4.RestClient
{
    public enum HttpVerb
    {
        GET, POST
    }

    public class RestClient
    {
        public string endpoint { get; set; }

        public RestClient()
        {
            this.endpoint = string.Empty;
        }

        public string makeRequest(HttpVerb httpMethod)
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(endpoint);

            request.Method = httpMethod.ToString();

          

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException($"Error Code: {response.StatusCode}");
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }

            return strResponseValue;


        }

    }
}
