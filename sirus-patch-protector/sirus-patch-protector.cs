using System;
using System.IO;
using System.Threading;

class Program
{
    static string patchesDir;
    static string dataDir;

    static void Main()
    {
        string scriptDir = AppDomain.CurrentDomain.BaseDirectory;
        patchesDir = Path.Combine(scriptDir, "patches");
        dataDir = Path.Combine(Directory.GetParent(scriptDir).Parent.FullName, "Data");

        if (!Directory.Exists(patchesDir))
            Environment.Exit(1);
        if (!Directory.Exists(dataDir))
            Directory.CreateDirectory(dataDir);
        CopyFiles();

        using (var watcher = new FileSystemWatcher(dataDir))
        {
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.Size;
            watcher.EnableRaisingEvents = true;

            watcher.Deleted += OnChanged;
            watcher.Created += OnChanged;
            watcher.Changed += OnChanged;
            watcher.Renamed += OnChanged;

            new ManualResetEvent(false).WaitOne();
        }
    }

    static void OnChanged(object sender, FileSystemEventArgs e)
    {
        string fileName = Path.GetFileName(e.FullPath);
        string sourceFile = Path.Combine(patchesDir, fileName);
        
        if (File.Exists(sourceFile))
        {
            try
            {
                File.Copy(sourceFile, e.FullPath, true);
            }
            catch { }
        }
    }

    static void CopyFiles()
    {
        foreach (string file in Directory.GetFiles(patchesDir, "*", SearchOption.AllDirectories))
        {
            string fileName = Path.GetFileName(file);
            string dest = Path.Combine(dataDir, fileName);
            try
            {
                File.Copy(file, dest, true);
            }
            catch { }
        }
    }
}