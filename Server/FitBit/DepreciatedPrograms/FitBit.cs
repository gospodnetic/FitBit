using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;


namespace FitBit
{
    public class FitBit

    {

        static void Main(string[] args)
        {
            WebServer ws = new WebServer(SendResponse, "http://localhost:8080/test/");
            ws.Run();

            var clientIdentifier = "11df6ee71f2fa9e5799164c20c8bc099";
            var clientSecret = "57862a3d8bc915d728566216575697b3";
            var authorizationEndpointUrl = "https://www.fitbit.com/oauth2/authorize?client_id=229XX2";
            var tokenEndpoint = "https://api.fitbit.com/oauth2/token";

            Authenticator auth=new Authenticator(clientIdentifier, clientSecret, authorizationEndpointUrl, tokenEndpoint);
            
            auth.Authenticate2();

            Console.WriteLine("A simple web server. Press a key to quit!");
            Console.ReadKey();
            ws.Stop();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            return string.Format("<HTML><BODY><B>My web page.<br>{0}</B></BODY></HTML>", DateTime.Now);
        }
    }
}
