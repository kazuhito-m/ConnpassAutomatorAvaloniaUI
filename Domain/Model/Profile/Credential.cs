namespace ConnpassAutomator.Domain.Model.Profile
{
    public class Credential
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public void SetPlainTextPassword(string plainTextPassword)
        {
            Password = plainTextPassword;
        }

        public string GetPlainTextPassword()
        {
            return Password;
        }
    }
}
