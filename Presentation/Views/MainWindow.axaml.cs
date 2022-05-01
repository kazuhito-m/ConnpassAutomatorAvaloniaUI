using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using ConnpassAutomator.Domain.Model.Connpass.Event;
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

            var result = ViewModel().CreateEvent();

            if (result == CreateEventResultState.成功) await ShowSuccessMessage("Connpassイベントの作成が完了しました。");
            else await ShowWarnMessage("Connpassイベントの作成に失敗しました。");
        }

        private async Task<bool> Validation()
        {
            if (await IsBlackInput("copyBaseEventTitle", "コピー元イベントタイトル")) return false;
            if (await IsBlackInput("title", "タイトル")) return false;
            if (await IsBlackInput("subTitle", "サブタイトル")) return false;
            if (await IsBlackInput("startDate", "開始日付")) return false;
            if (await IsBlackInput("startTime", "開始時刻")) return false;
            if (await IsBlackInput("endDate", "終了日付")) return false;
            if (await IsBlackInput("endTime", "開始時刻")) return false;
            if (await IsBlackInput("eventDescription", "イベントの説明")) return false;

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

            await ShowWarnMessage($"{fieldCaption} を入力してください。");
            input!.Focus();

            return true;
        }

        private async Task<bool> ValidationCredential()
        {
            var vm = ViewModel();
            if (vm.UserName.Trim().Length > 0
                && vm.Password.Trim().Length > 0) return true;
            await ShowWarnMessage("ログイン情報が未設定です。");
            // TODO ログイン情報を入力させる。
            return false;
        }

        private async Task ShowWarnMessage(string message)
            => await ThisSystemMessageBox.Show(Title, message, this, icon: MessageBox.Avalonia.Enums.Icon.Warning);

        private async Task ShowSuccessMessage(string message)
            => await ThisSystemMessageBox.Show(Title, message, this, icon: MessageBox.Avalonia.Enums.Icon.Success);

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await ThisSystemMessageBox.Show("タイトル", "メッセージボックス出せるよ！", this);
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
