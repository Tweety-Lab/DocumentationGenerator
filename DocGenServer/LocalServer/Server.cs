using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DocGenServer.LocalServer
{
    /// <summary>
    /// Manages the lifecycle of a local web-server.
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
            if (!System.IO.Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"The directory '{directory}' does not exist.");
            }

            Directory = directory;
            Port = port;
        }

        // Start a local server
        public void OpenServer(bool blocking = false)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{Port}/");
            _listener.Start();
            Console.WriteLine($"Hosting DocGen on http://localhost:{Port}/");

            if (blocking)
            {
                while (_listener.IsListening)
                {
                    var context = _listener.GetContext();
                    ProcessRequest(context);
                }
            }
            else
            {
                ThreadPool.QueueUserWorkItem(o =>
                {
                    while (_listener.IsListening)
                    {
                        var context = _listener.GetContext();
                        ProcessRequest(context);
                    }
                });
            }
        }

        // Process a HTTP request
        private void ProcessRequest(HttpListenerContext context)
        {
            try
            {
                // Get the requested file path
                var requestedUrl = context.Request.Url.LocalPath.Substring(1); // Remove leading "/"

                // Set home page to index.html
                if (string.IsNullOrEmpty(requestedUrl))
                {
                    requestedUrl = "index.html";
                }

                var filePath = Path.Combine(Directory, requestedUrl);

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    var fileExtension = Path.GetExtension(filePath).ToLower();
                    var mimeType = "text/plain"; // Default MIME type

                    // Set MIME type based on file extension
                    // TODO: Expand this
                    if (fileExtension == ".html") mimeType = "text/html";
                    else if (fileExtension == ".css") mimeType = "text/css";
                    else if (fileExtension == ".js") mimeType = "application/javascript";
                    else if (fileExtension == ".jpg" || fileExtension == ".jpeg") mimeType = "image/jpeg";
                    else if (fileExtension == ".png") mimeType = "image/png";

                    // Set the response type
                    context.Response.ContentType = mimeType;

                    // Send the file content
                    byte[] fileContent = File.ReadAllBytes(filePath);
                    context.Response.ContentLength64 = fileContent.Length;
                    context.Response.OutputStream.Write(fileContent, 0, fileContent.Length);
                }
                else
                {
                    // Send 404 if file is not found
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                    byte[] notFound = Encoding.UTF8.GetBytes("404 Not Found");
                    context.Response.ContentLength64 = notFound.Length;
                    context.Response.OutputStream.Write(notFound, 0, notFound.Length);
                }
            } catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error processing request: {ex.Message}");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                byte[] errorMsg = Encoding.UTF8.GetBytes("500 Internal Server Error");

                context.Response.ContentLength64 = errorMsg.Length;
                context.Response.OutputStream.Write(errorMsg, 0, errorMsg.Length);
            } finally
            {
                // Close the response
                context.Response.Close();
            }
        }

        // Stop the server
        public void ShutdownServer()
        {
            if (_listener != null && _listener.IsListening)
            {
                _listener.Stop();
                Console.WriteLine("Server stopped.");
            }
        }
    }
}
