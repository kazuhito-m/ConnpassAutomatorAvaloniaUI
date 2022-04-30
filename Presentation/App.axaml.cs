using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Presentation.ViewModels;
using Presentation.Views;
using System;

namespace Presentation
{
    public partial class App : Application
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public override void Initialize()
            => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainWindowViewModel,
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        public App() : this(null)
            => throw new InvalidOperationException();

        public App(MainWindowViewModel mainWindowViewModel)
            => this.mainWindowViewModel = mainWindowViewModel;
    }
}
