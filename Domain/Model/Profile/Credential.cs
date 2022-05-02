using ConnpassAutomator.Domain.Model.Profile.Password;
using System.Text.Json.Serialization;

namespace ConnpassAutomator.Domain.Model.Profile
{
    public class Credential
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        private readonly DecryptableEncrypter encripter = new();

        [JsonIgnore]
        public string PlainTextPassword
        {
            get => encripter.Decrypt(Password);
            set => Password = encripter.Encrypt(value);
        }
    }
}
