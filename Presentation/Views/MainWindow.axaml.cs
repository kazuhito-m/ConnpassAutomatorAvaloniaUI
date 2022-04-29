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
            await ThisSystemMessageBox.Show("�^�C�g��", "���b�Z�[�W�{�b�N�X�o�����I", this);
        }

        private void OnClosed(object? sender, EventArgs args)
        {
            ViewModel?.Save();
            Environment.Exit(0);    // FIXME AvaloniaUI�̏I����GenericHost�̏I�����ĂׂȂ��̂ŋ���̍�B����ȗ͋Z�ł͂Ȃ����C�t�T�C�N���ŉ��������i�ŏC������B
        }

        private MainWindowViewModel? ViewModel
            => DataContext as MainWindowViewModel;
    }
}
