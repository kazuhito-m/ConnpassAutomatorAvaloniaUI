using ConnpassAutomator.Application.Service;
using ConnpassAutomator.Domain.Model.Profile;
using Presentation.Alert;
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
        private string eventDescription = "";
        private DateTimeOffset? startDate;
        private TimeSpan? startTime;
        private DateTimeOffset? endDate;
        private TimeSpan? endTime;

        private int selectedProfileIndex = 0;

        private readonly ConnpassEventService connpassEventService;
        private readonly ProfileService profileService;

        public async void IncrimentVolNo()
        {
            await ThisSystemMessageBox.Show("Test", ",‘I‘ð‚Í:" + selectedProfileIndex);
        }

        public List<string> FindProjectNames
        {
            get
            {
                var profile = profileService.Load();
                var names = profile.Projects
                    .Select(project => project.CopySource.EventTitle)
                    .ToList();

                if (names.Count > 0)
                {
                    selectedProfileIndex = 0;
                    var selectedProject = profile.Projects[selectedProfileIndex];
                    this.ReflectFrom(selectedProject);
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
                this.ReflectFrom(selectedProject);

                this.RaiseAndSetIfChanged(ref selectedProfileIndex, value);
            }
        }

        private ConnpassProfile SaveInputOfNowSelectedProject()
        {
            var profile = profileService.Load();
            var lastSelectedProject = profile.Projects[selectedProfileIndex];
            this.ReflectTo(lastSelectedProject);
            profileService.Save(profile);
            return profile;
        }

        internal void Save()
            => SaveInputOfNowSelectedProject();

        // Simple Get/Set Only Properties

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

        public MainWindowViewModel(ConnpassEventService connpassEventService, ProfileService profileService)
        {
            this.connpassEventService = connpassEventService;
            this.profileService = profileService;
        }
    }
}
