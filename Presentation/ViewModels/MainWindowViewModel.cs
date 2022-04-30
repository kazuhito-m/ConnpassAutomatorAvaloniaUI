using Presentation.Alert;
using Presentation.Models.Profile;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string copyBaseEventTitle = "";
        private string eventTitle = "";
        private string subTitle = "";
        private DateTimeOffset? startDate;
        private TimeSpan? startTime;
        private DateTimeOffset? endDate;
        private TimeSpan? endTime;
        private string eventDescription = "";

        private int selectedProfileIndex = 0;

        private readonly ProfileRepository repository = new ProfileRepository();

        public List<string> FindProjectNames
        {
            get
            {
                var profile = LoadProfile();
                var names = profile.Projects
                    .Select(project => project.CopySource.EventTitle)
                    .ToList();

                if (names.Count > 0)
                {
                    selectedProfileIndex = 0;
                    var selectedProject = profile.Projects[selectedProfileIndex];
                    SetInputValueOf(selectedProject);
                }

                return names;
            }
        }

        public int SelectedProfileIndex
        {
            get => selectedProfileIndex;
            set
            {
                var profile = SaveInputOfNowSelectedProject();

                var selectedProject = profile.Projects[value];
                SetInputValueOf(selectedProject);

                this.RaiseAndSetIfChanged(ref selectedProfileIndex, value);
            }
        }

        private ConnpassWillbeRenamed SaveInputOfNowSelectedProject()
        {
            var profile = LoadProfile();
            var lastSelectedProject = profile.Projects[selectedProfileIndex];
            GetInputValueTo(lastSelectedProject);
            SaveProfile(profile);
            return profile;
        }

        private void GetInputValueTo(Project project)
        {
            project.CopySource.EventTitle = CopyBaseEventTitle;
            project.Changeset.EventTitle = EventTitle;
            project.Changeset.SubEventTitle = SubTitle;
            project.Changeset.Explanation = EventDescription;
        }

        private void SetInputValueOf(Project project)
        {
            CopyBaseEventTitle = project.CopySource.EventTitle;
            EventTitle = project.Changeset.EventTitle;
            SubTitle = project.Changeset.SubEventTitle;
            EventDescription = project.Changeset.Explanation;
        }

        internal void Save()
            => SaveInputOfNowSelectedProject();

        private void SaveProfile(ConnpassWillbeRenamed profile)
            => repository.Save(profile);

        private ConnpassWillbeRenamed LoadProfile()
            => repository.Load();

        // Simple Properties

        public string CopyBaseEventTitle
        {
            get => copyBaseEventTitle;
            set => this.RaiseAndSetIfChanged(ref copyBaseEventTitle, value);
        }

        public string EventTitle
        {
            get => eventTitle;
            set => this.RaiseAndSetIfChanged(ref eventTitle, value);
        }

        public string SubTitle
        {
            get => subTitle;
            set => this.RaiseAndSetIfChanged(ref subTitle, value);
        }

        public DateTimeOffset? StartDate
        {
            get => startDate;
            set => this.RaiseAndSetIfChanged(ref startDate, value);

        }

        public TimeSpan? StartTime
        {
            get => startTime;
            set => this.RaiseAndSetIfChanged(ref startTime, value);

        }

        public DateTimeOffset? EndDate
        {
            get => endDate;
            set => this.RaiseAndSetIfChanged(ref endDate, value);

        }

        public TimeSpan? EndTime
        {
            get => endTime;
            set => this.RaiseAndSetIfChanged(ref endTime, value);

        }

        public string EventDescription
        {
            get => eventDescription;
            set => this.RaiseAndSetIfChanged(ref eventDescription, value);
        }

        public async void IncrimentVolNo()
        {
            await ThisSystemMessageBox.Show("Test", ",‘I‘ð‚Í:" + selectedProfileIndex);
        }
    }
}
