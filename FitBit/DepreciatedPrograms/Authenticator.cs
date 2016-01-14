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
namespace FitBit
{
    public class Authenticator
    {
        private string ClientIdentifier;
        private string ClientSecret;
        private string AuthorizationEndpointUrl;
        private string TokenEndpointUrl;
        

        //Authenticator Constructor
        public Authenticator(string ClientIdentifier, string ClientSecret, string AuthorizationEndpointUrl, string TokenEndpointUrl)
        {
            this.ClientIdentifier = ClientIdentifier;
            this.ClientSecret = ClientSecret;
            this.AuthorizationEndpointUrl = AuthorizationEndpointUrl;
            this.TokenEndpointUrl = TokenEndpointUrl;
        }

        //Authentication Method
        public void Authenticate()
        {
            var serverDescription = new AuthorizationServerDescription();
            serverDescription.AuthorizationEndpoint = new Uri(AuthorizationEndpointUrl);
            serverDescription.TokenEndpoint = new Uri(TokenEndpointUrl);
            serverDescription.ProtocolVersion = ProtocolVersion.V20;

            var client = new WebServerClient(serverDescription);
            client.ClientIdentifier = ClientIdentifier;
            client.ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(ClientSecret);

            var token = client.GetClientAccessToken();

            //var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/test/");
            ////System.Diagnostics.Process.Start("http://localhost:8080/test/");

            //request.Method = "GET";
            //client.AuthorizeRequest(request, token);
            //var response = request.GetResponse();

            //var postreqreader = new StreamReader(response.GetResponseStream());
            //var json = postreqreader.ReadToEnd();
        }


        public void Authenticate2()
        {
            var brow=System.Diagnostics.Process.Start("https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=229XX2&scope=profile%20nutrition");

            //var callback_url = "http://localhost:8080/test/";
            //var url = "https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=229XX2&scope=profile%20nutrition&redirect_uri=http://localhost:8080/test/";
            //var request = (HttpWebRequest)WebRequest.Create(url);
            //request.UserAgent = "Chrome/47.0.2526.106";
            //request.Method = "GET";

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //var uri = response.ResponseUri;
            //var request2 = (HttpWebRequest)WebRequest.Create(uri);
            //request.UserAgent = "Chrome/47.0.2526.106";
            //request.Method = "GET";

            //HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
            //var uri2 = response2.ResponseUri;
            //WebClient web = new WebClient();

            //var postreqreader = new StreamReader(response.GetResponseStream());
            //var json = postreqreader.ReadToEnd();

            //Uri ourUri = new Uri(url);


            //if (ourUri.Equals(response.ResponseUri))
            //    Console.WriteLine("\nRequest Url : {0} was not redirected", url);
            //else
            //    Console.WriteLine("\nRequest Url : \n{0}\n\nwas redirected to \n{1}\n", url, response.ResponseUri);
            //// Release resources of response object.
            //response.Close();

            //var url = "https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=229XX2&scope=profile%20nutrition";

            //WebBrowser web = new WebBrowser();


            ////web.Url = new Uri(url);

            //web.Navigate(url);


        }

    }
}
