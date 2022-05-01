using ConnpassAutomator.Application.Service;
using ConnpassAutomator.Domain.Model.Connpass.Event;
using ConnpassAutomator.Domain.Model.Profile;
using Presentation.Alert;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.ViewModels
{
    public class CredentialEditWindowViewModel : ViewModelBase
    {
        private string userName = "";
        private string password = "";

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
        internal  void ReflectTo(MainWindowViewModel baseVm)
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
    }
}
