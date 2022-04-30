using ConnpassAutomator.Domain.Model.Profile;

namespace ConnpassAutomator.Application.Service
{
    public class ProfileService
    {
        private readonly IProfileRepository repository;
        public void Save(ConnpassProfile profile)
            => repository.Save(profile);

        public ConnpassProfile Load()
            => repository.Load();

        public ProfileService(IProfileRepository repository)
            => this.repository = repository;
    }
}
