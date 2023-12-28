using DSRemapperApp.RazorLayouts;
using DSRemapper.Framework;
using DSRemapper;
using FireLibs.Logging;

namespace DSRemapperApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DSRLogger.LogInformation("Loading plugins ...");
            PluginsLoader.LoadPluginAssemblies();
            PluginsLoader.LoadPlugins();

            DSRLogger.LogInformation("Loading app ...");
            ApplicationConfiguration.Initialize();
            DSRForm<MainLayout> main = new();
            main.FormClosing += Main_FormClosing;
            Application.Run(main);

            RemapperCore.Stop();
            DSRLogger.LogInformation("Exiting app ...");
        }
        private static void Main_FormClosing(object? sender, FormClosingEventArgs e)
        {
            RemapperCore.Stop();
        }
    }
}