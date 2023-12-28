using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using DSRemapperApp.RazorLayouts;
using DSRemapper.Core;

namespace DSRemapperApp
{
    public partial class DSRMain : Form
    {
        public DSRMain(bool def = true)
        {
            InitializeComponent();

            if (def)
            {
                var services = new ServiceCollection();
                services.AddWindowsFormsBlazorWebView();
                blazorWebView1.HostPage = "wwwroot/index.html";//Path.Combine(DSRPaths.ProgramPath, "wwwroot\\index.html");
                blazorWebView1.Services = services.BuildServiceProvider();
                blazorWebView1.RootComponents.Add<MainLayout>("#app");
                Text = "DSRemapper App";
            }
        }
    }
    public class DSRForm<T> : DSRMain where T : IComponent
    {
        /// <summary>
        /// DSForm class contructor
        /// </summary>
        /// <param name="parameters">Razor parameters for the Razor Layout</param>
        public DSRForm(IDictionary<string, object?>? parameters = null,int width = 800, int height= 450, string title="DSRemapper App") : base(false)
        {
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            blazorWebView1.HostPage = "wwwroot/index.html";//Path.Combine(DSRPaths.ProgramPath, "wwwroot\\index.html");
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<T>("#app", parameters);
            ClientSize = new Size(width, height);
            Text = title;
        }
    }
}
