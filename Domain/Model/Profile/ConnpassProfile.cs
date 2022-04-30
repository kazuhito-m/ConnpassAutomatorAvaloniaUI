using System.Collections.Generic;

namespace ConnpassAutomator.Domain.Model.Profile
{
    public class ConnpassProfile
    {
        public Credential Credential { get; set; } = new();
        public IList<Project> Projects { get; set; } = new List<Project>();

        public static ConnpassProfile Default()
        {
            return new ConnpassProfile()
            {
                Projects = new List<Project>() {
                    new Project()
                    {
                        CopySource = new CopySource()
                        {
                            EventTitle = "未設定(入力・変更してください)"
                        }
                    }
                },
                Credential = new Credential()
            };
        }
    }
}
