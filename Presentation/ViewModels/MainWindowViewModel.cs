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
        private Project currentProject = new Project();

        private int selectedProfileIndex = 0;

        public List<string> FindProjectNames
        {
            get => Profile().Projects
                .Select(project => project.CopySource.EventTitle)
                .ToList();
        }

        public string SearchEventTitle
        {
            get => currentProject.CopySource.EventTitle;
            set => currentProject.CopySource.EventTitle = value;
        }

        public string EventTitle
        {
            get => currentProject.Changeset.EventTitle;
            set => currentProject.Changeset.EventTitle = value;
        }

        public string SubTitle
        {
            get => currentProject.Changeset.SubEventTitle;
            set => currentProject.Changeset.SubEventTitle = value;
        }

        public string EventDescription
        {
            get => currentProject.Changeset.Explanation;
            set => currentProject.Changeset.Explanation = value;
        }

        private ConnpassWillbeRenamed Profile()
        {
            if (profile != null) return profile;

            profile = repository.Load();
            SelectedProfileIndex = 0;
            currentProject = profile.Projects[SelectedProfileIndex];
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
