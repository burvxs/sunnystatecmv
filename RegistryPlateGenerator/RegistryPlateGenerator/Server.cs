using System;
using System.IO;
using System.Text.Json;
using System.Net;

using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Mime;

namespace RegistryPlateGenerator
{
    class Server
    {
        private class JSONResponse
        { 
            public string Response { get; set; }
        }

        // Build admin layer
        // send through JWT 
        private HttpListener listener = new();
        const int PORT = 8080;
        readonly string localURL = $"http://localhost:{PORT}/endpoint/"; 

        public void Configure()
        {
            listener.Prefixes.Add(localURL);
            listener.Start();

            Console.WriteLine("Started HTTP server...");
        }

        private void Respond(string JSONdata)
        {
            using (Stream responseStream = listener.GetContext().Response.OutputStream)
            {
                using StreamWriter responseWriter = new(responseStream);             
                responseWriter.Write(JSONdata);          
            }

            Console.WriteLine("Responded to: " + listener.GetContext().Request.UserHostAddress);
        }

        public void WriteImage(HttpListenerContext ctx, string path)
        {
            HttpListenerResponse response = ctx.Response;
            using (FileStream fs = File.OpenRead(path))
            {
                string fileName = Path.GetFileName(path);

                response.ContentLength64 = fs.Length;
                response.SendChunked = false;
                response.ContentType = MediaTypeNames.Application.Octet;
                response.AddHeader("Content-disposition", "attachment; filename=" + fileName);

                byte[] buffer = new byte[12 * 1024];
                int read;

                using (BinaryWriter bWriter = new(response.OutputStream))
                {
                    while((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bWriter.Write(buffer, 0, read);
                    }

                    bWriter.Close();
                }

                response.StatusCode = (int)HttpStatusCode.OK;
                response.StatusDescription = "OK";
                response.OutputStream.Close();
            }
        }

        private string SerializeResponse(JSONResponse serializee, string data)
        {
            serializee = new JSONResponse()
            {
                Response = data
            };

            return JsonSerializer.Serialize(serializee);
        }

        public void Listen()
        {
            try
            {
                while (listener.IsListening)
                {              
                    WriteImage(listener.GetContext(), "poo.png");
                    Respond(SerializeResponse(new JSONResponse(), "PNG Written to server"));
                }
            }
            catch (HttpListenerException httpException)
            {
                listener.Abort();
                Console.WriteLine(httpException.ToString());
            }
        }
    }
}
