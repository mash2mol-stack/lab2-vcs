namespace AuthenticationLab
{
    public class Credentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Fingerprint { get; set; }
        public string QrCode { get; set; }
        public string Token { get; set; }

        public Credentials()
        {
            Login = "";
            Password = "";
            Fingerprint = "";
            QrCode = "";
            Token = "";
        }
    }
}