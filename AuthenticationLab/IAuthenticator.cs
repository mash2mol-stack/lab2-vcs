namespace AuthenticationLab
{
    public interface IAuthenticator
    {
        bool Authenticate(Credentials credentials);
    }
}