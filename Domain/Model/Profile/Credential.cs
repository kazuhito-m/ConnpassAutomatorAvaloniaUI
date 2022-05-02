using ConnpassAutomator.Domain.Model.Profile.Password;

namespace ConnpassAutomator.Domain.Model.Profile
{
    public class Credential
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        private readonly DecryptableEncrypter encripter = new();

        public void SetPlainTextPassword(string plainTextPassword)
            => Password = encripter.Encrypt(plainTextPassword);

        public string GetPlainTextPassword()
            => encripter.Decrypt(Password);
    }
}
