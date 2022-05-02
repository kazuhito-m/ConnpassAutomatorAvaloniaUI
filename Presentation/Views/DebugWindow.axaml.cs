using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Linq;

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

        private void OnClickGo(object sender, RoutedEventArgs e)
        {
            var input = this.FindControl<TextBox>("debugText");
            var names = System.Drawing.FontFamily.Families
                .Select(i => i.Name);
            input.Text = string.Join("\n", names);
        }
    }
}
