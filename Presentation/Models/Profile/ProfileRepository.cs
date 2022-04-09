using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;


namespace Presentation.Models.Profile
{
    class ProfileRepository
    {
        private readonly string PROFILE_FILE_NAME = "profile.json"; 

        public void Save(ConnpassWillbeRenamed profile)
        {
        }
        private string ProfilePath() 
            => Path.Combine(Directory.GetCurrentDirectory(), PROFILE_FILE_NAME);
    }
}
