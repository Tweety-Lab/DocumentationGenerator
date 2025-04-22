using DocumentationGenerator.Builder;
using Host;
using Host.HotReloading;

namespace DocumentationGenerator.MarkdownServer;

/// <summary>
///     Creates a local server for markdown documentation.
/// </summary>
public class MDServer
{
    // Local server
    private LocalServer server;

    public MDServer(string directory, int port)
    {
        // Resolve relative to the current working directory of the shell that launched this process
        if (!Path.IsPathRooted(directory))
            directory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), directory));

        MDDirectory = directory;
        Port = port;

        // Exit handler
        AppDomain.CurrentDomain.ProcessExit += OnExit;
    }

    // Directory to the root of the markdown files, this is where the navbar json lives.
    public string MDDirectory { get; set; }

    // Port to open server on
    public int Port { get; set; }


    public void OpenServer()
    {
        var jsonPath = Path.Combine(MDDirectory, "navbar.json");
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine($"Error: Could not find JSON file at {jsonPath}");
            return;
        }

        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Build");

        // Build the markdown to a site
        var builder = new DocumentationBuilder(jsonPath, outputPath);
        Program.Builder = builder;
        builder.BuildAllPages();

        // Start a local server for the built site
        server = new LocalServer(outputPath, Port);

        // Build site when files change
        var hotReload = new HotReload(Directory.GetCurrentDirectory());
        hotReload.RegisterCallback(filePath =>
        {
            builder.BuildAllPages(); // Build the site
            server.ForceRefresh(); // Refresh the server
        });

        // Open server and block until closed
        server.OpenServer(true);

        // When we reach this, server has closed.
        Console.WriteLine("Shutting Server Down.");
    }

    private void OnExit(object sender, EventArgs e)
    {
        // Path to the Build folder
        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Build");

        // Delete the Build folder if it exists
        if (Directory.Exists(outputPath))
            try
            {
                Console.WriteLine("Cleaning Up Build...");
                Directory.Delete(outputPath, true); // Delete folder recursively
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Cleaning Up Build: {ex.Message}");
            }
    }
}