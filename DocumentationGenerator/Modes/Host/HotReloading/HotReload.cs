using System;
using System.IO;
using System.Collections.Generic;

namespace DocumentationGenerator.Modes.Host.HotReloading
{
    public class HotReload : IDisposable
    {
        private readonly FileSystemWatcher watcher;
        private readonly List<Action<string>> onFileChangedCallbacks = new();

        public HotReload(string directoryPath, string filter = "*.*", bool includeSubdirectories = true)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");

            watcher = new FileSystemWatcher(directoryPath, filter)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                IncludeSubdirectories = includeSubdirectories,
                EnableRaisingEvents = true
            };

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;
        }

        public void RegisterCallback(Action<string> callback)
        {
            if (callback != null)
                onFileChangedCallbacks.Add(callback);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            foreach (var callback in onFileChangedCallbacks)
            {
                callback.Invoke(e.FullPath);
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            foreach (var callback in onFileChangedCallbacks)
            {
                callback.Invoke(e.FullPath);
            }
        }

        public void Dispose()
        {
            watcher.Dispose();
        }
    }
}
