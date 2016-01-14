using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace FitBit
{
    public class Program

    {   
        
        static void Main(string[] args)
        {
            WebServer ws = new WebServer(SendResponse, "http://localhost:8080/test/");

            ws.Run();
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
