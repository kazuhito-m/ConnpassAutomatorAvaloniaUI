using Avalonia;
using Avalonia.Controls;
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
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
