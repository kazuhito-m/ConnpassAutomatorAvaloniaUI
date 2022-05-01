using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Presentation.Alert;
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
            if (!await Validation()) return;
            await ThisSystemMessageBox.Show("�^�C�g��", "umakuikanai", this);
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
            if (input is TextBox)
            {
                var textBox = (TextBox)input;
                var value = textBox!.Text.Trim();
                if (value.Length > 0) return false;
            }
            if (input is DatePicker)
            {
                var datePicker = (DatePicker)input;
                if (datePicker.SelectedDate != null) return false;
            }
            if (input is TimePicker)
            {
                var timePicker = (TimePicker)input;
                if (timePicker.SelectedTime != null) return false;
            }

            await ThisSystemMessageBox.Show(Title, $"{fieldCaption} ����͂��Ă��������B", this, icon: MessageBox.Avalonia.Enums.Icon.Warning);
            input!.Focus();

            return true;
        }

        private async Task<bool> ValidationCredential()
        {
            var vm = ViewModel;
            if (vm == null) return false;
            if (vm.UserName.Trim().Length > 0
                && vm.Password.Trim().Length > 0) return true;
            await ThisSystemMessageBox.Show(Title, "���O�C����񂪖��ݒ�ł��B", this, icon: MessageBox.Avalonia.Enums.Icon.Warning);
            // TODO ���O�C��������͂�����B
            return false;
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
