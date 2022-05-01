using ReactiveUI;
using System.ComponentModel;

namespace Presentation.ViewModels
{
    public class CredentialEditWindowViewModel : ViewModelBase
    {
        private string userName = "";
        private string password = "";

        private bool commitable = false;

        private void ThisPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Commitable") return;
            Commitable = Validateion();
        }

        private bool Validateion()
            => UserName.Length > 6 && Password.Length > 6;

        internal void ReflectTo(MainWindowViewModel baseVm)
        {
            baseVm.UserName = UserName;
            baseVm.Password = Password;
        }

        internal static CredentialEditWindowViewModel Of(MainWindowViewModel baseVm)
            => new()
            {
                UserName = baseVm.UserName,
                Password = baseVm.Password
            };

        // Simple Get/Set Only Properties

        public string UserName
        {
            get => userName;
            set => this.RaiseAndSetIfChanged(ref userName, value);
        }

        public string Password
        {
            get => password;
            set => this.RaiseAndSetIfChanged(ref password, value);
        }

        public bool Commitable
        {
            get => commitable;
            set => this.RaiseAndSetIfChanged(ref commitable, value);
        }

        public CredentialEditWindowViewModel()
        {
            this.PropertyChanged += ThisPropertyChanged;
        }
    }
}
