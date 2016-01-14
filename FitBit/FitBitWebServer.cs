using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Web;


namespace FitBit
{
    public class FitBitWebServer
    {
        //Create simple HTTP protocol _listener
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;

        public FitBitWebServer(string[] prefixes, Func<HttpListenerRequest, string> method)
        {
            //URI prefixes are required
            if (prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("URI prefixes are required!");
            }

            if (method == null)
            {
                throw new ArgumentException("Method has to be defined!");
            }

            foreach (string s in prefixes)
            {
                _listener.Prefixes.Add(s);
            }

            _responderMethod = method;
            _listener.Start();


        }

        public FitBitWebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
            : this(prefixes, method)
        { }


        public void Start()
        {
            //SHOULDN'T BE HERE - TESTING
            var clientIdentifier = "11df6ee71f2fa9e5799164c20c8bc099";
            var clientSecret = "57862a3d8bc915d728566216575697b3";
            var tokenEndpoint = "https://api.fitbit.com/oauth2/token";
            var clientID = "229XX2";

            //START THE AUTH PROCESS - provide needed info
            Authenticator auth = new Authenticator(clientID, clientIdentifier, clientSecret, tokenEndpoint);

            //define scope to gain access for
            auth.Authenticate();



            ThreadPool.QueueUserWorkItem((o) =>
            {
                Console.WriteLine("Server stared!");

                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var context = c as HttpListenerContext;

                            try
                            {
                                string responderString = _responderMethod(context.Request);

                                //Get auth code from request URL to server
                                Uri requestUri = context.Request.Url;
                                var code = HttpUtility.ParseQueryString(requestUri.Query).Get("code");

                                byte[] buf = Encoding.UTF8.GetBytes(responderString);

                                context.Response.ContentLength64 = buf.Length;
                                context.Response.OutputStream.Write(buf, 0, buf.Length);

                                //Exchange code for accessToken
                                string accessToken = auth.AccessToken(code);

                                //Send request for FitBit info - define what info you want
                                FitBitRequest req = new FitBitRequest(accessToken);
                                string requestScope = "profile";
                                req.SendRequest(requestScope);
                            }

                            //Suppress any exceptions
                            catch { }

                            finally
                            {
                                context.Response.OutputStream.Close();
                            }

                        }, _listener.GetContext());
                    }
                }
                //Suppress any exceptions
                catch { }

            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }

    }

    public class FitBit
    {
        static void Main(string[] args)
        {
            //Starting web server
            FitBitWebServer ws = new FitBitWebServer(SendResponse, "http://localhost:8080/test/");
            ws.Start();

            //Stoping web server - after key is pressed
            Console.WriteLine("A simple web server. To stop server, press any key!");
            Console.ReadKey();
            ws.Stop();
        }



        public static string SendResponse(HttpListenerRequest request)
        {
            return string.Format("<HTML><BODY><B>FitBit Web Server!<br>{0}</B></BODY></HTML>", DateTime.Now);
        }
    }
}
