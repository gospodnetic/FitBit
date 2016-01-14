using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FitBit
{
    class FitBitRequest
    {
        private string _accesToken;

        public FitBitRequest(string accesToken)
        {
            this._accesToken = accesToken;
        }

        public void SendRequest(string requestScope)
        {
            string urlworkout = "https://api.fitbit.com/1/user/36XJP9/" + requestScope +".json";


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlworkout);
            request.Method = "GET";
            request.Headers["Authorization"] = "Bearer " + _accesToken;
            request.Accept = "application/json";


            //Response results - contains requested data
            WebResponse response;
            var results = "";
            response = request.GetResponse();
            StreamReader HttpStreamReader = new StreamReader(response.GetResponseStream());
            results = HttpStreamReader.ReadToEnd();

            JObject resultsJSON = JObject.Parse(results);

            Console.Write("Age" +   resultsJSON["user"]["age"]);

            response.Close();
            HttpStreamReader.Close();
        }
    }
}
