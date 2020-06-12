using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace  WebApplication4.RestClientTwitter
{
    public enum HttpVerb
    {
        GET, POST
    }

    public class RestClientTwitter
    {
        public string endpoint { get; set; }

        public RestClientTwitter()
        {
            this.endpoint = string.Empty;
        }

        public string makeRequest(HttpVerb httpMethod, Dictionary<string, string> headers, string msg = null)
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(endpoint);

            request.Method = httpMethod.ToString();

            //for each header in our list (dictionary) headers
            foreach (KeyValuePair<string, string> header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            if (msg != null)
            {
                request.ContentType = "application/x-www-form-urlencoded";
                using (Stream objStream = request.GetRequestStream())
                {
                    byte[] content = ASCIIEncoding.ASCII.GetBytes("status=" + Uri.EscapeDataString(msg));
                    objStream.Write(content, 0, content.Length);
                }
            }

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