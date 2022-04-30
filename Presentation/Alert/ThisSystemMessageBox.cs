using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace Presentation.Alert
{
    static class ThisSystemMessageBox
    {
        internal static async Task Show(string title, string message, Window? window = null, ButtonEnum button = ButtonEnum.Ok, Icon icon = Icon.Info)
        {
            var param = new MessageBoxStandardParams
            {
                ButtonDefinitions = button,
                ContentTitle = title,
                ContentMessage = message,
                FontFamily = "Meiryo",
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Icon = icon
            };
            var messageBox = MessageBoxManager.GetMessageBoxStandardWindow(param);
            if (window == null) await messageBox.Show();
            else await messageBox.ShowDialog(window);
        }
    }
}
