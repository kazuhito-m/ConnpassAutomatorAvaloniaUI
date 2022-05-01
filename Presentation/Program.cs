using Avalonia;
using ConnpassAutomator.Application.Service;
using ConnpassAutomator.Domain.Model.Profile;
using ConnpassAutomator.Infrastructure.Datasource.Profile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ViewModels;
using System;

namespace Presentation
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                 .Build()
                 .Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IProfileRepository, ProfileDatasource>();
                    services.AddSingleton<ProfileService>();
                    services.AddSingleton<ConnpassEventService>();

                    services.AddSingleton<MainWindowViewModel>();
                    services.AddHostedService<AvaloniaAdapterHostedService>();
                });

        /// <summary>
        /// AvaloniaUIのデザイナのためのメソッド。
        /// <p/>
        /// ウィザードで自動生成され、デザイナが「Programクラスにこのメソッドがあること」を期待している。
        /// が、本プロジェクトでは、
        /// </summary>
        /// <returns></returns>
        public static AppBuilder BuildAvaloniaApp()
            => new AvaloniaAdapterHostedService(null, new MainWindowViewModel())
                .BuildAvaloniaApp();
    }
}
