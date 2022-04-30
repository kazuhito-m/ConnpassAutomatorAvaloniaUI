using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace ConnpassAutomator.Domain.Model.Profile
{
    public class ProfileRepository
    {
        private readonly string PROFILE_FILE_NAME = "profile.json";

        public void Save(ConnpassWillbeRenamed profile)
        {
            var json = JsonSerializer.Serialize(profile);
            Debug.WriteLine(json);
            File.WriteAllText(ProfilePath(), json);
        }

        public ConnpassWillbeRenamed Load()
        {
            try
            {
                var json = File.ReadAllText(ProfilePath());
                var result = JsonSerializer.Deserialize<ConnpassWillbeRenamed>(json);
                return result == null
                    ? ConnpassWillbeRenamed.Default()
                    : result;
            }
            catch (Exception e)
            {
                return ConnpassWillbeRenamed.Default();
            }
        }

        private string ProfilePath()
            => Path.Combine(Directory.GetCurrentDirectory(), PROFILE_FILE_NAME);
    }
}
