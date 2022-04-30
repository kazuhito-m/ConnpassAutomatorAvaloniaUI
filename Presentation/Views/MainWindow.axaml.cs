using Avalonia.Controls;
using Avalonia.Interactivity;
using Presentation.Alert;
using Presentation.ViewModels;
using System;

namespace Presentation.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Closed += OnClosed;
        }
        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await ThisSystemMessageBox.Show("�^�C�g��", "���b�Z�[�W�{�b�N�X�o�����I", this);
        }

        private void OnClosed(object? sender, EventArgs args)
            => ViewModel?.Save();

        private MainWindowViewModel? ViewModel
            => DataContext as MainWindowViewModel;
    }
}
