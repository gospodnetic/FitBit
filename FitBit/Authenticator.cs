using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using DotNetOpenAuth;
using DotNetOpenAuth.OAuth2;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Specialized;
using Newtonsoft.Json;


namespace FitBit
{
    //  predefined data structure for obtaining AccessToken
    public class AccessTokenJSON
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
        public string user_id { get; set; }
    }

    public class Authenticator
    {
        private string _ClientIdentifier;
        private string _ClientSecret;
        private string _TokenEndpointUrl;
        private string _ClientID;


        //  Authenticator Constructor
        public Authenticator(string clientID, string ClientIdentifier, string ClientSecret, string TokenEndpointUrl)
        {
            this._ClientIdentifier = ClientIdentifier;
            this._ClientID = clientID;
            this._ClientSecret = ClientSecret;
            this._TokenEndpointUrl = TokenEndpointUrl;
        }

        //  Function to start Authentication process - opens browser for a user with a agree/deny dialog
        //  There are two scenarios: in first user denies access to requested scope and auth process is terminated
        //  In the other scenario user grants access and gets authentication code in callback_uri and has to exchange it for
        //  access and refresh token
        //  With access and refresh token, user can fetch data in requested scope

        public void Authenticate()
        {
            var brow = System.Diagnostics.Process.Start("https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=229XX2&redirect_uri=http%3A%2F%2Flocalhost%3A8080%2Ftest%2F&scope=profile%20activity");
        }

        //  Generating request to exchange authentication code for access token
        public string AccessToken(string code)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(_ClientID + ":" + _ClientSecret);
            var token = Convert.ToBase64String(plainTextBytes);

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["grant_type"] = "authorization_code";
                data["client_id"] = _ClientID;
                data["redirect_uri"] = "http://localhost:8080/test/";
                data["code"] = code;

                wb.Headers["Authorization"] = "Basic " + token;

                //  POST request
                var response = wb.UploadValues(_TokenEndpointUrl, "POST", data);
                var responseString = Encoding.ASCII.GetString(response);

                //  Deserialize JSON object to a predefined data structure for AccessToken
                AccessTokenJSON responseStingDeserialized = JsonConvert.DeserializeObject<AccessTokenJSON>(responseString);
                var accessToken = responseStingDeserialized.access_token;

                return accessToken;
            }
        }
    }
}