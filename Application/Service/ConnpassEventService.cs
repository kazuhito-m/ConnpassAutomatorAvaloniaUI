using ConnpassAutomator.Domain.Model.Connpass.Event;
using ConnpassAutomator.Domain.Model.Profile;
using System;

namespace ConnpassAutomator.Application.Service
{
    public class ConnpassEventService
    {
        public CreateEventResultState CreateEvent(Project project, Credential credential)
        {
            return CreateEventResultState.成功;
        }
    }
}
