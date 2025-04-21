using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DocGenServer.LocalServer
{
    /// <summary>
    /// Manages the lifecycle of a local web-server
    /// </summary>
    public class Server
    {
        // The Directory to read files from
        public string Directory { get; set; }

        // The port to open on
        public int Port { get; set; }

        // HttpListener instance
        private HttpListener _listener;

        public Server(string directory, int port = 9999)
        {
            Directory = directory;
            Port = port;
        }

        // Start a local server
        public void OpenServer()
        {
            // Initialize HTTPListener
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{Port}/");

            // Start the server
            _listener.Start();
            Console.WriteLine($"Hosting DocGen on http://localhost:{Port}/");

            // Handle requests
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (_listener.IsListening)
                {
                    try
                    {
                        // Wait for a request
                        var context = _listener.GetContext();
                        ProcessRequest(context);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            });
        }

        // Process a HTTP request
        private void ProcessRequest(HttpListenerContext context)
        {

        }
    }
}
