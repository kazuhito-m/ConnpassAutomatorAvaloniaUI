using System.Collections.Generic;

namespace Presentation.Models.Profile
{
    internal class ConnpassWillbeRenamed
    {
        internal ConnpassWillbeRenamed() { }
        internal Credential Credential { get; set; } = new();
        internal IList<Project> Projects { get; set; } = new List<Project>();

        internal static ConnpassWillbeRenamed Default()
        {
            var result = new ConnpassWillbeRenamed()
            {
                Projects = new List<Project>() { 
                    new Project()
                    {
                        CopySource = new CopySource()
                        {
                            EventTitle = "未設定(追加してください)"
                        }
                    }
                },
                Credential = new Credential()
            };
            return result;
        }
    }

    internal class Credential
    {
        internal Credential() { }
        internal string UserName { get; set; } = "";
        internal string Password { get; set; } = "";
    }

    internal class Project
    {
        internal Project() { }
        internal CopySource CopySource { get; set; } = new CopySource();
        internal Changeset Changeset { get; set; } = new Changeset();
    }

    internal class CopySource
    {
        internal CopySource() { }
        internal string EventTitle { get; set; } = "";
    }

    internal class Changeset
    {
        internal Changeset() { }
        internal string EventTitle { get; set; } = "";
        internal string SubEventTitle { get; set; } = "";
        internal string StartDate { get; set; } = "";
        internal string StartTime { get; set; } = "";
        internal string EndDate { get; set; } = "";
        internal string EndTime { get; set; } = "";
        internal string Explanation { get; set; } = "";
    }
}
