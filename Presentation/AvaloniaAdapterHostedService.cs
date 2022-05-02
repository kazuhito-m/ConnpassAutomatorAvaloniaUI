using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.Hosting;
using Presentation.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    public class AvaloniaAdapterHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime host;
        private readonly MainWindowViewModel mainWindowViewModel;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var app = BuildAvaloniaApp();
            app.StartWithClassicDesktopLifetime(new string[] { }, ShutdownMode.OnMainWindowClose);

            host.StopApplication();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        internal AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure(() => new App(mainWindowViewModel))
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
        }

        public AvaloniaAdapterHostedService(IHostApplicationLifetime host, MainWindowViewModel mainWindowViewModel)
        {
            this.host = host;
            this.mainWindowViewModel = mainWindowViewModel;
        }
    }
}
