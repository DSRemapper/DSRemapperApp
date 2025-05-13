using DSRemapperApp.RazorLayouts;
using DSRemapper.Framework;
using DSRemapper;
using FireLibs.Logging;
using System.Runtime.Versioning;

namespace DSRemapperApp
{
    internal static class Program
    {
        private static readonly DSRLogger logger = DSRLogger.GetLogger("DSRempper.MainThread");

        internal static bool firstBoot=true;
        internal static bool sidePanel=false;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            logger.LogInformation("Loading plugins ...");
            PluginLoader.LoadPluginAssemblies();
            PluginLoader.LoadPlugins();

            logger.LogInformation("Loading app ...");
            ApplicationConfiguration.Initialize();
            DSRForm<MainLayout> main = new();
            main.FormClosing += Main_FormClosing;
            Application.Run(main);

            RemapperCore.Stop();
            logger.LogInformation("Exiting app ...");
        }

        private static void CurrentDomain_UnhandledException(object? sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            logger.LogCritical($"{sender?.GetType().FullName}: {exception.Message}");
            MessageBox.Show($"{sender?.GetType().FullName}: {exception.Message}", "Unhandled Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private static void Main_FormClosing(object? sender, FormClosingEventArgs e)
        {
            RemapperCore.Stop();
        }
    }
}