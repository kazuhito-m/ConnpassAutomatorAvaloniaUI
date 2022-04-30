using ConnpassAutomator.Domain.Model.Profile;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace ConnpassAutomator.Infrastructure.Datasource.Profile
{
    public class ProfileDatasource : IProfileRepository
    {
        private readonly string PROFILE_FILE_NAME = "profile.json";

        public void Save(ConnpassProfile profile)
        {
            var json = JsonSerializer.Serialize(profile);
            Debug.WriteLine(json);
            File.WriteAllText(ProfilePath(), json);
        }

        public ConnpassProfile Load()
        {
            try
            {
                var json = File.ReadAllText(ProfilePath());
                var result = JsonSerializer.Deserialize<ConnpassProfile>(json);
                return result == null
                    ? ConnpassProfile.Default()
                    : result;
            }
            catch (Exception e)
            {
                return ConnpassProfile.Default();
            }
        }

        private string ProfilePath()
            => Path.Combine(Directory.GetCurrentDirectory(), PROFILE_FILE_NAME);
    }
}
