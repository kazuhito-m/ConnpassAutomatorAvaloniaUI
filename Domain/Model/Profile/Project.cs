namespace ConnpassAutomator.Domain.Model.Profile
{
    public class Project
    {
        public CopySource CopySource { get; set; } = new CopySource();
        public Changeset Changeset { get; set; } = new Changeset();
    }
}
