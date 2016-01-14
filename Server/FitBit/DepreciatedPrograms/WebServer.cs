using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace FitBit
{
    public class WebServer
    {
        //Create simple HTTP protocol _listener -> HttpListener
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;


        public WebServer(string[] prefixes, Func<HttpListenerRequest, string> method)
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


        //public WebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
        //{
        //    this._responderMethod = method;

        //    foreach (string s in prefixes)
        //    {
        //        _listener.Prefixes.Add(s);
        //    }
        //}

        public WebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
            : this(prefixes, method) { }

        public void Run()
        {
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
                                byte[] buf = Encoding.UTF8.GetBytes(responderString);

                                context.Response.ContentLength64 = buf.Length;
                                context.Response.OutputStream.Write(buf, 0, buf.Length);
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
}
