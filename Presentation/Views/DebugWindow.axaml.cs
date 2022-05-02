using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Presentation.Views
{
    public partial class DebugWindow : Window
    {
        public DebugWindow()
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
