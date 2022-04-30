namespace ConnpassAutomator.Domain.Model.Profile
{
    public interface IProfileRepository
    {
        void Save(ConnpassProfile profile);
        ConnpassProfile Load();
    }
}
