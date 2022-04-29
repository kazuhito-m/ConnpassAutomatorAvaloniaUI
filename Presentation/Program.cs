using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Presentation
{
    class Program
    {
        private static Task? backgroundHostTask;
        private static Task<int>? uiTask;

        [STAThread]
        public static async Task<int> Main(string[] args)
        {
            backgroundHostTask = CreateHostBuilder(args).Build().RunAsync();

            uiTask = Task.Run(() =>
            {
                var app = BuildAvaloniaApp();
                return app.StartWithClassicDesktopLifetime(args, ShutdownMode.OnExplicitShutdown);
            });


            await backgroundHostTask;
            var lifetime = Application.Current.ApplicationLifetime as IControlledApplicationLifetime;
            lifetime?.Shutdown(0);
            var result = await uiTask;
            return result;
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                });
    }
}
