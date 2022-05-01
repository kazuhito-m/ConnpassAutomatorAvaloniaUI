using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using ConnpassAutomator.Domain.Model.Connpass.Event;
using Presentation.Alert;
using Presentation.Extension.Avalonia;
using Presentation.ViewModels;
using System;
using System.Threading.Tasks;

namespace Presentation.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += OnClosed;
        }

        private async void OnClickCreateEvent(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.IsEnabled = false;

            await CreateEvent();

            button.IsEnabled = true;
        }

        private async Task CreateEvent()
        {
            if (!await Validation()) return;

            var result = ViewModel().CreateEvent();

            await ShowCreateEventResultMessage(result);
        }

        private async Task<bool> Validation()
        {
            if (await IsBlackInput("copyBaseEventTitle", "�R�s�[���C�x���g�^�C�g��")) return false;
            if (await IsBlackInput("title", "�^�C�g��")) return false;
            if (await IsBlackInput("subTitle", "�T�u�^�C�g��")) return false;
            if (await IsBlackInput("startDate", "�J�n���t")) return false;
            if (await IsBlackInput("startTime", "�J�n����")) return false;
            if (await IsBlackInput("endDate", "�I�����t")) return false;
            if (await IsBlackInput("endTime", "�J�n����")) return false;
            if (await IsBlackInput("eventDescription", "�C�x���g�̐���")) return false;

            return await ValidationCredential();
        }

        private async Task<bool> IsBlackInput(string fieldName, string fieldCaption)
        {
            var input = this.FindControl<TemplatedControl>(fieldName);
            if (!input.IsBlank()) return false;

            await ShowWarnMessage($"{fieldCaption} ����͂��Ă��������B");
            input!.Focus();

            return true;
        }

        private async Task<bool> ValidationCredential()
        {
            var vm = ViewModel();
            if (vm.UserName.Trim().Length > 0
                && vm.Password.Trim().Length > 0) return true;

            await ShowWarnMessage("���O�C����񂪖��ݒ�ł��B");

            return await ShowEditCredentialWindow();
        }


        private async Task ShowCreateEventResultMessage(CreateEventResultState result)
        {
            switch (result)
            {
                case CreateEventResultState.����:
                    await ShowSuccessMessage("Connpass�C�x���g�̍쐬���������܂����B");
                    break;
                case CreateEventResultState.���O�C�����s:
                    await ShowWarnMessage("Connpass�ւ̃��O�C���Ɏ��s���܂����B\n���O�C�������m�F���Ă��������B");
                    break;
                default:
                    await ShowWarnMessage("Connpass�C�x���g�̍쐬�Ɏ��s���܂����B");
                    break;
            }
        }

        private async void OnClickEditCredential(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.IsEnabled = false;

            await ShowEditCredentialWindow();

            button.IsEnabled = true;
        }

        private async Task<bool> ShowEditCredentialWindow()
        {
            var vm = CredentialEditWindowViewModel.Of(ViewModel());
            var window = new CredentialEditWindow() { DataContext = vm };

            var commited = await window.ShowDialog<bool>(this);

            if (!commited) return false;

            var myVm = ViewModel();
            vm.ReflectTo(myVm);
            myVm.Save();

            return true;
        }

        private async Task ShowWarnMessage(string message)
            => await ThisSystemMessageBox.Show(Title, message, this, icon: MessageBox.Avalonia.Enums.Icon.Warning);

        private async Task ShowSuccessMessage(string message)
            => await ThisSystemMessageBox.Show(Title, message, this, icon: MessageBox.Avalonia.Enums.Icon.Success);

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await ThisSystemMessageBox.Show("�^�C�g��", "���b�Z�[�W�{�b�N�X�o�����I", this);
        }

        private void OnClosed(object? sender, EventArgs args)
            => ViewModel().Save();

        private MainWindowViewModel ViewModel()
        {
            if (DataContext == null || !(DataContext is MainWindowViewModel))
                throw new InvalidOperationException();
            return (MainWindowViewModel)DataContext;
        }
    }
}
