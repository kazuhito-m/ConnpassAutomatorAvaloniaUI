using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Presentation.Extension.Avalonia
{
    internal static class ControlExtension
    {
        public static bool IsBlank(this TemplatedControl control)
        {
            if (control is TextBox)
            {
                var textBox = (TextBox)control;
                var value = textBox!.Text.Trim();
                if (value.Length > 0) return false;
            }
            if (control is DatePicker)
            {
                var datePicker = (DatePicker)control;
                if (datePicker.SelectedDate != null) return false;
            }
            if (control is TimePicker)
            {
                var timePicker = (TimePicker)control;
                if (timePicker.SelectedTime != null) return false;
            }

            return true;
        }
    }
}
