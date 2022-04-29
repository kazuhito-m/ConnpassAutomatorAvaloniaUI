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
            get => CurrentProject.CopySource.EventTitle;
            set => CurrentProject.CopySource.EventTitle = value;
        }

        public string EventTitle
        {
            get => CurrentProject.Changeset.EventTitle;
            set => CurrentProject.Changeset.EventTitle = value;
        }

        public string SubTitle
        {
            get => CurrentProject.Changeset.SubEventTitle;
            set => CurrentProject.Changeset.SubEventTitle = value;
        }

        public string EventDescription
        {
            get => CurrentProject.Changeset.Explanation;
            set => CurrentProject.Changeset.Explanation = value;
        }

        private ConnpassWillbeRenamed Profile()
        {
            if (profile != null) return profile;

            profile = repository.Load();
            SelectedProfileIndex = 0;
            CurrentProject = profile.Projects[SelectedProfileIndex];
            return profile;
        }

        public async void IncrimentVolNo()
        {
            await ThisSystemMessageBox.Show("Test", ",選択は:" + selectedProfileIndex);
        }

        public int SelectedProfileIndex
        {
            get => selectedProfileIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedProfileIndex, value);
                CurrentProject = Profile().Projects[value];
                ReflectProperty();
            }
        }

        // FIXME 以下は苦肉の策。プロパティを反応させるため、あえてもう一度入れている。
        private void ReflectProperty()
        {
            SearchEventTitle = CurrentProject.CopySource.EventTitle;
            EventTitle = CurrentProject.Changeset.EventTitle;
            SubTitle = CurrentProject.Changeset.SubEventTitle;
            EventDescription = CurrentProject.Changeset.Explanation;
        }

        private Project CurrentProject
        {
            get => currentProject;
            set => this.RaiseAndSetIfChanged(ref currentProject, value);
        }

        internal void Save()
            => repository.Save(Profile());
    }
}
