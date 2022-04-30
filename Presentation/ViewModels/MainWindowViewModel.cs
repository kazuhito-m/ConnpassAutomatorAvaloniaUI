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
        private DateTimeOffset? startDate = new DateTimeOffset(DateTime.Now);
        private TimeSpan? startTime = DateTime.Now.Subtract(new DateTime(1970, 1, 9, 0, 0, 00));
        private DateTimeOffset? endDate = new DateTimeOffset(DateTime.Now);
        private TimeSpan? endTime = DateTime.Now.Subtract(new DateTime(1970, 1, 9, 0, 0, 00));
        private string eventDescription = "";

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
            CopyBaseEventTitle = CurrentProject.CopySource.EventTitle;
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
