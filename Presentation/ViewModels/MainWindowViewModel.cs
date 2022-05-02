using Avalonia.Collections;
using ConnpassAutomator.Application.Service;
using ConnpassAutomator.Domain.Model.Connpass.Event;
using ConnpassAutomator.Domain.Model.Profile;
using ReactiveUI;
using System;
using System.Linq;

namespace Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string copyBaseEventTitle = "";
        private string eventTitle = "";
        private string subTitle = "";
        private string eventDescription = "";
        private DateTimeOffset? startDate;
        private TimeSpan? startTime;
        private DateTimeOffset? endDate;
        private TimeSpan? endTime;

        private string userName = "";
        private string password = "";

        private AvaloniaList<string> profileNames = new();
        private int selectedProfileIndex = 0;

        private readonly ConnpassEventService connpassEventService;
        private readonly ProfileService profileService;

        internal CreateEventResultState CreateEvent()
        {
            var profile = SaveNowInputState();
            var selectedProject = profile.Projects[selectedProfileIndex];

            return connpassEventService.CreateEvent(selectedProject, profile.Credential);
        }

        internal void AddNewProfile()
        {
            var profile = SaveNowInputState();
            var newProject = profile.AddNewProject();
            profileService.Save(profile);

            profileNames.Add(newProject.CopySource.EventTitle);
            this.RaisePropertyChanged("DeletableProfile");
            SelectedProfileIndex = profile.Projects.Count - 1;
        }

        internal void DeleteSelectedtProfile()
        {
            var profile = profileService.Load();
            profile.Projects.RemoveAt(selectedProfileIndex);
            profileService.Save(profile);

            var nowIndex = selectedProfileIndex;

            profileNames.RemoveAt(nowIndex);
            this.RaisePropertyChanged("DeletableProfile");

            var nextIndex = nowIndex;
            if (profileNames.Count == nextIndex) nextIndex--;
            SelectedProfileIndex = nextIndex;
        }

        internal void Plus7DayOfEventStartAndEndDateTime()
        {
            if (StartDate.HasValue) StartDate = Add7DayOf(StartDate.Value);
            if (EndDate.HasValue) EndDate = Add7DayOf(EndDate.Value);
        }

        private DateTimeOffset Add7DayOf(DateTimeOffset date)
            => date.AddDays(7);

        internal void IncrimentVolNo()
            => EventTitle = new EventTitle(EventTitle)
                .IncrimentVolNo()
                .Value;

        internal AvaloniaList<string> ProfileNames
        {
            get
            {
                if (profileNames.Count > 0) return profileNames;

                var profile = profileService.Load();
                var names = profile.Projects
                    .Select(project => project.CopySource.EventTitle)
                    .ToList();
                profileNames = new(names);

                selectedProfileIndex = 0;
                var selectedProject = profile.Projects[selectedProfileIndex];
                this.ReflectFrom(selectedProject);
                this.ReflectFrom(profile.Credential);

                return profileNames;
            }
        }

        internal int SelectedProfileIndex
        {
            get => selectedProfileIndex;
            set
            {
                if (value == -1)
                {
                    this.RaiseAndSetIfChanged(ref selectedProfileIndex, value);
                    return;
                }

                var profile = profileService.Load();

                if (selectedProfileIndex != -1)
                {
                    ReflectNowInputStateTo(profile);
                    profileService.Save(profile);
                }

                var selectedProject = profile.Projects[value];
                this.ReflectFrom(selectedProject);

                this.RaiseAndSetIfChanged(ref selectedProfileIndex, value);
            }
        }

        internal bool DeletableProfile { get => ProfileNames.Count > 1; }

        private ConnpassProfile SaveNowInputState()
        {
            var profile = profileService.Load();
            ReflectNowInputStateTo(profile);
            profileService.Save(profile);
            return profile;
        }

        private void ReflectNowInputStateTo(ConnpassProfile profile)
        {
            var lastSelectedProject = profile.Projects[selectedProfileIndex];
            this.ReflectTo(lastSelectedProject);
            this.ReflectTo(profile.Credential);
        }

        internal void Save()
            => SaveNowInputState();

        // Simple Get/Set Only Properties

        internal string CopyBaseEventTitle
        {
            get => copyBaseEventTitle;
            set => this.RaiseAndSetIfChanged(ref copyBaseEventTitle, value);
        }

        internal string EventTitle
        {
            get => eventTitle;
            set => this.RaiseAndSetIfChanged(ref eventTitle, value);
        }

        internal string SubTitle
        {
            get => subTitle;
            set => this.RaiseAndSetIfChanged(ref subTitle, value);
        }

        internal DateTimeOffset? StartDate
        {
            get => startDate;
            set => this.RaiseAndSetIfChanged(ref startDate, value);
        }

        internal TimeSpan? StartTime
        {
            get => startTime;
            set => this.RaiseAndSetIfChanged(ref startTime, value);
        }

        internal DateTimeOffset? EndDate
        {
            get => endDate;
            set => this.RaiseAndSetIfChanged(ref endDate, value);
        }

        internal TimeSpan? EndTime
        {
            get => endTime;
            set => this.RaiseAndSetIfChanged(ref endTime, value);
        }

        internal string EventDescription
        {
            get => eventDescription;
            set => this.RaiseAndSetIfChanged(ref eventDescription, value);
        }

        internal string UserName
        {
            get => userName;
            set => this.RaiseAndSetIfChanged(ref userName, value);
        }

        internal string Password
        {
            get => password;
            set => this.RaiseAndSetIfChanged(ref password, value);
        }

        public MainWindowViewModel() : this(null, null) { }

        public MainWindowViewModel(ConnpassEventService connpassEventService, ProfileService profileService)
        {
            this.connpassEventService = connpassEventService;
            this.profileService = profileService;
        }
    }
}
