using Avalonia.Controls;
using Avalonia.Interactivity;
using Presentation.Alert;
using Presentation.Models.Profile;
using System.Collections.Generic;

namespace Presentation.Views
{
    public partial class MainWindow : Window
    {
        private readonly ProfileRepository profileRepository;

        public MainWindow()
        {
            InitializeComponent();

            profileRepository = new ProfileRepository();
        }
        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await ThisSystemMessageBox.Show("タイトル", "メッセージボックス出せるよ！", this);
        }
    }
}
