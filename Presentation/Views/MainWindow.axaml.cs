using Avalonia.Controls;
using Avalonia.Interactivity;
using Presentation.Alert;

namespace Presentation.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await ThisSystemMessageBox.Show("タイトル", "メッセージボックス出せるよ！", this);
        }
    }
}
