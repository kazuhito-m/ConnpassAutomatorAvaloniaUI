using Presentation.Alert;
using Presentation.Models.Profile;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ProfileRepository repository = new ProfileRepository();

        private ConnpassWillbeRenamed? profile = null;
        private Changeset currentChangeset = new Changeset();

        private int selectedProfileIndex = 0;

        public List<string> FindProjectNames
        {
            get => Profile().Projects
                .Select(project => project.CopySource.EventTitle)
                .ToList();
        }

        public string SubTitle
        {
            get => currentChangeset.SubEventTitle;
            set => currentChangeset.SubEventTitle = value;
        }

        private ConnpassWillbeRenamed Profile()
        {
            if (profile == null)
            {
                profile = repository.Load();
                SelectedProfileIndex = 0;
                currentChangeset = profile.Projects[SelectedProfileIndex].Changeset;
            }
            return profile;
        }

        public async void IncrimentVolNo()
        {
            await ThisSystemMessageBox.Show("Test", ",‘I‘ð‚Í:" + selectedProfileIndex);
        }

        public int SelectedProfileIndex
        {
            get => selectedProfileIndex;
            set => this.RaiseAndSetIfChanged(ref selectedProfileIndex, value);
        }

        internal void Save()
            => repository.Save(Profile());
    }
}
