using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.IO;

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
            string urlworkout = "https://api.fitbit.com/1/user/229XX2/" + requestScope +".json";


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlworkout);
            request.Method = "GET";
            request.Headers["Authorization"] = "Bearer " + _accesToken;
            request.Accept = "application/json";


            //Response results - contains requested data
            WebResponse response;
            string results = "";
            response = request.GetResponse();
            StreamReader HttpStreamReader = new StreamReader(response.GetResponseStream());
            results = HttpStreamReader.ReadToEnd();
            Console.Write(results);

            response.Close();
            HttpStreamReader.Close();
        }
    }
}
