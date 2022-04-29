using Avalonia.Controls;
using Avalonia.Interactivity;
using Presentation.Alert;
using Presentation.Models.Profile;
using Presentation.ViewModels;
using System;

namespace Presentation.Views
{
    public partial class MainWindow : Window
    {
        private readonly ProfileRepository profileRepository;

        public MainWindow()
        {
            InitializeComponent();

            profileRepository = new ProfileRepository();

            Closed += OnClosed;
        }
        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await ThisSystemMessageBox.Show("タイトル", "メッセージボックス出せるよ！", this);
        }

        private void OnClosed(object? sender, EventArgs args)
        {
            ViewModel?.Save();
            Environment.Exit(0);    // FIXME AvaloniaUIの終了でGenericHostの終了を呼べないので苦肉の策。こんな力技ではなくライフサイクルで解決する手段で修正する。
        }

        private MainWindowViewModel? ViewModel
            => DataContext as MainWindowViewModel;
    }
}
