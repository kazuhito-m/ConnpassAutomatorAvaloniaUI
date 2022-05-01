using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Presentation.Views
{
    public partial class CredentialEditWindow : Window
    {
        public CredentialEditWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
            => AvaloniaXamlLoader.Load(this);

        private void OnClickOk(object sender, RoutedEventArgs e)
            => Close(true);

        private void OnClickCancel(object sender, RoutedEventArgs e)
            => Close(false);
    }
}
