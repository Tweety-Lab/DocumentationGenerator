using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.Utilities
{
    public static class FileUtil
    {
        public static string SafeReadAllText(string path, int retries = 5, int delay = 200)
        {
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    return File.ReadAllText(path);
                }
                catch (IOException)
                {
                    if (i == retries - 1) throw;
                    Thread.Sleep(delay);
                }
            }
            return null;
        }

    }
}
