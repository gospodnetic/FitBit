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

    public class JSONUserID
    {
        public string user_id { get; set; }
    }


    class FitBitRequest
    {
        private string _accesToken;

        public FitBitRequest(string accesToken)
        {
            this._accesToken = accesToken;
        }

        // Functions for sending GET requests for the scope we have been granted to access to

        public void SendRequest()
        {

            var userID = "36XJP9";
            string jsonUserID = @"{userID:'36XJP9'}";

            string urlworkout = "https://api.fitbit.com/1/user/" + userID + "/activities/date/2016-01-01.json";


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

            Console.Write(resultsJSON);

            response.Close();
            HttpStreamReader.Close();
        }
    }
}
