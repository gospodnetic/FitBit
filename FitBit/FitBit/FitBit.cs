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
            return string.Format("<HTML><BODY><B>My web page.<br>{0}</B></BODY></HTML>", DateTime.Now);
        }
    }
}
